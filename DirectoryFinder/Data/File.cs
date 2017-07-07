using System;
using System.IO;

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
            return new File
            {
                Name = info.FullName,
                Size = info.Length,
                Parent = parent
            };
        }
    }
}