using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DirectoryFinder.Data;
using Microsoft.Win32;

namespace DirectoryFinder.Services
{
    public class SeralizationHandler
    {
        private DirectorySearchHandler searchHandler;

        public SeralizationHandler(DirectorySearchHandler searchHandler)
        {
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
                        DefaultExt = "xml"
                    };

                    var value = saveFileDialog.ShowDialog();
                    if (!value.HasValue || !value.Value)
                        continue;

                    using (var writer = new System.IO.StreamWriter(saveFileDialog.FileName))
                    {
                        var serializer = new XmlSerializer(typeof(Item), new[] { typeof(Directory), typeof(File)});
                        serializer.Serialize(writer, this.searchHandler.Root);
                        writer.Flush();
                    }
                }
            });
            thread.Start();
            token.Register(() => thread.Abort());
        }
    }
}
