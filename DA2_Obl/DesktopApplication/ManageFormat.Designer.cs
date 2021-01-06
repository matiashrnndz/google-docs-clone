namespace DesktopApplication
{
    partial class ManageFormat
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
            this.lblFormatName = new System.Windows.Forms.Label();
            this.tbxFormatName = new System.Windows.Forms.TextBox();
            this.btnAddFormat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblFormatName
            // 
            this.lblFormatName.AutoSize = true;
            this.lblFormatName.Location = new System.Drawing.Point(12, 9);
            this.lblFormatName.Name = "lblFormatName";
            this.lblFormatName.Size = new System.Drawing.Size(38, 13);
            this.lblFormatName.TabIndex = 0;
            this.lblFormatName.Text = "Name:";
            // 
            // tbxFormatName
            // 
            this.tbxFormatName.Location = new System.Drawing.Point(15, 25);
            this.tbxFormatName.Name = "tbxFormatName";
            this.tbxFormatName.Size = new System.Drawing.Size(176, 20);
            this.tbxFormatName.TabIndex = 1;
            // 
            // btnAddFormat
            // 
            this.btnAddFormat.Location = new System.Drawing.Point(28, 72);
            this.btnAddFormat.Name = "btnAddFormat";
            this.btnAddFormat.Size = new System.Drawing.Size(143, 37);
            this.btnAddFormat.TabIndex = 2;
            this.btnAddFormat.Text = "Register this Format";
            this.btnAddFormat.UseVisualStyleBackColor = true;
            this.btnAddFormat.Click += new System.EventHandler(this.btnAddFormat_Click);
            // 
            // AddFormat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(209, 129);
            this.Controls.Add(this.btnAddFormat);
            this.Controls.Add(this.tbxFormatName);
            this.Controls.Add(this.lblFormatName);
            this.Name = "AddFormat";
            this.Text = "AddFormat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFormatName;
        private System.Windows.Forms.TextBox tbxFormatName;
        private System.Windows.Forms.Button btnAddFormat;
    }
}