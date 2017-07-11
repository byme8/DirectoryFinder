using System.Reactive;
using System.Threading;
using DirectoryFinder.Core.ViewModels;
using DirectoryFinder.Domain.Providers;
using DirectoryFinder.Domain.Services;
using ReactiveUI;

namespace DirectoryFinder.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel(IDirectorySearchHandler handler, IFolderPathProvider folderPathProvider)
        {
            this.StartSearch = ReactiveCommand.Create(() =>
            {
                var path = folderPathProvider.GetPath();
                if (string.IsNullOrWhiteSpace(path))
                {
                    return;
                }

                this.CancellationTokenSource = new CancellationTokenSource();
                handler.StartHandler(path, this.CancellationTokenSource.Token);
            });

            this.StopSearch = ReactiveCommand.Create(() =>
            {
                if (this.CancellationTokenSource == null || this.CancellationTokenSource.IsCancellationRequested)
                    return;

                this.CancellationTokenSource.Cancel();
            });
        }

        public ReactiveCommand<Unit, Unit> StartSearch
        {
            get;
            private set;
        }

        private CancellationTokenSource CancellationTokenSource
        {
            get;
            set;
        }

        public ReactiveCommand<Unit, Unit> StopSearch
        {
            get;
            private set;
        }
    }
}