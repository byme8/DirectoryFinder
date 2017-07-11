using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using DirectoryFinder.Core.ViewModels;
using DirectoryFinder.Services;
using ReactiveUI;
using DirectoryFinder.Domain.Services;

namespace DirectoryFinder.ViewModels
{
    public class ErrorsViewModel : ViewModel
    {
        public ErrorsViewModel(IDirectorySearchHandler searchHandler)
        {
            this.Errors = new ObservableCollection<string>();
            searchHandler.Error.ObserveOn(RxApp.MainThreadScheduler).Subscribe(o => this.Errors.Add(o));

            this.Clear = ReactiveCommand.Create(() => this.Errors.Clear());
        }

        public ObservableCollection<string> Errors
        {
            get;
            private set;
        }

        public ReactiveCommand<Unit, Unit> Clear
        {
            get;
            private set;
        }
    }
}
