using Domain;
using System;
using Service;
using System.Collections.Generic;

namespace ServiceImp
{
    internal class StyleHTMLBuilder : IStyleBuilder
    {
        internal StyleHTML ConvertToHTML(IEnumerable<Style> styles)
        {
            StyleHTML convertedStyles = new StyleHTML();

            bool strong = false;
            bool em = false;
            bool pStyle = false;

            string leftPStyleCode = "<p style='";
            string rightPStyleCode = "</p>";

            foreach (Style style in styles)
            {
                switch (style.Key)
                {
                    case "Font":
                        string[] fontData = style.Value.Split(',');
                        pStyle = true;
                        leftPStyleCode += "font-family:" + ToFont(fontData[0])
                            + "; font-size:" + fontData[1] + "pt;";
                        break;
                    case "Underlined":
                        leftPStyleCode += "text-decoration: underline;";
                        pStyle = true;
                        break;
                    case "Bold":
                        strong = true;
                        break;
                    case "Italics":
                        em = true;
                        break;
                    case "Color":
                        leftPStyleCode += "color:" + style.Value.ToLower() + ";";
                        pStyle = true;
                        break;
                    case "Align":
                        leftPStyleCode += "text-align:" + style.Value.ToLower() + ";";
                        pStyle = true;
                        break;
                    case "Border":
                        leftPStyleCode += "border:" + style.Value.ToLower() + ";";
                        pStyle = true;
                        break;
                    default:
                        break;
                }
                
            }
            if (pStyle) {
                convertedStyles.LeftHTMLCode = leftPStyleCode + "'>";
                convertedStyles.RightHTMLCode = "</p>";
             }
            if (strong)
            {
                convertedStyles.LeftHTMLCode = "<strong>" + convertedStyles.LeftHTMLCode;
                convertedStyles.RightHTMLCode += "</strong>";
            }
            if (em)
            {
                convertedStyles.LeftHTMLCode = "<em>" + convertedStyles.LeftHTMLCode;
                convertedStyles.RightHTMLCode += "</em>";
            }
            return convertedStyles;
        }

        private string ToFont(string v)
        {
            if (v.ToLower() == "times new roman")
            {
                return "times";
            }
            else return v;
        }
    }
}