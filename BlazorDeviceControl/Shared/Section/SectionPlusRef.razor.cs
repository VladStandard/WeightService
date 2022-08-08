// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Section;

public partial class SectionPlusRef
{
    #region Public and private fields, properties, constructor

    private List<PluRefV2Entity> ItemsCast => Items == null ? new() : Items.Select(x => (PluRefV2Entity)x).ToList();

    #endregion

    #region Public and private methods

    private void Default()
    {
        IsLoaded = false;
        Table = new TableScaleEntity(ProjectsEnums.TableScale.PluRefs);
        IsShowMarkedFilter = true;
        Items = new();
        ButtonSettings = new();
    }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters).ConfigureAwait(true);
        RunTasks($"{LocaleCore.Action.ActionMethod} {nameof(SetParametersAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
            new Task(async () =>
            {
                Default();
                await GuiRefreshWithWaitAsync();

                long? scaleId = null;
                if (ItemFilter is ScaleEntity scale)
                    scaleId = scale.IdentityId;
                if (IsShowMarkedItems)
                {
                    if (scaleId == null)
                        Items = AppSettings.DataAccess.Crud.GetEntities<PluRefV2Entity>(null,
							null, //new($"{nameof(PluRefV2Entity.Plu)}.{DbField.IdentityId}"),
                            IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                            ?.ToList<BaseEntity>();
                    else
                    {
                        Items = AppSettings.DataAccess.Crud.GetEntities<PluRefV2Entity>(
                                new(new() { new($"{nameof(PluRefV2Entity.Scale)}.{DbField.IdentityId}", DbComparer.Equal, scaleId) }),
								null, //new($"{nameof(PluRefV2Entity.Plu)}.{DbField.IdentityId}"),
								IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                            ?.ToList<BaseEntity>();
                    }
                }
                else
                {
                    if (scaleId == null)
                    {
						Items = AppSettings.DataAccess.Crud.GetEntities<PluRefV2Entity>(
                            null,
							null, //new($"{nameof(PluRefV2Entity.Plu)}.{DbField.IdentityId}"),
							IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
							?.ToList<BaseEntity>();
                    }
                    else
                    {
                        Items = AppSettings.DataAccess.Crud.GetEntities<PluRefV2Entity>(
                            new(new() { new($"{nameof(PluRefV2Entity.Scale)}.{DbField.IdentityId}", DbComparer.Equal, scaleId)}),
							null, //new($"{nameof(PluRefV2Entity.Plu)}.{DbField.IdentityId}"),
							IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                            ?.ToList<BaseEntity>();
                    }
                }

                ButtonSettings = new(true, true, true, true, true, false, false);
                IsLoaded = true;
                await GuiRefreshWithWaitAsync();
            }), true);
    }

    private void SetFilterItems(List<BaseEntity>? items, long? scaleId)
    {
        if (items != null)
        {
            Items = new();
            foreach (BaseEntity item in items)
            {
                if (item is PluRefV2Entity plu && plu.Scale.IdentityId == scaleId)
                    Items.Add(item);
            }
        }
    }

    #endregion
}
