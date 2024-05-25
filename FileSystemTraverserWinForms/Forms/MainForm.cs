using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using FileSystemTraverser.Core;

namespace FileSystemTraverserWinForms
{
    public partial class MainForm : Form
    {
        private bool IsFilterTextBoxEmpty => string.IsNullOrWhiteSpace(_currentFilterText);
        private bool IsFolderToSearchTextBoxEmpty => string.IsNullOrWhiteSpace(_currentFolderToSearchText);

        private string _currentFolderToSearchText;
        private string _currentFilterText;
        private bool _currentApplyFilterCheckBoxState;

        private string _lastFolderToSearchTextWithResultsRetrieved;
        private string _lastFilterTextWithResultsRetrieved;
        private bool _lastApplyFilterCheckBoxStateWithResultsRetrieved;

        private readonly Dictionary<TreeNodeCollection, CancellationTokenSource> _nodeCollectionCancellationTokens = [];
        private readonly FileSystemVisitor _fileSystemVisitor;

        public MainForm()
        {
            InitializeComponent();
            _fileSystemVisitor = new FileSystemVisitor();
        }

        //this mechanism of checking if the search button should be enabled or not allows to keep the button disabled
        //in any circumstance if the current search field and filter field text combination and has already been searched and not aborted
        private void EnableSearchButton()
        {
            searchButton.Enabled =
                   _currentApplyFilterCheckBoxState || (!IsFolderToSearchTextBoxEmpty && (_lastFolderToSearchTextWithResultsRetrieved == null || _currentFolderToSearchText != _lastFolderToSearchTextWithResultsRetrieved))
                || _currentApplyFilterCheckBoxState || (!IsFilterTextBoxEmpty && (_lastFilterTextWithResultsRetrieved == null || (_currentFilterText != _lastFilterTextWithResultsRetrieved && _currentFolderToSearchText != _lastFolderToSearchTextWithResultsRetrieved)))
                || _lastApplyFilterCheckBoxStateWithResultsRetrieved != _currentApplyFilterCheckBoxState;
        }

        private void EnableApplyFilterCheckbox()
        {
            applyFilterCheckbox.Enabled = !IsFilterTextBoxEmpty;
        }

        private bool ValidateFolderToSearchBox(string folderToSearch)
        {
            folderToSearchValidationMessageBox.Text = string.Empty;

            if (File.Exists(folderToSearch))
            {
                folderToSearchValidationMessageBox.Text = "The provided folder is actually a file";
                return false;
            }
            else if (!Directory.Exists(folderToSearch))
            {
                folderToSearchValidationMessageBox.Text = $"No such directory found";
                return false;
            }

            return true;
        }

        private void UpdateResultsForTextBox()
        {
            if (string.IsNullOrEmpty(resultsForLabel.Text))
                resultsForLabel.Text = $"Results for: {_currentFolderToSearchText}";
        }

        private void AddRemoveBrowseSearchStatusRelatedHandlers(bool add)
        {
            if (add)
            {
                _fileSystemVisitor.FileSystemSearchStarted += HandleFileSystemSearchStartedTriggeredByBrowseButton;
                _fileSystemVisitor.FileSystemSearchStarted += HandleFileSystemSearchStarted;
                _fileSystemVisitor.FileSystemSearchCompleted += HandleFileSystemSearchCompletedTriggeredByBrowseButton;
                _fileSystemVisitor.FileSystemSearchCompleted += HandleFileSystemSearchCompleted;
                _fileSystemVisitor.FileSystemSearchCompleted += HandleFileSystemSearchCompletedStatusStrip;
            }
            else
            {
                _fileSystemVisitor.FileSystemSearchStarted -= HandleFileSystemSearchStartedTriggeredByBrowseButton;
                _fileSystemVisitor.FileSystemSearchStarted -= HandleFileSystemSearchStarted;
                _fileSystemVisitor.FileSystemSearchCompleted -= HandleFileSystemSearchCompletedTriggeredByBrowseButton;
                _fileSystemVisitor.FileSystemSearchCompleted -= HandleFileSystemSearchCompleted;
                _fileSystemVisitor.FileSystemSearchCompleted -= HandleFileSystemSearchCompletedStatusStrip;
            }
        }

        private void AddRemoveExpandSearchStatusRelatedHandlers(bool add)
        {
            if (add)
            {
                _fileSystemVisitor.FileSystemSearchStarted += HandleFileSystemSearchStarted;
                _fileSystemVisitor.FileSystemSearchCompleted += HandleFileSystemSearchCompleted;
                _fileSystemVisitor.FileSystemSearchCompleted += HandleFileSystemSearchCompletedStatusStrip;
            }
            else
            {
                _fileSystemVisitor.FileSystemSearchStarted -= HandleFileSystemSearchStarted;
                _fileSystemVisitor.FileSystemSearchCompleted -= HandleFileSystemSearchCompleted;
                _fileSystemVisitor.FileSystemSearchCompleted -= HandleFileSystemSearchCompletedStatusStrip;
            }
        }
    }
}
