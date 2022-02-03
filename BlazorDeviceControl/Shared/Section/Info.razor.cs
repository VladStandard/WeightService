// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class Info
    {
        #region Public and private fields and properties

        public string ProgramVer => LocalizationCore.Methods.GetAppVersion(System.Reflection.Assembly.GetExecutingAssembly());
        public string CoreVer => LocalizationCore.Methods.GetCoreVersion();
        public string IsDebug => $@"{LocalizationCore.Strings.Main.IsEnableHe(AppSettings.IsDebug)}";
        public List<TypeEntity<ShareEnums.Lang>> TemplateLanguages { get; set; }
        public List<TypeEntity<bool>> TemplateIsDebug { get; set; }
        private uint DbCurSize { get; set; } = 0;
        private string DbCurSizeAsString => $"{DbCurSize:### ###} MB";
        private uint DbMaxSize { get; set; } = 10240;
        private string DbMaxSizeAsString => $"{DbMaxSize:### ###} MB";
        private uint DbFillSize => DbCurSize == 0 ? 0 : DbCurSize * 100 / DbMaxSize;
        private string DbFillSizeAsString => $"{DbFillSize:### ###} %";

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    lock (Locker)
                    {
                        TemplateLanguages = AppSettings.DataSource.GetTemplateLanguages();
                        TemplateIsDebug = AppSettings.DataSource.GetTemplateIsDebug();
                        object[] objects = AppSettings.DataAccess.Crud.GetEntitiesNativeObject(SqlQueries.DbSystem.Properties.GetDbSpace);
                        DbCurSize = 0;
                        foreach (object obj in objects)
                        {
                            if (obj is object[] { Length: 5 } item)
                            {
                                if (uint.TryParse(Convert.ToString(item[2]), out uint dbSizeMb))
                                {
                                    DbCurSize += dbSizeMb;
                                }
                            }
                        }
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
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
                    if (value is ShareEnums.Lang lang)
                    {
                        LocalizationCore.Lang = lang;
                        LocalizationData.Lang = lang;
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
            }
        }

        #endregion
    }
}
