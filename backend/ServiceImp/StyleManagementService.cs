using Domain;
using Exception;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceImp
{
    public class StyleManagementService : IStyleManagementService
    {
        internal IStyleRepository StyleRepository { get; set; }
        internal IFormatRepository FormatRepository { get; set; }
        internal IStyleClassRepository StyleClassRepository { get; set; }
        internal GenericStyleBuilder StyleBuilder { get; set; }

        public IEnumerable<Style> GetAll(string formatName, string styleClassName)
        {
            return StyleRepository.GetStyles(styleClassName, formatName);
        }

        public Style Add(string formatName, string styleClassName, Style style)
        {
            if (StyleBuilder.CanBuildStyle(style))
            {
                style.Id = Guid.NewGuid();

                if (ExistsKeyInStyleClassAndFormat(formatName, styleClassName, style))
                {
                    DeleteStylesSharingKey(formatName, styleClassName, style);
                    StyleRepository.Add(style);
                }
                else
                {
                    style.Format = FormatRepository.GetByName(formatName);
                    style.StyleClass = StyleClassRepository.GetByName(styleClassName);

                    StyleRepository.Add(style);
                }

                return style;
            }
            else
            {
                throw new InvalidStyleException("This style has an invalid format.");
            }
        }

        private void DeleteStylesSharingKey(string formatName, string styleClassName, Style style)
        {
            List<Style> allStylesInFormat = GetAll(formatName, styleClassName).ToList();
            foreach (Style styleInFormat in allStylesInFormat)
            {
                if (styleInFormat.Key.Equals(style.Key))
                {
                    StyleRepository.Delete(styleInFormat);
                }
            }
        }

        private bool ExistsKeyInStyleClassAndFormat(string formatName, string styleClassName, Style style)
        {
            bool ok = false;
            List<Style> allStylesInFormat = GetAll(formatName, styleClassName).ToList();
            foreach (Style styleInFormat in allStylesInFormat)
            {
                if (styleInFormat.Key.Equals(style.Key))
                {
                    ok = true;
                }
            }
            return ok;
        }


        public void Delete(Guid styleId)
        {
            if (StyleRepository.ExistsById(styleId))
            {
                Style styleToDelete = StyleRepository.GetById(styleId);
                StyleRepository.Delete(styleToDelete);
            }
            else
            {
                throw new MissingStyleException("This style is not in the database");
            }
        }
    }
}
