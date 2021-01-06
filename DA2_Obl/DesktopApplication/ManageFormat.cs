using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Service;
using Domain;
using Exception;

namespace DesktopApplication
{
    public partial class ManageFormat : Form
    {
        IFormatManagementService formatManager;

        public ManageFormat(IFormatManagementService aFormatManager)
        {
            InitializeComponent();
            formatManager = aFormatManager;
        }

        private void btnAddFormat_Click(object sender, EventArgs e)
        {
            if (tbxFormatName.Text != "")
            {
                Format newFormat = new Format()
                {
                    Name = tbxFormatName.Text
                };
                try
                {
                    formatManager.Add(newFormat);
                    MessageBox.Show("The Format was added successfully.");
                    this.Close();
                }
                catch (ExistingFormatException exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }
    }
}
