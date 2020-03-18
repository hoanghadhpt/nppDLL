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
using System.Text;
using System.Windows.Forms;
using phdesign.NppToolBucket.Forms;
using phdesign.NppToolBucket.Infrastructure;
using phdesign.NppToolBucket.PluginCore;

namespace phdesign.NppToolBucket
{
    internal class IndentationSettings
    {
        #region Fields

        private readonly Editor _editor;
        /// <summary>
        /// The single reused instance of the dialog.
        /// </summary>
        private readonly IndentationSettingsForm _dialog;
        /// <summary>
        /// By attaching to the correct Notepad++ window, if user switching applications, the dialog hides with Notepad++.
        /// </summary>
        private readonly IWin32Window _owner;
        /// <summary>
        /// This class is a singleton.
        /// </summary>
        private static IndentationSettings _instance;

        #endregion

        #region Constructor

        private IndentationSettings()
        {
            _editor = Editor.GetActive();
            _dialog = new IndentationSettingsForm();
            _owner = new WindowWrapper(PluginBase.nppData._nppHandle);
        }

        #endregion

        #region Public Static Methods

        internal static void Show()
        {
            if (_instance == null)
                _instance = new IndentationSettings();
            _instance.ShowDialog();
        }

        #endregion

        #region Private Methods

        // C:\Working\Sandbox\NppPlugin.NET\scintilla\scintilla\scite\src\SciTEBase.cxx@2795
        // C:\Working\Sandbox\NppPlugin.NET\scintilla\scintilla\scite\gtk\SciTEGTK.cxx@1874
        private void ShowDialog()
        {
            if (_dialog.Visible) return;

            _dialog.TabSize = _editor.GetTabWidth();
            _dialog.IndentSize = _editor.GetIndent();
            _dialog.UseTabs = (_editor.GetUseTabs() != 0);

            var result = _dialog.ShowDialog(_owner);
            if (result != DialogResult.OK) return;

            SetTabSize(_dialog.TabSize, _dialog.IndentSize, _dialog.UseTabs);
            if (_dialog.ConvertIndent)
                ConvertIndentation(_dialog.TabSize, _dialog.UseTabs);
        }

        private void SetTabSize(int tabSize, int indentSize, bool useTabs)
        {
            if (tabSize > 0)
                _editor.SetTabWidth(tabSize);
            if (indentSize >= 0)
                _editor.SetIndent(indentSize);
            _editor.SetUseTabs(useTabs);
        }

        private void ConvertIndentation(int tabSize, bool useTabs)
        {
            _editor.BeginUndoAction();
            var maxLine = _editor.GetLineCount();
            for (var line = 0; line < maxLine; line++)
            {
                var lineStart = _editor.PositionFromLine(line);
                var indent = _editor.GetLineIndentation(line);
                var indentPos = _editor.GetLineIndentPosition(line);
                const int maxIndentation = 1000;
                if (indent < maxIndentation)
                {
                    var indentationNow = _editor.GetTextByRange(lineStart, indentPos, maxIndentation);
                    var indentationWanted = CreateIndentation(indent, tabSize, !useTabs);
                    if (indentationNow != indentationWanted)
                    {
                        _editor.ReplaceText(lineStart, indentPos, indentationWanted);
                    }
                }
            }
            _editor.EndUndoAction();
        }

        private static string CreateIndentation(int indent, int tabSize, bool insertSpaces)
        {
            var indentation = new StringBuilder();
            if (!insertSpaces)
            {
                while (indent >= tabSize)
                {
                    indentation.Append("\t");
                    indent -= tabSize;
                }
            }
            while (indent > 0)
            {
                indentation.Append(" ");
                indent--;
            }
            return indentation.ToString();
        }

        #endregion
    }
}