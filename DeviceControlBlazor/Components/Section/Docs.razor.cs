// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControlBlazor.Utils;
using System;

namespace DeviceControlBlazor.Components.Section
{
    public partial class Docs
    {
        #region Public and private fields and properties

        private string SqlUser => AppSettings.JsonAppSettings.Trusted ? "windows-account" : $"sql-account: {AppSettings.JsonAppSettings.Username}";
        public string Doc => LocalizationStrings.DocText + Environment.NewLine + Environment.NewLine +
                             $@"SQL-Server: {AppSettings.JsonAppSettings.Server}" + Environment.NewLine +
                             $@"SQL-DB: {AppSettings.JsonAppSettings.Db}" + Environment.NewLine +
                             $@"{SqlUser}" + Environment.NewLine;

        #endregion

        #region Public and private methods

        #endregion
    }
}