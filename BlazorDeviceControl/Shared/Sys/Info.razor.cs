// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using BlazorCore.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Sys
{
    public partial class Info
    {
        #region Public and private fields and properties

        public string ProgramVer => LocalizationCore.Methods.GetAppVersion(System.Reflection.Assembly.GetExecutingAssembly());
        public string CoreVer => LocalizationCore.Methods.GetCoreVersion();
        public string IsDebug => $@"{LocalizationCore.Strings.IsEnableHe(AppSettings.IsDebug)}";
        public List<TypeEntity<EnumLang>> TemplateLanguages { get; set; }
        public List<TypeEntity<bool>> TemplateIsDebug { get; set; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
            new List<Task> {
                new(async() => {
                    TemplateLanguages = AppSettings.DataSource.GetTemplateLanguages();
                    TemplateIsDebug = AppSettings.DataSource.GetTemplateIsDebug();
                    await GuiRefreshWithWaitAsync();
                }),
            }, true);
        }

        private void OnChange(object value, string name)
        {
            switch (name)
            {
                case nameof(AppSettings.JsonAppSettings.Server):
                    if (value is string server)
                    {
                        AppSettings.JsonAppSettings.Server = server;
                    }
                    break;
                case nameof(AppSettings.JsonAppSettings.Db):
                    if (value is string db)
                    {
                        AppSettings.JsonAppSettings.Db = db;
                    }
                    break;
                case nameof(AppSettings.JsonAppSettings.Trusted):
                    if (value is bool trusted)
                    {
                        AppSettings.JsonAppSettings.Trusted = trusted;
                    }
                    break;
                case nameof(AppSettings.JsonAppSettings.Username):
                    if (value is string username)
                    {
                        AppSettings.JsonAppSettings.Username = username;
                    }
                    break;
                case nameof(AppSettings.JsonAppSettings.Password):
                    if (value is string password)
                    {
                        AppSettings.JsonAppSettings.Password = password;
                    }
                    break;
                case nameof(TemplateLanguages):
                    if (value is EnumLang lang)
                    {
                        LocalizationCore.Lang = lang;
                    }
                    TemplateLanguages = AppSettings.DataSource.GetTemplateLanguages();
                    break;
                case nameof(TemplateIsDebug):
                    if (value is bool isDebug)
                    {
                        AppSettings.JsonAppSettings.IsDebug = isDebug;
                    }
                    break;
                case nameof(AppSettings.JsonAppSettings.SectionRowCount):
                    if (value is int sectionRowCount)
                    {
                        AppSettings.JsonAppSettings.SectionRowCount = sectionRowCount;
                    }
                    break;
                case nameof(AppSettings.JsonAppSettings.ItemRowCount):
                    if (value is int itemRowCount)
                    {
                        AppSettings.JsonAppSettings.ItemRowCount = itemRowCount;
                    }
                    break;
                case nameof(AppSettings.JsonAppSettings.ButtonWidth):
                    if (value is string buttonWidth)
                    {
                        AppSettings.JsonAppSettings.ButtonWidth = buttonWidth;
                    }
                    break;
                case nameof(AppSettings.JsonAppSettings.ButtonHeight):
                    if (value is string buttonHeight)
                    {
                        AppSettings.JsonAppSettings.ButtonHeight = buttonHeight;
                    }
                    break;
            }
        }

        #endregion
    }
}