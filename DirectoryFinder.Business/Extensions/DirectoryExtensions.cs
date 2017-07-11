using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using EnumUtilities;

namespace DirectoryFinder.Business
{
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
        public static Data.Directory ToDirectory(this DirectoryInfo info, Data.Directory parent = null)
        {
            var directory = new Data.Directory
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