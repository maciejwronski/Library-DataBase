namespace Library_Database
{
    partial class MainWindowForm
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
            this.DataGenerationButton = new System.Windows.Forms.Button();
            this.CreateUsers = new System.Windows.Forms.Button();
            this.TotalUsersTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.NumberOfUsers = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataGenerationButton
            // 
            this.DataGenerationButton.Location = new System.Drawing.Point(598, 47);
            this.DataGenerationButton.Name = "DataGenerationButton";
            this.DataGenerationButton.Size = new System.Drawing.Size(150, 59);
            this.DataGenerationButton.TabIndex = 0;
            this.DataGenerationButton.Text = "GenerateData";
            this.DataGenerationButton.UseVisualStyleBackColor = true;
            this.DataGenerationButton.Click += new System.EventHandler(this.GenerateData);
            // 
            // CreateUsers
            // 
            this.CreateUsers.Location = new System.Drawing.Point(203, 27);
            this.CreateUsers.Name = "CreateUsers";
            this.CreateUsers.Size = new System.Drawing.Size(146, 92);
            this.CreateUsers.TabIndex = 1;
            this.CreateUsers.Text = "Create List of Users";
            this.CreateUsers.UseVisualStyleBackColor = true;
            this.CreateUsers.Click += new System.EventHandler(this.CreateListOfUsers);
            // 
            // TotalUsersTextBox
            // 
            this.TotalUsersTextBox.Location = new System.Drawing.Point(56, 64);
            this.TotalUsersTextBox.Name = "TotalUsersTextBox";
            this.TotalUsersTextBox.Size = new System.Drawing.Size(100, 20);
            this.TotalUsersTextBox.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.NumberOfUsers);
            this.groupBox1.Controls.Add(this.CreateUsers);
            this.groupBox1.Controls.Add(this.TotalUsersTextBox);
            this.groupBox1.Location = new System.Drawing.Point(393, 140);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 156);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // NumberOfUsers
            // 
            this.NumberOfUsers.AutoSize = true;
            this.NumberOfUsers.Location = new System.Drawing.Point(-3, 16);
            this.NumberOfUsers.Name = "NumberOfUsers";
            this.NumberOfUsers.Size = new System.Drawing.Size(88, 13);
            this.NumberOfUsers.TabIndex = 3;
            this.NumberOfUsers.Text = "Number Of Users";
            // 
            // MainWindowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.DataGenerationButton);
            this.Name = "MainWindowForm";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button DataGenerationButton;
        private System.Windows.Forms.Button CreateUsers;
        private System.Windows.Forms.TextBox TotalUsersTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label NumberOfUsers;
    }
}

