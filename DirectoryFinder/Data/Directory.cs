using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryFinder.Data
{
    public class Directory : Item
    {
        public Directory[] SubDirectories
        {
            get;
            set;
        }

        public File[] Files
        {
            get;
            set;
        }
    }
}
