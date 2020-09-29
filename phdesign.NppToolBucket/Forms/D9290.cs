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
using System.Data;
using Microsoft.VisualBasic.FileIO;
using System.Data.OleDb;

namespace phdesign.NppToolBucket.Forms
{
    public partial class D9290 : Form
    {
        public D9290()
        {
            InitializeComponent();
        }

        private void D9290_Load(object sender, EventArgs e)
        {
            BindData("9290-Citelist.csv");
            dataGridView1.Sort(dataGridView1.Columns["DBFile"], System.ComponentModel.ListSortDirection.Ascending);
        }

        private void BindData(string filePath)
        {
            DataTable dt = new DataTable();
            string[] lines = System.IO.File.ReadAllLines(filePath);
            if (lines.Length > 0)
            {
                //first line to create header
                string firstLine = lines[0];
                string[] headerLabels = firstLine.Split(',');
                foreach (string headerWord in headerLabels)
                {
                    dt.Columns.Add(new DataColumn(headerWord));
                }
                //For Data
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] dataWords = lines[i].Split(',');
                    DataRow dr = dt.NewRow();
                    int columnIndex = 0;
                    foreach (string headerWord in headerLabels)
                    {
                        dr[headerWord] = dataWords[columnIndex++];
                    }
                    dt.Rows.Add(dr);
                }
            }
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
            }

        }


        private void txtDB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("DBFile LIKE '%{0}%'", txtDB.Text.Trim());
            }
            catch { }
        }

        private void txtDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ProcessDate();
            }
            catch { }
        }

        private void ProcessDate()
        {
            string tmp = "";
            txtSegments.Clear();
            //==================================================//
            // SESSION DATE
            Match mSession = Regex.Match(txtDate.Text.ToLower(), @"(spring|winter|summer|fall)(.*)(\d{4})");
            if (mSession.Success)
            {
                if (mSession.Groups[1].Value.ToLower() == "spring")
                {
                    tmp = string.Format("$21:April {0}\r\n$22:May {0}\r\n$23:June {0}\r\n$27:Spring, {0}\r\n", mSession.Groups[3].Value);
                }
                else if (mSession.Groups[1].Value.ToLower() == "summer")
                {
                    tmp = string.Format("$21:July {0}\r\n$22:August {0}\r\n$23:September {0}\r\n$27:Summer, {0}\r\n", mSession.Groups[3].Value);
                }
                else if (mSession.Groups[1].Value.ToLower() == "fall")
                {
                    tmp = string.Format("$21:October {0}\r\n$22:November {0}\r\n$23:December {0}\r\n$27:Fall, {0}\r\n", mSession.Groups[3].Value);
                }
                else if (mSession.Groups[1].Value.ToLower() == "winter")
                {
                    tmp = string.Format("$21:January {0}\r\n$22:February {0}\r\n$23:March {0}\r\n$27:Winter, {0}\r\n", mSession.Groups[3].Value);
                }
                txtSegments.Text += tmp;
            }
            // ONLY YEAR
            if (txtDate.Text.Length == 4 && IsNumeric(txtDate.Text))
            {
                tmp = string.Format("$02:January {0}\r\n$03:February {0}\r\n$04:March {0}\r\n$05:April {0}\r\n$06:May {0}\r\n$07:June {0}\r\n$08:July {0}\r\n$09:August {0}\r\n$10:September {0}\r\n$11:October {0}\r\n$12:November {0}\r\n$13:December {0}\r\n$27:{0}\r\n", txtDate.Text);
                txtSegments.Text += tmp;
            }
            Match mTwoYears = Regex.Match(txtDate.Text.ToLower(), @"(\d{4})(.*)(\d{4})");
            if (mTwoYears.Success)
            {
                tmp = string.Format("$02:January {0}\r\n$03:February {0}\r\n$04:March {0}\r\n$05:April {0}\r\n$06:May {0}\r\n$07:June {0}\r\n$08:July {0}\r\n$09:August {0}\r\n$10:September {0}\r\n$11:October {0}\r\n$12:November {0}\r\n$13:December {0}\r\n$14:January {1}\r\n$15:February {1}\r\n$16:March {1}\r\n$17:April {1}\r\n$18:May {1}\r\n$19:June {1}\r\n$20:July {1}\r\n$21:August {1}\r\n$22:September {1}\r\n$23:October {1}\r\n$25:November {1}\r\n$26:December {1}\r\n$27:{0} / {1}\r\n",mTwoYears.Groups[1].Value, mTwoYears.Groups[3].Value);
                txtSegments.Text += tmp;
            }


        }
        // (?:Jan(?:uary)?|Feb(?:ruary)?|Mar(?:ch)?|Apr(?:il)?|May|Jun(?:e)?|Jul(?:y)?|Aug(?:ust)?|Sep(?:tember)?|Oct(?:ober)?|(Nov|Dec)(?:ember)?) (\d{4})
        public static bool IsNumeric(object Expression)
        {
            double retNum;

            bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        private void txtSegments_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                txtSegments.SelectAll();
            }
        }
    }
}
