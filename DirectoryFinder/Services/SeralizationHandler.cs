using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DirectoryFinder.Data;
using Microsoft.Win32;
using DirectoryFinder.Views.ProgresShower;

namespace DirectoryFinder.Services
{
    public class SeralizationHandler
    {
        private DirectorySearchHandler searchHandler;
        private ProgresService progresService;

        public SeralizationHandler(DirectorySearchHandler searchHandler, ProgresService progresService)
        {
            this.progresService = progresService;
            this.searchHandler = searchHandler;
        }

        public void StartHandeling(CancellationToken token)
        {
            var thread = new Thread(() =>
            {
                while (true)
                {
                    this.searchHandler.NewSearchEvent.WaitOne();
                    this.searchHandler.SearchFinishedEvent.WaitOne();

                    var saveFileDialog = new SaveFileDialog()
                    {
                        Title = "Save results",
                        AddExtension = true,
                        Filter ="XML|*.xml"
                    };

                    var value = saveFileDialog.ShowDialog();
                    if (!value.HasValue || !value.Value)
                        continue;

                    var taskName = string.Format("Saving data to {0}", saveFileDialog.FileName);
                    this.progresService.Start(taskName);

                    using (var writer = new System.IO.StreamWriter(saveFileDialog.FileName))
                    {
                        var serializer = new XmlSerializer(typeof(Item), new[] { typeof(Directory), typeof(File)});
                        serializer.Serialize(writer, this.searchHandler.Root);
                        writer.Flush();
                    }

                    this.progresService.Stop(taskName);
                }
            });
            thread.Start();
            token.Register(() => thread.Abort());
        }
    }
}
