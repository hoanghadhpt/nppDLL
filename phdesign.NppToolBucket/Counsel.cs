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
                   
                    tmp = tmp.Replace(" , USA", " USA");
                    tmp = tmp.Replace(", USA", " USA");
                    tmp = tmp.Replace(", \r\n,", ", ");

                    tmpAllLines += tmp + "\r\n";
                }
                //  End line -> ;
                tmpAllLines = tmpAllLines.Replace("\r\n", ";\r\n");
                // --> Parties Pagination -> :
                foreach (string t in listPartiesDesignation)
                {
                    tmpAllLines = tmpAllLines.Replace(t + ";", t + ":");
                }
                // Ending Remove ;
                tmpAllLines = tmpAllLines.Remove(tmpAllLines.Length - 3, 3) + ".";
                editor.SetSelectedText(tmpAllLines);
                editor.SetSelection(pos.cpMin, pos.cpMin + tmpAllLines.Length);


            }
        }

        private static List<string> listPartiesDesignation = new List<string> { "Cross Appellants", "Cross Appellant", "Plaintiff-Appellant", "Plaintiff/Appellant", "Plaintiffs/Appellants", "Defendant/Respondent", "Defendants/Respondents", "Defendants-Respondents", "Defendants-Respondents-Cross Appellants", "Respondent/Intervener", "Respondents/Intervener", "Respondents/Defendants", "Appellant/Plaintiff", "Appellant-Plaintiff", "Appellants/Plaintiffs", "Intervenor-Respondent", "Plaintiff and Appellant", "Plaintiffs and Appellants", "Defendant and Respondent", "Defendants and Respondents", "Plaintiff", "Plaintiffs", "Defendant", "Defendants", "Appellee", "Appellant", "Appellants", "Third-Party", "Petitioner", "Petitioners", "Respondent", "Respondents", "Intervenor and Counterclaim Plaintiff", "Interpleader Defendant", "Counter-Defendant", "Consolidated Plaintiffs", "Consolidated Defendants", "Intervenor-Appellants", "Real party in interest and Respondent", "Respondents-Respondents", "Appellants/Appellants", "Defendant/Appellee", "Respondent-Appellant", "Petitioner-Appellee", "Defendants/Appellees/Cross-Appellants", "Defendants/Appellees", "Plaintiffs/Appellants/Cross-Appellees", "Intervenors/Appellants", "Defendants/Appellants", "Defendants'/Counterclaimants'/Appellants'", "Plaintiff/Counterdefendant/Appellee", "Defendant/Counterclaimant/Appellant", "Plaintiffs/Counterdefendants/Appellees/Cross-Appellants", "Defendants/Counterclaimants/Appellants/Cross-Appellees", "Real Party in Interest/Appellee", "Respondents/Appellees", "Plaintiffs / Appellants", "Defendants / Appellees", "Petitioner/Appellee", "Respondent/Appellee", "Plaintiffs/Appellees", "Cross Petitioner", "Cross-Respondent", "Real Parties in Interest and Appellants", "Plaintiff & Appellant", "Defendant & Respondent", "Plaintiff/Cross-Defendant and Respondent", "Cross-Complainant & Respondent", "Cross-Defendant & Appellant", "Plaintiffs & Respondents", "Defendant & Appellant", "Petitioner and Appellant", "Defendant and Respondent", "Plaintiff and Respondent", "Defendant and Appellant", "Defendants and Appellants", "Petitioners and Appellants", "Cross Respondents", "Criminal Case Defendant", "Intervenor Plaintiff", "Party Defendants", "Party Defendant", "Party Plaintiffs", "Party Plaintiff", "Defendants' - Appellees'", "Plaintiff - Appellant", "Additional Respondent", "DEFENDANT - COUNTERPLAINTIFF/RESPONDENT", "Respondent/Cross Appeal Appellant", "Defendants/Interlocutory Appellants", "Third-party defendants and cross-claim plaintiffs/Respondents", "Defendants/Third Party Plaintiffs/Appellants", "Third Party Defendants/Cross Claim Plaintiffs/Respondents", "Plaintiff/Respondent", "Appellant/Cross Appeal Respondent", "Respondent / Cross-Appeal Appellant", "Appellees/Cross Appealant", "Respondent, Cross Appellant", "Third Party Defendants", "Third Party Plaintiff", "Third Party Defendant / Appellant", "Defendant/Counterclaimant/Appellees", "Counterdefendants/Appellants", "Defendants/Counterclaimants/Appellants", "Plaintiffs/Counterdefendants/Appellees", "Party Respondent", "Counterclaim Plaintiff/Respondent", "Counterclaim Defendant/Respondent", "Appellant/ Respondent/ Attorney for Defendant", "Defendant/Counterclaimant/Third Party Claimant/Cross Claimant/Appellant", "Third Party and Cross Claim Defendant/Appellee", "Defendants and Counterclaim Plaintiffs", "Cross-Petitioners", "Petitioners-Counter Defendants-Appellants", "Amici", "Amicus", "Curae", "U. S. Attorneys"};

        private static string SingleCounselProcess(string input)
        {
            string tmp = input;
            
            // Pre Process

            string[] txtCC = tmp.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None);
            int cc = 0;
            foreach (string t in txtCC)
            {
                if (t.Trim().ToUpper().Contains(" STREET")
                        || t.Trim().ToUpper().Contains("SUITE ")
                        || t.Trim().ToUpper().Contains("ST STE")
                        || t.Trim().ToUpper().Contains("BLDG.")
                    )
                {
                    txtCC[cc] = "";
                }
                else if (t.Trim().ToUpper().StartsWith("STE ")
                        || t.Trim().ToUpper().StartsWith("#")
                        || t.Trim().ToUpper().StartsWith("STE.")
                        || t.Trim().ToUpper().StartsWith("TERMINUS")
                        || t.Trim().ToUpper().StartsWith("DIRECT:")
                        || t.Trim().ToUpper().StartsWith("V.")
                        || t.Trim().ToUpper().StartsWith("TELEPHONE")
                        || t.Trim().ToUpper().StartsWith("FACSIMILE")
                        || t.Trim().ToUpper().StartsWith("REPRESENTED BY")
                        )
                {
                    txtCC[cc] = "";
                }
                else if (t.Trim().ToUpper().StartsWith("STE ")
                        || t.Trim().ToUpper().StartsWith("#")
                        || t.Trim().ToUpper().StartsWith("STE.")
                        || t.Trim().ToUpper().StartsWith("1")
                        || t.Trim().ToUpper().StartsWith("2")
                        || t.Trim().ToUpper().StartsWith("3")
                        || t.Trim().ToUpper().StartsWith("4")
                        || t.Trim().ToUpper().StartsWith("5")
                        || t.Trim().ToUpper().StartsWith("6")
                        || t.Trim().ToUpper().StartsWith("7")
                        || t.Trim().ToUpper().StartsWith("8")
                        || t.Trim().ToUpper().StartsWith("9")
                        || t.Trim().ToUpper().StartsWith("TERMINUS")
                        || t.Trim().ToUpper().StartsWith("ROOM")
                        || t.Trim().ToUpper().StartsWith("[COR ")
                        )
                {
                    txtCC[cc] = "";
                }
                else if (t.Trim().ToUpper().EndsWith("MALL")
                        || t.Trim().ToUpper().EndsWith("BLVD")
                        || t.Trim().ToUpper().EndsWith("BLDG")
                        || t.Trim().ToUpper().EndsWith(" MAIN")
                        || t.Trim().ToUpper().EndsWith(" SQUARE")
                        || t.Trim().ToUpper().EndsWith(" ST")
                        || t.Trim().ToUpper().EndsWith(" FLOOR")
                        || t.Trim().ToUpper().EndsWith(" PLACE")
                        || t.Trim().ToUpper().EndsWith(" HIGHWAY")
                        || t.Trim().ToUpper().EndsWith(" STATION")
                        || t.Trim().ToUpper().EndsWith(" ROAD")
                        || t.Trim().ToUpper().EndsWith(" RD.")
                        || t.Trim().ToUpper().EndsWith(" BUILDING")
                        || t.Trim().ToUpper().EndsWith(" VILLAGE")
                        || t.Trim().ToUpper().EndsWith(" VILLAGE")
                        )
                {
                    txtCC[cc] = "";
                }
                else if (isNUMBER(t.Trim()))
                {
                    txtCC[cc] = "";
                }
                cc++;
            }
            tmp = string.Join(System.Environment.NewLine, txtCC);

            // Regex
            tmp = Regex.Replace(tmp, "Email:.*", "");
            tmp = Regex.Replace(tmp, "Fax:.*", "");
            tmp = Regex.Replace(tmp, "Phone:.*", "");
            
            tmp = Regex.Replace(tmp, @"\(?[0-9]{3}\)?\.?/?-? ?[0-9]{3}\.?/?-? ?[0-9]{4}", "");

            tmp = Regex.Replace(tmp, @"^(\d+) ?([A-Za-z](?= ))? (.*?) ([^ ]+?) ?((?<= )APT)? ?((?<= )\d*)?$", ""); // Street Number start with \d
            tmp = Regex.Replace(tmp, @"(\w+)(.*)(, \w{2})(.*)( \d+|\d+-\d+)", "$1$2$3"); // Return City, State
            tmp = Regex.Replace(tmp, @"(.*)\d+[ ](?:[A-Za-z0-9.-]+[ ]?)+(?:Avenue|Lane|Road|Boulevard|Drive|Street|Ave|Dr|Rd|Blvd|Ln|St)\.?(.*)?", ""); //Street
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

        private static bool isNUMBER(string input)
        {
            int n;
            bool isNumeric = int.TryParse(input, out n);
            return isNumeric;
        }
    }
}