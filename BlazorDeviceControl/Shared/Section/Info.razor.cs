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
        private string DbCurSizeAsString => $"{DbCurSize:### ###} {LocalizationCore.Strings.Main.From} {DbMaxSize:### ###} MB";
        private uint DbMaxSize { get; set; } = 10240;
        private float DbFillSize => DbCurSize == 0 ? 0 : DbCurSize * 100 / DbMaxSize;
        //private float DbFillSize => DbCurSize == 0 ? 0 : (float)Convert.ToDecimal($"{(decimal)(DbCurSize * 100) / DbMaxSize:0.00}");
        //private float DbFillSize => DbCurSize == 0 ? 0 : (float)Math.Floor((double)(DbCurSize * 100) / DbMaxSize);
        //private float DbFillSize => DbCurSize == 0 ? 0 : (float)Math.Floor((double)(DbCurSize * 100) / DbMaxSize * Math.Pow(10, 2) / Math.Pow(10, 2));
        private string DbFillSizeAsString => $"{DbFillSize:### ###} %";

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    TemplateLanguages = AppSettings.DataReference.GetTemplateLanguages();
                    TemplateIsDebug = AppSettings.DataReference.GetTemplateIsDebug();
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
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        private void OnChange(object value, string name)
        {
            switch (name)
            {
                case nameof(AppSettings.DataAccess.JsonSettings.Server):
                    if (value is string server)
                    {
                        AppSettings.DataAccess.JsonSettings.Server = server;
                    }
                    break;
                case nameof(AppSettings.DataAccess.JsonSettings.Db):
                    if (value is string db)
                    {
                        AppSettings.DataAccess.JsonSettings.Db = db;
                    }
                    break;
                case nameof(AppSettings.DataAccess.JsonSettings.Trusted):
                    if (value is bool trusted)
                    {
                        AppSettings.DataAccess.JsonSettings.Trusted = trusted;
                    }
                    break;
                case nameof(AppSettings.DataAccess.JsonSettings.Username):
                    if (value is string username)
                    {
                        AppSettings.DataAccess.JsonSettings.Username = username;
                    }
                    break;
                case nameof(AppSettings.DataAccess.JsonSettings.Password):
                    if (value is string password)
                    {
                        AppSettings.DataAccess.JsonSettings.Password = password;
                    }
                    break;
                case nameof(TemplateLanguages):
                    if (value is ShareEnums.Lang lang)
                    {
                        LocalizationCore.Lang = lang;
                        LocalizationData.Lang = lang;
                    }
                    TemplateLanguages = AppSettings.DataReference.GetTemplateLanguages();
                    break;
                case nameof(TemplateIsDebug):
                    if (value is bool isDebug)
                    {
                        AppSettings.DataAccess.JsonSettings.IsDebug = isDebug;
                    }
                    break;
                case nameof(AppSettings.DataAccess.JsonSettings.SectionRowCount):
                    if (value is int sectionRowCount)
                    {
                        AppSettings.DataAccess.JsonSettings.SectionRowCount = sectionRowCount;
                    }
                    break;
                case nameof(AppSettings.DataAccess.JsonSettings.ItemRowCount):
                    if (value is int itemRowCount)
                    {
                        AppSettings.DataAccess.JsonSettings.ItemRowCount = itemRowCount;
                    }
                    break;
            }
        }

        #endregion
    }
}
