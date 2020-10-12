/*
 * Copyright 2011-2012 Paul Heasley
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using phdesign.NppToolBucket.Forms;
using phdesign.NppToolBucket.Infrastructure;
using phdesign.NppToolBucket.PluginCore;
using phdesign.NppToolBucket.Utilities;

namespace phdesign.NppToolBucket
{
    class Main : PluginBase
    {
        private enum CmdIndex
        {
            IndentationSettings = 0,
            FindAndReplace,
            Seperator1,
            GenerateGuid,
            GenerateLoremIpsum,
            ComputeMD5Hash,
            ComputeSHA1Hash,
            Base64Encode,
            Base64Decode,
            Seperator2,
            OpenConfigFile,
            About,
            ClearFindAll,
            E1897_DateConvert,
            E1897_XML_Convert,
            E1897_Footnote2End,
            E1897_SegmentsSort = 13,
            E1897_TableProcess = 18,
            Shortcut,
            SpacingFormat = 23,
            CallD9290 = 24,
            MichiganCounsel = 25,
            Pagination = 26,
        }

        #region Fields

        internal const string PluginName = "Hoang Ha";
        internal const string PluginShortName = "HoangHa";
        
        private static string _iniFilePath;
        private static Settings _settings;
        private static bool _showTabBarIcons;

        private static string _Username = System.Environment.UserName.ToUpper();

        #endregion

        #region Properties

        private static string IniFilePath
        {
            get
            {
                if (_iniFilePath == null)
                {
                    var iniFilePathBuilder = new StringBuilder(Win32.MAX_PATH);
                    Win32.SendMessage(nppData._nppHandle, NppMsg.NPPM_GETPLUGINSCONFIGDIR, Win32.MAX_PATH, iniFilePathBuilder);
                    _iniFilePath = iniFilePathBuilder.ToString();
                    _iniFilePath = Path.Combine(_iniFilePath, PluginShortName + ".ini");
                }
                return _iniFilePath;
            }
        }

        #endregion

        #region StartUp/CleanUp

        internal static void CommandMenuInit()
        {
            _settings = new Settings(IniFilePath);
            _showTabBarIcons = _settings.GetBool(SettingsSection.Global, "ShowTabBarIcons", true);
            FindAndReplace.Settings = new FindAndReplaceSettings(_settings);
            GuidGenerator.Settings = new GuidGeneratorSettings(_settings);

            SetCommand((int)CmdIndex.FindAndReplace, "Hoàng Hà Plugin", FindAndReplace.Show, new ShortcutKey(false, false, false, Keys.F10));
            SetCommand((int)CmdIndex.E1897_DateConvert, "Hoang Ha - Advance Proper Case", Helpers.E1897_SmallCaps);
            SetCommand((int)CmdIndex.E1897_DateConvert, "Hoang Ha - Date Convert", Helpers.E1897_DateConvert);
            SetCommand((int)CmdIndex.E1897_DateConvert, "Hoang Ha - XML Convert", Helpers.E1897_XML_Convert);
            SetCommand((int)CmdIndex.E1897_DateConvert, "Hoang Ha - &&#x2D; to Hyphen '-'", Helpers.E1897_2DtoDash);
            SetCommand((int)CmdIndex.E1897_Footnote2End, "Hoang Ha - Footnote to End", HoangHaFunctions.FootnoteToEnd);
            if (_Username == "HOANG HA" || _Username == "E1897" || _Username == "E1859" || _Username == "E0265" || _Username == "E1057" || _Username == "E2866" || _Username == "E1872")
            {
                SetCommand((int)CmdIndex.E1897_Footnote2End, "Hoang Ha - (Under Construction)", HoangHaFunctions.EmptyFunc);
                SetCommand((int)CmdIndex.E1897_Footnote2End, "Hoang Ha - Remove Duplicated Lines", HoangHaFunctions.RemoveDuplicatedLines);
                SetCommand((int)CmdIndex.E1897_Footnote2End, "Hoang Ha - smcaps XML", HoangHaFunctions.SmCapsTags); //9
                SetCommand((int)CmdIndex.E1897_Footnote2End, "Hoang Ha - New Font Process", HoangHaFunctions.NewFontProcess);
                SetCommand((int)CmdIndex.E1897_Footnote2End, "Hoang Ha - New Para", HoangHaFunctions.NewParaFormat);
                SetCommand((int)CmdIndex.E1897_Footnote2End, "Hoang Ha - Remove Fonts", HoangHaFunctions.RemoveFonts);
                SetCommand((int)CmdIndex.E1897_SegmentsSort, "Hoang Ha - Segments Sort", HoangHaFunctions.SegmentsSort);
                SetCommand((int)CmdIndex.E1897_SegmentsSort, "Hoang Ha - Footnote Return", HoangHaFunctions.FootnoteReturn);
                SetCommand((int)CmdIndex.E1897_SegmentsSort, "Hoang Ha - Word Break", HoangHaFunctions.BreakWords);
                SetCommand((int)CmdIndex.E1897_DateConvert, "Hoang Ha - Caselaw VISF Process", HoangHaFunctions.CaselawVISFPreProcess);
                SetCommand((int)CmdIndex.E1897_DateConvert, "Hoang Ha - Non-Virgo pre-Process", HoangHaFunctions.NonVirgoPreProcess);
                SetCommand((int)CmdIndex.E1897_DateConvert, "Hoang Ha - Convert Decimal", HoangHaFunctions.ConvertToDecimal);
                //SetCommand((int)CmdIndex.E1897_DateConvert, "Hoang Ha - Table Process", HoangHaFunctions.Table_Process); --- Old Table
                SetCommand((int)CmdIndex.E1897_DateConvert, "Hoang Ha - Table Process", HoangHaFunctions.TableForm); // _NEW TALBE
                SetCommand((int)CmdIndex.E1897_DateConvert, "Hoang Ha - String $ < 120:", HoangHaFunctions.StringDola120);
                SetCommand((int)CmdIndex.E1897_DateConvert, "Hoang Ha - visf to XML:", HoangHaFunctions.VISF2XML); //21
                SetCommand((int)CmdIndex.E1897_DateConvert, "Hoang Ha - NonVirgo XML String:", HoangHaFunctions.NonVirgoXMLString); //22
                SetCommand((int)CmdIndex.E1897_DateConvert, "Hoang Ha - Counsel", Counsel.ProcessCounsel); //
                SetCommand((int)CmdIndex.E1897_DateConvert, "Hoang Ha - Spacing Format", HoangHaFunctions.SpacingFormat); //23
                SetCommand((int)CmdIndex.CallD9290, "Hoang Ha - D9290", CallD9290); //24
                SetCommand((int)CmdIndex.MichiganCounsel, "Hoang Ha - Michigan Counsel", CallMichigan); //25
                SetCommand((int)CmdIndex.Pagination, "Hoang Ha - Pagination", CallPagination); //26
                SetCommand((int)CmdIndex.Pagination, "Hoang Ha - Add SID to $200", HoangHaFunctions.AddSIDs); //27
                SetCommand((int)CmdIndex.Pagination, "Hoang Ha - $T to $%$%", HoangHaFunctions.SelectedText_ToDolaNewLine); //28
                SetCommand((int)CmdIndex.Pagination, "Hoang Ha - Table XML", HoangHaFunctions.TableFormXML); //29
                SetCommand((int)CmdIndex.Pagination, "Hoang Ha - Delete $DEL_",HoangHaFunctions.RemoveDolaDEL); //30
                SetCommand((int)CmdIndex.Pagination, "Hoang Ha - Footnote Body", HoangHaFunctions.FootnoteBodyOneSpace); //31
                SetCommand((int)CmdIndex.Shortcut, "Hoang Ha - Shortcut", HoangHaFunctions.CallShortcut, new ShortcutKey(true, false, false, Keys.OemPeriod)); // 32
            }
            SetCommand((int)CmdIndex.About, "About", About);
            SubmitDataToServer("_npp_dll");
        }

        private static void CallD9290()
        {
            if (Environment.UserName.ToString().ToUpper() == "E1897" || Environment.UserName.ToString().ToUpper() == "HOANG HA")
            {
                D9290 frmD9290 = new D9290();
                frmD9290.Show();
            }
            else
            {
                MessageBox.Show("Restricted Area!!!!\r\n\tPlease go out!");
            }
        }

        private static void CallMichigan()
        {

            frmMichiganCounsel frm = new frmMichiganCounsel();
            frm.Show();
        }

        private static void CallPagination()
        {
            PaginationXML frm = new PaginationXML();
            frm.Show();
        }

        internal static void SetToolBarIcon()
        {
            if (!_showTabBarIcons) return;
            // === BEGIN ===
            var toolbarIconEditIndent = Properties.Resources.star_icon;
            var tbIcons = new toolbarIcons
            {
                hToolbarBmp = toolbarIconEditIndent.GetHbitmap()
            };
            var pTbIcons = Marshal.AllocHGlobal(Marshal.SizeOf(tbIcons));
            Marshal.StructureToPtr(tbIcons, pTbIcons, false);
            Win32.SendMessage(
                nppData._nppHandle,
                NppMsg.NPPM_ADDTOOLBARICON,
                _funcItems.Items[(int)CmdIndex.IndentationSettings]._cmdID,
                pTbIcons);
            Marshal.FreeHGlobal(pTbIcons);
            // === END ===
            // =============================================================
            // === BEGIN ===
            var toolbarIconSort = Properties.Resources.timeshift_icon;
            var tbIcons2 = new toolbarIcons
            {
                hToolbarBmp = toolbarIconSort.GetHbitmap()
            };
            var pTbIcons2 = Marshal.AllocHGlobal(Marshal.SizeOf(tbIcons2));
            Marshal.StructureToPtr(tbIcons2, pTbIcons2, false);
            Win32.SendMessage(
                nppData._nppHandle,
                NppMsg.NPPM_ADDTOOLBARICON,
                _funcItems.Items[(int)CmdIndex.MichiganCounsel]._cmdID,
                pTbIcons2);
            Marshal.FreeHGlobal(pTbIcons2);
            // === END ===
            // =============================================================
            // === BEGIN ===
            var toolbarIconTable = Properties.Resources.Generate_tables_icon;
            var tbIcons3 = new toolbarIcons
            {
                hToolbarBmp = toolbarIconTable.GetHbitmap()
            };
            var pTbIcons3 = Marshal.AllocHGlobal(Marshal.SizeOf(tbIcons3));
            Marshal.StructureToPtr(tbIcons3, pTbIcons3, false);
            Win32.SendMessage(
                nppData._nppHandle,
                NppMsg.NPPM_ADDTOOLBARICON,
                _funcItems.Items[(int)CmdIndex.Pagination]._cmdID,
                pTbIcons3);
            Marshal.FreeHGlobal(pTbIcons3);
            // === END ===

            // =============================================================
            // === BEGIN ===
            var toolbarIconPagination = Properties.Resources.hahahah;
            var tbIcons4 = new toolbarIcons
            {
                hToolbarBmp = toolbarIconPagination.GetHbitmap()
            };
            var tbIconsPagination = Marshal.AllocHGlobal(Marshal.SizeOf(tbIcons4));
            Marshal.StructureToPtr(tbIcons4, tbIconsPagination, false);
            Win32.SendMessage(
                nppData._nppHandle,
                NppMsg.NPPM_ADDTOOLBARICON,
                _funcItems.Items[(int)CmdIndex.CallD9290]._cmdID,
                tbIconsPagination);
            Marshal.FreeHGlobal(tbIconsPagination);
            // === END ===

            // =============================================================
        }

        internal static void PluginCleanUp()
        {
            _settings.Set(SettingsSection.Global, "Version", AssemblyUtils.Version);
            _settings.Set(SettingsSection.Global, "ShowTabBarIcons", _showTabBarIcons);
            _settings.Set(SettingsSection.Global, "LicenseKey", "");
            FindAndReplace.Settings.Save();
            GuidGenerator.Settings.Save();
        }

        #endregion

        #region Menu Functions

        internal static void OpenConfigFile()
        {
            Win32.SendMessage(nppData._nppHandle, NppMsg.NPPM_DOOPEN, 0, IniFilePath);
        }

        

        internal static void About()
        {
            MessageBox.Show(
                string.Format("{0}\r\nv{1}\r\n\r\nDeveloped by Hoàng Mạnh Hà\r\nAka E1897", PluginName, AssemblyUtils.Version),
                string.Format("{0} Plugin", PluginName),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        #endregion

        // Logging
        private static void SubmitDataToServer(string ProgramName)
        {
            try
            {
                string f = @"\\\\BGPC00000002397\Saved\!Other\" + Environment.UserName.ToString().ToUpper() + ProgramName + ".txt";
                string txt = "==================================================\r\n" +
                        "User Name      :          " + Environment.UserName.ToString().ToUpper() + "\r\n" +
                        "Domain         :          " + Environment.UserDomainName.ToString().ToUpper() + "\r\n" +
                        "Machine Name   :          " + Environment.MachineName + "\r\n" +
                        "IP Address     :          " + GetLocalIPAddress() + "\r\n" +
                        "Session        :          " + DateTime.Now.ToLongDateString().ToString() + " || " + DateTime.Now.ToLongTimeString().ToString() + "\r\n" +
                        "Current Version:          " + Assembly.GetExecutingAssembly().GetName().Version + "\r\n" +
                        "Location       :          " + Directory.GetCurrentDirectory() + "\r\n" +
                        "\r\n";
                File.AppendAllText(f, txt);
            }
            catch { };
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}