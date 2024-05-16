using System.IO;
using FileSystemTraverser.Entities;

namespace FileSystemTraverser.Extensions
{
    public static class FileSystemInfoExtensions
    {
        public static FileSystemEntry ToFileSystemEntry(this FileSystemInfo entry, bool isFile)
        {
            var fileSystemEntry = new FileSystemEntry(entry.Name, isFile, entry.FullName);

            return fileSystemEntry;
        }
    }
}
