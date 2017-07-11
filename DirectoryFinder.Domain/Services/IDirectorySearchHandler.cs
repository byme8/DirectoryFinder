using DirectoryFinder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
