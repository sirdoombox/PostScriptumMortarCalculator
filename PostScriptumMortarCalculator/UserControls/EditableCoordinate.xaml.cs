using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PostScriptumMortarCalculator.Models;

namespace PostScriptumMortarCalculator.UserControls
{
    public partial class EditableCoordinate
    {
        private static readonly Regex GridReferenceLetterPattern =
            new Regex("^[a-zA-Z]$", RegexOptions.Compiled);

        private static readonly Regex GridReferenceNumberPattern =
            new Regex("^[0-9]{1,2}$", RegexOptions.Compiled);

        public static readonly DependencyProperty LabelTextProperty =
            DependencyProperty.Register(nameof(LabelText), typeof(string), typeof(EditableCoordinate),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty GridReferenceLetterProperty =
            DependencyProperty.Register(nameof(GridReferenceLetter), typeof(string), typeof(EditableCoordinate),
                new FrameworkPropertyMetadata("A", FrameworkPropertyMetadataOptions.AffectsMeasure,
                    OnGridReferenceLetterChanged));

        public static readonly DependencyProperty GridReferenceNumberProperty =
            DependencyProperty.Register(nameof(GridReferenceNumber), typeof(int), typeof(EditableCoordinate),
                new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsMeasure,
                    OnGridReferenceNumberChanged));

        public static readonly DependencyProperty KeypadMajorProperty = DependencyProperty.Register(nameof(KeypadMajor),
            typeof(int), typeof(EditableCoordinate),
            new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsMeasure,
                OnKeypadMajorChanged));

        public static readonly DependencyProperty KeypadMinorProperty =
            DependencyProperty.Register(nameof(KeypadMinor), typeof(int), typeof(EditableCoordinate),
                new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsMeasure,
                    OnKeypadMinorChanged));

        public static readonly DependencyProperty CoordinatesProperty =
            DependencyProperty.Register(nameof(Coordinates), typeof(MapCoordinate),
                typeof(EditableCoordinate),
                new FrameworkPropertyMetadata(new MapCoordinate("A", 1, 1, 1),
                    FrameworkPropertyMetadataOptions.AffectsMeasure, OnCoordinateChanged));

        public string LabelText
        {
            get => (string) GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }

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

        public MapCoordinate Coordinates
        {
            get => (MapCoordinate) GetValue(CoordinatesProperty);
            set => SetValue(CoordinatesProperty, value);
        }

        public EditableCoordinate()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        private static void OnGridReferenceLetterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is null) return;
            if (!GridReferenceLetterPattern.IsMatch(e.NewValue.ToString()))
                d.SetValue(GridReferenceLetterProperty, e.OldValue);
            else
            {
                var coord = (MapCoordinate) d.GetValue(CoordinatesProperty);
                d.SetValue(CoordinatesProperty,
                    new MapCoordinate((string) e.NewValue, coord.GridNumber, coord.KeypadMajor, coord.KeypadMinor));
            }
        }

        private static void OnGridReferenceNumberChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!GridReferenceNumberPattern.IsMatch(e.NewValue.ToString()))
                d.SetValue(GridReferenceNumberProperty, e.OldValue);
            else
            {
                var coord = (MapCoordinate) d.GetValue(CoordinatesProperty);
                d.SetValue(CoordinatesProperty,
                    new MapCoordinate(coord.GridLetter, (int) e.NewValue, coord.KeypadMajor, coord.KeypadMinor));
            }
        }

        private static void OnKeypadMajorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!IsValidKeypadInt(e.NewValue))
                d.SetValue(KeypadMajorProperty, e.OldValue);
            else
            {
                var coord = (MapCoordinate) d.GetValue(CoordinatesProperty);
                d.SetValue(CoordinatesProperty,
                    new MapCoordinate(coord.GridLetter, coord.GridNumber, (int) e.NewValue, coord.KeypadMinor));
            }
        }

        private static void OnKeypadMinorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!IsValidKeypadInt(e.NewValue))
                d.SetValue(KeypadMinorProperty, e.OldValue);
            else
            {
                var coord = (MapCoordinate) d.GetValue(CoordinatesProperty);
                d.SetValue(CoordinatesProperty,
                    new MapCoordinate(coord.GridLetter, coord.GridNumber, coord.KeypadMajor, (int) e.NewValue));
            }
        }

        private static void OnCoordinateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var coord = (MapCoordinate) e.NewValue;
            d.SetValue(GridReferenceLetterProperty, coord.GridLetter);
            d.SetValue(GridReferenceNumberProperty, coord.GridNumber);
            d.SetValue(KeypadMajorProperty, coord.KeypadMajor);
            d.SetValue(KeypadMinorProperty, coord.KeypadMinor);
        }

        private void KeypadValidateInput(object _, TextCompositionEventArgs e) =>
            e.Handled = !IsValidKeypadInt(e.Text);

        private void GridReferenceLetterValidateInput(object _, TextCompositionEventArgs e) =>
            e.Handled = !GridReferenceLetterPattern.IsMatch(e.Text);

        private void GridReferenceNumberValidateInput(object _, TextCompositionEventArgs e) =>
            e.Handled = !IsValidKeypadInt(e.Text);

        private static bool IsValidKeypadInt(object value)
        {
            var isInt = int.TryParse(value.ToString(), out var i);
            if (!isInt) return false;
            return i >= 1 && i <= 9;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox) sender).MaxLength != ((TextBox) sender).Text.Length) return;
            var ue = (FrameworkElement) e.OriginalSource;
            e.Handled = true;
            ue.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
}