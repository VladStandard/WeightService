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
    private ScaleEntity ItemFilterCast
	{
	    get => ItemFilter == null ? new() : (ScaleEntity)ItemFilter;
	    set => ItemFilter = value;
    }

	public SectionPlusRef()
	{
	    IsLoaded = false;
	    Table = new TableScaleEntity(ProjectsEnums.TableScale.PluRefs);
	    IsShowMarkedItems = true;
	    IsShowMarkedFilter = true;
	    IsShowAdditionalFilter = true;
		Items = new();
	    ButtonSettings = new();
	}

    #endregion

    #region Public and private methods

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters).ConfigureAwait(true);
        RunTasks($"{LocaleCore.Action.ActionMethod} {nameof(SetParametersAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
            new Task(() =>
            {
	            IsLoaded = false;
				GuiRefreshWithWaitAsync().ConfigureAwait(true);

                long scaleId = ItemFilterCast.IdentityId != 0 ? ItemFilterCast.IdentityId : 0;
                if (IsShowMarkedItems)
                {
                    Items = AppSettings.DataAccess.Crud.GetEntities<PluRefV2Entity>(
	                    scaleId == 0
		                    ? null
							: new(new()
		                    {
			                    new($"{nameof(PluRefV2Entity.Scale)}.{DbField.IdentityId}", DbComparer.Equal, scaleId),
		                    }),
	                    null, //new($"{nameof(PluRefV2Entity.Plu)}.{DbField.IdentityId}"),
						IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                        ?.ToList<BaseEntity>();
                }
                else
                {
					Items = AppSettings.DataAccess.Crud.GetEntities<PluRefV2Entity>(
						scaleId == 0 
							? new(new()
							{
								new(DbField.IsMarked, DbComparer.Equal, false)
							})
							: new(new()
							{
								new(DbField.IsMarked, DbComparer.Equal, false),
								new($"{nameof(PluRefV2Entity.Scale)}.{DbField.IdentityId}", DbComparer.Equal, scaleId),
							}),
						null, //new($"{nameof(PluRefV2Entity.Plu)}.{DbField.IdentityId}"),
						IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
						?.ToList<BaseEntity>();
                }

                ButtonSettings = new(true, true, true, true, true, false, false);
                IsLoaded = true;
				GuiRefreshWithWaitAsync().ConfigureAwait(true);
			}), true);
    }

    #endregion
}
