﻿/*
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
            Hide();
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
            try
            {
                FindText = "1";
                ReplaceText = "1";
                MatchCase = true;
                UseRegularExpression = false;
                OnDoAction(Action.ReplaceAll2);
            }
            catch
            {
            }
            
            //Done
            MessageBox.Show("Caselaw Format Replaced.", "Hoàng Hà Plugin");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FindText = "1";
            ReplaceText = "1";
            MatchCase = true;
            UseRegularExpression = false;
            OnDoAction(Action.ReplaceAll3);
            //Done
            MessageBox.Show("Non-Virgo Format Replaced.", "Hoàng Hà Plugin");
        }

        private void FindAndReplaceForm_Load(object sender, EventArgs e)
        {
            label7.Text = AssemblyUtils.Version;
            button1.Focus();
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
            FindText = "1";
            ReplaceText = "1";
            MatchCase = true;
            UseRegularExpression = false;
            OnDoAction(Action.footnote2end);
            MessageBox.Show("Processed....", "Hoang Ha Plugin");
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
        footnote2end
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
