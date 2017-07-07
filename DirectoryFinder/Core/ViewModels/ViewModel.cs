using ReactiveUI;

namespace DirectoryFinder.Core.ViewModels
{
    public class ViewModel : ReactiveObject
    {
    }

    public class ViewModel<TValue> : ViewModel
    {
        public TValue Value
        {
            get;
            set;
        }

        public ViewModel(TValue value)
        {
            this.Value = value;
        }
    }
}