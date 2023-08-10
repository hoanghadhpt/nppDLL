using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace phdesign.NppToolBucket
{
    class classMosaic
    {
        public string MOSAIC_VISF_TO_XML(string text)
        {
            string output = text;

            // Ha dau $
            output = output.Replace("\r\n", " ");
            output = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<document>\r\n<text>\r\n" + output + "</p>\r\n</text>\r\n</document>";

            // para
            output = output.Replace("$%$%", "</p>\r\n<p class=\"center\">");
            output = output.Replace("$T", "</p>\r\n<p class=\"indent\">");
            output = output.Replace("$%", "</p>\r\n<p class=\"right\">");

            //Fonts cleanup
            //  ===> Valid Font

            output = output.Replace("$N\r\n$I", "\r\n");
            output = output.Replace("$N,\r\n$I", ",\r\n");
            output = output.Replace("v$N.\r\n$I", "v.\r\n");

            output = output.Replace("$=R\r\n$=B", "\r\n");
            output = output.Replace("$=R,\r\n$=B", ",\r\n");
            output = output.Replace("v$=R.\r\n$=B", "v.\r\n");

            output = output.Replace("$I\r\n$U", "\r\n");
            output = output.Replace("$O,\r\n$U", ",\r\n");
            output = output.Replace("v$O.\r\n$U", "v.\r\n");

            output = output.Replace("“", "\"");
            output = output.Replace("”", "\"");
            output = output.Replace("‘", "\'");
            output = output.Replace("’", "\'");

            output = output.Replace("Id$N.", "Id.$N");
            output = output.Replace("id$N.", "id.$N");
            output = output.Replace(" seq$N.", " seq.$N");
            output = output.Replace(" seq$N.", " seq.$N");
            output = output.Replace("$N.,", ".$N,");
            output = output.Replace("$N; $I", "; ");
            output = output.Replace("$N, $I", ", ");
            output = output.Replace("$N $I", " ");

            output = output.Replace("Id$=R.", "Id.$=R");
            output = output.Replace("id$=R.", "id.$=R");
            output = output.Replace(" seq$=R.", " seq.$=R");
            output = output.Replace(" seq$=R.", " seq.$=R");
            output = output.Replace("$=R.,", ".$=R,");
            output = output.Replace("$=R; $=B", "; ");
            output = output.Replace("$=R, $=B", ", ");
            output = output.Replace("$=R $=B", " ");

            output = output.Replace("Id$O.", "Id.$O");
            output = output.Replace("id$O.", "id.$O");
            output = output.Replace(" seq$O.", " seq.$O");
            output = output.Replace(" seq$O.", " seq.$O");
            output = output.Replace("$O.,", ".$O,");
            output = output.Replace("$O; $U", "; ");
            output = output.Replace("$O, $U", ", ");
            output = output.Replace("$O $U", " ");

            output = output.Replace("$=R?", "?$=R");
            output = output.Replace("$N?", "?$N");
            output = output.Replace("$O?", "?$O");

            // Font Xuyen
            output = output.Replace("$=R $=B", " ");
            output = output.Replace("$N $I", " ");
            output = output.Replace("$O $U", " ");

            output = output.Replace("$=R,\r\n$=B", ",\r\n");
            output = output.Replace("$N,\r\n$I", ",\r\n");
            output = output.Replace("$O,\r\n$U", ",\r\n");

            output = output.Replace("$N\r\nv. $I", "\r\nv. ");
            output = output.Replace("$O\r\nv. $U", "\r\nv. ");
            output = output.Replace("$=R\r\nv. $=B", "\r\nv. ");

            output = output.Replace("$N. v.\r\n$I", ". v.\r\n");
            output = output.Replace("$O. v.\r\n$U", ". v.\r\n");
            output = output.Replace("$=R. v.\r\n$=B", ". v.\r\n");

            output = output.Replace("$=R. $=B", " ");
            output = output.Replace("$N. $I", " ");
            output = output.Replace("$O. $U", " ");

            output = output.Replace("$=R, $=B", " ");
            output = output.Replace("$N, $I", " ");
            output = output.Replace("$O, $U", " ");

            output = output.Replace("$=R v. $=B", " v. ");
            output = output.Replace("$N v. $I", " v. ");
            output = output.Replace("$O v. $U", " v. ");

            output = output.Replace("$=R. v. $=B", ". v. ");
            output = output.Replace("$N. v. $I", ". v. ");
            output = output.Replace("$O. v. $U", ". v. ");

            output = output.Replace("$=R, v. $=B", ". v. ");
            output = output.Replace("$N, v. $I", ". v. ");
            output = output.Replace("$O, v. $U", ". v. ");

            output = output.Replace("$=R. v $=B", ". v. ");
            output = output.Replace("$N. v $I", ". v. ");
            output = output.Replace("$O. v $U", ". v. ");

            output = output.Replace("$=R, v $=B", ". v. ");
            output = output.Replace("$N, v $I", ". v. ");
            output = output.Replace("$O, v $U", ". v. ");

            output = output.Replace("$=R v $=B", ". v. ");
            output = output.Replace("$N v $I", ". v. ");
            output = output.Replace("$O v $U", ". v. ");

            output = output.Replace("$N.\r\n$Iv. ", ".\r\nv. ");
            output = output.Replace("$O.\r\n$Uv. ", ".\r\nv. ");
            output = output.Replace("$=R.\r\n$=Bv. ", ".\r\nv. ");

            output = output.Replace("$=R\r\n$=B", "\r\n");
            output = output.Replace("$N\r\n$I", "\r\n");
            output = output.Replace("$O\r\n$U", "\r\n");

            output = output.Replace("$I$N", "");
            output = output.Replace("$=B$=R", "");
            output = output.Replace("$U$O", "");

            output = output.Replace("$I.$N", ".");
            output = output.Replace("$=B.$=R", ".");
            output = output.Replace("$U.$O", ".");

            output = output.Replace("$N--$I", "--");
            output = output.Replace("$=R--$=B", "--");
            output = output.Replace("$O--$U", "--");

            output = output.Replace("$I, ", ", $I");
            output = output.Replace("$=B, ", ", $=B");
            output = output.Replace("$U, ", ", $U");

            output = output.Replace(". . .$N", "$N. . .");
            output = output.Replace(". .$N .", "$N. . .");
            output = output.Replace(".$N . .", "$N. . .");


            // fonts
            output = output.Replace("$=B", "<b>");
            output = output.Replace("$=R", "</b>");

            output = output.Replace("$U", "<u>");
            output = output.Replace("$O", "</u>");

            output = output.Replace("$I", "<i>");
            output = output.Replace("$N", "</i>");

            output = Regex.Replace(output, @" n(\d+)", "<sup>$1</sup>");

            output = output.Replace("</i>. <i>", ". ");
            output = output.Replace("</u>. <u>", ". ");
            output = output.Replace("</b>. <b>", ". ");

            output = output.Replace("</i>, <i>", ", ");
            output = output.Replace("</u>, <u>", ", ");
            output = output.Replace("</b>, <b>", ", ");

            output = output.Replace("</i> <i>", " ");
            output = output.Replace("</u> <u>", " ");
            output = output.Replace("</b> <b>", " ");

            output = Regex.Replace(output, "Id</(b|i|u)>\\.", "Id.</$1>");
            output = Regex.Replace(output, "id</(b|i|u)>\\.", "id.</$1>");
            output = Regex.Replace(output, "</(b|i|u)>\\.,", ".</$1>,");


            // Special Chars

            output = output.Replace("&", "&#38;");
            output = output.Replace("’s", "'s");
            output = output.Replace("“", "&#34;");
            output = output.Replace("”", "&#34;");

            output = output.Replace("§", "&#167;");
            output = output.Replace("¶", "&#182;");


            // CLEANUP
            output = output.Replace("<text>\r\n</p>", "<text>");
            output = output.Replace(" </p>", "</p>");


            // Return
            return output;
        }

        public string AddImageLink(string text,string name)
        {
            string Pattern = @"__@|__#";
            var result = "";
            int i = 0;
            result = Regex.Replace(text, Pattern, (Match n) =>
            {
                if (n.Value == "__@")
                {
                    result = String.Format("<attach file=\"{0}_{1}.jpg\" title=\"attachment{1}\"/>",name,++i);
                }
                else if (n.Value == "__#")
                {
                    result = String.Format("<imgsrc=\"{0}_{1}.jpg\" alt=\"attachment{1}\">", name, ++i);
                }
                return result;
            });

            result = result.Replace("<p class=\"indent\"><attach", "<attach");
            result = result.Replace("\"/> </p>", "\"/>");
            result = result.Replace("\"/></p>", "\"/>");

            return result;

        }

    }
}
