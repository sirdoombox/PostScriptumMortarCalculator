using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PostScriptumMortarCalculator.Events;
using PostScriptumMortarCalculator.Extensions;
using PostScriptumMortarCalculator.Models;
using PostScriptumMortarCalculator.Services;
using PostScriptumMortarCalculator.Utils;
using Stylet;
using Wpf.Controls.PanAndZoom;

namespace PostScriptumMortarCalculator.ViewModels
{
    public class MapViewModel : Screen, IHandle<MortarChangedEvent>
    {
        public RoundedVector2 TargetPositionPixels { get; set; }

        public RoundedVector2 MortarPositionPixels { get; set; }

        public double Angle =>
            RoundedVector2.Angle(MortarPositionPixels, TargetPositionPixels);

        public double MortarMinDistancePixels { get; private set; }

        public double HalfMortarMinDistancePixels =>
            MortarMinDistancePixels / 2d;

        public double MortarMaxDistancePixels { get; private set; }

        public double HalfMortarMaxDistancePixels =>
            MortarMaxDistancePixels / 2d;

        public double DistancePixels =>
            RoundedVector2.Distance(MortarPositionPixels, TargetPositionPixels);

        public RoundedVector2 TargetInnateSplashPixels { get; private set; }


        public RoundedVector2 HalfTargetInnateSplashPixels =>
            -(TargetInnateSplashPixels / 2d);

        public bool IsMortarPositionSet =>
            !MortarPositionPixels.Equals(default);

        public bool IsTargetPositionSet =>
            !TargetPositionPixels.Equals(default);

        public bool IsMortarAndTargetSet =>
            IsMortarPositionSet && IsTargetPositionSet;

        public BindableCollection<MapDataModel> AvailableMaps { get; set; } =
            new BindableCollection<MapDataModel>();

        public MapDataModel SelectedMap { get; set; }

        public MortarDataModel SelectedMortar { get; private set; }
        
        public double MapPixelsPerMeter { get; set; }
        
        public double Opacity { get; set; }
        
        public string MapImageSource => c_RESOURCE_PATH + SelectedMap.MapImagePath;

        private const string c_RESOURCE_PATH = "/PostScriptumMortarCalculator;component/Assets";

        private ZoomBorder m_zoomBorder;
        private Canvas m_canvas;
        private bool m_isMouseCaptured;
        private MouseButton m_capturedButton;
        private readonly IEventAggregator m_eventAggregator;
        private readonly ConfigService m_configService;
        private readonly UserConfigModel m_configModel;

        public MapViewModel(IReadOnlyList<MapDataModel> availableMaps,
            MortarDataModel defaultMortar,
            ConfigService configService,
            IEventAggregator eventAggregator)
        {
            m_eventAggregator = eventAggregator;
            m_eventAggregator.Subscribe(this);
            AvailableMaps.AddRange(availableMaps);
            m_configService = configService;
            m_configModel = configService.ActiveConfig;
            SelectedMortar = defaultMortar;
            SelectedMap = string.IsNullOrWhiteSpace(m_configModel.LastMapName)
                ? AvailableMaps.First()
                : AvailableMaps.First(x => x.Name == m_configModel.LastMapName);
            Opacity = m_configModel.Opacity;
        }

        public void OnSelectedMapChanged()
        {
            if (m_zoomBorder is null) return;
            Reset(true);
            MapPixelsPerMeter = m_zoomBorder.Child.RenderSize.Height.PixelBoundsToPixelsPerMeter(SelectedMap.Bounds);
            m_configModel.LastMapName = SelectedMap.Name;
            m_configService.SerialiseUserConfig();
        }

        public void OnOpacityChanged()
        {
            m_configModel.Opacity = Opacity;
            m_configService.SerialiseUserConfig();
        }

        public void OnTargetPositionPixelsChanged() => PublishUpdate();
        public void OnMortarPositionPixelsChanged() => PublishUpdate();

        private void PublishUpdate()
        {
            m_eventAggregator.Publish(new PositionChangedEvent(
                MortarPositionPixels.ToMetersScale(MapPixelsPerMeter),
                TargetPositionPixels.ToMetersScale(MapPixelsPerMeter), Angle, SelectedMap.Bounds));
        }

        public void OnSelectedMortarChanged()
        {
            MortarMinDistancePixels = SelectedMortar.MinRange.Distance.ToPixelScale(MapPixelsPerMeter) * 2d;
            MortarMaxDistancePixels = SelectedMortar.MaxRange.Distance.ToPixelScale(MapPixelsPerMeter) * 2d;
            NotifyOfPropertyChange(() => HalfMortarMaxDistancePixels);
            NotifyOfPropertyChange(() => HalfMortarMinDistancePixels);
            RecalculateTargetSplash();
            if (!IsTargetStillInRange()) TargetPositionPixels = default;
        }

        public void Handle(MortarChangedEvent message)
        {
            SelectedMortar = message.ActiveMortar;
        }

        public void CanvasLoaded(object sender, RoutedEventArgs _)
        {
            m_canvas = (Canvas) sender;
            m_zoomBorder = (ZoomBorder) m_canvas.Parent;
            OnSelectedMapChanged();
            OnSelectedMortarChanged();
        }

        public void ZoomBorderKeyDown(object _, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.R:
                    m_zoomBorder?.Uniform();
                    Reset();
                    break;
            }
        }

        public void MouseDown(object _, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(m_zoomBorder.Child);
            switch (e.ChangedButton)
            {
                case MouseButton.Right:
                    m_isMouseCaptured = true;
                    m_capturedButton = MouseButton.Right;
                    break;
                case MouseButton.Left:
                    m_isMouseCaptured = true;
                    m_capturedButton = MouseButton.Left;
                    break;
            }
        }

        public void MouseMove(object _, MouseEventArgs e)
        {
            if (!m_isMouseCaptured) return;
            var pos = e.GetPosition(m_zoomBorder.Child);
            switch (m_capturedButton)
            {
                case MouseButton.Right:
                    MortarPositionPixels = new RoundedVector2(pos.X, pos.Y);
                    break;
                case MouseButton.Left:
                    if (!IsMortarPositionSet) break;
                    if (!IsMousePosInValidRange(pos.X, pos.Y)) break;
                    TargetPositionPixels = new RoundedVector2(pos.X, pos.Y);
                    RecalculateTargetSplash();
                    break;
            }
        }

        public void MouseUp(object _, MouseButtonEventArgs e)
        {
            if (!m_isMouseCaptured) return;
            var pos = e.GetPosition(m_zoomBorder.Child);
            switch (e.ChangedButton)
            {
                case MouseButton.Right:
                    m_isMouseCaptured = false;
                    m_capturedButton = default;
                    MortarPositionPixels = new RoundedVector2(pos.X, pos.Y);
                    if (!IsTargetStillInRange()) TargetPositionPixels = default;
                    break;
                case MouseButton.Left:
                    m_isMouseCaptured = false;
                    m_capturedButton = default;
                    if (!IsMortarPositionSet) break;
                    if (!IsMousePosInValidRange(pos.X, pos.Y)) break;
                    TargetPositionPixels = new RoundedVector2(pos.X, pos.Y);
                    RecalculateTargetSplash();
                    break;
            }
        }

        public void MouseEntered()
        {
            m_zoomBorder?.Focus();
        }

        // Filthy hack because the first time the canvas is initialised it resizes, but never after for no clear reason.
        public void CanvasSizeChanged()
        {
            OnSelectedMapChanged();
            OnSelectedMortarChanged();
        }

        private bool IsMousePosInValidRange(double posX, double posY)
        {
            var tempTarget = new RoundedVector2(posX, posY);
            var tempDist = RoundedVector2.Distance(MortarPositionPixels, tempTarget);
            return tempDist >= SelectedMortar.MinRange.Distance.ToPixelScale(MapPixelsPerMeter)
                   && tempDist <= SelectedMortar.MaxRange.Distance.ToPixelScale(MapPixelsPerMeter);
        }

        private bool IsTargetStillInRange()
        {
            var dist = RoundedVector2.Distance(MortarPositionPixels, TargetPositionPixels);
            return dist >= SelectedMortar.MinRange.Distance.ToPixelScale(MapPixelsPerMeter)
                   && dist <= SelectedMortar.MaxRange.Distance.ToPixelScale(MapPixelsPerMeter);
            ;
        }

        private void Reset(bool resetMapPixels = false)
        {
            MortarPositionPixels = default;
            TargetPositionPixels = default;
            if (!resetMapPixels) return;
            MapPixelsPerMeter = default;
        }

        private void RecalculateTargetSplash()
        {
            TargetInnateSplashPixels = RoundedVector2.LerpBetween(SelectedMortar.DispersionRadiusAtMinRange,
                    SelectedMortar.DispersionRadiusAtMaxRange,
                    SelectedMortar.PercentageBetweenMinAndMaxDistance(
                        DistancePixels.ToScaledMeters(MapPixelsPerMeter)))
                .ToPixelScale(MapPixelsPerMeter) * 2;
        }
    }
}