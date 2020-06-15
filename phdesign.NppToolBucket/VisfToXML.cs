using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace phdesign.NppToolBucket
{
    class VisfToXML
    {
        public string tempdata;
        public int Linenumber1;
        public bool nboo;
        public static int footnotecount = 0;
        public static bool footnotecountboo = false;
        public static bool foottestcount = false;
        public static bool footcountboo = false;
        public static bool footNcountboo = false;

        internal string visf2xml(string VisPath, string filename, string FilePath)
        {
            string text = "";
            FileStream stream = File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader streamReader = new StreamReader(stream, Encoding.Default);
            int num = 0;
            while (streamReader.Peek() != -1)
            {
                string text2 = streamReader.ReadLine();
                switch (num)
                {
                    case 0:
                        text += text2;
                        break;
                    case 1:
                        if (Regex.IsMatch(text2, "^(\\$T|\\$=S|\\$%|\\$\\d|\\$D|\\$M|\\$L)"))
                        {
                            text = text + "\r\n" + text2;
                        }
                        else
                        {
                            text = text + " " + text2;
                        }
                        break;
                    case 2:
                        text = text + "\r\n" + text2;
                        break;
                }
                if (Regex.IsMatch(text2, "^(\\$T|\\$=S|\\$%)"))
                {
                    num = 1;
                }
                else if (Regex.IsMatch(text2, "^(\\$\\d)"))
                {
                    num = 2;
                }
            }
            streamReader.Close();

            this.tempdata = text;
            text = this.EntitiesConv(text);
            text = this.Formatcontrol(text);
            Match match = Regex.Match(text, "(\\$\\d+\\:)(.*?)(\\$\\d+\\:|$)", RegexOptions.Singleline);
            string text3 = "";
            checked
            {
                while (match.Success)
                {
                    int num2 = 0;
                    num2++;
                    text3 = string.Concat(new string[]
                    {
                text3,
                "<",
                match.Groups[1].ToString(),
                Strings.Replace(match.Groups[2].ToString(), "\r\n", "", 1, -1, CompareMethod.Binary),
                match.Groups[1].ToString(),
                ">\r\n"
                    });
                    string text4 = match.Groups[2].ToString();
                    string str = Strings.Replace(match.Groups[1].ToString(), "$", "<SPiCON_Dollar>", 1, -1, CompareMethod.Binary);
                    string left = match.Groups[1].ToString();
                    if (Operators.CompareString(left, "$00:", false) == 0)
                    {
                        text4 = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>\r\n<COURTCASE>\r\n<lndocmeta:docinfo>\r\n<lndocmeta:lnlni lnlni=\"\"/>\r\n<lndocmeta:lnminrev lnminrev=\"00000\"/>\r\n<lndocmeta:smi lnsmi=\"\"/>\r\n<lndocmeta:dpsi lndpsi=\"\"/>\r\n<lndocmeta:lnsourcedocid lnsourcedocid=\"\"/>\r\n<lndocmeta:lndoctype lndoctypename=\"COURTCASE\"/>\r\n<lndocmeta:lndoctypeversion lndoctypeversionmajor=\"06\" lndoctypeversionminor=\"000\"/>\r\n<lndocmeta:lndoctypelang lndoctypelang=\"EN\"/>\r\n<lndocmeta:lnfilenum lnfilenum=\"\"/>\r\n<lndocmeta:fabinfo><lndocmeta:fabinfoitem name=\"B4DBNO\" value=\"\"/></lndocmeta:fabinfo>\r\n</lndocmeta:docinfo>\r\n";
                    }
                    else if (Operators.CompareString(left, "$10:", false) == 0)
                    {
                        text4 = Regex.Replace(text4, "\r\n", " ");
                        if (Regex.IsMatch(text4, "<p><lnvxe:text>(.*?)</lnvxe:fnbody>"))
                        {
                            text4 = Regex.Replace(text4, "<p><lnvxe:text>(.*?)</lnvxe:fnbody>", "<p><lnvxe:text>$1</lnvxe:text></p></lnvxe:fnbody>");
                        }
                        bool flag = false;
                        if (Regex.IsMatch(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>"))
                        {
                            flag = true;
                        }
                        Match match2 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>");
                        while (match2.Success)
                        {
                            string text5 = match2.Groups[0].ToString();
                            text5 = Regex.Replace(text5, "<lnvxe:footnotegrp>", "</lnvxe:fullcasename>\r\n<lnvxe:footnotegrp>\r\n");
                            text5 = Regex.Replace(text5, "<lnvxe:fnlabel alt-content(.*?)</lnvxe:fnlabel>", "\r\n<lnvxe:fnlabel alt-content$1</lnvxe:fnlabel>\r\n");
                            text5 = Regex.Replace(text5, "> <", "><");
                            text5 = Regex.Replace(text5, " <", "<");
                            text4 = Strings.Replace(text4, match2.Groups[0].ToString(), text5, 1, 1, CompareMethod.Binary);
                            match2 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>");
                        }
                        if (flag)
                        {
                            text4 = "<lnv:FULL-NAME>\r\n<lnvxe:fullcasename>" + text4 + "\r\n</lnv:FULL-NAME>\r\n";
                        }
                        else
                        {
                            text4 = "<lnv:FULL-NAME>\r\n<lnvxe:fullcasename>" + text4 + "</lnvxe:fullcasename>\r\n</lnv:FULL-NAME>\r\n";
                        }
                    }
                    else if (Operators.CompareString(left, "$15:", false) == 0)
                    {
                        text4 = Strings.Replace(text4, "\r\n", "", 1, -1, CompareMethod.Binary);
                        text4 = "<lnv:SHORT-NAME>\r\n<lnvxe:shortcasename>" + text4 + "</lnvxe:shortcasename>\r\n</lnv:SHORT-NAME>\r\n";
                    }
                    else if (Operators.CompareString(left, "$20:", false) == 0)
                    {
                        text4 = Strings.Replace(text4, "\r\n", "", 1, -1, CompareMethod.Binary);
                        bool flag2 = false;
                        if (Regex.IsMatch(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>"))
                        {
                            flag2 = true;
                        }
                        Match match3 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>");
                        while (match3.Success)
                        {
                            string text6 = match3.Groups[0].ToString();
                            text6 = Regex.Replace(text6, "<lnvxe:footnotegrp>", "</lnvxe:docketnumber>\r\n<lnvxe:footnotegrp>\r\n");
                            text6 = Regex.Replace(text6, "<lnvxe:fnlabel alt-content(.*?)</lnvxe:fnlabel>", "\r\n<lnvxe:fnlabel alt-content$1</lnvxe:fnlabel>\r\n");
                            text6 = Regex.Replace(text6, "> <", "><");
                            text6 = Regex.Replace(text6, " <", "<");
                            text4 = Strings.Replace(text4, match3.Groups[0].ToString(), text6, 1, 1, CompareMethod.Binary);
                            match3 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>");
                        }
                        if (flag2)
                        {
                            text4 = "<lnv:NUMBER>\r\n<lnvxe:docketnumber>" + text4 + "\r\n</lnv:NUMBER>\r\n";
                        }
                        else
                        {
                            text4 = "<lnv:NUMBER>\r\n<lnvxe:docketnumber>" + text4 + "</lnvxe:docketnumber>\r\n</lnv:NUMBER>\r\n";
                        }
                    }
                    else if (Operators.CompareString(left, "$25:", false) == 0)
                    {
                        text4 = Strings.Replace(text4, "\r\n", "", 1, -1, CompareMethod.Binary);
                        bool flag3 = false;
                        if (Regex.IsMatch(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>"))
                        {
                            flag3 = true;
                        }
                        Match match4 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>");
                        while (match4.Success)
                        {
                            string text7 = match4.Groups[0].ToString();
                            text7 = Regex.Replace(text7, "<lnvxe:footnotegrp>", "\r\n<lnvxe:footnotegrp>\r\n");
                            text7 = Regex.Replace(text7, "<lnvxe:fnlabel alt-content(.*?)</lnvxe:fnlabel>", "\r\n<lnvxe:fnlabel alt-content$1</lnvxe:fnlabel>\r\n");
                            text7 = Regex.Replace(text7, "> <", "><");
                            text7 = Regex.Replace(text7, " <", "<");
                            text4 = Strings.Replace(text4, match4.Groups[0].ToString(), text7, 1, 1, CompareMethod.Binary);
                            match4 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>");
                        }
                        if (flag3)
                        {
                            text4 = "<lnv:COURT>" + text4 + "</lnv:COURT>\r\n";
                        }
                        else
                        {
                            text4 = "<lnv:COURT>" + text4 + "</lnv:COURT>\r\n";
                        }
                    }
                    else if (Operators.CompareString(left, "$29:", false) == 0)
                    {
                        text4 = Strings.Replace(text4, "\r\n", "", 1, -1, CompareMethod.Binary);
                        text4 = "<lnv:COURT-CITE-OFF>\r\n<p><lnvxe:text>" + text4 + "</lnvxe:text></p>\r\n</lnv:COURT-CITE-OFF>\r\n";
                    }
                    else if (Operators.CompareString(left, "$30:", false) == 0)
                    {
                        text4 = Strings.Replace(text4, "\r\n", "", 1, -1, CompareMethod.Binary);
                        text4 = Strings.Replace(text4, "; ", "</cite4thisdoc>\r\n<cite4thisdoc>", 1, -1, CompareMethod.Binary);
                        text4 = "<lnv:CITE>\r\n<cite4thisdoc>" + text4 + "</cite4thisdoc>\r\n</lnv:CITE>\r\n";
                    }
                    else if (Operators.CompareString(left, "$35:", false) == 0)
                    {
                        text4 = Strings.Replace(text4, "\r\n", "", 1, -1, CompareMethod.Binary);
                        text4 = Strings.Replace(text4, "$?$%", "", 1, -1, CompareMethod.Binary);
                        text4 = Regex.Replace(text4, "^((?:Jan|January|Feb|February|Mar|March|Apr|April|May|Jun|June|Jul|July|Aug|August|Sep|September|Oct|October|Nov|November|Dec|December) [0-9]{1,2},? [0-9]{4})\\b(.*?)$", "<lnvxe:date>$1</lnvxe:date>$2");
                        bool flag4 = false;
                        if (Regex.IsMatch(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>"))
                        {
                            flag4 = true;
                        }
                        Match match5 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>");
                        while (match5.Success)
                        {
                            string text8 = match5.Groups[0].ToString();
                            text8 = Regex.Replace(text8, "<lnvxe:footnotegrp>", "\r\n<lnvxe:footnotegrp>\r\n");
                            text8 = Regex.Replace(text8, "<lnvxe:fnlabel alt-content(.*?)</lnvxe:fnlabel>", "\r\n<lnvxe:fnlabel alt-content$1</lnvxe:fnlabel>\r\n");
                            text8 = Regex.Replace(text8, "> <", "><");
                            text8 = Regex.Replace(text8, " <", "<");
                            text4 = Strings.Replace(text4, match5.Groups[0].ToString(), text8, 1, 1, CompareMethod.Binary);
                            match5 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>");
                        }
                        if (flag4)
                        {
                            text4 = "<lnv:ARGUEDDATE>\r\n" + text4 + "\r\n</lnv:ARGUEDDATE>\r\n";
                        }
                        else
                        {
                            text4 = "<lnv:ARGUEDDATE>\r\n" + text4 + "\r\n</lnv:ARGUEDDATE>\r\n";
                        }
                    }
                    else if (Operators.CompareString(left, "$40:", false) == 0)
                    {
                        text4 = Strings.Replace(text4, "\r\n", "", 1, -1, CompareMethod.Binary);
                        text4 = Strings.Replace(text4, "$?$%", "", 1, -1, CompareMethod.Binary);
                        text4 = Regex.Replace(text4, "^((?:Jan|January|Feb|February|Mar|March|Apr|April|May|Jun|June|Jul|July|Aug|August|Sep|September|Oct|October|Nov|November|Dec|December) [0-9]{1,2},? [0-9]{4})\\b(.*?)$", "<lnvxe:date>$1</lnvxe:date>$2");
                        bool flag5 = false;
                        if (Regex.IsMatch(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>"))
                        {
                            flag5 = true;
                        }
                        Match match6 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>");
                        while (match6.Success)
                        {
                            string text9 = match6.Groups[0].ToString();
                            text9 = Regex.Replace(text9, "<lnvxe:footnotegrp>", "\r\n<lnvxe:footnotegrp>\r\n");
                            text9 = Regex.Replace(text9, "<lnvxe:fnlabel alt-content(.*?)</lnvxe:fnlabel>", "\r\n<lnvxe:fnlabel alt-content$1</lnvxe:fnlabel>\r\n");
                            text9 = Regex.Replace(text9, "> <", "><");
                            text9 = Regex.Replace(text9, " <", "<");
                            text4 = Strings.Replace(text4, match6.Groups[0].ToString(), text9, 1, 1, CompareMethod.Binary);
                            match6 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>");
                        }
                        if (flag5)
                        {
                            text4 = "<lnv:DECIDEDDATE>\r\n" + text4 + "\r\n</lnv:DECIDEDDATE>\r\n";
                        }
                        else
                        {
                            text4 = "<lnv:DECIDEDDATE>\r\n" + text4 + "\r\n</lnv:DECIDEDDATE>\r\n";
                        }
                    }
                    else if (Operators.CompareString(left, "$45:", false) == 0)
                    {
                        text4 = Strings.Replace(text4, "\r\n", "", 1, -1, CompareMethod.Binary);
                        text4 = Strings.Replace(text4, "$?$%", "", 1, -1, CompareMethod.Binary);
                        text4 = Regex.Replace(text4, "^((?:Jan|January|Feb|February|Mar|March|Apr|April|May|Jun|June|Jul|July|Aug|August|Sep|September|Oct|October|Nov|November|Dec|December) [0-9]{1,2},? [0-9]{4})\\b(.*?)$", "<lnvxe:date>$1</lnvxe:date>$2");
                        bool flag6 = false;
                        if (Regex.IsMatch(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>"))
                        {
                            flag6 = true;
                        }
                        Match match7 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>");
                        while (match7.Success)
                        {
                            string text10 = match7.Groups[0].ToString();
                            text10 = Regex.Replace(text10, "<lnvxe:footnotegrp>", "\r\n<lnvxe:footnotegrp>\r\n");
                            text10 = Regex.Replace(text10, "<lnvxe:fnlabel alt-content(.*?)</lnvxe:fnlabel>", "\r\n<lnvxe:fnlabel alt-content$1</lnvxe:fnlabel>\r\n");
                            text10 = Regex.Replace(text10, "> <", "><");
                            text10 = Regex.Replace(text10, " <", "<");
                            text4 = Strings.Replace(text4, match7.Groups[0].ToString(), text10, 1, 1, CompareMethod.Binary);
                            match7 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>");
                        }
                        if (flag6)
                        {
                            text4 = "<lnv:FILEDDATE>\r\n" + text4 + "\r\n</lnv:FILEDDATE>\r\n";
                        }
                        else
                        {
                            text4 = "<lnv:FILEDDATE>\r\n" + text4 + "\r\n</lnv:FILEDDATE>\r\n";
                        }
                    }
                    else if (Operators.CompareString(left, "$55:", false) == 0)
                    {
                        text4 = Strings.Replace(text4, "\r\n", "", 1, -1, CompareMethod.Binary);
                        bool flag7 = false;
                        if (Regex.IsMatch(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>"))
                        {
                            flag7 = true;
                        }
                        Match match8 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>");
                        while (match8.Success)
                        {
                            string text11 = match8.Groups[0].ToString();
                            text11 = Regex.Replace(text11, "<lnvxe:footnotegrp>", "\r\n<lnvxe:footnotegrp>\r\n");
                            text11 = Regex.Replace(text11, "<lnvxe:fnlabel alt-content(.*?)</lnvxe:fnlabel>", "\r\n<lnvxe:fnlabel alt-content$1</lnvxe:fnlabel>\r\n");
                            text11 = Regex.Replace(text11, "> <", "><");
                            text11 = Regex.Replace(text11, " <", "<");
                            text4 = Strings.Replace(text4, match8.Groups[0].ToString(), text11, 1, 1, CompareMethod.Binary);
                            match8 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>");
                        }
                        if (flag7)
                        {
                            text4 = "<lnv:DOC-STATUS>\r\n<p><lnvxe:text>" + text4 + "</lnvxe:text></p>\r\n</lnv:DOC-STATUS>\r\n";
                            text4 = Strings.Replace(text4, "</lnvxe:text></p></lnvxe:footnotegrp></lnvxe:text></p>", "</lnvxe:text></p></lnvxe:footnotegrp>", 1, -1, CompareMethod.Binary);
                        }
                        else
                        {
                            text4 = "<lnv:DOC-STATUS>\r\n<p><lnvxe:text>" + text4 + "</lnvxe:text></p>\r\n</lnv:DOC-STATUS>\r\n";
                        }
                    }
                    else if (Operators.CompareString(left, "$60:", false) == 0)
                    {
                        text4 = Strings.Replace(text4, "\r\n", "", 1, -1, CompareMethod.Binary);
                        text4 = Regex.Replace(text4, "^\\$\\%\\$\\%", "", RegexOptions.IgnoreCase);
                        bool flag8 = false;
                        if (Regex.IsMatch(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>"))
                        {
                            flag8 = true;
                        }
                        Match match9 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>");
                        while (match9.Success)
                        {
                            string text12 = match9.Groups[0].ToString();
                            text12 = Regex.Replace(text12, "<lnvxe:footnotegrp>", "\r\n<lnvxe:footnotegrp>\r\n");
                            text12 = Regex.Replace(text12, "<lnvxe:fnlabel alt-content(.*?)</lnvxe:fnlabel>", "\r\n<lnvxe:fnlabel alt-content$1</lnvxe:fnlabel>\r\n");
                            text12 = Regex.Replace(text12, "> <", "><");
                            text12 = Regex.Replace(text12, " <", "<");
                            text4 = Strings.Replace(text4, match9.Groups[0].ToString(), text12, 1, 1, CompareMethod.Binary);
                            match9 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>");
                        }
                        if (flag8)
                        {
                            text4 = "<lnv:PUB-STATUS>\r\n<p><lnvxe:text>" + text4 + "</lnvxe:text></p>\r\n</lnv:PUB-STATUS>\r\n";
                            text4 = Strings.Replace(text4, "</lnvxe:text></p></lnvxe:footnotegrp></lnvxe:text></p>", "</lnvxe:text></p></lnvxe:footnotegrp>", 1, -1, CompareMethod.Binary);
                        }
                        else
                        {
                            text4 = "<lnv:PUB-STATUS>\r\n<p><lnvxe:text>" + text4 + "</lnvxe:text></p>\r\n</lnv:PUB-STATUS>\r\n";
                        }
                    }
                    else if (Operators.CompareString(left, "$75:", false) == 0)
                    {
                        text4 = Strings.Replace(text4, "\r\n", "", 1, -1, CompareMethod.Binary);
                        text4 = "<lnv:SUBSEQ-HISTORY>\r\n<lnvxe:subseqhistory>\r\n<p><lnvxe:text>" + text4 + "</lnvxe:text></p>\r\n</lnvxe:subseqhistory>\r\n</lnv:SUBSEQ-HISTORY>\r\n";
                    }
                    else if (Operators.CompareString(left, "$80:", false) == 0)
                    {
                        text4 = Strings.Replace(text4, "\r\n", "", 1, -1, CompareMethod.Binary);
                        bool flag9 = false;
                        if (Regex.IsMatch(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>"))
                        {
                            flag9 = true;
                        }
                        Match match10 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>");
                        while (match10.Success)
                        {
                            string text13 = match10.Groups[0].ToString();
                            text13 = Regex.Replace(text13, "<lnvxe:footnotegrp>", "</lnvxe:priorhistory>\r\n<lnvxe:footnotegrp>\r\n");
                            text13 = Regex.Replace(text13, "<lnvxe:fnlabel alt-content(.*?)</lnvxe:fnlabel>", "\r\n<lnvxe:fnlabel alt-content$1</lnvxe:fnlabel>\r\n");
                            text13 = Regex.Replace(text13, "> <", "><");
                            text13 = Regex.Replace(text13, " <", "<");
                            text4 = Strings.Replace(text4, match10.Groups[0].ToString(), text13, 1, 1, CompareMethod.Binary);
                            match10 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>");
                        }
                        if (flag9)
                        {
                            text4 = "<lnv:PRIOR-HISTORY>\r\n<lnvxe:priorhistory>\r\n<p><lnvxe:text>" + text4 + "</lnvxe:text></p>\r\n</lnv:PRIOR-HISTORY>\r\n";
                            text4 = Strings.Replace(text4, "</lnvxe:text></p></lnvxe:footnotegrp></lnvxe:text></p>", "</lnvxe:text></p></lnvxe:footnotegrp>", 1, -1, CompareMethod.Binary);
                            text4 = Strings.Replace(text4, "</lnvxe:fnbody></lnvxe:footnote></lnvxe:text></p></lnvxe:footnotegrp>", "</lnvxe:text></p></lnvxe:fnbody></lnvxe:footnote></lnvxe:footnotegrp>", 1, -1, CompareMethod.Binary);
                        }
                        else
                        {
                            text4 = "<lnv:PRIOR-HISTORY>\r\n<lnvxe:priorhistory>\r\n<p><lnvxe:text>" + text4 + "</lnvxe:text></p>\r\n</lnvxe:priorhistory>\r\n</lnv:PRIOR-HISTORY>\r\n";
                        }
                    }
                    else if (Operators.CompareString(left, "$90:", false) == 0)
                    {
                        text4 = Strings.Replace(text4, "\r\n", "", 1, -1, CompareMethod.Binary);
                        bool flag10 = false;
                        if (Regex.IsMatch(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>"))
                        {
                            flag10 = true;
                        }
                        Match match11 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>");
                        while (match11.Success)
                        {
                            string text14 = match11.Groups[0].ToString();
                            text14 = Regex.Replace(text14, "<lnvxe:footnotegrp>", "\r\n<lnvxe:footnotegrp>\r\n");
                            text14 = Regex.Replace(text14, "<lnvxe:fnlabel alt-content(.*?)</lnvxe:fnlabel>", "\r\n<lnvxe:fnlabel alt-content$1</lnvxe:fnlabel>\r\n");
                            text14 = Regex.Replace(text14, "> <", "><");
                            text14 = Regex.Replace(text14, " <", "<");
                            text4 = Strings.Replace(text4, match11.Groups[0].ToString(), text14, 1, 1, CompareMethod.Binary);
                            match11 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>");
                        }
                        if (flag10)
                        {
                            text4 = "<lnv:DISPOSITION-1>\r\n<p><lnvxe:text>" + text4 + "</lnvxe:text></p>\r\n</lnv:DISPOSITION-1>\r\n";
                            text4 = Strings.Replace(text4, "</lnvxe:text></p></lnvxe:footnotegrp></lnvxe:text></p>", "</lnvxe:text></p></lnvxe:footnotegrp>", 1, -1, CompareMethod.Binary);
                        }
                        else
                        {
                            text4 = "<lnv:DISPOSITION-1>\r\n<p><lnvxe:text>" + text4 + "</lnvxe:text></p>\r\n</lnv:DISPOSITION-1>\r\n";
                        }
                    }
                    // $92
                    else if (Operators.CompareString(left, "$92:", false) == 0)
                    {
                        text4 = Strings.Replace(text4, "\r\n$%$%", "</lnvxe:text></p>\r\n<p><lnvxe:text>", 1, 1, CompareMethod.Binary);
                        text4 = Strings.Replace(text4, "\r\n$T", "</lnvxe:text></p>\r\n<p i=\"3\"><lnvxe:text>", 1, 1, CompareMethod.Binary);
                        text4 = Strings.Replace(text4, "$%$%", "", 1, 1, CompareMethod.Binary);
                        bool flag12 = false;
                        if (Regex.IsMatch(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>", RegexOptions.Singleline))
                        {
                            flag12 = true;
                        }
                        Match match13 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>", RegexOptions.Singleline);
                        while (match13.Success)
                        {
                            string text16 = match13.Groups[0].ToString();
                            text16 = Strings.Replace(text16, "</lnvxe:fnbody></lnvxe:footnote></lnvxe:text></p>", "</lnvxe:text></p></lnvxe:fnbody></lnvxe:footnote>", 1, -1, CompareMethod.Binary);
                            text4 = Strings.Replace(text4, match13.Groups[0].ToString(), text16, 1, 1, CompareMethod.Binary);
                            match13 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>");
                        }
                        if (flag12)
                        {
                            text4 = "<lnv:SUMMARY>\r\n<p i=\"3\"><lnvxe:text>" + text4 + "\r\n</lnv:SUMMARY>\r\n";
                            text4 = Strings.Replace(text4, "\r\n\r\n", "\r\n", 1, -1, CompareMethod.Binary);
                            text4 = Strings.Replace(text4, "</lnvxe:text></p>\r\n</lnvxe:footnotegrp>\r\n</lnvxe:text></p>", "</lnvxe:text></p></lnvxe:footnotegrp>", 1, -1, CompareMethod.Binary);
                        }
                        else
                        {
                            text4 = "<lnv:SUMMARY>\r\n<p i=\"3\"><lnvxe:text>" + text4 + "</lnvxe:text></p>\r\n</lnv:SUMMARY>\r\n";
                        }
                        text4 = Strings.Replace(text4, "\r\n<p><lnvxe:text></lnvxe:text></p>", "", 1, -1, CompareMethod.Binary);
                    }
                    //
                    else if (Operators.CompareString(left, "$95:", false) == 0)
                    {
                        text4 = Strings.Replace(text4, "$%$%", "", 1, -1, CompareMethod.Binary);
                        bool flag11 = false;
                        if (Regex.IsMatch(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>", RegexOptions.Singleline))
                        {
                            flag11 = true;
                        }
                        Match match12 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>", RegexOptions.Singleline);
                        while (match12.Success)
                        {
                            string text15 = match12.Groups[0].ToString();
                            text15 = Regex.Replace(text15, "<lnvxe:footnotegrp>", "</lnvxe:headnote-simple>\r\n<lnvxe:footnotegrp>");
                            text15 = Strings.Replace(text15, "</lnvxe:fnbody></lnvxe:footnote></lnvxe:text></p>", "</lnvxe:text></p></lnvxe:fnbody></lnvxe:footnote>", 1, -1, CompareMethod.Binary);
                            text15 = Regex.Replace(text15, " <", "<");
                            text4 = Strings.Replace(text4, match12.Groups[0].ToString(), text15, 1, 1, CompareMethod.Binary);
                            match12 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>");
                        }
                        if (flag11)
                        {
                            text4 = "<lnv:HEADNOTES-1>\r\n<lnvxe:headnote-simple>\r\n<p><lnvxe:text>" + text4 + "</lnvxe:text></p>\r\n</lnv:HEADNOTES-1>\r\n";
                            text4 = Strings.Replace(text4, "</lnvxe:footnotegrp>\r\n</lnvxe:text></p>", "</lnvxe:footnotegrp>", 1, -1, CompareMethod.Binary);
                            text4 = Regex.Replace(text4, "<p><lnvxe:text>(.*?)<p><lnvxe:text>", "<p><lnvxe:text>$1</lnvxe:text></p>\r\n<p><lnvxe:text>", RegexOptions.Singleline);
                            text4 = Regex.Replace(text4, "\r\n</lnvxe:text></p>", "</lnvxe:text></p>");
                        }
                        else
                        {
                            text4 = "<lnv:HEADNOTES-1>\r\n<lnvxe:headnote-simple>\r\n<p><lnvxe:text>" + text4 + "</lnvxe:text></p>\r\n</lnvxe:headnote-simple>\r\n</lnv:HEADNOTES-1>\r\n";
                            text4 = Regex.Replace(text4, "<p><lnvxe:text>(.*?)<p><lnvxe:text>", "<p><lnvxe:text>$1</lnvxe:text></p>\r\n<p><lnvxe:text>", RegexOptions.Singleline);
                            text4 = Regex.Replace(text4, "\r\n</lnvxe:text></p>", "</lnvxe:text></p>");
                            text4 = Strings.Replace(text4, "\r\n<p><lnvxe:text></lnvxe:text></p>", "", 1, -1, CompareMethod.Binary);
                        }
                    }
                    else if (Operators.CompareString(left, "$100:", false) == 0)
                    {
                        text4 = Strings.Replace(text4, "\r\n", "</lnvxe:text></p>\r\n<p><lnvxe:text>", 1, 1, CompareMethod.Binary);
                        text4 = Strings.Replace(text4, "$%$%", "", 1, 1, CompareMethod.Binary);
                        bool flag12 = false;
                        if (Regex.IsMatch(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>", RegexOptions.Singleline))
                        {
                            flag12 = true;
                        }
                        Match match13 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>", RegexOptions.Singleline);
                        while (match13.Success)
                        {
                            string text16 = match13.Groups[0].ToString();
                            text16 = Strings.Replace(text16, "</lnvxe:fnbody></lnvxe:footnote></lnvxe:text></p>", "</lnvxe:text></p></lnvxe:fnbody></lnvxe:footnote>", 1, -1, CompareMethod.Binary);
                            text4 = Strings.Replace(text4, match13.Groups[0].ToString(), text16, 1, 1, CompareMethod.Binary);
                            match13 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>");
                        }
                        if (flag12)
                        {
                            text4 = "<lnv:SYLLABUS-1>\r\n<p><lnvxe:text>" + text4 + "\r\n</lnv:SYLLABUS-1>\r\n";
                            text4 = Strings.Replace(text4, "\r\n\r\n", "\r\n", 1, -1, CompareMethod.Binary);
                            text4 = Strings.Replace(text4, "</lnvxe:text></p>\r\n</lnvxe:footnotegrp>\r\n</lnvxe:text></p>", "</lnvxe:text></p></lnvxe:footnotegrp>", 1, -1, CompareMethod.Binary);
                        }
                        else
                        {
                            text4 = "<lnv:SYLLABUS-1>\r\n<p><lnvxe:text>" + text4 + "</lnvxe:text></p>\r\n</lnv:SYLLABUS-1>\r\n";
                        }
                        text4 = Strings.Replace(text4, "\r\n<p><lnvxe:text></lnvxe:text></p>", "", 1, -1, CompareMethod.Binary);
                    }
                    else if (Operators.CompareString(left, "$105:", false) == 0)
                    {
                        if (Regex.IsMatch(text4, "\\,\r\n", RegexOptions.Singleline))
                        {
                            text4 = Regex.Replace(text4, "\\,\r\n", "<SPiCON_LINE_BRK>", RegexOptions.Singleline);
                            text4 = Strings.Replace(text4, "<SPiCON_LINE_BRK>", ", ", 1, -1, CompareMethod.Binary);
                            text4 = Strings.Replace(text4, "\r\n", "</lnvxe:counsel>\r\n<lnvxe:counsel>", 1, -1, CompareMethod.Binary);
                            text4 = Strings.Replace(text4, "$%$%", "", 1, -1, CompareMethod.Binary);
                            text4 = "<lnv:COUNSEL>\r\n<lnvxe:counsel>" + text4 + "</lnvxe:counsel>\r\n</lnv:COUNSEL>\r\n";
                            text4 = Strings.Replace(text4, "\r\n<lnvxe:counsel></lnvxe:counsel>", "", 1, -1, CompareMethod.Binary);
                        }
                        else
                        {
                            text4 = Strings.Replace(text4, "</lnvxe:footnotegrp>", "</lnvxe:footnotegrp>\r\n", 1, -1, CompareMethod.Binary);
                            text4 = Strings.Replace(text4, "\r\n", "</lnvxe:counsel>\r\n<lnvxe:counsel>", 1, -1, CompareMethod.Binary);
                            text4 = Strings.Replace(text4, "$%$%", "", 1, -1, CompareMethod.Binary);
                            text4 = "<lnv:COUNSEL>\r\n<lnvxe:counsel>" + text4 + "</lnvxe:counsel>\r\n</lnv:COUNSEL>\r\n";
                            text4 = Strings.Replace(text4, "\r\n<lnvxe:counsel></lnvxe:counsel>", "", 1, -1, CompareMethod.Binary);
                        }
                        bool flag13 = false;
                        if (Regex.IsMatch(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>", RegexOptions.Singleline))
                        {
                            flag13 = true;
                        }
                        Match match14 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>", RegexOptions.Singleline);
                        if (match14.Success)
                        {
                            string text17 = match14.Groups[0].ToString();
                            text17 = Strings.Replace(text17, "<lnvxe:counsel>", "", 1, -1, CompareMethod.Binary);
                            text17 = Strings.Replace(text17, "</lnvxe:counsel>", "", 1, -1, CompareMethod.Binary);
                            text17 = Strings.Replace(text17, "</lnvxe:fnbody></lnvxe:footnote></lnvxe:text></p>", "</lnvxe:text></p></lnvxe:fnbody></lnvxe:footnote>", 1, -1, CompareMethod.Binary);
                            text4 = Strings.Replace(text4, match14.Groups[0].ToString(), text17, 1, 1, CompareMethod.Binary);
                        }
                        text4 = Strings.Replace(text4, "<lnvxe:counsel><p><lnvxe:text>", "<lnvxe:counsel>", 1, -1, CompareMethod.Binary);
                        text4 = Strings.Replace(text4, "</lnvxe:text></p></lnvxe:counsel>", "</lnvxe:counsel>", 1, -1, CompareMethod.Binary);
                        if (flag13)
                        {
                            text4 = Strings.Replace(text4, "<lnvxe:counsel><lnvxe:footnotegrp>", "<lnvxe:footnotegrp>", 1, -1, CompareMethod.Binary);
                            text4 = Strings.Replace(text4, "</lnvxe:footnotegrp></lnvxe:counsel>", "</lnvxe:footnotegrp>", 1, -1, CompareMethod.Binary);
                            text4 = Strings.Replace(text4, "<lnvxe:fnbody></lnvxe:counsel>", "<lnvxe:fnbody>", 1, -1, CompareMethod.Binary);
                            text4 = Regex.Replace(text4, "> ", ">");
                            text4 = Regex.Replace(text4, " <", "<");
                        }
                    }
                    else if (Operators.CompareString(left, "$110:", false) == 0)
                    {
                        text4 = Strings.Replace(text4, "\r\n", "", 1, -1, CompareMethod.Binary);
                        bool flag14 = false;
                        if (Regex.IsMatch(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>"))
                        {
                            flag14 = true;
                        }
                        Match match15 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>");
                        while (match15.Success)
                        {
                            string text18 = match15.Groups[0].ToString();
                            text18 = Regex.Replace(text18, "<lnvxe:footnotegrp>", "</lnvxe:judges>\r\n<lnvxe:footnotegrp>\r\n");
                            text18 = Regex.Replace(text18, "<lnvxe:fnlabel alt-content(.*?)</lnvxe:fnlabel>", "\r\n<lnvxe:fnlabel alt-content$1</lnvxe:fnlabel>\r\n");
                            text18 = Strings.Replace(text18, "</lnvxe:fnbody></lnvxe:footnote></lnvxe:text></p>", "</lnvxe:text></p></lnvxe:fnbody></lnvxe:footnote>", 1, -1, CompareMethod.Binary);
                            text18 = Regex.Replace(text18, "> <", "><");
                            text18 = Regex.Replace(text18, " <", "<");
                            text4 = Strings.Replace(text4, match15.Groups[0].ToString(), text18, 1, 1, CompareMethod.Binary);
                            match15 = Regex.Match(text4, "<lnvxe:footnotegrp>(.*?)</lnvxe:footnotegrp>");
                        }
                        if (flag14)
                        {
                            text4 = "<lnv:JUDGES>\r\n<lnvxe:judges>" + text4 + "\r\n</lnv:JUDGES>\r\n";
                        }
                        else
                        {
                            text4 = "<lnv:JUDGES>\r\n<lnvxe:judges>" + text4 + "</lnvxe:judges>\r\n</lnv:JUDGES>\r\n";
                        }
                    }
                    else if (Operators.CompareString(left, "$115:", false) == 0)
                    {
                        text4 = Strings.Replace(text4, "\r\n", "", 1, -1, CompareMethod.Binary);
                        text4 = "<lnv:OPINIONBY>" + text4 + "</lnv:OPINIONBY>\r\n";
                    }
                    else if (Operators.CompareString(left, "$120:", false) == 0)
                    {
                        text4 = Regex.Replace(text4, "\\$%\\$%(.*?)\r\n", "<p><lnvxe:text>$1</lnvxe:text></p>\r\n");
                        text4 = Regex.Replace(text4, "\\$T(.*?)\r\n", "<p i=\"3\"><lnvxe:text>$1</lnvxe:text></p>\r\n");
                        text4 = Regex.Replace(text4, "\\$%\\$%(.*?)$", "<p><lnvxe:text>$1</lnvxe:text></p>");
                        text4 = Regex.Replace(text4, "\\$T(.*?)$", "<p i=\"3\"><lnvxe:text>$1</lnvxe:text></p>");
                        text4 = Regex.Replace(text4, "\\$%(.*?)\r\n", "<p nl=\"1\"><lnvxe:text>$1</lnvxe:text></p>\r\n");
                        text4 = Regex.Replace(text4, "\\$%(.*?)$", "<p nl=\"1\"><lnvxe:text>$1</lnvxe:text></p>");
                        text4 = Regex.Replace(text4, "\\$\\?", "&#x00A0;");
                        text4 = "<lnv:OPINION>\r\n" + text4 + "</lnv:OPINION>\r\n";
                    }
                    else if (Operators.CompareString(left, "$125:", false) == 0)
                    {
                        text4 = Strings.Replace(text4, "\r\n", "", 1, -1, CompareMethod.Binary);
                        text4 = "<lnv:CONCURBY>" + text4 + "</lnv:CONCURBY>\r\n";
                    }
                    else if (Operators.CompareString(left, "$130:", false) == 0)
                    {
                        text4 = Regex.Replace(text4, "\\$%\\$%(.*?)\r\n", "<p><lnvxe:text>$1</lnvxe:text></p>\r\n");
                        text4 = Regex.Replace(text4, "\\$T(.*?)\r\n", "<p i=\"3\"><lnvxe:text>$1</lnvxe:text></p>\r\n");
                        text4 = Regex.Replace(text4, "\\$%\\$%(.*?)$", "<p i=\"3\"><lnvxe:text>$1</lnvxe:text></p>");
                        text4 = Regex.Replace(text4, "\\$T(.*?)$", "<p i=\"3\"><lnvxe:text>$1</lnvxe:text></p>");
                        text4 = Regex.Replace(text4, "\\$%(.*?)\r\n", "<p nl=\"1\"><lnvxe:text>$1</lnvxe:text></p>\r\n");
                        text4 = Regex.Replace(text4, "\\$%(.*?)$", "<p nl=\"1\"><lnvxe:text>$1</lnvxe:text></p>");
                        text4 = "<lnv:CONCURS>\r\n<lnvxe:concur>\r\n" + text4 + "</lnvxe:concur>\r\n</lnv:CONCURS>\r\n";
                    }
                    else if (Operators.CompareString(left, "$135:", false) == 0)
                    {
                        text4 = Strings.Replace(text4, "\r\n", "", 1, -1, CompareMethod.Binary);
                        text4 = "<lnv:DISSENTBY>" + text4 + "</lnv:DISSENTBY>\r\n";
                    }
                    else if (Operators.CompareString(left, "$140:", false) == 0)
                    {
                        text4 = Regex.Replace(text4, "\\$%\\$%(.*?)\r\n", "<p><lnvxe:text>$1</lnvxe:text></p>\r\n");
                        text4 = Regex.Replace(text4, "\\$T(.*?)\r\n", "<p i=\"3\"><lnvxe:text>$1</lnvxe:text></p>\r\n");
                        text4 = Regex.Replace(text4, "\\$%\\$%(.*?)$", "<p i=\"3\"><lnvxe:text>$1</lnvxe:text></p>");
                        text4 = Regex.Replace(text4, "\\$T(.*?)$", "<p i=\"3\"><lnvxe:text>$1</lnvxe:text></p>");
                        text4 = Regex.Replace(text4, "\\$%(.*?)\r\n", "<p nl=\"1\"><lnvxe:text>$1</lnvxe:text></p>\r\n");
                        text4 = Regex.Replace(text4, "\\$%(.*?)$", "<p nl=\"1\"><lnvxe:text>$1</lnvxe:text></p>");
                        text4 = "<lnv:DISSENTS>\r\n<lnvxe:dissent>\r\n" + text4 + "</lnvxe:dissent>\r\n</lnv:DISSENTS>\r\n";
                    }
                    else if (Operators.CompareString(left, "$200:", false) == 0)
                    {
                        text4 = Strings.Replace(text4, "\r\n", "", 1, -1, CompareMethod.Binary);
                        text4 = Strings.Replace(text4, "$?", "", 1, -1, CompareMethod.Binary);
                        if (!filename.Contains("00000-00"))
                        {
                            text4 = "<lnv:SYS-AUDIT-TRAIL>" + text4 + "</lnv:SYS-AUDIT-TRAIL>\r\n";
                        }
                        else
                        {
                            text4 = "<lnv:SYS-AUDIT-TRAIL></lnv:SYS-AUDIT-TRAIL>\r\n";
                        }

                    }
                    else if (Operators.CompareString(left, "$210:", false) == 0)
                    {
                        text4 = Strings.Replace(text4, "\r\n", "", 1, -1, CompareMethod.Binary);
                        text4 = "<lnv:SYS-AUDIT-2>" + text4 + "</lnv:SYS-AUDIT-2>\r\n";
                    }
                    else if (Operators.CompareString(left, "$220:", false) == 0)
                    {
                        text4 = Strings.Replace(text4, "\r\n", "", 1, -1, CompareMethod.Binary);
                        text4 = "<lnv:SYS-FILE-CODE>" + text4 + "</lnv:SYS-FILE-CODE>\r\n";
                    }
                    else if (Operators.CompareString(left, "$243:", false) == 0)
                    {
                        text4 = Strings.Replace(text4, "\r\n", "", 1, -1, CompareMethod.Binary);
                        if (Regex.IsMatch(text4, "^#BPOCLTAG#$"))
                        {
                            text4 = Strings.Replace(text4, "#BPOCLTAG#", "", 1, -1, CompareMethod.Binary);
                        }
                        else
                        {
                            text4 = Strings.Replace(text4, "#BPOCLTAG#", "", 1, -1, CompareMethod.Binary);
                            text4 = Strings.Replace(text4, "$?", "", 1, -1, CompareMethod.Binary);
                            if (text.Contains(" missing") || text.Contains(" Missing") || text.Contains(" MISSING"))
                            {
                                text4 = "<lnv:SYS-PROD-INFO>" + text4 + " #OKILLEGIBLE#</lnv:SYS-PROD-INFO>\r\n";
                                text4 = Regex.Replace(text4, "\\s<", "<");
                            }
                            else
                            {
                                text4 = "<lnv:SYS-PROD-INFO>" + text4 + "</lnv:SYS-PROD-INFO>\r\n";
                                text4 = Regex.Replace(text4, "\\s<", "<");
                            }
                        }
                    }
                    else
                    {
                        text4 = str + text4;
                    }
                    text = Strings.Replace(text, match.Groups[1].ToString() + match.Groups[2].ToString(), text4, 1, 1, CompareMethod.Binary);
                    match = Regex.Match(text, "(\\$\\d+\\:)(.*?)(\\$\\d+\\:|$)", RegexOptions.Singleline);
                }
                text = Strings.Replace(text, "<SPiCON_Dollar>", "$", 1, -1, CompareMethod.Binary);
                text = Strings.Replace(text, "<spicon_enter/>", "\r\n", 1, -1, CompareMethod.Binary);
                text = Regex.Replace(text, "\\s<\\/lnvxe:fullcasename>", "</lnvxe:fullcasename>");
                if (Regex.IsMatch(text, "<lnvxe:text>(n[0-9]+(\\,)?(\\;)?(\\.)?(\\))?)(\\s)"))
                {
                    text = Regex.Replace(text, "<lnvxe:text>(n[0-9]+(\\,)?(\\;)?(\\.)?(\\))?)(\\s)", "<lnvxe:text>");
                }
                MatchCollection matchCollection = Regex.Matches(text, "<lnvxe:fnbody>(.*?)</lnvxe:fnbody>", RegexOptions.Singleline);
                int num3 = 0;
                int num4 = matchCollection.Count - 1;
                for (int i = num3; i <= num4; i++)
                {
                    string input = matchCollection[i].Groups[1].ToString();
                    MatchCollection matchCollection2 = Regex.Matches(input, "(<p i=\"3\"><lnvxe:text>(.*?))\r\n", RegexOptions.Singleline);
                    int num5 = 0;
                    int num6 = matchCollection2.Count - 1;
                    for (int j = num5; j <= num6; j++)
                    {
                        string str2 = matchCollection2[j].Groups[1].ToString();
                        string text19 = matchCollection2[j].Groups[2].ToString();
                        if (!Regex.IsMatch(text19, "</lnvxe:text></p>$"))
                        {
                            text19 = str2 + "</lnvxe:text></p>\r\n";
                            text = Strings.Replace(text, matchCollection2[j].Groups[0].ToString(), text19, 1, 1, CompareMethod.Binary);
                        }
                    }
                }
                text = Regex.Replace(text, "<p><lnvxe:text>(.*?)\r\n", "<p><lnvxe:text>$1</lnvxe:text></p>\r\n", RegexOptions.Singleline);
                text = Regex.Replace(text, "<p><lnvxe:text><p><lnvxe:text>", "<p><lnvxe:text>", RegexOptions.Singleline);
                text = Regex.Replace(text, "</lnvxe:text></p></lnvxe:text></p>", "</lnvxe:text></p>", RegexOptions.Singleline);
                text = Regex.Replace(text, "</lnvxe:text></p>\r</lnvxe:text></p>", "</lnvxe:text></p>", RegexOptions.Singleline);
                text = Regex.Replace(text, "</lnvxe:text></p>\n</lnvxe:text></p>", "</lnvxe:text></p>", RegexOptions.Singleline);
                text = Regex.Replace(text, "</lnvxe:text></p>\r\n</lnvxe:text></p>", "</lnvxe:text></p>", RegexOptions.Singleline);
                text = Regex.Replace(text, "</lnvxe:text></p></lnvxe:text></p>\r\n", "</lnvxe:text></p>\r\n", RegexOptions.Singleline);
                text = Regex.Replace(text, "</lnvxe:footnotegrp> \r\n<lnvxe:footnotegrp>\r\n", "", RegexOptions.Singleline);
                text = Regex.Replace(text, "</lnvxe:fnbody></lnvxe:footnote></lnvxe:text></p>\r\n", "</lnvxe:text></p>\r\n</lnvxe:fnbody></lnvxe:footnote>\r\n", RegexOptions.Singleline);
                text = Regex.Replace(text, "</lnvxe:blockquote></lnvxe:text></p>", "</lnvxe:text></p></lnvxe:blockquote>", RegexOptions.Singleline);
                text = Regex.Replace(text, "</lnvxe:text></p></lnvxe:footnotegrp>(\\s*)?</lnvxe:text></p>", "</lnvxe:footnotegrp>", RegexOptions.Singleline);
                text = Regex.Replace(text, "<emph typestyle=\"dblSPIHYPHENun\">", "<emph typestyle=\"dbl-un\">", RegexOptions.Singleline);
                text = Regex.Replace(text, " </lnvxe:text></p>", "</lnvxe:text></p>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                text = Regex.Replace(text, " <lnvxe:fnr", "<lnvxe:fnr", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                text = Regex.Replace(text, "</lnvxe:fnr>(\r\n?|\n)<lnvxe:blockquote>", "</lnvxe:fnr></lnvxe:text></p>\r\n<lnvxe:blockquote>", RegexOptions.None);
                text = Regex.Replace(text, "(\r\n?|\n)</lnvxe:text></p>", "</lnvxe:text></p>", RegexOptions.None);
                text = Regex.Replace(text, "<lnvxe:blockquote><p i=\"3\"><lnvxe:text>", "<lnvxe:blockquote>\r\n<p i=\"3\"><lnvxe:text>", RegexOptions.None);
                text = Regex.Replace(text, "</lnvxe:text></p></lnvxe:blockquote>", "</lnvxe:text></p>\r\n</lnvxe:blockquote>", RegexOptions.None);
                text = Regex.Replace(text, "</lnvxe:text></p>\r\n<lnvxe:blockquote>", "</lnvxe:text>\r\n<lnvxe:blockquote>", RegexOptions.None);
                text = Regex.Replace(text, "</lnvxe:blockquote>\r\n<p><lnvxe:text>", "</lnvxe:blockquote>\r\n<lnvxe:text>", RegexOptions.None);
                text = Regex.Replace(text, "</lnvxe:blockquote>\r\n<p i=\"3\"><lnvxe:text>", "</lnvxe:blockquote></p>\r\n<p i=\"3\"><lnvxe:text>", RegexOptions.None);
                text = Regex.Replace(text, "</lnvxe:fnr><emph typestyle=", "</lnvxe:fnr> <emph typestyle=", RegexOptions.None);
                text = Regex.Replace(text, "</lnvxe:footnotegrp><lnvxe:h>", "</lnvxe:footnotegrp>\r\n<lnvxe:h>", RegexOptions.None);
                text = Regex.Replace(text, "</lnvxe:text></p></lnvxe:text></p>\r\n</lnvxe:blockquote>", "</lnvxe:text></p>\r\n</lnvxe:blockquote></p>", RegexOptions.None);
                text = Regex.Replace(text, "</lnvxe:fnr> </lnvxe:text></p>", "</lnvxe:fnr></lnvxe:text></p>", RegexOptions.None);
                text = Regex.Replace(text, "</lnvxe:blockquote></lnvxe:text></p>", "</lnvxe:blockquote></p>", RegexOptions.None);
                text = Regex.Replace(text, "</lnvxe:footnotegrp>\r\n<lnvxe:footnotegrp>", "", RegexOptions.None);
                //</lnvxe:fnr> <emph typestyle="it">
                //<lnvxe:blockquote><p i="3"><lnvxe:text> </lnvxe:text></p></lnvxe:blockquote>
                text = text.Replace("</lnv:FULL-NAME>" + Environment.NewLine + "<lnv:FULL-NAME>", "");
                text = text.Replace("</lnv:NUMBER>" + Environment.NewLine + "<lnv:NUMBER>", "<lnvxe:connector>,</lnvxe:connector>");
                text = text.Replace("</lnvxe:footnotegrp>" + Environment.NewLine + "<lnvxe:footnotegrp>", "");
                text = text.Replace("</lnvxe:footnotegrp>" + Environment.NewLine + "<lnvxe:footnotegrp>", "");
                text = text.Replace("</lnvxe:fnr> ,", "</lnvxe:fnr>,");
                text = text.Replace("</lnvxe:fnr> .", "</lnvxe:fnr>.");
                text = text.Replace("</lnvxe:fnr> :", "</lnvxe:fnr>:");

                if (text.Contains("<lnv:COURT>UNITED STATES DISTRICT COURT"))
                {
                    text = text.Replace("<lnvxe:docketnumber>", "<lnvxe:docketnumber pacernum=\"\">");
                }

                text = text.Replace("</lnvxe:fnr>" + Environment.NewLine + "<lnvxe:blockquote>", "</lnvxe:fnr></lnvxe:text></p>" + Environment.NewLine + "<lnvxe:blockquote>");
                if (text.Contains(" missing") || text.Contains(" Missing") || text.Contains(" MISSING"))
                {
                    text += "<lnv:SYS-PROD-INFO>#OKILLEGIBLE#</lnv:SYS-PROD-INFO>\r\n";
                }
                text += "</COURTCASE>\r\n";
                text = Regex.Replace(text, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
                text = Regex.Replace(text, @"(?m)[\s-[\r]]+\r?$", "", RegexOptions.Multiline);
                //File.WriteAllText(path, text, Encoding.Default);
                return text;
            }
        }

        internal string EntitiesConv(string wholedatatxt)
        {
            wholedatatxt = Regex.Replace(wholedatatxt, "\\$\\(O\\>(.*?)\\<O\\$\\)", "<lnvxe:strike>$1</lnvxe:strike>");
            wholedatatxt = Strings.Replace(wholedatatxt, "&", "&#x0026;", 1, -1, CompareMethod.Binary);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(System.Windows.Forms.Application.StartupPath + "\\SpiconEntities.xml");
            try
            {
                foreach (object obj in xmlDocument.SelectNodes("//*/item"))
                {
                    XmlNode xmlNode = (XmlNode)obj;
                    if ((xmlNode["character"] != null & xmlNode["entity"] != null) && xmlNode["character"].InnerText.Length >= 1)
                    {
                        string replacement = "";
                        if (xmlNode["entity"].InnerText.Length > 0)
                        {
                            replacement = xmlNode["entity"].InnerText;
                        }
                        wholedatatxt = Strings.Replace(wholedatatxt, xmlNode["character"].InnerText, replacement, 1, -1, CompareMethod.Binary);
                    }
                }
            }
            finally
            {

            }
            return wholedatatxt;
        }

        // Token: 0x060004D4 RID: 1236 RVA: 0x000D4540 File Offset: 0x000D3540
        internal string Formatcontrol(string wholedatatxt)
        {
            wholedatatxt = Regex.Replace(wholedatatxt, "(\\$=P(\\d+)?\\*P(\\d+)?)", "<page count=\"P$3\" rsc=\"$2\"/>");
            wholedatatxt = Regex.Replace(wholedatatxt, "(\\$=P(\\d+)?\\*(\\d+)?)", "<page count=\"$3\" rsc=\"$2\"/>");
            wholedatatxt = Regex.Replace(wholedatatxt, "\r\n", "</spicon_enter><spicon_enter>");
            wholedatatxt = "<spicon_enter>" + wholedatatxt;
            MatchCollection matchCollection = Regex.Matches(wholedatatxt, "<spicon_enter>(.*?)</spicon_enter>");
            int num = 0;
            checked
            {
                int num2 = matchCollection.Count - 1;
                for (int i = num; i <= num2; i++)
                {
                    string text = matchCollection[i].Groups[0].ToString();
                    if (Regex.IsMatch(text, "\\$F\\>FTNT\\>\\$T\\*|\\$F\\>FTNT\\>\\$\\%\\$\\%\\*|\\$F\\$\\%\\$\\%\\*|\\$F\\$T\\*"))
                    {
                        text = matchCollection[i].Groups[0].ToString();
                        MatchCollection matchCollection2 = Regex.Matches(text, "\\*");
                        int num3 = 0;
                        int num4 = matchCollection2.Count - 1;
                        for (int j = num3; j <= num4; j++)
                        {
                            footnotecount = Conversions.ToInteger(matchCollection2.Count.ToString());
                            footnotecountboo = true;
                            foottestcount = true;
                            footcountboo = true;
                            footNcountboo = true;
                            text = Regex.Replace(text, "\\s\\*", string.Concat(new string[]
                            {
                                " <lnvxe:fnr alt-content=\"<spicon_ast/>\" fnrtoken=\"ref",
                                Conversions.ToString(j + 1),
                                "\" fntoken=\"fnote",
                                Conversions.ToString(j + 1),
                                "\"><spicon_ast/></lnvxe:fnr>"
                            }));
                        }
                        if (matchCollection2.Count > 0)
                        {
                            wholedatatxt = Strings.Replace(wholedatatxt, matchCollection[i].Groups[0].ToString(), text, 1, 1, CompareMethod.Binary);
                        }
                    }
                }
                MatchCollection matchCollection3 = Regex.Matches(wholedatatxt, "<spicon_enter>(.*?)</spicon_enter>");
                int num5 = 0;
                int num6 = matchCollection3.Count - 1;
                for (int k = num5; k <= num6; k++)
                {
                    string text2 = matchCollection3[k].Groups[0].ToString();
                    if (Regex.IsMatch(text2, "\\$F\\>FTNT\\>\\$T\\*|\\$F\\>FTNT\\>\\$\\%\\$\\%\\*|\\$F\\$\\%\\$\\%\\*|\\$F\\$T\\*"))
                    {
                        text2 = matchCollection3[k - 1].Groups[0].ToString();
                        MatchCollection matchCollection4 = Regex.Matches(text2, "\\*");
                        int num7 = 0;
                        int num8 = matchCollection4.Count - 1;
                        for (int l = num7; l <= num8; l++)
                        {
                            text2 = Regex.Replace(text2, "\\*", string.Concat(new string[]
                            {
                                "<lnvxe:fnr alt-content=\"<spicon_ast/>\" fnrtoken=\"ref",
                                Conversions.ToString(l + 1),
                                "\" fntoken=\"fnote",
                                Conversions.ToString(l + 1),
                                "\"><spicon_ast/></lnvxe:fnr>"
                            }));
                        }
                        if (matchCollection4.Count > 0)
                        {
                            wholedatatxt = Strings.Replace(wholedatatxt, matchCollection3[k - 1].Groups[0].ToString(), text2, 1, 1, CompareMethod.Binary);
                        }
                    }
                }
                wholedatatxt = Regex.Replace(wholedatatxt, "</spicon_enter><spicon_enter>", "\r\n");
                wholedatatxt = Regex.Replace(wholedatatxt, "<spicon_enter>", "");
                wholedatatxt = Regex.Replace(wholedatatxt, "</spicon_enter>", "");
                Match match = Regex.Match(wholedatatxt, "(\\s)(n[0-9]+(\\))?(\\,)?(\\;)?(\\.)?(\\))?)(\\s)?", RegexOptions.Singleline);
                bool flag = false;
                int num9 = 0;
                int num10 = 1;
                while (match.Success)
                {
                    string text3 = match.Groups[0].ToString();
                    string expression = match.Groups[0].ToString();
                    string text4 = match.Groups[2].ToString();
                    text3 = Strings.Replace(text3, "n", "<spicon_n/>", 1, -1, CompareMethod.Binary);
                    expression = Strings.Replace(expression, "n", "<spicon_n/>", 1, -1, CompareMethod.Binary);
                    text4 = Strings.Replace(text4, "n", "<spicon_n/>", 1, -1, CompareMethod.Binary);
                    text4 = Regex.Replace(text4, "\\,$", "");
                    if (Regex.IsMatch(text3, "<spicon_n/>[0-9]+\\)\\,"))
                    {
                        flag = true;
                    }
                    Match match2 = Regex.Match(match.Groups[0].ToString(), "n[0-9]+");
                    string text5 = match2.Groups[0].ToString();
                    text5 = Strings.Replace(text5, "n", "<spicon_n/>", 1, -1, CompareMethod.Binary);
                    text5 = Regex.Replace(text5, "\\,$", "");
                    if (footnotecountboo)
                    {
                        num9 = footnotecount + 1;
                        footnotecountboo = false;
                    }
                    if (flag)
                    {
                        wholedatatxt = Strings.Replace(wholedatatxt, match.Groups[0].ToString(), string.Concat(new string[]
                        {
                            " <lnvxe:fnr alt-content=\"",
                            text5,
                            "\" fnrtoken=\"ref",
                            Conversions.ToString(num9),
                            "\" fntoken=\"fnote",
                            Conversions.ToString(num9),
                            "\">",
                            text5,
                            "</lnvxe:fnr>), "
                        }), 1, 1, CompareMethod.Binary);
                        flag = false;
                    }
                    else
                    {
                        wholedatatxt = Strings.Replace(wholedatatxt, match.Groups[0].ToString(), string.Concat(new string[]
                        {
                            " <lnvxe:fnr alt-content=\"",
                            text5,
                            "\" fnrtoken=\"ref",
                            Conversions.ToString(num9),
                            "\" fntoken=\"fnote",
                            Conversions.ToString(num9),
                            "\">",
                            text5,
                            "</lnvxe:fnr> "
                        }), 1, 1, CompareMethod.Binary);
                    }
                    match = Regex.Match(wholedatatxt, "(\\s)(n[0-9]+(\\))?(\\,)?(\\;)?(\\.)?(\\))?)(\\s)", RegexOptions.Singleline);
                    num9++;
                    num10++;
                }
                int num11 = 0;
                Match match3 = Regex.Match(wholedatatxt, "\\$F\\>FTNT\\>(.*?)\\>ENDFN\\>\\$E", RegexOptions.Singleline);
                while (match3.Success)
                {
                    if (foottestcount)
                    {
                        num11 = footnotecount + 1;
                        foottestcount = false;
                    }
                    num11++;
                    string text6 = match3.Groups[0].ToString();
                    if (Regex.IsMatch(text6, "<spicon_ast/>"))
                    {
                        text6 = Regex.Replace(text6, "<lnvxe:fnr(.*?)</lnvxe:fnr>", "");
                        text6 = Strings.Replace(text6, "$F>FTNT>", string.Concat(new string[]
                        {
                            "<lnvxe:footnotegrp>\r\n<lnvxe:footnote fnrtokens=\"ref",
                            Conversions.ToString(num11),
                            "\" fntoken=\"fnote",
                            Conversions.ToString(num11),
                            "\">\r\n<lnvxe:fnlabel alt-content=\"*\">*</lnvxe:fnlabel>\r\n<lnvxe:fnbody>\r\n"
                        }), 1, 1, CompareMethod.Binary);
                        text6 = Strings.Replace(text6, ">ENDFN>$E", "</lnvxe:fnbody></lnvxe:footnote>\r\n</lnvxe:footnotegrp>", 1, 1, CompareMethod.Binary);
                    }
                    else
                    {
                        text6 = Strings.Replace(text6, "$F>FTNT>", string.Concat(new string[]
                        {
                            "<lnvxe:footnotegrp>\r\n<lnvxe:footnote fnrtokens=\"ref",
                            Conversions.ToString(num11),
                            "\" fntoken=\"fnote",
                            Conversions.ToString(num11),
                            "\">\r\n<lnvxe:fnlabel alt-content=\"n",
                            Conversions.ToString(num11),
                            "\">",
                            Conversions.ToString(num11),
                            "</lnvxe:fnlabel>\r\n<lnvxe:fnbody>\r\n"
                        }), 1, 1, CompareMethod.Binary);
                        text6 = Strings.Replace(text6, ">ENDFN>$E", "</lnvxe:fnbody></lnvxe:footnote>\r\n</lnvxe:footnotegrp>", 1, 1, CompareMethod.Binary);
                    }
                    if (Regex.IsMatch(text6, "\\$F\\$T(n[0-9]+(\\,)?(\\;)?(\\.)?(\\))?)(\\s)"))
                    {
                        text6 = Regex.Replace(text6, "\\$F\\$T(n[0-9]+(\\,)?(\\;)?(\\.)?(\\))?)(\\s)", string.Concat(new string[]
                        {
                            "<lnvxe:footnote fnrtokens=\"ref",
                            Conversions.ToString(num11),
                            "\" fntoken=\"fnote",
                            Conversions.ToString(num11),
                            "\"><lnvxe:fnlabel alt-content=\"n",
                            Conversions.ToString(num11),
                            "\">",
                            Conversions.ToString(num11),
                            "</lnvxe:fnlabel>\r\n<lnvxe:fnbody>\r\n<p i=\"3\"><lnvxe:text>"
                        }));
                    }
                    if (Regex.IsMatch(text6, "\\$T"))
                    {
                        text6 = Regex.Replace(text6, "\\$T", "<p i=\"3\"><lnvxe:text>");
                    }
                    if (Regex.IsMatch(text6, "</lnvxe:fnbody>"))
                    {
                        text6 = Regex.Replace(text6, "</lnvxe:fnbody>", "</lnvxe:text></p></lnvxe:fnbody>");
                    }
                    if (Regex.IsMatch(text6, "\\$E"))
                    {
                        text6 = Strings.Replace(text6, "$E", "</lnvxe:text></p></lnvxe:fnbody></lnvxe:footnote>\r\n", 1, 1, CompareMethod.Binary);
                    }
                    if (Regex.IsMatch(text6, "\\$F"))
                    {
                        text6 = Strings.Replace(text6, "$F", "<lnvxe:fnbody>\r\n", 1, 1, CompareMethod.Binary);
                    }
                    text6 = Regex.Replace(text6, "</lnvxe:fnbody>", "\r\n</lnvxe:fnbody>");
                    Match match4 = Regex.Match(text6, "<lnvxe:fnbody>\r\n\\$\\%\\$\\%n(\\d+)", RegexOptions.Singleline);
                    if (match4.Success)
                    {
                        text6 = Regex.Replace(text6, "<lnvxe:footnote(.[^<>]*?)fntoken=(.[^<>]*?)\">", "");
                        text6 = Regex.Replace(text6, "<lnvxe:fnlabel(.*?)</lnvxe:fnlabel>", "");
                        text6 = Regex.Replace(text6, "<lnvxe:footnotegrp>\r\n\r\n\r\n", "<lnvxe:footnotegrp>\r\n");
                    }
                    while (match4.Success)
                    {
                        string text7 = match4.Groups[1].ToString();
                        string text8 = match4.Groups[0].ToString();
                        if (footcountboo)
                        {
                            text7 = Conversions.ToString(footnotecount + 1);
                            footcountboo = false;
                        }
                        text6 = Strings.Replace(text6, match4.Groups[0].ToString(), string.Concat(new string[]
                        {
                            "<lnvxe:footnote fnrtokens=\"ref",
                            text7,
                            "\" fntoken=\"fnote",
                            text7,
                            "\">\r\n<lnvxe:fnlabel alt-content=\"n",
                            text7,
                            "\">",
                            text7,
                            "</lnvxe:fnlabel>\r\n<lnvxe:fnbody>\r\n<p><lnvxe:text>"
                        }), 1, 1, CompareMethod.Binary);
                        match4 = Regex.Match(text6, "<lnvxe:fnbody>\r\n\\$\\%\\$\\%n(\\d+)", RegexOptions.Singleline);
                    }
                    wholedatatxt = Strings.Replace(wholedatatxt, match3.Groups[0].ToString(), text6, 1, 1, CompareMethod.Binary);
                    match3 = Regex.Match(wholedatatxt, "\\$F\\>FTNT\\>(.*?)\\>ENDFN\\>\\$E", RegexOptions.Singleline);
                }
                int num12 = 0;
                Match match5 = Regex.Match(wholedatatxt, "\\$F(.*?)\\$E", RegexOptions.Singleline);
                while (match5.Success)
                {
                    num12++;
                    string text9 = match5.Groups[0].ToString();
                    if (Regex.IsMatch(text9, "\\$F\\$\\%\\$\\%\\*|\\$F\\$T\\*"))
                    {
                        text9 = Strings.Replace(text9, "*", "<spicon_ast/>", 1, -1, CompareMethod.Binary);
                    }
                    if (Regex.IsMatch(text9, "<spicon_ast/>"))
                    {
                        text9 = Strings.Replace(text9, "$F", string.Concat(new string[]
                        {
                            "<lnvxe:footnotegrp>\r\n<lnvxe:footnote fnrtokens=\"ref",
                            Conversions.ToString(num12),
                            "\" fntoken=\"fnote",
                            Conversions.ToString(num12),
                            "\">\r\n<lnvxe:fnlabel alt-content=\"*\">*</lnvxe:fnlabel>\r\n<lnvxe:fnbody>\r\n"
                        }), 1, 1, CompareMethod.Binary);
                        text9 = Strings.Replace(text9, "$E", "</lnvxe:fnbody></lnvxe:footnote>\r\n</lnvxe:footnotegrp>", 1, 1, CompareMethod.Binary);
                        text9 = Strings.Replace(text9, "<spicon_ast/>", "", 1, -1, CompareMethod.Binary);
                    }
                    else
                    {
                        text9 = Strings.Replace(text9, "$F", string.Concat(new string[]
                        {
                            "<lnvxe:footnotegrp>\r\n<lnvxe:footnote fnrtokens=\"ref",
                            Conversions.ToString(num12),
                            "\" fntoken=\"fnote",
                            Conversions.ToString(num12),
                            "\">\r\n<lnvxe:fnlabel alt-content=\"n",
                            Conversions.ToString(num12),
                            "\">",
                            Conversions.ToString(num12),
                            "</lnvxe:fnlabel>\r\n<lnvxe:fnbody>\r\n"
                        }), 1, 1, CompareMethod.Binary);
                        text9 = Strings.Replace(text9, "$E", "</lnvxe:fnbody></lnvxe:footnote>\r\n</lnvxe:footnotegrp>", 1, 1, CompareMethod.Binary);
                    }
                    wholedatatxt = Strings.Replace(wholedatatxt, match5.Groups[0].ToString(), text9, 1, 1, CompareMethod.Binary);
                    match5 = Regex.Match(wholedatatxt, "\\$F(.*?)\\$E", RegexOptions.Singleline);
                }
                wholedatatxt = Regex.Replace(wholedatatxt, "<lnvxe:fnbody>\\$\\%\\$\\%(n|\\*)([0-9]+)?(.[^<>]*?)</lnvxe:fnbody>", "<lnvxe:fnbody><p><lnvxe:text>$3</lnvxe:text></p></lnvxe:fnbody>");
                wholedatatxt = Regex.Replace(wholedatatxt, "<lnvxe:fnbody>\\$T(n|\\*)([0-9]+)?(.[^<>]*?)</lnvxe:fnbody>", "<lnvxe:fnbody><p i=\"3\"><lnvxe:text>$3</lnvxe:text></p></lnvxe:fnbody>");
                wholedatatxt = Regex.Replace(wholedatatxt, "<lnvxe:fnbody><p i=\"3\"><lnvxe:text>\\s", "<lnvxe:fnbody><p i=\"3\"><lnvxe:text>", RegexOptions.Singleline);
                wholedatatxt = Strings.Replace(wholedatatxt, "<lnvxe:footnotegrp>", "\r\n<lnvxe:footnotegrp>", 1, -1, CompareMethod.Binary);
                wholedatatxt = Strings.Replace(wholedatatxt, "<spicon_n/>", "n", 1, -1, CompareMethod.Binary);
                wholedatatxt = Strings.Replace(wholedatatxt, "<spicon_ast/>", "*", 1, -1, CompareMethod.Binary);
                wholedatatxt = Regex.Replace(wholedatatxt, "n(\\d+)?\\</lnvxe:fnr>", "$1</lnvxe:fnr>", RegexOptions.Singleline);
                wholedatatxt = Regex.Replace(wholedatatxt, "n(\\d+)?(\\,)?(\\;)?(\\.)?(\\))?\\</lnvxe:fnr>", "$1</lnvxe:fnr>", RegexOptions.Singleline);
                wholedatatxt = Regex.Replace(wholedatatxt, "\\$=DU(.*?)\\$=DO", "<emph typestyle=\"dblSPIHYPHENun\">$1</emph>");
                wholedatatxt = Regex.Replace(wholedatatxt, "\\$I\\$U\\$=B(.*?)\\$=R\\$O\\$N", "<emph typestyle=\"biu\">$1</emph>");
                wholedatatxt = Regex.Replace(wholedatatxt, "\\$I\\$=B(.*?)\\$=R\\$N", "<emph typestyle=\"ib\">$1</emph>");
                wholedatatxt = Regex.Replace(wholedatatxt, "\\$=B\\$I(.*?)\\$N\\$=R", "<emph typestyle=\"ib\">$1</emph>");
                wholedatatxt = Regex.Replace(wholedatatxt, "\\$U\\$=B(.*?)\\$=R\\$O", "<emph typestyle=\"bu\">$1</emph>");
                wholedatatxt = Regex.Replace(wholedatatxt, "\\$I\\$U(.*?)\\$O\\$N", "<emph typestyle=\"iu\">$1</emph>");
                wholedatatxt = Regex.Replace(wholedatatxt, "\\$U\\$I(.*?)\\$N\\$O", "<emph typestyle=\"iu\">$1</emph>");
                wholedatatxt = Regex.Replace(wholedatatxt, "\\$=B\\$U(.*?)\\$O\\$=R", "<emph typestyle=\"bu\">$1</emph>");
                wholedatatxt = Regex.Replace(wholedatatxt, "\\$W(.*?)\\$K", "<emph typestyle=\"smcaps\">$1</emph>");
                wholedatatxt = Regex.Replace(wholedatatxt, "\\$=B\\$I", "<emph typestyle=\"ib\">");
                wholedatatxt = Regex.Replace(wholedatatxt, "\\$U\\$=B", "<emph typestyle=\"bu\"> ");
                wholedatatxt = Regex.Replace(wholedatatxt, "\\$O\\$=B", "</emph>");
                wholedatatxt = Regex.Replace(wholedatatxt, "\\$I\\$U", "<emph typestyle=\"iu\">");
                wholedatatxt = Regex.Replace(wholedatatxt, "\\$N\\$O", "</emph>");
                wholedatatxt = Regex.Replace(wholedatatxt, "\\$I", "<emph typestyle=\"it\">");
                wholedatatxt = Regex.Replace(wholedatatxt, "\\$N", "</emph>");
                wholedatatxt = Regex.Replace(wholedatatxt, "\\$=B", "<emph typestyle=\"bf\">");
                wholedatatxt = Regex.Replace(wholedatatxt, "\\$=R", "</emph>");
                wholedatatxt = Regex.Replace(wholedatatxt, "\\$U", "<emph typestyle=\"un\">");
                wholedatatxt = Regex.Replace(wholedatatxt, "\\$O", "</emph>");
                Match match6 = Regex.Match(wholedatatxt, "\\$=S\\$T(.*?)\\$=I", RegexOptions.Singleline);
                while (match6.Success)
                {
                    string str = match6.Groups[1].ToString();
                    MatchCollection matchCollection5 = Regex.Matches(wholedatatxt, "\\$T(.*?)\r\n", RegexOptions.Singleline);
                    int num13 = 0;
                    int num14 = matchCollection5.Count - 1;
                    for (int m = num13; m <= num14; m++)
                    {
                        if (Operators.CompareString(matchCollection5[m].Groups[0].ToString(), "", false) != 0)
                        {
                            string text10 = matchCollection5[m].Groups[0].ToString();
                            text10 = Regex.Replace(text10, "^\\$T", "");
                            text10 = Regex.Replace(text10, "\r\n", "");
                            string replacement = "<p i=\"3\"><lnvxe:text>" + text10 + "</lnvxe:text></p>\r\n";
                            wholedatatxt = Strings.Replace(wholedatatxt, matchCollection5[m].Groups[0].ToString(), replacement, 1, 1, CompareMethod.Binary);
                        }
                    }
                    wholedatatxt = Strings.Replace(wholedatatxt, match6.Groups[0].ToString(), "<lnvxe:blockquote>\r\n<p i=\"3\"><lnvxe:text>" + str + "\r\n</lnvxe:text></p>\r\n</lnvxe:blockquote>", 1, 1, CompareMethod.Binary);
                    match6 = Regex.Match(wholedatatxt, "\\$=S\\$T(.*?)\\$=I", RegexOptions.Singleline);
                }
                match6 = Regex.Match(wholedatatxt, "\\$=S\\$%\\$%(.*?)\\$=I", RegexOptions.Singleline);
                while (match6.Success)
                {
                    string str2 = match6.Groups[1].ToString();
                    MatchCollection matchCollection6 = Regex.Matches(wholedatatxt, "\\$T(.*?)\r\n", RegexOptions.Singleline);
                    int num15 = 0;
                    int num16 = matchCollection6.Count - 1;
                    for (int n = num15; n <= num16; n++)
                    {
                        if (Operators.CompareString(matchCollection6[n].Groups[0].ToString(), "", false) != 0)
                        {
                            string text11 = matchCollection6[n].Groups[0].ToString();
                            text11 = Regex.Replace(text11, "^\\$T", "");
                            text11 = Regex.Replace(text11, "\r\n", "");
                            string replacement2 = "<p i=\"3\"><lnvxe:text>" + text11 + "</lnvxe:text></p>\r\n";
                            wholedatatxt = Strings.Replace(wholedatatxt, matchCollection6[n].Groups[0].ToString(), replacement2, 1, 1, CompareMethod.Binary);
                        }
                    }
                    wholedatatxt = Strings.Replace(wholedatatxt, match6.Groups[0].ToString(), "<lnvxe:blockquote>\r\n<p><lnvxe:text>" + str2 + "\r\n</lnvxe:text></p>\r\n</lnvxe:blockquote>", 1, 1, CompareMethod.Binary);
                    match6 = Regex.Match(wholedatatxt, "\\$=S\\$%\\$%(.*?)\\$=I", RegexOptions.Singleline);
                }
                match6 = Regex.Match(wholedatatxt, "\\$=S\\$%(.*?)\\$=I", RegexOptions.Singleline);
                while (match6.Success)
                {
                    string str3 = match6.Groups[1].ToString();
                    MatchCollection matchCollection7 = Regex.Matches(wholedatatxt, "\\$T(.*?)\r\n", RegexOptions.Singleline);
                    int num17 = 0;
                    int num18 = matchCollection7.Count - 1;
                    for (int num19 = num17; num19 <= num18; num19++)
                    {
                        if (Operators.CompareString(matchCollection7[num19].Groups[0].ToString(), "", false) != 0)
                        {
                            string text12 = matchCollection7[num19].Groups[0].ToString();
                            text12 = Regex.Replace(text12, "^\\$T", "");
                            text12 = Regex.Replace(text12, "\r\n", "");
                            string replacement3 = "<p i=\"3\"><lnvxe:text>" + text12 + "</lnvxe:text></p>\r\n";
                            wholedatatxt = Strings.Replace(wholedatatxt, matchCollection7[num19].Groups[0].ToString(), replacement3, 1, 1, CompareMethod.Binary);
                        }
                    }
                    wholedatatxt = Strings.Replace(wholedatatxt, match6.Groups[0].ToString(), "<lnvxe:blockquote><p nl=\"1\">\r\n<lnvxe:text>" + str3 + "\r\n</lnvxe:text></p>\r\n</lnvxe:blockquote>", 1, 1, CompareMethod.Binary);
                    match6 = Regex.Match(wholedatatxt, "\\$=S\\$%(.*?)\\$=I", RegexOptions.Singleline);
                }
                MatchCollection matchCollection8 = Regex.Matches(wholedatatxt, "\\$T(.*?)\r\n", RegexOptions.Singleline);
                int num20 = 0;
                int num21 = matchCollection8.Count - 1;
                for (int num22 = num20; num22 <= num21; num22++)
                {
                    if (Operators.CompareString(matchCollection8[num22].Groups[0].ToString(), "", false) != 0)
                    {
                        string text13 = matchCollection8[num22].Groups[0].ToString();
                        text13 = Regex.Replace(text13, "^\\$T", "");
                        text13 = Regex.Replace(text13, "\r\n", "");
                        string replacement4 = "<p i=\"3\"><lnvxe:text>" + text13 + "</lnvxe:text></p>\r\n";
                        wholedatatxt = Strings.Replace(wholedatatxt, matchCollection8[num22].Groups[0].ToString(), replacement4, 1, 1, CompareMethod.Binary);
                    }
                }
                MatchCollection matchCollection9 = Regex.Matches(wholedatatxt, "\\$%\\$%(.*?)\r\n", RegexOptions.Singleline);
                int num23 = 0;
                int num24 = matchCollection9.Count - 1;
                for (int num25 = num23; num25 <= num24; num25++)
                {
                    if (Operators.CompareString(matchCollection9[num25].Groups[0].ToString(), "", false) != 0)
                    {
                        string text14 = matchCollection9[num25].Groups[0].ToString();
                        text14 = Regex.Replace(text14, "^\\$%\\$%", "");
                        text14 = Regex.Replace(text14, "\r\n", "");
                        string replacement5 = "<p><lnvxe:text>" + text14 + "</lnvxe:text></p>\r\n";
                        wholedatatxt = Strings.Replace(wholedatatxt, matchCollection9[num25].Groups[0].ToString(), replacement5, 1, 1, CompareMethod.Binary);
                    }
                }
                wholedatatxt = Regex.Replace(wholedatatxt, "\\$\\=H", "<lnvxe:h>");
                wholedatatxt = Regex.Replace(wholedatatxt, "\\$\\=E", "</lnvxe:h>");
                wholedatatxt = Regex.Replace(wholedatatxt, " </lnvxe:text></p>", "</lnvxe:text></p>");
                wholedatatxt = Regex.Replace(wholedatatxt, "</lnvxe:text></p>\r\n\r\n", "</lnvxe:text></p>\r\n");
                wholedatatxt = Regex.Replace(wholedatatxt, ">\\s<", "><");
                wholedatatxt = Regex.Replace(wholedatatxt, "> <", "><");
                wholedatatxt = Regex.Replace(wholedatatxt, "<lnvxe:text>\\s", "<lnvxe:text>");
                wholedatatxt = Regex.Replace(wholedatatxt, "<lnvxe:text>\\s", "<lnvxe:text>");
                Match match7 = Regex.Match(wholedatatxt, "<lnvxe:h>(.*?)</lnvxe:h></lnvxe:text></p>");
                while (match7.Success)
                {
                    string text15 = match7.Groups[0].ToString();
                    text15 = Regex.Replace(text15, "<lnvxe:h>(.*?)</lnvxe:h></lnvxe:text></p>", "</lnvxe:text></p>\r\n<lnvxe:h>$1</lnvxe:h>\r\n");
                    text15 = Regex.Replace(text15, "</lnvxe:h>", "</lnvxe:h>\r\n");
                    wholedatatxt = Strings.Replace(wholedatatxt, match7.Groups[0].ToString(), text15, 1, 1, CompareMethod.Binary);
                    match7 = Regex.Match(wholedatatxt, "<lnvxe:h>(.*?)</lnvxe:h></lnvxe:text></p>");
                }
                wholedatatxt = Regex.Replace(wholedatatxt, "\\$F", string.Concat(new string[]
                {
                    "\r\n<lnvxe:footnotegrp>\r\n<lnvxe:footnote fnrtokens=\"ref",
                    Conversions.ToString(num11),
                    "\" fntoken=\"fnote",
                    Conversions.ToString(num11),
                    "\">\r\n<lnvxe:fnlabel alt-content=\"n",
                    Conversions.ToString(num11),
                    "\">",
                    Conversions.ToString(num11),
                    "</lnvxe:fnlabel>\r\n<lnvxe:fnbody>\r\n"
                }), RegexOptions.Singleline);
                MatchCollection matchCollection10 = Regex.Matches(wholedatatxt, "<lnvxe:footnote fnrtokens=(.[^<>]*?)fntoken=(.[^<>]*?)>", RegexOptions.Singleline);
                int num26 = 0;
                int num27 = matchCollection10.Count - 1;
                for (int num28 = num26; num28 <= num27; num28++)
                {
                    if (Operators.CompareString(matchCollection10[num28].Groups[0].ToString(), "", false) != 0)
                    {
                        string text16 = matchCollection10[num28].Groups[0].ToString();
                        text16 = Regex.Replace(text16, "<lnvxe:footnote (.[^<>]*?)>", string.Concat(new string[]
                        {
                            "<1lnvxe:footnote fnrtokens=\"ref",
                            Conversions.ToString(num28 + 1),
                            "\" fntoken=\"fnote",
                            Conversions.ToString(num28 + 1),
                            "\">"
                        }));
                        wholedatatxt = Strings.Replace(wholedatatxt, matchCollection10[num28].Groups[0].ToString(), text16, 1, 1, CompareMethod.Binary);
                    }
                }
                wholedatatxt = Regex.Replace(wholedatatxt, "<1lnvxe:footnote ", "<lnvxe:footnote ");
                MatchCollection matchCollection11 = Regex.Matches(wholedatatxt, "<lnvxe:fnr alt-content=(.[^<>]*?)fnrtoken=(.[^<>]*?)fntoken=(.[^<>]*?)>(.[^<>]*?)</lnvxe:fnr>", RegexOptions.Singleline);
                int num29 = 0;
                int num30 = matchCollection11.Count - 1;
                for (int num31 = num29; num31 <= num30; num31++)
                {
                    if (Operators.CompareString(matchCollection11[num31].Groups[0].ToString(), "", false) != 0)
                    {
                        string text17 = matchCollection11[num31].Groups[0].ToString();
                        text17 = Regex.Replace(text17, "<lnvxe:fnr alt-content=(.[^<>]*?)>", string.Concat(new string[]
                        {
                            "<1lnvxe:fnr alt-content=",
                            matchCollection11[num31].Groups[1].ToString(),
                            "fnrtoken=\"ref",
                            Conversions.ToString(num31 + 1),
                            "\" fntoken=\"fnote",
                            Conversions.ToString(num31 + 1),
                            "\">"
                        }));
                        wholedatatxt = Strings.Replace(wholedatatxt, matchCollection11[num31].Groups[0].ToString(), text17, 1, 1, CompareMethod.Binary);
                    }
                }
                wholedatatxt = Regex.Replace(wholedatatxt, "<1lnvxe:fnr alt-content", "<lnvxe:fnr alt-content");
                MatchCollection matchCollection12 = Regex.Matches(this.tempdata, "\\$\\%\\$\\%(n[0-9]+(\\,)?(\\;)?(\\.)?(\\))?)(\\s)", RegexOptions.Singleline);
                string text18 = "";
                int num32 = 0;
                int num33 = matchCollection12.Count - 1;
                for (int num34 = num32; num34 <= num33; num34++)
                {
                    string str4 = matchCollection12[num34].Groups[1].ToString();
                    text18 = text18 + str4 + "|";
                }
                MatchCollection matchCollection13 = Regex.Matches(wholedatatxt, "<lnvxe:footnote fnrtokens=\"ref(.[^<>]*?)\" fntoken=\"fnote(.[^<>]*?)\">(\r\n)?<lnvxe:fnlabel alt-content=\"n(.[^<>]*?)\">(.[^<>]*?)</lnvxe:fnlabel>", RegexOptions.Singleline);
                int num35 = 0;
                int num36 = matchCollection13.Count - 1;
                for (int num37 = num35; num37 <= num36; num37++)
                {
                    if (Operators.CompareString(matchCollection13[num37].Groups[0].ToString(), "", false) != 0)
                    {
                        string text19 = matchCollection13[num37].Groups[0].ToString();
                        string text20 = matchCollection13[num37].Groups[1].ToString();
                        string input = "n" + matchCollection13[num37].Groups[1].ToString();
                        if (Regex.IsMatch(input, "^(" + text18 + ")$"))
                        {
                            text19 = Regex.Replace(text19, "<lnvxe:fnlabel alt-content=\"n(.[^<>]*?)\">(.[^<>]*?)</lnvxe:fnlabel>", string.Concat(new string[]
                            {
                                "<lnvxe:fnlabel alt-content=\"n",
                                text20,
                                "\">",
                                text20,
                                "</lnvxe:fnlabel>"
                            }));
                        }
                        wholedatatxt = Strings.Replace(wholedatatxt, matchCollection13[num37].Groups[0].ToString(), text19, 1, 1, CompareMethod.Binary);
                    }
                }
                MatchCollection matchCollection14 = Regex.Matches(wholedatatxt, "<lnvxe:fnr alt-content=\"\\*\"(.*?)</lnvxe:footnotegrp>", RegexOptions.Singleline);
                int num38 = 0;
                int num39 = matchCollection14.Count - 1;
                for (int num40 = num38; num40 <= num39; num40++)
                {
                    if (Operators.CompareString(matchCollection14[num40].Groups[0].ToString(), "", false) != 0)
                    {
                        string text21 = matchCollection14[num40].Groups[0].ToString();
                        Match match8 = Regex.Match(text21, "<lnvxe:fnlabel alt-content=\"n(.[^<>]*?)\">(.[^<>]*?)</lnvxe:fnlabel>", RegexOptions.Singleline);
                        if (match8.Success)
                        {
                            string pattern = match8.Groups[0].ToString();
                            text21 = Regex.Replace(text21, pattern, "<lnvxe:fnlabel alt-content=\"*\">*</lnvxe:fnlabel>", RegexOptions.Singleline);
                        }
                        Match match9 = Regex.Match(text21, "<lnvxe:fnbody>(.*?)</lnvxe:fnbody>", RegexOptions.Singleline);
                        if (match9.Success)
                        {
                            string text22 = match9.Groups[0].ToString();
                            text22 = Regex.Replace(text22, "\\*(\\s*)?", "");
                            text21 = Strings.Replace(text21, match9.Groups[0].ToString(), text22, 1, 1, CompareMethod.Binary);
                        }
                        wholedatatxt = Strings.Replace(wholedatatxt, matchCollection14[num40].Groups[0].ToString(), text21, 1, 1, CompareMethod.Binary);
                    }
                }
                MatchCollection matchCollection15 = Regex.Matches(wholedatatxt, "<lnvxe:footnote fnrtokens=\"ref(.[^<>]*?)\" fntoken=\"fnote(.[^<>]*?)\">(\r\n)?<lnvxe:fnlabel alt-content=\"n(.[^<>]*?)\">(.[^<>]*?)</lnvxe:fnlabel>", RegexOptions.Singleline);
                string text23 = "";
                int num41 = 0;
                int num42 = matchCollection15.Count - 1;
                for (int num43 = num41; num43 <= num42; num43++)
                {
                    if (Operators.CompareString(matchCollection15[num43].Groups[0].ToString(), "", false) != 0)
                    {
                        string text24 = matchCollection15[num43].Groups[0].ToString();
                        string left = matchCollection15[num43].Groups[1].ToString();
                        string text25 = matchCollection15[num43].Groups[4].ToString();
                        if (this.nboo && Operators.CompareString(text23, "", false) != 0)
                        {
                            text25 = Conversions.ToString(unchecked(Conversions.ToDouble(text23) + 1.0));
                        }
                        if (Operators.CompareString(left, text25, false) != 0)
                        {
                            text24 = Regex.Replace(text24, "<lnvxe:fnlabel alt-content=\"n(.[^<>]*?)\">(.[^<>]*?)</lnvxe:fnlabel>", string.Concat(new string[]
                            {
                                "<lnvxe:fnlabel alt-content=\"n",
                                text25,
                                "\">",
                                text25,
                                "</lnvxe:fnlabel>"
                            }));
                            this.nboo = true;
                        }
                        Match match10 = Regex.Match(text24, "<lnvxe:fnlabel alt-content=\"n(.[^<>]*?)\">(.[^<>]*?)</lnvxe:fnlabel>", RegexOptions.Singleline);
                        text23 = match10.Groups[1].ToString();
                        wholedatatxt = Strings.Replace(wholedatatxt, matchCollection15[num43].Groups[0].ToString(), text24, 1, 1, CompareMethod.Binary);
                    }
                }
                this.nboo = false;
                wholedatatxt = Strings.Replace(wholedatatxt, "$E", "</lnvxe:fnbody></lnvxe:footnote>\r\n</lnvxe:footnotegrp>", 1, -1, CompareMethod.Binary);
                MatchCollection matchCollection16 = Regex.Matches(wholedatatxt, "<lnvxe:blockquote>(.*?)</lnvxe:blockquote>", RegexOptions.Singleline);
                int num44 = 0;
                int num45 = matchCollection16.Count - 1;
                for (int num46 = num44; num46 <= num45; num46++)
                {
                    if (Operators.CompareString(matchCollection16[num46].Groups[0].ToString(), "", false) != 0)
                    {
                        string text26 = matchCollection16[num46].Groups[1].ToString();
                        text26 = Regex.Replace(text26, "\\$\\%\\$\\%", "<p><lnvxe:text>");
                        text26 = Regex.Replace(text26, "<p><lnvxe:text>(.*?)\n", "<p><lnvxe:text>$1</lnvxe:text></p>\r\n", RegexOptions.Singleline);
                        wholedatatxt = Strings.Replace(wholedatatxt, matchCollection16[num46].Groups[1].ToString(), text26, 1, 1, CompareMethod.Binary);
                    }
                }
                wholedatatxt = Regex.Replace(wholedatatxt, "\\$=S", "<lnvxe:blockquote>", RegexOptions.Singleline);
                wholedatatxt = Regex.Replace(wholedatatxt, "\\$=I", "</lnvxe:blockquote>", RegexOptions.Singleline);
                wholedatatxt = Regex.Replace(wholedatatxt, "\r\n\r\n", "\r\n");
                wholedatatxt = Regex.Replace(wholedatatxt, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
                wholedatatxt = Regex.Replace(wholedatatxt, @"(?m)[\s-[\r]]+\r?$", "", RegexOptions.Multiline);
                return wholedatatxt;
            }
        }
        ////////////////////////
        public interface IEnumerator
        {
            bool MoveNext();
            object Current { get; }
            void Reset();
        }



    }    

}
