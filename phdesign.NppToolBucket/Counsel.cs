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
    class Counsel
    {
        internal static void ProcessCounsel()
        {
            var editor = Editor.GetActive();
            var text = editor.GetSelectedText();
            var pos = editor.GetSelectionRange();
            if (string.IsNullOrEmpty(text)) return;
            else
            {
                string tmp = "";
                string tmpAllLines = "";
                string[] allLines = text.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string a in allLines)
                {
                    tmp = SingleCounselProcess(a);
                    tmp = tmp.Replace(", \r\n", "; \r\n");
                    // Remove Blank Lines

                    tmp = Regex.Replace(tmp, @"^(?:[\t ]*(?:\r?\n|\r))+", "");
                    tmp = tmp.Replace(" , USA", " USA");
                    tmp = tmp.Replace(", USA", " USA");
                    tmp = tmp.Replace("Fax: ", "");
                    tmp = tmp.Replace("represented by", ":");
                    tmp = tmp.Replace(", \r\n,", ", ");
                    tmp = tmp.Replace("\r\nTERMINATED:", "TERMINATED:");
                    tmp = tmp.Replace("\r\nV.\r\n", "\r\n\r\n");

                    tmpAllLines += tmp + "\r\n";
                }


                editor.SetSelectedText(tmpAllLines);
                editor.SetSelection(pos.cpMin, pos.cpMin + tmpAllLines.Length);


            }
        }

        private static string SingleCounselProcess(string input)
        {
            string tmp = input;

            // Regex
            tmp = Regex.Replace(tmp, "Email:.*", "");
            tmp = Regex.Replace(tmp, "Fax:.*", "");
            tmp = Regex.Replace(tmp, "Phone:.*", "");
            tmp = Regex.Replace(tmp, @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", "");
            tmp = Regex.Replace(tmp, @"[0-9]{3}\.[0-9]{3}\.[0-9]{4}", "");

            tmp = Regex.Replace(tmp, @"^(\d+) ?([A-Za-z](?= ))? (.*?) ([^ ]+?) ?((?<= )APT)? ?((?<= )\d*)?$", ""); // Street Number start with \d
            tmp = Regex.Replace(tmp, @"(\w+)(.*)(, \w{2})(.*)( \d+|\d+-\d+)", "$1$2$3"); // Return City, State
            tmp = Regex.Replace(tmp, @"(.*)\d+[ ](?:[A-Za-z0-9.-]+[ ]?)+(?:Avenue|Lane|Road|Boulevard|Drive|Street|Ave|Dr|Rd|Blvd|Ln|St)\.?(.*)", ""); //Street
            tmp = Regex.Replace(tmp, @"(.*)(?:Post(?:al)? (?:Office )?|P[. ]?O\.? )?Box(.*)", ""); // Remove PO.Box
            tmp = Regex.Replace(tmp, @"Designation:(.*)", ""); // Remove Designation
            tmp = Regex.Replace(tmp, @"\d(.*)(Floor|Street|Broadway|Place|Plaza|Center|Centre|Drive|Building)", "");
            tmp = Regex.Replace(tmp, @"(.*)Suite (\d+)(.*)", "");
            tmp = Regex.Replace(tmp, @"^(?:[\t ]*(?:\r?\n|\r))+", "");
            tmp = Regex.Replace(tmp, @"(.*)(\d+)(.*)Broadway(.*)", "");
            tmp = Regex.Replace(tmp, @"(.*)(\d+)(.*)Floor(.*)", "");
            tmp = Regex.Replace(tmp, @"\[COR.*\]", "");
            

            //Normal

            tmp = tmp.Replace("ATTORNEY TO BE NOTICED", "");
            tmp = tmp.Replace("[State Government]", "");
            tmp = tmp.Replace("DOJ - ", "");
            tmp = tmp.Replace("DOJ-", "");
            tmp = tmp.Replace("AUSA - ", "");
            tmp = tmp.Replace("AUSA-", "");
            tmp = tmp.Replace("USA - ", "");
            tmp = tmp.Replace("USA-", "");
            tmp = tmp.Trim();

            // Remove Blank Lines

            tmp = Regex.Replace(tmp, @"^(?:[\t ]*(?:\r?\n|\r))+", "");

            // Split

            string[] textArray = tmp.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None);
            int i = 0;
            foreach (string str in textArray.ToList())
            {
                if (str.Contains("LEAD ATTORNEY"))
                {
                    textArray[0] = textArray[0] + ", LEAD ATTORNEY";
                    textArray[i] = "";
                }
                if (str.Contains("Lead Attorney"))
                {
                    textArray[0] = textArray[0] + ", Lead Attorney";
                    textArray[i] = "";
                }
                if (str.Contains("lead attorney"))
                {
                    textArray[0] = textArray[0] + ", lead attorney";
                    textArray[i] = "";
                }
                if (str.Contains("PRO HAC VICE"))
                {
                    textArray[0] = textArray[0] + ", PRO HAC VICE";
                    textArray[i] = "";
                }
                if (str.Contains("pro hac vice"))
                {
                    textArray[0] = textArray[0] + ", pro hac vice";
                    textArray[i] = "";
                }
                if (str.Contains("Pro Hac Vice"))
                {
                    textArray[0] = textArray[0] + ", Pro Hac Vice";
                    textArray[i] = "";
                }
                if (str.ToUpper().Contains("PRO SE"))
                {
                    textArray[0] = textArray[0] + ", Pro se";
                    textArray[i] = "";
                }
                textArray[i] = textArray[i].Replace("\n","");
                textArray[i] = textArray[i].Replace("\n", "");
                textArray[i] = textArray[i].Replace("\n", "");
                textArray[i] = textArray[i].Replace("\n", "");
                textArray[i] = textArray[i].Replace("\n", "");

                i++;
            }
            // Remove Empty Lines
            textArray = textArray.Where(y => !string.IsNullOrEmpty(y)).ToArray();
            // Continue Join
                var x = String.Join(", ", textArray);
            x = x.Replace(" \r\n", " ");
            x = x.Replace(" \r\n", " ");
            x = x.Replace(", \r\n", "");
            for (int k = 0; k <= 6; k++)
            {
                x = x.Replace("\r\n\r\n", "");
                x = x.Replace(@" ,", ",");
                x = x.Replace(@"  ", " ");
                x = x.Replace(@",,", ",");
            }
            return x.ToString().Trim();
        }
    }
}