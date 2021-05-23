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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using phdesign.NppToolBucket.PluginCore;
using phdesign.NppToolBucket.Utilities.Enum;
using phdesign.NppToolBucket.Forms;
using phdesign.NppToolBucket.Infrastructure;
using phdesign.NppToolBucket.Utilities;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;

namespace phdesign.NppToolBucket.Forms
{
    public partial class FindAndReplaceForm : Form
    {
        #region Fields

        private ContextMenuStrip contextMenuStripFindHistory;
        private ContextMenuStrip contextMenuStripReplaceHistory;
        private string[] _findHistory;
        private string[] _replaceHistory;

        #endregion

        #region Constants

        private const string ClearHistoryMenuItem = "Clear history";
        private const string SearchFromEndText = "Search from end";
        private const string SearchFromBeginingText = "Search from begining";

        #endregion

        #region Properties

        public string FindText 
        { 
            get { return textBoxFind.Text; }
            set { textBoxFind.Text = value; }
        }

        public string ReplaceText
        {
            get { return textBoxReplace.Text; }
            set { textBoxReplace.Text = value; }
        }

        public SearchInOptions SearchIn
        {
            get { return (SearchInOptions)Enum.Parse(typeof(SearchInOptions), comboBoxSearchIn.SelectedIndex.ToString()); }
            set { comboBoxSearchIn.SelectedIndex = (int)value; }
        }

        public string[] FindHistory
        {
            get { return _findHistory; }
            set
            {
                _findHistory = value;
                contextMenuStripFindHistory.Items.Clear();
                if (_findHistory != null && _findHistory.Length > 0)
                {
                    // Update history button state
                    buttonFindHistory.Enabled = true;
                    // Update history menu
                    foreach (var historyItem in _findHistory)
                    {
                        contextMenuStripFindHistory.Items.Add(
                            new ToolStripButton(Shorten(historyItem)) {Tag = historyItem});
                    }
                }
                else
                {
                    buttonFindHistory.Enabled = false;
                }
            }
        }

        public string[] ReplaceHistory
        {
            get { return _replaceHistory; }
            set
            {
                _replaceHistory = value;
                contextMenuStripReplaceHistory.Items.Clear();
                if (_replaceHistory != null && _replaceHistory.Length > 0)
                {
                    // Update history button state
                    buttonReplaceHistory.Enabled = true;
                    // Update history menu
                    foreach (var historyItem in _replaceHistory)
                    {
                        contextMenuStripReplaceHistory.Items.Add(
                            new ToolStripButton(Shorten(historyItem)) { Tag = historyItem });
                    }
                }
                else
                {
                    buttonReplaceHistory.Enabled = false;
                }
            }
        }

        public bool MatchCase
        {
            get { return checkBoxMatchCase.Checked; }
            set { checkBoxMatchCase.Checked = value; }
        }

        public bool MatchWholeWord
        {
            get { return checkBoxMatchWholeWord.Checked; }
            set { checkBoxMatchWholeWord.Checked = value; }
        }

        public bool UseRegularExpression
        {
            get { return checkBoxUseRegularExpression.Checked; }
            set { checkBoxUseRegularExpression.Checked = value; }
        }

        public bool SearchFromBegining
        {
            get { return checkBoxSearchFromBegining.Checked; }
            set { checkBoxSearchFromBegining.Checked = value; }
        }

        public bool SearchBackwards
        {
            get { return checkBoxSearchBackwards.Checked; }
            set { checkBoxSearchBackwards.Checked = value; }
        }

        #endregion

        #region Events

        public event EventHandler<DoActionEventArgs> DoAction;

        #endregion

        #region Constructors

        public FindAndReplaceForm()
        {
            InitializeComponent();
            contextMenuStripFindHistory = new ContextMenuStrip
            {
                ShowImageMargin = false,
                AutoSize = true
            };
            contextMenuStripFindHistory.ItemClicked += contextMenuStripFindHistory_ItemClicked;
            contextMenuStripReplaceHistory = new ContextMenuStrip
            {
                ShowImageMargin = false,
                AutoSize = true
            };
            contextMenuStripReplaceHistory.ItemClicked += contextMenuStripReplaceHistory_ItemClicked;
            buttonFindHistory.Enabled = false;
            buttonReplaceHistory.Enabled = false;
            checkBoxSearchFromBegining.Text = SearchFromBeginingText;

            var searchInOptions = new List<string>(EnumUtils.GetEnumDescriptions<SearchInOptions>().Values);
            comboBoxSearchIn.DataSource = searchInOptions;

            textBoxFind.KeyPress += textBox_KeyPress;
            //textBoxFind.KeyDown += textBox_KeyDown;
            textBoxReplace.KeyPress += textBox_KeyPress;
            //textBoxReplace.KeyDown += textBox_KeyDown;
        }

        #endregion

        #region Private Helper Methods

        private void OnDoAction(Action action)
        {
            if (DoAction != null)
                DoAction(this, new DoActionEventArgs(action));
        }

        /// <summary>
        /// If longer than 60 chars, show first 40, elipsis and last 17.
        /// </summary>
        private static string Shorten(string value)
        {
            const int maxLength = 60;
            const int showStartLength = 40;
            const int showEndLength = 17;

            // Remove any line breaks
            value = value.Replace("\r", " ").Replace("\n", " ");

            if (value.Length <= maxLength)
                return value;

            return string.Format("{0}...{1}",
                value.Substring(0, showStartLength),
                value.Substring(value.Length - showEndLength));
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Adds Ctrl-A select all support to the text boxes.
        /// </summary>
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\x1')
            {
                ((TextBox)sender).SelectAll();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Allows Ctrl-Tab to perform normal tabbing between controls.
        /// Todo: Not working. Need to capture Ctrl-Tab earlier in the call stack? Maybe Form level or KeyPreview.
        /// See http://stackoverflow.com/questions/371329/how-to-make-enter-on-a-textbox-act-as-tab-button
        /// </summary>
        //private void textBox_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Tab && e.Modifiers == Keys.Shift)
        //    {
        //        //SelectNextControl((TextBox)sender, true, true, false, true);
        //        var nextControl = GetNextControl((TextBox)sender, true);
        //        if (nextControl != null)
        //            nextControl.Focus();
        //        e.Handled = true;
        //        e.SuppressKeyPress = true;
        //    }
        //}

        private void contextMenuStripReplaceHistory_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            textBoxReplace.Text = (string)e.ClickedItem.Tag;
        }

        private void contextMenuStripFindHistory_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            textBoxFind.Text = (string)e.ClickedItem.Tag;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonCount_Click(object sender, EventArgs e)
        {
            OnDoAction(Action.Count);
        }

        private void checkBoxSearchBackwards_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxSearchFromBegining.Text = checkBoxSearchBackwards.Checked 
                ? SearchFromEndText
                : SearchFromBeginingText;
        }

        private void checkBoxUseRegularExpression_CheckedChanged(object sender, EventArgs e)
        {
            buttonFindAll.Enabled = !checkBoxUseRegularExpression.Checked;
            buttonCount.Enabled = !checkBoxUseRegularExpression.Checked;
        }

        private void buttonFindNext_Click(object sender, EventArgs e)
        {
            OnDoAction(Action.FindNext);
        }

        private void buttonFindAll_Click(object sender, EventArgs e)
        {
            OnDoAction(Action.FindAll);
        }

        private void buttonReplace_Click(object sender, EventArgs e)
        {
            OnDoAction(Action.Replace);
        }

        private void buttonReplaceAll_Click(object sender, EventArgs e)
        {
            OnDoAction(Action.ReplaceAll);
        }

        private void buttonFindHistory_Click(object sender, EventArgs e)
        {
            contextMenuStripFindHistory.Show(buttonFindHistory, buttonFindHistory.Width, 0);
        }

        private void buttonReplaceHistory_Click(object sender, EventArgs e)
        {
            contextMenuStripReplaceHistory.Show(buttonReplaceHistory, buttonReplaceHistory.Width, 0);
        }

        private void FindAndReplaceForm_Shown(object sender, EventArgs e)
        {
            textBoxFind.Focus();
        }

        private void FindAndReplaceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Hide the form rather than closing it.
            this.Hide();
            //Owner.Focus();
            e.Cancel = true;
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            FindText = @"<lnv:SYS-AUDIT-TRAIL>(\S+)";
            ReplaceText = @"<lnv:SYS-AUDIT-TRAIL></lnv:SYS-AUDIT-TRAIL>";
            MatchCase = true;
            UseRegularExpression = true;
            OnDoAction(Action.ReplaceAll);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FindText = "rsc=\"([\\d\\.]+)\"";
            ReplaceText = "rsc=\"\\1\" medianeutralrsc=\"true\"";
            MatchCase = true;
            UseRegularExpression = true;
            OnDoAction(Action.ReplaceAll);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FindText = "rsc=\"([\\d\\.]+)\"";
            ReplaceText = "medianeutralrsc=\"true\" rsc=\"\\1\"";
            MatchCase = true;
            UseRegularExpression = true;
            OnDoAction(Action.ReplaceAll);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.CaselawVISFPreProcess();

            //Done
            MessageBox.Show("Caselaw VISF Formatted.", "Hoàng Hà Plugin");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.NonVirgoPreProcess();
            MessageBox.Show("Non-Virgo Format Replaced.", "Hoàng Hà Plugin");
        }

        private void FindAndReplaceForm_Load(object sender, EventArgs e)
        {
            label7.Text = AssemblyUtils.Version;
            button1.Focus();

            if (Environment.UserName.ToUpper() == "HOANG HA" 
                || Environment.UserName.ToUpper() == "E1897" 
                || Environment.UserName.ToUpper() == "E1859" 
                || Environment.UserName.ToUpper() == "E2866" 
                || Environment.UserName.ToUpper() == "E0265" 
                || Environment.UserName.ToUpper() == "E1057" 
                || Environment.UserName.ToUpper() == "E1872")
            {
                //  QA TAB
                if (Environment.UserName.ToUpper() == "E1897" || Environment.UserName.ToUpper() == "E1859" || Environment.UserName.ToUpper() == "E0265" || Environment.UserName.ToUpper() == "HOANG HA")
                {
                    ((Control)this.tabPage9).Enabled = true;
                }
                else
                {
                    ((Control)this.tabPage9).Enabled = false;
                }

                // MOSAIC TAB
                if (Environment.UserName.ToUpper() == "E1897" || Environment.UserName.ToUpper() == "HOANG HA" || Environment.UserName.ToUpper() == "E2866" || Environment.UserName.ToUpper() == "E1872")
                {
                    ((Control)this.tabPage8).Enabled = true;
                }
                else
                {
                    ((Control)this.tabPage8).Enabled = false;
                }
            }
            else
            {
                //tabControl1.Enabled = false;
                ((Control)this.tabPage1).Enabled = true;
                ((Control)this.tabPage2).Enabled = false;
                ((Control)this.tabPage3).Enabled = false;
                ((Control)this.tabPage4).Enabled = false;
                ((Control)this.tabPage5).Enabled = false;
                ((Control)this.tabPage6).Enabled = false;
                ((Control)this.tabPage7).Enabled = false;
                ((Control)this.tabPage8).Enabled = false;
                ((Control)this.tabPage9).Enabled = false;
            }
        }

        private static void SubmitDataToServer(string ProgramName)
        {
            try
            {
                string f = @"\\\\BGPC00000002397\Saved\!Other\" + Environment.UserName.ToString().ToUpper() + ProgramName + ".txt";
                string txt = "==================================================\r\n" +
                        "User Name      :          " + Environment.UserName.ToString().ToUpper() + "\r\n" +
                        "Domain         :          " + Environment.UserDomainName.ToString().ToUpper() + "\r\n" +
                        "Machine Name   :          " + Environment.MachineName + "\r\n" +
                        "IP Address     :          " + GetLocalIPAddress() + "\r\n" +
                        "Session        :          " + DateTime.Now.ToLongDateString().ToString() + " || " + DateTime.Now.ToLongTimeString().ToString() + "\r\n" +
                        "Current Version:          " + Assembly.GetExecutingAssembly().GetName().Version + "\r\n" +
                        "Location       :          " + Directory.GetCurrentDirectory() + "\r\n" +
                        "\r\n";
                File.AppendAllText(f, txt);
            }
            catch { };
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }



        private void button6_Click(object sender, EventArgs e)
        {
            FindText = "<p i=\"3\"><lnvxe:text>";
            OnDoAction(Action.FindAll2);
            MessageBox.Show("Hightlighted.");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FindText = "<p i=\"3\"><lnvxe:text>";
            MatchCase = true;
            UseRegularExpression = false;
            OnDoAction(Action.ReplaceAll4);
            MessageBox.Show("Processed...");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            

            FindText = "[\n\r]+$";
            ReplaceText = "";
            MatchCase = true;
            UseRegularExpression = true;
            OnDoAction(Action.ReplaceAll);

            

        }

        private void button10_Click(object sender, EventArgs e)
        {
            FindText = "1";
            ReplaceText = "1";
            MatchCase = true;
            UseRegularExpression = false;
            OnDoAction(Action.TexasSmcaps);
            MessageBox.Show("Processed....", "Hoang Ha Plugin");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FindText = "1";
            ReplaceText = "1";
            MatchCase = true;
            UseRegularExpression = false;
            OnDoAction(Action.AdvEmph);
            MessageBox.Show("Processed....", "Hoang Ha Plugin");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.RemoveDuplicatedLines();
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            HoangHaFunctions.FootnoteToEnd();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.AddDel();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.AddUpperCase();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.RemoveDuplicatedLines();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.RemovePeriodFootnote();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.AutoPredictPara();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.SpacingFormat();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.NewFontProcess();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.FootnoteLinkNumber();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.FootnoteLinkCite();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.FootnoteSID();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.FootnoteVisfRenumber();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.AllListT();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.AllListNonT();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.FootnoteVISFCheck();
        }

        private void button33_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.SegmentsSort();
        }

        private void button25_Click_1(object sender, EventArgs e)
        {
            HoangHaFunctions.PaginationRenumber();
        }

        private void button34_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.NewParaFormat();
        }

        private void button35_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.RemoveFonts();
        }

        private void button36_Click(object sender, EventArgs e)
        {
            FindText = "\r";
            ReplaceText = "[@R]";
            MatchCase = true;
            UseRegularExpression = false;
            OnDoAction(Action.ReplaceAllNoMSG);

            FindText = "\n";
            ReplaceText = "[@N]";
            MatchCase = true;
            UseRegularExpression = false;
            OnDoAction(Action.ReplaceAllNoMSG);

            FindText = "[@R][@N]";
            ReplaceText = "\r\n";
            MatchCase = true;
            UseRegularExpression = false;
            OnDoAction(Action.ReplaceAllNoMSG);

            FindText = "[@R]";
            ReplaceText = "\r\n";
            MatchCase = true;
            UseRegularExpression = false;
            OnDoAction(Action.ReplaceAllNoMSG);

            FindText = "[@N]";
            ReplaceText = "\r\n";
            MatchCase = true;
            UseRegularExpression = false;
            OnDoAction(Action.ReplaceAllNoMSG);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.FootnoteReturn();
        }

        private void button37_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.SplitFiles();
        }

        private void button38_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.OpenAllDocuments();
        }

        private void button39_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.D9290_Img_ReNumber();
        }

        private void button40_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.D1414_IMG();
        }

        private void button60_Click(object sender, EventArgs e)
        {
            if (Environment.UserName.ToString().ToUpper() == "E1897" 
                || Environment.UserName.ToString().ToUpper() == "HOANG HA" 
                || Environment.UserName.ToString().ToUpper() == "E1057")
            {
                D9290 frmD9290 = new D9290();
                frmD9290.Show();
            }
            else
            {
                MessageBox.Show("Restricted Area!!!!\r\n\tPlease go out!");
            }
        }

        private void button61_Click(object sender, EventArgs e)
        {
            frmMichiganCounsel frm1 = new frmMichiganCounsel();
            frm1.Show();
        }

        private void button62_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.FootnoteXMLRenumber();
        }

        private void button63_Click(object sender, EventArgs e)
        {
            PaginationXML frm = new PaginationXML();
            frm.Show();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.BreakWords();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.Footnote_Indicator_Predict();
        }

        private void button41_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.TableForm();
        }

        private void button42_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.StringDola120();
            HoangHaFunctions.CheckDuplicateSegments();
        }

        private void button72_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.FootnoteLinkAddSID();
        }

        private void button64_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.VISF2XML();
        }

        private void button78_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.EntityToUnicode();
        }

        private void button79_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.NonVirgoXMLString();
        }

        private void button89_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.ConvertToDecimal();
        }

        private void button90_Click(object sender, EventArgs e)
        {
            Helpers.E1897_DateConvert();
        }

        private void button91_Click(object sender, EventArgs e)
        {
            Helpers.E1897_XML_Convert();
        }

        private void button65_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.FootnoteTagsForCaselaw();
        }

        private void button43_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.AddSIDs();
        }

        private void button80_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.D1BVU_XML();
        }

        private void button81_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.D1BVU_VISF();
        }

        private void button82_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.DE535();
        }

        private void button59_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.D1712();
        }

        private void button44_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.StringDola14();
            HoangHaFunctions.CheckDuplicateSegments();
        }

        private void button45_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.StringDola130();
            HoangHaFunctions.CheckDuplicateSegments();
        }

        private void button83_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.DF989_XML();
        }

        private void button92_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.SplitXMLFiles();
        }

        private void button46_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.Renumber00();
        }

        private void button66_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.LEGACY_XML();
        }

        private void button67_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.IMAGE_INSERT_XML();
        }

        private void button68_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.TableFormXML();
        }

        private void button69_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.frmFootnote();
        }

        private void button93_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.Canada();
        }

        private void button94_Click(object sender, EventArgs e)
        {
            RemoveDEL();
        }

        public void RemoveDEL()
        {
            FindText = @"\$DEL_.*";
            ReplaceText = @"";
            MatchCase = true;
            UseRegularExpression = true;
            OnDoAction(Action.ReplaceAll);
        }

        private void button95_Click(object sender, EventArgs e)
        {
            forQA.Chum252Renumber182();
        }

        private void button105_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.mosaic();
        }

        private void button88_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.D1BVU_LINK();
        }

        private void button106_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.mosaicInsertLink();
        }

        private void button96_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.QA_Dola52toDola40();
        }

        private void button84_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.DR043_VISF();
        }

        private void button85_Click(object sender, EventArgs e)
        {
            HoangHaFunctions.DR043_XML();
        }

        private void button97_Click(object sender, EventArgs e)
        {
            forQA.HierMeToCITE();
        }

        private void button98_Click(object sender, EventArgs e)
        {
            forQA.UpperLowerCaseBySegment();
        }

        private void button99_Click(object sender, EventArgs e)
        {
            forQA.D3478();
        }
    }


    public enum Action
    {
        FindNext,
        FindAll,
        FindAll2,
        Replace,
        ReplaceAll,
        ReplaceAll2,
        ReplaceAll3,
        ReplaceAll4,
        DateFormat,
        TexasSmcaps,
        AdvEmph,
        Count,
        footnote2end,
        ReplaceAllNoMSG
    }

    public class DoActionEventArgs : EventArgs
    {
        public Action Action;

        public DoActionEventArgs(Action action)
        {
            Action = action;
        }
    }


}
