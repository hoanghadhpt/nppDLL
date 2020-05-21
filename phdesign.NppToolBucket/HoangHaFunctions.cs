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

namespace phdesign.NppToolBucket
{
    internal static class HoangHaFunctions
    {
        internal static void BringFootnoteBack()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            if (string.IsNullOrEmpty(text)) return;




            text = Regex.Replace(text, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
        }

        internal static void EmptyFunc()
        {
            System.Windows.Forms.MessageBox.Show("Hề lú.\r\nBạn đang sử dụng Plugin của Hà đấy. Nhớ donate nhé....", "hehe");
        }

        internal static void RemoveDuplicatedLines()
        {

            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            if (string.IsNullOrEmpty(text)) return;

            string[] SplitTxt = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            SplitTxt = RemoveDuplicates(SplitTxt);
            string _New = string.Join(Environment.NewLine, SplitTxt);
            editor.SetDocumentText(_New);
            System.Windows.Forms.MessageBox.Show("Duplicate Lines Removed");
        }


        internal static void SmCapsTags()
        {
            var editor = Editor.GetActive();
            var text = editor.GetSelectedText();
            if (string.IsNullOrEmpty(text)) return;

            editor.SetSelectedText("<emph typestyle=\"smcaps\">" + Helpers.asTitleCase(text) + "</emph>");

        }

        internal static void NonVirgoPreProcess()
        {
            // Read all Doc.
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            if (string.IsNullOrEmpty(text)) return;
            // Begin process
            text = text.Replace("¶", "P");
            text = text.Replace(@"\P", "P");
            text = text.Replace("§§", " $Z ");
            text = text.Replace("§", " $S ");
            text = text.Replace("§", " $S ");
            text = text.Replace("[", "$(");
            text = text.Replace("]", "$)");
            text = text.Replace("{", "$(");
            text = text.Replace("}", "$)");
            text = text.Replace("½", " 1/2");
            text = text.Replace("¼", " 1/4");
            text = text.Replace("¼", " 1/4");
            text = text.Replace("‘", "'");
            text = text.Replace("’", "'");
            text = text.Replace("“", "\"");
            text = text.Replace("”", "\"");
            text = text.Replace("—", "--");
            text = text.Replace("–", "--");
            text = text.Replace("…", "...");
            text = text.Replace("•", ".");
            text = text.Replace("¢", " $(CENT$) ");
            text = text.Replace("£", " $(POUND$) ");
            text = text.Replace("©", " (C) ");
            text = text.Replace("®", " (R) ");
            text = text.Replace("±", "+/-");
            text = Regex.Replace(text, @"(\d+)--(\d+)", "$1-$2");

            //  ===> Valid Font

            text = text.Replace("$N\r\n$I", "\r\n");
            text = text.Replace("$N,\r\n$I", ",\r\n");
            text = text.Replace("v$N.\r\n$I", "v.\r\n");

            text = text.Replace("$=R\r\n$=B", "\r\n");
            text = text.Replace("$=R,\r\n$=B", ",\r\n");
            text = text.Replace("v$=R.\r\n$=B", "v.\r\n");

            text = text.Replace("$I\r\n$=U", "\r\n");
            text = text.Replace("$O,\r\n$U", ",\r\n");
            text = text.Replace("v$O.\r\n$U", "v.\r\n");

            text = text.Replace("“", "\"");
            text = text.Replace("”", "\"");
            text = text.Replace("‘", "\'");
            text = text.Replace("’", "\'");

            text = text.Replace("Id$N.", "Id.$N");
            text = text.Replace("id$N.", "id.$N");
            text = text.Replace(" seq$N.", " seq.$N");
            text = text.Replace(" seq$N.", " seq.$N");
            text = text.Replace("$N.,", ".$N,");
            text = text.Replace("$N; $I", "; ");
            text = text.Replace("$N, $I", ", ");
            text = text.Replace("$N $I", " ");

            text = text.Replace("Id$=R.", "Id.$=R");
            text = text.Replace("id$=R.", "id.$=R");
            text = text.Replace(" seq$=R.", " seq.$=R");
            text = text.Replace(" seq$=R.", " seq.$=R");
            text = text.Replace("$=R.,", ".$=R,");
            text = text.Replace("$=R; $=B", "; ");
            text = text.Replace("$=R, $=B", ", ");
            text = text.Replace("$=R $=B", " ");

            text = text.Replace("Id$O.", "Id.$O");
            text = text.Replace("id$O.", "id.$O");
            text = text.Replace(" seq$O.", " seq.$U");
            text = text.Replace(" seq$O.", " seq.$U");
            text = text.Replace("$O.,", ".$U,");
            text = text.Replace("$O; $U", "; ");
            text = text.Replace("$O, $U", ", ");
            text = text.Replace("$O $U", " ");

            text = text.Replace("$=R?", "?$=R");
            text = text.Replace("$N?", "?$N");
            text = text.Replace("$O?", "?$O");

            // Font Xuyen
            text = text.Replace("$=R $=B", " ");
            text = text.Replace("$N $I", " ");
            text = text.Replace("$O $U", " ");

            text = text.Replace("$=R. $=B", " ");
            text = text.Replace("$N. $I", " ");
            text = text.Replace("$O. $U", " ");

            text = text.Replace("$=R, $=B", " ");
            text = text.Replace("$N, $I", " ");
            text = text.Replace("$O, $U", " ");

            text = text.Replace("$=R v. $=B", " v. ");
            text = text.Replace("$N v. $I", " v. ");
            text = text.Replace("$O v. $U", " v. ");

            text = text.Replace("$=R. v. $=B", ". v. ");
            text = text.Replace("$N. v. $I", ". v. ");
            text = text.Replace("$O. v. $U", ". v. ");

            text = text.Replace("$=R, v. $=B", ". v. ");
            text = text.Replace("$N, v. $I", ". v. ");
            text = text.Replace("$O, v. $U", ". v. ");

            //SpaceAfterDollarAmount

            text = Regex.Replace(text, @"\$\ ?[+-]?[0-9]{1,3}(?:,?[0-9])*(?:\.[0-9]{1,2})?:", m => $"__{m.Groups[0].Value}");
            text = Regex.Replace(text, @"\$\ ?[+-]?[0-9]{1,3}(?:,?[0-9])*(?:\.[0-9]{1,2})?", m => $"$__{m.Groups[0].Value}");

            text = text.Replace("__$__$", "$");
            text = text.Replace("$__$", "$ ");

            // End Process
            editor.SetDocumentText(text);

        }


        internal static void CaselawVISFPreProcess()
        {
            // Read all Doc.
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            if (string.IsNullOrEmpty(text)) return;
            // Begin process
            text = text.Replace("¶", @"\P");
            text = text.Replace("§§", "$Z");
            text = text.Replace("§", " $S");
            text = text.Replace("½", " 1/2");
            text = text.Replace("¼", " 1/4");
            text = text.Replace("‘", "'");
            text = text.Replace("’", "'");
            text = text.Replace("“", "\"");
            text = text.Replace("”", "\"");
            text = text.Replace("—", "MDASH");
            text = text.Replace("–", "MDASH");
            text = text.Replace("…", "...");
            text = text.Replace("±", "+/-");
            text = Regex.Replace(text, @"(\d+)MDASH(\d+)", "$1-$2");
            text = Regex.Replace(text, @"(\d+)MDASH\r\n(\d+)", "$1-$2");
            //  ===> Valid Font

            text = text.Replace("$N\r\n$I", "\r\n");
            text = text.Replace("$N,\r\n$I", ",\r\n");
            text = text.Replace("v$N.\r\n$I", "v.\r\n");

            text = text.Replace("$=R\r\n$=B", "\r\n");
            text = text.Replace("$=R,\r\n$=B", ",\r\n");
            text = text.Replace("v$=R.\r\n$=B", "v.\r\n");

            text = text.Replace("$O\r\n$=U", "\r\n");
            text = text.Replace("$O,\r\n$U", ",\r\n");
            text = text.Replace("v$O.\r\n$U", "v.\r\n");

            text = text.Replace("“", "\"");
            text = text.Replace("”", "\"");
            text = text.Replace("‘", "\'");
            text = text.Replace("’", "\'");

            text = text.Replace("Id$N.", "Id.$N");
            text = text.Replace("id$N.", "id.$N");
            text = text.Replace(" seq$N.", " seq.$N");
            text = text.Replace(" seq$N.", " seq.$N");
            text = text.Replace("$N.,", ".$N,");
            text = text.Replace("$N; $I", "; ");
            text = text.Replace("$N, $I", ", ");
            text = text.Replace("$N $I", " ");

            text = text.Replace("Id$=R.", "Id.$=R");
            text = text.Replace("id$=R.", "id.$=R");
            text = text.Replace(" seq$=R.", " seq.$=R");
            text = text.Replace(" seq$=R.", " seq.$=R");
            text = text.Replace("$=R.,", ".$=R,");
            text = text.Replace("$=R; $=B", "; ");
            text = text.Replace("$=R, $=B", ", ");
            text = text.Replace("$=R $=B", " ");

            text = text.Replace("Id$O.", "Id.$O");
            text = text.Replace("id$O.", "id.$O");
            text = text.Replace(" seq$O.", " seq.$U");
            text = text.Replace(" seq$O.", " seq.$U");
            text = text.Replace("$O.,", ".$U,");
            text = text.Replace("$O; $U", "; ");
            text = text.Replace("$O, $U", ", ");
            text = text.Replace("$O $U", " ");

            text = text.Replace("$=R?", "?$=R");
            text = text.Replace("$N?", "?$N");
            text = text.Replace("$O?", "?$O");

            // Font Xuyen
            text = text.Replace("$=R $=B", " ");
            text = text.Replace("$N $I", " ");
            text = text.Replace("$O $U", " ");

            text = text.Replace("$=R. $=B", " ");
            text = text.Replace("$N. $I", " ");
            text = text.Replace("$O. $U", " ");

            text = text.Replace("$=R, $=B", " ");
            text = text.Replace("$N, $I", " ");
            text = text.Replace("$O, $U", " ");

            text = text.Replace("$=R v. $=B", " v. ");
            text = text.Replace("$N v. $I", " v. ");
            text = text.Replace("$O v. $U", " v. ");

            text = text.Replace("$=R. v. $=B", ". v. ");
            text = text.Replace("$N. v. $I", ". v. ");
            text = text.Replace("$O. v. $U", ". v. ");

            text = text.Replace("$=R, v. $=B", ". v. ");
            text = text.Replace("$N, v. $I", ". v. ");
            text = text.Replace("$O, v. $U", ". v. ");

            //SpaceAfterDollarAmount

            text = Regex.Replace(text, @"\$\ ?[+-]?[0-9]{1,3}(?:,?[0-9])*(?:\.[0-9]{1,2})?:", m => $"__{m.Groups[0].Value}");
            text = Regex.Replace(text, @"\$\ ?[+-]?[0-9]{1,3}(?:,?[0-9])*(?:\.[0-9]{1,2})?", m => $"$__{m.Groups[0].Value}");

            text = text.Replace("__$__$", "$");
            text = text.Replace("$__$", "$ ");

            // End Process
            editor.SetDocumentText(text);

        }


        internal static void NewFontProcess()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            if (string.IsNullOrEmpty(text)) return;

            text = text.Replace(" <i>", "<i> ");
            text = text.Replace(" <b>", "<b> ");
            text = text.Replace(" <u>", "<u> ");
            text = text.Replace(" <s>", "<s> ");

            text = text.Replace(",</i>", "</i>,");
            text = text.Replace(",</b>", "</b>,");
            text = text.Replace(",</u>", "</u>,");
            text = text.Replace(",</s>", "</s>,");

            text = text.Replace("., </i>", ".</i>, ");
            text = text.Replace("., </b>", ".</b>, ");
            text = text.Replace("., </u>", ".</u>, ");
            text = text.Replace("., </s>", ".,</s> ");

            text = text.Replace("<i> ", " <i>");
            text = text.Replace("<b> ", " <b>");
            text = text.Replace("<u> ", " <u>");
            text = text.Replace("<s> ", " <s>");

            text = text.Replace(" </i>", "</i> ");
            text = text.Replace(" </b>", "</b> ");
            text = text.Replace(" </u>", "</u> ");
            text = text.Replace(" </s>", "</s> ");

            text = text.Replace("<i>", "$I");
            text = text.Replace("<u>", "$U");
            text = text.Replace("<b>", "$=B");
            text = text.Replace("<s>", "<DEL>");

            text = text.Replace("</i>", "$N");
            text = text.Replace("</u>", "$O");
            text = text.Replace("</b>", "$=R");
            text = text.Replace("</s>", "</DEL>");

            text = Regex.Replace(text, @"<n>(\d+)</n>", " n$1");
            text = text.Replace("<n>", "");
            text = text.Replace("</n>", "");

            //Validate fonts
            text = text.Replace("$U$O", "");
            text = text.Replace("$=B$=R", "");
            text = text.Replace("$I$N", "");
            text = text.Replace("\r\n$=R", "$=R\r\n");
            text = text.Replace("\r\n$N", "$N\r\n");
            text = text.Replace("\r\n$O", "$O\r\n");

            text = text.Replace(",$=R", "$=R,");
            text = text.Replace(",$N", "$N,");
            text = text.Replace(",$O", "$O,");

            text = text.Replace(".$=R", "$=R.");
            text = text.Replace(".$N", "$N.");
            text = text.Replace(".$O", "$O.");


            editor.SetDocumentText(text);
        }

        internal static void SpacingFormat()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            if (string.IsNullOrEmpty(text)) return;
            // Begin process

            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            Regex regex2 = new Regex(@"\s+\r\n", options);
            text = regex.Replace(text, " ");
            text = regex2.Replace(text, "\r\n");
            text.Trim();

            editor.SetDocumentText(text);
        }

        internal static void AddDel()
        {
            // Read all Doc.
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            if (string.IsNullOrEmpty(text)) return;
            // Begin process

            text = Regex.Replace(text, @"<ADD>(.*?)</ADD>", "$(A> $1 <A$)", RegexOptions.Singleline);
            text = Regex.Replace(text, @"<DEL>(.*?)</DEL>", "$(D> $1 <D$)", RegexOptions.Singleline);

            editor.SetDocumentText(text);

        }

        internal static void AddUpperCase()
        {
            // Read all Doc.
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            if (string.IsNullOrEmpty(text)) return;
            // Begin process

            text = Regex.Replace(text, @"\$\(A> (.*?) <A\$\)", m => $@"$(A> {m.Groups[1].Value.ToUpper()} <A$)");

            editor.SetDocumentText(text);

        }

        internal static void RemovePeriodFootnote()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            if (string.IsNullOrEmpty(text)) return;
            // Begin process
            text = Regex.Replace(text, @"\$Fn(\d+)\. ", m => $@"$Fn{m.Groups[1].Value.ToUpper()} ");
            editor.SetDocumentText(text);
        }




        internal static void AutoPredictPara()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            if (string.IsNullOrEmpty(text)) return;
            // Begin process
            text = Regex.Replace(text, @"\$Fn(\d+)\. ", m => $@"$Fn{m.Groups[1].Value.ToUpper()} ");

            foreach (string s in PredictIndent)
            {
                text = text.Replace("\r\n" + s, "\r\n$T" + s);
            }

            foreach (string s in PredictNotIndent)
            {
                text = text.Replace("\r\n" + s, "\r\n$T" + s);
            }

            editor.SetDocumentText(text);

        }


        internal static void FootnoteLinkNumber()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            if (string.IsNullOrEmpty(text)) return;

            text = Regex.Replace(text, @" n(\d+)", @" $=<$=T1*.fo_number(_@NUMBER@_)_and_footnotes(n$1);.vk $?n$1$=>");

            editor.SetDocumentText(text);
        }

        internal static void FootnoteLinkCite()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            if (string.IsNullOrEmpty(text)) return;

            text = Regex.Replace(text, @" n(\d+)", @" $=T1*.fo_cite(_@CITE@_)_and_footnotes(n$1);.vk $?n$1");

            editor.SetDocumentText(text);
        }

        internal static void FootnoteSID()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            if (string.IsNullOrEmpty(text)) return;

            text = Regex.Replace(text, @" n(\d+)", @" $=T1*.fo_#AUDIT-TRAIL(#SID_@@_#)_and_footnotes(n$1);.vk $?n$1");

            editor.SetDocumentText(text);
        }

        //Note for hyphen breaklines ""               (?i)([a-z]+)-\n([a-z]+)         || word-word               ((?:\w+-)+\w+)

        internal static string[] RemoveDuplicates(string[] s)
        {
            HashSet<string> set = new HashSet<string>(s);
            string[] result = new string[set.Count];
            set.CopyTo(result);
            return result;
        }

        internal static void AllListT()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            if (string.IsNullOrEmpty(text)) return;

            text = Regex.Replace(text, @"\r\n(\d+)\. ", "\r\n$T$1. ");
            text = Regex.Replace(text, @"\r\n([A-Za-z])\. ", "\r\n$T$1. ");
            text = Regex.Replace(text, @"\r\n(i|ii|iii|iv|v|vi|vii|viii|ix|x|xi|xii|xiii|xiv|xv|xvi|xvii|xviii|xix|xx)\. ", "\r\n$T$1. ");
            text = Regex.Replace(text, @"\r\n\(([A-Za-z])\.\) ", "\r\n$T($1.) ");
            text = Regex.Replace(text, @"\r\n\((\d+)\.\) ", "\r\n$T($1.) ");
            text = Regex.Replace(text, @"\r\n\((\d+)\) ", "\r\n$T($1) ");
            text = Regex.Replace(text, @"\r\n\(([A-Za-z]{1,2})\) ", "\r\n$T($1) ");

            editor.SetDocumentText(text);
        }

        internal static void AllListNonT()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            if (string.IsNullOrEmpty(text)) return;

            text = Regex.Replace(text, @"\r\n(\d+)\. ", "\r\n$%$%$1. ");
            text = Regex.Replace(text, @"\r\n([A-Za-z])\. ", "\r\n$%$%$1. ");
            text = Regex.Replace(text, @"\r\n(i|ii|iii|iv|v|vi|vii|viii|ix|x|xi|xii|xiii|xiv|xv|xvi|xvii|xviii|xix|xx)\. ", "\r\n$%$%$1. ");
            text = Regex.Replace(text, @"\r\n\(([A-Za-z])\.\) ", "\r\n$%$%($1.) ");
            text = Regex.Replace(text, @"\r\n\((\d+)\.\) ", "\r\n$%$%($1.) ");
            text = Regex.Replace(text, @"\r\n\((\d+)\) ", "\r\n$%$%($1) ");
            text = Regex.Replace(text, @"\r\n\(([A-Za-z]{1,2})\) ", "\r\n$%$%($1) ");

            editor.SetDocumentText(text);
        }

        internal static void FootnoteLinkCount()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Empty Document");
                return;
            }
            else
            {
                List<string> indicator = new List<string>();
                List<string> body = new List<string>();

                MatchCollection mIndicators = Regex.Matches(text, @"vk \$\?n(\d+)");
                foreach (Match m1 in mIndicators)
                {
                    indicator.Add(m1.Groups[1].ToString());
                }

                MatchCollection mBody = Regex.Matches(text, @"\$F(.*)(\d+) ");
                foreach (Match m1 in mBody)
                {
                    body.Add(m1.Groups[2].ToString());
                }
                var list3 = body.Where(o => !indicator.Contains(o)).ToList();
                var list4 = indicator.Where(o => !body.Contains(o)).ToList();


                string temp = "";
                string temp2 = "";
                foreach (string x in list3)
                {
                    temp += "$F___" + x + "\r\n";
                }
                foreach (string x in list4)
                {
                    temp2 += "n" + x + "\r\n";
                }
                MessageBox.Show("\tBody: \r\n\t\t" + temp + "\r\n" + "\tIndicator: \r\n\t\t" + temp2);
            }
        }

        internal static void FootnoteVisfRenumber()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            var pos = editor.GetSelectionLength();
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Empty Document");
                return;
            }
            else
            {
                string myValue = Interaction.InputBox("Enter Start Number", "Input Value to Renumber", "", 100, 100);
                int i = Convert.ToInt32(myValue) - 1;
                //string result = "";
                //result = Regex.Replace(text,@" n(\d+)", (Match n) =>
                //{
                //    result = string.Format(" n{0}", ++i);
                //    return(result);
                //});

                text = VisfRenumberIndicator(text, i);
                text = VisfRenumberBody(text, i);

                editor.SetDocumentText(text);
            }
        }

        internal static void PaginationRenumber()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Empty Document");
                return;
            }
            else
            {
                string myValue = Interaction.InputBox("Enter Start Number", "Input Value to Renumber", "", 100, 100);
                int i = Convert.ToInt32(myValue) - 1;

                text = Pagination(text, i);

                editor.SetDocumentText(text);
            }
        }

        public static string Pagination(string text, int i)
        {
            string result = Regex.Replace(text, @"\$=P(\d+)", (Match n) =>
            {
                result = string.Format("$=P{0}", ++i);
                return (result);
            });
            return result;
        }

        public static string VisfRenumberIndicator(string text, int i)
        {
            string result = Regex.Replace(text, @" n(\d+)", (Match n) =>
            {
                result = string.Format(" n{0}", ++i);
                return (result);
            });
            return result;
        }
        public static string VisfRenumberBody(string text, int i)
        {
            string result = Regex.Replace(text, @"\$F(.*?)n(\d+)", (Match n) =>
            {
                result = string.Format("$F{1}n{0}", ++i, n.Groups[1].Value);
                return (result);
            });
            return result;
        }




        internal static void SegmentsSort()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();

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
            editor.SetDocumentText(tempAllFiles);

            MessageBox.Show("Segments Sorted.");
        }

        internal static void NewParaFormat()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();

            text = Regex.Replace(text, @"\r\n\r\n([A-Z])", "\r\n\r\n$T$1");
            text = Regex.Replace(text, @"\r\n\r\n\$=B(.*?)\$=R\r\n", "\r\n\r\n$=H$=B$1$=R$=E\r\n");


            editor.SetDocumentText(text);
        }

        internal static void RemoveFonts()
        {
            var editor = Editor.GetActive();
            var text = editor.GetSelectedText();

            var post = editor.GetSelectionLength();
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Select text before process");
                return;
            }
            else
            {
                text = text.Replace("$=B", "");
                text = text.Replace("$=R", "");
                text = text.Replace("$U", "");
                text = text.Replace("$O", "");
                text = text.Replace("$I", "");
                text = text.Replace("$N", "");
                text = text.Replace("$W", "");
                text = text.Replace("$K", "");
                text = text.Replace("$=S", "");
                text = text.Replace("$=I", "");
                text = text.Replace("$=H", "");
                text = text.Replace("$=E", "");
                text = text.Replace("$T", "");
                text = text.Replace("$%", "");
            }

            editor.SetSelectedText(text);

        }


        public static List<string> bodylist = new List<string>();
        internal static void FootnoteToEnd()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();


            string[] listFiles = Regex.Split(text, @"(?=\$00:)");
            string tempAllFiles = "";
            foreach (string file in listFiles)
            {
                bodylist.Clear();
                string temp = file;
                string pattern = @"\$F(.*?)\$E";
                RegexOptions options = RegexOptions.Singleline;
                // Remove From Body
                foreach (Match m in Regex.Matches(temp, pattern, options))
                {
                    bodylist.Add(m.Value.ToString());
                    temp = temp.Replace(m.Value, "");
                }

                foreach (string body in bodylist)
                {
                    //System.Windows.Forms.MessageBox.Show(body);
                    temp += "\r\n" + body + "\r\n";
                }
                // Remove Empty Lines =>> beauty document
                temp = Regex.Replace(temp, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);

                tempAllFiles += temp;
            }
            editor.SetDocumentText(tempAllFiles);
            System.Windows.Forms.MessageBox.Show("Moved to end of document.", "Footnote to End", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }

        internal static void FootnoteReturn()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Emply Document.");
                return;
            }
            else
            {
                text = text.Replace("\r\n", "[!!]");
                text = text.Replace("$=I[!!]$%$%", "$=I[!!]___$%$%");
                text = text.Replace("[!!]$T", "\r\n$T");
                text = text.Replace("[!!]$%$%", "\r\n$%$%");


                text = Regex.Replace(text, @"\[!!\](\$(\d+):)", "\r\n$1");

                bodylist.Clear();

                //GetBODY

                foreach (Match m in Regex.Matches(text, @"\$F(.*?)\$E", RegexOptions.Singleline))
                {
                    bodylist.Add(m.Value.ToString());
                }
                for (int i = 0; i < bodylist.Count; i++)
                {
                    text = text.Replace(bodylist[i], "");
                }


                // Get Indicator
                foreach (Match m in Regex.Matches(text, ".* n([0-9]{1,5}).*"))
                {
                    string linecontainindicator = m.Value;
                    string pattern2 = " (n[0-9]{1,5})";
                    foreach (Match m2 in Regex.Matches(linecontainindicator, pattern2))
                    {
                        linecontainindicator += "<" + m2.Value + ">";
                    }
                    text = text.Replace(m.Value, linecontainindicator);
                }

                var filename = editor.GetCurrentFileName();
                var Dir = editor.GetCurrentDirectory();
                // Main Function
                string tmp = "";
                foreach (string body in bodylist)
                {
                    tmp += body + "\r\n";
                    string pattern2 = "(n[0-9]{1,5})";
                    string indicator_final = "";
                    foreach (Match m2 in Regex.Matches(body, pattern2))
                    {
                        indicator_final = "< " + m2.Value + ">";
                        text = text.Replace(indicator_final, "\r\n" + body + "\r\n");
                    }
                    
                }

                File.WriteAllText(Dir + "\\" + Path.GetFileNameWithoutExtension(filename) + "_BodyFootnote.H.Ha", "");
                File.AppendAllText(Dir + "\\" + Path.GetFileNameWithoutExtension(filename) + "_BodyFootnote.H.Ha", tmp.Replace("[!!]","\r\n"));

                text = text.Replace("[!!]___$%$%", "\r\n$%$%");
                text = text.Replace("[!!]", "\r\n");

                editor.SetDocumentText(text);
                MessageBox.Show("Footnote Returned.");
            }
        }

        internal static void SplitFiles()
        {
            var editor = Editor.GetActive();
            var filename = editor.GetCurrentFileName();
            var Dir = editor.GetCurrentDirectory();
            var text = editor.GetDocumentText();

            string[] listFiles = Regex.Split(text, @"(?=\$00:)");
            int i = 0;
            foreach (string f in listFiles)
            {
                if (listFiles.Length < 100)
                {
                    if (i < 10)
                    {
                        File.WriteAllText(Dir + "\\" + Path.GetFileNameWithoutExtension(filename) + "(0" + i + ").visf", f, Encoding.GetEncoding("ISO-8859-1"));
                        i++;
                    }
                    else
                    {
                        File.WriteAllText(Dir + "\\" + Path.GetFileNameWithoutExtension(filename) + "(" + i + ").visf", f, Encoding.GetEncoding("ISO-8859-1"));
                        i++;
                    }
                }
                else
                {
                    if (i < 10)
                    {
                        File.WriteAllText(Dir + "\\" + Path.GetFileNameWithoutExtension(filename) + "(00" + i + ").visf", f, Encoding.GetEncoding("ISO-8859-1"));
                        i++;
                    }
                    else if (i < 100)
                    {
                        File.WriteAllText(Dir + "\\" + Path.GetFileNameWithoutExtension(filename) + "(0" + i + ").visf", f, Encoding.GetEncoding("ISO-8859-1"));
                        i++;
                    }
                    else
                    {
                        File.WriteAllText(Dir + "\\" + Path.GetFileNameWithoutExtension(filename) + "(" + i + ").visf", f, Encoding.GetEncoding("ISO-8859-1"));
                        i++;
                    }
                }
            }
            MessageBox.Show("Spilited into " + (listFiles.Length - 1) + " file(s).");
        }

        internal static void OpenAllDocuments()
        {
            var editor = Editor.GetActive();
            int totalopenfiles = editor.GetAllOpenFiles()-1;
            for (int i = 0; i < totalopenfiles; i++)
            {
                editor.ActiveDocumentByIndex(i);
                var text = editor.GetDocumentLength();
                MessageBox.Show(i.ToString() + "\r\n" + text);
            }
        }


        // Class - Somethings else
        public static List<string> PredictIndent = new List<string>
        {
            "Therefore, ", "Although ", "When ", "What ", "10) ", "1) ", "2) ", "3) ", "4) ", "5) ", "6) ", "7) ", "8) ", "9) ", "11) ", "12) ", "13) ", "14) ", "15) ", "16) ", "Following ", "After ", "An ", "Another ", "Basing ", "We ", "Other ", "One ", "To ", "$=BNote$=R: ", "$=BExample ", "At ", "*** ", "***", "* * *", "...", ". . .", "__1. ", "__2. ", "__3. ", "__4. ", "__5. ", "__6. ", "__7. ", "__8. ", "__9. ", "__11. ", "__20. ", "__12. ", "__13. ", "__14. ", "__15. ", "__16. ", "__17. ", "__18. ", "__19. ", "__21. ", "__30. ", "__22. ", "__23. ", "__24. ", "__25. ", "__26. ", "__27. ", "__28. ", "__29. ", "__31. ", "IT IS ", "$=BIT IS", "DATED: ", "HONORABLE ", "UNITED STATES ", "Third", "The ", "By ", "There ", "Under ", "These ", "A ", "As ", "$II. ", "$III. ", "$IIII. ", "$IIV. ", "$IV. ", "$IVI. ", "$IVII. ", "$IVIII. ", "$IIX. ", "$IX. ", "Fifth,", "I. ", "II. ", "III. ", "IV. ", "V. ", "VI. ", "VII. ", "VIII. ", "IX. ", "X. ", "A. ", "$IA. ", "$IB. ", "$IC. ", "$ID. ", "$IE. ", "$IF. ", "$IG. ", "$IH. ", "$IJ. ", "$IK. ", "1. ", "$I1. ", "$I2. ", "$I3. ", "$I4. ", "$I5. ", "$I6. ", "$I7. ", "$I8. ", "$I9. ", "$I11. ", "$I20. ", "$I12. ", "$I13. ", "$I14. ", "$I15. ", "$I16. ", "$I17. ", "$I18. ", "$I19. ", "$I21. ", "$I30. ", "$I22. ", "$I23. ", "$I24. ", "$I25. ", "$I26. ", "$I27. ", "$I28. ", "$I29. ", "$I31. ", "$IL. ", "$IM. ", "$IN. ", "$IO. ", "$IP. ", "$IQ. ", "$IR. ", "$IS. ", "$IT. ", "$IU. ", "$IW. ", "Respectfully submitted", "Dated:", "$ICERTIFICATE OF", "Thus, ", "Finally, ", "Grounds ", "For ", "Furthermore ", "With ", "Accordingly,", "Accordingly ", "Moreover,", "Specifically,", "(Ex. ", "Even ", "Here, ", "Of ", "This ", "Where ", "While ", "Without ", "Email:", "Each ", "Also,", "By:", "Consequently,", "During", "Fourth,", "Hence,", "However,", "If ", "I hereby", "Independent ", "Moreover, ", "Of these,", "ORDERED", "$IORDERED", "FURTHER", "$IFURTHER", "Sixth,", "Seventh,", "Email: ", "Telephone:", "Facsimile:", "$U/s/ ", "CO[2]__", "$I$UA. ", "$I$UB. ", "$I$UC. ", "$I$UD. ", "$I$UE. ", "$I$UF. ", "$I$UG. ", "$I$UH. ", "$I$UJ. ", "$I$UK. ", "$I$UL. ", "$I$UM. ", "$I$UO. ", "$I$UP. ", "$I$UQ. ", "$I$UR. ", "$I$US. ", "$I$UT. ", "$I$UU. ", "$I$UV. ", "$I$UI. ", "$I$UII. ", "$I$UIII. ", "$I$UIV. ", "$I$UVI. ", "$I$UVII. ", "$I$UVIII. ", "$I$UIX. ", "$I$UX. ", "In ", "Those ", "It ", "On ", "First", "Second", "Pursuant to", "$=BI. ", "$=BII. ", "$=BIII. ", "$=BIV. ", "$=BV. ", "$=BVI. ", "$=BVII. ", "$=BVIII. ", "$=BIX. ", "$=BX. ", "$=BA. ", "$=BB. ", "$=BC. ", "$=BD. ", "$=BE. ", "$=BF. ", "$=BG. ", "$=BH. ", "$=BJ. ", "$=BK. ", "$=B1. ", "$=B2. ", "$=B3. ", "$=B4. ", "$=B5. ", "$=B6. ", "$=B7. ", "$=B8. ", "$=B9. ", "$=B11. ", "$=B20. ", "$=B12. ", "$=B13. ", "$=B14. ", "$=B15. ", "$=B16. ", "$=B17. ", "$=B18. ", "$=B19. ", "$=B21. ", "$=B30. ", "$=B22. ", "$=B23. ", "$=B24. ", "$=B25. ", "$=B26. ", "$=B27. ", "$=B28. ", "$=B29. ", "$=B31. ", "$=BL. ", "$=BM. ", "$=BN. ", "$=BO. ", "$=BP. ", "$=BQ. ", "$=BR. ", "$=BS. ", "$=BT. ", "$=BU. ", "$=BW. ", "$=BCERTIFICATE OF", "$=BORDERED", "$=BFURTHER", "$=B$IA. ", "$=B$IB. ", "$=B$IC. ", "$=B$ID. ", "$=B$IE. ", "$=B$IF. ", "$=B$IG. ", "$=B$IH. ", "$=B$IJ. ", "$=B$IK. ", "$=B$IL. ", "$=B$IM. ", "$=B$IO. ", "$=B$IP. ", "$=B$IQ. ", "$=B$IR. ", "$=B$IS. ", "$=B$IT. ", "$=B$IU. ", "$=B$IV. ", "$=B$II. ", "$=B$III. ", "$=B$IIII. ", "$=B$IIV. ", "$=B$IVI. ", "$=B$IVII. ", "$=B$IVIII. ", "$=B$IIX. ", "$=B$IX. ", "/s/", "$=BIT IS SO ", "$=BCONCLUSION$=R", "Reg. No.", "$I/s/"
        };

        public static List<string> PredictNotIndent = new List<string>
        {
            "THE COURT: ", "MR. ", "Date: ", "Judgment Entered", "Q. ", "A. ", "Q: ", "A: ", "$=BFrom$=R: ", "$=BSent$=R: ", "$=BTo$=R: ", "$=BCc$=R: ", "$=BSubject$=R: ", "Dated:"
        };

        public class MyComparer : IComparer<string>
        {

            [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
            static extern int StrCmpLogicalW(String x, String y);

            public int Compare(string x, string y)
            {
                return StrCmpLogicalW(x, y);
            }

        }

        private static bool IsViewVisible(int targetView)
        {
            int currentView;
            Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETCURRENTSCINTILLA, 0, out currentView);
            // If the view is active it must be visible
            if (currentView == targetView) return true;
            var currentDocIndex = (int)Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETCURRENTDOCINDEX, 0, currentView);
            var currentDocIndexTargetView = (int)Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETCURRENTDOCINDEX, 0, targetView);

            // Try switching to other view, if that fails it must be hidden.
            Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_ACTIVATEDOC, targetView, currentDocIndexTargetView);
            int newView;
            Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETCURRENTSCINTILLA, 0, out newView);

            // Restore active doc
            Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_ACTIVATEDOC, currentView, currentDocIndex);

            return newView == targetView;
        }

    }
}
