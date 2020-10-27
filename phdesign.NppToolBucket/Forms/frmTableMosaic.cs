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
    public partial class frmTableMosaic : Form
    {
        public frmTableMosaic()
        {
            InitializeComponent();
        }

        #region Values

        private int MaxColumn;
        public string tbl = "__Empty___";
        private int rownum = 0;


        #endregion



        private void frmTableMosaic_Load(object sender, EventArgs e)
        {
            
            DataTable dt = TexttoDataTable("tableTemp.txt");
            dataGridView1.DataSource = dt;
            // Disable Sort Mode
            dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            //
            // Load

            AddTDtoCell();
            BreakLineMerge();
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

        private void AddTDtoCell()
        {
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                try
                {
                    for (int i = 0; i < MaxColumn; i++)
                    {
                        if (!String.IsNullOrEmpty(dr.Cells[0].Value.ToString()))
                        {

                            dr.Cells[i].Value = String.Format("<td>{1}</td>", i + 1, dr.Cells[i].Value.ToString().Trim());
                        }
                        else
                        {
                            dr.Cells[i].Value = String.Format("<td></td>", i + 1);
                        }
                        dr.Cells[i].Value = dr.Cells[i].Value.ToString();
                    }
                }
                catch
                {

                };
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

        private string clearFormat(string input)
        {
            string output = input;
            output = output.Replace("<b>", "");
            output = output.Replace("<i>", "");
            output = output.Replace("<u>", "");
            output = output.Replace("</b>", "");
            output = output.Replace("</i>", "");
            output = output.Replace("</u>", "");
            return output;
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

        private bool isBlankLine(int MaxColumn, DataGridViewRow Row)
        {
            int K = 0;
            for (int i = 0; i < MaxColumn; i++)
            {
                if (Row.Cells[i].Value.ToString().Contains("<td></td>"))
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button10_Click(object sender, EventArgs e)
        {

            //Export Table
            tbl = textBox1.Text + "<table>\r\n";

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (i < Convert.ToInt32(textBox2.Text))
                {
                    tbl += "<tr>\r\n";
                    {
                        for (int j = 0; j < MaxColumn; j++)
                        {
                            tbl += dataGridView1.Rows[i].Cells[j].Value.ToString().Replace("td>","th>") + "\r\n";
                        }
                    }
                    tbl += "</tr>\r\n";
                }
                else
                {
                    tbl += "<tr>\r\n";
                    {
                        for (int j = 0; j < MaxColumn; j++)
                        {
                            tbl += dataGridView1.Rows[i].Cells[j].Value + "\r\n";
                        }
                    }
                    tbl += "</tr>\r\n";
                }
            }
            tbl += "</table>";

            // Close After All
            this.Close();
        }

        private void BreakLineMerge()
        {
            
            int numRows = dataGridView1.Rows.Count;

            for(int j = numRows; j>0;j--)
            {
                try
                {
                    for (int i = 0; i < MaxColumn; i++)
                    {
                        string cellValue = dataGridView1.Rows[j].Cells[i].Value.ToString();
                        if (j > 0 && cellValue.StartsWith("<td>!!"))
                        {
                            dataGridView1.Rows[j - 1].Cells[i].Value += cellValue;
                            dataGridView1.Rows[j - 1].Cells[i].Value = dataGridView1.Rows[j - 1].Cells[i].Value.ToString().Replace("</td><td>", "");
                            dataGridView1.Rows[j].Cells[i].Value = "<td></td>";
                        }
                    }
                }
                catch
                {

                };
            }
            // Remove Blank Lines
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                if (isBlankLine(MaxColumn, dr))
                {
                    dataGridView1.Rows.RemoveAt(dr.Index);
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                for (int i = 0; i < MaxColumn; i++)
                {
                    if (row.Cells[i].Selected)
                    {
                        string cellValue = row.Cells[i].Value.ToString();
                        cellValue = clearFormat(cellValue);
                        cellValue = Regex.Replace(cellValue, "<td>(.*)</td>", "<td><b>$1</b></td>");
                        cellValue = EmptyCell(cellValue);
                        row.Cells[i].Value = cellValue;
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                for (int i = 0; i < MaxColumn; i++)
                {
                    if (row.Cells[i].Selected)
                    {
                        string cellValue = row.Cells[i].Value.ToString();
                        cellValue = clearFormat(cellValue);
                        cellValue = Regex.Replace(cellValue, "<td>(.*)</td>", "<td><i>$1</i></td>");
                        cellValue = EmptyCell(cellValue);
                        row.Cells[i].Value = cellValue;
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                for (int i = 0; i < MaxColumn; i++)
                {
                    if (row.Cells[i].Selected)
                    {
                        string cellValue = row.Cells[i].Value.ToString();
                        cellValue = clearFormat(cellValue);
                        cellValue = Regex.Replace(cellValue, "<td>(.*)</td>", "<td><u>$1</u></td>");
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
                for (int i = 0; i < MaxColumn; i++)
                {
                    if (row.Cells[i].Selected)
                    {
                        string cellValue = row.Cells[i].Value.ToString();
                        cellValue = clearFormat(cellValue);
                        cellValue = Regex.Replace(cellValue, "<td>(.*)</td>", "<td><b><i>$1</i></b></td>");
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
                for (int i = 0; i < MaxColumn; i++)
                {
                    if (row.Cells[i].Selected)
                    {
                        string cellValue = row.Cells[i].Value.ToString();
                        cellValue = clearFormat(cellValue);
                        cellValue = Regex.Replace(cellValue, "<td>(.*)</td>", "<td><b><i><u>$1<u></i></b></td>");
                        cellValue = EmptyCell(cellValue);
                        row.Cells[i].Value = cellValue;
                    }
                }
            }
        }

        private string EmptyCell(string input)
        {
            string output = input;
            output = Regex.Replace(output, "<td>(<b>|<i>|<u>)+(</b>|</i>|</u>)+</td>", "<td></td>"); // => Remove Empty Fonts
            return output;
        }




    }
}
