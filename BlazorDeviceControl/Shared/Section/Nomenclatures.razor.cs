// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using DataCore;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class Nomenclatures
    {
        #region Public and private fields and properties

        private List<NomenclatureEntity> ItemsCast => Items == null ? new List<NomenclatureEntity>() : Items.Select(x => (NomenclatureEntity)x).ToList();
        private readonly object _locker = new();

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    lock (_locker)
                    {
                        Table = new TableScaleEntity(ProjectsEnums.TableScale.Nomenclatures);
                        Items = AppSettings.DataAccess.Crud.GetEntities<NomenclatureEntity>(null,
                            new FieldOrderEntity(ShareEnums.DbField.Name, ShareEnums.DbOrderDirection.Asc))
                            .ToList<BaseEntity>();
                        ButtonSettings = new ButtonSettingsEntity(true, true, true, true, true, false, false);
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
