using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using DirectoryFinder.Core.ViewModels;
using DirectoryFinder.Domain.Services;
using ReactiveUI;

namespace DirectoryFinder.ViewModels
{
    public class TreeViewModel : ViewModel
    {
        private ItemViewModel selectedItem;

        public TreeViewModel(IUIHandler uiHandler)
        {
            this.Tree = new ObservableCollection<ItemViewModel>();
            uiHandler.ItemUpdated.ObserveOn(RxApp.MainThreadScheduler).Subscribe(item =>
            {
                this.Tree.Clear();
                this.Tree.Add(new ItemViewModel(item));
            });
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