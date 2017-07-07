using System.Threading;
using System.Windows;
using DirectoryFinder.ViewModels;

namespace DirectoryFinder.Services
{
    public class UIHandler
    {
        private TreeViewModel treeViewModel;
        private DirectorySearchHandler searchHandler;

        public UIHandler(TreeViewModel treeViewModel, DirectorySearchHandler searchHandler)
        {
            this.treeViewModel = treeViewModel;
            this.searchHandler = searchHandler;
        }

        public void StartHandeling(CancellationToken token)
        {
            var thread = new Thread(() =>
            {
                while (true)
                {
                    this.searchHandler.NewSearchEvent.WaitOne();
                    this.searchHandler.SearchFinishedEvent.WaitOne();

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.treeViewModel.Tree.Clear();
                        this.treeViewModel.Tree.Add(new ItemViewModel(this.searchHandler.Root));
                    });
                }
            });
            thread.Start();
            token.Register(() => thread.Abort());
        }
    }
}