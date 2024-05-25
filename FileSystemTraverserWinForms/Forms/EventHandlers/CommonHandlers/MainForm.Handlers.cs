using System;
using System.Linq;
using System.Windows.Forms;

namespace FileSystemTraverserWinForms
{
    public partial class MainForm
    {
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
            var lastCancellationToken = _nodeCollectionCancellationTokens.Values.Last();
            lastCancellationToken.Cancel();
            searchButton.Enabled = true;
            _fileSystemVisitor.FileSystemSearchCompleted -= HandleFileSystemSearchCompletedStatusStrip;
            toolStripStatusLabel.Text = $"FileSystemSearch ABORTED at {DateTime.Now:MM/dd/yyyThh:mm:ss.ffffff}";
            
        }

        private void HandleBrowseButtonClick(object sender, EventArgs e)
        {
            var result = selectFolderToSearchDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                folderToSearchBox.Text = selectFolderToSearchDialog.SelectedPath;
            }
        }
    }
}
