using Domain;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImp
{
    public class GenericStyleBuilder : IStyleBuilder
    {
        public bool CanBuildStyle(Style style)
        {
            string styleKey = style.Key;
            bool ok = false;

            switch(styleKey)
            {
                case "Bold":
                    ok = true;
                    break;
                case "Italics":
                    ok = true;
                    break;
                case "Underlined":
                    ok = true;
                    break;
                case "Font":
                    ok = HasValidFont(style);
                    break;
                case "Color":
                    ok = HasValidColor(style);
                    break;
                case "Align":
                    ok = HasValidTextAlignment(style);
                    break;
                case "Border":
                    ok = HasValidBorder(style);
                    break;
                default:
                    break;
            }

            return ok;
        }

        private bool HasValidBorder(Style style)
        {
            List<string> validBorders = new List<string>
            {
                "solid"
            };

            return validBorders.Contains(style.Value.ToLower());
        }

        private bool HasValidTextAlignment(Style style)
        {
            List<string> validAlignments = new List<string>
            {
                "left",
                "center",
                "right",
                "justify"
            };

            return validAlignments.Contains(style.Value.ToLower());
        }

        private bool HasValidColor(Style style)
        {
            List<string> validColors = new List<string>
            {
                "red",
                "blue",
                "black"
            };

            return validColors.Contains(style.Value.ToLower());
        }

        private bool HasValidFont(Style style)
        {
            bool ok = true;
            string[] values = style.Value.Split(',');
            if (values.Count() != 2)
            {
                ok = false;
                return ok;
            }
            List<string> validFonts = new List<string>
            { "times new roman",
              "arial",
              "courier",
              "verdana"
            };

            ok = validFonts.Contains(values.ElementAt(0).ToLower()) && HasValidFontSize(values.ElementAt(1));

            return ok;
        }

        private bool HasValidFontSize(string value)
        {
            return Int32.Parse(value) > 1 && Int32.Parse(value) <= 99;
        }
    }
}
