using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using phdesign.NppToolBucket.PluginCore;
using phdesign.NppToolBucket.Utilities.Security;

namespace phdesign.NppToolBucket.Forms
{
    public partial class PaginationXML : Form
    {
        public PaginationXML()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var editor = Editor.GetActive();
            var text = editor.GetDocumentText();

            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Empty Document");
                return;
            }
            else
            {
                if (txtRSC.Text == "")
                {
                    MessageBox.Show("Input RSC then continue.");
                }
                else
                {
                    editor.SetDocumentText(rsc_Process(text));
                }
            }
        }

        private string rsc_Process(string text)
        {
            string tmp = text;
            switch (comboBox1.SelectedIndex.ToString())
            {
                case "0":
                    if (checkBox1.Checked)
                    {
                        tmp = rscReplaceMediaTrueEND(text, @"\[\*([\d]+)\]");
                    }
                    if (checkBox2.Checked)
                    {
                        tmp = rscReplaceMediaTrueMIDDLE(text, @"\[\*([\d]+)\]");
                    }
                    if (checkBox3.Checked)
                    {
                        tmp = rscReplaceNoMediaTrue(text, @"\[\*([\d]+)\]");
                    }
                    //rsc_replace(@"\[\*([\d]+)\]");
                    MessageBox.Show("Done!");
                    break;
                case "1":
                    if (checkBox1.Checked)
                    {
                        tmp = rscReplaceMediaTrueEND_P(text, @"\[P(\d+)\]");
                    }
                    if (checkBox2.Checked)
                    {
                        tmp = rscReplaceMediaTrueMIDDLE_P(text, @"\[P(\d+)\]");
                    }
                    if (checkBox3.Checked)
                    {
                        tmp = rscReplaceNoMediaTrue_P(text, @"\[P(\d+)\]");
                    }
                    //rsc_replace(@"\[\\P([\d]+)\]");
                    MessageBox.Show("Done!");
                    break;
                case "2":
                    if (checkBox1.Checked)
                    {
                        tmp = rscReplaceMediaTrueEND_P(text, @"\[\\P(\d+)\]");
                    }
                    if (checkBox2.Checked)
                    {
                        tmp = rscReplaceMediaTrueMIDDLE_P(text, @"\[\\P(\d+)\]");
                    }
                    if (checkBox3.Checked)
                    {
                        tmp = rscReplaceNoMediaTrue_P(text, @"\[\\P(\d+)\]");
                    }
                    // 
                    MessageBox.Show("Done!");
                    break;
                case "3":
                    if (checkBox1.Checked)
                    {
                        tmp = rscReplaceMediaTrueEND_P(text, @"\[¶(\d+)\]");
                    }
                    if (checkBox2.Checked)
                    {
                        tmp = rscReplaceMediaTrueMIDDLE_P(text, @"\[¶(\d+)\]");
                    }
                    if (checkBox3.Checked)
                    {
                        tmp = rscReplaceNoMediaTrue_P(text, @"\[¶(\d+)\]");
                    }
                    break;
                case "-1":
                    MessageBox.Show("Select type of Pagination", "Not Yet....");
                    break;
            }
            return tmp;
        }
        private string rscReplaceMediaTrueEND(string text, string pattern)
        {
            string tmp = Regex.Replace(text, pattern, @"<page count=""" + @"$1"" rsc=""" + txtRSC.Text + @""" medianeutralrsc=""true""/>");
            return tmp;
        }
        private string rscReplaceMediaTrueMIDDLE(string text, string pattern)
        {
            string tmp = Regex.Replace(text, pattern, @"<page count=""" + @"$1"" " + @"medianeutralrsc=""true"" rsc=""" + txtRSC.Text + @"""/>");
            return tmp;
        }
        private string rscReplaceNoMediaTrue(string text, string pattern)
        {
            string tmp = Regex.Replace(text, pattern, @"<page count=""" + @"$1"" rsc=""" + txtRSC.Text + @"""/>");
            return tmp;
        }

        private string rscReplaceMediaTrueEND_P(string text, string pattern)
        {
            string tmp = Regex.Replace(text, pattern, @"<page count=""" + @"P$1"" rsc=""" + txtRSC.Text + @""" medianeutralrsc=""true""/>");
            return tmp;
        }
        private string rscReplaceMediaTrueMIDDLE_P(string text, string pattern)
        {
            string tmp = Regex.Replace(text, pattern, @"<page count=""" + @"P$1"" " + @"medianeutralrsc=""true"" rsc=""" + txtRSC.Text + @"""/>");
            return tmp;
        }
        private string rscReplaceNoMediaTrue_P(string text, string pattern)
        {
            string tmp = Regex.Replace(text, pattern, @"<page count=""" + @"P$1"" rsc=""" + txtRSC.Text + @"""/>");
            return tmp;
        }

        private void txtRSC_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtRSC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
