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

using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
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

        }

        #region Fields

        internal const string PluginName = "Hoàng Hà";
        internal const string PluginShortName = "Hoàng Hà";

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
            SetCommand((int)CmdIndex.E1897_DateConvert, "Hoàng Hà - Advance Proper Case", Helpers.E1897_SmallCaps);
            SetCommand((int)CmdIndex.E1897_DateConvert, "Hoàng Hà - Date Convert", Helpers.E1897_DateConvert);
            SetCommand((int)CmdIndex.E1897_DateConvert, "Hoàng Hà - XML Convert", Helpers.E1897_XML_Convert);
            SetCommand((int)CmdIndex.E1897_DateConvert, "Hoàng Hà - &&#x2D; to Hyphen '-'", Helpers.E1897_2DtoDash);
            SetCommand((int)CmdIndex.E1897_Footnote2End, "Hoàng Hà - Footnote to End", HoangHaFunctions.FootnoteToEnd);
            SetCommand((int)CmdIndex.E1897_Footnote2End, "-------------------------------", HoangHaFunctions.EmptyFunc);
            if (_Username == "HOANG HA" || _Username == "E1897" || _Username == "E1859" || _Username == "E0265" || _Username == "E1057")
            {
                SetCommand((int)CmdIndex.E1897_Footnote2End, "Hoàng Hà - (Under Construction)", HoangHaFunctions.EmptyFunc);
                SetCommand((int)CmdIndex.E1897_Footnote2End, "Hoàng Hà - Remove Duplicated Lines", HoangHaFunctions.RemoveDuplicatedLines);
                SetCommand((int)CmdIndex.E1897_Footnote2End, "Hoàng Hà - smcaps XML", HoangHaFunctions.SmCapsTags);
                SetCommand((int)CmdIndex.E1897_Footnote2End, "Hoàng Hà - New Font Process", HoangHaFunctions.NewFontProcess);
                SetCommand((int)CmdIndex.E1897_Footnote2End, "Hoàng Hà - New Para", HoangHaFunctions.NewParaFormat);
                SetCommand((int)CmdIndex.E1897_Footnote2End, "Hoàng Hà - Remove Fonts", HoangHaFunctions.RemoveFonts);
                SetCommand((int)CmdIndex.E1897_SegmentsSort, "Hoàng Hà - Segments Sort", HoangHaFunctions.SegmentsSort);
                SetCommand((int)CmdIndex.E1897_SegmentsSort, "Hoàng Hà - Footnote Return", HoangHaFunctions.FootnoteReturn);
            }
            SetCommand((int)CmdIndex.About, "About", About);
            
        }

        internal static void SetToolBarIcon()
        {
            if (!_showTabBarIcons) return;

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

            var toolbarIconSort = Properties.Resources.Sort_ascending_icon;
            var tbIcons2 = new toolbarIcons
            {
                hToolbarBmp = toolbarIconSort.GetHbitmap()
            };
            var pTbIcons2 = Marshal.AllocHGlobal(Marshal.SizeOf(tbIcons2));
            Marshal.StructureToPtr(tbIcons2, pTbIcons2, false);
            Win32.SendMessage(
                nppData._nppHandle,
                NppMsg.NPPM_ADDTOOLBARICON,
                _funcItems.Items[(int)CmdIndex.E1897_SegmentsSort]._cmdID,
                pTbIcons2);
            Marshal.FreeHGlobal(pTbIcons2);

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
                string.Format("{0}\r\nv{1}\r\n\r\nBy Hoang Ha\r\nAka E1897", PluginName, AssemblyUtils.Version),
                string.Format("{0} Plugin", PluginName),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        #endregion
    }
}