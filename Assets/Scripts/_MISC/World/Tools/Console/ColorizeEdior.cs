using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deceilio.Psychain
{
    public class ColorizeEdior 
    {
        // Color Example

        public static ColorizeEdior Red = new ColorizeEdior(Color.red);
        public static ColorizeEdior Yellow = new ColorizeEdior(Color.yellow);
        public static ColorizeEdior Green = new ColorizeEdior(Color.green);
        public static ColorizeEdior Blue = new ColorizeEdior(Color.blue);
        public static ColorizeEdior Cyan = new ColorizeEdior(Color.cyan);
        public static ColorizeEdior Magenta = new ColorizeEdior(Color.magenta);

        // Hex Example

        public static ColorizeEdior Orange = new ColorizeEdior("#FFA500");
        public static ColorizeEdior Olive = new ColorizeEdior("#808000");
        public static ColorizeEdior Purple = new ColorizeEdior("#800080");
        public static ColorizeEdior DarkRed = new ColorizeEdior("#8B0000");
        public static ColorizeEdior DarkGreen = new ColorizeEdior("#006400");
        public static ColorizeEdior DarkOrange = new ColorizeEdior("#FF8C00");
        public static ColorizeEdior Gold = new ColorizeEdior("#FFD700");

        private readonly string _prefix;

        private const string Suffix = "</color>";

        // Convert Color to HtmlString
        public ColorizeEdior(Color color)
        {
            _prefix = $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>";
        }
        // Use Hex Color
        public ColorizeEdior(string hexColor)
        {
            _prefix = $"<color={hexColor}>";
        }

        public static string operator %(string text, ColorizeEdior color)
        {
            return color._prefix + text + Suffix;
        }


    }
}
