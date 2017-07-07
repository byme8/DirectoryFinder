using System;
using System.IO;
using System.Linq;
using System.Threading;
using DirectoryFinder.Data;
using MaterialDesignThemes.Wpf;

namespace DirectoryFinder.Services
{
    public class DirectorySearchHandler
    {
        public AutoResetEvent SearchFinishingEvent;
        private ISnackbarMessageQueue messageQueue;

        public DirectorySearchHandler(ISnackbarMessageQueue messageQueue)
        {
            this.SearchFinishingEvent = new AutoResetEvent(false);
            this.messageQueue = messageQueue;
        }

        public Data.Directory Root
        {
            get;
            private set;
        }

        public void StartHandler(string path, CancellationToken token)
        {
            new Thread(() =>
            {
                this.SearchFinishingEvent.Reset();
                this.Root = this.SearchDirectoryInternal(new DirectoryInfo(path), token);
                this.SearchFinishingEvent.Set();
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
                this.messageQueue.Enqueue(ex.Message);
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
                this.messageQueue.Enqueue(ex.Message);
            }

            directory.Size = directory.Items.Sum(o => o.Size);

            return directory;
        }
    }
}