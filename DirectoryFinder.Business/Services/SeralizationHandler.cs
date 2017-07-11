using System.Threading;
using System.Xml.Serialization;
using DirectoryFinder.Data;
using DirectoryFinder.Domain.Providers;
using DirectoryFinder.Domain.Services;

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

                    this.progresNotifier.StartTask(string.Format("Saving data to {0}", path), () =>
                    {
                        using (var writer = new System.IO.StreamWriter(path))
                        {
                            var serializer = new XmlSerializer(typeof(Item), new[] { typeof(Directory), typeof(File) });
                            serializer.Serialize(writer, this.searchHandler.Root);
                            writer.Flush();
                        }
                    });
                }
            });
            thread.Start();
            token.Register(() => thread.Abort());
        }
    }
}