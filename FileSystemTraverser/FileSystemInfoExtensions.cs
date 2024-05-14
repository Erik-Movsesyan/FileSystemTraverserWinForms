using System.IO;

namespace FileSystemTraverser
{
    public static class FileSystemInfoExtensions
    {
        public static FileSystemEntry[] ToFileSystemEntry(this FileSystemInfo[] entries)
        {
            var fileSystemEntries = new FileSystemEntry[entries.Length];

            for (int i = 0; i < entries.Length; i++)
            {
                var entry = entries[i];
                var isFile = entry.Attributes.HasFlag(FileAttributes.Archive) && !string.IsNullOrEmpty(entry.Extension);
                fileSystemEntries[i] = new FileSystemEntry(entries[i].Name, isFile, entry.FullName);
            }

            return fileSystemEntries;
        }

        public static FileSystemEntry ToFileSystemEntry(this FileSystemInfo entry)
        {
            var isFile = entry.Attributes.HasFlag(FileAttributes.Archive) && !string.IsNullOrEmpty(entry.Extension);
            var fileSystemEntry = new FileSystemEntry(entry.Name, isFile, entry.FullName);
            
            return fileSystemEntry;
        }
    }
}
