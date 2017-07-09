using EnumUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.AccessControl;

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
        /// <summary>
        /// Creates the <see cref="Directory"/> base on <see cref="DirectoryInfo"/>.
        /// </summary>
        /// <param name="info">The directory info.</param>
        /// <param name="parent">The parent</param>
        /// <returns>The result directory.</returns>
        /// <remarks>
        /// It can't be merged with <see cref="FileExtensions.ToFile"/>, 
        /// because <see cref="DirectoryInfo"/> and <see cref="FileInfo"/> have different hierarchy.
        /// </remarks>
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

            try
            {
                directory.Owner = access.GetOwner(typeof(System.Security.Principal.NTAccount)).Value;
            }
            catch (Exception)
            {
                directory.Owner = "N/A";
            }

            directory.Attributes = EnumUtil.GetNameValue<FileAttributes>().Where(o => (info.Attributes & o.Value) > 0).Select(o => o.Key).ToArray();
            directory.UserRights = new[] { "N/A" };

            var currentUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            foreach (FileSystemAccessRule rule in access.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount)))
            {
                if (rule.IdentityReference.Value == currentUser)
                {
                    directory.UserRights = EnumUtil.GetNameValue<FileSystemRights>().Where(o => (rule.FileSystemRights & o.Value) > 0).Select(o => o.Key).ToArray();
                    break;
                }
            }

            return directory;
        }
    }
}