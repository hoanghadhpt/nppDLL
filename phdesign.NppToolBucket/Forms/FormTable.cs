using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using phdesign.NppToolBucket.PluginCore;
using phdesign.NppToolBucket.Utilities.Security;
using phdesign.NppToolBucket.Forms;

namespace phdesign.NppToolBucket
{
    public partial class frmTable : Form
    {
        public frmTable()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Process();
        }

        private int MaxColumn;
        List<int> ColWith = new List<int>();
        int TableWith = 0;

        private void Process()
        {
            DataTable dt = TexttoDataTable("tableTemp.txt");
            dataGridView1.DataSource = dt;
            TrimData();
            _ColWith();
            Insert_LH();
            //MessageBox.Show(MaxColumn.ToString());
        }

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
                        string[] rows = Fulltext.Split('\n');//split file content to get the rows

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
                ColWith.Add(longestLength+1);
            }
            TableWith = ColWith.Sum(); // Sum Total Col With
        }

        private void Insert_LH()
        {
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                try
                {
                    for (int i = 0; i < MaxColumn; i++)
                    {
                        if (!String.IsNullOrEmpty(dr.Cells[0].Value.ToString()))
                        {
                            if (i > 0)
                            {
                                dr.Cells[i].Value = "$H" + dr.Cells[i].Value.ToString().Trim();
                            }
                            else
                            {
                                dr.Cells[i].Value = "$L" + dr.Cells[i].Value.ToString().Trim();
                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                dr.Cells[i].Value = "$L";
                            }
                        }
                    }
                    
                }
                catch
                {

                };
            }
            textBox1.Text = "$M" + String.Format("{00:00},",ColWith.Count);
            for (int i = 0; i < ColWith.Count; i++)
            {
                textBox1.Text += String.Format("{00:00},", ColWith[i]);
            }
            textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1) + "$D";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TableWith > 78)
            {
                MessageBox.Show("Table Exceed 640 pixels","Warning");
            }
            ExportTable();
            this.Close();
        }

        public string tbl;
        private void ExportTable()
        {
            tbl = textBox1.Text +"\r\n";

            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                try
                {
                    for (int i = 0; i < MaxColumn; i++)
                    {
                        if (i < MaxColumn - 1)
                        {
                            tbl += dr.Cells[i].Value.ToString().Trim();
                        }
                        else
                        {
                            tbl += dr.Cells[i].Value.ToString().Trim() + "\r\n";
                        }
                        tbl = tbl.Replace("$H\r\n", "\r\n");
                    }
                    
                }
                catch
                {

                };
            }

            tbl = tbl + "$X";
            tbl = tbl.Replace("$H\r\n$X", "$X");
        }
    }
}
