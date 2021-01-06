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
using FormatImporter;
using System.Configuration;
using System.Reflection;

namespace DesktopApplication
{
    public partial class ImporterControl : UserControl
    {
        IFormatManagementService formatService;
        IStyleClassManagementService styleClassService;
        IStyleManagementService styleService;
        ILoggingService loggerService;
        User importingUser;

        public ImporterControl(IServiceHandler serviceHandler, User loggedUser)
        {
            InitializeComponent();
            formatService = serviceHandler.GetFormatManagementService();
            styleClassService = serviceHandler.GetStyleClassManagementService();
            styleService = serviceHandler.GetStyleManagementService();
            loggerService = serviceHandler.GetLoggingService();
            importingUser = loggedUser;
        }

        private void btnSelectImporter_Click(object sender, EventArgs e)
        {
            OpenFileDialog importerSelector = new OpenFileDialog();

            importerSelector.Filter = "Assemblies (.dll)|*.dll|All Files (*.*)|*.*";
            importerSelector.FilterIndex = 1;

            importerSelector.Multiselect = false;

            bool? userClickedOK = (DialogResult.OK == importerSelector.ShowDialog());

            if (userClickedOK == true)
            {
                tbxImporterPath.Text = importerSelector.FileName;
            }
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileSelector = new OpenFileDialog();

            fileSelector.Filter = "All Files (*.*)|*.*";
            fileSelector.FilterIndex = 1;

            fileSelector.Multiselect = false;

            bool? userClickedOK = (DialogResult.OK == fileSelector.ShowDialog());

            if (userClickedOK == true)
            {
                tbxFilePath.Text = fileSelector.FileName;
            }
        }

        private void btnStartImport_Click(object sender, EventArgs e)
        {
            try
            {
                IFormatImporterLogic loadedImporter = LoadFormatImporter();
                if (loadedImporter.isCompatibleFile(tbxFilePath.Text))
                {
                    ImportedFormatModel importedFormat = loadedImporter.getFormat(tbxFilePath.Text);

                    AddImportedData(importedFormat);
                    loggerService.AddLogForFormatImport(importingUser.UserName);

                    MessageBox.Show("Importing Successful!");

                }
                else
                {
                    MessageBox.Show("An invalid file was selected");
                }
            }
            catch (NullReferenceException exception)
            {
                MessageBox.Show("An invalid importer was selected. " + exception.Message);
            }
        }

        private void AddImportedData(ImportedFormatModel importedFormat)
        {
            try
            {
                formatService.Add(importedFormat.ImportedFormat);

                foreach (StyleClass styleClass in importedFormat.ImportedStyleClasses)
                {
                    try
                    {
                        styleClassService.Add(styleClass);
                    }
                    catch (ExistingStyleClassException exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }

                foreach (Style style in importedFormat.ImportedStyles)
                {
                    try
                    {
                        styleService.Add(style.Format.Name, style.StyleClass.Name, style);
                    }
                    catch (InvalidStyleException exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }

            }
            catch (ExistingFormatException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private IFormatImporterLogic LoadFormatImporter()
        {
            Assembly assembly = Assembly.LoadFile(tbxImporterPath.Text);

            foreach (var item in assembly.GetTypes())
            {
                if (typeof(IFormatImporterLogic).IsAssignableFrom(item))
                {
                    return (IFormatImporterLogic)Activator.CreateInstance(item);
                }
            }

            throw new NullReferenceException();
        }
    }
}
