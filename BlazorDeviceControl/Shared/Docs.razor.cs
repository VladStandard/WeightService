// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using BlazorCore.Models;
using BlazorCore.Utils;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared
{
    public partial class Docs
    {
        #region Public and private fields and properties

        public string ProgramVer => $@"{LocalizationStrings.Share.ProgramVer}: {LocalizationStrings.GetAppVersion(System.Reflection.Assembly.GetExecutingAssembly())}";
        public string HostName => $@"{LocalizationStrings.Share.HostName}: {Environment.MachineName}";
        public string SqlServer => $@"{LocalizationStrings.Share.SqlServer}: {AppSettings.JsonAppSettings.Server}";
        public string SqlDb => $@"{LocalizationStrings.Share.SqlDb}: {AppSettings.JsonAppSettings.Db}";
        public string SqlUser => AppSettings.JsonAppSettings.Trusted ? "windows-account" : $"sql-account: {AppSettings.JsonAppSettings.Username}";
        public string IsDebug => $@"{LocalizationStrings.Share.IsEnableHe(AppSettings.IsDebug)}";
        public List<TypeEntity<string>> TemplateLanguages { get; set; }
        public List<TypeEntity<bool>> TemplateIsDebug { get; set; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

            await GetDataAsync(new Task(delegate
            {
                TemplateLanguages = LocalizationStrings.Lang switch
                {
                    EnumLang.Eng => AppSettings.DataSource.GetTemplateLanguagesEng(),
                    EnumLang.Rus => AppSettings.DataSource.GetTemplateLanguagesRus(),
                    _ => new List<TypeEntity<string>>()
                };
                TemplateIsDebug = AppSettings.DataSource.GetTemplateIsDebug();
            }), false).ConfigureAwait(false);
        }

        private async Task OnChange(object value, string name)
        {
            switch (name)
            {
                case "TemlateCategories":
                    if (value is bool isDebug)
                    {
                        AppSettings.JsonAppSettings.IsDebug = isDebug;
                        Console.WriteLine($"OnChange: {isDebug}");
                    }
                    break;
            }
            await GuiRefreshAsync().ConfigureAwait(true);
        }

        #endregion
    }
}