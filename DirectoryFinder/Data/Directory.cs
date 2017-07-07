using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DirectoryFinder.Data
{
    public class Directory : Item
    {
        public Directory()
        {
            this.Files = new List<File>();
            this.SubDirectories = new List<Directory>();
        }

        public List<Directory> SubDirectories
        {
            get;
            set;
        }

        public List<File> Files
        {
            get;
            set;
        }

        public override Item[] Items
        {
            get
            {
                if (this.SubDirectories == null)
                    return this.Files.ToArray();

                return this.SubDirectories.Cast<Item>().Union(this.Files).ToArray();
            }
        }
    }

    public static class DirectoryExtensions
    {
        public static Directory ToDirectory(this DirectoryInfo info, Directory parent = null)
        {
            return new Directory
            {
                Name = info.FullName,
                Parent = parent
            };
        }
    }
}