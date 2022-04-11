// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using DataCore;
using DataCore.DAL;
using DataCore.Models;
using DataCore.Utils;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class SectionInfo
    {
        #region Public and private fields and properties

        public string VerApp => DataCoreUtuls.GetAppVersion(System.Reflection.Assembly.GetExecutingAssembly());
        public string VerLibBlazorCore => BlazorCoreUtuls.GetLibVersion();
        public string VerLibDataCore => DataCoreUtuls.GetLibVersion();
        public string IsDebug => $@"{LocalizationCore.Strings.Main.IsEnableHe(AppSettings.IsDebug)}";
        public List<TypeEntity<ShareEnums.Lang>>? TemplateLanguages { get; set; }
        public List<TypeEntity<bool>> TemplateIsDebug { get; set; } = new();
        private uint DbCurSize { get; set; } = 0;
        private string DbCurSizeAsString => $"{DbCurSize:### ###} {LocalizationCore.Strings.Main.From} {DbMaxSize:### ###} MB";
        private uint DbMaxSize { get; set; } = 10_240;
        private uint DbFillSize => DbCurSize == 0 ? 0 : DbCurSize * 100 / DbMaxSize;
        private string DbFillSizeAsString => $"{DbFillSize:### ###} %";

        #endregion

        #region Constructor and destructor

        public SectionInfo() : base()
        {
            //Default();
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                IsBusy = false;
            }
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    Default();
                    await GuiRefreshWithWaitAsync();

                    if (!IsBusy)
                    {
                        IsBusy = true;
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
                        IsBusy = false;
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
