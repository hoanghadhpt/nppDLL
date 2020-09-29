using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace phdesign.NppToolBucket.Forms
{
    public partial class frmFootnoteXML : Form
    {
        public frmFootnoteXML()
        {
            InitializeComponent();
        }
        #region  Public Region
        public string InputText = "";

        #endregion


        private void button1_Click(object sender, EventArgs e)
        {
            int footnote = Convert.ToInt32(txtFootnote.Text);
            int token = Convert.ToInt32(txtToken.Text);
            InputText = ReNumber(InputText, footnote, token);
            this.Close();
        }

        private void Process_All(string text)
        {

        }

        private string ReNumber(string text, int footnote, int token)
        {
            //
            string pattent_body1 = "<lnvxe:footnote fnrtokens=\"ref[0-9]{1,4}\" fntoken=\"fnote[0-9]{1,4}\">";
            string pattent_body2 = @"<lnvxe:fnlabel alt-content=""(\*\*\*|\*\*|\*|n[0-9]{0,4})"">(\*\*\*|\*\*|\*|[0-9]{0,4})<\/lnvxe:fnlabel>";
            string pattent_indicator = "<lnvxe:fnr alt-content=\"(\\*\\*\\*|\\*\\*|\\*|n[0-9]{0,4})\" fnrtoken=\"ref[0-9]{0,4}\" fntoken=\"fnote[0-9]{0,4}\">(\\*\\*\\*|\\*\\*|\\*|[0-9]{0,4})</lnvxe:fnr>";

            int footnote1 = footnote - 1;
            int token1 = token - 1;

            string result = Regex.Replace(text, pattent_body1, (Match n) =>
            {
                result = string.Format("<lnvxe:footnote fnrtokens=\"ref{0}\" fntoken=\"fnote{0}\">", ++token1);
                return (result);
            });

            footnote1 = footnote - 1;
            token1 = token - 1;

            string result2 = Regex.Replace(result, pattent_body2, (Match n) =>
            {
                if (n.Value.Contains(">***<"))
                {
                    result = string.Format("<lnvxe:fnlabel alt-content=\"***\">***</lnvxe:fnlabel>");
                    return (result);
                }
                else if (n.Value.Contains(">**<"))
                {
                    result = string.Format("<lnvxe:fnlabel alt-content=\"**\">**</lnvxe:fnlabel>");
                    return (result);
                }
                else if (n.Value.Contains(">*<"))
                {
                    result = string.Format("<lnvxe:fnlabel alt-content=\"*\">*</lnvxe:fnlabel>");
                    return (result);
                }
                else
                {
                    result = string.Format("<lnvxe:fnlabel alt-content=\"n{0}\">{0}</lnvxe:fnlabel>",++footnote1);
                    return (result);
                }
            });

            footnote1 = footnote - 1;
            token1 = token - 1;

            string result3 = Regex.Replace(result2, pattent_indicator, (Match n) =>
            {
                if (n.Value.Contains(">***<"))
                {
                    result = string.Format("<lnvxe:fnr alt-content=\"***\" fnrtoken=\"ref{0}\" fntoken=\"fnote{0}\">***</lnvxe:fnr>",++token1);
                    return (result);
                }
                else if (n.Value.Contains(">**<"))
                {
                    result = string.Format("<lnvxe:fnr alt-content=\"**\" fnrtoken=\"ref{0}\" fntoken=\"fnote{0}\">**</lnvxe:fnr>", ++token1);
                    return (result);
                }
                else if (n.Value.Contains(">*<"))
                {
                    result = string.Format("<lnvxe:fnr alt-content=\"*\" fnrtoken=\"ref{0}\" fntoken=\"fnote{0}\">*</lnvxe:fnr>", ++token1);
                    return (result);
                }
                else
                {
                    result = string.Format("<lnvxe:fnr alt-content=\"n{1}\" fnrtoken=\"ref{0}\" fntoken=\"fnote{0}\">{1}</lnvxe:fnr>", ++token1, ++footnote1);
                    return (result);
                }
            });
            return result3;
        }
    }
}
