// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using BlazorCore.Utils;

namespace BlazorDeviceControl.Shared
{
    public partial class Docs
    {
        #region Public and private fields and properties

        private string SqlUser => AppSettings.JsonAppSettings.Trusted ? "windows-account" : $"sql-account: {AppSettings.JsonAppSettings.Username}";
        public string Doc => 
            @$"{LocalizationStrings.Share.ProgramVer}: {LocalizationStrings.GetAppVersion()}" + Environment.NewLine + 
            @$"{LocalizationStrings.Share.HostName}: {Environment.MachineName}" + Environment.NewLine + 
            @$"{LocalizationStrings.Share.Language}: {LocalizationStrings.Share.LanguageDetect}" + Environment.NewLine + 
            @$"{LocalizationStrings.Share.DebugMode}: {LocalizationStrings.Share.IsEnable(AppSettings.IsDebug)}" + Environment.NewLine + 
            Environment.NewLine +
            @$"{LocalizationStrings.Share.SqlServer}: {AppSettings.JsonAppSettings.Server}" + Environment.NewLine +
            @$"{LocalizationStrings.Share.SqlDb}: {AppSettings.JsonAppSettings.Db}" + Environment.NewLine +
            @$"{SqlUser}" + Environment.NewLine + Environment.NewLine +
            Environment.NewLine;

        #endregion

        #region Public and private methods

        #endregion
    }
}