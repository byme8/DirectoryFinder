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
        public TreeViewModel(Services.DirectoryService finder)
        {
            this.Tree = new ObservableCollection<Item>();

            finder.NewSearch.
                ObserveOn(RxApp.MainThreadScheduler).
                Subscribe(_ => this.Tree.Clear());

            finder.NewItem.
                Buffer(TimeSpan.FromSeconds(1)).
                ObserveOn(RxApp.MainThreadScheduler).
                Subscribe(items =>
                {
                    foreach (var parentItemsGroup in items.GroupBy(o => o.Parent))
                    {
                        if (parentItemsGroup.Key == null)
                        {
                            foreach (var item in parentItemsGroup)
                            {
                                this.Tree.Add(item);
                            }
                        }

                    }
                    this.RaisePropertyChanged("Tree");
                });
        }

        public ObservableCollection<Item> Tree
        {
            get;
        }
    }
}
