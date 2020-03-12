using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PostScriptumMortarCalculator.Controls
{
    public class GridOverlayControl : Canvas
    {
        public static readonly DependencyProperty MapPixelsPerMeterProperty = DependencyProperty.Register("MapPixelsPerMeter",
            typeof(double), typeof(GridOverlayControl), new PropertyMetadata(0d, PropertyChangedCallback));

        public static readonly DependencyProperty MajorLineThicknessProperty = DependencyProperty.Register("MajorLineThickness",
            typeof(double), typeof(GridOverlayControl), new PropertyMetadata(0d));
        
        public static readonly DependencyProperty MinorLineThicknessProperty = DependencyProperty.Register("MinorLineThickness",
            typeof(double), typeof(GridOverlayControl), new PropertyMetadata(0d));
        
        public static readonly DependencyProperty MicroLineThicknessProperty = DependencyProperty.Register("MicroLineThickness",
            typeof(double), typeof(GridOverlayControl), new PropertyMetadata(0d));
        
        public double MapPixelsPerMeter
        {
            get => (double) GetValue(MapPixelsPerMeterProperty);
            set => SetValue(MapPixelsPerMeterProperty, value);
        }

        public double MajorLineThickness
        {
            get => (double) GetValue(MajorLineThicknessProperty);
            set => SetValue(MajorLineThicknessProperty, value);
        }

        public double MinorLineThickness
        {
            get => (double) GetValue(MinorLineThicknessProperty);
            set => SetValue(MinorLineThicknessProperty, value);
        }

        public double MicroLineThickness
        {
            get => (double) GetValue(MicroLineThicknessProperty);
            set => SetValue(MicroLineThicknessProperty, value);
        }
        
        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((GridOverlayControl)d).DrawGrid();
        }

        private void DrawGrid()
        {
            Children.Clear();
            // This value is initially set to 0 until it updates, which causes a deadlock
            if (MapPixelsPerMeter <= 0) return; 
            // Draw major grid lines
            for (double x = 0; x <= ActualWidth; x += 300 * MapPixelsPerMeter)
                Children.Add(CreateLine(x,x,0,ActualHeight, MajorLineThickness));
            for (double y = 0; y <= ActualHeight; y += 300 * MapPixelsPerMeter)
                Children.Add(CreateLine(0, ActualWidth, y, y, MajorLineThickness));
            // Draw minor grid lines
            for (double x = 0; x <= ActualWidth; x += 100 * MapPixelsPerMeter)
                Children.Add(CreateLine(x,x,0,ActualHeight, MinorLineThickness));
            for (double y = 0; y <= ActualHeight; y += 100 * MapPixelsPerMeter)
                Children.Add(CreateLine(0, ActualWidth, y, y, MinorLineThickness));
            // Draw micro grid lines
            for (double x = 0; x <= ActualWidth; x += 33.333 * MapPixelsPerMeter)
                Children.Add(CreateLine(x,x,0,ActualHeight, MicroLineThickness));
            for (double y = 0; y <= ActualHeight; y += 33.333 * MapPixelsPerMeter)
                Children.Add(CreateLine(0, ActualWidth, y, y, MicroLineThickness));
        }

        private Line CreateLine(double x1, double x2, double y1, double y2, double thickness)
        {
            return new Line
            {
                StrokeThickness = thickness,
                Stroke = Brushes.Black,
                X1 = x1,
                X2 = x2,
                Y1 = y1,
                Y2 = y2
            };
        }
    }
}