namespace DesktopApplication
{
    partial class MainMenu
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

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mitSession = new System.Windows.Forms.ToolStripMenuItem();
            this.mitLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.mitLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.mitMaintenance = new System.Windows.Forms.ToolStripMenuItem();
            this.mitManageFormatting = new System.Windows.Forms.ToolStripMenuItem();
            this.mitLogging = new System.Windows.Forms.ToolStripMenuItem();
            this.mitViewLogs = new System.Windows.Forms.ToolStripMenuItem();
            this.mitImport = new System.Windows.Forms.ToolStripMenuItem();
            this.mitImportFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mitSession,
            this.mitMaintenance,
            this.mitLogging,
            this.mitImport});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(772, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mitSession
            // 
            this.mitSession.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mitLogin,
            this.mitLogout});
            this.mitSession.Name = "mitSession";
            this.mitSession.Size = new System.Drawing.Size(58, 20);
            this.mitSession.Text = "Session";
            // 
            // mitLogin
            // 
            this.mitLogin.Name = "mitLogin";
            this.mitLogin.Size = new System.Drawing.Size(112, 22);
            this.mitLogin.Text = "Login";
            // 
            // mitLogout
            // 
            this.mitLogout.Name = "mitLogout";
            this.mitLogout.Size = new System.Drawing.Size(112, 22);
            this.mitLogout.Text = "Logout";
            // 
            // mitMaintenance
            // 
            this.mitMaintenance.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mitManageFormatting});
            this.mitMaintenance.Name = "mitMaintenance";
            this.mitMaintenance.Size = new System.Drawing.Size(88, 20);
            this.mitMaintenance.Text = "Maintenance";
            // 
            // mitManageFormatting
            // 
            this.mitManageFormatting.Name = "mitManageFormatting";
            this.mitManageFormatting.Size = new System.Drawing.Size(188, 22);
            this.mitManageFormatting.Text = "Manage Formatting...";
            this.mitManageFormatting.Click += new System.EventHandler(this.mitManageFormatting_Click);
            // 
            // mitLogging
            // 
            this.mitLogging.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mitViewLogs});
            this.mitLogging.Name = "mitLogging";
            this.mitLogging.Size = new System.Drawing.Size(63, 20);
            this.mitLogging.Text = "Logging";
            // 
            // mitViewLogs
            // 
            this.mitViewLogs.Name = "mitViewLogs";
            this.mitViewLogs.Size = new System.Drawing.Size(127, 22);
            this.mitViewLogs.Text = "View Logs";
            this.mitViewLogs.Click += new System.EventHandler(this.mitViewLogs_Click);
            // 
            // mitImport
            // 
            this.mitImport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mitImportFormat});
            this.mitImport.Name = "mitImport";
            this.mitImport.Size = new System.Drawing.Size(55, 20);
            this.mitImport.Text = "Import";
            // 
            // mitImportFormat
            // 
            this.mitImportFormat.Name = "mitImportFormat";
            this.mitImportFormat.Size = new System.Drawing.Size(112, 22);
            this.mitImportFormat.Text = "Format";
            this.mitImportFormat.Click += new System.EventHandler(this.mitImportFormat_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(12, 27);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(748, 440);
            this.pnlMain.TabIndex = 1;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 479);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainMenu";
            this.Text = "Main Menu";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mitSession;
        private System.Windows.Forms.ToolStripMenuItem mitLogin;
        private System.Windows.Forms.ToolStripMenuItem mitLogout;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.ToolStripMenuItem mitMaintenance;
        private System.Windows.Forms.ToolStripMenuItem mitLogging;
        private System.Windows.Forms.ToolStripMenuItem mitManageFormatting;
        private System.Windows.Forms.ToolStripMenuItem mitViewLogs;
        private System.Windows.Forms.ToolStripMenuItem mitImport;
        private System.Windows.Forms.ToolStripMenuItem mitImportFormat;
    }
}

