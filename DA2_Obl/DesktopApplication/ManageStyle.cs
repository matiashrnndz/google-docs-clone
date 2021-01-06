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
    public partial class ManageStyle : Form
    {
        IStyleManagementService styleManager;
        string selectedFormatName;
        string selectedClassName;

        public ManageStyle(IStyleManagementService aStyleManager, string formatName, string styleClassName)
        {
            InitializeComponent();
            styleManager = aStyleManager;
            selectedFormatName = formatName;
            selectedClassName = styleClassName;
        }

        private void btnSubmitStyle_Click(object sender, EventArgs e)
        {
            Style newStyle = new Style()
            {
                Key = tbxKey.Text,
                Value = tbxValue.Text
            };
            try
            {
                styleManager.Add(selectedFormatName, selectedClassName, newStyle);
                MessageBox.Show("The style was added successfully");
                this.Close();
            } catch (InvalidStyleException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
