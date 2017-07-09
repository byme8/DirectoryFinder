using System.Reactive;
using System.Threading;
using System.Windows.Forms;
using DirectoryFinder.Core.ViewModels;
using DirectoryFinder.Services;
using ReactiveUI;

namespace DirectoryFinder.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel(DirectorySearchHandler finder)
        {
            this.StartSearch = ReactiveCommand.Create(() =>
            {
                using (var folderBrowserDialog = new FolderBrowserDialog())
                {
                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        this.CancellationTokenSource = new CancellationTokenSource();
                        finder.StartHandler(folderBrowserDialog.SelectedPath, this.CancellationTokenSource.Token);
                    }
                }
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