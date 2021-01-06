using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.IO;
using FormatImporter;
using Newtonsoft.Json;

namespace JSONFormatImporter
{
    public class JSONFormatImporterLogic : IFormatImporterLogic
    {
        public ImportedFormatModel getFormat(string filepath)
        {
            ImportedFormatModel importedFormat = new ImportedFormatModel();
            importedFormat.ImportedFormat = new Format();
            importedFormat.ImportedStyleClasses = new List<StyleClass>().AsEnumerable();
            importedFormat.ImportedStyles = new List<Style>().AsEnumerable();

            string jsonFormat;

            using (System.IO.StreamReader r = new System.IO.StreamReader(filepath))
            {
                jsonFormat = r.ReadToEnd();
            }
            try
            {
                dynamic formatData = JsonConvert.DeserializeObject(jsonFormat);
                RegisterFormatName(importedFormat, formatData["format"]);

                foreach (dynamic item in formatData["styles"])
                {
                    RegisterStyle(importedFormat, item);
                }


            }
            catch (Exception e)
            {
                throw new InvalidDataException(e.Message);
            }

            return importedFormat;
        }

        private void RegisterStyle(ImportedFormatModel importedFormat, dynamic style)
        {
            List<Style> stylesForClassesAndFormat = importedFormat.ImportedStyles.ToList();
            List<StyleClass> styleClassesForCurrentFormat = importedFormat.ImportedStyleClasses.ToList();

            StyleClass newClass = new StyleClass()
            {
                Name = style["StyleClass"],
                BasedOn = null
            };

            if(!styleClassesForCurrentFormat.Contains(newClass))
            {
                styleClassesForCurrentFormat.Add(newClass);
                importedFormat.ImportedStyleClasses = styleClassesForCurrentFormat.AsEnumerable();
            }

            Style newStyle = new Style()
            {
                Format = importedFormat.ImportedFormat,
                StyleClass = newClass,
                Key = style["Key"],
                Value = style["Value"]
            };

            stylesForClassesAndFormat.Add(newStyle);

            importedFormat.ImportedStyles = stylesForClassesAndFormat.AsEnumerable();
        }

        private void RegisterFormatName(ImportedFormatModel importedFormat, dynamic format)
        {
            importedFormat.ImportedFormat = new Format()
            {
                Name = format.Name
            };
        }

        public bool isCompatibleFile(string filepath)
        {
            return filepath.EndsWith(".json");
        }
    }
}
