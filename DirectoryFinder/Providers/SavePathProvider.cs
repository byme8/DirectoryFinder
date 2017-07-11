using DirectoryFinder.Domain.Providers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryFinder.Providers
{
    public class SavePathProvider : ISavePathProvider
    {
        public string GetPath()
        {
            var saveFileDialog = new SaveFileDialog()
            {
                Title = "Save results",
                AddExtension = true,
                Filter = "XML|*.xml"
            };

            var value = saveFileDialog.ShowDialog();
            if (!value.HasValue || !value.Value)
                return string.Empty;

            return saveFileDialog.FileName;
        }
    }
}
