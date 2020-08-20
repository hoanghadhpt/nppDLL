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
    internal static class HoangHaFunctions
    {
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
            var pos = editor.GetSelectionRange();
            if (string.IsNullOrEmpty(text)) return;

            editor.SetSelectedText("<emph typestyle=\"smcaps\">" + Helpers.asTitleCase(text) + "</emph>");
            editor.SetSelection(pos.cpMin, pos.cpMin + text.Length + 32);

        }

        internal static void NonVirgoPreProcess()
        {
            // Read all Doc.
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            var CurrentPos = editor.GetCurrentPosition();

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

            text = text.Replace("$I\r\n$U", "\r\n");
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
            text = text.Replace(" seq$O.", " seq.$O");
            text = text.Replace(" seq$O.", " seq.$O");
            text = text.Replace("$O.,", ".$O,");
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

            text = text.Replace("$=R,\r\n$=B", ",\r\n");
            text = text.Replace("$N,\r\n$I", ",\r\n");
            text = text.Replace("$O,\r\n$U", ",\r\n");

            text = text.Replace("$N\r\nv. $I", "\r\nv. ");
            text = text.Replace("$O\r\nv. $U", "\r\nv. ");
            text = text.Replace("$=R\r\nv. $=B", "\r\nv. ");

            text = text.Replace("$N. v.\r\n$I", ". v.\r\n");
            text = text.Replace("$O. v.\r\n$U", ". v.\r\n");
            text = text.Replace("$=R. v.\r\n$=B", ". v.\r\n");

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

            text = text.Replace("$=R. v $=B", ". v. ");
            text = text.Replace("$N. v $I", ". v. ");
            text = text.Replace("$O. v $U", ". v. ");

            text = text.Replace("$=R, v $=B", ". v. ");
            text = text.Replace("$N, v $I", ". v. ");
            text = text.Replace("$O, v $U", ". v. ");

            text = text.Replace("$=R v $=B", ". v. ");
            text = text.Replace("$N v $I", ". v. ");
            text = text.Replace("$O v $U", ". v. ");

            text = text.Replace("$N.\r\n$Iv. ", ".\r\nv. ");
            text = text.Replace("$O.\r\n$Uv. ", ".\r\nv. ");
            text = text.Replace("$=R.\r\n$=Bv. ", ".\r\nv. ");

            text = text.Replace("$=R\r\n$=B", "\r\n");
            text = text.Replace("$N\r\n$I", "\r\n");
            text = text.Replace("$O\r\n$U", "\r\n");

            text = text.Replace("$I$N", "");
            text = text.Replace("$=B$=R", "");
            text = text.Replace("$U$O", "");

            text = text.Replace("$I.$N", ".");
            text = text.Replace("$=B.$=R", ".");
            text = text.Replace("$U.$O", ".");

            text = text.Replace("$N--$I", "--");
            text = text.Replace("$=R--$=B", "--");
            text = text.Replace("$O--$U", "--");

            text = text.Replace("$I, ", ", $I");
            text = text.Replace("$=B, ", ", $=B");
            text = text.Replace("$U, ", ", $U");

            text = text.Replace(". . .$N", "$N. . .");
            text = text.Replace(". .$N .", "$N. . .");
            text = text.Replace(".$N . .", "$N. . .");


            text = Regex.Replace(text, @"(\d+)\-\-\r\n(\d+)", "$1-$2");
            text = Regex.Replace(text, @"(\d+)\-\r\n(\d+)", "$1-$2");
            text = Regex.Replace(text, @"\$=P(\d+)\r\n", "$=P$1 ");

            //SpaceAfterDollarAmount

            text = Regex.Replace(text, @"\$\ ?[+-]?[0-9]{1,3}(?:,?[0-9])*(?:\.[0-9]{1,2})?:", m => $"__{m.Groups[0].Value}");
            text = Regex.Replace(text, @"\$\ ?[+-]?[0-9]{1,3}(?:,?[0-9])*(?:\.[0-9]{1,2})?", m => $"$__{m.Groups[0].Value}");

            text = text.Replace("__$__$", "$");
            text = text.Replace("$__$", "$ ");

            // Xoa Mac Dinh Doan Thua
            text = text.Replace("$%$%$T", "$%$%");
            text = text.Replace("$%$%$=H", "$%$%");
            text = text.Replace("$T$=H", "$T");
            text = text.Replace("$T$%", "$T");
            text = text.Replace("$%$%$%$%", "$%$%");
            text = text.Replace("$%$%$%", "$%$%");
            text = text.Replace("$T$T", "$T");
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
            text = text.Replace("§", "$S");
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
            text = text.Replace("$N\r\n$Iv.", "\r\nv.");

            text = text.Replace("$=R\r\n$=B", "\r\n");
            text = text.Replace("$=R,\r\n$=B", ",\r\n");
            text = text.Replace("v$=R.\r\n$=B", "v.\r\n");
            text = text.Replace("$=R\r\n$=Bv.", "\r\nv.");

            text = text.Replace("$O\r\n$U", "\r\n");
            text = text.Replace("$O,\r\n$U", ",\r\n");
            text = text.Replace("v$O.\r\n$U", "v.\r\n");
            text = text.Replace("$O\r\n$Uv.", "\r\nv.");

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
            text = text.Replace(" seq$O.", " seq.$O");
            text = text.Replace("$O.,", ".$O,");
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

            text = text.Replace("$=R. $=B", ". ");
            text = text.Replace("$N. $I", ". ");
            text = text.Replace("$O. $U", ". ");

            text = text.Replace("$=R, $=B", " ");
            text = text.Replace("$N, $I", " ");
            text = text.Replace("$O, $U", " ");

            text = text.Replace("$=R,\r\n$=B", ",\r\n");
            text = text.Replace("$N,\r\n$I", ",\r\n");
            text = text.Replace("$O,\r\n$U", ",\r\n");

            text = text.Replace("$=R v. $=B", " v. ");
            text = text.Replace("$N v. $I", " v. ");
            text = text.Replace("$O v. $U", " v. ");

            text = text.Replace("$=R. v. $=B", ". v. ");
            text = text.Replace("$N. v. $I", ". v. ");
            text = text.Replace("$O. v. $U", ". v. ");

            text = text.Replace("$=R. v $=B", ". v. ");
            text = text.Replace("$N. v $I", ". v. ");
            text = text.Replace("$O. v $U", ". v. ");

            text = text.Replace("$=R, v $=B", ". v. ");
            text = text.Replace("$N, v $I", ". v. ");
            text = text.Replace("$O, v $U", ". v. ");

            text = text.Replace("$=R v $=B", ". v. ");
            text = text.Replace("$N v $I", ". v. ");
            text = text.Replace("$O v $U", ". v. ");

            text = text.Replace("$=R, v. $=B", ", v. ");
            text = text.Replace("$N, v. $I", ", v. ");
            text = text.Replace("$O, v. $U", ", v. ");

            text = text.Replace("$I$N", "");
            text = text.Replace("$=B$=R", "");
            text = text.Replace("$U$O", "");

            text = text.Replace("$I.$N", ".");
            text = text.Replace("$=B.$=R", ".");
            text = text.Replace("$U.$O", ".");


            text = text.Replace("$N.\r\n$Iv. ", ".\r\nv. ");
            text = text.Replace("$O.\r\n$Uv. ", ".\r\nv. ");
            text = text.Replace("$=R.\r\n$=Bv. ", ".\r\nv. ");

            text = text.Replace("$N\r\nv. $I", "\r\nv. ");
            text = text.Replace("$O\r\nv. $U", "\r\nv. ");
            text = text.Replace("$=R\r\nv. $=B", "\r\nv. ");

            text = text.Replace("$N. v.\r\n$I", ". v.\r\n");
            text = text.Replace("$O. v.\r\n$U", ". v.\r\n");
            text = text.Replace("$=R. v.\r\n$=B", ". v.\r\n");


            text = Regex.Replace(text, @"(\d+)-\r\n(\d+)", "$1-$2");
            text = Regex.Replace(text, @"(\d+)- (\d+)", "$1-$2");
            text = Regex.Replace(text, @"(\d+)MDASH\r\n(\d+)", "$1-$2");
            text = Regex.Replace(text, @"(\d+)MDASH(\d+)", "$1-$2");
            // Xoa Mac Dinh Doan Thua

            // Xoa Mac Dinh Doan Thua
            text = text.Replace("$%$%$T", "$%$%");
            text = text.Replace("$%$%$=H", "$%$%");
            text = text.Replace("$T$=H", "$T");
            text = text.Replace("$T$%", "$T");
            text = text.Replace("$%$%$%$%", "$%$%");
            text = text.Replace("$%$%$%", "$%$%");
            text = text.Replace("$T$T", "$T");

            text = text.Replace("$=H$%$%", "$=H");
            text = text.Replace("$=H$T", "$=H");

            // End Process
            editor.SetDocumentText(text);

        }


        internal static void NewFontProcess()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            if (string.IsNullOrEmpty(text)) return;

            text = text.Replace("<I>", "<i>");
            text = text.Replace("<B>", "<b>");
            text = text.Replace("<U>", "<u>");
            text = text.Replace("<S>", "<s>");
            text = text.Replace("<N>", "<s>");

            text = text.Replace("</I>", "</i>");
            text = text.Replace("</B>", "</b>");
            text = text.Replace("</U>", "</u>");
            text = text.Replace("</S>", "</s>");
            text = text.Replace("</N>", "</s>");


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

            text = text.Replace("$I. ", ". $I");
            text = text.Replace("$=B. ", ". $=B");
            text = text.Replace("$U. ", ". $U");

            // Font I footnote 
            text = text.Replace("\r\n$E\r\n$Fn$N", "$N\r\n$E\r\n$Fn");


            // Footnote BODY =

            text = Regex.Replace(text, @"\r\n n(\d+) (.*?)\r\n\r\n\r\n", "\r\n$F>FTNT>$Tn$1 $2>ENDFN>$E\r\n\r\n\r\n", RegexOptions.Singleline);

            // ===============


            editor.SetDocumentText(text);
        }

        internal static void SpacingFormat()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            if (string.IsNullOrEmpty(text)) return;
            // Begin process
            
            RegexOptions options = RegexOptions.None;
            text = text.Replace("\t", " ");
            text = Regex.Replace(text, @"^\s+", "");
            Regex regex = new Regex("[ ]{2,}", options);
            Regex regex2 = new Regex(@"\s+\r\n", options);
            text = regex.Replace(text, " ");
            text = regex2.Replace(text, "\r\n");
            text = Regex.Replace(text, @"\r\n\s+", "\r\n");
            text = Regex.Replace(text, @"\$(\d{2,3}): \$", m => "$"+ m.Groups[1].Value + ":$");
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

            text = Regex.Replace(text, @"\$\(A> (.*?) <A\$\)", m => $@"$(A> {m.Groups[1].Value.ToUpper()} <A$)", RegexOptions.Singleline);

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
            text = Regex.Replace(text, @"\r\n(cm|cd|d?c{0,3})(xc|xl|l?x{0,3})(ix|iv|v?i{0,3})\. ", "\r\n$T$1$2$3. ");
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
            text = Regex.Replace(text, @"\r\n(cm|cd|d?c{0,3})(xc|xl|l?x{0,3})(ix|iv|v?i{0,3})\. ", "\r\n$%$%$1$2$3. ");
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


        internal static void FootnoteXMLRenumber()
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

                text = xmlRenumber1(text, i);
                text = xmlRenumber2(text, i);

                editor.SetDocumentText(text);
            }
        }

        private static string xmlRenumber1(string text, int i)
        {
            string tmp = text;
            string Patt = @"<lnvxe:fnr alt-content=""(n[0-9]{1,4}|\x2a|\x2a\x2a|\x2a\x2a\x2a)"" fnrtoken=""ref([0-9]{1,4})"" fntoken=""fnote([0-9]{1,4})"">([0-9]{1,4}|\x2a|\x2a\x2a|\x2a\x2a\x2a)<\/lnvxe:fnr>";
            var result = "";
            result = Regex.Replace(text, Patt, (Match n) =>
            {

                result = string.Format("<lnvxe:fnr alt-content=\"{0}\" fnrtoken=\"ref{1}\" fntoken=\"fnote{1}\">{2}</lnvxe:fnr>", n.Groups[1], ++i, n.Groups[4]);
                return result;
            });

            string Patt2 = @"<lnvxe:footnote fnrtokens=""ref([0-9]{1,4})"" fntoken=""fnote([0-9]{1,4})"">";
            var result2 = "";
            result2 = Regex.Replace(result, Patt2, (Match n) =>
            {

                result2 = string.Format("<lnvxe:footnote fnrtokens=\"ref{0}\" fntoken=\"fnote{0}\">", ++i);
                return result2;
            });
            return result;
        }

        private static string xmlRenumber2(string text, int i)
        {
            string Patt2 = @"<lnvxe:footnote fnrtokens=""ref([0-9]{1,4})"" fntoken=""fnote([0-9]{1,4})"">";
            var result2 = "";
            result2 = Regex.Replace(text, Patt2, (Match n) =>
            {

                result2 = string.Format("<lnvxe:footnote fnrtokens=\"ref{0}\" fntoken=\"fnote{0}\">", ++i);
                return result2;
            });
            return result2;
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

        internal static void StringDola120()
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

                // Find Opinion $

                int Index = Array.FindIndex(Segments, StartbyDola120);
                for (int i = 0; i < Index; i++)
                {
                    Segments[i] = Segments[i].Replace("[!!]", " ");
                    Segments[i] = Segments[i].Replace(" $%$%", "[!!]$%$%");
                    Segments[i] = Segments[i].Replace(" $", "[!!]$");
                    Segments[i] = Segments[i] + "[!!]";
                }
                // ===================
                string tempFile = "";
                foreach (string s in Segments)
                {
                    tempFile += s;
                }
                tempAllFiles += tempFile;
            }
            tempAllFiles = tempAllFiles.Replace("[!!]", "\r\n");
            editor.SetDocumentText(tempAllFiles);
        }

        internal static void StringDola14()
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

                // Find Opinion $

                int Index = Array.FindIndex(Segments, StartbyDola14);
                for (int i = 0; i < Index; i++)
                {
                    Segments[i] = Segments[i].Replace("[!!]", " ");
                    Segments[i] = Segments[i].Replace(" $%$%", "[!!]$%$%");
                    Segments[i] = Segments[i].Replace(" $", "[!!]$");
                    Segments[i] = Segments[i] + "[!!]";
                }
                // ===================
                string tempFile = "";
                foreach (string s in Segments)
                {
                    tempFile += s;
                }
                tempAllFiles += tempFile;
            }
            tempAllFiles = tempAllFiles.Replace("[!!]", "\r\n");
            editor.SetDocumentText(tempAllFiles);
        }

        internal static void StringDola130()
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

                // Find Opinion $

                int Index = Array.FindIndex(Segments, StartbyDola130);
                for (int i = 0; i < Index; i++)
                {
                    Segments[i] = Segments[i].Replace("[!!]", " ");
                    Segments[i] = Segments[i].Replace(" $%$%", "[!!]$%$%");
                    Segments[i] = Segments[i].Replace(" $", "[!!]$");
                    Segments[i] = Segments[i] + "[!!]";
                }
                // ===================
                string tempFile = "";
                foreach (string s in Segments)
                {
                    tempFile += s;
                }
                tempAllFiles += tempFile;
            }
            tempAllFiles = tempAllFiles.Replace("[!!]", "\r\n");
            editor.SetDocumentText(tempAllFiles);
        }


        private static bool StartbyDola120(String s)
        {
            if (s.StartsWith("$120:"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static bool StartbyDola14(String s)
        {
            if (s.StartsWith("$14:"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool StartbyDola130(String s)
        {
            if (s.StartsWith("$130:"))
            {
                return true;
            }
            else
            {
                return false;
            }
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
            var pos = editor.GetSelectionRange();

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
            editor.SetSelection(pos.cpMin, pos.cpMin + text.Length);

        }

        internal static void RemoveTableOnly()
        {
            var editor = Editor.GetActive();
            var text = editor.GetSelectedText();
            var pos = editor.GetSelectionRange();

            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Select text before process");
                return;
            }
            else
            {
                text = text.Replace("$Q", "");
                text = text.Replace("$G", "");
                text = text.Replace("$R", "");
                text = text.Replace("$B", "");
                text = text.Replace("$J", "");
                text = text.Replace("$Y", "");
                text = text.Replace("$X", "");
                text = text.Replace("$L", "");
                text = text.Replace("$H", "");
                text = text.Replace("$D", "");
            }

            editor.SetSelectedText(text);
            editor.SetSelection(pos.cpMin, pos.cpMin + text.Length);

        }

        internal static void FootnoteBodyOneSpace()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            var pos = editor.GetSelectionRange();

            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Select text before process");
                return;
            }
            else
            {
                text = Regex.Replace(text, @"\r\n n(\d+) ", "\r\n$E\r\n$Fn$1 ");
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

        internal static void D9290_Img_ReNumber()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();

            string[] listFiles = Regex.Split(text, @"(?=\$00:)");
            string tempAllFiles = "";
            foreach (string file in listFiles)
            {
                int i = -1;
                tempAllFiles += D9290_IMG_NUMBER(file, i);
            }
            editor.SetDocumentText(tempAllFiles);
            D9290_198();
        }

        private static void D9290_198()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            string[] listFiles = Regex.Split(text, @"(?=\$00:)");
            string tempAllFiles = "";
            foreach (string file in listFiles)
            {
                string d198 = "$198:";
                MatchCollection matches = Regex.Matches(file, @"15125:(.*);1");
                foreach (Match m in matches)
                {

                    d198 += m.Groups[1].Value + " ";
                }
                tempAllFiles += file + "\r\n" + d198 + "\r\n";
            }
            editor.SetDocumentText(tempAllFiles.Replace("\r\n$198:\r\n", ""));

        }

        private static string D9290_IMG_NUMBER(string text, int i = -1)
        {
            string result = Regex.Replace(text, @"15125:(.*)([0-9]{4})([a-z]{1,5});1", (Match n) =>
            {
                result = string.Format("15125:{0}{2}{1};1", n.Groups[1].Value, D9290imgList[++i], n.Groups[2].Value);
                return (result);
            });
            return result;
        }

        internal static void D1414_IMG()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();

            string[] listFiles = Regex.Split(text, @"(?=\$00:)");
            string tempAllFiles = "";
            foreach (string file in listFiles)
            {
                int i = 0;
                tempAllFiles += D1414_IMG_ForProcess(file, i);
            }
            editor.SetDocumentText(tempAllFiles);
            D1414_233();
        }

        private static void D1414_233()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            string[] listFiles = Regex.Split(text, @"(?=\$00:)");
            string tempAllFiles = "";
            foreach (string file in listFiles)
            {
                string d198 = "$233:";
                MatchCollection matches = Regex.Matches(file, @";11931:(.*);1");
                foreach (Match m in matches)
                {

                    d198 += m.Groups[1].Value + " ";
                }
                tempAllFiles += file + "\r\n" + d198 + "\r\n";
            }
            editor.SetDocumentText(tempAllFiles.Replace("\r\n$233:\r\n", ""));

        }

        internal static void FootnoteLinkAddSID()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            string[] listFiles = Regex.Split(text, @"(?=\$00:)");
            string tempAllFiles = "";
            foreach (string file in listFiles)
            {
                string ssid = "";
                Match SID;
                SID = Regex.Match(file, "#SID([A-Z0-9]{14})", RegexOptions.None);

                if (SID.Success)
                {
                    ssid = SID.Groups[1].Value.ToString();
                }
                else
                {
                    ssid = "_@@_";
                }

                tempAllFiles += file.Replace("_@@_", ssid);
            }
            editor.SetDocumentText(tempAllFiles);
        }

        private static string D1414_IMG_ForProcess(string text, int i)
        {
            string result = Regex.Replace(text, @":1414(.*)([0-9]{2});1", (Match n) =>
            {
                if (i < 9)
                {
                    result = string.Format(":1414{0}0{1};1", n.Groups[1].Value, ++i);
                    return (result);
                }
                else
                {
                    result = string.Format(":1414{0}{1};1", n.Groups[1].Value, ++i);
                    return (result);
                }
            });
            return result;
        }

        internal static void FootnoteReturn()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            var filename = editor.GetCurrentFileName();
            var Dir = editor.GetCurrentDirectory();

            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Emply Document.");
                return;
            }
            else
            {
                string[] listFiles = Regex.Split(text, @"(?=\$00:)");
                int i = 0;
                string tempAllFiles = "";
                foreach (string f in listFiles)
                {

                    tempAllFiles += FootnoteReturnSingleFile(f, filename, Dir, i) + "\r\n";
                    i++;
                }

                // Remove Blank Lines



                editor.SetDocumentText(tempAllFiles);
                MessageBox.Show("Footnote Returned.");
            }
        }

        // Process footnote before Move
        private static string footnoteReturnBlockquoteProcess(string input)
        {
            string output = input;
            foreach (Match m in Regex.Matches(input, @"\$=S(.*?)\$=I", RegexOptions.Singleline))
            {
                string mReplaced = m.Value.ToString().Replace("$T", "___$T");
                mReplaced = mReplaced.Replace("$%$%", "___$%$%");
                output = output.Replace(m.Value.ToString(), mReplaced);
            }
            return output;
        }


        private static string FootnoteReturnSingleFile(string text, string fileName, string Dir, int fileNumber)
        {
            text = footnoteReturnBlockquoteProcess(text);
            text = text.Replace("\r\n", "[!!]");
            text = text.Replace("$=I[!!]$%$%", "$=I[!!]___$%$%");
            text = text.Replace("[!!]$T", "\r\n$T");
            text = text.Replace("[!!]$=H", "\r\n$=H");
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

            File.WriteAllText(Dir + "\\" + Path.GetFileNameWithoutExtension(fileName) + "_" + fileNumber + "_BodyFootnote.H.Ha", "");
            File.AppendAllText(Dir + "\\" + Path.GetFileNameWithoutExtension(fileName) + "_" + fileNumber + "_BodyFootnote.H.Ha", tmp.Replace("[!!]", "\r\n"));

            text = text.Replace("[!!]___$%$%", "\r\n$%$%");
            text = text.Replace("[!!]", "\r\n");
            text = Regex.Replace(text, @"^(?:[\t ]*(?:\r?\n|\r))+", "");
            text = text.Replace("\r\n\n", "\r\n");
            text = text.Replace("\r\r\n", "\r\n");
            text = text.Replace("___$T", "$T");
            text = text.Replace("___$%$%", "$%$%");

            return text;
        }

        // TABLE
        internal static void Table_Process()
        {
            var editor = Editor.GetActive();
            var selectedtext = editor.GetSelectedText();
            var pos = editor.GetSelectionRange();

            string MathCommand = "$M02,39,39$D\r\n$M03,26,26,26$D\r\n$M04,20,20,20,18$D\r\n$M05L,14,13,24,14,13$D";
            if (string.IsNullOrEmpty(selectedtext))
            {
                return;
            }
            else
            {
                for (int i = 0; i < 150; i++)
                {
                    selectedtext = selectedtext.Replace("   ", " ");
                    selectedtext = selectedtext.Replace("  ", " ");
                    selectedtext = selectedtext.Replace(" $H", "$H");
                    selectedtext = selectedtext.Replace("$H ", "$H");
                    selectedtext = selectedtext.Replace(" $L", "$L");
                    selectedtext = selectedtext.Replace("$L ", "$L");
                    selectedtext = selectedtext.Replace(" $D", "$D");
                    selectedtext = selectedtext.Replace(" $X", "$X");
                    selectedtext = selectedtext.Replace("$H\r\n", "\r\n");
                }
                selectedtext = MathCommand + "\r\n" + selectedtext + "$X";
                editor.SetSelectedText(selectedtext);
                editor.SetSelection(pos.cpMin, pos.cpMin + selectedtext.Length + 2);
            }
        }

        //

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
            int totalopenfiles = editor.GetAllOpenFiles() - 1;
            for (int i = 0; i < totalopenfiles; i++)
            {
                editor.ActiveDocumentByIndex(i);
                var text = editor.GetDocumentLength();
                MessageBox.Show(i.ToString() + "\r\n" + text);
            }
        }

        internal static void BreakWords()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Document Empty");
                return;
            }
            else
            {
                text = Regex.Replace(text, @"self-\r\n([a-z]+)", "self-$1");
                text = Regex.Replace(text, @"-of-\r\n([a-z]+)", "-of-$1");
                text = Regex.Replace(text, @"post-\r\n([a-z]+)", "post-$1");
                text = Regex.Replace(text, @"cross-\r\n([a-z]+)", "cross-$1");
                text = Regex.Replace(text, @"-by-\r\n([a-z]+)", "-by-$1");
                text = Regex.Replace(text, @"non-\r\n([a-z]+)", "non-$1");
                text = Regex.Replace(text, @"([a-z])-\r\n([a-z]+)", "$1$2");
                editor.SetDocumentText(text);
            }
        }



        internal static void Footnote_Indicator_Predict()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Document Empty");
                return;
            }
            else
            {
                text = Regex.Replace(text, @"([a-z]{2,}[.,?:;’”\'""\)]{0,})(\d+)", @"$1  n$2 ");
                text = Regex.Replace(text, @"\)\.(\d+)", @").  n$1 ");
                text = Regex.Replace(text, @"\)(\d+)", @")  n$1 ");
                text = Regex.Replace(text, @"19(\d+)\.(\d+)", @"19$1.  n$2 ");
                text = Regex.Replace(text, @"20(\d+)\.(\d+)", @"20$1.  n$2 ");
                text = Regex.Replace(text, @"19(\d+),(\d+)", @"19$1,  n$2 ");
                text = Regex.Replace(text, @"20(\d+),(\d+)", @"20$1,  n$2 ");
                text = Regex.Replace(text, @"([A-Z]{2,})(\d+)\$=R", @"$1$=R  n$2 ");
                text = Regex.Replace(text, @"([A-Z]{2,})(\d+)\$O", @"$1$O  n$2 ");
                text = Regex.Replace(text, @"([A-Z]{2,})(\d+)\$N", @"$1$N  n$2 ");

                // after process
                text = Regex.Replace(text, @"  n(\d+)\$=R", @"$=R  n$1");
                text = Regex.Replace(text, @"  n(\d+)\$N", @"$N  n$1");
                text = Regex.Replace(text, @"  n(\d+)\$O", @"$O  n$1");
                text = Regex.Replace(text, @"Cir.  n(\d+)", @"Cir.$1");
                text = Regex.Replace(text, @"Cal.  n(\d+)", @"Cal.$1");
                text = Regex.Replace(text, @" pp.  n(\d+)", @" pp.$1");
                text = Regex.Replace(text, @"Supp.  n(\d+)", @"Supp.$1");

                editor.SetDocumentText(text);
            }
        }

        internal static void ConvertToDecimal()
        {
            var editor = Editor.GetActive();
            var text = editor.GetSelectedText();
            var pos = editor.GetSelectionRange();

            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Document Empty");
                return;
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (char ch in text)
                {
                    if (ch >= ' ')
                    {
                        stringBuilder.Append("&#");
                        stringBuilder.Append(((Decimal)ch).ToString());
                        stringBuilder.Append(";");
                    }
                    else
                        stringBuilder.Append(ch);
                }
                editor.SetSelectedText(stringBuilder.ToString());
                editor.SetSelection(pos.cpMin, pos.cpMin + stringBuilder.ToString().Length);
            }
        }

        internal static void EntityToUnicode()
        {
            string output = "";
            var editor = Editor.GetActive();
            var text = editor.GetSelectedText();
            var pos = editor.GetSelectionRange();
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Document Empty");
                return;
            }
            else
            {
                output = System.Web.HttpUtility.HtmlDecode(text);

                editor.SetSelectedText(output);
                editor.SetSelection(pos.cpMin, pos.cpMin + output.Length);
            }

        }

        internal static void FootnoteTagsForCaselaw()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            if (String.IsNullOrEmpty(text))
            {
                return;
            }
            else
            {
                text = text.Replace(">ENDFN>$E", "$E");
                text = text.Replace("$Fn", "$F>FTNT>$Tn");
                text = text.Replace("$E", ">ENDFN>$E");
                editor.SetDocumentText(text);
            }
        }

        // Call Shortcut
        internal static void CallShortcut()
        {
            frmShortcut frm1 = new frmShortcut();
            frm1.ShowDialog();
        }
        //

        internal static void VISF2XML()
        {
            var editor = Editor.GetActive();
            var text = editor.GetSelectedOrAllText();
            var fName = editor.GetCurrentFileName();
            var fPath = editor.GetCurrentDirectory() + "\\" + fName;
            string text2 = text;
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Document Empty");
                return;
            }
            else
            {
                VisfToXML v = new VisfToXML();
                text = v.visf2xml(text, fName, fPath);

                text = xZero.insertCourt(text);
                // Write XML
                string fileOutput = Path.ChangeExtension(fPath, ".xml");
                File.WriteAllText(fileOutput, text, Encoding.GetEncoding("ISO-8859-1"));
                editor.OpenFile(fileOutput);
            }
        }

        internal static void NonVirgoXMLString()
        {

            var editor = Editor.GetActive();
            var text = editor.GetSelectedText();
            var pos = editor.GetSelectionRange();
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Document Empty");
                return;
            }
            else
            {
                text = text.Replace("$=B", "<emph typestyle=\"bf\">");
                text = text.Replace("$I", "<emph typestyle=\"it\">");
                text = text.Replace("$U", "<emph typestyle=\"un\">");
                text = text.Replace("$=R", "</emph>");
                text = text.Replace("$N", "</emph>");
                text = text.Replace("$O", "</emph>");

                text = "<p><lnvxe:text>" + text.Trim() + "</lnvxe:text></p>";
                text = text.Replace("$00:0100000001:", "\r\n\r\n$T$00:0100000001:\r\n\r\n");
                text = text.Replace("\r\n", " ");
                text = text.Replace("$%$%", "</lnvxe:text></p>\n<p><lnvxe:text>");
                text = text.Replace("$T", "</lnvxe:text></p>\n<p><lnvxe:text>");
                text = text.Replace("$=H", "\r\n<lnvxe:h>");
                text = text.Replace("$E</lnvxe:text></p>", "$E\r\n");
                text = text.Replace("$Fn", "</lnvxe:text></p>\r\n$Fn");
                text = text.Replace("$=E", "</lnvxe:h>\n");
                text = text.Replace("\r\n</lnvxe:text></p>", "</lnvxe:text></p>");
                text = text.Replace(" </lnvxe:text></p>", "</lnvxe:text></p>");
                text = text.Replace("<p></lnvxe:text></p>", "");
                //List Replace
                text = text.Replace("$=S", "\n<lnvxe:l>");
                text = text.Replace("$=I", "</lnvxe:text></p>\n</lnvxe:li>\n</lnvxe:l>\n");
                text = text.Replace("$W", "</lnvxe:text></p>\n</lnvxe:li>\n<lnvxe:li>\n<lnvxe:lilabel>");
                text = text.Replace("$K", "</lnvxe:lilabel>\n<p><lnvxe:text align=\"left\">");
                text = text.Replace("<lnvxe:l></lnvxe:text></p>\n</lnvxe:li>", "<lnvxe:l>");
                text = text.Replace("</lnvxe:l>\n</lnvxe:text></p>", "</lnvxe:l>");

                text = text.Replace("<p><lnvxe:text>$00:0100000001:  </lnvxe:text></p>", "\r\n\r\n$00:0100000001:\r\n\r\n");

                editor.SetSelectedText(text);
                editor.SetSelection(pos.cpMin, pos.cpMin + text.Length);
            }
        }

        internal static void AddSIDs()
        {
            var editor = Editor.GetActive();
            var filename = editor.GetCurrentFileName();
            var Dir = editor.GetCurrentDirectory();
            var text = editor.GetDocumentText();

            string[] listPDF = Directory.GetFiles(Dir, "*.pdf", SearchOption.TopDirectoryOnly);

            if (listPDF.Length == 0)
            {
                MessageBox.Show("PDF not found.");
            }
            else
            {
                string Pattern = @"\$200:(.*)";
                var result = "";
                int i = -1;
                result = Regex.Replace(text, Pattern, (Match n) =>
                {
                    if (i >= listPDF.Length - 1)
                    {
                        return "$200:$?#SID___#$?";
                    }
                    else
                    {
                        result = string.Format("$200:$?#SID" + getSID(listPDF[++i]) + "#$?");
                        return result;
                    }
                });

                string Pattern2 = @"\$00:(\d{2})(.*)";
                var result2 = "";
                int i2 = 0;
                result2 = Regex.Replace(result, Pattern2, (Match nn) =>
                {
                    if (i2 < 9)
                    {
                        result2 = string.Format("$00:{0}0000000" + (++i2) + ":", nn.Groups[1].Value);
                        return result2;
                    }
                    else if (i2 < 99)
                    {
                        result2 = string.Format("$00:{0}000000" + (++i2) + ":", nn.Groups[1].Value);
                        return result2;
                    }
                    else if (i2 < 999)
                    {
                        result2 = string.Format("$00:{0}00000" + (++i2) + ":", nn.Groups[1].Value);
                        return result2;
                    }
                    return result2;
                });

                editor.SetDocumentText(result2);
            }
        }
        private static string getSID(string filename)
        {
            string a = Path.GetFileNameWithoutExtension(filename);
            string SID = a.Substring(a.Length - 14, 14);
            return SID;
        }

        internal static void D1BVU_XML()
        {
            var editor = Editor.GetActive();
            var filename = editor.GetCurrentFileName();
            var Dir = editor.GetCurrentDirectory();
            var text = editor.GetDocumentText();
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Document Empty");
            }
            else
            {
                classD1BVU d1bvu = new classD1BVU();
                // Convert to XML
                text = d1bvu.D1BVU_Process(text);
                // Renumber Level
                text = d1bvu.D1BVU_ReNumberLevel(text);

                editor.SetDocumentText(text);
            }

        }

        internal static void D1BVU_VISF()
        {
            var editor = Editor.GetActive();
            var filename = editor.GetCurrentFileName();
            var Dir = editor.GetCurrentDirectory();
            var text = editor.GetDocumentText();
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Document Empty");
            }
            else
            {
                classD1BVU d1bvu = new classD1BVU();
                text = d1bvu.D1BVU_PreProcess(text);
                editor.SetDocumentText(text);
            }
        }
        internal static void DE535()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Document Empty");
            }
            else
            {
                classDE535 de535 = new classDE535();
                text = de535.processDE535(text);
                editor.SetDocumentText(text);
            }
        }

        internal static void D1712()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            string tempAllFiles = "";
            // Split Into Multiple Files
            string[] listFiles = Regex.Split(text, @"(?=\$00:)");
            foreach (string file in listFiles)
            {
                classD1712 d1712 = new classD1712();
                tempAllFiles += d1712.D1712FootnoteProcess(file);
            }
            editor.SetDocumentText(tempAllFiles);
        }

        internal static void TableForm()
        {
            var editor = Editor.GetActive();
            var text = editor.GetSelectedText();
            var pos = editor.GetSelectionRange();
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Vui lòng chọn Text trước....","Hoàng Hà's Table");
            }
            else
            {
                string tbl = "";
                File.WriteAllText("tableTemp.txt", text);
                frmTable frmTable = new frmTable();
                frmTable.ShowDialog();
                tbl = frmTable.tbl;
                editor.SetSelectedText(tbl);
                editor.SetSelection(pos.cpMin, pos.cpMin + tbl.Length);
            }
        }

        // Class - Somethings else

        private static List<string> D9290imgList = new List<string>
        {
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az", "ba", "bb", "bc", "bd", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br", "bs", "bt", "bu", "bv", "bw", "bx", "by", "bz", "ca", "cb", "cc", "cd", "ce", "cf", "cg", "ch", "ci", "cj", "ck", "cl", "cm", "cn", "co", "cp", "cq", "cr", "cs", "ct", "cu", "cv", "cw", "cx", "cy", "cz", "da", "db", "dc", "dd", "de", "df", "dg", "dh", "di", "dj", "dk", "dl", "dm", "dn", "do", "dp", "dq", "dr", "ds", "dt", "du", "dv", "dw", "dx", "dy", "dz", "ea", "eb", "ec", "ed", "ee", "ef", "eg", "eh", "ei", "ej", "ek", "el", "em", "en", "eo", "ep", "eq", "er", "es", "et", "eu", "ev", "ew", "ex", "ey", "ez", "fa", "fb", "fc", "fd", "fe", "ff", "fg", "fh", "fi", "fj", "fk", "fl", "fm", "fn", "fo", "fp", "fq", "fr", "fs", "ft", "fu", "fv", "fw", "fx", "fy", "fz", "ga", "gb", "gc", "gd", "ge", "gf", "gg", "gh", "gi", "gj", "gk", "gl", "gm", "gn", "go", "gp", "gq", "gr", "gs", "gt", "gu", "gv", "gw", "gx", "gy", "gz", "ha", "hb", "hc", "hd", "he", "hf", "hg", "hh", "hi", "hj", "hk", "hl", "hm", "hn", "ho", "hp", "hq", "hr", "hs", "ht", "hu", "hv", "hw", "hx", "hy", "hz", "ia", "ib", "ic", "id", "ie", "if", "ig", "ih", "ii", "ij", "ik", "il", "im", "in", "io", "ip", "iq", "ir", "is", "it", "iu", "iv", "iw", "ix", "iy", "iz", "ja", "jb", "jc", "jd", "je", "jf", "jg", "jh", "ji", "jj", "jk", "jl", "jm", "jn", "jo", "jp", "jq", "jr", "js", "jt", "ju", "jv", "jw", "jx", "jy", "jz", "ka", "kb", "kc", "kd", "ke", "kf", "kg", "kh", "ki", "kj", "kk", "kl", "km", "kn", "ko", "kp", "kq", "kr", "ks", "kt", "ku", "kv", "kw", "kx", "ky", "kz", "la", "lb", "lc", "ld", "le", "lf", "lg", "lh", "li", "lj", "lk", "ll", "lm", "ln", "lo", "lp", "lq", "lr", "ls", "lt", "lu", "lv", "lw", "lx", "ly", "lz", "ma", "mb", "mc", "md", "me", "mf", "mg", "mh", "mi", "mj", "mk", "ml", "mm", "mn", "mo", "mp", "mq", "mr", "ms", "mt", "mu", "mv", "mw", "mx", "my", "mz", "na", "nb", "nc", "nd", "ne", "nf", "ng", "nh", "ni", "nj", "nk", "nl", "nm", "nn", "no", "np", "nq", "nr", "ns", "nt", "nu", "nv", "nw", "nx", "ny", "nz", "oa", "ob", "oc", "od", "oe", "of", "og", "oh", "oi", "oj", "ok", "ol", "om", "on", "oo", "op", "oq", "or", "os", "ot", "ou", "ov", "ow", "ox", "oy", "oz", "pa", "pb", "pc", "pd", "pe", "pf", "pg", "ph", "pi", "pj", "pk", "pl", "pm", "pn", "po", "pp", "pq", "pr", "ps", "pt", "pu", "pv", "pw", "px", "py", "pz", "qa", "qb", "qc", "qd", "qe", "qf", "qg", "qh", "qi", "qj", "qk", "ql", "qm", "qn", "qo", "qp", "qq", "qr", "qs", "qt", "qu", "qv", "qw", "qx", "qy", "qz", "ra", "rb", "rc", "rd", "re", "rf", "rg", "rh", "ri", "rj", "rk", "rl", "rm", "rn", "ro", "rp", "rq", "rr", "rs", "rt", "ru", "rv", "rw", "rx", "ry", "rz", "sa", "sb", "sc", "sd", "se", "sf", "sg", "sh", "si", "sj", "sk", "sl", "sm", "sn", "so", "sp", "sq", "sr", "ss", "st", "su", "sv", "sw", "sx", "sy", "sz", "ta", "tb", "tc", "td", "te", "tf", "tg", "th", "ti", "tj", "tk", "tl", "tm", "tn", "to", "tp", "tq", "tr", "ts", "tt", "tu", "tv", "tw", "tx", "ty", "tz", "ua", "ub", "uc", "ud", "ue", "uf", "ug", "uh", "ui", "uj", "uk", "ul", "um", "un", "uo", "up", "uq", "ur", "us", "ut", "uu", "uv", "uw", "ux", "uy", "uz", "va", "vb", "vc", "vd", "ve", "vf", "vg", "vh", "vi", "vj", "vk", "vl", "vm", "vn", "vo", "vp", "vq", "vr", "vs", "vt", "vu", "vv", "vw", "vx", "vy", "vz", "wa", "wb", "wc", "wd", "we", "wf", "wg", "wh", "wi", "wj", "wk", "wl", "wm", "wn", "wo", "wp", "wq", "wr", "ws", "wt", "wu", "wv", "ww", "wx", "wy", "wz", "xa", "xb", "xc", "xd", "xe", "xf", "xg", "xh", "xi", "xj", "xk", "xl", "xm", "xn", "xo", "xp", "xq", "xr", "xs", "xt", "xu", "xv", "xw", "xx", "xy", "xz", "ya", "yb", "yc", "yd", "ye", "yf", "yg", "yh", "yi", "yj", "yk", "yl", "ym", "yn", "yo", "yp", "yq", "yr", "ys", "yt", "yu", "yv", "yw", "yx", "yy", "yz", "za", "zb", "zc", "zd", "ze", "zf", "zg", "zh", "zi", "zj", "zk", "zl", "zm", "zn", "zo", "zp", "zq", "zr", "zs", "zt", "zu", "zv", "zw", "zx", "zy", "zz", "aaa", "aab", "aac", "aad", "aae", "aaf", "aag", "aah", "aai", "aaj", "aak", "aal", "aam", "aan", "aao", "aap", "aaq", "aar", "aas", "aat", "aau", "aav", "aaw", "aax", "aay", "aaz", "aba", "abb", "abc", "abd", "abe", "abf", "abg", "abh", "abi", "abj", "abk", "abl", "abm", "abn", "abo", "abp", "abq", "abr", "abs", "abt", "abu", "abv", "abw", "abx", "aby", "abz", "aca", "acb", "acc", "acd", "ace", "acf", "acg", "ach", "aci", "acj", "ack", "acl", "acm", "acn", "aco", "acp", "acq", "acr", "acs", "act", "acu", "acv", "acw", "acx", "acy", "acz", "ada", "adb", "adc", "add", "ade", "adf", "adg", "adh", "adi", "adj", "adk", "adl", "adm", "adn", "ado", "adp", "adq", "adr", "ads", "adt", "adu", "adv", "adw", "adx", "ady", "adz", "aea", "aeb", "aec", "aed", "aee", "aef", "aeg", "aeh", "aei", "aej", "aek", "ael", "aem", "aen", "aeo", "aep", "aeq", "aer", "aes", "aet", "aeu", "aev", "aew", "aex", "aey", "aez", "afa", "afb", "afc", "afd", "afe", "aff", "afg", "afh", "afi", "afj", "afk", "afl", "afm", "afn", "afo", "afp", "afq", "afr", "afs", "aft", "afu", "afv", "afw", "afx", "afy", "afz", "aga", "agb", "agc", "agd", "age", "agf", "agg", "agh", "agi", "agj", "agk", "agl", "agm", "agn", "ago", "agp", "agq", "agr", "ags", "agt", "agu", "agv", "agw", "agx", "agy", "agz", "aha", "ahb", "ahc", "ahd", "ahe", "ahf", "ahg", "ahh", "ahi", "ahj", "ahk", "ahl", "ahm", "ahn", "aho", "ahp", "ahq", "ahr", "ahs", "aht", "ahu", "ahv", "ahw", "ahx", "ahy", "ahz", "aia", "aib", "aic", "aid", "aie", "aif", "aig", "aih", "aii", "aij", "aik", "ail", "aim", "ain", "aio", "aip", "aiq", "air", "ais", "ait", "aiu", "aiv", "aiw", "aix", "aiy", "aiz", "aja", "ajb", "ajc", "ajd", "aje", "ajf", "ajg", "ajh", "aji", "ajj", "ajk", "ajl", "ajm", "ajn", "ajo", "ajp", "ajq", "ajr", "ajs", "ajt", "aju", "ajv", "ajw", "ajx", "ajy", "ajz", "aka", "akb", "akc", "akd", "ake", "akf", "akg", "akh", "aki", "akj", "akk", "akl", "akm", "akn", "ako", "akp", "akq", "akr", "aks", "akt", "aku", "akv", "akw", "akx", "aky", "akz", "ala", "alb", "alc", "ald", "ale", "alf", "alg", "alh", "ali", "alj", "alk", "all", "alm", "aln", "alo", "alp", "alq", "alr", "als", "alt", "alu", "alv", "alw", "alx", "aly", "alz", "ama", "amb", "amc", "amd", "ame", "amf", "amg", "amh", "ami", "amj", "amk", "aml", "amm", "amn", "amo", "amp", "amq", "amr", "ams", "amt", "amu", "amv", "amw", "amx", "amy", "amz", "ana", "anb", "anc", "and", "ane", "anf", "ang", "anh", "ani", "anj", "ank", "anl", "anm", "ann", "ano", "anp", "anq", "anr", "ans", "ant", "anu", "anv", "anw", "anx", "any", "anz", "aoa", "aob", "aoc", "aod", "aoe", "aof", "aog", "aoh", "aoi", "aoj", "aok", "aol", "aom", "aon", "aoo", "aop", "aoq", "aor", "aos", "aot", "aou", "aov", "aow", "aox", "aoy", "aoz", "apa", "apb", "apc", "apd", "ape", "apf", "apg", "aph", "api", "apj", "apk", "apl", "apm", "apn", "apo", "app", "apq", "apr", "aps", "apt", "apu", "apv", "apw", "apx", "apy", "apz", "aqa", "aqb", "aqc", "aqd", "aqe", "aqf", "aqg", "aqh", "aqi", "aqj", "aqk", "aql", "aqm", "aqn", "aqo", "aqp", "aqq", "aqr", "aqs", "aqt", "aqu", "aqv", "aqw", "aqx", "aqy", "aqz", "ara", "arb", "arc", "ard", "are", "arf", "arg", "arh", "ari", "arj", "ark", "arl", "arm", "arn", "aro", "arp", "arq", "arr", "ars", "art", "aru", "arv", "arw", "arx", "ary", "arz", "asa", "asb", "asc", "asd", "ase", "asf", "asg", "ash", "asi", "asj", "ask", "asl", "asm", "asn", "aso", "asp", "asq", "asr", "ass", "ast", "asu", "asv", "asw", "asx", "asy", "asz", "ata", "atb", "atc", "atd", "ate", "atf", "atg", "ath", "ati", "atj", "atk", "atl", "atm", "atn", "ato", "atp", "atq", "atr", "ats", "att", "atu", "atv", "atw", "atx", "aty", "atz", "aua", "aub", "auc", "aud", "aue", "auf", "aug", "auh", "aui", "auj", "auk", "aul", "aum", "aun", "auo", "aup", "auq", "aur", "aus", "aut", "auu", "auv", "auw", "aux", "auy", "auz", "ava", "avb", "avc", "avd", "ave", "avf", "avg", "avh", "avi", "avj", "avk", "avl", "avm", "avn", "avo", "avp", "avq", "avr", "avs", "avt", "avu", "avv", "avw", "avx", "avy", "avz", "awa", "awb", "awc", "awd", "awe", "awf", "awg", "awh", "awi", "awj", "awk", "awl", "awm", "awn", "awo", "awp", "awq", "awr", "aws", "awt", "awu", "awv", "aww", "awx", "awy", "awz", "axa", "axb", "axc", "axd", "axe", "axf", "axg", "axh", "axi", "axj", "axk", "axl", "axm", "axn", "axo", "axp", "axq", "axr", "axs", "axt", "axu", "axv", "axw", "axx", "axy", "axz", "aya", "ayb", "ayc", "ayd", "aye", "ayf", "ayg", "ayh", "ayi", "ayj", "ayk", "ayl", "aym", "ayn", "ayo", "ayp", "ayq", "ayr", "ays", "ayt", "ayu", "ayv", "ayw", "ayx", "ayy", "ayz", "aza", "azb", "azc", "azd", "aze", "azf", "azg", "azh", "azi", "azj", "azk", "azl", "azm", "azn", "azo", "azp", "azq", "azr", "azs", "azt", "azu", "azv", "azw", "azx", "azy", "azz"
        };

        private static List<string> PredictIndent = new List<string>
        {
            "Therefore, ", "Although ", "When ", "What ", "10) ", "1) ", "2) ", "3) ", "4) ", "5) ", "6) ", "7) ", "8) ", "9) ", "11) ", "12) ", "13) ", "14) ", "15) ", "16) ", "Following ", "After ", "An ", "Another ", "Basing ", "We ", "Other ", "One ", "To ", "$=BNote$=R: ", "$=BExample ", "At ", "*** ", "***", "* * *", "...", ". . .", "__1. ", "__2. ", "__3. ", "__4. ", "__5. ", "__6. ", "__7. ", "__8. ", "__9. ", "__11. ", "__20. ", "__12. ", "__13. ", "__14. ", "__15. ", "__16. ", "__17. ", "__18. ", "__19. ", "__21. ", "__30. ", "__22. ", "__23. ", "__24. ", "__25. ", "__26. ", "__27. ", "__28. ", "__29. ", "__31. ", "IT IS ", "$=BIT IS", "DATED: ", "HONORABLE ", "UNITED STATES ", "Third", "The ", "By ", "There ", "Under ", "These ", "A ", "As ", "$II. ", "$III. ", "$IIII. ", "$IIV. ", "$IV. ", "$IVI. ", "$IVII. ", "$IVIII. ", "$IIX. ", "$IX. ", "Fifth,", "I. ", "II. ", "III. ", "IV. ", "V. ", "VI. ", "VII. ", "VIII. ", "IX. ", "X. ", "A. ", "$IA. ", "$IB. ", "$IC. ", "$ID. ", "$IE. ", "$IF. ", "$IG. ", "$IH. ", "$IJ. ", "$IK. ", "1. ", "$I1. ", "$I2. ", "$I3. ", "$I4. ", "$I5. ", "$I6. ", "$I7. ", "$I8. ", "$I9. ", "$I11. ", "$I20. ", "$I12. ", "$I13. ", "$I14. ", "$I15. ", "$I16. ", "$I17. ", "$I18. ", "$I19. ", "$I21. ", "$I30. ", "$I22. ", "$I23. ", "$I24. ", "$I25. ", "$I26. ", "$I27. ", "$I28. ", "$I29. ", "$I31. ", "$IL. ", "$IM. ", "$IN. ", "$IO. ", "$IP. ", "$IQ. ", "$IR. ", "$IS. ", "$IT. ", "$IU. ", "$IW. ", "Respectfully submitted", "Dated:", "$ICERTIFICATE OF", "Thus, ", "Finally, ", "Grounds ", "For ", "Furthermore ", "With ", "Accordingly,", "Accordingly ", "Moreover,", "Specifically,", "(Ex. ", "Even ", "Here, ", "Of ", "This ", "Where ", "While ", "Without ", "Email:", "Each ", "Also,", "By:", "Consequently,", "During", "Fourth,", "Hence,", "However,", "If ", "I hereby", "Independent ", "Moreover, ", "Of these,", "ORDERED", "$IORDERED", "FURTHER", "$IFURTHER", "Sixth,", "Seventh,", "Email: ", "Telephone:", "Facsimile:", "$U/s/ ", "CO[2]__", "$I$UA. ", "$I$UB. ", "$I$UC. ", "$I$UD. ", "$I$UE. ", "$I$UF. ", "$I$UG. ", "$I$UH. ", "$I$UJ. ", "$I$UK. ", "$I$UL. ", "$I$UM. ", "$I$UO. ", "$I$UP. ", "$I$UQ. ", "$I$UR. ", "$I$US. ", "$I$UT. ", "$I$UU. ", "$I$UV. ", "$I$UI. ", "$I$UII. ", "$I$UIII. ", "$I$UIV. ", "$I$UVI. ", "$I$UVII. ", "$I$UVIII. ", "$I$UIX. ", "$I$UX. ", "In ", "Those ", "It ", "On ", "First", "Second", "Pursuant to", "$=BI. ", "$=BII. ", "$=BIII. ", "$=BIV. ", "$=BV. ", "$=BVI. ", "$=BVII. ", "$=BVIII. ", "$=BIX. ", "$=BX. ", "$=BA. ", "$=BB. ", "$=BC. ", "$=BD. ", "$=BE. ", "$=BF. ", "$=BG. ", "$=BH. ", "$=BJ. ", "$=BK. ", "$=B1. ", "$=B2. ", "$=B3. ", "$=B4. ", "$=B5. ", "$=B6. ", "$=B7. ", "$=B8. ", "$=B9. ", "$=B11. ", "$=B20. ", "$=B12. ", "$=B13. ", "$=B14. ", "$=B15. ", "$=B16. ", "$=B17. ", "$=B18. ", "$=B19. ", "$=B21. ", "$=B30. ", "$=B22. ", "$=B23. ", "$=B24. ", "$=B25. ", "$=B26. ", "$=B27. ", "$=B28. ", "$=B29. ", "$=B31. ", "$=BL. ", "$=BM. ", "$=BN. ", "$=BO. ", "$=BP. ", "$=BQ. ", "$=BR. ", "$=BS. ", "$=BT. ", "$=BU. ", "$=BW. ", "$=BCERTIFICATE OF", "$=BORDERED", "$=BFURTHER", "$=B$IA. ", "$=B$IB. ", "$=B$IC. ", "$=B$ID. ", "$=B$IE. ", "$=B$IF. ", "$=B$IG. ", "$=B$IH. ", "$=B$IJ. ", "$=B$IK. ", "$=B$IL. ", "$=B$IM. ", "$=B$IO. ", "$=B$IP. ", "$=B$IQ. ", "$=B$IR. ", "$=B$IS. ", "$=B$IT. ", "$=B$IU. ", "$=B$IV. ", "$=B$II. ", "$=B$III. ", "$=B$IIII. ", "$=B$IIV. ", "$=B$IVI. ", "$=B$IVII. ", "$=B$IVIII. ", "$=B$IIX. ", "$=B$IX. ", "/s/", "$=BIT IS SO ", "$=BCONCLUSION$=R", "Reg. No.", "$I/s/"
        };

        private static List<string> PredictNotIndent = new List<string>
        {
            "THE COURT: ", "MR. ", "Date: ", "Judgment Entered", "Q. ", "A. ", "Q: ", "A: ", "$=BFrom$=R: ", "$=BSent$=R: ", "$=BTo$=R: ", "$=BCc$=R: ", "$=BSubject$=R: ", "Dated:"
        };

        private class MyComparer : IComparer<string>
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
