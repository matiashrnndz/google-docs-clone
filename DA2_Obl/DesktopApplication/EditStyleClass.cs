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
    public partial class EditStyleClass : Form
    {
        IStyleClassManagementService styleClassManager;
        StyleClass selectedClass;

        public EditStyleClass(IStyleClassManagementService aStyleClassManager, StyleClass aStyleClass)
        {
            InitializeComponent();
            styleClassManager = aStyleClassManager;
            selectedClass = aStyleClass;
            LoadStyleClasses();
            LoadName();
        }

        private void LoadName()
        {
            tbxStyleClassName.Text = selectedClass.Name;
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
            if(((string) cbxStyleClassBasedOn.SelectedItem) == "---None---")
            {
                selectedClass.BasedOn = null;
            } else
            {
                selectedClass.BasedOn = styleClassManager.GetByName((string)cbxStyleClassBasedOn.SelectedItem);
            }
            try
            {
                styleClassManager.Update(selectedClass.Name, selectedClass);
                MessageBox.Show("The Style Class was successfully modified.");
                this.Close();
            } catch (RedundantStyleClassException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
