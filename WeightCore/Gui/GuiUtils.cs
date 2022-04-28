// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Sql;
using DataCore.Localizations;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using WeightCore.Gui.XamlPages;

namespace WeightCore.Gui
{
    /// <summary>
    /// GUI utils.
    /// </summary>
    public static class GuiUtils
    {
        /// <summary>
        /// Show WPF form inside WinForm.
        /// </summary>
        public static class WpfForm
        {
            public static DataAccessHelper DataAccess { get; private set; } = DataAccessHelper.Instance;

            /// <summary>
            /// Show pin-code form.
            /// </summary>
            /// <param name="owner"></param>
            /// <returns></returns>
            public static bool ShowNewPinCode(IWin32Window owner)
            {
                using PasswordForm passwordForm = new();
                DialogResult resultPsw = passwordForm.ShowDialog(owner);
                passwordForm.Close();
                passwordForm.Dispose();
                return resultPsw == DialogResult.OK;
            }

            /// <summary>
            /// Show new form.
            /// </summary>
            /// <param name="owner"></param>
            /// <param name="caption"></param>
            /// <param name="message"></param>
            public static DialogResult ShowNew(IWin32Window owner, string caption, string message, VisibilitySettingsEntity visibilitySettings)
            {
                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
                wpfPageLoader.MessageBox.Caption = caption;
                wpfPageLoader.MessageBox.Message = message;
                wpfPageLoader.MessageBox.VisibilitySettings = visibilitySettings;
                wpfPageLoader.MessageBox.VisibilitySettings.Localization();
                DialogResult resultWpf;
                if (owner != null)
                    resultWpf = wpfPageLoader.ShowDialog(owner);
                else
                    resultWpf = wpfPageLoader.ShowDialog();
                wpfPageLoader.Close();
                wpfPageLoader.Dispose();
                return resultWpf;
            }

            /// <summary>
            /// Show settings form.
            /// </summary>
            /// <param name="owner"></param>
            /// <param name="isDebug"></param>
            public static DialogResult ShowNewSettings(IWin32Window owner, bool isDebug)
            {
                DialogResult resultWpf = ShowNew(owner, LocaleCore.Scales.OperationControl,
                    LocaleCore.Scales.DeviceControlIsPreview,
                    new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible });
                if (resultWpf == DialogResult.Yes)
                    Process.Start(isDebug
                        ? "https://device-control-dev-preview.kolbasa-vs.local/" : "https://device-control-prod-preview.kolbasa-vs.local/");
                else
                    Process.Start(isDebug
                        ? "https://device-control-dev.kolbasa-vs.local/" : "https://device-control.kolbasa-vs.local/");
                return resultWpf;
            }

            /// <summary>
            /// Show new OperationControl form.
            /// </summary>
            /// <param name="owner"></param>
            /// <param name="message"></param>
            public static DialogResult ShowNewOperationControl(IWin32Window owner, string message, VisibilitySettingsEntity visibility = null)
            {
                DataAccess.Log.LogInformation(message);
                return ShowNew(owner, LocaleCore.Scales.OperationControl, message,
                    visibility ?? new() { ButtonOkVisibility = Visibility.Visible });
            }

            /// <summary>
            /// Show catch form.
            /// </summary>
            /// <param name="owner"></param>
            /// <param name="message"></param>
            /// <param name="lineNumber"></param>
            /// <param name="memberName"></param>
            public static DialogResult ShowNewCatch(IWin32Window owner, string message,
                bool isLog, string filePath, int lineNumber, string memberName)
            {
                if (isLog)
                    DataAccess.Log.LogError(message, null, null, filePath, lineNumber, memberName);
                return ShowNew(owner, LocaleCore.Scales.Exception,
                    $"{LocaleCore.Scales.Method}: {memberName}." + Environment.NewLine +
                    $"{LocaleCore.Scales.Line}: {lineNumber}." + Environment.NewLine + Environment.NewLine + message,
                    new() { ButtonOkVisibility = Visibility.Visible });
            }
        }

        public static class WinForm
        {
            /// <summary>
            /// Create a TableLayoutPanel.
            /// </summary>
            /// <param name="tableLayoutPanelParent"></param>
            /// <param name="name"></param>
            /// <param name="column"></param>
            /// <param name="row"></param>
            /// <param name="columnSpan"></param>
            /// <returns></returns>
            public static TableLayoutPanel NewTableLayoutPanel(TableLayoutPanel tableLayoutPanelParent, string name, int column, int row, int columnSpan)
            {
                TableLayoutPanel tableLayoutPanel = new()
                {
                    Name = name,
                    ColumnCount = 1,
                    Dock = DockStyle.Fill,
                    Location = new System.Drawing.Point(3, 535),
                    RowCount = 1,
                    Size = new System.Drawing.Size(1018, 130),
                    TabIndex = 99,
                };
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                tableLayoutPanelParent.SetColumnSpan(tableLayoutPanel, columnSpan);
                tableLayoutPanelParent.Controls.Add(tableLayoutPanel, column, row);
                return tableLayoutPanel;
            }

            /// <summary>
            /// Create a Button.
            /// </summary>
            /// <param name="tableLayoutPanel"></param>
            /// <param name="name"></param>
            /// <param name="column"></param>
            /// <returns></returns>
            public static Button NewTableLayoutPanelButton(TableLayoutPanel tableLayoutPanel, string name, int column)
            {
                Button button = new()
                {
                    Name = name,
                    Enabled = false,
                    Visible = false,
                    BackColor = Color.Transparent,
                    Dock = DockStyle.Fill,
                    ForeColor = System.Drawing.SystemColors.ControlText,
                    Margin = new Padding(5, 2, 5, 2),
                    Size = new System.Drawing.Size(100, 100),
                    UseVisualStyleBackColor = false,
                    TabIndex = 100 + column,
                    Location = new System.Drawing.Point(2, 2),
                };
                tableLayoutPanel.Controls.Add(button, column - 1, 0);
                return button;
            }

            /// <summary>
            /// Set the ColumnStyles for TableLayoutPanel.
            /// </summary>
            /// <param name="tableLayoutPanel"></param>
            /// <param name="column"></param>
            public static void SetTableLayoutPanelColumnStyles(TableLayoutPanel tableLayoutPanel, int column)
            {
                tableLayoutPanel.ColumnCount = column;
                float size = 100 / tableLayoutPanel.ColumnCount;
                if (tableLayoutPanel.ColumnStyles.Count > 0)
                    tableLayoutPanel.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, size);
                if (tableLayoutPanel.ColumnCount > 1)
                {
                    for (int i = 0; i < tableLayoutPanel.ColumnCount; i++)
                    {
                        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, size));
                    }
                }
            }
        }
    }
}
