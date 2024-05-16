using FileSystemTraverser;

namespace FileSystemTraverserWinForms
{
    public partial class MainForm : Form
    {
        private string _currentFolderToSearchText;
        private string _currentFilterText;
        private string _lastFolderToSearchTextWithResultsRetrieved;
        private string _lastFilterTextWithResultsRetrieved;
        private readonly FileSystemVisitor _fileSystemVisitor;

        public MainForm()
        {
            InitializeComponent();
            _fileSystemVisitor = new FileSystemVisitor();
            _fileSystemVisitor.FileSystemSearchStared += HandleFileSystemSearchStarted;
            _fileSystemVisitor.FileSystemSearchCompleted += HandleFileSystemSearchCompleted;
        }

        private async void searchButton_Click(object sender, EventArgs e)
        {
            var folderToSearchText = folderToSearchBox.Text;
            if (!ValidateFolderToSearchBox(folderToSearchText))
                return;

            ShowResultsForTextBox();

            listBox.Items.Clear();
            searchButton.Enabled = false;

            if (!applyFilterCheckbox.Checked)
            {
                await _fileSystemVisitor.StartFileSystemSearch(folderToSearchText);
            }
            else
            {
                applyFilterCheckbox.Enabled = false;
                await _fileSystemVisitor.StartFilteredFileSystemSearch(folderToSearchText, _currentFilterText);
                _lastFilterTextWithResultsRetrieved = _currentFilterText;
            }
            _lastFolderToSearchTextWithResultsRetrieved = folderToSearchText;

            await Task.Run(() =>
            {
                foreach (var VARIABLE in _fileSystemVisitor)
                {
                    listBox.Items.Add(VARIABLE.Name);
                }
            });
        }

        private void EnableSearchButton()
        {
            searchButton.Enabled = !string.IsNullOrWhiteSpace(folderToSearchBox.Text)
                && ((_currentFolderToSearchText != folderToSearchBox.Text
                && (_lastFolderToSearchTextWithResultsRetrieved == null || folderToSearchBox.Text != _lastFolderToSearchTextWithResultsRetrieved))
                    || (_lastFilterTextWithResultsRetrieved == null || filterTextBox.Text != _lastFilterTextWithResultsRetrieved));

            _currentFolderToSearchText = folderToSearchBox.Text;
        }

        private void EnableApplyFilterCheckbox()
        {
            applyFilterCheckbox.Enabled = !string.IsNullOrWhiteSpace(filterTextBox.Text);
        }

        private void ShowResultsForTextBox()
        {
            var folderToSearchText = folderToSearchBox.Text;
            resultsForLabel.Text = $"Results for: {folderToSearchText}";
            resultsForLabel.Visible = true;
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

        void HandleFileSystemSearchStarted(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = $"FileSystemSearch STARTED at {DateTime.Now:MM/dd/yyyThh:mm:ss.ffffff}";
        }

        void HandleFileSystemSearchCompleted(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = ($"FileSystemSearch COMPLETED at {DateTime.Now:MM/dd/yyyThh:mm:ss.ffffff}");
        }

        private void folderToSearchBox_TextChanged(object sender, EventArgs e)
        {
            EnableSearchButton();
            EnableApplyFilterCheckbox();/////
        }

        private void filterTextBox_TextChanged(object sender, EventArgs e)
        {
            _currentFilterText = filterTextBox.Text;
            EnableApplyFilterCheckbox();
            EnableSearchButton();
        }

        private void applyFilterCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            EnableSearchButton();
        }
    }
}
