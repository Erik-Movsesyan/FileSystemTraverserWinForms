using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        public async Task StartFileSystemSearch(string folderPath, TreeNode currentNodeInformation = null, CancellationToken token = default)
        {
            OnFileSystemSearchStartedEvent(EventArgs.Empty);
            //add a small delay so the UI can update while raising the handlers of the above event
            await Task.Delay(TimeSpan.FromMilliseconds(100), CancellationToken.None);

            await BrowseDirectory(folderPath, currentNodeInformation, token);

            OnFileSystemSearchCompletedEvent(EventArgs.Empty);
        }

        public async Task StartFilteredFileSystemSearch(string folderPath, string filter, TreeNode currentNodeInformation = null, CancellationToken token = default)
        {
            OnFileSystemSearchStartedEvent(EventArgs.Empty);
            //add a small delay so the UI can update after running the handlers of the above event
            await Task.Delay(TimeSpan.FromMilliseconds(100), CancellationToken.None);

            await BrowseDirectory(folderPath, currentNodeInformation, token);
            _currentFilteringPredicate = (fileFolderName) => fileFolderName.Contains(filter);

            await Task.Run(() => FilterResults(currentNodeInformation), CancellationToken.None);

            OnFileSystemSearchCompletedEvent(EventArgs.Empty);
        }

        public IEnumerator<FileSystemEntry> GetEnumerator()
        {
            return new FieSystemEntryEnumerator(_fileSystemEntries);
        }

        private async Task BrowseDirectory(string folderPath, TreeNode currentNodeInformation, CancellationToken token)
        {
            try
            {
                var dirInfo = new DirectoryInfo(folderPath);

                var entries = dirInfo.GetFileSystemInfos();
                _fileSystemEntries = new FileSystemEntry[entries.Length];
                
                for (int i = 0; i < entries.Length; i++)
                {
                    var entry = entries[i];
                    var isFile = File.Exists(entry.FullName);
                    var convertedEntry = await Task.Run(() => entry.ToFileSystemEntry(isFile), CancellationToken.None);

                    token.ThrowIfCancellationRequested();
                    await Task.Run(() => FireFileDirectoryFoundEvents(isFile, convertedEntry, currentNodeInformation), CancellationToken.None);

                    _fileSystemEntries[i] = convertedEntry;
                }
            }
            catch (Exception e) when (e is OperationCanceledException or IOException or UnauthorizedAccessException)
            {
                //IOException or UnauthorizedAccessException exception get thrown when we try to access a restricted folder/file
                //Ignore access related exceptions for now as well as OperationCanceledException exceptions
            }
        }

        private void FilterResults(TreeNode currentNodeInformation = null)
        {
            if (_currentFilteringPredicate is null)
                return;

            var filteredArray = new FileSystemEntry[_fileSystemEntries.Length];
            var filtrationPassedEntries = 0;
            foreach (var entry in _fileSystemEntries)
            {
                if (_currentFilteringPredicate(entry.Name))
                {
                    FireFilteredFileDirectoryFoundEvents(entry.IsFile, entry, currentNodeInformation);

                    filteredArray[filtrationPassedEntries] = entry;
                    filtrationPassedEntries++;
                }
            }

            _fileSystemEntries = new FileSystemEntry[filtrationPassedEntries];
            Array.Copy(filteredArray, _fileSystemEntries, filtrationPassedEntries);
        }

        private void FireFileDirectoryFoundEvents(bool fileEvent, FileSystemEntry entry, TreeNode currentTreeNode)
        {
            var fileSystemEntryEventArgs = new FileSystemEntryEventArgs 
            { 
                FileSystemEntry = entry,
                TreeNode = currentTreeNode
            };

            if (fileEvent)
            {
                OnFileFoundEvent(fileSystemEntryEventArgs);
            }
            else
            {
                OnDirectoryFoundEvent(fileSystemEntryEventArgs);
            }
        }

        private void FireFilteredFileDirectoryFoundEvents(bool fileEvent, FileSystemEntry entry, TreeNode currentNodeInformation)
        {
            var fileSystemEntryEventArgs = new FileSystemEntryEventArgs 
            { 
                FileSystemEntry = entry,
                TreeNode = currentNodeInformation
            };

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
