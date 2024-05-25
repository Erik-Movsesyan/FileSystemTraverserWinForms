using FileSystemTraverser.Core.CustomEventArgs;
using System.Windows.Forms;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace FileSystemTraverserWinForms
{
    public partial class MainForm
    {
        private async void HandleSearchButtonClick(object sender, EventArgs e)
        {
            if (!ValidateFolderToSearchBox(_currentFolderToSearchText))
                return;

            ClearNodes();
            searchButton.Enabled = false;

            var topLevelNodes = resultsTree.Nodes;
            _nodeCollectionCancellationTokens.Add(topLevelNodes, new CancellationTokenSource());
            var cancellationToken = _nodeCollectionCancellationTokens[topLevelNodes].Token;

            await Task.Run(() => AddRemoveBrowseSearchStatusRelatedHandlers(add: true), CancellationToken.None);

            if (!applyFilterCheckbox.Checked || IsFilterTextBoxEmpty)
            {
                _fileSystemVisitor.FileFound += HandleFileFoundTriggeredByBrowseButton;
                _fileSystemVisitor.DirectoryFound += HandleDirectoryFoundTriggeredByBrowseButton;

                await _fileSystemVisitor.StartFileSystemSearch(_currentFolderToSearchText, null, cancellationToken);

                _fileSystemVisitor.FileFound -= HandleFileFoundTriggeredByBrowseButton;
                _fileSystemVisitor.DirectoryFound -= HandleDirectoryFoundTriggeredByBrowseButton;
            }
            else
            {
                _fileSystemVisitor.FilteredFileFound += HandleFilteredFileFoundTriggeredByBrowseButton;
                _fileSystemVisitor.FilteredDirectoryFound += HandleFilteredDirectoryFoundTriggeredByBrowseButton;

                applyFilterCheckbox.Enabled = false;
                await _fileSystemVisitor.StartFilteredFileSystemSearch(_currentFolderToSearchText, _currentFilterText, null, cancellationToken);

                _fileSystemVisitor.FilteredFileFound -= HandleFilteredFileFoundTriggeredByBrowseButton;
                _fileSystemVisitor.FilteredDirectoryFound -= HandleFilteredDirectoryFoundTriggeredByBrowseButton;

                _lastFilterTextWithResultsRetrieved = _currentFilterText;
            }

            await Task.Run(() => AddRemoveBrowseSearchStatusRelatedHandlers(add: false), CancellationToken.None);

            _lastApplyFilterCheckBoxStateWithResultsRetrieved = applyFilterCheckbox.Checked;
            _lastFolderToSearchTextWithResultsRetrieved = _currentFolderToSearchText;
            _nodeCollectionCancellationTokens.Remove(topLevelNodes);
        }

        private void HandleFileSystemSearchStartedTriggeredByBrowseButton(object sender, EventArgs e)
        {
            resultsForLabel.Text = string.Empty;
        }

        private void HandleFileSystemSearchStarted(object sender, EventArgs e)
        {
            abortSearchButton.Enabled = true;
            folderToSearchBox.Enabled = false;
            filterTextBox.Enabled = false;
            applyFilterCheckbox.Enabled = false;
            toolStripStatusLabel.Text = $"FileSystemSearch STARTED at {DateTime.Now:MM/dd/yyyThh:mm:ss.ffffff}";
        }

        private void HandleFileSystemSearchCompletedTriggeredByBrowseButton(object sender, EventArgs e)
        {
            EnableApplyFilterCheckbox();

            if (resultsTree.Nodes.Count == 0)
            {
                resultsForLabel.Text = $"No results found for folder: {_currentFolderToSearchText}";
            }
        }

        private void HandleFileSystemSearchCompleted(object sender, EventArgs e)
        {
            abortSearchButton.Enabled = false;
            folderToSearchBox.Enabled = true;
            filterTextBox.Enabled = true;
            applyFilterCheckbox.Enabled = true;
        }

        private void HandleFileSystemSearchCompletedStatusStrip(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = $"FileSystemSearch COMPLETED at {DateTime.Now:MM/dd/yyyThh:mm:ss.ffffff}";
        }

        private void HandleFileFoundTriggeredByBrowseButton(object sender, FileSystemEntryEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(() => HandleFileFoundTriggeredByBrowseButton(sender, e));
            }
            else
            {
                UpdateResultsForTextBox();
                var nodeToAdd = new TreeNode(e.FileSystemEntry.Path);
                resultsTree.Nodes.Add(nodeToAdd);
            }
        }

        private void HandleDirectoryFoundTriggeredByBrowseButton(object sender, FileSystemEntryEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(() => HandleDirectoryFoundTriggeredByBrowseButton(sender, e));
            }
            else
            {
                UpdateResultsForTextBox();

                var nodeToAdd = new TreeNode(e.FileSystemEntry.Path);
                AddLoadingNode(nodeToAdd);
                resultsTree.Nodes.Add(nodeToAdd);
            }
        }

        private void HandleFilteredFileFoundTriggeredByBrowseButton(object sender, FileSystemEntryEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(() => HandleFilteredFileFoundTriggeredByBrowseButton(sender, e));
            }
            else
            {
                HandleFileFoundTriggeredByBrowseButton(sender, e);
            }
        }

        private void HandleFilteredDirectoryFoundTriggeredByBrowseButton(object sender, FileSystemEntryEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(() => HandleFilteredDirectoryFoundTriggeredByBrowseButton(sender, e));
            }
            else
            {
                HandleDirectoryFoundTriggeredByBrowseButton(sender, e);
            }
        }
    }
}
