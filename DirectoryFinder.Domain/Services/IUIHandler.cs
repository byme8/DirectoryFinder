using DirectoryFinder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
