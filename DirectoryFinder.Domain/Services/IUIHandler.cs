using System;
using System.Threading;
using DirectoryFinder.Data;

namespace DirectoryFinder.Domain.Services
{
    public interface IUIHandler
    {
        IObservable<Item> ItemUpdated
        {
            get;
        }

        void StartHandeling(CancellationToken token);
    }
}