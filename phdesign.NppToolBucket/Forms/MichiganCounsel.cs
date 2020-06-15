using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace phdesign.NppToolBucket.Forms
{
    public partial class frmMichiganCounsel : Form
    {
        public frmMichiganCounsel()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox2.Clear();
            ProcessCounsel();
            littleEmphasys();
        }

        private void ProcessCounsel()
        {
            foreach (string line in textBox1.Lines)
            {
                //textBox2.Text += ReplaceDesignation(line)+Environment.NewLine;
                textBox2.Text += ReplaceDesignationV2(line) + Environment.NewLine;
            }
        }

        private string ReplaceDesignationV2(string input)
        {
            string[] CounselContents = input.Split(' ');
            for (int i = 0; i < CounselContents.Length; i++)
            {
                if (CounselContents[i].EndsWith(":"))
                {
                    CounselContents[i] = CounselContents[i].Replace("AC", "Amicus Curiae");
                    CounselContents[i] = CounselContents[i].Replace("AE", "Appellee");
                    CounselContents[i] = CounselContents[i].Replace("AT", "Appellant");
                    CounselContents[i] = CounselContents[i].Replace("AK", "Also Known As ");
                    CounselContents[i] = CounselContents[i].Replace("CG", "Charging Party (MERC)");
                    CounselContents[i] = CounselContents[i].Replace("CL", "Claimant");
                    CounselContents[i] = CounselContents[i].Replace("CD", "Counter Defendant");
                    CounselContents[i] = CounselContents[i].Replace("CP", "Counter Plaintiff");
                    CounselContents[i] = CounselContents[i].Replace("DB", "Doing Business As");
                    CounselContents[i] = CounselContents[i].Replace("DF", "Defendant");
                    CounselContents[i] = CounselContents[i].Replace("FK", "Formerly Known As");
                    CounselContents[i] = CounselContents[i].Replace("GD", "Garnishee Defendant");
                    CounselContents[i] = CounselContents[i].Replace("GR", "Garnishor");
                    CounselContents[i] = CounselContents[i].Replace("GP", "Garnishee Plaintiff");
                    CounselContents[i] = CounselContents[i].Replace("ID", "Intervening Defendant");
                    CounselContents[i] = CounselContents[i].Replace("IE", "Intervening Appellee");
                    CounselContents[i] = CounselContents[i].Replace("IR", "Intervening Respondent");
                    CounselContents[i] = CounselContents[i].Replace("IN", "Intervenor - Miscellaneous");
                    CounselContents[i] = CounselContents[i].Replace("IP", "Intervening Plaintiff");
                    CounselContents[i] = CounselContents[i].Replace("IT", "Intervening Appellant");
                    CounselContents[i] = CounselContents[i].Replace("NP", "Not A Party Or Not Participating");
                    CounselContents[i] = CounselContents[i].Replace("PL", "Plaintiff");
                    CounselContents[i] = CounselContents[i].Replace("PT", "Petitioner");
                    CounselContents[i] = CounselContents[i].Replace("RS", "Respondent");
                    CounselContents[i] = CounselContents[i].Replace("TD", "Third Party Defendant");
                    CounselContents[i] = CounselContents[i].Replace("TP", "Third Party Plaintiff");
                    CounselContents[i] = CounselContents[i].Replace("XD", "Cross Defendant");
                    CounselContents[i] = CounselContents[i].Replace("XE", "Cross Appellee");
                    CounselContents[i] = CounselContents[i].Replace("XP", "Cross Plaintiff");
                    CounselContents[i] = CounselContents[i].Replace("XT", "Cross Appellant");
                    CounselContents[i] = CounselContents[i].Replace("ZZ", "Other Misc Connections");
                    CounselContents[i] = CounselContents[i].Replace("AG", "Attorney General");
                    CounselContents[i] = CounselContents[i].Replace("AMI", "Amicus Attorneys");
                    CounselContents[i] = CounselContents[i].Replace("APP", "Appointed");
                    CounselContents[i] = CounselContents[i].Replace("CAD", "Criminal Appellate Division");
                    CounselContents[i] = CounselContents[i].Replace("CO", "Co-Counsel");
                    CounselContents[i] = CounselContents[i].Replace("COR", "Corporate Counsel");
                    CounselContents[i] = CounselContents[i].Replace("CTY", "City Attorney");
                    CounselContents[i] = CounselContents[i].Replace("GAL", "Guardian Ad Litem");
                    CounselContents[i] = CounselContents[i].Replace("NOA", "Corporate Party - No Attorney");
                    CounselContents[i] = CounselContents[i].Replace("MAC", "Michigan Appellate Assigned Counsel Service");
                    CounselContents[i] = CounselContents[i].Replace("OF", "Of Counsel to Attorney of Record");
                    CounselContents[i] = CounselContents[i].Replace("OSA", "Out-of-State Attorney");
                    CounselContents[i] = CounselContents[i].Replace("PAS", "Prosecutors Appellate Service");
                    CounselContents[i] = CounselContents[i].Replace("PRO", "Pro Per");
                    CounselContents[i] = CounselContents[i].Replace("PRS", "Prosecutor");
                    CounselContents[i] = CounselContents[i].Replace("RET", "Retained");
                    CounselContents[i] = CounselContents[i].Replace("SAD", "State Appellate Defender's Office");
                    CounselContents[i] = CounselContents[i].Replace("SAM", "(SEE ABOVE)");
                    CounselContents[i] = CounselContents[i].Replace("SG", "Solicitor General");
                    CounselContents[i] = CounselContents[i].Replace("SUP", "Supervising Attorney - PRS");
                    CounselContents[i] = CounselContents[i].Replace("TWP", "Township Attorney");
                }
                else if (CounselContents[i].Equals("PRO.") || CounselContents[i].Equals("PRO.</lnvxe:counsel>"))
                {
                    CounselContents[i] = CounselContents[i].Replace("PRO.", "Pro se.");
                }
                else if (CounselContents[i].Equals("PRS.") || CounselContents[i].Equals("PRS.</lnvxe:counsel>"))
                {
                    CounselContents[i] = CounselContents[i].Replace("PRS.", "Prosecutor.");
                }
                else if (CounselContents[i].Equals("RET.") || CounselContents[i].Equals("RET.</lnvxe:counsel>"))
                {
                    CounselContents[i] = CounselContents[i].Replace("RET.", "Retained.");
                }
                else if (CounselContents[i].Equals("SAD.") || CounselContents[i].Equals("SAD.</lnvxe:counsel>"))
                {
                    CounselContents[i] = CounselContents[i].Replace("SAD.", "State Appellate Defender's Office.");
                }
                else if (CounselContents[i].Equals("SAM.") || CounselContents[i].Equals("SAM.</lnvxe:counsel>"))
                {
                    CounselContents[i] = CounselContents[i].Replace("SAM.", "(SEE ABOVE).");
                }
                else if (CounselContents[i].Equals("SG.") || CounselContents[i].Equals("SG.</lnvxe:counsel>"))
                {
                    CounselContents[i] = CounselContents[i].Replace("SG.", "Solicitor General.");
                }
                else if (CounselContents[i].Equals("SUP.") || CounselContents[i].Equals("SUP.</lnvxe:counsel>"))
                {
                    CounselContents[i] = CounselContents[i].Replace("SUP.", "Supervising Attorney - PRS.");
                }
                else if (CounselContents[i].Equals("TWP.") || CounselContents[i].Equals("TWP.</lnvxe:counsel>"))
                {
                    CounselContents[i] = CounselContents[i].Replace("TWP.", "Township Attorney.");
                }
            }
            string result = String.Join(" ", CounselContents);
            return result;
        }

        private void littleEmphasys()
        {
            textBox2.Text = textBox2.Text.Replace("&", "&#x0026;");
            textBox2.Text = textBox2.Text.Replace("-", "&#x2D;");
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
            {
                textBox1.SelectAll();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
            {
                textBox2.SelectAll();
            }
        }
    }
}
