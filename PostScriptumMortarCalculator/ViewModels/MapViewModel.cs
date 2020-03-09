using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using PostScriptumMortarCalculator.Events;
using PostScriptumMortarCalculator.Extensions;
using PostScriptumMortarCalculator.Models;
using PostScriptumMortarCalculator.Utils;
using Stylet;
using Wpf.Controls.PanAndZoom;

namespace PostScriptumMortarCalculator.ViewModels
{
    public class MapViewModel : Screen, IHandle<MortarChangedEvent>
    {
        private const string c_RESOURCE_PATH = "/PostScriptumMortarCalculator;component/Assets";

        #region Properties

        public RoundedVector2 TargetPositionPixels { get; set; }

        public RoundedVector2 MortarPositionPixels { get; set; }

        public double Angle =>
            RoundedVector2.Angle(MortarPositionPixels, TargetPositionPixels);

        public double MortarMinDistancePixels { get; private set; }


        public double HalfMortarMinDistancePixels =>
            -(MortarMinDistancePixels / 2d);

        public double MortarMaxDistancePixels { get; private set; }

        public double HalfMortarMaxDistancePixels =>
            -(MortarMaxDistancePixels / 2d);

        public double DistancePixels =>
            RoundedVector2.Distance(MortarPositionPixels, TargetPositionPixels);

        public RoundedVector2 TargetInnateSplashPixels { get; private set; }


        public RoundedVector2 TargetInnateSplashOffset =>
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

        public double Opacity { get; set; } = .25d;
        public string MapImageSource => c_RESOURCE_PATH + SelectedMap.MapPath;

        #endregion

        #region Private Fields

        private ZoomBorder m_zoomBorder;
        private Canvas m_canvas;
        private bool m_isMouseCaptured;
        private MouseButton m_capturedButton;
        private double m_mapPixelsPerMeter;
        private readonly IEventAggregator m_eventAggregator;

        #endregion

        #region Initialisation

        public MapViewModel(IReadOnlyList<MapDataModel> availableMaps,
            MortarDataModel defaultMortar,
            IEventAggregator eventAggregator)
        {
            m_eventAggregator = eventAggregator;
            m_eventAggregator.Subscribe(this);
            AvailableMaps.AddRange(availableMaps);
            SelectedMortar = defaultMortar;
            SelectedMap = AvailableMaps.First();
        }

        #endregion

        #region PropertyChanged Handlers

        public void OnSelectedMapChanged()
        {
            if (m_zoomBorder is null) return;
            Reset(true);
            m_mapPixelsPerMeter = m_zoomBorder.Child.RenderSize.Height.PixelBoundsToPixelsPerMeter(SelectedMap.Bounds);
        }

        public void OnTargetPositionPixelsChanged() => PublishUpdate();
        public void OnMortarPositionPixelsChanged() => PublishUpdate();

        private void PublishUpdate()
        {
            m_eventAggregator.Publish(new PositionChangedEvent(
                MortarPositionPixels.ToMetersScale(m_mapPixelsPerMeter),
                TargetPositionPixels.ToMetersScale(m_mapPixelsPerMeter), Angle));
        }

        public void OnSelectedMortarChanged()
        {
            MortarMinDistancePixels = SelectedMortar.MinRange.Distance.ToPixelScale(m_mapPixelsPerMeter) * 2d;
            MortarMaxDistancePixels = SelectedMortar.MaxRange.Distance.ToPixelScale(m_mapPixelsPerMeter) * 2d;
            NotifyOfPropertyChange(() => HalfMortarMaxDistancePixels);
            NotifyOfPropertyChange(() => HalfMortarMinDistancePixels);
            if (!IsTargetStillInRange()) TargetPositionPixels = default;
        }

        public void Handle(MortarChangedEvent message)
        {
            SelectedMortar = message.ActiveMortar;
        }

        #endregion

        #region UI Event Handlers

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

        #endregion

        #region Helpers

        private bool IsMousePosInValidRange(double posX, double posY)
        {
            var tempTarget = new RoundedVector2(posX, posY);
            var tempDist = RoundedVector2.Distance(MortarPositionPixels, tempTarget);
            return tempDist >= SelectedMortar.MinRange.Distance.ToPixelScale(m_mapPixelsPerMeter)
                   && tempDist <= SelectedMortar.MaxRange.Distance.ToPixelScale(m_mapPixelsPerMeter);
        }

        private bool IsTargetStillInRange()
        {
            var dist = RoundedVector2.Distance(MortarPositionPixels, TargetPositionPixels);
            return dist >= SelectedMortar.MinRange.Distance.ToPixelScale(m_mapPixelsPerMeter)
                   && dist <= SelectedMortar.MaxRange.Distance.ToPixelScale(m_mapPixelsPerMeter);
            ;
        }

        private void Reset(bool resetMapPixels = false)
        {
            MortarPositionPixels = default;
            TargetPositionPixels = default;
            if (!resetMapPixels) return;
            m_mapPixelsPerMeter = default;
        }

        private void RecalculateTargetSplash()
        {
            TargetInnateSplashPixels = RoundedVector2.LerpBetween(SelectedMortar.DispersionRadiusAtMinRange,
                    SelectedMortar.DispersionRadiusAtMaxRange,
                    SelectedMortar.PercentageBetweenMinAndMaxDistance(
                        DistancePixels.ToScaledMeters(m_mapPixelsPerMeter)))
                .ToPixelScale(m_mapPixelsPerMeter) * 2;
        }

        #endregion
    }
}