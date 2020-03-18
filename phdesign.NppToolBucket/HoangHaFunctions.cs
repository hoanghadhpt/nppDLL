using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
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

            string[] SplitTxt = text.Split(new[] { Environment.NewLine },StringSplitOptions.None);
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


        internal static string[] RemoveDuplicates(string[] s)
        {
            HashSet<string> set = new HashSet<string>(s);
            string[] result = new string[set.Count];
            set.CopyTo(result);
            return result;
        }

    }
}
