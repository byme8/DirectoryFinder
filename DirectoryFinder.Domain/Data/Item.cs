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

        public DateTime ModificationDate
        {
            get;
            set;
        }

        public DateTime LastAccessDate
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

        public string[] UserRights
        {
            get;
            set;
        }

        [XmlIgnore]
        public virtual Item[] Items
        {
            get;
            private set;
        }
    }
}