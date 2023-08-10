/*
 * Copyright 2011-2012 Paul Heasley
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using phdesign.NppToolBucket.PluginCore;
using phdesign.NppToolBucket.Utilities.Security;

namespace phdesign.NppToolBucket
{
    internal static class Helpers
    {
        private const string LoremIpsum =
            "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

        internal static void ComputeSHA1Hash()
        {
            var editor = Editor.GetActive();
            var text = editor.GetSelectedOrAllText();
            if (string.IsNullOrEmpty(text)) return;

            var hash = SHA1.ComputeHash(text);
            editor.SetSelectedText(hash);
        }

        internal static void ComputeMD5Hash()
        {
            var editor = Editor.GetActive();
            var text = editor.GetSelectedOrAllText();
            if (string.IsNullOrEmpty(text)) return;

            var hash = MD5.ComputeHash(text);
            editor.SetSelectedText(hash);
        }

        internal static void GenerateLoremIpsum()
        {
            Editor.GetActive().SetSelectedText(LoremIpsum);
        }

        internal static void Base64Encode()
        {
            var editor = Editor.GetActive();
            var text = editor.GetSelectedOrAllText();
            if (string.IsNullOrEmpty(text)) return;

            var bytes = Encoding.UTF8.GetBytes(text);
            var result = Convert.ToBase64String(bytes);
            editor.SetSelectedText(result);
        }

        internal static void Base64Decode()
        {
            var editor = Editor.GetActive();
            var text = editor.GetSelectedOrAllText();
            if (string.IsNullOrEmpty(text)) return;

            var bytes = Convert.FromBase64String(text);
            var result = Encoding.UTF8.GetString(bytes);
            editor.SetSelectedText(result);
        }

        internal static void E1897_SmallCaps()
        {
            var editor = Editor.GetActive();
            var text = editor.GetSelectedText();
            var pos = editor.GetSelectionRange();
            if (string.IsNullOrEmpty(text))
            {
                System.Windows.Forms.MessageBox.Show("Bạn chưa chọn Text","Hoàng Hà");
            }
            
            var result = asTitleCase(text);
            editor.SetSelectedText(result);
            editor.SetSelection(pos.cpMin, pos.cpMax);
        }

        internal static void E1897_DateConvert()
        {
            var editor = Editor.GetActive();
            var text = editor.GetSelectedText();
            var pos = editor.GetSelectionRange();
            if (string.IsNullOrEmpty(text))
            {
                System.Windows.Forms.MessageBox.Show("Bạn chưa chọn Text", "Hoàng Hà");
            }
            else
            {
                if (text == "now")
                {
                    editor.SetSelectedText(DateTime.Now.ToString("MMMM d, yyyy"));
                    editor.SetSelection(pos.cpMin, pos.cpMin + DateTime.Now.ToString("MMMM d, yyyy").Length);
                }
                else if (text == "now_")
                {
                    editor.SetSelectedText(DateTime.Now.ToString("mm/dd/yy"));
                    editor.SetSelection(pos.cpMin, pos.cpMin + DateTime.Now.ToString("MM/dd/yy").Length);
                }
                else if (text.Contains("_"))
                {
                    DateTime dt = Convert.ToDateTime(text.Replace("_",""));
                    var result = dt.ToString("MM/dd/yy");
                    editor.SetSelectedText(result);
                    editor.SetSelection(pos.cpMin, pos.cpMin + result.Length);
                }
                else if (text.Contains("#"))
                {
                    DateTime dt = Convert.ToDateTime(text.Replace("#", ""));
                    var result = dt.ToString("MM/dd/yyyy");
                    editor.SetSelectedText(result);
                    editor.SetSelection(pos.cpMin, pos.cpMin + result.Length);
                }
                else 
                {
                    DateTime dt = Convert.ToDateTime(text);
                    var result = dt.ToString("MMMM d, yyyy");
                    editor.SetSelectedText(result);
                    editor.SetSelection(pos.cpMin, pos.cpMin + result.Length);
                }
            }
        }

        internal static void E1897_XML_Convert()
        {
            var editor = Editor.GetActive();
            var text = editor.GetSelectedText();
            var pos = editor.GetSelectionRange();
            if (string.IsNullOrEmpty(text))
            {
                System.Windows.Forms.MessageBox.Show("Bạn chưa chọn Text", "Hoàng Hà");
            }

            var result = XML_Convert(text);
            editor.SetSelectedText(result);
            editor.SetSelection(pos.cpMin, pos.cpMin+ result.Length);
        }


        internal static void E1897_2DtoDash()
        {
            var editor = Editor.GetActive();
            var text = editor.GetSelectedText();
            if (string.IsNullOrEmpty(text))
            {
                System.Windows.Forms.MessageBox.Show("Bạn chưa chọn Text", "Hoàng Hà");
            }

            var result = text.Replace("&#x2D;", "-");
            editor.SetSelectedText(result);
        }

        static List<string> bodylist = new List<string>();
        internal static void Ftnt2end()
        {
            bodylist.Clear();
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();

            string pattern = @"\$F(.*?)\$E";
            RegexOptions options = RegexOptions.Singleline;

            foreach (Match m in Regex.Matches(text, pattern, options))
            {
                bodylist.Add(m.Value.ToString());
                text = text.Replace(m.Value, "");
            }

            foreach (string body in bodylist)
            {
                //System.Windows.Forms.MessageBox.Show(body);
                text += "\r\n" + body + "\r\n";
            }
            // Remove Empty Lines =>> beauty document
            text = Regex.Replace(text, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);

            editor.SetDocumentText(text);
            System.Windows.Forms.MessageBox.Show("Done", "Footnote to End", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }


        public static string XML_Convert(this string input)
        {
            string toReturn = input;

            StringBuilder builder = new StringBuilder();
            foreach (char c in toReturn)
            {


                if ((int)c == 38)
                {
                    builder.Append("&#x0026;");
                }
                else if((int)c == 133)
                {
                    builder.Append("...");
                }
                else if ((int)c == 45)
                {
                    builder.Append("&#x2D;");
                }
                else if ((int)c == 45)
                {
                    builder.Append("&#x2D;");
                }
                else if ((int)c >= 160)
                {
                    builder.Append("&#x");
                    builder.Append(((int)c).ToString("X4"));
                    builder.Append(";");
                }
                else if ((int)c == 126)
                {
                    builder.Append("&#x");
                    builder.Append(((int)c).ToString("X4"));
                    builder.Append(";");
                }
                else
                {
                    builder.Append(c);
                }
            }


            return builder.ToString();
        }


        public static string asTitleCase(this string title)
        {
            string WorkingTitle = title;

            if (string.IsNullOrEmpty(WorkingTitle) == false)
            {
                char[] space = new char[] { ' ' };

                List<string> artsAndPreps = new List<string>()
              { "a", "an", "and", "any", "at", "but", "by", "for", "from", "in", "nor", "of", "on", "or", "so", "the", "to", "up", "yet", "'s", "'t", "'l", "'n", "'m", "&#x", "et al.",};

                //Get the culture property of the thread.
                CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                //Create TextInfo object.
                TextInfo textInfo = cultureInfo.TextInfo;

                //Convert to title case.
                WorkingTitle = textInfo.ToTitleCase(title.ToLower());

                List<string> tokens = WorkingTitle.Split(space, StringSplitOptions.None).ToList();

                WorkingTitle = tokens[0];

                tokens.RemoveAt(0);

                WorkingTitle += tokens.Aggregate<String, String>(String.Empty, (String prev, String input)
                                       => prev +
                                           (artsAndPreps.Contains(input.ToLower()) // If True
                                               ? " " + input.ToLower()             // Return the prep/art lowercase
                                               : " " + input));                   // Otherwise return the valid word.

                // Handle an "Out Of" but not in the start of the sentance
                WorkingTitle = Regex.Replace(WorkingTitle, @"(?!^Out)(Out\s+Of)", "out of");
            }

            return WorkingTitle;

        }

        internal static void ClearFindAllInAllDocuments()
        {
            int originalView;
            Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETCURRENTSCINTILLA, 0, out originalView);

            // Do the other view first so we leave the user back on the original view they started
            var otherView = originalView == (int)NppMsg.MAIN_VIEW ? (int)NppMsg.SUB_VIEW : (int)NppMsg.MAIN_VIEW;
            ClearFindAllInView(otherView);
            ClearFindAllInView(originalView);
        }



        private static void ClearFindAllInView(int view)
        {
            var originalDocument = (int)Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETCURRENTDOCINDEX, 0, view);
            var docCount = (int)Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETNBOPENFILES, 0, view + 1);

            for (int i = 0; i < docCount; i++)
            {
                Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_ACTIVATEDOC, view, i);
                ClearFindAllInCurrentDocument();
            }
            // Restore original doc
            Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_ACTIVATEDOC, view, originalDocument);
        }

        private static void ClearFindAllInCurrentDocument()
        {
            var editor = Editor.GetActive();
            editor.RemoveFindMarks();
            editor.RemoveAllBookmarks();
        }
    }
}