namespace DesktopApplication
{
    partial class LoggerControl
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
            this.dtmStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblLastDate = new System.Windows.Forms.Label();
            this.dtmLastDate = new System.Windows.Forms.DateTimePicker();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lbxLogs = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // dtmStartDate
            // 
            this.dtmStartDate.Location = new System.Drawing.Point(19, 52);
            this.dtmStartDate.Name = "dtmStartDate";
            this.dtmStartDate.Size = new System.Drawing.Size(200, 20);
            this.dtmStartDate.TabIndex = 0;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(16, 36);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(72, 13);
            this.lblStartDate.TabIndex = 1;
            this.lblStartDate.Text = "Starting Date:";
            // 
            // lblLastDate
            // 
            this.lblLastDate.AutoSize = true;
            this.lblLastDate.Location = new System.Drawing.Point(238, 36);
            this.lblLastDate.Name = "lblLastDate";
            this.lblLastDate.Size = new System.Drawing.Size(56, 13);
            this.lblLastDate.TabIndex = 3;
            this.lblLastDate.Text = "Last Date:";
            // 
            // dtmLastDate
            // 
            this.dtmLastDate.Location = new System.Drawing.Point(241, 52);
            this.dtmLastDate.Name = "dtmLastDate";
            this.dtmLastDate.Size = new System.Drawing.Size(200, 20);
            this.dtmLastDate.TabIndex = 2;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(164, 78);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(130, 36);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lbxLogs
            // 
            this.lbxLogs.FormattingEnabled = true;
            this.lbxLogs.Location = new System.Drawing.Point(19, 131);
            this.lbxLogs.Name = "lbxLogs";
            this.lbxLogs.Size = new System.Drawing.Size(404, 225);
            this.lbxLogs.TabIndex = 5;
            // 
            // LoggerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbxLogs);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblLastDate);
            this.Controls.Add(this.dtmLastDate);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.dtmStartDate);
            this.Name = "LoggerControl";
            this.Size = new System.Drawing.Size(478, 388);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtmStartDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblLastDate;
        private System.Windows.Forms.DateTimePicker dtmLastDate;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ListBox lbxLogs;
    }
}
