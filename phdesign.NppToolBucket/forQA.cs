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
    internal static  class forQA
    {
        internal static void Chum252Renumber182()
        {
            var editor = Editor.GetActive();
            var filename = editor.GetCurrentFileName();
            var Dir = editor.GetCurrentDirectory();
            var text = editor.GetDocumentText();


            string Pattern2 = @"\$182:(.*?)\r\n";
            var result2 = "";
            int i2 = 0;
            result2 = Regex.Replace(text, Pattern2, (Match nn) =>
            {
                if (i2 < 9)
                {
                    result2 = string.Format("$182:00000" + (++i2) + "00\r\n");
                    return result2;
                }
                else if (i2 < 99)
                {
                    result2 = string.Format("$182:0000" + (++i2) + "00\r\n");
                    return result2;
                }
                else if (i2 < 999)
                {
                    result2 = string.Format("$182:000" + (++i2) + "00\r\n");
                    return result2;
                }
                else if (i2 < 9999)
                {
                    result2 = string.Format("$182:00" + (++i2) + "00\r\n");
                    return result2;
                }
                else if (i2 < 99999)
                {
                    result2 = string.Format("$182:0" + (++i2) + "00\r\n");
                    return result2;
                }
                return result2;
            });
            editor.SetDocumentText(result2);
        }

        internal static void HierMeToCITE()
        {
            var editor = Editor.GetActive();
            var filename = editor.GetCurrentFileName();
            var Dir = editor.GetCurrentDirectory();
            var text = editor.GetDocumentText();

            string[] listFiles = Regex.Split(text, @"(?=<\?xml version.*>)");
            string AllText = "";
            foreach (string file in listFiles)
            {
                Match cite = Regex.Match(file, "<lnvxe:title>&#x00A7;(.*?) ");
                string result = Regex.Replace(file, "&#x00A7;(.*)</lnv:CITE>", "&#x00A7;" + cite.Groups[1].ToString() + "</lnv:CITE>");
                result = Regex.Replace(result, "<lnci:content status=\"valid\">(.*?) &#x00A7;(.*)</lnci:content>", "<lnci:content status=\"valid\">$1 &#x00A7;" + cite.Groups[1].ToString() + "</lnci:content>");
                AllText += result;
            }

            editor.SetDocumentText(AllText);
            
        }

        internal static void UpperLowerCaseBySegment()
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Hoàng Hà Dev.",
                       "Hoàng Hà Dev.",
                       "Nhập segment vào đây nhé, không nhập $ chỉ số thôi ^^!", 200, 200);

            var editor = Editor.GetActive();
            int totalopenfiles = editor.GetAllOpenFiles() - 1;
            for (int i = 0; i < totalopenfiles; i++)
            {
                editor.ActiveDocumentByIndex(i);
                string text = editor.GetDocumentText();
                text = UpperLowerSegment(text, input);
                editor.SetDocumentText(text);
            }
            editor.ActiveDocumentByIndex(0);
            MessageBox.Show("Đã upper-LOWER case cho " + totalopenfiles + " file.","Hoàng Hà - Dev.");
        }

        

        private static string UpperLowerSegment(string inputText, string inputSegment)
        {
            string pattern = @"\$"+inputSegment+@":(.*?)\r\n";
            var result = "";
            result = Regex.Replace(inputText, pattern, (Match n) =>
            {
                string outputText = Helpers.asTitleCase(n.Value.ToString());
                return outputText;
            });
            return result;
        }


        internal static void D3478()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            var curLine = editor.GetCurrentLineNumber();
            text = text.Replace("\r\n", "[!!]");
            string tempAllFiles = "";
            // Split Into Multiple Files
            string[] listFiles = Regex.Split(text, @"(?=\$00:.*?:)");
            List<string> listNewFile = new List<string>();

            foreach (string file in listFiles)
            {
                
                string pattern = @"\$40:(.*?) ([A-Z]{2}.?) (.*?)?\[!!\]";
                Match m = Regex.Match(file, pattern);
                string d00 = "$_0:" + m.Groups[2].ToString() + " " + m.Groups[3].ToString()+"$__:";
                listNewFile.Add(d00 + file);
            }

            listNewFile.Sort(new WinCompare());

            foreach (string s in listNewFile)
            {
                tempAllFiles += s;
            }

            tempAllFiles = tempAllFiles.Replace("[!!]", "\r\n");
            tempAllFiles = Regex.Replace(tempAllFiles, @"\$_0:(.*?)\$__:", "\r\n");

            editor.SetDocumentText(tempAllFiles);
            editor.GotoLineNumber(curLine);
        }


        internal static void ChumDL()
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();
            var curLine = editor.GetCurrentLineNumber();
            text = text.Replace("\r\n", "[!!]");
            string tempAllFiles = "";
            // Split Into Multiple Files
            string[] listFiles = Regex.Split(text, @"(?=\$00:.*?:)");
            List<string> listNewFile = new List<string>();

            foreach (string file in listFiles)
            {

                string pattern = @"\$54:\$(\d+) (.*?)";
                Match m = Regex.Match(file, pattern);
                string d50_52_54 = "";
                if (m.Groups[1].ToString() != "")
                {
                    d50_52_54 = "$50:" + m.Groups[1].ToString();
                    d50_52_54 += "\n$52:" + m.Groups[2].ToString();
                    d50_52_54 += "\n$54:" + m.Groups[1].ToString() + m.Groups[2].ToString();
                }
                else
                {
                    d50_52_54 = "$50:" + m.Groups[2].ToString();
                    d50_52_54 += "\n$52:" + m.Groups[2].ToString();
                    d50_52_54 += "\n$54:" + m.Groups[2].ToString();
                }
                MessageBox.Show(d50_52_54);
                //file.Replace("$54:" + m.Groups[1].ToString() + m.Groups[2].ToString(), d50_52_54);
            }

            listNewFile.Sort(new WinCompare());

            foreach (string s in listNewFile)
            {
                tempAllFiles += s;
            }

            tempAllFiles = tempAllFiles.Replace("[!!]", "\r\n");
            tempAllFiles = Regex.Replace(tempAllFiles, @"\$_0:(.*?)\$__:", "\r\n");

            editor.SetDocumentText(tempAllFiles);
            editor.GotoLineNumber(curLine);
        }


        private class WinCompare : IComparer<string>
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
