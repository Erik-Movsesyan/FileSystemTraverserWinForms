using FileSystemTraverser.Core.CustomEventArgs;
using System;

namespace FileSystemTraverser.Core
{
    public partial class FileSystemVisitor
    {
        public event EventHandler FileSystemSearchStarted;
        public event EventHandler FileSystemSearchCompleted;
        public event EventHandler<FileSystemEntryEventArgs> FileFound;
        public event EventHandler<FileSystemEntryEventArgs> DirectoryFound;
        public event EventHandler<FileSystemEntryEventArgs> FilteredFileFound;
        public event EventHandler<FileSystemEntryEventArgs> FilteredDirectoryFound;

        protected void OnFileSystemSearchStartedEvent(EventArgs e)
        {
            EventHandler raiseEvent = FileSystemSearchStarted;
            raiseEvent?.Invoke(this, e);
        }

        protected void OnFileSystemSearchCompletedEvent(EventArgs e)
        {
            EventHandler raiseEvent = FileSystemSearchCompleted;
            raiseEvent?.Invoke(this, e);
        }

        protected void OnFileFoundEvent(FileSystemEntryEventArgs e)
        {
            EventHandler<FileSystemEntryEventArgs> raiseEvent = FileFound;
            raiseEvent?.Invoke(this, e);
        }

        protected void OnDirectoryFoundEvent(FileSystemEntryEventArgs e)
        {
            EventHandler<FileSystemEntryEventArgs> raiseEvent = DirectoryFound;
            raiseEvent?.Invoke(this, e);
        }

        protected void OnFilteredFileFoundEvent(FileSystemEntryEventArgs e)
        {
            EventHandler<FileSystemEntryEventArgs> raiseEvent = FilteredFileFound;
            raiseEvent?.Invoke(this, e);
        }

        protected void OnFilteredDirectoryFoundEvent(FileSystemEntryEventArgs e)
        {
            EventHandler<FileSystemEntryEventArgs> raiseEvent = FilteredDirectoryFound;
            raiseEvent?.Invoke(this, e);
        }
    }
}
