using EnumUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryFinder.Business
{
    public static class FileExtensions
    {
        /// <summary>
        /// Creates the <see cref="File"/> base on <see cref="FileInfo"/>.
        /// </summary>
        /// <param name="info">The file info.</param>
        /// <param name="parent">The parent</param>
        /// <returns>The result file.</returns>
        /// <remarks>
        /// It can't be merged with <see cref="DirectoryExtensions.ToDirectory"/>, 
        /// because <see cref="DirectoryInfo"/> and <see cref="FileInfo"/> have different hierarchy.
        /// </remarks>
        public static Data.File ToFile(this FileInfo info, Data.Directory parent)
        {
            var file = new Data.File
            {
                Name = info.FullName,
                Parent = parent,
                CreationDate = info.CreationTime,
                LastAccessDate = info.LastAccessTime,
                ModificationDate = info.LastWriteTime,
                Size = info.Length
            };

            var access = info.GetAccessControl();

            try
            {
                file.Owner = access.GetOwner(typeof(System.Security.Principal.NTAccount)).Value;
            }
            catch (Exception)
            {
                file.Owner = "N/A";
            }

            file.Attributes = EnumUtil.GetNameValue<FileAttributes>().Where(o => (info.Attributes & o.Value) > 0).Select(o => o.Key).ToArray();
            file.UserRights = new[] { "N/A" };

            var currentUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            foreach (FileSystemAccessRule rule in access.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount)))
            {
                if (rule.IdentityReference.Value == currentUser)
                {
                    file.UserRights = EnumUtil.GetNameValue<FileSystemRights>().Where(o => (rule.FileSystemRights & o.Value) > 0).Select(o => o.Key).ToArray();
                    break;
                }
            }
            return file;
        }
    }
}
