using System.Threading;
using System.Windows;
using DirectoryFinder.ViewModels;

namespace DirectoryFinder.Services
{
    public class TreeHandler
    {
        private TreeViewModel treeViewModel;
        private DirectorySearchHandler searchHandler;

        public TreeHandler(TreeViewModel treeViewModel, DirectorySearchHandler searchHandler)
        {
            this.treeViewModel = treeViewModel;
            this.searchHandler = searchHandler;
        }

        public void StartHandeling(CancellationToken token)
        {
            new Thread(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    this.searchHandler.SearchFinishingEvent.WaitOne();
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.treeViewModel.Tree.Clear();
                        this.treeViewModel.Tree.Add(new ItemViewModel(this.searchHandler.Root));
                    });
                }
            }).Start();
        }
    }
}