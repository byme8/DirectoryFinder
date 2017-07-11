using System;
using System.Collections.Generic;

namespace DirectoryFinder.Domain.Services
{
    public interface IProgresNotifier : IObservable<IEnumerable<string>>
    {
        void StartTask(string description, Action task);
    }
}