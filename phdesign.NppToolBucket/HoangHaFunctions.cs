using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
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
            text = Regex.Replace(text,@"(\d+)--(\d+)", "$1-$2");

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
                text = text.Replace("\r\n" + s, "\r\n$T" +s);
            }

            foreach (string s in PredictNotIndent)
            {
                text = text.Replace("\r\n" + s, "\r\n$T" + s);
            }

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

        public static List<string> PredictIndent = new List<string>
        {
            "Therefore, ", "Although ", "When ", "What ", "10) ", "1) ", "2) ", "3) ", "4) ", "5) ", "6) ", "7) ", "8) ", "9) ", "11) ", "12) ", "13) ", "14) ", "15) ", "16) ", "Following ", "After ", "An ", "Another ", "Basing ", "We ", "Other ", "One ", "To ", "$=BNote$=R: ", "$=BExample ", "At ", "*** ", "***", "* * *", "...", ". . .", "__1. ", "__2. ", "__3. ", "__4. ", "__5. ", "__6. ", "__7. ", "__8. ", "__9. ", "__11. ", "__20. ", "__12. ", "__13. ", "__14. ", "__15. ", "__16. ", "__17. ", "__18. ", "__19. ", "__21. ", "__30. ", "__22. ", "__23. ", "__24. ", "__25. ", "__26. ", "__27. ", "__28. ", "__29. ", "__31. ", "IT IS ", "$=BIT IS", "DATED: ", "HONORABLE ", "UNITED STATES ", "Third", "The ", "By ", "There ", "Under ", "These ", "A ", "As ", "$II. ", "$III. ", "$IIII. ", "$IIV. ", "$IV. ", "$IVI. ", "$IVII. ", "$IVIII. ", "$IIX. ", "$IX. ", "Fifth,", "I. ", "II. ", "III. ", "IV. ", "V. ", "VI. ", "VII. ", "VIII. ", "IX. ", "X. ", "A. ", "$IA. ", "$IB. ", "$IC. ", "$ID. ", "$IE. ", "$IF. ", "$IG. ", "$IH. ", "$IJ. ", "$IK. ", "1. ", "$I1. ", "$I2. ", "$I3. ", "$I4. ", "$I5. ", "$I6. ", "$I7. ", "$I8. ", "$I9. ", "$I11. ", "$I20. ", "$I12. ", "$I13. ", "$I14. ", "$I15. ", "$I16. ", "$I17. ", "$I18. ", "$I19. ", "$I21. ", "$I30. ", "$I22. ", "$I23. ", "$I24. ", "$I25. ", "$I26. ", "$I27. ", "$I28. ", "$I29. ", "$I31. ", "$IL. ", "$IM. ", "$IN. ", "$IO. ", "$IP. ", "$IQ. ", "$IR. ", "$IS. ", "$IT. ", "$IU. ", "$IW. ", "Respectfully submitted", "Dated:", "$ICERTIFICATE OF", "Thus, ", "Finally, ", "Grounds ", "For ", "Furthermore ", "With ", "Accordingly,", "Accordingly ", "Moreover,", "Specifically,", "(Ex. ", "Even ", "Here, ", "Of ", "This ", "Where ", "While ", "Without ", "Email:", "Each ", "Also,", "By:", "Consequently,", "During", "Fourth,", "Hence,", "However,", "If ", "I hereby", "Independent ", "Moreover, ", "Of these,", "ORDERED", "$IORDERED", "FURTHER", "$IFURTHER", "Sixth,", "Seventh,", "Email: ", "Telephone:", "Facsimile:", "$U/s/ ", "CO[2]__", "$I$UA. ", "$I$UB. ", "$I$UC. ", "$I$UD. ", "$I$UE. ", "$I$UF. ", "$I$UG. ", "$I$UH. ", "$I$UJ. ", "$I$UK. ", "$I$UL. ", "$I$UM. ", "$I$UO. ", "$I$UP. ", "$I$UQ. ", "$I$UR. ", "$I$US. ", "$I$UT. ", "$I$UU. ", "$I$UV. ", "$I$UI. ", "$I$UII. ", "$I$UIII. ", "$I$UIV. ", "$I$UVI. ", "$I$UVII. ", "$I$UVIII. ", "$I$UIX. ", "$I$UX. ", "In ", "Those ", "It ", "On ", "First", "Second", "Pursuant to", "$=BI. ", "$=BII. ", "$=BIII. ", "$=BIV. ", "$=BV. ", "$=BVI. ", "$=BVII. ", "$=BVIII. ", "$=BIX. ", "$=BX. ", "$=BA. ", "$=BB. ", "$=BC. ", "$=BD. ", "$=BE. ", "$=BF. ", "$=BG. ", "$=BH. ", "$=BJ. ", "$=BK. ", "$=B1. ", "$=B2. ", "$=B3. ", "$=B4. ", "$=B5. ", "$=B6. ", "$=B7. ", "$=B8. ", "$=B9. ", "$=B11. ", "$=B20. ", "$=B12. ", "$=B13. ", "$=B14. ", "$=B15. ", "$=B16. ", "$=B17. ", "$=B18. ", "$=B19. ", "$=B21. ", "$=B30. ", "$=B22. ", "$=B23. ", "$=B24. ", "$=B25. ", "$=B26. ", "$=B27. ", "$=B28. ", "$=B29. ", "$=B31. ", "$=BL. ", "$=BM. ", "$=BN. ", "$=BO. ", "$=BP. ", "$=BQ. ", "$=BR. ", "$=BS. ", "$=BT. ", "$=BU. ", "$=BW. ", "$=BCERTIFICATE OF", "$=BORDERED", "$=BFURTHER", "$=B$IA. ", "$=B$IB. ", "$=B$IC. ", "$=B$ID. ", "$=B$IE. ", "$=B$IF. ", "$=B$IG. ", "$=B$IH. ", "$=B$IJ. ", "$=B$IK. ", "$=B$IL. ", "$=B$IM. ", "$=B$IO. ", "$=B$IP. ", "$=B$IQ. ", "$=B$IR. ", "$=B$IS. ", "$=B$IT. ", "$=B$IU. ", "$=B$IV. ", "$=B$II. ", "$=B$III. ", "$=B$IIII. ", "$=B$IIV. ", "$=B$IVI. ", "$=B$IVII. ", "$=B$IVIII. ", "$=B$IIX. ", "$=B$IX. ", "/s/", "$=BIT IS SO ", "$=BCONCLUSION$=R", "Reg. No.", "$I/s/"
        };

        public static List<string> PredictNotIndent = new List<string>
        {
            "THE COURT: ", "MR. ", "Date: ", "Judgment Entered", "Q. ", "A. ", "Q: ", "A: ", "$=BFrom$=R: ", "$=BSent$=R: ", "$=BTo$=R: ", "$=BCc$=R: ", "$=BSubject$=R: ", "Dated:"
        };

    }
}
