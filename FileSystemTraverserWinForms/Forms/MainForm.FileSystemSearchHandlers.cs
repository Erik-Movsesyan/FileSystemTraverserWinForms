using System;
using System.Linq;
using System.Windows.Forms;
using FileSystemTraverser.Core.CustomEventArgs;

namespace FileSystemTraverserWinForms
{
    public partial class MainForm
    {
        #region Custom Handlers

        private void HandleFileSystemSearchStarted(object sender, EventArgs e)
        {
            resultsForLabel.Text = string.Empty;
            abortSearchButton.Enabled = true;
            toolStripStatusLabel.Text = $"FileSystemSearch STARTED at {DateTime.Now:MM/dd/yyyThh:mm:ss.ffffff}";
        }

        private void HandleFileSystemSearchCompleted(object sender, EventArgs e)
        {
            EnableApplyFilterCheckbox();
            abortSearchButton.Enabled = false;
        }

        private void HandleFileSystemSearchCompletedStatusStrip(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = $"FileSystemSearch COMPLETED at {DateTime.Now:MM/dd/yyyThh:mm:ss.ffffff}";
        }

        private void HandleFileFound(object sender, FileSystemEntryEventArgs e)
        {
            UpdateResultsForTextBox();

            var fileText = "[File]:".PadRight(17);
            if (InvokeRequired)
            {
                Invoke(() => HandleFileFound(sender, e));
            }
            else
            {
                listBox.Items.Add($"{fileText} {e.FileSystemEntry.Name}");
            }
        }

        private void HandleDirectoryFound(object sender, FileSystemEntryEventArgs e)
        {
            UpdateResultsForTextBox();

            if (InvokeRequired)
            {
                Invoke(() => HandleDirectoryFound(sender, e));
            }
            else
            {
                listBox.Items.Add($"[Directory]: {e.FileSystemEntry.Name}");
            }
        }

        private void HandleFilteredFileFound(object sender, FileSystemEntryEventArgs e)
        {
            UpdateResultsForTextBox();

            var fileText = "[Filtered File]:".PadRight(26);
            if (InvokeRequired)
            {
                Invoke(() => HandleFilteredFileFound(sender, e));
            }
            else
            {
                listBox.Items.Add($"{fileText} {e.FileSystemEntry.Name}");
            }
        }

        private void HandleFilteredDirectoryFound(object sender, FileSystemEntryEventArgs e)
        {
            UpdateResultsForTextBox();
            if (InvokeRequired)
            {
                Invoke(() => HandleFilteredDirectoryFound(sender, e));
            }
            else
            {
                listBox.Items.Add($"[Filtered Directory]: {e.FileSystemEntry.Name}");
            }
        }

        #endregion

        #region Built-in Handlers

        private void HandleFolderToSearchBoxTextChanged(object sender, EventArgs e)
        {
            _currentFolderToSearchText = folderToSearchBox.Text;
            EnableSearchButton();
            EnableApplyFilterCheckbox();
        }

        private void HandleFilterBoxTextChanged(object sender, EventArgs e)
        {
            _currentFilterText = filterTextBox.Text;
            EnableApplyFilterCheckbox();
            EnableSearchButton();
        }

        private void HandleApplyFilterCheckboxStateChanged(object sender, EventArgs e)
        {
            _currentApplyFilterCheckBoxState = applyFilterCheckbox.Checked;
            EnableSearchButton();
        }

        private void HandleAbortSearchButtonClick(object sender, EventArgs e)
        {
            if(_cancellationTokenSource is not null) 
            {
                _cancellationTokenSource.Cancel();
                searchButton.Enabled = true;
                _fileSystemVisitor.FileSystemSearchCompleted -= HandleFileSystemSearchCompletedStatusStrip;
                toolStripStatusLabel.Text = $"FileSystemSearch ABORTED at {DateTime.Now:MM/dd/yyyThh:mm:ss.ffffff}";
            }
        }

        //adds ability to copy results of the search to the clipboard
        private void HandleListBoxMouseUp(object sender, MouseEventArgs e)
        {
            if (listBox.SelectedItems.Count > 0)
            {
                var arrayOfSelectedItems = listBox.SelectedItems.Cast<string>().ToArray();
                string selectedItems = string.Join("\n", arrayOfSelectedItems);
                Clipboard.SetText(selectedItems);
            }
        }

        #endregion
    }
}
