using System.Collections.ObjectModel;
using DirectoryFinder.Core.ViewModels;

namespace DirectoryFinder.ViewModels
{
    public class TreeViewModel : ViewModel
    {
        public TreeViewModel()
        {
            this.Tree = new ObservableCollection<ItemViewModel>();
        }

        public ObservableCollection<ItemViewModel> Tree
        {
            get;
        }
    }
}