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
            source = source.Replace(Environment.NewLine, " ");
            source = source.Replace("As a", "<B>As a</B>")
                .Replace("I want", "<br/><B>I want</B>")
                .Replace("So that", "<br/><B>So that</B>");

            return source;
        }

        public static string ToGiven(this string source)
        {
            // Strip out all carriage returns as they will be reformatted
            source = source.Replace(Environment.NewLine, " ");
            source = source.Replace("Given", "<B>Given</B>")
                .Replace("And", "<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<B>And</B>");

            return source;
        }
        public static string ToWhen(this string source)
        {
            // Strip out all carriage returns as they will be reformatted
            source = source.Replace(Environment.NewLine, " ");
            source = source.Replace("When", "<b>When</b>")
                .Replace("And", "<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<B>And</B>");

            return source;
        }
        public static string ToThen(this string source)
        {
            // Strip out all carriage returns as they will be reformatted
            source = source.Replace(Environment.NewLine, " ");
            source = source.Replace("Then", "")
                .Replace("And", "<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<B>And</B>");

            return source;
        }
    }
}
