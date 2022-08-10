// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;
using Radzen;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Section;

public partial class SectionAccess
{
    #region Public and private fields, properties, constructor

    private List<AccessEntity> ItemsCast => Items == null ? new() : Items.Select(x => (AccessEntity)x).ToList();

    /// <summary>
    /// Constructor.
    /// </summary>
    public SectionAccess()
    {
        Table = new TableSystemEntity(ProjectsEnums.TableSystem.Accesses);
        Items = new();
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        SetParametersWithAction(new()
        {
	        () =>
	        {
		        Items = AppSettings.DataAccess.Crud.GetEntities<AccessEntity>(
				        (IsShowMarkedItems == true) ? null
					        : new FilterListEntity(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
				        new(DbField.User, DbOrderDirection.Asc),
				        IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
			        ?.ToList<BaseEntity>();
		        ButtonSettings = new(true, false, true, true, true, false, false);
	        }
        });
	}

    public void RowRender(RowRenderEventArgs<AccessEntity> args)
    {
        args.Attributes.Add("class", UserSettings.Identity.GetColorAccessRights(args.Data.Rights));
        //RowCounter += 1;
    }

    #endregion
}
