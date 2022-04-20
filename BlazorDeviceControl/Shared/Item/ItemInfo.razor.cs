// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using DataCore;
using DataCore.DAL;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Utils;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class ItemInfo
    {
        #region Public and private fields and properties

        public string VerApp => AssemblyUtuls.GetAppVersion(System.Reflection.Assembly.GetExecutingAssembly());
        public string VerLibBlazorCore => BlazorCoreUtuls.GetLibVersion();
        public string VerLibDataCore => AssemblyUtuls.GetLibVersion();
        public List<TypeEntity<ShareEnums.Lang>>? TemplateLanguages { get; set; }
        public List<TypeEntity<bool>> TemplateIsDebug { get; set; } = new();
        private uint DbCurSize { get; set; } = 0;
        private string DbCurSizeAsString => $"{DbCurSize:### ###} {LocaleCore.Strings.Main.From} {DbMaxSize:### ###} MB";
        private uint DbMaxSize { get; set; } = 10_240;
        private uint DbFillSize => DbCurSize == 0 ? 0 : DbCurSize * 100 / DbMaxSize;
        private string DbFillSizeAsString => $"{DbFillSize:### ###} %";

        #endregion

        #region Constructor and destructor

        public ItemInfo() : base()
        {
            //
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            IsLoaded = false;
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocaleCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocaleCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    Default();
                    await GuiRefreshWithWaitAsync();

                    TemplateLanguages = AppSettings.DataSourceDics.GetTemplateLanguages();
                    TemplateIsDebug = DataSourceDicsEntity.GetTemplateIsDebug();
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
                    IsLoaded = true;
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
