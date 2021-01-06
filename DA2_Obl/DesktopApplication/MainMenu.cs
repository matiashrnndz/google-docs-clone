using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain;
using Service;

namespace DesktopApplication
{
    public partial class MainMenu : Form
    {
        private static MainMenu instance;

        User loggedUser;
        IServiceHandler serviceHandler;

        private MainMenu(IServiceHandler aServiceHandler)
        {
            InitializeComponent();
            loggedUser = new User();
            serviceHandler = aServiceHandler;

            HideAllMenus();
            DisplayLogin();
        }

        public static MainMenu CreateInstance(IServiceHandler aServiceHandler)
        {
            instance = new MainMenu(aServiceHandler);

            return instance;
        }

        public static MainMenu GetInstance()
        {
            return instance;
        }

        private void DisplayLogin()
        {
            pnlMain.Controls.Clear();
            UserControl pnlLogin = new Login(serviceHandler, loggedUser);
            pnlMain.Controls.Add(pnlLogin);
        }

        private void HideAllMenus()
        {
            mitMaintenance.Visible = false;
            mitLogging.Visible = false;
            mitImport.Visible = false;
            mitLogout.Visible = false;
        }

        public void DisplayMenus()
        {
            mitMaintenance.Visible = true;
            mitLogging.Visible = true;
            pnlMain.Controls.Clear();
            mitLogin.Visible = false;
            mitLogout.Visible = true;
            mitImport.Visible = true;
        }

        private void mitManageFormatting_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            UserControl pnlManageFormats = new ManageFormats(serviceHandler, loggedUser);
            pnlMain.Controls.Add(pnlManageFormats);
        }

        private void mitViewLogs_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            UserControl pnlLoggerControl = new LoggerControl(serviceHandler, loggedUser);
            pnlMain.Controls.Add(pnlLoggerControl);
        }

        private void mitImportFormat_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            UserControl pnlImporterControl = new ImporterControl(serviceHandler, loggedUser);
            pnlMain.Controls.Add(pnlImporterControl);
        }
    }
}
