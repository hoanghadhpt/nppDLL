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
using System.Linq;

namespace phdesign.NppToolBucket.Forms
{
    public partial class frmShortcut : Form
    {
        public frmShortcut()
        {
            InitializeComponent();
            
        }

        private void Shortcut_Load(object sender, EventArgs e)
        {
            var editor = Editor.GetActive();
            int currentLanguage = editor.GetCurrentLanguage();
            if (currentLanguage == 15)
            {
                label1.Text = "Language: OUT/VISF";
                ReadShortcutVISF();
            }
            if (currentLanguage == 9)
            {
                label1.Text = "Language: XML";
                ReadShortcutXML();
            }
            if (currentLanguage == 8)
            {
                label1.Text = "Language: HTML";
            }
        }

        private void frmShortcut_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                frmShortcut.ActiveForm.Close();
            }
        }
        

        private void AddRows(string Shortcut, string StartTag, string EndTag)
        {
            dataGridView1.Rows.Add(Shortcut, StartTag,EndTag);
        }

        DataTable dt = new DataTable();
        private void ReadShortcutVISF()
        {
            dt.Columns.Add("Shortcut", typeof(string));
            dt.Columns.Add("BeginTag", typeof(string));
            dt.Columns.Add("EndTag", typeof(string));
            var lines = File.ReadAllLines(Environment.CurrentDirectory + "\\plugins\\hoanghaShortcut.txt");
            if (lines.Count() > 0)
            {
                foreach (string line in lines)
                {
                    string[] splitLine = line.Split('|');
                    //dataGridView1.Rows.Add(splitLine[0], splitLine[1], splitLine[2]);
                    dt.Rows.Add(new object[] { splitLine[0], splitLine[1], splitLine[2] });
                }
                dataGridView1.DataSource = dt;
                dataGridView1.Sort(this.dataGridView1.Columns["Shortcut"], ListSortDirection.Ascending);
            }
        }

        private void ReadShortcutXML()
        {
            dt.Columns.Add("Shortcut", typeof(string));
            dt.Columns.Add("BeginTag", typeof(string));
            dt.Columns.Add("EndTag", typeof(string));
            var lines = File.ReadAllLines(Environment.CurrentDirectory + "\\plugins\\hoanghaShortcutXML.txt");
            if (lines.Count() > 0)
            {
                foreach (string line in lines)
                {
                    string[] splitLine = line.Split('|');
                    //dataGridView1.Rows.Add(splitLine[0], splitLine[1], splitLine[2]);
                    dt.Rows.Add(new object[] { splitLine[0], splitLine[1], splitLine[2] });
                }
                dataGridView1.DataSource = dt;
                dataGridView1.Sort(this.dataGridView1.Columns["Shortcut"], ListSortDirection.Ascending);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (textBox1.Text.Trim() == "110")
                {
                    var editor = Editor.GetActive();
                    var selectedText = editor.GetSelectedText();
                    var selPos = editor.GetSelectionRange();

                    selectedText = selectedText.Replace("\r\n", ", ");
                    selectedText = selectedText.Replace("/s/ ", "");
                    selectedText = selectedText.Replace(", ,", ",");
                    selectedText = selectedText.Replace("..", ".");

                    selectedText = RemoveFonts(selectedText);

                    selectedText = "$110:" + selectedText + ".\r\n$115:";
                    editor.SetSelectedText(selectedText);
                    editor.SetSelection(selPos.cpMin, selPos.cpMin + selectedText.Length);

                }
                else
                {// Add Tags
                    var editor = Editor.GetActive();
                    var selectedText = editor.GetSelectedText();
                    var selPos = editor.GetSelectionRange();

                    string forReturnText = "";


                    if (GetBeginTag().Contains("@@") || GetEndTag().Contains("@@"))
                    {
                        forReturnText = tagsReplace(GetBeginTag(), selectedText) + tagsReplace(GetEndTag(), selectedText);
                    }
                    else
                    {
                        forReturnText = GetBeginTag() + selectedText + GetEndTag();
                    }
                    forReturnText = forReturnText.Replace("\\r\\n", "\r\n");
                    editor.SetSelectedText(forReturnText);
                    editor.SetSelection(selPos.cpMin, selPos.cpMin + forReturnText.Length);
                    this.Hide();
                    //
                }
            }
        }
        private string tagsReplace(string inputTag, string selectedText)
        {
            string output = inputTag;
            output = output.Replace("@@", selectedText);
            return output;
        }

        private string GetBeginTag()
        {
            string beginTag = "";
            try
            {
                beginTag = dataGridView1.Rows[0].Cells[1].Value.ToString();
            }
            catch { }
            return beginTag;
        }

        private string GetEndTag()
        {
            string EndTag = "";
            try
            {
                EndTag = dataGridView1.Rows[0].Cells[2].Value.ToString();
            }
            catch { }
            return EndTag;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dt.DefaultView.RowFilter = string.Format("Shortcut LIKE '{0}%'", textBox1.Text);

                //DataTable dt = new DataTable();
                //dt = GetDataTableFromDGV(dataGridView1);
                //dt.DefaultView.RowFilter = string.Format("Shortcut LIKE '%{0}%'", textBox1.Text.Trim());
            }
            catch
            { };
        }

        private DataTable GetDataTableFromDGV(DataGridView dgv)
        {
            var dt = new DataTable();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column.Visible)
                {
                    // You could potentially name the column based on the DGV column name (beware of dupes)
                    // or assign a type based on the data type of the data bound to this DGV column.
                    dt.Columns.Add();
                }
            }

            object[] cellValues = new object[dgv.Columns.Count];
            foreach (DataGridViewRow row in dgv.Rows)
            {
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    cellValues[i] = row.Cells[i].Value;
                }
                dt.Rows.Add(cellValues);
            }

            return dt;
        }

        private string RemoveFonts(string textInput)
        {
            string text = textInput;

            text = text.Replace("$=B", "");
            text = text.Replace("$=R", "");
            text = text.Replace("$U", "");
            text = text.Replace("$O", "");
            text = text.Replace("$I", "");
            text = text.Replace("$N", "");
            text = text.Replace("$W", "");
            text = text.Replace("$K", "");
            text = text.Replace("$=S", "");
            text = text.Replace("$=I", "");
            text = text.Replace("$=H", "");
            text = text.Replace("$=E", "");
            text = text.Replace("$T", "");
            text = text.Replace("$%", "");

            return text;
        }
    }
}
