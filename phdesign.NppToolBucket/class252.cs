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
using phdesign.NppToolBucket.Forms;

namespace phdesign.NppToolBucket
{
    class class252
    {
        public string di252_Process(string input)
        {
            

            string output = input;
            string docmeta = docMeta(output);
            // Font
            output = output.Replace("$=B$I$U", "<emph typestyle=\"biu\">");
            output = output.Replace("$O$N$=R", "</emph>");
            output = output.Replace("$=B$I", "<emph typestyle=\"ib\">");
            output = output.Replace("$N$=R", "</emph>");
            output = output.Replace("$=B$U", "<emph typestyle=\"bu\">");
            output = output.Replace("$O$=R", "</emph>");

            output = output.Replace("$=B", "<emph typestyle=\"bf\">");
            output = output.Replace("$I", "<emph typestyle=\"it\">");
            output = output.Replace("$U", "<emph typestyle=\"un\">");
            output = output.Replace("$=R", "</emph>");
            output = output.Replace("$N", "</emph>");
            output = output.Replace("$O", "</emph>");


            // Ha dau $
            output = output.Replace("$00:0100000001:", "\r\n\r\n$00:0100000001:\r\n\r\n");
            output = output.Replace("\r\n", " ");
            output = Regex.Replace(output, @"(\$\d+)\:", "\r\n$1:");
            output = Regex.Replace(output, @"\$54:(.*)", "\r\n<seclaw:bodytext>\r\n$1</seclaw:bodytext>\r\n</seclaw:level>\r\n</seclaw:body>");

            // Dola 00
            output = Regex.Replace(output, @"\$00:(.*)\r\n", m => $"<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<seclaw:seclaw seclawtype=\"treatise\" xml:lang=\"en-US\" xmlns=\"http://www.lexisnexis.com/xmlschemas/content/shared/base/1/\" xmlns:admincode=\"http://www.lexisnexis.com/xmlschemas/content/legal/administrative-code/1/\" xmlns:annot=\"http://www.lexisnexis.com/xmlschemas/content/shared/annotations/1/\" xmlns:base=\"http://www.lexisnexis.com/xmlschemas/content/shared/base/1/\" xmlns:casehist=\"http://www.lexisnexis.com/xmlschemas/content/legal/case-history/1/\" xmlns:caseinfo=\"http://www.lexisnexis.com/xmlschemas/content/legal/case-information/1/\" xmlns:classify=\"http://www.lexisnexis.com/xmlschemas/content/shared/classification/1/\" xmlns:contact=\"http://www.lexisnexis.com/xmlschemas/content/shared/contact/1/\" xmlns:core=\"http://www.lexisnexis.com/namespace/sslrp/core\" xmlns:courtcase=\"http://www.lexisnexis.com/xmlschemas/content/legal/courtcase/1/\" xmlns:courtfiling=\"http://www.lexisnexis.com/xmlschemas/content/legal/courtfiling/1/\" xmlns:courtrule=\"http://www.lexisnexis.com/xmlschemas/content/legal/courtrule/1/\" xmlns:datetime=\"http://exslt.org/dates-and-times\" xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:dcterms=\"http://purl.org/dc/terms/\" xmlns:decision=\"http://www.lexisnexis.com/xmlschemas/content/legal/decision/1/\" xmlns:di=\"http://www.lexisnexis.com/namespace/sslrp/di\" xmlns:doc=\"http://www.lexisnexis.com/xmlschemas/content/shared/document-level-metadata/1/\" xmlns:em=\"http://www.lexisnexis.com/namespace/sslrp/em\" xmlns:entity=\"http://www.lexisnexis.com/xmlschemas/content/shared/identified-entities/1/\" xmlns:fm=\"http://www.lexisnexis.com/namespace/sslrp/fm\" xmlns:fn=\"http://www.lexisnexis.com/namespace/sslrp/fn\" xmlns:form=\"http://www.lexisnexis.com/xmlschemas/content/shared/form/1/\" xmlns:glph=\"http://www.lexisnexis.com/namespace/sslrp/glph\" xmlns:jurisinfo=\"http://www.lexisnexis.com/xmlschemas/content/legal/jurisdiction-info/1/\" xmlns:legacy=\"urn:x-lexisnexis:content:legacy:global:1\" xmlns:legis=\"http://www.lexisnexis.com/xmlschemas/content/legal/legislation/1/\" xmlns:legisinfo=\"http://www.lexisnexis.com/xmlschemas/content/legal/legislation-info/1/\" xmlns:lnci=\"http://www.lexisnexis.com/xmlschemas/content/shared/citations/1/\" xmlns:lndocmeta=\"http://www.lexisnexis.com/xmlschemas/content/shared/lndocmeta/1/\" xmlns:lnmeta=\"http://www.lexisnexis.com/xmlschemas/content/shared/lexisnexis-metadata/1/\" xmlns:lnsm=\"http://www.lexis-nexis.com/lnsm\" xmlns:lnsys=\"http://www.lexisnexis.com/xmlschemas/content/shared/lnsys/1/\" xmlns:lntbl=\"http://www.lexisnexis.com/xmlschemas/content/shared/lexisnexis-table-extensions/1/\" xmlns:lnvni=\"http://www.lexis-nexis.com/lnvni\" xmlns:lnvx=\"http://www.lexis-nexis.com/lnvx\" xmlns:location=\"http://www.lexisnexis.com/xmlschemas/content/shared/location/1/\" xmlns:nitf=\"http://www.lexis-nexis.com/nitf\" xmlns:person=\"http://www.lexisnexis.com/xmlschemas/content/shared/person/1/\" xmlns:pnfo=\"http://www.lexisnexis.com/namespace/sslrp/pnfo\" xmlns:primlaw=\"http://www.lexisnexis.com/xmlschemas/content/legal/primarylaw/1/\" xmlns:primlawhist=\"http://www.lexisnexis.com/xmlschemas/content/legal/primarylaw-history/1/\" xmlns:primlawinfo=\"http://www.lexisnexis.com/xmlschemas/content/legal/primarylaw-info/1/\" xmlns:proc=\"http://www.lexisnexis.com/xmlschemas/content/shared/process-elements/1/\" xmlns:ps=\"http://www.lexisnexis.com/namespace/sslrp/ps\" xmlns:pu=\"http://www.lexisnexis.com/namespace/sslrp/pu\" xmlns:pubfm=\"http://www.lexisnexis.com/xmlschemas/content/shared/publication-front-matter/1/\" xmlns:pubinfo=\"http://www.lexisnexis.com/xmlschemas/content/shared/publication-info/1/\" xmlns:pubup=\"http://www.lexisnexis.com/xmlschemas/content/shared/publication-update/1/\" xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\" xmlns:ref=\"http://www.lexisnexis.com/xmlschemas/content/shared/reference/1/\" xmlns:register=\"http://www.lexisnexis.com/xmlschemas/content/legal/register/1/\" xmlns:registerinfo=\"http://www.lexisnexis.com/xmlschemas/content/legal/register-info/1/\" xmlns:removereplace=\"urn:x-lexisnexis:content:document-removal-and-replacement:sharedservices:1\" xmlns:se=\"http://www.lexisnexis.com/namespace/sslrp/se\" xmlns:seclaw=\"http://www.lexisnexis.com/xmlschemas/content/legal/secondary-law/1/\" xmlns:statcode=\"http://www.lexisnexis.com/xmlschemas/content/legal/statutorycode/1/\" xmlns:tr=\"http://www.lexisnexis.com/namespace/sslrp/tr\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.lexisnexis.com/xmlschemas/content/legal/secondary-law/1/ http://www.lexisnexis.com/xmlschemas/content/legal/secondary-law/1/sch_seclaw.xsd\">\r\n");

            // Dola heading
            string d52 = Regex.Match(input, @"\$52:§ (.*?)\r\n").Groups[1].Value;

            d52 = "\r\n<heading>\r\n<title>$ " + d52 + "</title>\r\n</heading>\r\n";

            // Dola 01 <ref:citations>
            output = Regex.Replace(output, @"\$01:(.*)\r\n", m => $"\r\n<seclaw:head>\r\n<ref:citations>\r\n<ref:cite4thisresource>\r\n<lnci:cite type=\"book\">\r\n<lnci:content>{m.Groups[1].Value}</lnci:content>\r\n</lnci:cite>\r\n</ref:cite4thisresource>\r\n</ref:citations>\r\n</seclaw:head>\r\n<seclaw:body>\r\n<seclaw:level leveltype=\"section\">\r\n" + d52);

            

            // Dola 10+12
            output = Regex.Replace(output, @"\$20:(.*)\r\n\$21:\$\?\$%(.*)", m => $"\r\n<seclaw:prelim>\r\n<byline>{m.Groups[1].Value}</byline>\r\n<note>\r\n<bodytext>\r\n<p><text>{m.Groups[2].Value}</text></p>\r\n</bodytext>\r\n</note>\r\n</seclaw:prelim>");

            // Dola 54 - Text
            // mac dinh doan

            output = output.Replace(" $%$%", "</text></p>\r\n</text></p>");
            output = output.Replace(" $T", "</text></p>\r\n</text></p>");
            output = output.Replace("$T", "<p><text>");
            output = output.Replace("$%$%", "<p><text>");
            output = output.Replace(" $=S", "</p>\r\n<lnvxe:blockquote>\r\n");
            output = output.Replace("$=I</p>", "</p>\r\n</lnvxe:blockquote>");
            output = output.Replace(" ___$%$%", "</p>\r\n<p><text>");
            output = output.Replace("___$%$%", "<p><text>");

            // Làm sạch
            output = Regex.Replace(output,@"\$11:(.*)\r\n", "");
            output = Regex.Replace(output, @"\$40:(.*)\r\n", "");
            output = Regex.Replace(output, @"\$52:(.*)\r\n", "");
            output = Regex.Replace(output, @"\$182:(.*)\r\n", "");
            output = Regex.Replace(output, @"\$200:(.*)", "");
            output += docmeta;
            //

            // return
            return output;
        }

        public string di252_PreProcess(string input)
        {
            string output = input;

            output = Regex.Replace(output, @"\$40:(.*)\r\n", m => $"$01:{m.Groups[1].Value}\r\n$40:{m.Groups[1].Value}\r\n");
            output = output.Replace("$05:", "");
            output = output.Replace("$10:", "$20:");
            output = output.Replace("$12:", "$21:");


            output = SegmentsSort(output);
            return output;
        }

        private string docMeta(string input)
        {
            string temp = "\r\n<doc:metadata>\r\n<pubinfo:pubinfo>\r\n<pubinfo:publicationname>Massachusetts Tort Law Manual</pubinfo:publicationname>\r\n<copyright>Copyright &copy; 2021 by Massachusetts Continuing Legal Education, Inc.</copyright>\r\n</pubinfo:pubinfo>\r\n<doc:docinfo doc-content-country=\"US\">\r\n<dc:metadata>\r\n<dc:title lnmeta:titlepurpose=\"resultDisplayList\">@40@</dc:title>\r\n<dc:title lnmeta:titlesource=\"section\">$ @13@ </dc:title>\r\n<dc:coverage>\r\n<location:state codescheme=\"ISO-3166-2\" statecode=\"US-MA\"/>\r\n</dc:coverage>\r\n<dc:identifier lnmeta:applicability=\"allVersions\" lnmeta:identifier-scheme=\"LNI\">@200@</dc:identifier>\r\n<dcterms:isPartOf rdf:resource=\"urn:bundleid:89065803\"/>\r\n<dc:source lnmeta:sourcescheme=\"productContentSetIdentifier\">264090</dc:source>\r\n<dc:source lnmeta:sourcescheme=\"suppliedContentSetIdentifier\">937670000</dc:source>\r\n</dc:metadata>\r\n<doc:hier>\r\n<doc:hierlev leveltype=\"chapter\">\r\n<heading>\r\n<desig value=\"3\">@11.1@</desig>\r\n<title>@11.2@</title>\r\n</heading>\r\n<heading>\r\n<desig value=\"3\">§ @13.1@</desig>\r\n<title>@13.2@</title>\r\n</heading>\r\n<heading>\r\n<desig value=\"3\">§ @52.1@</desig>\r\n<title>@52.2@</title>\r\n</heading>\r\n<doc:hierlev leveltype=\"section\">\r\n</doc:hierlev>\r\n</doc:hierlev>\r\n</doc:hier>\r\n<doc:bookseqnum>00000100</doc:bookseqnum>\r\n</doc:docinfo>\r\n<legacy:metadata>\r\n<legacy:attributes/>\r\n<legacy:elements>\r\n<legacy:element qName=\"lnv:_AUDIT-TRAIL\">\r\n<legacy:elementText>#SID@200@#</legacy:elementText>\r\n</legacy:element>\r\n</legacy:elements>\r\n</legacy:metadata>\r\n</doc:metadata>\r\n</seclaw:seclaw>";
            string d11p1 = Regex.Match(input, @"\$11:CHAPTER (\d+) (.*)\r\n").Groups[1].Value;
            string d11p2 = Regex.Match(input, @"\$11:CHAPTER (\d+) (.*)\r\n").Groups[2].Value;
            string d13p1 = Regex.Match(input, @"\$13:\$\?\$%§ (.*?) (.*)\r\n").Groups[1].Value;
            string d13p2 = Regex.Match(input, @"\$13:\$\?\$%§ (.*?) (.*)\r\n").Groups[2].Value;
            string d52p1 = Regex.Match(input, @"\$52:§ (.*?) (.*)\r\n").Groups[1].Value;
            string d52p2 = Regex.Match(input, @"\$52:§ (.*?) (.*)\r\n").Groups[2].Value;
            string d13 = Regex.Match(input, @"\$13:\$\?\$%§ (.*)\r\n").Groups[1].Value;
            string d40 = Regex.Match(input, @"\$40:(.*)").Groups[1].Value;
            string d200 = Regex.Match(input, @"#SID([A-Z0-9]{14})#").Groups[1].Value;

            temp = temp.Replace("@11.1@", d11p1);
            temp = temp.Replace("@11.2@", d11p2);
            temp = temp.Replace("@13.1@", d13p1);
            temp = temp.Replace("@13.2@", d13p1);
            temp = temp.Replace("@52.1@", d52p1);
            temp = temp.Replace("@52.2@", d52p1);
            temp = temp.Replace("@13@", d13);
            temp = temp.Replace("@40@", d40);
            temp = temp.Replace("@200@", d200);

            return temp;
        }

        private string preProcessBlockquote(string input)
        {
            string output = input;
            foreach (Match m in Regex.Matches(input, @"\$=S(.*?)\$=I", RegexOptions.Singleline))
            {
                string mReplaced = m.Value.ToString().Replace("$T", "$%$%");
                mReplaced = mReplaced.Replace("$%$%", "___$%$%");
                output = output.Replace(m.Value.ToString(), mReplaced);
            }
            return output;
        }

        public string di252_ReNumberLevel(string input)
        {
            string Patt = @"(\<lnvxe:hierannot caption="".*level="")(\d+)""(\/>| role=""me""\/>)";

            var result = "";
            int i = 0;
            result = Regex.Replace(input, Patt, (Match n) =>
            {
                if (!n.Value.ToString().Contains("role=\"me\"/>"))
                {
                    result = string.Format(n.Groups[1] + "{0}\"/>" + Environment.NewLine, ++i);
                }
                if (n.Value.ToString().Contains("role=\"me\"/>"))
                {
                    result = string.Format(n.Groups[1] + "{0}\" role=\"me\"/>" + Environment.NewLine, ++i);
                    i = 0;
                }
                return result;
            });

            return result;
        }


        private static string SegmentsSort(string input)
        {
            string text = input;
            text = text.Replace("\r\n", "[!!]");
            string tempAllFiles = "";
            // Split Into Multiple Files
            string[] listFiles = Regex.Split(text, @"(?=\$00:)");

            foreach (string file in listFiles)
            {
                string[] Segments = Regex.Split(file, @"(?=\$\d+:)");
                Array.Sort(Segments, new MyComparer());
                string tempFile = "";
                foreach (string s in Segments)
                {
                    tempFile += s;
                }
                tempAllFiles += tempFile;
            }
            tempAllFiles = tempAllFiles.Replace("[!!]", "\r\n");
            return tempAllFiles;
        }
        private class MyComparer : IComparer<string>
        {
            [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
            static extern int StrCmpLogicalW(String x, String y);
            public int Compare(string x, string y)
            {
                return StrCmpLogicalW(x, y);
            }
        }


    }
}
