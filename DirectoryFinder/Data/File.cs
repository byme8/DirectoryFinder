﻿using System;
using System.IO;
using System.Linq;
using EnumUtilities;

namespace DirectoryFinder.Data
{
    [Serializable]
    public class File : Item
    {
    }

    public static class FileExtensions
    {
        public static File ToFile(this FileInfo info, Directory parent)
        {
            var file = new File
            {
                Name = info.FullName,
                Parent = parent,
                CreationDate = info.CreationTime,
                LastAccessDate = info.LastAccessTime,
                ModificationDate = info.LastWriteTime,
                Size = info.Length
            };

            var access = info.GetAccessControl();

            file.Owner = access.GetOwner(typeof(System.Security.Principal.NTAccount)).Value;
            file.Attributes = EnumUtil.GetNameValue<FileAttributes>().Where(o => (info.Attributes & o.Value) > 0).Select(o => o.Key).ToArray();

            return file;
        }
    }
}