using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace phdesign.NppToolBucket
{
    class classD1712
    {
        
        public string D1712FootnoteProcess(string input)
        {
            string output = input;
            output = RemoveLink(output);
            output = ProcessFootnoteIndicator(output);
            output = ProcessFootnoteBody(output);
            return output;
        }

        private string ProcessFootnoteIndicator(string input)
        {
            string output = input;
            string temp = "";
            var result = "";
            string Cite = extractCITE(input);
            result = Regex.Replace(output, @" n(\d+)", (Match n) =>
            {
                string indicator = n.Groups[1].Value.ToString();
                int Count = countOccurences(temp, "n"+ indicator + " ");
                //System.Windows.Forms.MessageBox.Show("Indicator: " + indicator + "\r\nOccurences: " + Count + "\r\nString: " + temp);
                if (Count == 0)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1});.vk $?n{1}$=>",Cite ,indicator);
                }
                else if (Count == 1)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}a);.vk $?n{1}a$=>", Cite, indicator);
                }
                else if (Count == 2)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}b);.vk $?n{1}b$=>", Cite, indicator);
                }
                else if (Count == 3)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}c);.vk $?n{1}c$=>", Cite, indicator);
                }
                else if (Count == 4)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}d);.vk $?n{1}d$=>", Cite, indicator);
                }
                else if (Count == 5)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}e);.vk $?n{1}e$=>", Cite, indicator);
                }
                else if (Count == 6)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}f);.vk $?n{1}f$=>", Cite, indicator);
                }
                else if (Count == 7)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}g);.vk $?n{1}g$=>", Cite, indicator);
                }
                else if (Count == 8)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}h);.vk $?n{1}h$=>", Cite, indicator);
                }
                else if (Count == 9)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}i);.vk $?n{1}i$=>", Cite, indicator);
                }
                else if (Count == 10)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}j);.vk $?n{1}j$=>", Cite, indicator);
                }
                else if (Count == 11)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}k);.vk $?n{1}k$=>", Cite, indicator);
                }
                else if (Count == 12)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}l);.vk $?n{1}l$=>", Cite, indicator);
                }
                else if (Count == 13)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}m);.vk $?n{1}m$=>", Cite, indicator);
                }
                else if (Count == 14)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}n);.vk $?n{1}n$=>", Cite, indicator);
                }
                else if (Count == 15)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}o);.vk $?n{1}o$=>", Cite, indicator);
                }
                else if (Count == 16)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}p);.vk $?n{1}p$=>", Cite, indicator);
                }
                else if (Count == 17)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}q);.vk $?n{1}q$=>", Cite, indicator);
                }
                else if (Count == 18)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}r);.vk $?n{1}r$=>", Cite, indicator);
                }
                else if (Count == 19)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}s);.vk $?n{1}s$=>", Cite, indicator);
                }
                else if (Count == 20)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}t);.vk $?n{1}t$=>", Cite, indicator);
                }
                else if (Count == 21)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}u);.vk $?n{1}u$=>", Cite, indicator);
                }
                else if (Count == 22)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}v);.vk $?n{1}v$=>", Cite, indicator);
                }
                else if (Count == 24)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}w);.vk $?n{1}w$=>", Cite, indicator);
                }
                else if (Count == 25)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}x);.vk $?n{1}x$=>", Cite, indicator);
                }
                else if (Count == 26)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}y);.vk $?n{1}y$=>", Cite, indicator);
                }
                else if (Count == 29)
                {
                    result = string.Format(" $=<$=T1*.fo_number(perb_and_{0})_and_footnotes(n{1}z);.vk $?n{1}z$=>", Cite, indicator);
                }

                temp += n.Value.ToString() + " ";
                return result;
            });
            return result;
        }

        private string ProcessFootnoteBody(string input)
        {
            string output = input;
            string temp = "";
            var result = "";
            string Cite = extractCITE(input);
            result = Regex.Replace(output, @"\$F\$Tn(\d+)", (Match n) =>
            {
                string indicator = n.Groups[1].Value.ToString();
                int Count = countOccurences(temp, "n"+indicator+" ");
                if (Count == 0)
                {
                    result = string.Format("$F$Tn{1}", Cite, indicator);
                }
                else if (Count == 1)
                {
                    result = string.Format("$F$Tn{1}a", Cite, indicator);
                }
                else if (Count == 2)
                {
                    result = string.Format("$F$Tn{1}b", Cite, indicator);
                }
                else if (Count == 3)
                {
                    result = string.Format("$F$Tn{1}c", Cite, indicator);
                }
                else if (Count == 4)
                {
                    result = string.Format("$F$Tn{1}d", Cite, indicator);
                }
                else if (Count == 5)
                {
                    result = string.Format("$F$Tn{1}e", Cite, indicator);
                }
                else if (Count == 6)
                {
                    result = string.Format("$F$Tn{1}f", Cite, indicator);
                }
                else if (Count == 7)
                {
                    result = string.Format("$F$Tn{1}g", Cite, indicator);
                }
                else if (Count == 8)
                {
                    result = string.Format("$F$Tn{1}h", Cite, indicator);
                }
                else if (Count == 9)
                {
                    result = string.Format("$F$Tn{1}i", Cite, indicator);
                }
                else if (Count == 10)
                {
                    result = string.Format("$F$Tn{1}j", Cite, indicator);
                }
                else if (Count == 11)
                {
                    result = string.Format("$F$Tn{1}k", Cite, indicator);
                }
                else if (Count == 12)
                {
                    result = string.Format("$F$Tn{1}l", Cite, indicator);
                }
                else if (Count == 13)
                {
                    result = string.Format("$F$Tn{1}m", Cite, indicator);
                }
                else if (Count == 14)
                {
                    result = string.Format("$F$Tn{1}n", Cite, indicator);
                }
                else if (Count == 15)
                {
                    result = string.Format("$F$Tn{1}o", Cite, indicator);
                }
                else if (Count == 16)
                {
                    result = string.Format("$F$Tn{1}p", Cite, indicator);
                }
                else if (Count == 17)
                {
                    result = string.Format("$F$Tn{1}q", Cite, indicator);
                }
                else if (Count == 18)
                {
                    result = string.Format("$F$Tn{1}r", Cite, indicator);
                }
                else if (Count == 19)
                {
                    result = string.Format("$F$Tn{1}s", Cite, indicator);
                }
                else if (Count == 20)
                {
                    result = string.Format("$F$Tn{1}t", Cite, indicator);
                }
                else if (Count == 21)
                {
                    result = string.Format("$F$Tn{1}u", Cite, indicator);
                }
                else if (Count == 22)
                {
                    result = string.Format("$F$Tn{1}v", Cite, indicator);
                }
                else if (Count == 24)
                {
                    result = string.Format("$F$Tn{1}w", Cite, indicator);
                }
                else if (Count == 25)
                {
                    result = string.Format("$F$Tn{1}x", Cite, indicator);
                }
                else if (Count == 26)
                {
                    result = string.Format("$F$Tn{1}y", Cite, indicator);
                }
                else if (Count == 29)
                {
                    result = string.Format("$F$Tn{1}z", Cite, indicator);
                }

                temp += n.Value.ToString() + " ";
                return result;
            });
            return result;
        }

        private int countOccurences(string input, string pattern)
        {
            int i = Regex.Matches(input, pattern).Count;
            return i;
        }

        private string RemoveLink(string input)
        {
            string output = input;

            output = Regex.Replace(output, @"\$=<.*\(n(\d+).*\$=>", " n$1");
            output = Regex.Replace(output, @"\$F\$Tn(\d+)[a-z] ", "$F$Tn$1 ");
            return output;
        }

        private string extractCITE(string input)
        {
            Match m = Regex.Match(input, @"\$30:(.*) (\d+)(.*)");
            if (m.Success)
            {
                string Cite = m.Groups[2].Value.ToString() + m.Groups[3].Value.ToString();
                Cite = Cite.Replace(" ", "_");
                    Cite = Cite.Replace("\r\n", "");
                Cite = Cite.Replace("\r", "");
                Cite = Cite.Replace("\n", "");
                return Cite;
            }
            else
            {
                return "@CITE@";
            }
            
        }
    }
}
