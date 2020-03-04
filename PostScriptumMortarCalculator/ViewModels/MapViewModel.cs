using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using PostScriptumMortarCalculator.Models;
using PostScriptumMortarCalculator.Utils;
using PostScriptumMortarCalculator.ViewModels.Base;
using Wpf.Controls.PanAndZoom;

namespace PostScriptumMortarCalculator.ViewModels
{
    public class MapViewModel : ViewModelBase<MapModel>
    {
        private ZoomBorder m_zoomBorder;

        private DispatcherTimer m_timer;

        private CalculatorViewModel m_calculatorViewModel;


        protected override void OnViewLoaded()
        {
            m_zoomBorder = View.FindChild<ZoomBorder>();
            m_timer = new DispatcherTimer {Interval = TimeSpan.FromMilliseconds(10)};
            m_timer.Tick += (_, __) => UpdateScale();
            m_timer.Start();
        }

        public void MouseDown(object _, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(m_zoomBorder.Child);
            switch (e.ChangedButton)
            {
                case MouseButton.Right:
                    Model.MortarPosition = new Vector2(pos.X, pos.Y);
                    break;
                case MouseButton.Left:
                    Model.TargetPosition = new Vector2(pos.X, pos.Y);
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