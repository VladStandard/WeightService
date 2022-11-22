// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Files;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Sql.Core;
using DataCore.Sql.TableScaleModels;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using WeightCore.Helpers;

namespace WeightCore.Gui;

/// <summary>
/// GUI utils.
/// </summary>
[SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract")]
public static partial class GuiUtils
{
    /// <summary>
    /// Show WPF form inside WinForm.
    /// </summary>
    public static class WpfForm
    {
        #region Public and private fields, properties, constructor

        private static DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
        public static WpfPageLoader WpfPage { get; private set; }
        private static FileLoggerHelper FileLogger { get; } = FileLoggerHelper.Instance;

        #endregion

        #region Public and private methods

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
            if (WpfPage is not null)
            {
                if (!WpfPage.Visible)
                {
                    resultWpf = owner is not null ? WpfPage.ShowDialog(owner) : WpfPage.ShowDialog();
                }
                WpfPage.Activate();
                return true;
            }
            return false;
        }

        public static void Dispose()
        {
            if (WpfPage is not null)
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
        private static DialogResult ShowNew(IWin32Window owner, string caption, string message,
            VisibilitySettingsModel visibilitySettings)
        {
            Dispose();

            WpfPage = new(PageEnum.MessageBox, false) { Width = 700, Height = 400 };
            WpfPage.MessageBox.Caption = caption;
            WpfPage.MessageBox.Message = message;
            WpfPage.MessageBox.VisibilitySettings = visibilitySettings;
            WpfPage.MessageBox.VisibilitySettings.Localization();
            DialogResult resultWpf = owner is not null ? WpfPage.ShowDialog(owner) : WpfPage.ShowDialog();
            WpfPage.Close();
            WpfPage.Dispose();
            return resultWpf;
        }

        public static DialogResult ShowNewRegistration(string message)
        {
            Dispose();

            WpfPage = new(PageEnum.MessageBox, false) { Width = 700, Height = 400 };
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
        /// <param name="deviceName"></param>
        /// <param name="appName"></param>
        /// <returns></returns>
        public static DialogResult ShowNewOperationControl(IWin32Window owner, string message, bool isLog,
            LogTypeEnum logType, VisibilitySettingsModel visibility, string deviceName, string appName)
        {
            if (isLog)
                ShowNewOperationControlLogType(message, logType, deviceName, appName);
            return ShowNew(owner, LocaleCore.Scales.OperationControl, message, visibility);
        }

        public static DialogResult ShowNewOperationControl(string message, bool isLog,
            LogTypeEnum logType, VisibilitySettingsModel visibility, string deviceName, string appName)
        {
            if (isLog)
                ShowNewOperationControlLogType(message, logType, deviceName, appName);
            return ShowNew(null, LocaleCore.Scales.OperationControl, message, visibility);
        }

        private static void ShowNewOperationControlLogType(string message, LogTypeEnum logType, string deviceName,
            string appName)
        {
            switch (logType)
            {
                case LogTypeEnum.None:
                    DataAccess.LogInformation(message, deviceName, appName);
                    break;
                case LogTypeEnum.Error:
                    DataAccess.LogError(message, deviceName, appName);
                    break;
                case LogTypeEnum.Question:
                    DataAccess.LogQuestion(message, deviceName, appName);
                    break;
                case LogTypeEnum.Warning:
                    DataAccess.LogWarning(message, deviceName, appName);
                    break;
                case LogTypeEnum.Information:
                    DataAccess.LogInformation(message, deviceName, appName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logType), logType, null);
            }
        }

        public static DeviceModel SetNewDeviceWithQuestion(string deviceName, string ip, string mac)
        {
            DeviceModel device = DataAccess.GetItemDeviceNotNullable(deviceName);
            if (device.IdentityIsNew)
            {
                DialogResult result = ShowNewOperationControl(
                    LocaleCore.Scales.HostNotFound(deviceName) + Environment.NewLine + LocaleCore.Scales.QuestionWriteToDb,
                    false, LogTypeEnum.Information,
                    new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible },
                    UserSessionHelper.Instance.DeviceScaleFk.Device.Name, nameof(WeightCore));
                if (result == DialogResult.Yes)
                {
                    device = new()
                    {
                        Name = deviceName,
                        PrettyName = deviceName,
                        Ipv4 = ip,
                        MacAddress = new(mac),
                        CreateDt = DateTime.Now,
                        ChangeDt = DateTime.Now,
                        LoginDt = DateTime.Now,
                        IsMarked = false,
                    };
                    DataAccess.Save(device);
                }
            }
            else {
                device.Ipv4 = ip;
                device.MacAddress = new(mac);
                device.ChangeDt = DateTime.Now;
                device.LoginDt = DateTime.Now;
                device.IsMarked = false;
                DataAccess.Update(device);
            }
            return device;
        }

        /// <summary>
        /// Show new host not found.
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static DialogResult ShowNewHostNotFound(Guid uid)
        {
            Dispose();

            WpfPage = new(PageEnum.MessageBox, false) { Width = 700, Height = 400 };
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

        public static DialogResult CatchExceptionCore(Exception ex, IWin32Window owner, 
            bool isFileLog, bool isDbLog, bool isShowWindow, 
            string filePath, int lineNumber, string memberName)
        {
            if (isFileLog)
                FileLogger.StoreExceptionWithParams(ex, filePath, lineNumber, memberName);

            if (isDbLog)
                DataAccess.LogError(ex, UserSessionHelper.Instance.DeviceScaleFk.Device.Name, null, filePath, lineNumber, memberName);


            if (isShowWindow)
            {
                string message = ex.InnerException is null ? ex.Message : ex.Message + Environment.NewLine + ex.InnerException.Message;
                return ShowNew(owner, LocaleCore.Scales.Exception,
                $"{LocaleCore.Scales.Method}: {memberName}." + Environment.NewLine +
                $"{LocaleCore.Scales.Line}: {lineNumber}." + Environment.NewLine + message,
                new() { ButtonOkVisibility = Visibility.Visible });
            }
            
            return DialogResult.OK;
        }

        /// <summary>
        /// Show catch exception window..
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="owner"></param>
        /// <param name="isFileLog"></param>
        /// <param name="isDbLog"></param>
        /// <param name="isShowWindow"></param>
        /// <param name="filePath"></param>
        /// <param name="lineNumber"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public static DialogResult CatchException(Exception ex, IWin32Window owner,
            bool isFileLog = false, bool isDbLog = false, bool isShowWindow = false,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
            CatchExceptionCore(ex, owner, isFileLog, isDbLog, isShowWindow, filePath, lineNumber, memberName);

        /// <summary>
        /// Show catch exception window.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="isFileLog"></param>
        /// <param name="isDbLog"></param>
        /// <param name="isShowWindow"></param>
        /// <param name="filePath"></param>
        /// <param name="lineNumber"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public static DialogResult CatchException(Exception ex,
            bool isFileLog = false, bool isDbLog = false, bool isShowWindow = false,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
            CatchExceptionCore(ex, null, isFileLog, isDbLog, isShowWindow, filePath, lineNumber, memberName);

        #endregion
    }
}
