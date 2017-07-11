using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using DirectoryFinder.Data;
using DirectoryFinder.Domain.Services;

namespace DirectoryFinder.Services
{
    public class UIHandler : IUIHandler
    {
        private IDirectorySearchHandler searchHandler;
        private Subject<Item> itemUpdated;

        public IObservable<Item> ItemUpdated
        {
            get
            {
                return this.itemUpdated.AsObservable();
            }
        }

        public UIHandler(IDirectorySearchHandler searchHandler)
        {
            this.searchHandler = searchHandler;
            this.itemUpdated = new Subject<Item>();
        }

        public void StartHandeling(CancellationToken token)
        {
            var thread = new Thread(() =>
            {
                while (true)
                {
                    this.searchHandler.NewSearchEvent.WaitOne();
                    this.searchHandler.SearchFinishedEvent.WaitOne();

                    this.itemUpdated.OnNext(this.searchHandler.Root);
                }
            });
            thread.Start();
            token.Register(() => thread.Abort());
        }
    }
}