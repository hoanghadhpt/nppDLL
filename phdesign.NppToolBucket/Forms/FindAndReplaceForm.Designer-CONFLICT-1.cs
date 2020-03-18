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

namespace phdesign.NppToolBucket.Forms
{
    partial class FindAndReplaceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonFindNext = new System.Windows.Forms.Button();
            this.buttonFindAll = new System.Windows.Forms.Button();
            this.buttonCount = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonReplace = new System.Windows.Forms.Button();
            this.buttonReplaceAll = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonReplaceHistory = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonFindHistory = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxReplace = new System.Windows.Forms.TextBox();
            this.textBoxFind = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxMatchCase = new System.Windows.Forms.CheckBox();
            this.checkBoxMatchWholeWord = new System.Windows.Forms.CheckBox();
            this.checkBoxSearchFromBegining = new System.Windows.Forms.CheckBox();
            this.checkBoxSearchBackwards = new System.Windows.Forms.CheckBox();
            this.checkBoxUseRegularExpression = new System.Windows.Forms.CheckBox();
            this.lbNotify = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button11 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxSearchIn = new System.Windows.Forms.ComboBox();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(828, 470);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.buttonFindNext);
            this.flowLayoutPanel1.Controls.Add(this.buttonFindAll);
            this.flowLayoutPanel1.Controls.Add(this.buttonCount);
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.buttonReplace);
            this.flowLayoutPanel1.Controls.Add(this.buttonReplaceAll);
            this.flowLayoutPanel1.Controls.Add(this.panel2);
            this.flowLayoutPanel1.Controls.Add(this.buttonClose);
            this.flowLayoutPanel1.Controls.Add(this.pictureBox1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(11, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1, 250);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.Visible = false;
            // 
            // buttonFindNext
            // 
            this.buttonFindNext.Location = new System.Drawing.Point(3, 3);
            this.buttonFindNext.Name = "buttonFindNext";
            this.buttonFindNext.Size = new System.Drawing.Size(75, 22);
            this.buttonFindNext.TabIndex = 9;
            this.buttonFindNext.Text = "&Find Next";
            this.buttonFindNext.UseVisualStyleBackColor = true;
            this.buttonFindNext.Click += new System.EventHandler(this.buttonFindNext_Click);
            // 
            // buttonFindAll
            // 
            this.buttonFindAll.Location = new System.Drawing.Point(3, 31);
            this.buttonFindAll.Name = "buttonFindAll";
            this.buttonFindAll.Size = new System.Drawing.Size(75, 22);
            this.buttonFindAll.TabIndex = 10;
            this.buttonFindAll.Text = "Find A&ll";
            this.buttonFindAll.UseVisualStyleBackColor = true;
            this.buttonFindAll.Click += new System.EventHandler(this.buttonFindAll_Click);
            // 
            // buttonCount
            // 
            this.buttonCount.Location = new System.Drawing.Point(3, 59);
            this.buttonCount.Name = "buttonCount";
            this.buttonCount.Size = new System.Drawing.Size(75, 22);
            this.buttonCount.TabIndex = 11;
            this.buttonCount.Text = "C&ount";
            this.buttonCount.UseVisualStyleBackColor = true;
            this.buttonCount.Click += new System.EventHandler(this.buttonCount_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(3, 87);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(75, 3);
            this.panel1.TabIndex = 7;
            // 
            // buttonReplace
            // 
            this.buttonReplace.Location = new System.Drawing.Point(3, 96);
            this.buttonReplace.Name = "buttonReplace";
            this.buttonReplace.Size = new System.Drawing.Size(75, 22);
            this.buttonReplace.TabIndex = 12;
            this.buttonReplace.Text = "&Replace";
            this.buttonReplace.UseVisualStyleBackColor = true;
            this.buttonReplace.Click += new System.EventHandler(this.buttonReplace_Click);
            // 
            // buttonReplaceAll
            // 
            this.buttonReplaceAll.Location = new System.Drawing.Point(3, 124);
            this.buttonReplaceAll.Name = "buttonReplaceAll";
            this.buttonReplaceAll.Size = new System.Drawing.Size(75, 22);
            this.buttonReplaceAll.TabIndex = 13;
            this.buttonReplaceAll.Text = "Replace &All";
            this.buttonReplaceAll.UseVisualStyleBackColor = true;
            this.buttonReplaceAll.Click += new System.EventHandler(this.buttonReplaceAll_Click);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(3, 152);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(75, 3);
            this.panel2.TabIndex = 8;
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(3, 161);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 22);
            this.buttonClose.TabIndex = 14;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::phdesign.NppToolBucket.Properties.Resources.cry_mouth_icon;
            this.pictureBox1.Location = new System.Drawing.Point(3, 189);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(71, 58);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tableLayoutPanel2.Controls.Add(this.buttonReplaceHistory, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.buttonFindHistory, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.textBoxReplace, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.textBoxFind, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(2, 464);
            this.tableLayoutPanel2.TabIndex = 2;
            this.tableLayoutPanel2.Visible = false;
            // 
            // buttonReplaceHistory
            // 
            this.buttonReplaceHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReplaceHistory.Image = global::phdesign.NppToolBucket.Properties.Resources.calendar_small_month;
            this.buttonReplaceHistory.Location = new System.Drawing.Point(2, 197);
            this.buttonReplaceHistory.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.buttonReplaceHistory.Name = "buttonReplaceHistory";
            this.buttonReplaceHistory.Size = new System.Drawing.Size(1, 19);
            this.buttonReplaceHistory.TabIndex = 3;
            this.buttonReplaceHistory.UseVisualStyleBackColor = true;
            this.buttonReplaceHistory.Click += new System.EventHandler(this.buttonReplaceHistory_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 5, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Fin&d:";
            // 
            // buttonFindHistory
            // 
            this.buttonFindHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFindHistory.Image = global::phdesign.NppToolBucket.Properties.Resources.calendar_small_month;
            this.buttonFindHistory.Location = new System.Drawing.Point(2, 8);
            this.buttonFindHistory.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.buttonFindHistory.Name = "buttonFindHistory";
            this.buttonFindHistory.Size = new System.Drawing.Size(1, 19);
            this.buttonFindHistory.TabIndex = 1;
            this.buttonFindHistory.UseVisualStyleBackColor = true;
            this.buttonFindHistory.Click += new System.EventHandler(this.buttonFindHistory_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 388);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 5, 0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Options:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 199);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 5, 0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Re&place:";
            // 
            // textBoxReplace
            // 
            this.textBoxReplace.AcceptsReturn = true;
            this.textBoxReplace.AcceptsTab = true;
            this.textBoxReplace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxReplace.Location = new System.Drawing.Point(3, 197);
            this.textBoxReplace.Multiline = true;
            this.textBoxReplace.Name = "textBoxReplace";
            this.textBoxReplace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxReplace.Size = new System.Drawing.Size(1, 183);
            this.textBoxReplace.TabIndex = 2;
            // 
            // textBoxFind
            // 
            this.textBoxFind.AcceptsReturn = true;
            this.textBoxFind.AcceptsTab = true;
            this.textBoxFind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxFind.Location = new System.Drawing.Point(3, 8);
            this.textBoxFind.Multiline = true;
            this.textBoxFind.Name = "textBoxFind";
            this.textBoxFind.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxFind.Size = new System.Drawing.Size(1, 183);
            this.textBoxFind.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.checkBoxMatchCase, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.checkBoxMatchWholeWord, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.checkBoxSearchFromBegining, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.checkBoxSearchBackwards, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.checkBoxUseRegularExpression, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.lbNotify, 1, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 386);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1, 75);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // checkBoxMatchCase
            // 
            this.checkBoxMatchCase.AutoSize = true;
            this.checkBoxMatchCase.Location = new System.Drawing.Point(3, 3);
            this.checkBoxMatchCase.Name = "checkBoxMatchCase";
            this.checkBoxMatchCase.Size = new System.Drawing.Size(1, 17);
            this.checkBoxMatchCase.TabIndex = 4;
            this.checkBoxMatchCase.Text = "Match ca&se";
            this.checkBoxMatchCase.UseVisualStyleBackColor = true;
            // 
            // checkBoxMatchWholeWord
            // 
            this.checkBoxMatchWholeWord.AutoSize = true;
            this.checkBoxMatchWholeWord.Location = new System.Drawing.Point(3, 26);
            this.checkBoxMatchWholeWord.Name = "checkBoxMatchWholeWord";
            this.checkBoxMatchWholeWord.Size = new System.Drawing.Size(1, 17);
            this.checkBoxMatchWholeWord.TabIndex = 5;
            this.checkBoxMatchWholeWord.Text = "Match &whole word";
            this.checkBoxMatchWholeWord.UseVisualStyleBackColor = true;
            // 
            // checkBoxSearchFromBegining
            // 
            this.checkBoxSearchFromBegining.AutoSize = true;
            this.checkBoxSearchFromBegining.Location = new System.Drawing.Point(3, 3);
            this.checkBoxSearchFromBegining.Name = "checkBoxSearchFromBegining";
            this.checkBoxSearchFromBegining.Size = new System.Drawing.Size(1, 17);
            this.checkBoxSearchFromBegining.TabIndex = 6;
            this.checkBoxSearchFromBegining.Text = "Search from &begining";
            this.checkBoxSearchFromBegining.UseVisualStyleBackColor = true;
            // 
            // checkBoxSearchBackwards
            // 
            this.checkBoxSearchBackwards.AutoSize = true;
            this.checkBoxSearchBackwards.Location = new System.Drawing.Point(3, 26);
            this.checkBoxSearchBackwards.Name = "checkBoxSearchBackwards";
            this.checkBoxSearchBackwards.Size = new System.Drawing.Size(1, 17);
            this.checkBoxSearchBackwards.TabIndex = 7;
            this.checkBoxSearchBackwards.Text = "Search bac&kwards";
            this.checkBoxSearchBackwards.UseVisualStyleBackColor = true;
            this.checkBoxSearchBackwards.CheckedChanged += new System.EventHandler(this.checkBoxSearchBackwards_CheckedChanged);
            // 
            // checkBoxUseRegularExpression
            // 
            this.checkBoxUseRegularExpression.AutoSize = true;
            this.checkBoxUseRegularExpression.Location = new System.Drawing.Point(3, 49);
            this.checkBoxUseRegularExpression.Name = "checkBoxUseRegularExpression";
            this.checkBoxUseRegularExpression.Size = new System.Drawing.Size(1, 17);
            this.checkBoxUseRegularExpression.TabIndex = 5;
            this.checkBoxUseRegularExpression.Text = "Use regular e&xpression";
            this.checkBoxUseRegularExpression.UseVisualStyleBackColor = true;
            this.checkBoxUseRegularExpression.CheckedChanged += new System.EventHandler(this.checkBoxUseRegularExpression_CheckedChanged);
            // 
            // lbNotify
            // 
            this.lbNotify.AutoSize = true;
            this.lbNotify.Location = new System.Drawing.Point(3, 46);
            this.lbNotify.Name = "lbNotify";
            this.lbNotify.Size = new System.Drawing.Size(0, 13);
            this.lbNotify.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 5);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 5, 0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 1);
            this.label4.TabIndex = 6;
            this.label4.Text = "Search in:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.button11);
            this.panel3.Controls.Add(this.button10);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.button9);
            this.panel3.Controls.Add(this.button8);
            this.panel3.Controls.Add(this.button7);
            this.panel3.Controls.Add(this.button6);
            this.panel3.Controls.Add(this.button5);
            this.panel3.Controls.Add(this.button4);
            this.panel3.Controls.Add(this.button3);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.pictureBox2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(11, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(814, 464);
            this.panel3.TabIndex = 3;
            // 
            // button11
            // 
            this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button11.Location = new System.Drawing.Point(213, 95);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(149, 31);
            this.button11.TabIndex = 3;
            this.button11.Text = "---";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.Location = new System.Drawing.Point(213, 58);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(149, 31);
            this.button10.TabIndex = 3;
            this.button10.Text = "Texas SMCAPS";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.comboBoxSearchIn);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 399);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(812, 63);
            this.panel4.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(610, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Version:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(662, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "...";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "E1897 -- Lexis-Nexis";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Developed by Hoàng Hà";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxSearchIn
            // 
            this.comboBoxSearchIn.FormattingEnabled = true;
            this.comboBoxSearchIn.Location = new System.Drawing.Point(388, 12);
            this.comboBoxSearchIn.Name = "comboBoxSearchIn";
            this.comboBoxSearchIn.Size = new System.Drawing.Size(214, 21);
            this.comboBoxSearchIn.TabIndex = 8;
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.Transparent;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.Location = new System.Drawing.Point(213, 22);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(149, 30);
            this.button9.TabIndex = 0;
            this.button9.Text = "Remove Empty Lines";
            this.button9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.Transparent;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.Location = new System.Drawing.Point(22, 274);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(149, 30);
            this.button8.TabIndex = 0;
            this.button8.Text = "Advance Emphasis...";
            this.button8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.Transparent;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.ForeColor = System.Drawing.Color.Red;
            this.button7.Location = new System.Drawing.Point(22, 238);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(149, 30);
            this.button7.TabIndex = 0;
            this.button7.Text = "Valid Emphasis";
            this.button7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Transparent;
            this.button6.Enabled = false;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(22, 202);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(149, 30);
            this.button6.TabIndex = 0;
            this.button6.Text = "Mark Paragraph";
            this.button6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Transparent;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(22, 166);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(149, 30);
            this.button5.TabIndex = 0;
            this.button5.Text = "NVG-VISF Replace";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(22, 130);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(149, 30);
            this.button4.TabIndex = 0;
            this.button4.Text = "Caselaw VISF Replace";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(22, 94);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(149, 30);
            this.button3.TabIndex = 0;
            this.button3.Text = "media=true rsc=(*)";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(22, 58);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(149, 30);
            this.button2.TabIndex = 0;
            this.button2.Text = "rsc=(*) media=true";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(22, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(149, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "Remove SID CL MOD";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::phdesign.NppToolBucket.Properties.Resources.Deadpool_2018;
            this.pictureBox2.Location = new System.Drawing.Point(441, 9);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(375, 396);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // FindAndReplaceForm
            // 
            this.AcceptButton = this.buttonFindNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(836, 480);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FindAndReplaceForm";
            this.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hoàng Hà Plugin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FindAndReplaceForm_FormClosing);
            this.Load += new System.EventHandler(this.FindAndReplaceForm_Load);
            this.Shown += new System.EventHandler(this.FindAndReplaceForm_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonFindNext;
        private System.Windows.Forms.Button buttonFindAll;
        private System.Windows.Forms.Button buttonCount;
        private System.Windows.Forms.Button buttonReplace;
        private System.Windows.Forms.Button buttonReplaceAll;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox textBoxReplace;
        private System.Windows.Forms.TextBox textBoxFind;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.CheckBox checkBoxMatchCase;
        private System.Windows.Forms.CheckBox checkBoxMatchWholeWord;
        private System.Windows.Forms.CheckBox checkBoxSearchFromBegining;
        private System.Windows.Forms.CheckBox checkBoxSearchBackwards;
        private System.Windows.Forms.CheckBox checkBoxUseRegularExpression;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxSearchIn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonReplaceHistory;
        private System.Windows.Forms.Button buttonFindHistory;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label lbNotify;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
    }
}