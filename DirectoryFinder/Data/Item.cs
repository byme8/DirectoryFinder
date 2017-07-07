using System;
using System.Xml.Serialization;

namespace DirectoryFinder.Data
{
    [Serializable]
    public abstract class Item
    {
        [XmlIgnore]
        public Directory Parent
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public DateTime CreationDate
        {
            get;
            set;
        }

        public DateTime Edited
        {
            get;
            set;
        }

        public DateTime LastAccess
        {
            get;
            set;
        }

        public string[] Attributes
        {
            get;
            set;
        }

        public long Size
        {
            get;
            set;
        }

        public string Owner
        {
            get;
            set;
        }

        public string[] UserAccess
        {
            get;
            set;
        }

        public virtual Item[] Items
        {
            get;
        }
    }
}