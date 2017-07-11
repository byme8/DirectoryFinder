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

        public void StartTask(string description, Action task)
        {
            lock (this.tasks)
            {
                this.tasks.Add(description);
                this.subject.OnNext(this.tasks.ToArray());
            }

            task();
           
            lock (this.tasks)
            {
                this.tasks.Remove(description);
                this.subject.OnNext(this.tasks.ToArray());
            }
        }
    
        public IDisposable Subscribe(IObserver<IEnumerable<string>> observer)
        {
            return this.subject.Subscribe(observer);
        }
    }
}