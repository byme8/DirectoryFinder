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

        public ItemViewModel SelectedItem
        {
            get
            {
                return this.selectedItem;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this.selectedItem, value);
            }
        }
    }
}