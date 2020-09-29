using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace phdesign.NppToolBucket
{
    class classCanada
    {
        public string TextString(string input)
        {
            string output = input;
            // Footnote
            output = footnote(output);

            // Font
            output = output.Replace("$=B", "<b>");
            output = output.Replace("$I", "<i>");
            output = output.Replace("$U", "<u>");
            output = output.Replace("$=R", "</b>");
            output = output.Replace("$N", "</i>");
            output = output.Replace("$O", "</u>");


            // Ha dau $
            output = output.Replace("$00:0100000001:", "\r\n\r\n$00:0100000001:\r\n\r\n");
            output = output.Replace("\r\n", " ");

            output = Regex.Replace(output, @"(\$\d+)\:", "\r\n$1:");
            output = Regex.Replace(output, @"\$120\:(.*)", "\r\n$1</text>\r\n</para>");

            // mac dinh doan

            output = output.Replace(" $T", "</text>\r\n</para>\r\n<para>\r\n<text>");
            output = output.Replace("$T", "<para>\r\n<text>");
            output = output.Replace(" $%$%", "</text>\r\n<text>");
            output = output.Replace("$%$%", "<text>");
            output = output.Replace(" $=H", "</text>\r\n</para>\r\n<heading level=\"1\">");
            output = output.Replace("$=E</para>", "</heading>");
            output = output.Replace("$=H", "<heading level=\"1\">");
            output = output.Replace(" $=S", "</text>\r\n<blockquote>\r\n");
            output = output.Replace("$=I", "</text>\r\n</blockquote>\r\n");

            // Clean up
            output = output.Replace("</text>\r\n</blockquote>\r\n</text>", "</text>\r\n</blockquote>");
            output = output.Replace("<para>\r\n\r\n<pnum>", "<para>\r\n<pnum>");
            output = output.Replace("$=E</text>\r\n</para>", "</heading>");
            output = output.Replace("</blockquote>\r\n</blockquote>\r\n</text>", "</blockquote>\r\n</blockquote>");

            // convert <pnum>
            output = Regex.Replace(output, @"\$=P(.*?) ", "\r\n<pnum>$1</pnum>\r\n<text>");

            // List
            output = output.Replace(" $(O>", "</text>\r\n<list>\r\n");
            output = output.Replace("<O$)", "\r\n</list>");
            output = output.Replace(" $%", "\r\n$%");
            output = output.Replace("$%", "\r\n$%");
            output = Regex.Replace(output, @"\$%(.*?) (.*?)\r\n", "<list-item>\r\n<label>$1</label>\r\n<text>$2</text>\r\n</list-item>");
            output = output.Replace("</list-item></list></text>", "</list-item>\r\n</list>");

            // Clean Up 2
            output = output.Replace("<text>\r\n<pnum>", "<pnum>");
            output = output.Replace("$=E</text>", "</heading>");
           
            
            // return
            return output;
        }

        private string footnote(string input)
        {
            lstBody.Clear();
            string text = input;
            text = text.Replace(">ENDFN>", "");
            text = text.Replace(">FTNT>", "");

            // Get Body footnote and Remove from Text
            foreach (Match m in Regex.Matches(text, @"\$F(.*?)n(\d+) (.*?)\$E", RegexOptions.Singleline))
            {
                lstBody.Add(m.Groups[3].ToString());  // Add to list
                text = text.Replace(m.Value, "");
            }
            string result = "";
            int j = 0;
            result = Regex.Replace(text, @" n(\d+)", (Match n) =>
                 {
                     result = String.Format("<footnote ref=\"{0}\"><text>{1}</text></footnote>", n.Groups[1].Value, lstBody[j]);
                     j++;
                     return result;
                 }
            );

            return result;
        }

        private List<string> lstBody = new List<string>();
    }
}
