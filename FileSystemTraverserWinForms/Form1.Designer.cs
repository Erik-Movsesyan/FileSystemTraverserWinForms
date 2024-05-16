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
            listBox = new ListBox();
            resultsForLabel = new Label();
            folderToSearchValidationMessageBox = new Label();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel = new ToolStripStatusLabel();
            filterTextBox = new TextBox();
            applyFilterCheckbox = new CheckBox();
            filterTextBoxValidationBox = new Label();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // folderToSearchBox
            // 
            folderToSearchBox.Location = new Point(86, 65);
            folderToSearchBox.Multiline = true;
            folderToSearchBox.Name = "folderToSearchBox";
            folderToSearchBox.PlaceholderText = "Which folder do you want to search?";
            folderToSearchBox.Size = new Size(538, 41);
            folderToSearchBox.TabIndex = 1;
            folderToSearchBox.TabStop = false;
            folderToSearchBox.WordWrap = false;
            folderToSearchBox.TextChanged += folderToSearchBox_TextChanged;
            // 
            // searchButton
            // 
            searchButton.Enabled = false;
            searchButton.Location = new Point(642, 65);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(112, 41);
            searchButton.TabIndex = 2;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += searchButton_Click;
            // 
            // listBox
            // 
            listBox.FormattingEnabled = true;
            listBox.ItemHeight = 28;
            listBox.Location = new Point(86, 240);
            listBox.Name = "listBox";
            listBox.Size = new Size(668, 508);
            listBox.TabIndex = 3;
            listBox.SelectedIndexChanged += listBox_SelectedIndexChanged;
            // 
            // resultsForLabel
            // 
            resultsForLabel.AutoEllipsis = true;
            resultsForLabel.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            resultsForLabel.Location = new Point(86, 187);
            resultsForLabel.MaximumSize = new Size(668, 50);
            resultsForLabel.Name = "resultsForLabel";
            resultsForLabel.Size = new Size(668, 50);
            resultsForLabel.TabIndex = 4;
            resultsForLabel.TextAlign = ContentAlignment.BottomLeft;
            resultsForLabel.Visible = false;
            // 
            // folderToSearchValidationMessageBox
            // 
            folderToSearchValidationMessageBox.AutoEllipsis = true;
            folderToSearchValidationMessageBox.BackColor = Color.Transparent;
            folderToSearchValidationMessageBox.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            folderToSearchValidationMessageBox.ForeColor = Color.Red;
            folderToSearchValidationMessageBox.Location = new Point(86, 40);
            folderToSearchValidationMessageBox.Name = "folderToSearchValidationMessageBox";
            folderToSearchValidationMessageBox.Size = new Size(538, 22);
            folderToSearchValidationMessageBox.TabIndex = 5;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel });
            statusStrip1.Location = new Point(0, 811);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(856, 22);
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
            filterTextBox.Size = new Size(394, 34);
            filterTextBox.TabIndex = 7;
            // 
            // applyFilterCheckbox
            // 
            applyFilterCheckbox.AutoSize = true;
            applyFilterCheckbox.Location = new Point(490, 140);
            applyFilterCheckbox.Name = "applyFilterCheckbox";
            applyFilterCheckbox.Size = new Size(139, 32);
            applyFilterCheckbox.TabIndex = 8;
            applyFilterCheckbox.Text = "Apply Filter";
            applyFilterCheckbox.UseVisualStyleBackColor = true;
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(856, 833);
            Controls.Add(filterTextBoxValidationBox);
            Controls.Add(applyFilterCheckbox);
            Controls.Add(filterTextBox);
            Controls.Add(statusStrip1);
            Controls.Add(folderToSearchValidationMessageBox);
            Controls.Add(resultsForLabel);
            Controls.Add(listBox);
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
        private ListBox listBox;
        private Label resultsForLabel;
        private Label folderToSearchValidationMessageBox;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel;
        private TextBox filterTextBox;
        private CheckBox applyFilterCheckbox;
        private Label filterTextBoxValidationBox;
    }
}
