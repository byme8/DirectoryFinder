using System.Collections.ObjectModel;
using DirectoryFinder.Core.ViewModels;
using ReactiveUI;

namespace DirectoryFinder.ViewModels
{
    public class TreeViewModel : ViewModel
    {
        private ItemViewModel selectedItem;

        public TreeViewModel()
        {
            this.Tree = new ObservableCollection<ItemViewModel>();
        }

        public ObservableCollection<ItemViewModel> Tree
        {
            get;
            private set;
        }
    }
}