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
    public partial class Info
    {
        #region Public and private fields and properties

        public string ProgramVer => $@"{LocalizationStrings.GetAppVersion(System.Reflection.Assembly.GetExecutingAssembly())}";
        public string IsDebug => $@"{LocalizationStrings.Share.IsEnableHe(AppSettings.IsDebug)}";
        public List<TypeEntity<EnumLang>> TemplateLanguages { get; set; }
        public List<TypeEntity<bool>> TemplateIsDebug { get; set; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

            RunTasks(LocalizationStrings.DeviceControl.MethodOnInitializedAsync, "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> {
                    new(() => {
                        TemplateLanguages = AppSettings.DataSource.GetTemplateLanguages();
                        TemplateIsDebug = AppSettings.DataSource.GetTemplateIsDebug();
                    }),
                }, GuiRefreshAsync, true);
        }

        private async Task OnChange(object value, string name)
        {
            switch (name)
            {
                case nameof(TemplateLanguages):
                    if (value is EnumLang lang)
                    {
                        LocalizationStrings.Lang = lang;
                    }
                    break;
                case nameof(TemplateIsDebug):
                    if (value is bool isDebug)
                    {
                        AppSettings.JsonAppSettings.IsDebug = isDebug;
                    }
                    break;
            }
            //await SetParametersAsync(new ParameterView()).ConfigureAwait(true);
            TemplateLanguages = AppSettings.DataSource.GetTemplateLanguages();
            await GuiRefreshAsync().ConfigureAwait(true);
        }

        #endregion
    }
}