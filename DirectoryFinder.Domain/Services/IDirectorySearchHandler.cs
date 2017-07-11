using System;
using System.Threading;
using DirectoryFinder.Data;

namespace DirectoryFinder.Domain.Services
{
    public interface IDirectorySearchHandler
    {
        IObservable<string> Error
        {
            get;
        }

        ManualResetEvent NewSearchEvent
        {
            get;
        }

        Directory Root
        {
            get;
        }

        ManualResetEvent SearchFinishedEvent
        {
            get;
        }

        void StartHandler(string path, CancellationToken token);
    }
}