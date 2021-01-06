using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Service;
using Domain;
using Exception;

namespace DesktopApplication
{
    public partial class LoggerControl : UserControl
    {
        ILoggingService loggerService;
        User loggedUser;

        public LoggerControl(IServiceHandler serviceHandler, User aUser)
        {
            InitializeComponent();
            loggerService = serviceHandler.GetLoggingService();
            loggedUser = aUser;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            lbxLogs.Items.Clear();
            IEnumerable <LoggedEntry> existingLogs = loggerService.getLogs(dtmStartDate.Value, dtmLastDate.Value);
            foreach (LoggedEntry log in existingLogs)
            {
                lbxLogs.Items.Add(log);
            }
        }
    }
}
