using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSpec.Extensions.Model
{
    public static class Formatter
    {
        public static string ToHtmlParagraph(this string source)
        {
            source = source.Replace(Environment.NewLine, "<BR/>");
            return source;
        }

        public static string ToStory(this string source)
        {
            source = source.Replace(Environment.NewLine, "<BR/>");
            source = source.Replace("As a", "<B>As a</B>")
                .Replace("I want", "<B>I want</B>")
                .Replace("So that", "<B>So that</B>");

            return source;
        }
    }
}
