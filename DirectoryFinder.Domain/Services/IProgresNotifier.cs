using System;
using System.Collections.Generic;

namespace DirectoryFinder.Domain.Services
{
    public interface IProgresNotifier : IObservable<IEnumerable<string>>
    {
        void Start(string task);

        void Stop(string task);
    }
}