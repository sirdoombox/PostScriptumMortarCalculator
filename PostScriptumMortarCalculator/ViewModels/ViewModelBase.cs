using Stylet;

namespace PostScriptumMortarCalculator.ViewModels
{
    public class ViewModelBase<T> : Screen where T : PropertyChangedBase, new()
    {
        public T Model { get; set; } = new T();
    }
}