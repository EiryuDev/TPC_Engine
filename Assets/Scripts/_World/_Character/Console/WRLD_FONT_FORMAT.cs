namespace Deceilio.TPC_Engine
{
    public class WRLD_FONT_FORMAT
    {
        private string _prefix;

        private string _suffix;

        public static WRLD_FONT_FORMAT Bold = new WRLD_FONT_FORMAT("b");
        public static WRLD_FONT_FORMAT Italic = new WRLD_FONT_FORMAT("i");
        private WRLD_FONT_FORMAT(string format)
        {
            _prefix = $"<{format}>";
            _suffix = $"</{format}>";
        }

        public static string operator %(string text, WRLD_FONT_FORMAT textFormat)
        {
            return textFormat._prefix + text + textFormat._suffix;
        }
    }
}