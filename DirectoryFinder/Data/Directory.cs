using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using EnumsNET;

namespace DirectoryFinder.Data
{
    [Serializable]
    public class Directory : Item
    {
        public Directory()
        {
            this.Files = new List<File>();
            this.SubDirectories = new List<Directory>();
        }

        [OnDeserialized()]
        internal void OnSerializedMethod(StreamingContext context)
        {
            foreach (var item in this.Items)
            {
                item.Parent = this;
            }
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
            var directory = new Directory
            {
                Name = info.FullName,
                Parent = parent,
                CreationDate = info.CreationTime,
                LastAccessDate = info.LastAccessTime,
                ModificationDate = info.LastWriteTime
            };

            var access = info.GetAccessControl();

            directory.Owner = access.GetOwner(typeof(System.Security.Principal.NTAccount)).Value;
            directory.Attributes = Enums.GetMembers<FileAttributes>().Where(o => (info.Attributes & o.Value) > 0).Select(o => o.Name).ToArray();

            return directory;
        }
    }
}