using System.Linq;
using System.Windows.Input;
using MahApps.Metro.Controls;
using PostScriptumMortarCalculator.Extensions;
using PostScriptumMortarCalculator.Models;
using PostScriptumMortarCalculator.Services;
using PostScriptumMortarCalculator.Utils;
using PostScriptumMortarCalculator.ViewModels.Base;
using PropertyChanged;
using Wpf.Controls.PanAndZoom;

namespace PostScriptumMortarCalculator.ViewModels
{
    public class MapViewModel : ViewModelBase<MapModel>
    {
        private ZoomBorder m_zoomBorder;

        public MapViewModel(DataResourceService dataResource)
        {
            Model.AvailableMaps.AddRange(dataResource.GetAvailableMapData());
        }
        
        protected override void OnViewLoaded()
        {
            m_zoomBorder = View.FindChild<ZoomBorder>();
            m_zoomBorder.Loaded += (_, __) =>
            {
                Model.SelectedMap = Model.AvailableMaps.First();
            };
        }

        public void MouseDown(object _, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(m_zoomBorder.Child);
            switch (e.ChangedButton)
            {
                case MouseButton.Right:
                    Model.MortarPosition = new RoundedVector2(pos.X, pos.Y);
                    break;
                case MouseButton.Left:
                    Model.TargetPosition = new RoundedVector2(pos.X, pos.Y);
                    break;
            }
        }
        
        [SuppressPropertyChangedWarnings]
        public void OnMapSelectionChanged()
        {
            Model.Reset(true);
            Model.MapPixelsPerMeter = m_zoomBorder.Child.RenderSize.Height.PixelBoundsToPixelsPerMeter(Model.SelectedMap.Bounds);
            Model.OnSelectedMortarChanged();
        }
        
        // Filthy hack because the first time the canvas is initialised it resizes, but never after for no clear reason.
        public void CanvasSizeChanged()
        {
            OnMapSelectionChanged();
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
            switch (e.Key)
            {
                case Key.R:
                    m_zoomBorder?.Uniform();
                    Model.Reset();
                    break;
                case Key.H:
                    Model.IsHelpVisible = !Model.IsHelpVisible;
                    break;
            }
        }
    }
}