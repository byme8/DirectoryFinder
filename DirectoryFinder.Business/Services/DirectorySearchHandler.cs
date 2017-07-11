using DirectoryFinder.Business;
using DirectoryFinder.Domain.Services;
using System;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;

namespace DirectoryFinder.Services
{
    public class DirectorySearchHandler : IDirectorySearchHandler
    {
        private Subject<string> error;
        private IProgresNotifier progresNotifier;

        public IObservable<string> Error
        {
            get
            {
                return this.error.AsObservable();
            }
        }

        public ManualResetEvent NewSearchEvent
        {
            get;
            private set;
        }

        public Data.Directory Root
        {
            get;
            private set;
        }

        public ManualResetEvent SearchFinishedEvent
        {
            get;
            private set;
        }

        public DirectorySearchHandler(IProgresNotifier progresNotifier)
        {
            this.NewSearchEvent = new ManualResetEvent(false);
            this.SearchFinishedEvent = new ManualResetEvent(false);
            this.error = new Subject<string>();
            this.progresNotifier = progresNotifier;
        }

        public void StartHandler(string path, CancellationToken token)
        {
            new Thread(() =>
            {
                var taskName = string.Format("Searching at {0} directory...", path);
                this.SearchFinishedEvent.Reset();
                this.NewSearchEvent.Set();

                this.progresNotifier.Start(taskName);
                this.Root = this.SearchDirectoryInternal(new DirectoryInfo(path), token);
                this.progresNotifier.Stop(taskName);

                this.NewSearchEvent.Reset();
                this.SearchFinishedEvent.Set();
            }).Start();
        }

        private Data.Directory SearchDirectoryInternal(DirectoryInfo directoryInfo, CancellationToken token, Data.Directory parent = null)
        {
            var directory = directoryInfo.ToDirectory(parent);

            try
            {
                foreach (var fileInfo in directoryInfo.EnumerateFiles())
                {
                    if (token.IsCancellationRequested)
                        return directory;

                    directory.Files.Add(fileInfo.ToFile(directory));
                }
            }
            catch (Exception ex)
            {
                this.error.OnNext(ex.Message);
            }

            try
            {
                foreach (var subDirectoryInfo in directoryInfo.EnumerateDirectories())
                {
                    if (token.IsCancellationRequested)
                        return directory;

                    directory.SubDirectories.Add(this.SearchDirectoryInternal(subDirectoryInfo, token, directory));
                }
            }
            catch (Exception ex)
            {
                this.error.OnNext(ex.Message);
            }

            directory.Size = directory.Items.Sum(o => o.Size);

            return directory;
        }
    }
}