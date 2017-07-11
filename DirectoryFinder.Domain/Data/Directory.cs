using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

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
}