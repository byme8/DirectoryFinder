using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryFinder.Domain.Services
{
    public interface IProgresNotifier : IObservable<IEnumerable<string>>
    {
        void Start(string task);
        void Stop(string task);
    }
}
