namespace study1_manager_gui
{
    partial class mainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.searchBox = new System.Windows.Forms.TextBox();
            this.participantLabel = new System.Windows.Forms.Label();
            this.resultsPanel = new System.Windows.Forms.Panel();
            this.dataModifyButton = new System.Windows.Forms.Button();
            this.completionProgressBar = new System.Windows.Forms.ProgressBar();
            this.completionStatusLabel = new System.Windows.Forms.Label();
            this.s3CompletionButton = new System.Windows.Forms.Button();
            this.s2CompletionButton = new System.Windows.Forms.Button();
            this.s1CompletionButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.searchInfoLabel = new System.Windows.Forms.Label();
            this.searchTitleLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.newParticipantButton = new System.Windows.Forms.Button();
            this.practiceLapButtonNormal = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.practiceLapInverted = new System.Windows.Forms.Button();
            this.practiceLapNone = new System.Windows.Forms.Button();
            this.restartSimphynityButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.showButton = new System.Windows.Forms.Button();
            this.participantIdBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.openFileDialogProcess = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelRunning1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelRunning2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.resultsPanel.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchBox
            // 
            this.searchBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.searchBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.searchBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.searchBox.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBox.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.searchBox.Location = new System.Drawing.Point(577, 60);
            this.searchBox.MaxLength = 60;
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(339, 26);
            this.searchBox.TabIndex = 0;
            this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
            this.searchBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.searchBox_KeyPress);
            // 
            // participantLabel
            // 
            this.participantLabel.AutoSize = true;
            this.participantLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.participantLabel.ForeColor = System.Drawing.Color.White;
            this.participantLabel.Location = new System.Drawing.Point(572, 27);
            this.participantLabel.Name = "participantLabel";
            this.participantLabel.Size = new System.Drawing.Size(160, 30);
            this.participantLabel.TabIndex = 1;
            this.participantLabel.Text = "Search by notes";
            // 
            // resultsPanel
            // 
            this.resultsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultsPanel.BackColor = System.Drawing.Color.DarkCyan;
            this.resultsPanel.Controls.Add(this.dataModifyButton);
            this.resultsPanel.Controls.Add(this.completionProgressBar);
            this.resultsPanel.Controls.Add(this.completionStatusLabel);
            this.resultsPanel.Controls.Add(this.s3CompletionButton);
            this.resultsPanel.Controls.Add(this.s2CompletionButton);
            this.resultsPanel.Controls.Add(this.s1CompletionButton);
            this.resultsPanel.Controls.Add(this.panel1);
            this.resultsPanel.Controls.Add(this.searchInfoLabel);
            this.resultsPanel.Controls.Add(this.searchTitleLabel);
            this.resultsPanel.Location = new System.Drawing.Point(-3, 135);
            this.resultsPanel.Name = "resultsPanel";
            this.resultsPanel.Size = new System.Drawing.Size(949, 324);
            this.resultsPanel.TabIndex = 2;
            // 
            // dataModifyButton
            // 
            this.dataModifyButton.BackColor = System.Drawing.Color.DarkSlateGray;
            this.dataModifyButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.dataModifyButton.FlatAppearance.BorderSize = 0;
            this.dataModifyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dataModifyButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataModifyButton.ForeColor = System.Drawing.Color.White;
            this.dataModifyButton.Location = new System.Drawing.Point(23, 254);
            this.dataModifyButton.Name = "dataModifyButton";
            this.dataModifyButton.Size = new System.Drawing.Size(114, 34);
            this.dataModifyButton.TabIndex = 12;
            this.dataModifyButton.Text = "MODIFY DATA";
            this.dataModifyButton.UseVisualStyleBackColor = true;
            this.dataModifyButton.Click += new System.EventHandler(this.dataModifyButton_Click);
            // 
            // completionProgressBar
            // 
            this.completionProgressBar.Cursor = System.Windows.Forms.Cursors.Default;
            this.completionProgressBar.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.completionProgressBar.Location = new System.Drawing.Point(421, 140);
            this.completionProgressBar.Name = "completionProgressBar";
            this.completionProgressBar.Size = new System.Drawing.Size(354, 23);
            this.completionProgressBar.Step = 33;
            this.completionProgressBar.TabIndex = 11;
            // 
            // completionStatusLabel
            // 
            this.completionStatusLabel.AutoSize = true;
            this.completionStatusLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.completionStatusLabel.ForeColor = System.Drawing.Color.White;
            this.completionStatusLabel.Location = new System.Drawing.Point(416, 47);
            this.completionStatusLabel.Name = "completionStatusLabel";
            this.completionStatusLabel.Size = new System.Drawing.Size(192, 30);
            this.completionStatusLabel.TabIndex = 10;
            this.completionStatusLabel.Text = "Completion status";
            // 
            // s3CompletionButton
            // 
            this.s3CompletionButton.BackColor = System.Drawing.Color.DarkSlateGray;
            this.s3CompletionButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.s3CompletionButton.FlatAppearance.BorderSize = 0;
            this.s3CompletionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.s3CompletionButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.s3CompletionButton.ForeColor = System.Drawing.Color.White;
            this.s3CompletionButton.Location = new System.Drawing.Point(661, 85);
            this.s3CompletionButton.Name = "s3CompletionButton";
            this.s3CompletionButton.Size = new System.Drawing.Size(114, 49);
            this.s3CompletionButton.TabIndex = 9;
            this.s3CompletionButton.Text = "SESSION #3 (none)";
            this.s3CompletionButton.UseVisualStyleBackColor = false;
            this.s3CompletionButton.Click += new System.EventHandler(this.s3CompletionButton_Click);
            // 
            // s2CompletionButton
            // 
            this.s2CompletionButton.BackColor = System.Drawing.Color.DarkSlateGray;
            this.s2CompletionButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.s2CompletionButton.FlatAppearance.BorderSize = 0;
            this.s2CompletionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.s2CompletionButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.s2CompletionButton.ForeColor = System.Drawing.Color.White;
            this.s2CompletionButton.Location = new System.Drawing.Point(541, 85);
            this.s2CompletionButton.Name = "s2CompletionButton";
            this.s2CompletionButton.Size = new System.Drawing.Size(114, 49);
            this.s2CompletionButton.TabIndex = 8;
            this.s2CompletionButton.Text = "SESSION #2 (inverted)";
            this.s2CompletionButton.UseVisualStyleBackColor = false;
            this.s2CompletionButton.Click += new System.EventHandler(this.s2CompletionButton_Click);
            // 
            // s1CompletionButton
            // 
            this.s1CompletionButton.BackColor = System.Drawing.Color.DarkSlateGray;
            this.s1CompletionButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.s1CompletionButton.FlatAppearance.BorderSize = 0;
            this.s1CompletionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.s1CompletionButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.s1CompletionButton.ForeColor = System.Drawing.Color.White;
            this.s1CompletionButton.Location = new System.Drawing.Point(421, 85);
            this.s1CompletionButton.Name = "s1CompletionButton";
            this.s1CompletionButton.Size = new System.Drawing.Size(114, 49);
            this.s1CompletionButton.TabIndex = 5;
            this.s1CompletionButton.Text = "SESSION #1 (normal)";
            this.s1CompletionButton.UseVisualStyleBackColor = true;
            this.s1CompletionButton.Click += new System.EventHandler(this.s1CompletionButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkSlateGray;
            this.panel1.Location = new System.Drawing.Point(368, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(10, 258);
            this.panel1.TabIndex = 7;
            // 
            // searchInfoLabel
            // 
            this.searchInfoLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchInfoLabel.ForeColor = System.Drawing.Color.White;
            this.searchInfoLabel.Location = new System.Drawing.Point(19, 85);
            this.searchInfoLabel.Name = "searchInfoLabel";
            this.searchInfoLabel.Size = new System.Drawing.Size(320, 130);
            this.searchInfoLabel.TabIndex = 6;
            this.searchInfoLabel.Text = "Group: normal -> inverted -> no motion\r\nCompleted sessions: 1, 2\r\nReal ID: 16\r\nNa" +
    "me/Notes: Ben\r\nEmail: blah";
            // 
            // searchTitleLabel
            // 
            this.searchTitleLabel.AutoSize = true;
            this.searchTitleLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchTitleLabel.ForeColor = System.Drawing.Color.White;
            this.searchTitleLabel.Location = new System.Drawing.Point(18, 47);
            this.searchTitleLabel.Name = "searchTitleLabel";
            this.searchTitleLabel.Size = new System.Drawing.Size(188, 30);
            this.searchTitleLabel.TabIndex = 5;
            this.searchTitleLabel.Text = "Participant 8ED32";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(378, 50);
            this.label1.TabIndex = 3;
            this.label1.Text = "Participant Manager";
            // 
            // newParticipantButton
            // 
            this.newParticipantButton.BackColor = System.Drawing.Color.DarkCyan;
            this.newParticipantButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.newParticipantButton.FlatAppearance.BorderSize = 0;
            this.newParticipantButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newParticipantButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newParticipantButton.ForeColor = System.Drawing.Color.White;
            this.newParticipantButton.Location = new System.Drawing.Point(20, 88);
            this.newParticipantButton.Name = "newParticipantButton";
            this.newParticipantButton.Size = new System.Drawing.Size(126, 34);
            this.newParticipantButton.TabIndex = 4;
            this.newParticipantButton.Text = "NEW PARTICIPANT";
            this.newParticipantButton.UseVisualStyleBackColor = false;
            this.newParticipantButton.Click += new System.EventHandler(this.newParticipantButton_Click);
            // 
            // practiceLapButtonNormal
            // 
            this.practiceLapButtonNormal.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.practiceLapButtonNormal.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.practiceLapButtonNormal.FlatAppearance.BorderSize = 0;
            this.practiceLapButtonNormal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.practiceLapButtonNormal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.practiceLapButtonNormal.ForeColor = System.Drawing.Color.White;
            this.practiceLapButtonNormal.Location = new System.Drawing.Point(20, 500);
            this.practiceLapButtonNormal.Name = "practiceLapButtonNormal";
            this.practiceLapButtonNormal.Size = new System.Drawing.Size(114, 49);
            this.practiceLapButtonNormal.TabIndex = 12;
            this.practiceLapButtonNormal.Text = "PRACTICE LAP (normal)";
            this.practiceLapButtonNormal.UseVisualStyleBackColor = false;
            this.practiceLapButtonNormal.Click += new System.EventHandler(this.practiceLapButtonNormal_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(15, 462);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 30);
            this.label2.TabIndex = 13;
            this.label2.Text = "Tools";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // practiceLapInverted
            // 
            this.practiceLapInverted.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.practiceLapInverted.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.practiceLapInverted.FlatAppearance.BorderSize = 0;
            this.practiceLapInverted.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.practiceLapInverted.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.practiceLapInverted.ForeColor = System.Drawing.Color.White;
            this.practiceLapInverted.Location = new System.Drawing.Point(140, 500);
            this.practiceLapInverted.Name = "practiceLapInverted";
            this.practiceLapInverted.Size = new System.Drawing.Size(114, 49);
            this.practiceLapInverted.TabIndex = 14;
            this.practiceLapInverted.Text = "PRACTICE LAP (inverted)";
            this.practiceLapInverted.UseVisualStyleBackColor = false;
            this.practiceLapInverted.Click += new System.EventHandler(this.practiceLapInverted_Click);
            // 
            // practiceLapNone
            // 
            this.practiceLapNone.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.practiceLapNone.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.practiceLapNone.FlatAppearance.BorderSize = 0;
            this.practiceLapNone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.practiceLapNone.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.practiceLapNone.ForeColor = System.Drawing.Color.White;
            this.practiceLapNone.Location = new System.Drawing.Point(260, 500);
            this.practiceLapNone.Name = "practiceLapNone";
            this.practiceLapNone.Size = new System.Drawing.Size(114, 49);
            this.practiceLapNone.TabIndex = 15;
            this.practiceLapNone.Text = "PRACTICE LAP (none)";
            this.practiceLapNone.UseVisualStyleBackColor = false;
            this.practiceLapNone.Click += new System.EventHandler(this.practiceLapNone_Click);
            // 
            // restartSimphynityButton
            // 
            this.restartSimphynityButton.BackColor = System.Drawing.Color.DarkSlateGray;
            this.restartSimphynityButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.restartSimphynityButton.FlatAppearance.BorderSize = 0;
            this.restartSimphynityButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.restartSimphynityButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restartSimphynityButton.ForeColor = System.Drawing.Color.White;
            this.restartSimphynityButton.Location = new System.Drawing.Point(446, 500);
            this.restartSimphynityButton.Name = "restartSimphynityButton";
            this.restartSimphynityButton.Size = new System.Drawing.Size(114, 49);
            this.restartSimphynityButton.TabIndex = 16;
            this.restartSimphynityButton.Text = "RESTART SIMPHYNITY";
            this.restartSimphynityButton.UseVisualStyleBackColor = false;
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "xlsx";
            this.openFileDialog.Filter = "Spreadsheet files|*.xlsx";
            // 
            // showButton
            // 
            this.showButton.BackColor = System.Drawing.Color.DarkCyan;
            this.showButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.showButton.FlatAppearance.BorderSize = 0;
            this.showButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showButton.ForeColor = System.Drawing.Color.White;
            this.showButton.Location = new System.Drawing.Point(841, 89);
            this.showButton.Name = "showButton";
            this.showButton.Size = new System.Drawing.Size(75, 34);
            this.showButton.TabIndex = 17;
            this.showButton.Text = "SHOW";
            this.showButton.UseVisualStyleBackColor = false;
            this.showButton.Click += new System.EventHandler(this.showButton_Click);
            // 
            // participantIdBox
            // 
            this.participantIdBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.participantIdBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.participantIdBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.participantIdBox.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.participantIdBox.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.participantIdBox.Location = new System.Drawing.Point(428, 60);
            this.participantIdBox.MaxLength = 60;
            this.participantIdBox.Name = "participantIdBox";
            this.participantIdBox.Size = new System.Drawing.Size(82, 26);
            this.participantIdBox.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(423, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 30);
            this.label3.TabIndex = 19;
            this.label3.Text = "Search by ID";
            // 
            // openFileDialogProcess
            // 
            this.openFileDialogProcess.DefaultExt = "bat";
            this.openFileDialogProcess.Filter = "Batch files|*.bat";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.MediumTurquoise;
            this.statusStrip1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.statusLabelRunning1,
            this.toolStripStatusLabel1,
            this.statusLabelRunning2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 564);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(945, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 20;
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(119, 17);
            this.statusLabel.Text = "Telemetry Writer";
            this.statusLabel.Click += new System.EventHandler(this.statusLabel_Click);
            // 
            // statusLabelRunning1
            // 
            this.statusLabelRunning1.BackColor = System.Drawing.Color.Green;
            this.statusLabelRunning1.ForeColor = System.Drawing.Color.White;
            this.statusLabelRunning1.Name = "statusLabelRunning1";
            this.statusLabelRunning1.Size = new System.Drawing.Size(56, 17);
            this.statusLabelRunning1.Text = "RUNNING";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(112, 17);
            this.toolStripStatusLabel1.Text = "       Inverter";
            // 
            // statusLabelRunning2
            // 
            this.statusLabelRunning2.BackColor = System.Drawing.Color.Green;
            this.statusLabelRunning2.ForeColor = System.Drawing.Color.White;
            this.statusLabelRunning2.Name = "statusLabelRunning2";
            this.statusLabelRunning2.Size = new System.Drawing.Size(56, 17);
            this.statusLabelRunning2.Text = "RUNNING";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkTurquoise;
            this.ClientSize = new System.Drawing.Size(945, 586);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.participantIdBox);
            this.Controls.Add(this.showButton);
            this.Controls.Add(this.restartSimphynityButton);
            this.Controls.Add(this.practiceLapNone);
            this.Controls.Add(this.newParticipantButton);
            this.Controls.Add(this.practiceLapInverted);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.practiceLapButtonNormal);
            this.Controls.Add(this.resultsPanel);
            this.Controls.Add(this.participantLabel);
            this.Controls.Add(this.searchBox);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "mainForm";
            this.Text = "Study #1 Participant Manager";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.mainForm_FormClosed);
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.resultsPanel.ResumeLayout(false);
            this.resultsPanel.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Label participantLabel;
        private System.Windows.Forms.Panel resultsPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button newParticipantButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label searchInfoLabel;
        private System.Windows.Forms.Label searchTitleLabel;
        private System.Windows.Forms.Label completionStatusLabel;
        private System.Windows.Forms.Button s3CompletionButton;
        private System.Windows.Forms.Button s2CompletionButton;
        private System.Windows.Forms.Button s1CompletionButton;
        private System.Windows.Forms.ProgressBar completionProgressBar;
        private System.Windows.Forms.Button practiceLapButtonNormal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button practiceLapNone;
        private System.Windows.Forms.Button practiceLapInverted;
        private System.Windows.Forms.Button restartSimphynityButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button showButton;
        private System.Windows.Forms.TextBox participantIdBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button dataModifyButton;
        private System.Windows.Forms.OpenFileDialog openFileDialogProcess;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelRunning1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelRunning2;
        private System.Windows.Forms.Timer timer;
    }
}

