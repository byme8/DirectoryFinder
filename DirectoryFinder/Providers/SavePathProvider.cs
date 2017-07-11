using DirectoryFinder.Domain.Providers;
using Microsoft.Win32;

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