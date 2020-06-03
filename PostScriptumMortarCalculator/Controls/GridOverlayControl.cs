using System.Collections.Generic;
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
            // This value is initially set to 0 until the map fully loads.
            if (MapPixelsPerMeter <= 0) return;
            
            var microIncrement = 33.333333333d * MapPixelsPerMeter;
            
            // Create divisions ahead of time so we can control what kind of line we draw where.
            var divisions = new List<double>();
            var currentCrawl = 0d;
            while ((currentCrawl += microIncrement) <= ActualWidth)
            {
                divisions.Add(currentCrawl);
            }

            for (var i = 1; i <= divisions.Count; i++)
            {
                var div = divisions[i-1];
                if (i % 9 == 0)
                {
                    DrawLine(div,div,0,ActualHeight, MajorLineThickness, Brushes.Black, 1);
                    DrawLine(0, ActualWidth, div, div, MajorLineThickness, Brushes.Black, 1);
                }
                else if (i % 3 == 0)
                {
                    DrawLine(div,div,0,ActualHeight, MinorLineThickness, Brushes.Black);
                    DrawLine(0, ActualWidth, div, div, MinorLineThickness, Brushes.Black);
                }
                else
                {
                    DrawLine(div,div,0,ActualHeight, MicroLineThickness, Brushes.White, -1);
                    DrawLine(0, ActualWidth, div, div, MicroLineThickness, Brushes.White, -1);
                }
            }
        }

        private void DrawLine(double x1, double x2, double y1, double y2, double thickness, Brush stroke, int zIndex = 0)
        {
            var line = new Line
            {
                StrokeThickness = thickness,
                Stroke = stroke,
                X1 = x1,
                X2 = x2,
                Y1 = y1,
                Y2 = y2,
                IsHitTestVisible = false
            };
            SetZIndex(line, zIndex);
            Children.Add(line);
        }
    }
}