namespace DesktopApplication
{
    partial class ImporterControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbxImporterPath = new System.Windows.Forms.TextBox();
            this.btnSelectImporter = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.tbxFilePath = new System.Windows.Forms.TextBox();
            this.btnStartImport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Importer:";
            // 
            // tbxImporterPath
            // 
            this.tbxImporterPath.Location = new System.Drawing.Point(31, 45);
            this.tbxImporterPath.Name = "tbxImporterPath";
            this.tbxImporterPath.Size = new System.Drawing.Size(331, 20);
            this.tbxImporterPath.TabIndex = 1;
            // 
            // btnSelectImporter
            // 
            this.btnSelectImporter.Location = new System.Drawing.Point(368, 41);
            this.btnSelectImporter.Name = "btnSelectImporter";
            this.btnSelectImporter.Size = new System.Drawing.Size(118, 27);
            this.btnSelectImporter.TabIndex = 2;
            this.btnSelectImporter.Text = "Select Importer...";
            this.btnSelectImporter.UseVisualStyleBackColor = true;
            this.btnSelectImporter.Click += new System.EventHandler(this.btnSelectImporter_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "File to Import:";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(368, 92);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(118, 27);
            this.btnSelectFile.TabIndex = 5;
            this.btnSelectFile.Text = "Browse Files...";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // tbxFilePath
            // 
            this.tbxFilePath.Location = new System.Drawing.Point(31, 96);
            this.tbxFilePath.Name = "tbxFilePath";
            this.tbxFilePath.Size = new System.Drawing.Size(331, 20);
            this.tbxFilePath.TabIndex = 4;
            // 
            // btnStartImport
            // 
            this.btnStartImport.Location = new System.Drawing.Point(131, 147);
            this.btnStartImport.Name = "btnStartImport";
            this.btnStartImport.Size = new System.Drawing.Size(276, 54);
            this.btnStartImport.TabIndex = 6;
            this.btnStartImport.Text = "Import";
            this.btnStartImport.UseVisualStyleBackColor = true;
            this.btnStartImport.Click += new System.EventHandler(this.btnStartImport_Click);
            // 
            // ImporterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnStartImport);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.tbxFilePath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSelectImporter);
            this.Controls.Add(this.tbxImporterPath);
            this.Controls.Add(this.label1);
            this.Name = "ImporterControl";
            this.Size = new System.Drawing.Size(553, 233);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxImporterPath;
        private System.Windows.Forms.Button btnSelectImporter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.TextBox tbxFilePath;
        private System.Windows.Forms.Button btnStartImport;
    }
}
