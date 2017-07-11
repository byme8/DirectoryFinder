using DirectoryFinder.Domain.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DirectoryFinder.Providers
{
    public class FolderPathProvider : IFolderPathProvider, IDisposable
    {
        private FolderBrowserDialog folderBrowserDialog;
        private bool disposed;

        public FolderPathProvider()
        {
            this.folderBrowserDialog = new FolderBrowserDialog();
        }

        public string GetPath()
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                return folderBrowserDialog.SelectedPath;
            }

            return string.Empty;
        }

        public void Dispose()
        {
            if (this.disposed)
            {
                return;
            }

            this.folderBrowserDialog.Dispose();

            this.disposed = true;
        }
    }
}
