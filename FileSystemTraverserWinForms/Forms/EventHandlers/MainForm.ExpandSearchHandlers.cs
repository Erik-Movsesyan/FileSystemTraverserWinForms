using FileSystemTraverser.Core.CustomEventArgs;
using System.Threading;
using System.Windows.Forms;

namespace FileSystemTraverserWinForms
{
    public partial class MainForm
    {
        private async void HandleResultsTreeNodeExpanded(object sender, TreeViewEventArgs e)
        {
            if (!_nodeCollectionCancellationTokens.TryGetValue(e.Node!.Nodes, out var cancellationTokenSource))
            {
                _nodeCollectionCancellationTokens.Add(e.Node.Nodes, new CancellationTokenSource());
                cancellationTokenSource = _nodeCollectionCancellationTokens[e.Node!.Nodes];
            }

            if (!e.Node!.Nodes.ContainsKey("Loading") && !cancellationTokenSource.IsCancellationRequested)
                return;

            if (cancellationTokenSource.IsCancellationRequested)
            {
                ClearNodes(e.Node);
                AddLoadingNode(e.Node);
                _nodeCollectionCancellationTokens.Remove(e.Node!.Nodes);
                _nodeCollectionCancellationTokens.Add(e.Node.Nodes, new CancellationTokenSource());
                cancellationTokenSource = _nodeCollectionCancellationTokens[e.Node!.Nodes];
            }

            AddRemoveExpandSearchStatusRelatedHandlers(true);

            if (!applyFilterCheckbox.Checked || IsFilterTextBoxEmpty)
            {
                _fileSystemVisitor.FileFound += HandleFileFoundTriggeredByExpandButton;
                _fileSystemVisitor.DirectoryFound += HandleDirectoryFoundTriggeredByExpandButton;

                await _fileSystemVisitor.StartFileSystemSearch(e.Node.FullPath, e.Node, cancellationTokenSource.Token);

                _fileSystemVisitor.FileFound -= HandleFileFoundTriggeredByExpandButton;
                _fileSystemVisitor.DirectoryFound -= HandleDirectoryFoundTriggeredByExpandButton;
            }
            else
            {
                _fileSystemVisitor.FilteredFileFound += HandleFilteredFileFoundTriggeredByExpandButton;
                _fileSystemVisitor.FilteredDirectoryFound += HandleFilteredDirectoryFoundTriggeredByExpandButton;

                await _fileSystemVisitor.StartFilteredFileSystemSearch(e.Node.FullPath, _currentFilterText, e.Node, cancellationTokenSource.Token);

                _fileSystemVisitor.FilteredFileFound -= HandleFilteredFileFoundTriggeredByExpandButton;
                _fileSystemVisitor.FilteredDirectoryFound -= HandleFilteredDirectoryFoundTriggeredByExpandButton;
            }

            ModifyLoadingNode(e.Node);
            AddRemoveExpandSearchStatusRelatedHandlers(false);
        }

        private void HandleFileFoundTriggeredByExpandButton(object sender, FileSystemEntryEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(() => HandleFileFoundTriggeredByExpandButton(sender, e));
            }
            else
            {
                var nodeToAdd = new TreeNode($"\\{e.FileSystemEntry.Name}");
                e.TreeNode.Nodes.Add(nodeToAdd);
            }
        }

        private void HandleDirectoryFoundTriggeredByExpandButton(object sender, FileSystemEntryEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(() => HandleDirectoryFoundTriggeredByExpandButton(sender, e));
            }
            else
            {
                var nodeToAdd = new TreeNode($"\\{e.FileSystemEntry.Name}");
                AddLoadingNode(nodeToAdd);
                e.TreeNode.Nodes.Add(nodeToAdd);
            }
        }

        private void HandleFilteredFileFoundTriggeredByExpandButton(object sender, FileSystemEntryEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(() => HandleFilteredFileFoundTriggeredByExpandButton(sender, e));
            }
            else
            {
                HandleFileFoundTriggeredByExpandButton(sender, e);
            }
        }

        private void HandleFilteredDirectoryFoundTriggeredByExpandButton(object sender, FileSystemEntryEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(() => HandleFilteredDirectoryFoundTriggeredByExpandButton(sender, e));
            }
            else
            {
                HandleDirectoryFoundTriggeredByExpandButton(sender, e);
            }
        }

        private void HandleResultsTreeNodeBeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node!.Nodes.ContainsKey("Loading"))
                e.Cancel = true;
        }

        private void HandleResultsTreeNodeBeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (abortSearchButton.Enabled)
                e.Cancel = true;
        }
    }
}
