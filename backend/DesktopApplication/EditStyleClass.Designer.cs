namespace DesktopApplication
{
    partial class EditStyleClass
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
            this.lblStyleClassName = new System.Windows.Forms.Label();
            this.lblBasedOn = new System.Windows.Forms.Label();
            this.cbxStyleClassBasedOn = new System.Windows.Forms.ComboBox();
            this.tbxStyleClassName = new System.Windows.Forms.TextBox();
            this.btnSubmitStyleClass = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblStyleClassName
            // 
            this.lblStyleClassName.AutoSize = true;
            this.lblStyleClassName.Location = new System.Drawing.Point(21, 16);
            this.lblStyleClassName.Name = "lblStyleClassName";
            this.lblStyleClassName.Size = new System.Drawing.Size(38, 13);
            this.lblStyleClassName.TabIndex = 9;
            this.lblStyleClassName.Text = "Name:";
            // 
            // lblBasedOn
            // 
            this.lblBasedOn.AutoSize = true;
            this.lblBasedOn.Location = new System.Drawing.Point(38, 91);
            this.lblBasedOn.Name = "lblBasedOn";
            this.lblBasedOn.Size = new System.Drawing.Size(57, 13);
            this.lblBasedOn.TabIndex = 8;
            this.lblBasedOn.Text = "Based On:";
            // 
            // cbxStyleClassBasedOn
            // 
            this.cbxStyleClassBasedOn.FormattingEnabled = true;
            this.cbxStyleClassBasedOn.Location = new System.Drawing.Point(108, 88);
            this.cbxStyleClassBasedOn.Name = "cbxStyleClassBasedOn";
            this.cbxStyleClassBasedOn.Size = new System.Drawing.Size(121, 21);
            this.cbxStyleClassBasedOn.TabIndex = 7;
            // 
            // tbxStyleClassName
            // 
            this.tbxStyleClassName.Location = new System.Drawing.Point(24, 32);
            this.tbxStyleClassName.Name = "tbxStyleClassName";
            this.tbxStyleClassName.ReadOnly = true;
            this.tbxStyleClassName.Size = new System.Drawing.Size(219, 20);
            this.tbxStyleClassName.TabIndex = 6;
            // 
            // btnSubmitStyleClass
            // 
            this.btnSubmitStyleClass.Location = new System.Drawing.Point(72, 130);
            this.btnSubmitStyleClass.Name = "btnSubmitStyleClass";
            this.btnSubmitStyleClass.Size = new System.Drawing.Size(112, 37);
            this.btnSubmitStyleClass.TabIndex = 5;
            this.btnSubmitStyleClass.Text = "Submit";
            this.btnSubmitStyleClass.UseVisualStyleBackColor = true;
            this.btnSubmitStyleClass.Click += new System.EventHandler(this.btnSubmitStyleClass_Click);
            // 
            // EditStyleClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 182);
            this.Controls.Add(this.lblStyleClassName);
            this.Controls.Add(this.lblBasedOn);
            this.Controls.Add(this.cbxStyleClassBasedOn);
            this.Controls.Add(this.tbxStyleClassName);
            this.Controls.Add(this.btnSubmitStyleClass);
            this.Name = "EditStyleClass";
            this.Text = "EditStyleClass";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStyleClassName;
        private System.Windows.Forms.Label lblBasedOn;
        private System.Windows.Forms.ComboBox cbxStyleClassBasedOn;
        private System.Windows.Forms.TextBox tbxStyleClassName;
        private System.Windows.Forms.Button btnSubmitStyleClass;
    }
}