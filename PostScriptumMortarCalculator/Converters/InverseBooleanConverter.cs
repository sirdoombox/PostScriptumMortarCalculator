namespace PostScriptumMortarCalculator.Converters
{
    public class InverseBooleanConverter : BooleanConverterBase<bool>
    {
        public InverseBooleanConverter() : base(false, true) { }
    }
}