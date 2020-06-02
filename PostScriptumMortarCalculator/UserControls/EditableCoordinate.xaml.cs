using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PostScriptumMortarCalculator.UserControls
{
    public partial class EditableCoordinate
    {
        private static readonly Regex GridReferenceLetterPattern = 
            new Regex("^[a-zA-Z]$", RegexOptions.Compiled);
        private static readonly Regex GridReferenceNumberPattern = 
            new Regex("^[0-9]{1,2}$", RegexOptions.Compiled);
        
        public static readonly DependencyProperty GridReferenceLetterProperty =
            DependencyProperty.Register(nameof(GridReferenceLetter), typeof(string), typeof(EditableCoordinate),
                new FrameworkPropertyMetadata("A", FrameworkPropertyMetadataOptions.AffectsMeasure,
                     OnGridReferenceLetterChanged));
        
        public static readonly DependencyProperty GridReferenceNumberProperty =
            DependencyProperty.Register(nameof(GridReferenceNumber), typeof(int), typeof(EditableCoordinate),
                new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsMeasure,
                    OnGridReferenceNumberChanged));


        public static readonly DependencyProperty KeypadMajorProperty = DependencyProperty.Register(nameof(KeypadMajor), typeof(int), typeof(EditableCoordinate),
                new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsMeasure,
                    OnKeypadMajorChanged));


        public static readonly DependencyProperty KeypadMinorProperty =
            DependencyProperty.Register(nameof(KeypadMinor), typeof(int), typeof(EditableCoordinate),
                new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsMeasure,
                    OnKeypadMinorChanged));

        public string GridReferenceLetter
        {
            get => (string) GetValue(GridReferenceLetterProperty);
            set => SetValue(GridReferenceLetterProperty, value);
        }
        
        public int GridReferenceNumber
        {
            get => (int) GetValue(GridReferenceNumberProperty);
            set => SetValue(GridReferenceNumberProperty, value);
        }

        public int KeypadMajor
        {
            get => (int) GetValue(KeypadMajorProperty);
            set => SetValue(KeypadMajorProperty, value);
        }

        public int KeypadMinor
        {
            get => (int) GetValue(KeypadMinorProperty);
            set => SetValue(KeypadMinorProperty, value);
        }

        public EditableCoordinate()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        private static void OnKeypadMajorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!IsValidKeypadInt(e.NewValue))
                d.SetValue(KeypadMajorProperty, e.OldValue);
        }

        private static void OnKeypadMinorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!IsValidKeypadInt(e.NewValue))
                d.SetValue(KeypadMinorProperty, e.OldValue);
        }
        
        private static void OnGridReferenceLetterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!GridReferenceLetterPattern.IsMatch(e.NewValue.ToString()))
                d.SetValue(GridReferenceLetterProperty, e.OldValue);
        }
        private static void OnGridReferenceNumberChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!GridReferenceNumberPattern.IsMatch(e.NewValue.ToString()))
                d.SetValue(GridReferenceNumberProperty, e.OldValue);
        }
        
        private void KeypadValidateInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValidKeypadInt(e.Text);
        }

        private void GridReferenceLetterValidateInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !GridReferenceLetterPattern.IsMatch(e.Text);
        }
        
        private void GridReferenceNumberValidateInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValidKeypadInt(e.Text);
        }

        private void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e)
        {
            DependencyObject parent = e.OriginalSource as UIElement;
            while (parent != null && !(parent is TextBox))
                parent = VisualTreeHelper.GetParent(parent);
            if (parent == null) return;
            var textBox = (TextBox) parent;
            if (textBox.IsKeyboardFocusWithin) return;
            textBox.Focus();
            e.Handled = true;
        }
        
        private static bool IsValidKeypadInt(object value)
        {
            var isInt = int.TryParse(value.ToString(), out var i);
            if (!isInt) return false;
            return i >= 1 && i <= 9;
        }

        private void SelectAllText(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is TextBox textBox)
                textBox.SelectAll();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox) sender).MaxLength != ((TextBox) sender).Text.Length) return;
            var ue = (FrameworkElement)e.OriginalSource;
            e.Handled = true;
            ue.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
}