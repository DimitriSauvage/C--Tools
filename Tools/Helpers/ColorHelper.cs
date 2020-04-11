using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Helpers
{
    public static class ColorHelper
    {
        #region Private attributes
        private static char[] hexDigits = {
         '0', '1', '2', '3', '4', '5', '6', '7',
         '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};
        #endregion

        #region Structs 

        internal struct RGB
        {
            internal double R;
            internal double G;
            internal double B;
        }
        internal struct HSB
        {
            internal double H;
            internal double S;
            internal double B;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Retourne la couleur selon sa valeur héxadécimale
        /// </summary>
        /// <param name="hexaCode">Code héxadécimale de la couleur</param>
        /// <returns></returns>
        public static Color GetColorFromHexa(string hexaCode)
        {
            //Récupération de la couleur en format argb
            ColorHelper.GetArgbFromHexa(hexaCode, out var a, out var r, out var g, out var b);

            return Color.FromArgb(a, r, g, b);
        }

        /// <summary>
        /// Récupère les valeurs argb depuis un code héxadécimale
        /// </summary>
        /// <param name="hexaCode">Code héxadécimal</param>
        /// <param name="a">A</param>
        /// <param name="r">R</param>
        /// <param name="g">G</param>
        /// <param name="b">B</param>
        public static void GetArgbFromHexa(string hexaCode, out byte a, out byte r, out byte g, out byte b)
        {
            string tempHexa = ToRgbaHexa(hexaCode);
            if (tempHexa == null || !uint.TryParse(tempHexa, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var packedValue))
            {
                throw new ArgumentException("Hexadecimal string is not in the correct format.", nameof(hexaCode));
            }

            a = (byte)(packedValue >> 0);
            r = (byte)(packedValue >> 24);
            g = (byte)(packedValue >> 16);
            b = (byte)(packedValue >> 8);
        }

        /// <summary>
        /// Retourne la couleur donnée plus fonçée
        /// </summary>
        /// <param name="color">couleur d'origine</param>
        /// <returns>couleur plus foncée</returns>
        public static Color Dark(this Color color)
        {
            return ChangeColorBrightness(color, -0.5f);
        }

        /// <summary>
        /// Retourne la couleur en plus clair
        /// </summary>
        /// <param name="color">Couleur d'origine</param>
        /// <returns>couleur éclaircie</returns>
        public static Color Light(this Color color)
        {
            return ChangeColorBrightness(color, 0.5f);
        }

        /// <summary>
        /// Obtient la luminosité
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int GetLuminance(this Color source)
        {
            return (int)(0.2126 * source.R + 0.7152 * source.G + 0.0722 * source.B);
        }

        /// <summary>
        /// Obtient si selon une couleur d'entrée, la couleur noire ou blanche sera la plus lisible
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Color GetBlackOrWhite(this Color source)
        {
            return GetLuminance(source) > 128 ? Color.Black : Color.White;
        }

        public static Color GetContrast(this Color source, bool preserveOpacity)
        {
            Color inputColor = source;
            //if RGB values are close to each other by a diff less than 10%, then if RGB values are lighter side, decrease the blue by 50% (eventually it will increase in conversion below), if RBB values are on darker side, decrease yellow by about 50% (it will increase in conversion)
            byte avgColorValue = (byte)((source.R + source.G + source.B) / 3);
            int diff_r = Math.Abs(source.R - avgColorValue);
            int diff_g = Math.Abs(source.G - avgColorValue);
            int diff_b = Math.Abs(source.B - avgColorValue);
            if (diff_r < 20 && diff_g < 20 && diff_b < 20) //The color is a shade of gray
            {
                if (avgColorValue < 123) //color is dark
                    inputColor = Color.FromArgb(50, 230, 220);
                else
                    inputColor = Color.FromArgb(50, 255, 255);
            }

            byte sourceAlphaValue = source.A;
            if (!preserveOpacity)
            {
                sourceAlphaValue = Math.Max(source.A, (byte)127); //We don't want contrast color to be more than 50% transparent ever.
            }
            RGB rgb = new RGB { R = inputColor.R, G = inputColor.G, B = inputColor.B };
            HSB hsb = ConvertToHSB(rgb);

            hsb.H = hsb.H < 180 ? hsb.H + 180 : hsb.H - 180;
            rgb = ConvertToRGB(hsb);

            return Color.FromArgb(sourceAlphaValue, (byte)rgb.R, (byte)rgb.G, (byte)rgb.B);// new Color { A = sourceAlphaValue, R = rgb.R, G = (byte)rgb.G, B = (byte)rgb.B };
        }

        /// <summary>
        /// Transforme une couleur en rgba sous forme de chaine de caractère
        /// </summary>
        /// <param name="color">Couleur à transformer</param>
        /// <returns>Renvoi une couleur sous forme de chaine de caractère en forme rgba</returns>
        public static string ToRGBA(this Color color)
        {
            return String.Format("rgba({0},{1},{2},{3})", color.R, color.G, color.B, color.A);
        }

        /// <summary>
        /// Convertit une couleur en format hexadécimal
        /// </summary>
        /// <param name="color">Couleur à transformer</param>
        /// <example>#FFFFFF</example>
        /// <returns>Code héxadécimal</returns>
        public static string ToHexa(this Color color)
        {
            byte[] bytes = new byte[3];
            bytes[0] = color.R;
            bytes[1] = color.G;
            bytes[2] = color.B;
            char[] chars = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                int b = bytes[i];
                chars[i * 2] = hexDigits[b >> 4];
                chars[i * 2 + 1] = hexDigits[b & 0xF];
            }
            return string.Concat("#", new string(chars));
        }
        #endregion

        #region Private methods

        internal static RGB ConvertToRGB(HSB hsb)
        {
            // By: <a href="http://blogs.msdn.com/b/codefx/archive/2012/02/09/create-a-color-picker-for-windows-phone.aspx" title="MSDN" target="_blank">Yi-Lun Luo</a>
            double chroma = hsb.S * hsb.B;
            double hue2 = hsb.H / 60;
            double x = chroma * (1 - Math.Abs(hue2 % 2 - 1));
            double r1 = 0d;
            double g1 = 0d;
            double b1 = 0d;
            if (hue2 >= 0 && hue2 < 1)
            {
                r1 = chroma;
                g1 = x;
            }
            else if (hue2 >= 1 && hue2 < 2)
            {
                r1 = x;
                g1 = chroma;
            }
            else if (hue2 >= 2 && hue2 < 3)
            {
                g1 = chroma;
                b1 = x;
            }
            else if (hue2 >= 3 && hue2 < 4)
            {
                g1 = x;
                b1 = chroma;
            }
            else if (hue2 >= 4 && hue2 < 5)
            {
                r1 = x;
                b1 = chroma;
            }
            else if (hue2 >= 5 && hue2 <= 6)
            {
                r1 = chroma;
                b1 = x;
            }
            double m = hsb.B - chroma;
            return new RGB()
            {
                R = r1 + m,
                G = g1 + m,
                B = b1 + m
            };
        }
        internal static HSB ConvertToHSB(RGB rgb)
        {
            // By: <a href="http://blogs.msdn.com/b/codefx/archive/2012/02/09/create-a-color-picker-for-windows-phone.aspx" title="MSDN" target="_blank">Yi-Lun Luo</a>
            double r = rgb.R;
            double g = rgb.G;
            double b = rgb.B;

            double max = Max(r, g, b);
            double min = Min(r, g, b);
            double chroma = max - min;
            double hue2 = 0d;
            if (chroma != 0)
            {
                if (max == r)
                {
                    hue2 = (g - b) / chroma;
                }
                else if (max == g)
                {
                    hue2 = (b - r) / chroma + 2;
                }
                else
                {
                    hue2 = (r - g) / chroma + 4;
                }
            }
            double hue = hue2 * 60;
            if (hue < 0)
            {
                hue += 360;
            }
            double brightness = max;
            double saturation = 0;
            if (chroma != 0)
            {
                saturation = chroma / brightness;
            }
            return new HSB()
            {
                H = hue,
                S = saturation,
                B = brightness
            };
        }

        private static double Max(double d1, double d2, double d3)
        {
            if (d1 > d2)
            {
                return Math.Max(d1, d3);
            }
            return Math.Max(d2, d3);
        }

        private static double Min(double d1, double d2, double d3)
        {
            if (d1 < d2)
            {
                return Math.Min(d1, d3);
            }
            return Math.Min(d2, d3);
        }

        /// <summary>
        /// Creates color with corrected brightness.
        /// </summary>
        /// <param name="color">Color to correct.</param>
        /// <param name="correctionFactor">The brightness correction factor. Must be between -1 and 1. 
        /// Negative values produce darker colors.</param>
        /// <returns>
        /// Corrected <see cref="Color"/> structure.
        /// </returns>
        private static Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }

        /// <summary>
        /// Met dans le bon format la chaine hexadécimale
        /// </summary>
        /// <param name="hex">Chaine à formater</param>
        /// <returns></returns>
        private static string ToRgbaHexa(string hex)
        {
            string result = string.Empty;

            //On enlève le #
            hex = hex.StartsWith("#") ? hex.Substring(1) : hex;

            if (hex.Length == 8)
            {
                //Déjà au bon format
                result = hex;
            }
            else if (hex.Length == 6)
            {
                //Il manque des caractères
                result = hex + "FF";
            }
            else if (hex.Length < 3 || hex.Length > 4)
            {
                //Format invalide
                result = null;
            }
            else
            {
                string red = char.ToString(hex[0]);
                string green = char.ToString(hex[1]);
                string blue = char.ToString(hex[2]);
                string alpha = hex.Length == 3 ? "F" : char.ToString(hex[3]);
                result = string.Concat(red, red, green, green, blue, blue, alpha, alpha);
            }


            return result;
        }
        #endregion


    }
}
