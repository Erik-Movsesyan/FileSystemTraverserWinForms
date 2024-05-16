using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FileSystemTraverser.Core.CustomEventArgs;
using FileSystemTraverser.Entities;
using FileSystemTraverser.Enumerators;
using FileSystemTraverser.Extensions;

namespace FileSystemTraverser.Core
{
    public partial class FileSystemVisitor : IEnumerable<FileSystemEntry>
    {
        private FileSystemEntry[] _fileSystemEntries = [];
        private Predicate<string> _currentFilteringPredicate;

        public async Task StartFileSystemSearch(string folderPath, CancellationToken token)
        {
            OnFileSystemSearchStartedEvent(EventArgs.Empty);
            //add a small delay so the UI can update while raising the handlers of the above event
            await Task.Delay(TimeSpan.FromMilliseconds(100), CancellationToken.None);

            await TraverseFileSystemEntries(folderPath, token);

            OnFileSystemSearchCompletedEvent(EventArgs.Empty);
        }

        public async Task StartFilteredFileSystemSearch(string folderPath, string filter, CancellationToken token)
        {
            OnFileSystemSearchStartedEvent(EventArgs.Empty);
            //add a small delay so the UI can update while raising the handlers of the above event
            await Task.Delay(TimeSpan.FromMilliseconds(100), CancellationToken.None);

            await TraverseFileSystemEntries(folderPath, token);
            _currentFilteringPredicate = (fileFolderName) => fileFolderName.Contains(filter);

            await Task.Run(FilterResults, CancellationToken.None);

            OnFileSystemSearchCompletedEvent(EventArgs.Empty);
        }

        public IEnumerator<FileSystemEntry> GetEnumerator()
        {
            return new FieSystemEntryEnumerator(_fileSystemEntries);
        }

        private async Task TraverseFileSystemEntries(string folderPath, CancellationToken token)
        {
            try
            {
                var dirInfo = new DirectoryInfo(folderPath);

                var entries = dirInfo.GetFileSystemInfos();
                var tempArray = new FileSystemEntry[entries.Length];

                for (int i = 0; i < entries.Length; i++)
                {
                    var entry = entries[i];
                    var isFile = File.Exists(entry.FullName);
                    var convertedEntry = await Task.Run(() => entry.ToFileSystemEntry(isFile), CancellationToken.None);

                    await Task.Run(() => FireFileDirectoryFoundEvents(isFile, convertedEntry), CancellationToken.None);

                    if (!isFile)
                    {
                        token.ThrowIfCancellationRequested();
                        await TraverseFileSystemEntries(entry.FullName, token);
                    }

                    tempArray[i] = convertedEntry;
                }

                var tempArrayExistingFileSystemEntries = _fileSystemEntries;
                _fileSystemEntries = new FileSystemEntry[tempArray.Length + tempArrayExistingFileSystemEntries.Length];

                Array.Copy(tempArrayExistingFileSystemEntries, _fileSystemEntries, tempArrayExistingFileSystemEntries.Length);
                Array.Copy(tempArray, 0, _fileSystemEntries, tempArrayExistingFileSystemEntries.Length, tempArray.Length);
            }
            catch (Exception e) when (e is OperationCanceledException or IOException)
            {
                //IOException exception gets thrown when we try to access a restricted folder/file
                //Ignore access related exceptions for now as well as OperationCanceledException exceptions
            }
        }

        private void FilterResults()
        {
            if (_currentFilteringPredicate is null)
                return;

            var filteredArray = new FileSystemEntry[_fileSystemEntries.Length];
            var filtrationPassedEntries = 0;
            foreach (var entry in _fileSystemEntries)
            {
                if (_currentFilteringPredicate(entry.Name))
                {
                    FireFilteredFileDirectoryFoundEvents(entry.IsFile, entry);

                    filteredArray[filtrationPassedEntries] = entry;
                    filtrationPassedEntries++;
                }
            }

            _fileSystemEntries = new FileSystemEntry[filtrationPassedEntries];
            Array.Copy(filteredArray, _fileSystemEntries, filtrationPassedEntries);
        }

        private void FireFileDirectoryFoundEvents(bool fileEvent, FileSystemEntry entry)
        {
            var fileSystemEntryEventArgs = new FileSystemEntryEventArgs { FileSystemEntry = entry };
            if (fileEvent)
            {
                OnFileFoundEvent(fileSystemEntryEventArgs);
            }
            else
            {
                OnDirectoryFoundEvent(fileSystemEntryEventArgs);
            }
        }

        private void FireFilteredFileDirectoryFoundEvents(bool fileEvent, FileSystemEntry entry)
        {
            var fileSystemEntryEventArgs = new FileSystemEntryEventArgs { FileSystemEntry = entry };
            if (fileEvent)
            {
                OnFilteredFileFoundEvent(fileSystemEntryEventArgs);
            }
            else
            {
                OnFilteredDirectoryFoundEvent(fileSystemEntryEventArgs);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
