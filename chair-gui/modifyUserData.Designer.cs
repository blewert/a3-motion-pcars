namespace study1_manager_gui
{
    partial class modifyUserData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(modifyUserData));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.notesBox = new System.Windows.Forms.TextBox();
            this.emailBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.modifyButtonDialog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(26, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Notes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(26, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Email";
            // 
            // notesBox
            // 
            this.notesBox.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notesBox.Location = new System.Drawing.Point(27, 136);
            this.notesBox.Name = "notesBox";
            this.notesBox.Size = new System.Drawing.Size(201, 23);
            this.notesBox.TabIndex = 2;
            // 
            // emailBox
            // 
            this.emailBox.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailBox.Location = new System.Drawing.Point(27, 219);
            this.emailBox.Name = "emailBox";
            this.emailBox.Size = new System.Drawing.Size(201, 23);
            this.emailBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Roboto Black", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(23, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(378, 38);
            this.label3.TabIndex = 4;
            this.label3.Text = "Modify participant 8ed3f";
            // 
            // modifyButtonDialog
            // 
            this.modifyButtonDialog.BackColor = System.Drawing.Color.DarkSlateGray;
            this.modifyButtonDialog.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.modifyButtonDialog.FlatAppearance.BorderSize = 0;
            this.modifyButtonDialog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.modifyButtonDialog.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modifyButtonDialog.ForeColor = System.Drawing.Color.White;
            this.modifyButtonDialog.Location = new System.Drawing.Point(353, 255);
            this.modifyButtonDialog.Name = "modifyButtonDialog";
            this.modifyButtonDialog.Size = new System.Drawing.Size(90, 43);
            this.modifyButtonDialog.TabIndex = 6;
            this.modifyButtonDialog.Text = "MODIFY";
            this.modifyButtonDialog.UseVisualStyleBackColor = true;
            this.modifyButtonDialog.Click += new System.EventHandler(this.modifyButtonDialog_Click);
            // 
            // modifyUserData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkCyan;
            this.ClientSize = new System.Drawing.Size(477, 320);
            this.Controls.Add(this.modifyButtonDialog);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.emailBox);
            this.Controls.Add(this.notesBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "modifyUserData";
            this.Text = "Modify participant data";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.modifyUserData_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox notesBox;
        private System.Windows.Forms.TextBox emailBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button modifyButtonDialog;
    }
}