using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace phdesign.NppToolBucket
{
    class classDE535
    {
        public string processDE535(string input)
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
            output = Regex.Replace(output, @"\$120\:(.*)", "\r\n<lnv:BODY-1>\r\n$1</p>\r\n</lnv:BODY-1>\r\n<lnv:URL-SEG></lnv:URL-SEG>");

            // mac dinh doan

            output = output.Replace(" $%$%", "</p>\r\n<p>");
            output = output.Replace(" $T", "</p>\r\n<p>");
            output = output.Replace("$T", "<p>");
            output = output.Replace("$%$%", "<p>");
            output = output.Replace(" $=H", "</p>\r\n<lnvxe:h>");
            output = output.Replace("$=E</p>", "</lnvxe:h>\r\n");
            output = output.Replace("$=H", "<lnvxe:h>");
            output = output.Replace(" $=S", "</p>\r\n<lnvxe:blockquote>\r\n");
            output = output.Replace("$=I</p>", "</p>\r\n</lnvxe:blockquote>");
            output = output.Replace(" ___$%$%", "</p>\r\n<p><lnvxe:text align=\"left\">");
            output = output.Replace("___$%$%", "<p><lnvxe:text align=\"left\">");
            // Dola 00
            output = Regex.Replace(output, @"\$00:(.*)", m => $"<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>\r\n<NEWSITEM>\r\n<lndocmeta:docinfo>\r\n<lndocmeta:lnlni lnlni=\"A\"/>\r\n<lndocmeta:smi lnsmi=\"A817\"/>\r\n<lndocmeta:dpsi lndpsi=\"0CJZ\"/>\r\n<lndocmeta:lndoctype lndoctypename=\"\"/>\r\n<lndocmeta:lndoctypeversion lndoctypeversionmajor=\"\" lndoctypeversionminor=\"\"/>\r\n<lndocmeta:lndoctypelang lndoctypelang=\"\"/>\r\n<lndocmeta:lnfilenum lnfilenum=\"001\"/>\r\n</lndocmeta:docinfo>" +
            $"\r\n<lnv:COPYRIGHT>" +
            $"\r\n<lnv:PUB-COPYRIGHT>Copyright" +
            $"\r\n<lnvxe:copyright.year year=\"{DateTime.Now.Year.ToString()}\">{DateTime.Now.Year.ToString()}</lnvxe:copyright.year>" +
            $"\r\n<lnvxe:copyright.holder>Archant Regional Limited</lnvxe:copyright.holder>" +
            $"\r\n<nl/>All Rights Reserved" +
            $"\r\n</lnv:PUB-COPYRIGHT>" +
            $"\r\n</lnv:COPYRIGHT>" +
            $"\r\n<lnv:PUB></lnv:PUB>" +
            $"\r\n<lnv:DISCLAIMER></lnv:DISCLAIMER>" +
            $"\r\n<lnv:ARL-DEL-DATE></lnv:ARL-DEL-DATE>" +
            $"\r\n<lnv:ARL-RST-DATE></lnv:ARL-RST-DATE>");
            // <Pub-date>
            output = Regex.Replace(output, @"\$10:(.*)", m => $"<lnv:PUB-DATE>\r\n" +
            $"<lnvxe:date year=\"____\" month=\"__\" day=\"__\">{m.Groups[1].ToString().Trim()}</lnvxe:date>\r\n" +
            $"</lnv:PUB-DATE>");
            // Section Info
            output = Regex.Replace(output, @"\$11:(.*)", m => $"<lnv:SECTION-INFO>\r\n" +
            $"<lnvxe:position.section>{m.Groups[1].ToString().Trim()}</lnvxe:position.section>\r\n" +
            $"</lnv:SECTION-INFO>");
            output = Regex.Replace(output, @"\$12:(.*)", m => $"<lnv:SECTION-INFO>\r\n" +
            $"<lnvxe:position.subsection>{m.Groups[1].ToString().Trim()}</lnvxe:position.subsection>\r\n" +
            $"</lnv:SECTION-INFO>");
            output = Regex.Replace(output, @"\$13:(.*)", m => $"<lnv:SECTION-INFO>\r\n" +
            $"<lnvxe:position.sequence>Pg. {m.Groups[1].ToString().Trim()}</lnvxe:position.sequence>\r\n" +
            $"</lnv:SECTION-INFO>\r\n<lnv:LENGTH></lnv:LENGTH>\r\n");
            output = output.Replace("</lnv:SECTION-INFO>\r\n<lnv:SECTION-INFO>", "");
            output = output.Replace("</lnv:SECTION-INFO>\r<lnv:SECTION-INFO>", "");
            output = output.Replace("</lnv:SECTION-INFO>\n<lnv:SECTION-INFO>", "");
            // Headline
            output = Regex.Replace(output, @"\$14:(.*)", m => $"<lnv:HEADLINE>\r\n" +
            $"<lnvxe:hl1>{m.Groups[1].ToString().Trim()}</lnvxe:hl1>\r\n" +
            $"</lnv:HEADLINE>\r\n<lnv:SPEC-LIB>#EDCUK# </lnv:SPEC-LIB>");
            // By line
            output = Regex.Replace(output, @"\$15:(.*)", m => $"<lnv:BYLINE>\r\n" +
            $"<lnvxe:bydesc>{m.Groups[1].ToString().Trim()}</lnvxe:bydesc>\r\n" +
            $"</lnv:BYLINE>");
            // Hightlight
            output = Regex.Replace(output, @"\$16:(.*)", m => $"<lnv:HIGHLIGHT>\r\n" +
            $"<p>{m.Groups[1].ToString().Trim()}</p>\r\n" +
            $"</lnv:HIGHLIGHT>");
            // <lnv:GRAPHIC>

            string result = Regex.Replace(output, @"\$130:(.*)", (Match n) =>
            {
                if (n.Groups[1].Value != "")
                {
                    result = "<lnv:GRAPHIC>\r\n" +
            $"<lnvxe:desc>Picture 1, " + n.Groups[1].ToString().Trim() + "</lnvxe:desc>\r\n" +
            $"</lnv:GRAPHIC>";
                }
                else
                {
                    result = "<lnv:GRAPHIC>\r\n" +
            $"<lnvxe:desc>Picture, no caption</lnvxe:desc>\r\n" +
            $"</lnv:GRAPHIC>";
                }
                return (result);
            });

            output = result;

            
            output = output.Replace("</lnv:GRAPHIC>\r\n<lnv:GRAPHIC>", "");
            output = output.Replace("</lnv:GRAPHIC>\r<lnv:GRAPHIC>", "");
            output = output.Replace("</lnv:GRAPHIC>\n<lnv:GRAPHIC>", "");
            // END
            output = Regex.Replace(output, @"\$200:(.*)", m => $"<lnv:LANGUAGE>\r\n" +
            $"<lnvxe:lang.english iso639-1=\"en\">ENGLISH</lnvxe:lang.english>\r\n" +
            $"</lnv:LANGUAGE>\r\n" +
            $"<lnv:PUBLICATION-TYPE></lnv:PUBLICATION-TYPE>\r\n" +
            $"<lnv:LN-SUBJ></lnv:LN-SUBJ>\r\n" +
            $"<lnv:LN-CO></lnv:LN-CO>\r\n" +
            $"<lnv:LN-ORG></lnv:LN-ORG>\r\n" +
            $"<lnv:LN-TS></lnv:LN-TS>\r\n" +
            $"<lnv:LN-IND></lnv:LN-IND>\r\n" +
            $"<lnv:LN-PROD></lnv:LN-PROD>\r\n" +
            $"<lnv:LN-PERSON></lnv:LN-PERSON>\r\n" +
            $"<lnv:LN-CITY></lnv:LN-CITY>\r\n" +
            $"<lnv:LN-ST></lnv:LN-ST>\r\n" +
            $"<lnv:LN-COUNTRY></lnv:LN-COUNTRY>\r\n" +
            $"<lnv:DOC-ID></lnv:DOC-ID>\r\n" +
            $"<lnv:EXTRACTED-TERMS></lnv:EXTRACTED-TERMS>\r\n" +
            $"<lnv:SYS-AUDIT></lnv:SYS-AUDIT>\r\n" +
            $"<lnv:REPORT-NO></lnv:REPORT-NO>\r\n" +
            $"<lnv:LOAD-DATE></lnv:LOAD-DATE>\r\n" +
            $"</NEWSITEM>\r\n");
            // Final Process
            output = output.Replace("\r\n</p>", "</p>");
            output = output.Replace("\r</p>", "</p>");
            output = output.Replace("\n</p>", "</p>");
            output = output.Replace(" </p>", "</p>");
            output = output.Replace(" </p>", "</p>");
            output = output.Replace(" </p>", "</p>");
            output = output.Replace(" </p>", "</p>");
            output = output.Replace(" </p>", "</p>");
            // Remove Empty Lines =>> beauty document // Blank Lines
            output = Regex.Replace(output, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);

            // return
            return output;
        }
    }
}
