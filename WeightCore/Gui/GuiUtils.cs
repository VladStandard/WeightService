// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Protocols;
using DataCore.Sql;
using DataCore.Sql.TableScaleModels;
using Microsoft.Data.SqlClient;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Linq;
using WeightCore.Gui.XamlPages;
using WeightCore.Helpers;
using static DataCore.ShareEnums;

namespace WeightCore.Gui
{
    /// <summary>
    /// GUI utils.
    /// </summary>
    [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract")]
    public static class GuiUtils
    {
        /// <summary>
        /// Show WPF form inside WinForm.
        /// </summary>
        public static class WpfForm
        {
            private static DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
            public static WpfPageLoader WpfPage { get; private set; }

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

            public static bool IsExistsWpfPage(IWin32Window owner, ref DialogResult resultWpf)
            {
                if (WpfPage != null)
                {
                    if (!WpfPage.Visible)
                    {
                        resultWpf = owner != null ? WpfPage.ShowDialog(owner) : WpfPage.ShowDialog();
                    }
                    WpfPage.Activate();
                    return true;
                }
                return false;
            }

            private static void CloseIfExistsWpfPage()
            {
                if (WpfPage != null)
                {
                    WpfPage.Close();
                    WpfPage.Dispose();
                }
            }

            /// <summary>
            /// Show new form.
            /// </summary>
            /// <param name="owner"></param>
            /// <param name="caption"></param>
            /// <param name="message"></param>
            /// <param name="visibilitySettings"></param>
            private static DialogResult ShowNew(IWin32Window owner, string caption, string message, VisibilitySettingsEntity visibilitySettings)
            {
                CloseIfExistsWpfPage();

                WpfPage = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
                WpfPage.MessageBox.Caption = caption;
                WpfPage.MessageBox.Message = message;
                WpfPage.MessageBox.VisibilitySettings = visibilitySettings;
                WpfPage.MessageBox.VisibilitySettings.Localization();
                DialogResult resultWpf = owner != null ? WpfPage.ShowDialog(owner) : WpfPage.ShowDialog();
                WpfPage.Close();
                WpfPage.Dispose();
                return resultWpf;
            }

            public static DialogResult ShowNewRegistration(string message)
            {
                CloseIfExistsWpfPage();

                WpfPage = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
                WpfPage.MessageBox.Caption = LocaleCore.Scales.Registration;
                WpfPage.MessageBox.Message = message;
                WpfPage.MessageBox.VisibilitySettings.ButtonOkVisibility = Visibility.Visible;
                WpfPage.MessageBox.VisibilitySettings.Localization();
                WpfPage.ShowDialog();
                DialogResult result = WpfPage.MessageBox.Result;
                WpfPage.Close();
                WpfPage.Dispose();
                return result;
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
            /// <param name="isLog"></param>
            /// <param name="logType"></param>
            /// <param name="visibility"></param>
            /// <returns></returns>
            public static DialogResult ShowNewOperationControl(IWin32Window owner, string message, bool isLog, LogType logType,
                VisibilitySettingsEntity visibility = null, string hostName = "", string appName = "",
                [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
            {
                if (isLog)
                    DataAccess.Log.Log(message, logType, hostName, appName, filePath, lineNumber, memberName);
                return ShowNew(owner, LocaleCore.Scales.OperationControl, message,
                    visibility ?? new() { ButtonOkVisibility = Visibility.Visible });
            }

            public static Guid ShowNewHostSaveInFile()
            {
                CloseIfExistsWpfPage();

                Guid uid = Guid.NewGuid();
                XDocument doc = new();
                XElement root = new("root");
                root.Add(new XElement("ID", uid));
                // new XElement("EncryptConnectionString", new XCData(EncryptDecryptUtils.Encrypt(connectionString)))
                doc.Add(root);

                DialogResult resultWpf = new();
                WpfPage = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
                WpfPage.MessageBox.Caption = LocaleCore.Scales.Registration;
                WpfPage.MessageBox.Message = LocaleCore.Scales.HostUidNotFound + Environment.NewLine + LocaleCore.Scales.HostUidQuestionWriteToFile;
                WpfPage.MessageBox.VisibilitySettings.ButtonYesVisibility = Visibility.Visible;
                WpfPage.MessageBox.VisibilitySettings.ButtonNoVisibility = Visibility.Visible;
                WpfPage.MessageBox.VisibilitySettings.Localization();
                WpfPage.ShowDialog();
                if (resultWpf == DialogResult.Yes)
                {
                    doc.Save(SqlUtils.FilePathToken);
                }
                WpfPage.Close();
                WpfPage.Dispose();
                return uid;
            }

            public static DialogResult ShowNewHostSaveInDb(Guid uid)
            {
                DialogResult result = ShowNewOperationControl(null,
                    LocaleCore.Scales.HostUidNotFound + Environment.NewLine + LocaleCore.Scales.HostUidQuestionWriteToDb,
                    false, LogType.Information,
                    new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible });
                if (result == DialogResult.Yes)
                {
                    SqlUtils.SqlConnect.ExecuteNonQuery(SqlQueries.DbScales.Tables.Hosts.InsertNew,
                       new SqlParameter[]
                       {
                        new SqlParameter("@uid", System.Data.SqlDbType.UniqueIdentifier) { Value = uid.ToString() },
                        new SqlParameter("@name", System.Data.SqlDbType.NVarChar, 150) { Value = Environment.MachineName },
                        new SqlParameter("@ip", System.Data.SqlDbType.VarChar, 15) { Value = NetUtils.GetLocalIpAddress() },
                        new SqlParameter("@mac", System.Data.SqlDbType.VarChar, 35) { Value = NetUtils.GetLocalMacAddress() },
                       }
                   );
                }
                return result;
            }

            public static DialogResult ShowNewHostSaveInDb(string hostName, string ip, string mac)
            {
                DialogResult result = ShowNewOperationControl(null,
                    LocaleCore.Scales.HostNotFound(hostName) + Environment.NewLine + LocaleCore.Scales.QuestionWriteToDb,
                    false, LogType.Information,
                    new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible });
                if (result == DialogResult.Yes)
                {
                    HostEntity host = new()
                    {
                        Name = hostName,
                        HostName = hostName,
                        Ip = ip,
                        MacAddress = new(mac),
                        IdRRef = Guid.NewGuid(),
                        CreateDt = DateTime.Now,
                        ChangeDt = DateTime.Now,
                        AccessDt = DateTime.Now,
                        IsMarked = false,
                    };
                    DataAccess.Crud.SaveEntity(host);
                }
                return result;
            }

            /// <summary>
            /// Show new host not found.
            /// </summary>
            /// <param name="uid"></param>
            /// <returns></returns>
            public static DialogResult ShowNewHostNotFound(Guid uid)
            {
                CloseIfExistsWpfPage();

                WpfPage = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
                WpfPage.MessageBox.Caption = LocaleCore.Scales.Registration;
                WpfPage.MessageBox.Message = LocaleCore.Scales.RegistrationWarning(uid);
                WpfPage.MessageBox.VisibilitySettings.ButtonOkVisibility = Visibility.Visible;
                WpfPage.MessageBox.VisibilitySettings.Localization();
                WpfPage.ShowDialog();
                DialogResult result = WpfPage.MessageBox.Result;
                WpfPage.Close();
                WpfPage.Dispose();
                return result;
            }

            /// <summary>
            /// Show catch exception window.
            /// </summary>
            /// <param name="owner"></param>
            /// <param name="ex"></param>
            /// <param name="isLog"></param>
            /// <param name="filePath"></param>
            /// <param name="lineNumber"></param>
            /// <param name="memberName"></param>
            /// <returns></returns>
            public static DialogResult CatchException(IWin32Window owner, Exception ex, bool isLog = true, bool isShowWindow = true,
                [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
            {
                if (isLog)
                    DataAccess.Log.LogError(ex, UserSessionHelper.Instance.Scale.Host.HostName, null, filePath, lineNumber, memberName);
                string message = ex.Message;
                if (ex.InnerException != null)
                    message += ex.InnerException.Message;
                if (isShowWindow)
                    return ShowNew(owner, LocaleCore.Scales.Exception,
                        $"{LocaleCore.Scales.Method}: {memberName}." + Environment.NewLine +
                        $"{LocaleCore.Scales.Line}: {lineNumber}." + Environment.NewLine + message,
                        new() { ButtonOkVisibility = Visibility.Visible });
                return DialogResult.OK;
            }

            /// <summary>
            /// Show catch exception window.
            /// </summary>
            /// <param name="owner"></param>
            /// <param name="ex"></param>
            /// <param name="isLog"></param>
            /// <param name="filePath"></param>
            /// <param name="lineNumber"></param>
            /// <param name="memberName"></param>
            /// <returns></returns>
            public static DialogResult CatchException(Exception ex,
                [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
            {
                return CatchException(null, ex, true, true, filePath, lineNumber, memberName);
            }
        }

        public static void Dispose()
        {
            WpfForm.WpfPage?.Close();
            WpfForm.WpfPage?.Dispose();
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
            public static Button NewTableLayoutPanelButton(TableLayoutPanel tableLayoutPanel, string name, int column, int row = -1)
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
                tableLayoutPanel.Controls.Add(button, column - 1, row >= 0 ? row : 0);
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
                float size = (float)100 / tableLayoutPanel.ColumnCount;
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
