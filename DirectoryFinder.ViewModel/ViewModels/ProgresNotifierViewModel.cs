using System;
using System.Collections.Generic;
using System.Linq;
using DirectoryFinder.Core.ViewModels;
using DirectoryFinder.Domain.Services;
using ReactiveUI;

namespace DirectoryFinder.ViewModels
{
    public class ProgresNotifierViewModel : ViewModel, IDisposable
    {
        private bool disposed;
        private IDisposable subsribtionToProgressNotifier;

        public ProgresNotifierViewModel(IProgresNotifier progresNotifier)
        {
            this.Tasks = Enumerable.Empty<string>();
            this.subsribtionToProgressNotifier = progresNotifier.Subscribe(tasks =>
            {
                this.Tasks = tasks;
                this.RaisePropertyChanged("Tasks");
                this.RaisePropertyChanged("Visibility");
            });
        }

        public bool Visibility
        {
            get
            {
                return this.Tasks.Any();
            }
        }

        public IEnumerable<string> Tasks
        {
            get;
            private set;
        }

        public void Dispose()
        {
            if (this.disposed)
            {
                return;
            }

            this.subsribtionToProgressNotifier.Dispose();

            this.disposed = true;
        }
    }
}