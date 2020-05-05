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
            E1897_XML_Convert

        }

        #region Fields

        internal const string PluginName = "Hoàng Hà";
        internal const string PluginShortName = "Hoàng Hà";

        private static string _iniFilePath;
        private static Settings _settings;
        private static bool _showTabBarIcons;

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
            SetCommand((int)CmdIndex.Base64Decode, "Hoàng Hà - Advance Proper Case", Helpers.E1897_SmallCaps);
            SetCommand((int)CmdIndex.E1897_DateConvert, "Hoàng Hà - Date Convert", Helpers.E1897_DateConvert);
            SetCommand((int)CmdIndex.E1897_DateConvert, "Hoàng Hà - XML Convert", Helpers.E1897_XML_Convert);
            SetCommand((int)CmdIndex.E1897_DateConvert, "Hoàng Hà - &&#x2D; to Hyphen '-'", Helpers.E1897_2DtoDash);
            SetCommand((int)CmdIndex.About, "About", About);
            
        }

        internal static void SetToolBarIcon()
        {
            if (!_showTabBarIcons) return;

            var toolbarIconEditIndent = Properties.Resources.edit_indent;
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

            var toolbarIconBinocularPencil = Properties.Resources.cry_mouth_icon16;
            tbIcons = new toolbarIcons
            {
                hToolbarBmp = toolbarIconBinocularPencil.GetHbitmap()
            };
            pTbIcons = Marshal.AllocHGlobal(Marshal.SizeOf(tbIcons));
            Marshal.StructureToPtr(tbIcons, pTbIcons, false);
            Win32.SendMessage(
                nppData._nppHandle,
                NppMsg.NPPM_ADDTOOLBARICON,
                _funcItems.Items[(int)CmdIndex.FindAndReplace]._cmdID,
                pTbIcons);
            Marshal.FreeHGlobal(pTbIcons);
        }

        internal static void PluginCleanUp()
        {
            _settings.Set(SettingsSection.Global, "Version", AssemblyUtils.Version);
            _settings.Set(SettingsSection.Global, "ShowTabBarIcons", _showTabBarIcons);
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