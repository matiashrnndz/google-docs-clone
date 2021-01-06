namespace DesktopApplication
{
    partial class ManageStyle
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSubmitStyle = new System.Windows.Forms.Button();
            this.tbxKey = new System.Windows.Forms.TextBox();
            this.tbxValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Style Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Value:";
            // 
            // btnSubmitStyle
            // 
            this.btnSubmitStyle.Location = new System.Drawing.Point(94, 98);
            this.btnSubmitStyle.Name = "btnSubmitStyle";
            this.btnSubmitStyle.Size = new System.Drawing.Size(131, 40);
            this.btnSubmitStyle.TabIndex = 2;
            this.btnSubmitStyle.Text = "Submit";
            this.btnSubmitStyle.UseVisualStyleBackColor = true;
            this.btnSubmitStyle.Click += new System.EventHandler(this.btnSubmitStyle_Click);
            // 
            // tbxKey
            // 
            this.tbxKey.Location = new System.Drawing.Point(94, 17);
            this.tbxKey.Name = "tbxKey";
            this.tbxKey.Size = new System.Drawing.Size(193, 20);
            this.tbxKey.TabIndex = 3;
            // 
            // tbxValue
            // 
            this.tbxValue.Location = new System.Drawing.Point(94, 55);
            this.tbxValue.Name = "tbxValue";
            this.tbxValue.Size = new System.Drawing.Size(193, 20);
            this.tbxValue.TabIndex = 4;
            // 
            // AddStyle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 162);
            this.Controls.Add(this.tbxValue);
            this.Controls.Add(this.tbxKey);
            this.Controls.Add(this.btnSubmitStyle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AddStyle";
            this.Text = "AddStyle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSubmitStyle;
        private System.Windows.Forms.TextBox tbxKey;
        private System.Windows.Forms.TextBox tbxValue;
    }
}