using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Service;
using System.Xml;

namespace XMLFormatImporter
{
    public class XMLImportedStyleBuilder : IStyleBuilder
    {
        public IEnumerable<Style> buildStylesFromXml(XmlNodeList styles)
        {
            List<Style> importedStyles = new List<Style>();

            foreach (XmlNode selectedStyle in styles)
            {
                Style styleToAdd = new Style();
                switch (selectedStyle.Name)
                {
                    case "TipoLetra":
                        styleToAdd.Key = "Font";
                        styleToAdd.Value = selectedStyle.InnerText + ",12";
                        foreach (XmlNode possibleFontSize in styles)
                        {
                            if (possibleFontSize.Name == "TamanioLetra")
                            {
                                styleToAdd.Value = selectedStyle.InnerText + "," + possibleFontSize.InnerText;
                            }
                        }
                        importedStyles.Add(styleToAdd);
                        break;
                    case "Negrita":
                        styleToAdd.Key = "Bold";
                        importedStyles.Add(styleToAdd);
                        break;
                    case "Subrayado":
                        styleToAdd.Key = "Underlined";
                        importedStyles.Add(styleToAdd);
                        break;
                    case "Cursiva":
                        styleToAdd.Key = "Italics";
                        importedStyles.Add(styleToAdd);
                        break;
                    case "Alineacion":
                        styleToAdd.Key = "Align";
                        styleToAdd.Value = translateAlignmentXMLtoStyleNotation(selectedStyle.InnerText);
                        importedStyles.Add(styleToAdd);
                        break;
                    case "Color":
                        styleToAdd.Key = "Color";
                        styleToAdd.Value = translateRGBtoStyleNotation(selectedStyle.InnerText);
                        importedStyles.Add(styleToAdd);
                        break;
                    case "Borde":
                        styleToAdd.Key = "Border";
                        styleToAdd.Value = translateBorderXMLtoStyleNotation(selectedStyle.InnerText);
                        importedStyles.Add(styleToAdd);
                        break;
                    default:
                        break;
                }
            }

            return importedStyles.AsEnumerable();
        }

        private string translateBorderXMLtoStyleNotation(string innerText)
        {
            switch(innerText.ToLower())
            {
                case "sólido":
                    return "solid";
                default:
                    return "solid";
            }
        }

        private string translateRGBtoStyleNotation(string innerText)
        {
            switch (innerText.ToLower())
            {
                case "255,0,0":
                    return "red";
                case "0,0,255":
                    return "blue";
                default:
                    return "black";
            }
        }

        private string translateAlignmentXMLtoStyleNotation(string innerText)
        {
            switch (innerText.ToLower())
            {
                case "derecha":
                    return "right";
                case "izquierda":
                    return "left";
                case "centrada":
                    return "center";
                case "justificada":
                    return "justify";
                default:
                    return "left";
            }
        }
    }
}
