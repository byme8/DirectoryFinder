using System.IO;
using HumanBytes;

namespace DirectoryFinder.Data
{
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
