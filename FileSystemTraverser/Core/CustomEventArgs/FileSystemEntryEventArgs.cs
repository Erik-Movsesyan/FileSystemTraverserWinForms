using System;
using FileSystemTraverser.Entities;

namespace FileSystemTraverser.Core.CustomEventArgs
{
    public class FileSystemEntryEventArgs : EventArgs
    {
        public FileSystemEntry FileSystemEntry { get; set; }
    }
}
