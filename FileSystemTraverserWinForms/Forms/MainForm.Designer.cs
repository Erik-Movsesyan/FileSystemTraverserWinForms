using FileSystemTraverser.Core;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FileSystemTraverserWinForms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            folderToSearchBox = new TextBox();
            searchButton = new Button();
            resultsForLabel = new Label();
            folderToSearchValidationMessageBox = new Label();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel = new ToolStripStatusLabel();
            filterTextBox = new TextBox();
            applyFilterCheckbox = new CheckBox();
            filterTextBoxValidationBox = new Label();
            abortSearchButton = new Button();
            browseButton = new Button();
            selectFolderToSearchDialog = new FolderBrowserDialog();
            resultsTree = new TreeView();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // folderToSearchBox
            // 
            folderToSearchBox.Location = new Point(86, 65);
            folderToSearchBox.Multiline = true;
            folderToSearchBox.Name = "folderToSearchBox";
            folderToSearchBox.PlaceholderText = "Which folder do you want to search?";
            folderToSearchBox.Size = new Size(628, 41);
            folderToSearchBox.TabIndex = 1;
            folderToSearchBox.TabStop = false;
            folderToSearchBox.WordWrap = false;
            folderToSearchBox.TextChanged += HandleFolderToSearchBoxTextChanged;
            // 
            // searchButton
            // 
            searchButton.Enabled = false;
            searchButton.Location = new Point(855, 65);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(112, 41);
            searchButton.TabIndex = 2;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += HandleSearchButtonClick;
            // 
            // resultsForLabel
            // 
            resultsForLabel.AutoEllipsis = true;
            resultsForLabel.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            resultsForLabel.Location = new Point(86, 187);
            resultsForLabel.MaximumSize = new Size(881, 50);
            resultsForLabel.Name = "resultsForLabel";
            resultsForLabel.Size = new Size(881, 50);
            resultsForLabel.TabIndex = 4;
            resultsForLabel.TextAlign = ContentAlignment.BottomLeft;
            // 
            // folderToSearchValidationMessageBox
            // 
            folderToSearchValidationMessageBox.AutoEllipsis = true;
            folderToSearchValidationMessageBox.BackColor = Color.Transparent;
            folderToSearchValidationMessageBox.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            folderToSearchValidationMessageBox.ForeColor = Color.Red;
            folderToSearchValidationMessageBox.Location = new Point(86, 40);
            folderToSearchValidationMessageBox.Name = "folderToSearchValidationMessageBox";
            folderToSearchValidationMessageBox.Size = new Size(746, 22);
            folderToSearchValidationMessageBox.TabIndex = 5;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel });
            statusStrip1.Location = new Point(0, 811);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1063, 22);
            statusStrip1.TabIndex = 6;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            toolStripStatusLabel.Name = "toolStripStatusLabel";
            toolStripStatusLabel.Size = new Size(0, 15);
            // 
            // filterTextBox
            // 
            filterTextBox.Location = new Point(86, 138);
            filterTextBox.Name = "filterTextBox";
            filterTextBox.PlaceholderText = "Found files and folders should contain text?";
            filterTextBox.Size = new Size(441, 34);
            filterTextBox.TabIndex = 7;
            filterTextBox.TextChanged += HandleFilterBoxTextChanged;
            // 
            // applyFilterCheckbox
            // 
            applyFilterCheckbox.AutoSize = true;
            applyFilterCheckbox.Enabled = false;
            applyFilterCheckbox.Location = new Point(549, 140);
            applyFilterCheckbox.Name = "applyFilterCheckbox";
            applyFilterCheckbox.Size = new Size(139, 32);
            applyFilterCheckbox.TabIndex = 8;
            applyFilterCheckbox.Text = "Apply Filter";
            applyFilterCheckbox.UseVisualStyleBackColor = true;
            applyFilterCheckbox.CheckedChanged += HandleApplyFilterCheckboxStateChanged;
            // 
            // filterTextBoxValidationBox
            // 
            filterTextBoxValidationBox.AutoEllipsis = true;
            filterTextBoxValidationBox.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            filterTextBoxValidationBox.ForeColor = Color.Red;
            filterTextBoxValidationBox.Location = new Point(86, 109);
            filterTextBoxValidationBox.Name = "filterTextBoxValidationBox";
            filterTextBoxValidationBox.Size = new Size(394, 26);
            filterTextBoxValidationBox.TabIndex = 9;
            filterTextBoxValidationBox.TextAlign = ContentAlignment.BottomLeft;
            // 
            // abortSearchButton
            // 
            abortSearchButton.Enabled = false;
            abortSearchButton.Location = new Point(802, 138);
            abortSearchButton.Name = "abortSearchButton";
            abortSearchButton.Size = new Size(165, 41);
            abortSearchButton.TabIndex = 10;
            abortSearchButton.Text = "Abort search";
            abortSearchButton.UseVisualStyleBackColor = true;
            abortSearchButton.Click += HandleAbortSearchButtonClick;
            // 
            // browseButton
            // 
            browseButton.Location = new Point(720, 65);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(112, 41);
            browseButton.TabIndex = 11;
            browseButton.Text = "Browse";
            browseButton.UseVisualStyleBackColor = true;
            browseButton.Click += HandleBrowseButtonClick;
            // 
            // selectFolderToSearchDialog
            // 
            selectFolderToSearchDialog.Description = "Select a folder to search";
            selectFolderToSearchDialog.ShowHiddenFiles = true;
            // 
            // resultsTree
            // 
            resultsTree.Indent = 25;
            resultsTree.Location = new Point(86, 249);
            resultsTree.Name = "resultsTree";
            resultsTree.Size = new Size(881, 482);
            resultsTree.TabIndex = 12;
            resultsTree.BeforeCollapse += HandleResultsTreeNodeBeforeCollapse;
            resultsTree.BeforeExpand += HandleResultsTreeNodeBeforeExpand;
            resultsTree.AfterExpand += HandleResultsTreeNodeExpanded;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1063, 833);
            Controls.Add(resultsTree);
            Controls.Add(browseButton);
            Controls.Add(abortSearchButton);
            Controls.Add(filterTextBoxValidationBox);
            Controls.Add(applyFilterCheckbox);
            Controls.Add(filterTextBox);
            Controls.Add(statusStrip1);
            Controls.Add(folderToSearchValidationMessageBox);
            Controls.Add(resultsForLabel);
            Controls.Add(searchButton);
            Controls.Add(folderToSearchBox);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.Black;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "File System Traverser";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox folderToSearchBox;
        private Button searchButton;
        private Label resultsForLabel;
        private Label folderToSearchValidationMessageBox;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel;
        private TextBox filterTextBox;
        private CheckBox applyFilterCheckbox;
        private Label filterTextBoxValidationBox;
        private Button abortSearchButton;
        private Button browseButton;
        private FolderBrowserDialog selectFolderToSearchDialog;
        private TreeView resultsTree;
    }
}
