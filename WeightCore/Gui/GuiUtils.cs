// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace WeightCore.Gui
{
    public static class GuiUtils
    {
        #region Public and private methods

        /// <summary>
        /// Show pin-code form.
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public static bool ShowPinCodeForm(IWin32Window owner)
        {
            using PasswordForm passwordForm = new();
            DialogResult resultPsw = passwordForm.ShowDialog(owner);
            passwordForm.Close();
            passwordForm.Dispose();
            return resultPsw == DialogResult.OK;
        }

        public static void ShowWpfSettings(IWin32Window owner, bool isDebug)
        {
            using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
            wpfPageLoader.MessageBox.Caption = LocalizationData.ScalesUI.OperationControl;
            wpfPageLoader.MessageBox.Message = LocalizationData.ScalesUI.DeviceControlIsPreview;
            wpfPageLoader.MessageBox.ButtonYesVisibility = Visibility.Visible;
            wpfPageLoader.MessageBox.ButtonNoVisibility = Visibility.Visible;
            wpfPageLoader.MessageBox.Localization();
            DialogResult resultWpf = wpfPageLoader.ShowDialog(owner);
            wpfPageLoader.Close();
            wpfPageLoader.Dispose();
            if (resultWpf == DialogResult.Yes)
                Process.Start(isDebug
                    ? "https://device-control-dev-preview.kolbasa-vs.local/" : "https://device-control-prod-preview.kolbasa-vs.local/");
            else
                Process.Start(isDebug
                    ? "https://device-control-dev.kolbasa-vs.local/" : "https://device-control.kolbasa-vs.local/");
        }

        public static void ShowWpfCatch(IWin32Window owner, string message, int lineNumber, string memberName)
        {
            using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
            wpfPageLoader.MessageBox.Caption = LocalizationData.ScalesUI.Exception;
            wpfPageLoader.MessageBox.Message =
                @$"{@LocalizationData.ScalesUI.Method}: {memberName}." + Environment.NewLine +
                $"{@LocalizationData.ScalesUI.Line}: {lineNumber}." + Environment.NewLine + Environment.NewLine + message;
            wpfPageLoader.MessageBox.ButtonOkVisibility = System.Windows.Visibility.Visible;
            wpfPageLoader.MessageBox.Localization();
            if (owner != null)
                wpfPageLoader.ShowDialog(owner);
            else
                wpfPageLoader.ShowDialog();
            wpfPageLoader.Close();
            wpfPageLoader.Dispose();
        }

        public static TableLayoutPanel GetTableLayoutPanel(TableLayoutPanel tableLayoutPanelParent, string name, int column, int row, int columnSpan)
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

        public static Button GetTableLayoutPanelButton(TableLayoutPanel tableLayoutPanel, string name, int column)
        {
            Button button = new()
            {
                Name = name,
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

        #endregion
    }
}
