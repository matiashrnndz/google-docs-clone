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
    public partial class ManageFormats : UserControl
    {
        IFormatManagementService formatManager;
        IStyleClassManagementService styleClassManager;
        IStyleManagementService styleManager;
        User administrator;

        public ManageFormats(IServiceHandler serviceHandler, User loggedUser)
        {
            InitializeComponent();

            formatManager = serviceHandler.GetFormatManagementService();
            styleClassManager = serviceHandler.GetStyleClassManagementService();
            styleManager = serviceHandler.GetStyleManagementService();

            administrator = loggedUser;

            LoadFormatsAndStyleClasses();
        }

        private void LoadFormatsAndStyleClasses()
        {
            IEnumerable<Format> existingFormats = formatManager.GetAll();
            lbxFormats.Items.Clear();
            foreach (Format format in existingFormats)
            {
                lbxFormats.Items.Add(format);
            }

            IEnumerable<StyleClass> existingStyleClasses = styleClassManager.GetAll();
            lbxStyleClasses.Items.Clear();
            foreach (StyleClass styleClass in existingStyleClasses)
            {
                lbxStyleClasses.Items.Add(styleClass);
            }
        }

        private void lbxFormats_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStyles();
        }

        private void LoadStyles()
        {
            lbxStyles.Items.Clear();
            if (lbxFormats.SelectedItem != null && lbxStyleClasses.SelectedItem != null)
            {
                Format selectedFormat = (Format)lbxFormats.SelectedItem;
                StyleClass selectedStyleClass = (StyleClass)lbxStyleClasses.SelectedItem;
                IEnumerable<Style> existingStyles = styleManager.GetAll(selectedFormat.Name, selectedStyleClass.Name);
                foreach (Style style in existingStyles)
                {
                    lbxStyles.Items.Add(style);
                }
            }
        }

        private void lbxStyleClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStyles();
        }

        private void btnAddFormat_Click(object sender, EventArgs e)
        {
            ManageFormat addFormatWindow = new DesktopApplication.ManageFormat(formatManager);
            addFormatWindow.Show();
        }

        private void btnDeleteFormat_Click(object sender, EventArgs e)
        {
            if (lbxFormats.SelectedItem != null)
            {
                try
                {
                    Format selectedFormat = (Format)lbxFormats.SelectedItem;
                    formatManager.Delete(selectedFormat.Name);
                    LoadFormatsAndStyleClasses();
                }
                catch (MissingFormatException exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void btnAddClass_Click(object sender, EventArgs e)
        {
            ManageStyleClass manageStyleClassWindow = new DesktopApplication.ManageStyleClass(styleClassManager);
            manageStyleClassWindow.Show();
        }

        private void btnEditClass_Click(object sender, EventArgs e)
        {
            if (lbxStyleClasses.SelectedItem != null)
            {
                EditStyleClass editStyleClassWindow = new DesktopApplication.EditStyleClass(styleClassManager, (StyleClass)lbxStyleClasses.SelectedItem);
                editStyleClassWindow.Show();
            }
            else
            {
                MessageBox.Show("Please select a Style Class.");
            }
        }

        private void btnDeleteClass_Click(object sender, EventArgs e)
        {
            if (lbxStyleClasses.SelectedItem != null)
            {
                try
                {
                    StyleClass selectedStyleClass = (StyleClass)lbxStyleClasses.SelectedItem;
                    styleClassManager.Delete(selectedStyleClass.Name);
                    LoadFormatsAndStyleClasses();
                }
                catch (MissingStyleClassException exception)
                {
                    MessageBox.Show(exception.Message);
                }
                catch (InvalidStyleClassException exception)
                {
                    MessageBox.Show(exception.Message);
                }
            } else
            {
                MessageBox.Show("Please select a style class first.");
            }
        }

        private void btnAddStyle_Click(object sender, EventArgs e)
        {
            if (lbxFormats.SelectedItem != null && lbxStyleClasses.SelectedItem != null)
            {
                Format selectedFormat = (Format)lbxFormats.SelectedItem;
                StyleClass selectedStyleClass = (StyleClass)lbxStyleClasses.SelectedItem;
                ManageStyle addStyleWindow = new DesktopApplication.ManageStyle(styleManager, selectedFormat.Name, selectedStyleClass.Name);
                addStyleWindow.Show();
            } else
            {
                MessageBox.Show("Select a Format and Style Class first.");
            }
        }

        private void btnDeleteStyle_Click(object sender, EventArgs e)
        {
            if (lbxStyles.SelectedItem != null)
            {
                try
                {
                    Style selectedStyle = (Style)lbxStyles.SelectedItem;
                    styleManager.Delete(selectedStyle.Id);
                    LoadFormatsAndStyleClasses();
                    LoadStyles();
                }
                catch (MissingStyleException exception)
                {
                    MessageBox.Show(exception.Message);
                }
            } else
            {
                MessageBox.Show("Please select a Style first.");
            }
        }
    }
}
