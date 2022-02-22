// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using DataCore;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class Workshop
    {
        #region Public and private fields and properties

        public WorkshopEntity WorkshopItem { get => (WorkshopEntity)Item; set => Item = value; }
        public List<ProductionFacilityEntity> ProductionFacilities { get; set; } = null;
        private readonly object _locker = new();

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async() => {
                    lock (_locker)
                    {
                        Table = new TableScaleEntity(ProjectsEnums.TableScale.Workshops);
                        WorkshopItem = AppSettings.DataAccess.Crud.GetEntity<WorkshopEntity>(new FieldListEntity(new Dictionary<string, object>
                            { { ShareEnums.DbField.Id.ToString(), Id } }), null);
                        if (Id != null && TableAction == ShareEnums.DbTableAction.New)
                            WorkshopItem.Id = (long)Id;
                        ProductionFacilities = AppSettings.DataAccess.Crud.GetEntities<ProductionFacilityEntity>(null, null).ToList();
                        ButtonSettings = new ButtonSettingsEntity(false, false, false, false, false, true, true);
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
