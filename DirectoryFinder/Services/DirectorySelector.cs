using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace DirectoryFinder.Services
{
    public class DirectorySelector
    {
        public DirectorySelector()
        {
            this.FolderBrowserDialog = new FolderBrowserDialog();
        }

        public FolderBrowserDialog FolderBrowserDialog
        {
            get;
        }

        public string Select()
        {
            if (this.FolderBrowserDialog.ShowDialog() == DialogResult.OK)
                return this.FolderBrowserDialog.SelectedPath;

            return string.Empty;
        }
    }
}
