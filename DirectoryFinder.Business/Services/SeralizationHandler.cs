﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DirectoryFinder.Data;
using Microsoft.Win32;
using DirectoryFinder.Domain.Services;
using DirectoryFinder.Domain.Providers;

namespace DirectoryFinder.Services
{
    public class SeralizationHandler : ISeralizationHandler
    {
        private IDirectorySearchHandler searchHandler;
        private IProgresNotifier progresNotifier;
        private ISavePathProvider savePathProvider;

        public SeralizationHandler(IDirectorySearchHandler searchHandler, IProgresNotifier progresNotifier, ISavePathProvider savePathProvider)
        {
            this.progresNotifier = progresNotifier;
            this.searchHandler = searchHandler;
            this.savePathProvider = savePathProvider;
        }

        public void StartHandeling(CancellationToken token)
        {
            var thread = new Thread(() =>
            {
                while (true)
                {
                    this.searchHandler.NewSearchEvent.WaitOne();
                    this.searchHandler.SearchFinishedEvent.WaitOne();

                    

                    var path = this.savePathProvider.GetPath();
                    if (string.IsNullOrWhiteSpace(path))
                    {
                        continue;
                    }

                    var taskName = string.Format("Saving data to {0}", path);
                    this.progresNotifier.Start(taskName);

                    using (var writer = new System.IO.StreamWriter(path))
                    {
                        var serializer = new XmlSerializer(typeof(Item), new[] { typeof(Directory), typeof(File)});
                        serializer.Serialize(writer, this.searchHandler.Root);
                        writer.Flush();
                    }

                    this.progresNotifier.Stop(taskName);
                }
            });
            thread.Start();
            token.Register(() => thread.Abort());
        }
    }
}