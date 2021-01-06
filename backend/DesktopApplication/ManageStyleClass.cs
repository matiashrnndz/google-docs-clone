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
    public partial class ManageStyleClass : Form
    {
        IStyleClassManagementService styleClassManager;

        public ManageStyleClass(IStyleClassManagementService aStyleClassManager)
        {
            InitializeComponent();
            styleClassManager = aStyleClassManager;
            LoadStyleClasses();
        }

        private void LoadStyleClasses()
        {
            cbxStyleClassBasedOn.Items.Clear();
            IEnumerable<StyleClass> existingClasses = styleClassManager.GetAll();
            cbxStyleClassBasedOn.Items.Add("---None---");
            foreach (StyleClass styleClass in existingClasses)
            {
                cbxStyleClassBasedOn.Items.Add(styleClass.Name);
            }
        }

        private void btnSubmitStyleClass_Click(object sender, EventArgs e)
        {
            if(tbxStyleClassName.Text != "")
            {
                try
                {
                    StyleClass newClass = new StyleClass()
                    {
                        Name = tbxStyleClassName.Text
                    };
                    if(((string) cbxStyleClassBasedOn.SelectedItem) != "---None---")
                    {
                        StyleClass parentClass = styleClassManager.GetByName((string)cbxStyleClassBasedOn.SelectedItem);
                        newClass.BasedOn = parentClass;
                    }
                    styleClassManager.Add(newClass);
                    MessageBox.Show("The new Style Class was successfully added.");
                    this.Close();
                } catch (ExistingStyleClassException exception)
                {
                    MessageBox.Show(exception.Message);
                } 
            }
        }
    }
}
