using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace phdesign.NppToolBucket
{
    class xZero
    {
        #region Process
        private static string court;
        private static string pattern = @"<lnv:COURT>(.*?)<\/lnv:COURT>";
        private static string pattern2 = @"(.*?),";
        private static string smi;
        private static string dspi;
        private static string lnfile;
        private static string meta;
        private static string status;
        private static string pub = "NO";
        private static string CsvPath = Environment.CurrentDirectory + "\\COURTDATABASE.csv";

        #endregion

        private static void resetData()
        {
            court = "";
            smi = "";
            dspi = "";
            lnfile = "";
            meta = "";
        }
        private static string replaceHyphenNumber(string text)
        {
            
            string numpartent = @"<lnvxe:docketnumber(.*?)<\/lnvxe:docketnumber>";
            string number;
            string number2;
            foreach (Match m in Regex.Matches(text, numpartent))
            {
                
                number = m.Value.ToString();
                number2 = number.Replace("&#x2D;", "-");
                text = text.Replace(number, number2);
                
            }
            return text;
        }

        private static string pacernumber(string text)
        {
            string pacer = "";
            string numpartent = @"(\d{1}):(\d{2})-(\w{2})-(\d{1,16})";
            Regex Reg = new Regex(numpartent);
            var m = Reg.Match(text);
            if (m.Success)
            {
                string lastPart = m.Groups[4].ToString();
                for (int i = 0; i < 6; i++)
                {
                    if (lastPart.Substring(0, 1) == "0")
                    {
                        lastPart = m.Groups[4].ToString().Remove(0, 1);
                    }
                }
                pacer = m.Groups[1].ToString() + ":" + m.Groups[2].ToString() + m.Groups[3].ToString().ToLower() + lastPart;
            }
            else
            {
                string numpartent2 = @"(\d{1}):(\d{2})(\w{2})(\d{1,16})";
                Regex Reg2 = new Regex(numpartent2);
                var m2 = Reg.Match(text);
                if (m2.Success)
                {
                    string lastPart = m2.Groups[4].ToString();
                    for (int i = 0; i < 6; i++)
                    {
                        if (lastPart.Substring(0, 1) == "0")
                        {
                            lastPart = m2.Groups[4].ToString().Remove(0, 1);
                        }
                    }
                    pacer = m2.Groups[1].ToString() + ":" + m2.Groups[2].ToString() + m2.Groups[3].ToString().ToLower() + lastPart;
                }
            }
            return pacer;
        }

        private static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            StreamReader sr = new StreamReader(strFilePath);
            string[] headers = sr.ReadLine().Split(',');
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }


        private static void QueryCourt()

        {
            DataTable dataDatale1 = ConvertCSVtoDataTable(CsvPath);
            DataRow[] rows = dataDatale1.Select("CNAME LIKE '" + court + "'");
            //COURT OF APPEALS OF WASHINGTON_DIVISION ONE
            foreach (DataRow row in rows)
            {
                
                smi = row[1].ToString();
                dspi = row[2].ToString();
                lnfile = row[3].ToString();
                meta = row[4].ToString();
            }
        }

        private static void extractCourt(string input)
        {
            foreach (Match m in Regex.Matches(input, pattern))
            {
                court = m.Groups[1].ToString();
                court = court.Replace("'", "s");
                
                //PreProcess(court);
                if (court.Contains("UNITED STATES DISTRICT COURT") || court.Contains("UNITED STATES BANKRUPTCY"))
                {
                    PreProcess(court);
                    CourtProcess(court);
                }
                else court = court.Replace(", ", "|").Trim();
                
                //=================================
            }
        }
        private static void PreProcess(string str)
        {
            foreach (Match m in Regex.Matches(str, pattern2))
            {
                court = m.Groups[1].ToString();
            }
        }
        private static void CourtProcess(string court2)
        {
            court2 = court;
            court2 = court2.Replace(" MIDDLE ", "|");
            court2 = court2.Replace(" EASTERN ", "|");
            court2 = court2.Replace(" CENTER ", "|");
            court2 = court2.Replace(" WESTERN ", "|");
            court2 = court2.Replace(" SOUTHERN ", "|");
            court2 = court2.Replace(" NORTHERN ", "|");
            court2 = court2.Replace(" CENTRAL ", "|");
            court2 = court2.Replace("TENNESSEE--SPECIAL WORKERS", "TENNESSEE--SPECIAL WORKERS");
            court2 = court2.Replace("FOR THE DISTRICT", "FOR THE|DISTRICT");
            court = court2;
        }

        // process
        public static string insertCourt(string input)
        {
            resetData();
            string output = input;

            try
            {
                
                extractCourt(output);
                
                QueryCourt();


                // Repace &#x2D; to '-'
                output = replaceHyphenNumber(output);

                output = output.Replace(@"lnsmi=""""", @"lnsmi=""" + smi + @"""");
                output = output.Replace(@"lndpsi=""""", @"lndpsi=""" + dspi + @"""");
                output = output.Replace(@"lnfilenum=""""", @"lnfilenum=""" + lnfile + @"""");
                output = output.Replace(@"value=""""", @"value=""" + meta + @"""");
                
                return output;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return output;
            };
            
        }
    }
}
