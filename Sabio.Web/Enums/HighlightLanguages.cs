using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Enums
{
    /*
        this enum is used to keep track of languages and their corresponding classes that can be used for the highlight.js library
        see https://github.com/isagalaev/highlight.js/tree/master/src/languages
    */
    public sealed class HighlightLanguages
    {
        private static readonly Dictionary<string, HighlightLanguages> instance = new Dictionary<string, HighlightLanguages>();

        private readonly string langName;
        private readonly string langClass;

        public static readonly HighlightLanguages bash = new HighlightLanguages("bash", "Bash (Linux)");
        public static readonly HighlightLanguages cs = new HighlightLanguages("cs", "C#");
        public static readonly HighlightLanguages css = new HighlightLanguages("css", "CSS");
        public static readonly HighlightLanguages http = new HighlightLanguages("http", "HTTP Request/Response");
        public static readonly HighlightLanguages ini = new HighlightLanguages("ini", "INI File");
        public static readonly HighlightLanguages js = new HighlightLanguages("javascript", "Javascript");
        public static readonly HighlightLanguages json = new HighlightLanguages("json", "JSON");
        public static readonly HighlightLanguages sql = new HighlightLanguages("sql", "SQL");
        public static readonly HighlightLanguages xml = new HighlightLanguages("xml", "HTML/XML");


        private HighlightLanguages(string langClass, string langName)
        {
            this.langName = langName;
            this.langClass = langClass;
            instance[langClass] = this;
        }

        public override String ToString()
        {
            return langClass;
        }

        public static Dictionary<string, string> getDictionary()
        {
            Dictionary<string, string> output = new Dictionary<string, string>();

            foreach (KeyValuePair<string, HighlightLanguages> entry in instance)
            {
                output.Add(entry.Value.langClass, entry.Value.langName);
            }
            return output;
        }

        public static explicit operator HighlightLanguages(string str)
        {
            HighlightLanguages result;
            if (instance.TryGetValue(str, out result))
                return result;
            else
                throw new InvalidCastException(str + " is an unrecognized language for highlighting");
        }
    }
}
