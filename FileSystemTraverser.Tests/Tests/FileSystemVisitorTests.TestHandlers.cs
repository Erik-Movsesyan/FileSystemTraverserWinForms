using System;

namespace FileSystemTraverser.Tests.FileSystemVisitorTests
{
    public partial class FileSystemVisitorTests
    {
        private static void HandleFileSystemSearchStarted(object sender, EventArgs e)
        {
            Assert.Pass($"The '{nameof(_fileSystemVisitor.FileSystemSearchStarted)}' event successfully fired " +
                $"since the '{nameof(HandleFileSystemSearchStarted)}' handler was invoked");
        }

        private static void HandleFileSystemSearchCompleted(object sender, EventArgs e)
        {
            Assert.Pass($"The '{nameof(_fileSystemVisitor.FileSystemSearchCompleted)}' event successfully fired " +
                $"since the '{nameof(HandleFileSystemSearchCompleted)}' handler was invoked");
        }

        private static void HandleFileFound(object sender, EventArgs e)
        {
            Assert.Pass($"The '{nameof(_fileSystemVisitor.FileFound)}' event successfully fired " +
                        $"since the '{nameof(HandleFileFound)}' handler was invoked");
        }

        private static void HandleDirectoryFound(object sender, EventArgs e)
        {
            Assert.Pass($"The '{nameof(_fileSystemVisitor.DirectoryFound)}' event successfully fired " +
                        $"since the '{nameof(HandleDirectoryFound)}' handler was invoked");
        }

        private static void HandleFilteredFileFound(object sender, EventArgs e)
        {
            Assert.Pass($"The '{nameof(_fileSystemVisitor.FilteredFileFound)}' event successfully fired " +
                        $"since the '{nameof(HandleFilteredFileFound)}' handler was invoked");
        }

        private static void HandleFilteredDirectoryFound(object sender, EventArgs e)
        {
            Assert.Pass($"The '{nameof(_fileSystemVisitor.FilteredDirectoryFound)}' event successfully fired " +
                        $"since the '{nameof(HandleFilteredDirectoryFound)}' handler was invoked");
        }
    }
}
