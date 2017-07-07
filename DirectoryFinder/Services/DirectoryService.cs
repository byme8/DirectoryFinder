using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DirectoryFinder.Services
{
    public class DirectoryService
    {
        private readonly Subject<Data.Item> newItem = new Subject<Data.Item>();

        public IObservable<Data.Item> NewItem
        {
            get
            {
                return this.newItem;
            }
        }

        private readonly Subject<Unit> newSearch = new Subject<Unit>();

        public IObservable<Unit> NewSearch
        {
            get
            {
                return this.newSearch;
            }
        }

        public DirectoryService()
        {
        }

        public void SearchDirectory(string path, CancellationToken token)
        {
            this.newSearch.OnNext(Unit.Default);
            var thread = new Thread(() => this.SearchDirectoryInternal(new DirectoryInfo(path), token));
            thread.Start();
        }

        private Data.Directory SearchDirectoryInternal(DirectoryInfo directoryInfo, CancellationToken token, Data.Directory parent = null)
        {
            var directory = new Data.Directory();

            // TODO: Add other fields
            directory.Name = directoryInfo.FullName;
            directory.Parent = parent;
            //
            this.newItem.OnNext(directory);

            foreach (var fileInfo in directoryInfo.EnumerateFiles())
            {
                if (token.IsCancellationRequested)
                {
                    return directory;
                }

                var file = new Data.File();

                // TODO: Add other fields
                file.Name = fileInfo.FullName;
                file.Parent = directory;
                //

                directory.Files.Add(file);
                this.newItem.OnNext(file);
            }

            foreach (var subDirectoryInfo in directoryInfo.EnumerateDirectories())
            {
                var subDirectory = this.SearchDirectoryInternal(subDirectoryInfo, token, directory);
                directory.SubDirectories.Add(subDirectory);
                this.newItem.OnNext(subDirectory);
            }

            return directory;
        }
    }
}
