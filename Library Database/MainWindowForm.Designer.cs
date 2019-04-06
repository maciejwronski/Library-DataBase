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
            // MainWindowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DataGenerationButton);
            this.Name = "MainWindowForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button DataGenerationButton;
    }
}

