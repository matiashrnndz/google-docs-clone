using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain;
using Service;
using Exception;

namespace DesktopApplication
{


    public partial class Login : UserControl
    {
        IUserManagementService userManager;
        ILoggingService loggerService;
        User userToLogin;

        public Login(IServiceHandler serviceHandler, User maybeAUser)
        {
            InitializeComponent();

            userToLogin = maybeAUser;
            userManager = serviceHandler.GetUserManagementService();
            loggerService = serviceHandler.GetLoggingService();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            User userInDatabase;
            try {
                userInDatabase = userManager.GetByEmail(tbxEmail.Text);

                if(tbxPassword.Text == userInDatabase.Password)
                {
                    userToLogin.Email = userInDatabase.Email;
                    userToLogin.Name = userInDatabase.Name;
                    userToLogin.LastName = userInDatabase.LastName;
                    userToLogin.UserName = userInDatabase.UserName;
                    userToLogin.Administrator = userInDatabase.Administrator;

                    if(userToLogin.Administrator == false)
                    {
                        MessageBox.Show("Only Admins can use this application.");
                        return;
                    }

                    loggerService.AddLogForLogin(userToLogin.UserName);
                    MainMenu.GetInstance().DisplayMenus();
                } else
                {
                    MessageBox.Show("Password does not match.");
                }
            } catch (MissingUserException userException)
            {
                MessageBox.Show(userException.Message);
            }
        }
    }
}
