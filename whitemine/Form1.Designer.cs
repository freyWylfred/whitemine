namespace whitemine
{
    partial class Form1
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
            panelHeader = Theme.CreateHeader("Whitemine", "Redmine desktop companion");
            panelSettings = Theme.CreateCard();
            labelUrl = Theme.CreateFieldLabel("REDMINE URL");
            textBoxUrl = new TextBox();
            labelApiKey = Theme.CreateFieldLabel("API KEY");
            textBoxApiKey = new TextBox();
            buttonSave = new Button();
            buttonTest = new Button();
            labelIssuesTitle = new Label();
            buttonMyIssues = new Button();
            panelGrid = Theme.CreateCard();
            dataGridViewIssues = new DataGridView();
            labelStatus = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewIssues).BeginInit();
            panelSettings.SuspendLayout();
            panelGrid.SuspendLayout();
            SuspendLayout();
            // 
            // panelSettings
            // 
            panelSettings.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelSettings.Location = new Point(24, 96);
            panelSettings.Name = "panelSettings";
            panelSettings.Size = new Size(852, 128);
            panelSettings.TabIndex = 0;
            // 
            // labelUrl
            // 
            labelUrl.Location = new Point(20, 18);
            labelUrl.Name = "labelUrl";
            // 
            // textBoxUrl
            // 
            textBoxUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxUrl.Location = new Point(20, 38);
            textBoxUrl.Name = "textBoxUrl";
            textBoxUrl.PlaceholderText = "https://redmine.example.com";
            textBoxUrl.Size = new Size(470, 25);
            textBoxUrl.TabIndex = 1;
            // 
            // labelApiKey
            // 
            labelApiKey.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelApiKey.Location = new Point(510, 18);
            labelApiKey.Name = "labelApiKey";
            // 
            // textBoxApiKey
            // 
            textBoxApiKey.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBoxApiKey.Location = new Point(510, 38);
            textBoxApiKey.Name = "textBoxApiKey";
            textBoxApiKey.PlaceholderText = "Your personal API access key";
            textBoxApiKey.Size = new Size(322, 25);
            textBoxApiKey.TabIndex = 2;
            textBoxApiKey.UseSystemPasswordChar = true;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(20, 78);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(110, 32);
            buttonSave.TabIndex = 3;
            buttonSave.Text = "Save Settings";
            buttonSave.Click += buttonSave_Click;
            // 
            // buttonTest
            // 
            buttonTest.Location = new Point(140, 78);
            buttonTest.Name = "buttonTest";
            buttonTest.Size = new Size(130, 32);
            buttonTest.TabIndex = 4;
            buttonTest.Text = "Test Connection";
            buttonTest.Click += buttonTest_Click;
            // 
            // labelStatus
            // 
            labelStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelStatus.AutoEllipsis = true;
            labelStatus.Location = new Point(284, 84);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(548, 20);
            labelStatus.TabIndex = 5;
            labelStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelIssuesTitle
            // 
            labelIssuesTitle.AutoSize = true;
            labelIssuesTitle.Font = Theme.SectionFont;
            labelIssuesTitle.ForeColor = Theme.Text;
            labelIssuesTitle.Location = new Point(24, 248);
            labelIssuesTitle.Name = "labelIssuesTitle";
            labelIssuesTitle.Text = "My Issues";
            // 
            // buttonMyIssues
            // 
            buttonMyIssues.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonMyIssues.Location = new Point(766, 242);
            buttonMyIssues.Name = "buttonMyIssues";
            buttonMyIssues.Size = new Size(110, 32);
            buttonMyIssues.TabIndex = 6;
            buttonMyIssues.Text = "Refresh";
            buttonMyIssues.Click += buttonMyIssues_Click;
            // 
            // panelGrid
            // 
            panelGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelGrid.Location = new Point(24, 284);
            panelGrid.Name = "panelGrid";
            panelGrid.Padding = new Padding(1);
            panelGrid.Size = new Size(852, 292);
            panelGrid.TabIndex = 7;
            // 
            // dataGridViewIssues
            // 
            dataGridViewIssues.AllowUserToAddRows = false;
            dataGridViewIssues.AllowUserToDeleteRows = false;
            dataGridViewIssues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewIssues.Dock = DockStyle.Fill;
            dataGridViewIssues.Name = "dataGridViewIssues";
            dataGridViewIssues.ReadOnly = true;
            dataGridViewIssues.TabIndex = 0;
            dataGridViewIssues.CellDoubleClick += dataGridViewIssues_CellDoubleClick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 600);
            MinimumSize = new Size(760, 520);
            panelSettings.Controls.Add(labelUrl);
            panelSettings.Controls.Add(textBoxUrl);
            panelSettings.Controls.Add(labelApiKey);
            panelSettings.Controls.Add(textBoxApiKey);
            panelSettings.Controls.Add(buttonSave);
            panelSettings.Controls.Add(buttonTest);
            panelSettings.Controls.Add(labelStatus);
            panelGrid.Controls.Add(dataGridViewIssues);
            Controls.Add(panelSettings);
            Controls.Add(labelIssuesTitle);
            Controls.Add(buttonMyIssues);
            Controls.Add(panelGrid);
            Controls.Add(panelHeader);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Whitemine";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewIssues).EndInit();
            panelSettings.ResumeLayout(false);
            panelGrid.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelHeader;
        private Panel panelSettings;
        private Label labelUrl;
        private TextBox textBoxUrl;
        private Label labelApiKey;
        private TextBox textBoxApiKey;
        private Button buttonSave;
        private Button buttonTest;
        private Label labelIssuesTitle;
        private Button buttonMyIssues;
        private Panel panelGrid;
        private DataGridView dataGridViewIssues;
        private Label labelStatus;
    }
}
