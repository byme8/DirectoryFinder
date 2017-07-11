using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using DirectoryFinder.Domain.Services;

namespace DirectoryFinder.Services
{
    public class ProgresNotifier : IProgresNotifier
    {
        private List<string> tasks;
        private Subject<IEnumerable<string>> subject;

        public ProgresNotifier()
        {
            this.tasks = new List<string>();
            this.subject = new Subject<IEnumerable<string>>();
        }

        public void Start(string task)
        {
            this.tasks.Add(task);
            this.subject.OnNext(tasks);
        }

        public void Stop(string task)
        {
            this.tasks.Remove(task);
            this.subject.OnNext(tasks);
        }

        public IDisposable Subscribe(IObserver<IEnumerable<string>> observer)
        {
            return this.subject.Subscribe(observer);
        }
    }
}