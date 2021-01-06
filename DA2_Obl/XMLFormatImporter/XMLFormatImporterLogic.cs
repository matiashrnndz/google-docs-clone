using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using Domain;
using FormatImporter;

namespace XMLFormatImporter
{
    public class XMLFormatImporterLogic : IFormatImporterLogic
    {
        public XMLImportedStyleBuilder XMLStyleBuilder { get; set; }

        public ImportedFormatModel getFormat(string filepath)
        {
            ImportedFormatModel importedFormat = new ImportedFormatModel();
            importedFormat.ImportedStyleClasses = new List<StyleClass>().AsEnumerable();
            importedFormat.ImportedStyles = new List<Style>().AsEnumerable();

            XmlDocument documentedFormat = new XmlDocument();
            XMLStyleBuilder = new XMLImportedStyleBuilder();

            try
            {
                documentedFormat.Load(filepath);
            } catch (Exception e)
            {
                throw new InvalidDataException(e.Message);
            }

            XmlNode formatInXml;

            try {
                 formatInXml = documentedFormat.SelectSingleNode("/Formato");
            }
            catch(Exception e)
            {
                throw new InvalidDataException("A format node was not found.");
            }

            string formatName;

            try {
                formatName = formatInXml.Attributes.GetNamedItem("nombre").Value;
            } catch (Exception e)
            {
                throw new InvalidDataException("This format has no name.");
            }

            importedFormat.ImportedFormat = new Format()
            {
                Name = formatName
            };

            XmlNodeList formatStyleClasses = formatInXml.ChildNodes;
            foreach(XmlNode styleClass in formatStyleClasses)
            {
                addStyleToImportedFormat(styleClass, importedFormat);
            }

            return importedFormat;
        }

        private void addStyleToImportedFormat(XmlNode styleClassInformation, ImportedFormatModel importedFormat)
        {
            List<StyleClass> styleClassesForCurrentFormat = importedFormat.ImportedStyleClasses.ToList();
            List<Style> stylesForClassesAndFormat = importedFormat.ImportedStyles.ToList();

            StyleClass styleClassToAdd = new StyleClass
            {
                Name = styleClassInformation.Name,
                BasedOn = null
            };

            XmlNode stylesInClass;
            try {
               stylesInClass  = styleClassInformation.SelectSingleNode("descendant::Estilos");
            } catch (XPathException e)
            {
                throw new InvalidDataException("The format for this style class is invalid.");
            }

            if (stylesInClass.InnerText != null  || stylesInClass.InnerText != "...")
            {
                XmlNodeList selectedClassStyles = stylesInClass.ChildNodes;
                IEnumerable<Style> newStyles;
                newStyles = XMLStyleBuilder.buildStylesFromXml(selectedClassStyles);
                foreach (Style newStyle in newStyles)
                {
                    newStyle.StyleClass = styleClassToAdd;
                    newStyle.Format = importedFormat.ImportedFormat;
                    stylesForClassesAndFormat.Add(newStyle);
                }
            }

            styleClassesForCurrentFormat.Add(styleClassToAdd);
            importedFormat.ImportedStyleClasses = styleClassesForCurrentFormat.AsEnumerable();
            importedFormat.ImportedStyles = stylesForClassesAndFormat.AsEnumerable();
         }

        public bool isCompatibleFile(string filepath)
        {
            return filepath.EndsWith(".xml");
        }
    }
}
