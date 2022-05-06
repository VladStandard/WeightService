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
using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;
using DataCore.Protocols;
using System.Xml.Linq;
using DataCore.Sql.TableScaleModels;

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
                return resultWpf;
            }

            public static DialogResult ShowNewRegistration(string message)
            {
                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
                wpfPageLoader.MessageBox.Caption = LocaleCore.Scales.Registration;
                wpfPageLoader.MessageBox.Message = message;
                wpfPageLoader.MessageBox.VisibilitySettings.ButtonOkVisibility = Visibility.Visible;
                wpfPageLoader.MessageBox.VisibilitySettings.Localization();
                wpfPageLoader.ShowDialog();
                DialogResult result = wpfPageLoader.MessageBox.Result;
                wpfPageLoader.Close();
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
            public static DialogResult ShowNewOperationControl(IWin32Window owner, string message, bool isLog, VisibilitySettingsEntity visibility = null)
            {
                if (isLog)
                    DataAccess.Log.LogInformation(message);
                return ShowNew(owner, LocaleCore.Scales.OperationControl, message,
                    visibility ?? new() { ButtonOkVisibility = Visibility.Visible });
            }

            public static Guid ShowNewHostSaveInFile()
            {
                Guid uid = Guid.NewGuid();
                XDocument doc = new();
                XElement root = new("root");
                root.Add(new XElement("ID", uid));
                // new XElement("EncryptConnectionString", new XCData(EncryptDecryptUtils.Encrypt(connectionString)))
                doc.Add(root);

                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
                wpfPageLoader.MessageBox.Caption = LocaleCore.Scales.Registration;
                wpfPageLoader.MessageBox.Message = LocaleCore.Scales.HostUidNotFound + Environment.NewLine + LocaleCore.Scales.HostUidQuestionWriteToFile;
                wpfPageLoader.MessageBox.VisibilitySettings.ButtonYesVisibility = Visibility.Visible;
                wpfPageLoader.MessageBox.VisibilitySettings.ButtonNoVisibility = Visibility.Visible;
                wpfPageLoader.MessageBox.VisibilitySettings.Localization();
                wpfPageLoader.ShowDialog();
                DialogResult result = wpfPageLoader.MessageBox.Result;
                if (result == DialogResult.Yes)
                {
                    doc.Save(DataCore.Sql.Controllers.HostsUtils.FilePathToken);
                }
                wpfPageLoader.Close();
                return uid;
            }
            
            public static DialogResult ShowNewHostSaveInDb(Guid uid)
            {
                DialogResult result = ShowNewOperationControl(null,
                    LocaleCore.Scales.HostUidNotFound + Environment.NewLine + LocaleCore.Scales.HostUidQuestionWriteToDb,
                    false, new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible });
                if (result == DialogResult.Yes)
                {
                    DataCore.Sql.Controllers.HostsUtils.SqlConnect.ExecuteNonQuery(SqlQueries.DbScales.Tables.Hosts.InsertNew,
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
                    false, new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible });
                if (result == DialogResult.Yes)
                {
                    HostEntity host = new() {
                        Name = hostName,
                        HostName = hostName,
                        Ip = ip,
                        MacAddress = new(mac),
                        IdRRef = Guid.NewGuid(),
                        CreateDt = DateTime.Now,
                        ChangeDt = DateTime.Now,
                        AccessDt = DateTime.Now,
                        IsMarked = false,
                        SettingsFile = string.Empty,
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
                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
                wpfPageLoader.MessageBox.Caption = LocaleCore.Scales.Registration;
                wpfPageLoader.MessageBox.Message = LocaleCore.Scales.RegistrationWarning(uid);
                wpfPageLoader.MessageBox.VisibilitySettings.ButtonOkVisibility = Visibility.Visible;
                wpfPageLoader.MessageBox.VisibilitySettings.Localization();
                wpfPageLoader.ShowDialog();
                DialogResult result = wpfPageLoader.MessageBox.Result;
                wpfPageLoader.Close();
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
                    DataAccess.Log.LogError(ex, null, null, filePath, lineNumber, memberName);
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
                float size = (float) 100 / tableLayoutPanel.ColumnCount;
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
