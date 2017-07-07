using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using DirectoryFinder.Core.ViewModels;
using DirectoryFinder.Data;
using ReactiveUI;

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
