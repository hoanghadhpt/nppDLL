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

            // $05: ShortName
            if (output.Contains("$05:"))
            {
                output = Regex.Replace(output, @"\$05\:(.*)\r\n", "<short-name>\r\n<text>$1</text>\r\n</short-name>\r\n");
            }
            // $10: Long name
            if (output.Contains("$10:"))
            {
                string text10 = Regex.Match(output, @"\$10\:(.*)\r\n").Value.ToString();
                string text10_process = text10.Replace(", and", "\r\n, and\r\n");
                text10_process = text10_process.Replace("Between", "<long-name>\r\n<proc-phrase>Between</proc-phrase>");
                text10_process = Regex.Replace(text10_process, "!!(.*)!(.*)", "<party>$1<party-role>$2</party-role></party>");
                text10_process = text10_process.Replace("\r</party-role></party>", "</party-role></party>");
                text10_process = text10_process.Replace("</party-role></party>\n", "</party-role></party>\r\n");
                text10_process = text10_process.Replace("</party-role></party>\r", "</party-role></party>\r\n");
                text10_process = text10_process.Replace("\n, and\r\n", ", and");
                text10_process = text10_process.Replace("\r, and\r\n", ", and");
                text10_process = text10_process.Replace("$10:", "") + "</long-name>\r\n";
                text10_process = text10_process.Replace("\n</long-name>", "</long-name>");

                output = output.Replace(text10, text10_process);
            }



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

            // TOC

            output = Regex.Replace(output, "<para>\r\n<text>>>(.*?)</text>\r\n</para>", "<toc>\r\n<toc-entry>\r\n<heading>$1</heading>\r\n</toc-entry>\r\n</toc>");
            output = output.Replace("\r\n</toc>\r\n<toc>\r\n", "\r\n");
            output = Regex.Replace(output, "<toc>\r\n<toc-entry>\r\n(.*?)\r\n</toc-entry>", "<toc>$1\r\n");

            output = output.Replace("<toc><heading>", "<toc>\r\n<heading>");
            // Table of Authorities

            // Clean Up 2
            output = output.Replace("<text>\r\n<pnum>", "<pnum>");
            output = output.Replace("$=E</text>", "</heading>");
            output = output.Replace("<heading level=\"1\"><b>", "<heading level=\"1\">");
            output = output.Replace("</b></heading>", "</heading>");

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
