// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class Plus
    {
        #region Public and private fields and properties

        [Parameter] public long? ScaleId { get; set; }
        private List<PluEntity>? ItemsCast => Items?.Select(x => (PluEntity)x).ToList();
        private readonly object _locker = new();

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    Table = new TableScaleEntity(ProjectsEnums.TableScale.Plus);
                    IsShowMarkedFilter = true;

                    lock (_locker)
                    {
                        Items = null;
                        ButtonSettings = new();
                    }
                    await GuiRefreshWithWaitAsync();

                    lock (_locker)
                    {
                        if (AppSettings.DataAccess != null)
                        {
                            Items = !IsShowMarkedItems
                                ? AppSettings.DataAccess.Crud.GetEntities<PluEntity>(ScaleId != null
                                    ? new FieldListEntity(new Dictionary<string, object?> { { "Scale.Id", ScaleId }, { ShareEnums.DbField.Marked.ToString(), false } })
                                    : new FieldListEntity(new Dictionary<string, object?> { { ShareEnums.DbField.Marked.ToString(), false } }),
                                        new FieldOrderEntity(ShareEnums.DbField.GoodsName, ShareEnums.DbOrderDirection.Asc))?
                                    .ToList<BaseEntity>()
                                : AppSettings.DataAccess.Crud.GetEntities<PluEntity>(ScaleId != null
                                    ? new FieldListEntity(new Dictionary<string, object?> { { "Scale.Id", ScaleId } })
                                    : new FieldListEntity(new Dictionary<string, object?> { { ShareEnums.DbField.Marked.ToString(), false } }),
                                        new FieldOrderEntity(ShareEnums.DbField.GoodsName, ShareEnums.DbOrderDirection.Asc))?
                                    .ToList<BaseEntity>()
                                ;
                        }
                        ButtonSettings = new(true, true, true, true, true, false, false);
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
