using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FileSystemTraverser.Entities;
using FileSystemTraverser.Enumerators;
using FileSystemTraverser.Extensions;

namespace FileSystemTraverser
{
    public class FileSystemVisitor : IEnumerable<FileSystemEntry>
    {
        public event EventHandler FileSystemSearchStared;
        public event EventHandler FileSystemSearchCompleted;
        private FileSystemEntry[] _fileSystemEntries  = [];
        private Predicate<string> _currentFilteringPredicate;

        public async Task StartFileSystemSearch(string folderPath)
        {
            OnFileSystemSearchStartedEvent(EventArgs.Empty);

            //add a small delay so the UI can update while raising the handlers of the above event
            await Task.Delay(TimeSpan.FromMilliseconds(100));

            await TraverseFileSystemEntries(folderPath);
            OnFileSystemSearchCompletedEvent(EventArgs.Empty);
        }

        public async Task StartFilteredFileSystemSearch(string folderPath, string filter)
        {
            OnFileSystemSearchStartedEvent(EventArgs.Empty);

            //add a small delay so the UI can update while raising the handlers of the above event
            await Task.Delay(TimeSpan.FromMilliseconds(100));

            await TraverseFileSystemEntries(folderPath);
            _currentFilteringPredicate = (fileFolderName)=> fileFolderName.Contains(filter);

            await Task.Run(FilterResults);

            OnFileSystemSearchCompletedEvent(EventArgs.Empty);
        }

        private async Task TraverseFileSystemEntries(string folderPath)
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
                    await TraverseFileSystemEntries(entry.FullName);
                }

                tempArray[i] = await Task.Run(() => entry.ToFileSystemEntry(isFile));
            }

            var tempArrayExistingFileSystemEntries = _fileSystemEntries;
            //_fileSystemEntries = null;
            _fileSystemEntries = new FileSystemEntry[tempArray.Length + tempArrayExistingFileSystemEntries.Length];

            Array.Copy(tempArrayExistingFileSystemEntries, _fileSystemEntries, tempArrayExistingFileSystemEntries.Length);
            Array.Copy(tempArray, 0, _fileSystemEntries, tempArrayExistingFileSystemEntries.Length, tempArray.Length);
        }

        private void FilterResults()
        {
            if (_currentFilteringPredicate is null)
                return;

            var filteredArray = new FileSystemEntry[_fileSystemEntries.Length];
            var filtrationPassedEntries = 0;
            for (int i = 0; i < _fileSystemEntries.Length; i++)
            {
                var entry = _fileSystemEntries[i];
                if (_currentFilteringPredicate(entry.Name))
                {
                    filteredArray[filtrationPassedEntries] = entry;
                    filtrationPassedEntries++;
                }
            }

            _fileSystemEntries = new FileSystemEntry[filtrationPassedEntries];
            Array.Copy(filteredArray, _fileSystemEntries, filtrationPassedEntries);
        }

        protected void OnFileSystemSearchStartedEvent(EventArgs e)
        {
            EventHandler raiseEvent = FileSystemSearchStared;
            raiseEvent?.Invoke(this, e);
        }

        protected void OnFileSystemSearchCompletedEvent(EventArgs e)
        {
            EventHandler raiseEvent = FileSystemSearchCompleted;
            raiseEvent?.Invoke(this, e);
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
