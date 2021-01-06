namespace DesktopApplication
{
    partial class ManageStyleClass
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
            this.btnSubmitStyleClass = new System.Windows.Forms.Button();
            this.tbxStyleClassName = new System.Windows.Forms.TextBox();
            this.cbxStyleClassBasedOn = new System.Windows.Forms.ComboBox();
            this.lblBasedOn = new System.Windows.Forms.Label();
            this.lblStyleClassName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSubmitStyleClass
            // 
            this.btnSubmitStyleClass.Location = new System.Drawing.Point(78, 136);
            this.btnSubmitStyleClass.Name = "btnSubmitStyleClass";
            this.btnSubmitStyleClass.Size = new System.Drawing.Size(112, 37);
            this.btnSubmitStyleClass.TabIndex = 0;
            this.btnSubmitStyleClass.Text = "Submit";
            this.btnSubmitStyleClass.UseVisualStyleBackColor = true;
            this.btnSubmitStyleClass.Click += new System.EventHandler(this.btnSubmitStyleClass_Click);
            // 
            // tbxStyleClassName
            // 
            this.tbxStyleClassName.Location = new System.Drawing.Point(30, 38);
            this.tbxStyleClassName.Name = "tbxStyleClassName";
            this.tbxStyleClassName.Size = new System.Drawing.Size(219, 20);
            this.tbxStyleClassName.TabIndex = 1;
            // 
            // cbxStyleClassBasedOn
            // 
            this.cbxStyleClassBasedOn.FormattingEnabled = true;
            this.cbxStyleClassBasedOn.Location = new System.Drawing.Point(114, 94);
            this.cbxStyleClassBasedOn.Name = "cbxStyleClassBasedOn";
            this.cbxStyleClassBasedOn.Size = new System.Drawing.Size(121, 21);
            this.cbxStyleClassBasedOn.TabIndex = 2;
            // 
            // lblBasedOn
            // 
            this.lblBasedOn.AutoSize = true;
            this.lblBasedOn.Location = new System.Drawing.Point(44, 97);
            this.lblBasedOn.Name = "lblBasedOn";
            this.lblBasedOn.Size = new System.Drawing.Size(57, 13);
            this.lblBasedOn.TabIndex = 3;
            this.lblBasedOn.Text = "Based On:";
            // 
            // lblStyleClassName
            // 
            this.lblStyleClassName.AutoSize = true;
            this.lblStyleClassName.Location = new System.Drawing.Point(27, 22);
            this.lblStyleClassName.Name = "lblStyleClassName";
            this.lblStyleClassName.Size = new System.Drawing.Size(38, 13);
            this.lblStyleClassName.TabIndex = 4;
            this.lblStyleClassName.Text = "Name:";
            // 
            // ManageStyleClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 195);
            this.Controls.Add(this.lblStyleClassName);
            this.Controls.Add(this.lblBasedOn);
            this.Controls.Add(this.cbxStyleClassBasedOn);
            this.Controls.Add(this.tbxStyleClassName);
            this.Controls.Add(this.btnSubmitStyleClass);
            this.Name = "ManageStyleClass";
            this.Text = "ManageStyleClass";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubmitStyleClass;
        private System.Windows.Forms.TextBox tbxStyleClassName;
        private System.Windows.Forms.ComboBox cbxStyleClassBasedOn;
        private System.Windows.Forms.Label lblBasedOn;
        private System.Windows.Forms.Label lblStyleClassName;
    }
}