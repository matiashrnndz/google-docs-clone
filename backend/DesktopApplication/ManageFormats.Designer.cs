namespace DesktopApplication
{
    partial class ManageFormats
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblFormats = new System.Windows.Forms.Label();
            this.lbxFormats = new System.Windows.Forms.ListBox();
            this.lbxStyleClasses = new System.Windows.Forms.ListBox();
            this.lblStyleClasses = new System.Windows.Forms.Label();
            this.lbxStyles = new System.Windows.Forms.ListBox();
            this.lblStyles = new System.Windows.Forms.Label();
            this.btnAddFormat = new System.Windows.Forms.Button();
            this.btnAddStyle = new System.Windows.Forms.Button();
            this.btnAddClass = new System.Windows.Forms.Button();
            this.btnDeleteStyle = new System.Windows.Forms.Button();
            this.btnDeleteFormat = new System.Windows.Forms.Button();
            this.btnDeleteClass = new System.Windows.Forms.Button();
            this.btnEditClass = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblFormats
            // 
            this.lblFormats.AutoSize = true;
            this.lblFormats.Location = new System.Drawing.Point(21, 21);
            this.lblFormats.Name = "lblFormats";
            this.lblFormats.Size = new System.Drawing.Size(47, 13);
            this.lblFormats.TabIndex = 0;
            this.lblFormats.Text = "Formats:";
            // 
            // lbxFormats
            // 
            this.lbxFormats.FormattingEnabled = true;
            this.lbxFormats.Location = new System.Drawing.Point(13, 37);
            this.lbxFormats.Name = "lbxFormats";
            this.lbxFormats.Size = new System.Drawing.Size(210, 134);
            this.lbxFormats.TabIndex = 1;
            this.lbxFormats.SelectedIndexChanged += new System.EventHandler(this.lbxFormats_SelectedIndexChanged);
            // 
            // lbxStyleClasses
            // 
            this.lbxStyleClasses.FormattingEnabled = true;
            this.lbxStyleClasses.Location = new System.Drawing.Point(13, 238);
            this.lbxStyleClasses.Name = "lbxStyleClasses";
            this.lbxStyleClasses.Size = new System.Drawing.Size(210, 134);
            this.lbxStyleClasses.TabIndex = 3;
            this.lbxStyleClasses.SelectedIndexChanged += new System.EventHandler(this.lbxStyleClasses_SelectedIndexChanged);
            // 
            // lblStyleClasses
            // 
            this.lblStyleClasses.AutoSize = true;
            this.lblStyleClasses.Location = new System.Drawing.Point(21, 222);
            this.lblStyleClasses.Name = "lblStyleClasses";
            this.lblStyleClasses.Size = new System.Drawing.Size(72, 13);
            this.lblStyleClasses.TabIndex = 2;
            this.lblStyleClasses.Text = "Style Classes:";
            // 
            // lbxStyles
            // 
            this.lbxStyles.FormattingEnabled = true;
            this.lbxStyles.Location = new System.Drawing.Point(345, 37);
            this.lbxStyles.Name = "lbxStyles";
            this.lbxStyles.Size = new System.Drawing.Size(210, 342);
            this.lbxStyles.TabIndex = 5;
            // 
            // lblStyles
            // 
            this.lblStyles.AutoSize = true;
            this.lblStyles.Location = new System.Drawing.Point(353, 21);
            this.lblStyles.Name = "lblStyles";
            this.lblStyles.Size = new System.Drawing.Size(38, 13);
            this.lblStyles.TabIndex = 4;
            this.lblStyles.Text = "Styles:";
            // 
            // btnAddFormat
            // 
            this.btnAddFormat.Location = new System.Drawing.Point(229, 52);
            this.btnAddFormat.Name = "btnAddFormat";
            this.btnAddFormat.Size = new System.Drawing.Size(98, 24);
            this.btnAddFormat.TabIndex = 6;
            this.btnAddFormat.Text = "Add Format";
            this.btnAddFormat.UseVisualStyleBackColor = true;
            this.btnAddFormat.Click += new System.EventHandler(this.btnAddFormat_Click);
            // 
            // btnAddStyle
            // 
            this.btnAddStyle.Location = new System.Drawing.Point(570, 52);
            this.btnAddStyle.Name = "btnAddStyle";
            this.btnAddStyle.Size = new System.Drawing.Size(98, 24);
            this.btnAddStyle.TabIndex = 7;
            this.btnAddStyle.Text = "Add Style";
            this.btnAddStyle.UseVisualStyleBackColor = true;
            this.btnAddStyle.Click += new System.EventHandler(this.btnAddStyle_Click);
            // 
            // btnAddClass
            // 
            this.btnAddClass.Location = new System.Drawing.Point(229, 249);
            this.btnAddClass.Name = "btnAddClass";
            this.btnAddClass.Size = new System.Drawing.Size(98, 24);
            this.btnAddClass.TabIndex = 8;
            this.btnAddClass.Text = "Add Class";
            this.btnAddClass.UseVisualStyleBackColor = true;
            this.btnAddClass.Click += new System.EventHandler(this.btnAddClass_Click);
            // 
            // btnDeleteStyle
            // 
            this.btnDeleteStyle.Location = new System.Drawing.Point(570, 82);
            this.btnDeleteStyle.Name = "btnDeleteStyle";
            this.btnDeleteStyle.Size = new System.Drawing.Size(98, 24);
            this.btnDeleteStyle.TabIndex = 9;
            this.btnDeleteStyle.Text = "Delete Style";
            this.btnDeleteStyle.UseVisualStyleBackColor = true;
            this.btnDeleteStyle.Click += new System.EventHandler(this.btnDeleteStyle_Click);
            // 
            // btnDeleteFormat
            // 
            this.btnDeleteFormat.Location = new System.Drawing.Point(229, 82);
            this.btnDeleteFormat.Name = "btnDeleteFormat";
            this.btnDeleteFormat.Size = new System.Drawing.Size(98, 24);
            this.btnDeleteFormat.TabIndex = 10;
            this.btnDeleteFormat.Text = "Delete Format";
            this.btnDeleteFormat.UseVisualStyleBackColor = true;
            this.btnDeleteFormat.Click += new System.EventHandler(this.btnDeleteFormat_Click);
            // 
            // btnDeleteClass
            // 
            this.btnDeleteClass.Location = new System.Drawing.Point(229, 309);
            this.btnDeleteClass.Name = "btnDeleteClass";
            this.btnDeleteClass.Size = new System.Drawing.Size(98, 24);
            this.btnDeleteClass.TabIndex = 11;
            this.btnDeleteClass.Text = "Delete Class";
            this.btnDeleteClass.UseVisualStyleBackColor = true;
            this.btnDeleteClass.Click += new System.EventHandler(this.btnDeleteClass_Click);
            // 
            // btnEditClass
            // 
            this.btnEditClass.Location = new System.Drawing.Point(229, 279);
            this.btnEditClass.Name = "btnEditClass";
            this.btnEditClass.Size = new System.Drawing.Size(98, 24);
            this.btnEditClass.TabIndex = 12;
            this.btnEditClass.Text = "Edit Class";
            this.btnEditClass.UseVisualStyleBackColor = true;
            this.btnEditClass.Click += new System.EventHandler(this.btnEditClass_Click);
            // 
            // ManageFormats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnEditClass);
            this.Controls.Add(this.btnDeleteClass);
            this.Controls.Add(this.btnDeleteFormat);
            this.Controls.Add(this.btnDeleteStyle);
            this.Controls.Add(this.btnAddClass);
            this.Controls.Add(this.btnAddStyle);
            this.Controls.Add(this.btnAddFormat);
            this.Controls.Add(this.lbxStyles);
            this.Controls.Add(this.lblStyles);
            this.Controls.Add(this.lbxStyleClasses);
            this.Controls.Add(this.lblStyleClasses);
            this.Controls.Add(this.lbxFormats);
            this.Controls.Add(this.lblFormats);
            this.Name = "ManageFormats";
            this.Size = new System.Drawing.Size(717, 449);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFormats;
        private System.Windows.Forms.ListBox lbxFormats;
        private System.Windows.Forms.ListBox lbxStyleClasses;
        private System.Windows.Forms.Label lblStyleClasses;
        private System.Windows.Forms.ListBox lbxStyles;
        private System.Windows.Forms.Label lblStyles;
        private System.Windows.Forms.Button btnAddFormat;
        private System.Windows.Forms.Button btnAddStyle;
        private System.Windows.Forms.Button btnAddClass;
        private System.Windows.Forms.Button btnDeleteStyle;
        private System.Windows.Forms.Button btnDeleteFormat;
        private System.Windows.Forms.Button btnDeleteClass;
        private System.Windows.Forms.Button btnEditClass;
    }
}
