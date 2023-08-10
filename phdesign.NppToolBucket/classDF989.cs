using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using phdesign.NppToolBucket.PluginCore;
using phdesign.NppToolBucket.Utilities.Security;
using phdesign.NppToolBucket.Forms;

namespace phdesign.NppToolBucket
{
    class classDF989
    {
        public string DF989_Process(string input)
        {
            string output = input;
            // Font
            output = output.Replace("$=B$I", "<emph typestyle=\"ib\">");
            output = output.Replace("$N$=R", "</emph>");
            output = output.Replace("$=B$U", "<emph typestyle=\"bu\">");
            output = output.Replace("$O$=R", "</emph>");

            output = output.Replace("$=B", "<emph typestyle=\"bf\">");
            output = output.Replace("$I", "<emph typestyle=\"it\">");
            output = output.Replace("$U", "<emph typestyle=\"un\">");
            output = output.Replace("$=R", "</emph>");
            output = output.Replace("$N", "</emph>");
            output = output.Replace("$O", "</emph>");


            // Ha dau $
            output = output.Replace("$00:0100000001:", "\r\n\r\n$00:0100000001:\r\n\r\n");
            output = output.Replace("\r\n", " ");
            output = Regex.Replace(output, @"(\$\d+)\:", "\r\n$1:");
            output = Regex.Replace(output, @"\$120\:(.*)", "\r\n<lnv:TEXT-1>\r\n<lnvxe:bodytext>\r\n$1</lnvxe:text></p>\r\n</lnvxe:bodytext>\r\n</lnv:TEXT-1>");

            // mac dinh doan
            output = preProcessBlockquote(output);
            output = output.Replace(" $%$%", "</lnvxe:text></p>\r\n<p><lnvxe:text>");
            output = output.Replace(" $T", "</lnvxe:text></p>\r\n<p><lnvxe:text>");
            output = output.Replace(" $=H", "</lnvxe:text></p>\r\n<lnvxe:h>");
            output = output.Replace("$=E</lnvxe:text></p>", "</lnvxe:h>\r\n");
            output = output.Replace("$=H", "<lnvxe:h>");
            output = output.Replace(" $=S", "</lnvxe:text></p>\r\n<lnvxe:blockquote>\r\n");
            output = output.Replace("$=I</lnvxe:text></p>", "</lnvxe:text></p>\r\n</lnvxe:blockquote>");
            output = output.Replace(" ___$%$%", "</lnvxe:text></p>\r\n<p><lnvxe:text align=\"left\">");
            output = output.Replace("___$%$%", "<p><lnvxe:text align=\"left\">");
            //<lnv:HIER>  Level
            output = Regex.Replace(output, @"\$11:(.*)", m => $"<lnv:HIER>\r\n<lnvxe:hierlev role=\"ancestor\">\r\n<lnvxe:hierannot caption=\"{m.Groups[1].Value.Trim()}\" role=\"ancestor\" level=\"1\"/>\r\n<lnvxe:heading inline=\"false\">\r\n<lnvxe:title>{m.Groups[1].Value.Trim()}</lnvxe:title>\r\n</lnvxe:heading>\r\n</lnvxe:hierlev>\r\n</lnv:HIER>");
            output = Regex.Replace(output, @"\$12:(.*)", m => $"<lnv:HIER>\r\n<lnvxe:hierlev role=\"ancestor\">\r\n<lnvxe:hierannot caption=\"{m.Groups[1].Value.Trim()}\" role=\"ancestor\" level=\"2\"/>\r\n<lnvxe:heading inline=\"false\">\r\n<lnvxe:title>{m.Groups[1].Value.Trim()}</lnvxe:title>\r\n</lnvxe:heading>\r\n</lnvxe:hierlev>\r\n</lnv:HIER>");
            output = Regex.Replace(output, @"\$13:(.*)", m => $"<lnv:HIER>\r\n<lnvxe:hierlev role=\"ancestor\">\r\n<lnvxe:hierannot caption=\"{m.Groups[1].Value.Trim()}\" role=\"ancestor\" level=\"3\"/>\r\n<lnvxe:heading inline=\"false\">\r\n<lnvxe:title>{m.Groups[1].Value.Trim()}</lnvxe:title>\r\n</lnvxe:heading>\r\n</lnvxe:hierlev>\r\n</lnv:HIER>");
            output = Regex.Replace(output, @"\$14:(.*)", m => $"<lnv:HIER>\r\n<lnvxe:hierlev role=\"ancestor\">\r\n<lnvxe:hierannot caption=\"{m.Groups[1].Value.Trim()}\" role=\"ancestor\" level=\"4\"/>\r\n<lnvxe:heading inline=\"false\">\r\n<lnvxe:title>{m.Groups[1].Value.Trim()}</lnvxe:title>\r\n</lnvxe:heading>\r\n</lnvxe:hierlev>\r\n</lnv:HIER>");
            output = Regex.Replace(output, @"\$15:(.*)", m => $"<lnv:HIER>\r\n<lnvxe:hierlev role=\"ancestor\">\r\n<lnvxe:hierannot caption=\"{m.Groups[1].Value.Trim()}\" role=\"ancestor\" level=\"5\"/>\r\n<lnvxe:heading inline=\"false\">\r\n<lnvxe:title>{m.Groups[1].Value.Trim()}</lnvxe:title>\r\n</lnvxe:heading>\r\n</lnvxe:hierlev>\r\n</lnv:HIER>");
            output = Regex.Replace(output, @"\$16:(.*)", m => $"<lnv:HIER>\r\n<lnvxe:hierlev role=\"ancestor\">\r\n<lnvxe:hierannot caption=\"{m.Groups[1].Value.Trim()}\" role=\"ancestor\" level=\"6\"/>\r\n<lnvxe:heading inline=\"false\">\r\n<lnvxe:title>{m.Groups[1].Value.Trim()}</lnvxe:title>\r\n</lnvxe:heading>\r\n</lnvxe:hierlev>\r\n</lnv:HIER>");
            output = Regex.Replace(output, @"\$17:(.*)", m => $"<lnv:HIER>\r\n<lnvxe:hierlev role=\"ancestor\">\r\n<lnvxe:hierannot caption=\"{m.Groups[1].Value.Trim()}\" role=\"ancestor\" level=\"7\"/>\r\n<lnvxe:heading inline=\"false\">\r\n<lnvxe:title>{m.Groups[1].Value.Trim()}</lnvxe:title>\r\n</lnvxe:heading>\r\n</lnvxe:hierlev>\r\n</lnv:HIER>");
            output = Regex.Replace(output, @"\$18:(.*)", m => $"<lnv:HIER>\r\n<lnvxe:hierlev role=\"ancestor\">\r\n<lnvxe:hierannot caption=\"{m.Groups[1].Value.Trim()}\" role=\"ancestor\" level=\"8\"/>\r\n<lnvxe:heading inline=\"false\">\r\n<lnvxe:title>{m.Groups[1].Value.Trim()}</lnvxe:title>\r\n</lnvxe:heading>\r\n</lnvxe:hierlev>\r\n</lnv:HIER>");
            output = Regex.Replace(output, @"\$19:(.*)", m => $"<lnv:HIER>\r\n<lnvxe:hierlev role=\"ancestor\">\r\n<lnvxe:hierannot caption=\"{m.Groups[1].Value.Trim()}\" role=\"ancestor\" level=\"9\"/>\r\n<lnvxe:heading inline=\"false\">\r\n<lnvxe:title>{m.Groups[1].Value.Trim()}</lnvxe:title>\r\n</lnvxe:heading>\r\n</lnvxe:hierlev>\r\n</lnv:HIER>");
            output = output.Replace("</lnv:HIER>\r\n<lnv:HIER>", "");
            output = output.Replace("</lnv:HIER>\n<lnv:HIER>", "");
            output = output.Replace("</lnv:HIER>\r<lnv:HIER>", "");
            // <lnv:HIER-ME>
            output = Regex.Replace(output, @"\$20:(.*)", m => $"<lnv:HIER-ME>\r\n<lnvxe:hierlev role=\"me\">\r\n<lnvxe:hierannot caption=\"{m.Groups[1].Value.Trim()}\" level=\"10\" role=\"me\"/>\r\n<lnvxe:heading inline=\"false\">\r\n<lnvxe:title>{m.Groups[1].Value.Trim()}</lnvxe:title>\r\n</lnvxe:heading>\r\n</lnvxe:hierlev>\r\n</lnv:HIER-ME>\r\n");
            output = Regex.Replace(output, @"\$30:(.*)", m => $"<lnv:CITE>{m.Groups[1].Value.Trim()}</lnv:CITE>\r\n");
            // <lnv:DOC-HEADING>
            output = Regex.Replace(output, @"\$10:(.*)", m => $"<lnv:PUB>\r\n<lnvxe:publication>{m.Groups[1].Value.Trim()}</lnvxe:publication>\r\n</lnv:PUB>\r\n");
            // <ENDING ELEMENTS>
            output = Regex.Replace(output, @"\$200:(.*)", m => $"<lnv:BOOKSEQNUM>0000000000000100</lnv:BOOKSEQNUM>\r\n" +
            $"<lnv:BOOKSEQNUM>0000000000000200</lnv:BOOKSEQNUM>\r\n" +
            $"<lnv:BOOKSEQNUM>0000000000000300</lnv:BOOKSEQNUM>\r\n" +
            $"<lnv:LANGUAGE>\r\n" +
            $"<lnvxe:lang.english iso639-1=\"en\">ENGLISH</lnvxe:lang.english>\r\n" +
            $"</lnv:LANGUAGE>\r\n" +
            $"<lnv:PUBLICATION-TYPE>\r\n" +
            $"<lnvxe:desc>#TREATISE#</lnvxe:desc>\r\n" +
            $"</lnv:PUBLICATION-TYPE>\r\n" +
            $"<lnv:REPORT-NO>NCJIMV2020</lnv:REPORT-NO>\r\n" +
            $"<lnv:LOAD-DATE>\r\n" +
            $"<lnvxe:date year=\"___\" month=\"__\" day=\"__\">___</lnvxe:date>\r\n" +
            $"</lnv:LOAD-DATE>\r\n" +
            $"</COMMENTARYDOC-LDC>");
            // List $T## Start/End
            output = output.Replace("$K ", "$K");
            output = output.Replace(" $W", "$W");
            output = output.Replace("<p><lnvxe:text>##$W", "<lnvxe:l>\r\n<lnvxe:li>");
            output = output.Replace(@"$W", "</lnvxe:text></p>\r\n</lnvxe:li>\r\n" +
            $"<lnvxe:li>\r\n" +
            $"<lnvxe:lilabel>");
            output = output.Replace(@"$K", "</lnvxe:lilabel>\r\n" +
            $"<p><lnvxe:text align=\"left\">");
            output = output.Replace(@"<p><lnvxe:text>##</lnvxe:text></p>", "</lnvxe:li>\r\n</lnvxe:l>");
            // Footnote Indicator

            output = Regex.Replace(output, @" n(\d+)", m => $"<lnvxe:fnr fnrtoken=\"fnr{m.Groups[1].Value.Trim()}\" fntoken=\"fn{m.Groups[1].Value.Trim()}\">{m.Groups[1].Value.Trim()}</lnvxe:fnr>");
            output = output.Replace(" <lnvxe:fnr", "<lnvxe:fnr");
            // Footnote $180:
            output = Regex.Replace(output, @"\$180:(.*)", m => $"<lnv:FOOTNOTE-1>\r\n{m.Groups[1].Value.Trim()}\r\n</lnv:FOOTNOTE-1>\r\n");
            // BODY FOOTNOTE
            output = Regex.Replace(output,
                @"\$F(.*?)n(\d+) (.*?)\$E", 
                m => String.Format("<lnvxe:footnotegrp>\r\n" +
                "<lnvxe:footnote fnrtokens=\"fn{0}\" fntoken=\"{0}\" type=\"default\">\r\n" +
                "<lnvxe:fnlabel text=\"label\">{0}</lnvxe:fnlabel>\r\n" +
                "<lnvxe:fnbody>\r\n<p><lnvxe:text>{1}</lnvxe:text></p>\r\n" +
                "</lnvxe:fnbody></lnvxe:footnote>\r\n" +
                "</lnvxe:footnotegrp>", m.Groups[2].Value, m.Groups[3].Value),RegexOptions.Singleline);

            
            output = output.Replace("</lnvxe:footnotegrp>\r\n<lnvxe:footnotegrp>", "");
            output = output.Replace("</lnvxe:footnotegrp> <lnvxe:footnotegrp>", "");
            output = SplitRenumberFootnote(output);

            // Final

            
            output = output.Replace(" \r\n</lnvxe:text></p>", "</lnvxe:text></p>");
            output = output.Replace(" \n</lnvxe:text></p>", "</lnvxe:text></p>");
            output = output.Replace(" \r</lnvxe:text></p>", "</lnvxe:text></p>");
            output = output.Replace("<lnvxe:bodytext>\r\n$T", "<lnvxe:bodytext>\r\n<p><lnvxe:text>");

            // return
            return output;
        }

        private string SplitRenumberFootnote(string inputText)
        {
            string allText = "";
            string[] splited = Regex.Split(inputText, @"(?=\$00:.*)", RegexOptions.None);
            for (int i = 0; i < splited.Length; i++)
            {
                allText += DF989_RenumberFootnote(splited[i])+"\r\n";
            }
            return allText;
        }

        private string DF989_RenumberFootnote(string input)
        {
            // Body1
            string Patt2 = @"<lnvxe:footnote fnrtokens=""fn([0-9]{1,3})"" fntoken=""([0-9]{1,3})"" type=""default"">";
            var i1 = 0;
            var result1 = "";
            result1 = Regex.Replace(input, Patt2, (Match n) =>
            {
                if (i1 < 9)
                {
                    result1 = string.Format("<lnvxe:footnote fnrtokens=\"fn00{0}\" fntoken=\"{0}\" type=\"default\">", ++i1);
                }
                else if (i1 < 99)
                {
                    result1 = string.Format("<lnvxe:footnote fnrtokens=\"fn0{0}\" fntoken=\"{0}\" type=\"default\">", ++i1);
                }
                else
                {
                    result1 = string.Format("<lnvxe:footnote fnrtokens=\"fn{0}\" fntoken=\"{0}\" type=\"default\">", ++i1);
                }
                return result1;
            });
            // Body2
            string Patt3 = @"<lnvxe:fnlabel text=""label"">([0-9]{1,3})<\/lnvxe:fnlabel>";
            var i2 = 0;
            var result2 = "";
            result2 = Regex.Replace(result1, Patt3, (Match n) =>
            {
                result2 = string.Format(@"<lnvxe:fnlabel text=""label"">{0}</lnvxe:fnlabel>", ++i2);
                return result2;
            });

            return result2;
        }

        private string preProcessBlockquote(string input)
        {
            string output = input;
            foreach (Match m in Regex.Matches(input, @"\$=S(.*?)\$=I", RegexOptions.Singleline))
            {
                string mReplaced = m.Value.ToString().Replace("$T", "$%$%");
                mReplaced = mReplaced.Replace("$%$%", "___$%$%");
                output = output.Replace(m.Value.ToString(), mReplaced);
            }
            return output;
        }

        public string DF989_ReNumberLevel(string input)
        {
            string Patt = @"(\<lnvxe:hierannot caption="".*level="")(\d+)""(\/>| role=""me""\/>)";

            var result = "";
            int i = 0;
            result = Regex.Replace(input, Patt, (Match n) =>
            {
                if (!n.Value.ToString().Contains("role=\"me\"/>"))
                {
                    result = string.Format(n.Groups[1] + "{0}\"/>" + Environment.NewLine, ++i);
                }
                if (n.Value.ToString().Contains("role=\"me\"/>"))
                {
                    result = string.Format(n.Groups[1] + "{0}\" role=\"me\"/>" + Environment.NewLine, ++i);
                    i = 0;
                }
                return result;
            });

            return result;
        }



        private static string SegmentsSort(string input)
        {
            string text = input;
            text = text.Replace("\r\n", "[!!]");
            string tempAllFiles = "";
            // Split Into Multiple Files
            string[] listFiles = Regex.Split(text, @"(?=\$00:)");

            foreach (string file in listFiles)
            {
                string[] Segments = Regex.Split(file, @"(?=\$\d+:)");
                Array.Sort(Segments, new MyComparer());
                string tempFile = "";
                foreach (string s in Segments)
                {
                    tempFile += s;
                }
                tempAllFiles += tempFile;
            }
            tempAllFiles = tempAllFiles.Replace("[!!]", "\r\n");
            return tempAllFiles;
        }
        private class MyComparer : IComparer<string>
        {
            [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
            static extern int StrCmpLogicalW(String x, String y);
            public int Compare(string x, string y)
            {
                return StrCmpLogicalW(x, y);
            }
        }
    }
}
