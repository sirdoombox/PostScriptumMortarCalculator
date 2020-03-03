using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PostScriptumMortarCalculator.Controls
{
    public class MapContainer : Border
    {
        private UIElement m_child = null;
        private Point m_origin;
        private Point m_start;

        public static readonly RoutedEvent MapSelectionEvent = EventManager.RegisterRoutedEvent("MapSelection",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MapContainer));

        public event RoutedEventHandler MapSelection
        {
            add => AddHandler(MapSelectionEvent, value);
            remove => RemoveHandler(MapSelectionEvent, value);
        }

        private TranslateTransform GetTranslateTransform(UIElement element)
        {
            return (TranslateTransform) ((TransformGroup) element.RenderTransform)
                .Children.First(tr => tr is TranslateTransform);
        }

        private ScaleTransform GetScaleTransform(UIElement element)
        {
            return (ScaleTransform) ((TransformGroup) element.RenderTransform)
                .Children.First(tr => tr is ScaleTransform);
        }

        public override UIElement Child
        {
            get { return base.Child; }
            set
            {
                if (value != null && value != Child)
                    Initialize(value);
                base.Child = value;
            }
        }

        public void Initialize(UIElement element)
        {
            m_child = element;
            if (m_child == null) return;
            var group = new TransformGroup();
            var st = new ScaleTransform();
            @group.Children.Add(st);
            var tt = new TranslateTransform();
            @group.Children.Add(tt);
            m_child.RenderTransform = @group;
            m_child.RenderTransformOrigin = new Point(0.0, 0.0);
            MouseWheel += OnMouseWheel;
            MouseDown += OnMouseDown;
            MouseUp += OnMouseUp;
            MouseMove += OnMouseMove;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var point = e.GetPosition(this);
            // Only interested in these events.
            // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
            switch (e.ChangedButton)
            {
                case MouseButton.Left:
                    LeftButtonDown(point);
                    break;
                case MouseButton.Middle:
                    MiddleMouseDown(point);
                    break;
                case MouseButton.Right:
                    RightButtonDown(point);
                    // IMPORTANT: Create events to bind to.
                    break;
            }
        }

        private void RightButtonDown(Point point)
        {
            var args = new MapSelectionEventArgs(MapSelectionEventType.Target)
            {
                RoutedEvent = MapSelectionEvent
            };
            RaiseEvent(args);
        }

        private void LeftButtonDown(Point point)
        {
            var args = new MapSelectionEventArgs(MapSelectionEventType.Mortar)
            {
                RoutedEvent = MapSelectionEvent
            };
            RaiseEvent(args);
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
                MiddleMouseUp();
        }

        private void MiddleMouseUp()
        {
            if (m_child == null) return;
            m_child.ReleaseMouseCapture();
            Cursor = Cursors.Arrow;
        }

        private void MiddleMouseDown(Point mouseDown)
        {
            if (m_child == null) return;
            var tt = GetTranslateTransform(m_child);
            m_start = mouseDown;
            m_origin = new Point(tt.X, tt.Y);
            Cursor = Cursors.Hand;
            m_child.CaptureMouse();
        }

        public void Reset()
        {
            if (m_child == null) return;
            // reset zoom
            var st = GetScaleTransform(m_child);
            st.ScaleX = 1.0;
            st.ScaleY = 1.0;

            // reset pan
            var tt = GetTranslateTransform(m_child);
            tt.X = 0.0;
            tt.Y = 0.0;
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (m_child == null) return;
            var st = GetScaleTransform(m_child);
            var tt = GetTranslateTransform(m_child);

            var zoom = e.Delta > 0 ? .2 : -.2;
            if (!(e.Delta > 0) && (st.ScaleX < .4 || st.ScaleY < .4))
                return;

            var relative = e.GetPosition(m_child);

            var absoluteX = relative.X * st.ScaleX + tt.X;
            var absoluteY = relative.Y * st.ScaleY + tt.Y;

            st.ScaleX += zoom;
            st.ScaleY += zoom;

            tt.X = absoluteX - relative.X * st.ScaleX;
            tt.Y = absoluteY - relative.Y * st.ScaleY;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (m_child == null) return;
            if (!m_child.IsMouseCaptured) return;
            var tt = GetTranslateTransform(m_child);
            var v = m_start - e.GetPosition(this);
            tt.X = m_origin.X - v.X;
            tt.Y = m_origin.Y - v.Y;
        }
    }
}