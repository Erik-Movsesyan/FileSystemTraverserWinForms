using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using FileSystemTraverser.Entities;
using FileSystemTraverser.Enumerators;
using FileSystemTraverser.Extensions;

namespace FileSystemTraverser
{
    public class FileSystemVisitor : IEnumerable<FileSystemEntry>
    {
        private FileSystemEntry[] _fileSystemEntries  = [];

        public void TraverseFileSystemEntries(string folderPath)
        {
            var dirInfo = new DirectoryInfo(folderPath);

            var entries = dirInfo.GetFileSystemInfos();
            var tempArray = new FileSystemEntry[entries.Length];

            for (int i = 0; i < entries.Length; i++)
            {
                var entry = entries[i];
                var isFile = File.Exists(entry.FullName);
                if (!isFile)
                {
                    TraverseFileSystemEntries(entry.FullName);
                }

                tempArray[i] = entry.ToFileSystemEntry(isFile);
            }

            var tempArrayExistingFileSystemEntries = _fileSystemEntries;
            _fileSystemEntries = new FileSystemEntry[tempArray.Length + tempArrayExistingFileSystemEntries.Length];

            Array.Copy(tempArrayExistingFileSystemEntries, _fileSystemEntries, tempArrayExistingFileSystemEntries.Length);
            Array.Copy(tempArray, 0, _fileSystemEntries, tempArrayExistingFileSystemEntries.Length, tempArray.Length);
        }

        public IEnumerator<FileSystemEntry> GetEnumerator()
        {
            return new FieSystemEntryEnumerator(_fileSystemEntries);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
