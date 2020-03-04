using System;
using System.Numerics;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using PostScriptumMortarCalculator.Data;
using PostScriptumMortarCalculator.Events;
using PostScriptumMortarCalculator.Extensions;
using PostScriptumMortarCalculator.Models;
using Stylet;
using Wpf.Controls.PanAndZoom;

namespace PostScriptumMortarCalculator.ViewModels
{
    public class MapViewModel : ViewModelBase<MapModel>
    {
        private ZoomBorder m_zoomBorder;

        private DispatcherTimer m_timer;

        private CalculatorViewModel m_calculatorViewModel;

        public double TestDistance { get; set; }

        public void LinkToCalculator(CalculatorViewModel calculatorViewModel)
        {
            m_calculatorViewModel = calculatorViewModel;
        }

        protected override void OnViewLoaded()
        {
            m_zoomBorder = View.FindChild<ZoomBorder>();
            m_timer = new DispatcherTimer {Interval = TimeSpan.FromMilliseconds(10)};
            m_timer.Tick += (_, __) => UpdateScale();
            m_timer.Start();
            m_calculatorViewModel.MapSelectionChanged += (data) =>
            {
                Model = new MapModel {Data = data};
                m_zoomBorder.Uniform();
                Model.MapSizeBoundsPixels =
                    (m_zoomBorder.Child.RenderSize.Width + m_zoomBorder.Child.RenderSize.Height) / 2d;
                Model.MapPixelsPerMeter = Model.MapSizeBoundsPixels.PixelBoundsToScaledMeters(Model.Data.Bounds);
            };
        }

        public void MouseDown(object _, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(m_zoomBorder.Child);
            switch (e.ChangedButton)
            {
                case MouseButton.Right:
                    Model.MortarPositionLeft = Math.Round(pos.X, 2);
                    Model.MortarPositionTop = Math.Round(pos.Y, 2);
                    break;
                case MouseButton.Left:
                    Model.TargetPositionLeft = Math.Round(pos.X, 2);
                    Model.TargetPositionTop = Math.Round(pos.Y, 2);
                    break;
            }

            if (!Model.IsMortarPositionSet || !Model.IsTargetPositionSet) return;
            TestDistance = Vector2.Distance(Model.MortarPositionMeters, Model.TargetPositionMeters);
            TestDistance = Math.Round(TestDistance, 2);
        }

        public void SizeChanged()
        {
            if (m_zoomBorder is null) return;
        }

        private void UpdateScale()
        {
            Model.IconScale = Clamp(30, 10000, 30 / m_zoomBorder.ZoomX);
        }

        private static double Clamp(double min, double max, double value)
        {
            return value <= min ? min : value >= max ? max : value;
        }

        public void MouseEntered()
        {
            m_zoomBorder?.Focus();
        }

        public void ZoomBorderKeyDown(object _, KeyEventArgs e)
        {
            if (e.Key == Key.R) m_zoomBorder?.Uniform();
        }
    }
}