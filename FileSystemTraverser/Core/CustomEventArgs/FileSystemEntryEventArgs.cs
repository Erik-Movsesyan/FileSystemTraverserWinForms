using System;
using System.Windows.Forms;
using FileSystemTraverser.Entities;

namespace FileSystemTraverser.Core.CustomEventArgs
{
    public class FileSystemEntryEventArgs : EventArgs
    {
        public FileSystemEntry FileSystemEntry { get; set; }

        public TreeNode TreeNode { get; set; }
    }
}
