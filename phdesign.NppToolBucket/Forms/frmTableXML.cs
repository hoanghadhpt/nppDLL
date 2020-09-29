using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace phdesign.NppToolBucket.Forms
{
    public partial class frmTableXML : Form
    {
        public frmTableXML()
        {
            InitializeComponent();
        }

        #region Values

        private int MaxColumn;
        List<int> ColWith = new List<int>();
        int TableWith = 0;
        public string tbl = "__Empty___";

        

        #endregion

        private void frmTableXML_Load(object sender, EventArgs e)
        {
            // Disable Sort Mode
            dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            //
            DataTable dt = TexttoDataTable("tableTemp.txt");
            dataGridView1.DataSource = dt;
            TrimData();
            _ColWith();
            Insert_OpenTags();
            BodyTable();
        }

        // Func
        public DataTable TexttoDataTable(string inputpath)
        {
            MaxColumn = 0;
            DataTable csvdt = new DataTable();
            string Fulltext;
            if (File.Exists(inputpath))
            {
                using (StreamReader sr = new StreamReader(inputpath))
                {
                    while (!sr.EndOfStream)
                    {
                        Fulltext = sr.ReadToEnd().ToString();//read full content
                        string[] rows = Fulltext.Split('\n');//split file content to get rows

                        for (int col = 0; col < 30; col++)
                        {
                            csvdt.Columns.Add("Column " + col);
                        }

                        for (int i = 0; i < rows.Count(); i++)
                        {
                            DataRow dataRow = csvdt.NewRow();
                            string[] SpitRow = Regex.Split(rows[i], @"\s{2,}");
                            if (SpitRow.Count() > MaxColumn)
                            {
                                MaxColumn = SpitRow.Count();
                            }
                            for (int r = 0; r < SpitRow.Count(); r++)
                            {
                                dataRow[r] = SpitRow[r];

                            }
                            csvdt.Rows.Add(dataRow);
                        }

                    }
                }
            }
            return csvdt;
        }

        private DataTable GetDataTableFromDGV(DataGridView dgv)
        {
            var dt = ((DataTable)dgv.DataSource).Copy();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (!column.Visible)
                {
                    dt.Columns.Remove(column.Name);
                }
            }
            return dt;
        }

        private void _ColWith()
        {


            for (int i = 0; i < MaxColumn; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                List<string> data = new List<string>();
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    if (r.Cells[i].Value == null)
                    {
                        data.Add("");
                    }
                    else
                    {
                        data.Add(r.Cells[i].Value.ToString().Trim());
                    }
                }
                int longestLength = data.Max(w => w.Length);
                ColWith.Add((longestLength + 1)*8);
            }
            TableWith = ColWith.Sum(); // Sum Total Col With
            lbTableWith.Text = (TableWith).ToString();

        }

        private void Insert_OpenTags()
        {
            // Open Table
            textBox1.Text += "<table>\r\n";
            // Total Column
            textBox1.Text += "<tgroup cols=\"" +ColWith.Count.ToString()+"\">\r\n";
            // <colspec ....
            for (int i = 0; i < ColWith.Count; i++)
            {
                textBox1.Text+= String.Format("<colspec colname=\"col{0}\" colwidth=\"{1}\"/>\r\n", i+1, (ColWith[i]).ToString());
            }
            // <thead  -- Heading of table


            // Body of table
            

        }

        private void BodyTable()
        {
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                try
                {
                    for (int i = 0; i < MaxColumn; i++)
                    {
                        if (!String.IsNullOrEmpty(dr.Cells[0].Value.ToString()))
                        {

                            dr.Cells[i].Value = String.Format("<entry align=\"left\" namest=\"col{0}\">{1}</entry>", i+1, dr.Cells[i].Value.ToString().Trim());
                        }
                        else
                        {
                            dr.Cells[i].Value = String.Format("<entry namest=\"col{0}\"/>", i+1);
                        }
                        dr.Cells[i].Value = EmptyCell(dr.Cells[i].Value.ToString());
                    }

                }
                catch
                {

                };
            }
        }

        private void TrimData()
        {
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                try
                {
                    dr.Cells[0].Value = dr.Cells[0].Value.ToString().Trim();
                    dr.Cells[1].Value = dr.Cells[1].Value.ToString().Trim();
                    dr.Cells[2].Value = dr.Cells[2].Value.ToString().Trim();
                    dr.Cells[3].Value = dr.Cells[3].Value.ToString().Trim();
                    dr.Cells[4].Value = dr.Cells[4].Value.ToString().Trim();
                    dr.Cells[5].Value = dr.Cells[5].Value.ToString().Trim();
                    dr.Cells[6].Value = dr.Cells[6].Value.ToString().Trim();
                    dr.Cells[7].Value = dr.Cells[7].Value.ToString().Trim();
                    dr.Cells[8].Value = dr.Cells[8].Value.ToString().Trim();
                    dr.Cells[9].Value = dr.Cells[9].Value.ToString().Trim();
                    dr.Cells[10].Value = dr.Cells[10].Value.ToString().Trim();
                }
                catch { };
            }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }
        
        
        // Left Alignt
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                for (int i = 0; i < ColWith.Count; i++)
                {
                    if (row.Cells[i].Selected)
                    {
                        string cellValue = row.Cells[i].Value.ToString();
                        cellValue = Regex.Replace(cellValue, "align=\"(.*?)\"", "align=\"left\"");
                        row.Cells[i].Value = cellValue;
                    }
                }
            }
        }
        // Center Alignt
        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                for (int i = 0; i < ColWith.Count; i++)
                {
                    if (row.Cells[i].Selected)
                    {
                        string cellValue = row.Cells[i].Value.ToString();
                        cellValue = Regex.Replace(cellValue, "align=\"(.*?)\"", "align=\"center\"");
                        row.Cells[i].Value = cellValue;
                    }
                }
            }
        }
        // Right Alignt
        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                for (int i = 0; i < ColWith.Count; i++)
                {
                    if (row.Cells[i].Selected)
                    {
                        string cellValue = row.Cells[i].Value.ToString();
                        cellValue = Regex.Replace(cellValue, "align=\"(.*?)\"", "align=\"right\"");
                        row.Cells[i].Value = cellValue;
                    }
                }
            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Clear Selection
            dataGridView1.ClearSelection();
            // Select all cell in column
            for (int r = 0; r < dataGridView1.RowCount; r++)
                dataGridView1[e.ColumnIndex, r].Selected = true;
        }
        
        
        // Add font  BOLD
        private void button4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                for (int i = 0; i < ColWith.Count; i++)
                {
                    if (row.Cells[i].Selected)
                    {
                        string cellValue = row.Cells[i].Value.ToString();
                        cellValue = clearFormat(cellValue);
                        cellValue = Regex.Replace(cellValue, "\">(.*)</entry>", "\"><emph typestyle=\"bf\">$1</emph></entry>");
                        cellValue = EmptyCell(cellValue);
                        row.Cells[i].Value = cellValue;
                    }
                }
            }
        }
        // Font Italic
        private void button5_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                for (int i = 0; i < ColWith.Count; i++)
                {
                    if (row.Cells[i].Selected)
                    {
                        string cellValue = row.Cells[i].Value.ToString();
                        cellValue = clearFormat(cellValue);
                        cellValue = Regex.Replace(cellValue, "\">(.*)</entry>", "\"><emph typestyle=\"it\">$1</emph></entry>");
                        cellValue = EmptyCell(cellValue);
                        row.Cells[i].Value = cellValue;
                    }
                }
            }
        }
        // Font Underline
        private void button6_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                for (int i = 0; i < ColWith.Count; i++)
                {
                    if (row.Cells[i].Selected)
                    {
                        string cellValue = row.Cells[i].Value.ToString();
                        cellValue = clearFormat(cellValue);
                        cellValue = Regex.Replace(cellValue, "<entry(.*)>(.*)</entry>", "<entry$1><emph typestyle=\"un\">$2</emph></entry>");
                        cellValue = EmptyCell(cellValue);
                        row.Cells[i].Value = cellValue;
                    }
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                for (int i = 0; i < ColWith.Count; i++)
                {
                    if (row.Cells[i].Selected)
                    {
                        string cellValue = row.Cells[i].Value.ToString();
                        cellValue = clearFormat(cellValue);
                        cellValue = Regex.Replace(cellValue, "\">(.*)</entry>", "\"><emph typestyle=\"ib\">$1</emph></entry>");
                        cellValue = EmptyCell(cellValue);
                        row.Cells[i].Value = cellValue;
                    }
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                for (int i = 0; i < ColWith.Count; i++)
                {
                    if (row.Cells[i].Selected)
                    {
                        string cellValue = row.Cells[i].Value.ToString();
                        cellValue = clearFormat(cellValue);
                        cellValue = Regex.Replace(cellValue, "\">(.*)</entry>", "\"><emph typestyle=\"biu\">$1</emph></entry>");
                        cellValue = EmptyCell(cellValue);
                        row.Cells[i].Value = cellValue;
                    }
                }
            }
        }
        // Clear
        private void button7_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                for (int i = 0; i < ColWith.Count; i++)
                {
                    if (row.Cells[i].Selected)
                    {
                        string cellValue = row.Cells[i].Value.ToString();
                        cellValue = clearFormat(cellValue);
                        row.Cells[i].Value = cellValue;
                    }
                }
            }
        }
        private string clearFormat(string input)
        {
            string output = input;
            output = Regex.Replace(output, "</?emph(.*?)?>","");
            return output;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Remove old header
            string t1 = textBox1.Text;
            t1 = Regex.Replace(t1, "<thead>.*</thead>", "", RegexOptions.Singleline);
            textBox1.Text = t1;
            // =================
            try
            {
                
                textBox1.Text += "<thead>\r\n";


                for (int i = 0; i < Convert.ToInt32(textBox2.Text); i++)
                {
                    textBox1.Text += "<row>\r\n";

                    if (isBlankLine(ColWith.Count, dataGridView1.Rows[i]))
                    {
                        textBox1.Text += "<entry/>\r\n";
                    }
                    else
                    {
                        for (int j = 0; j < ColWith.Count; j++)
                        {
                            textBox1.Text += dataGridView1.Rows[i].Cells[j].Value + "\r\n";
                        }
                    }
                    textBox1.Text += "</row>\r\n";
                }
                textBox1.Text += "</thead>\r\n";
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error~!!");
            }
        }

        private bool isBlankLine(int MaxColumn, DataGridViewRow Row)
        {
            int K = 0;
            for (int i = 0; i < MaxColumn; i++)
            {
                if (Row.Cells[i].Value.ToString().Contains("/>"))
                {
                    K++;
                }
            }
            if (K == MaxColumn)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ExportTable()
        {

        }
        private void button10_Click(object sender, EventArgs e)
        {
            //Export Table
            tbl = textBox1.Text + "<tbody>\r\n";

            for (int i = Convert.ToInt32(textBox2.Text); i < dataGridView1.Rows.Count; i++)
            {
                tbl += "<row>\r\n";
                if (isBlankLine(ColWith.Count, dataGridView1.Rows[i]))
                {
                    tbl += "<entry/>\r\n";
                }
                else
                {
                    for (int j = 0; j < ColWith.Count; j++)
                    {
                        tbl += dataGridView1.Rows[i].Cells[j].Value + "\r\n";
                    }
                }
                tbl += "</row>\r\n";
            }
            tbl += "</tbody>\r\n</tgroup>\r\n</table>";

            // Close After All
            this.Close();
        }

        private string EmptyCell(string input)
        {
            string output = input;
            output = Regex.Replace(output, "<emph typestyle=\".*?\"></emph>", ""); // => Remove Empty Fonts
            if (output.Contains("\"></entry>"))
            {
                output = Regex.Replace(output, @"<entry.*?col(\d+).*", "<entry namest=\"col$1\"/>");
            }
            return output;
        }

        // Shortcut
        private void frmTableXML_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                button10.PerformClick();
            }
        }
    }
}
