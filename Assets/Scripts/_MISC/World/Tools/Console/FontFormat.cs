using System;
using UnityEngine;

namespace Deceilio.Psychain
{
    public class FontFormat
    {
        private string _prefix;

        private string _suffix;

        public static FontFormat Bold = new FontFormat("b");
        public static FontFormat Italic = new FontFormat("i");
        private FontFormat(string format)
        {
            _prefix = $"<{format}>";
            _suffix = $"</{format}>";
        }

        public static string operator %(string text, FontFormat textFormat)
        {
            return textFormat._prefix + text + textFormat._suffix;
        }
    }
}