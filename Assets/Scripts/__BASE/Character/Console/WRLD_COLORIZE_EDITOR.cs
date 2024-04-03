using UnityEngine;

namespace Deceilio.TPC_Engine
{
    public class WRLD_COLORIZE_EDITOR 
    {
        // Color Example

        public static WRLD_COLORIZE_EDITOR Red = new WRLD_COLORIZE_EDITOR(Color.red);
        public static WRLD_COLORIZE_EDITOR Yellow = new WRLD_COLORIZE_EDITOR(Color.yellow);
        public static WRLD_COLORIZE_EDITOR Green = new WRLD_COLORIZE_EDITOR(Color.green);
        public static WRLD_COLORIZE_EDITOR Blue = new WRLD_COLORIZE_EDITOR(Color.blue);
        public static WRLD_COLORIZE_EDITOR Cyan = new WRLD_COLORIZE_EDITOR(Color.cyan);
        public static WRLD_COLORIZE_EDITOR Magenta = new WRLD_COLORIZE_EDITOR(Color.magenta);

        // Hex Example

        public static WRLD_COLORIZE_EDITOR Orange = new WRLD_COLORIZE_EDITOR("#FFA500");
        public static WRLD_COLORIZE_EDITOR Olive = new WRLD_COLORIZE_EDITOR("#808000");
        public static WRLD_COLORIZE_EDITOR Purple = new WRLD_COLORIZE_EDITOR("#800080");
        public static WRLD_COLORIZE_EDITOR DarkRed = new WRLD_COLORIZE_EDITOR("#8B0000");
        public static WRLD_COLORIZE_EDITOR DarkGreen = new WRLD_COLORIZE_EDITOR("#006400");
        public static WRLD_COLORIZE_EDITOR DarkOrange = new WRLD_COLORIZE_EDITOR("#FF8C00");
        public static WRLD_COLORIZE_EDITOR Gold = new WRLD_COLORIZE_EDITOR("#FFD700");

        private readonly string _prefix;

        private const string Suffix = "</color>";

        // Convert Color to HtmlString
        public WRLD_COLORIZE_EDITOR(Color color)
        {
            _prefix = $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>";
        }
        // Use Hex Color
        public WRLD_COLORIZE_EDITOR(string hexColor)
        {
            _prefix = $"<color={hexColor}>";
        }

        public static string operator %(string text, WRLD_COLORIZE_EDITOR color)
        {
            return color._prefix + text + Suffix;
        }


    }
}
