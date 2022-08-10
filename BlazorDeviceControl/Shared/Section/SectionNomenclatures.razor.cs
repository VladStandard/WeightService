// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Section;

public partial class SectionNomenclatures
{
    #region Public and private fields, properties, constructor

    private List<NomenclatureEntity> ItemsCast => Items == null ? new() : Items.Select(x => (NomenclatureEntity)x).ToList();

    /// <summary>
    /// Constructor.
    /// </summary>
    public SectionNomenclatures()
    {
        Table = new TableScaleEntity(ProjectsEnums.TableScale.Nomenclatures);
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
                Items = AppSettings.DataAccess.Crud.GetEntities<NomenclatureEntity>(null,
                    new(DbField.Name),
                    IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                    ?.ToList<BaseEntity>();
                ButtonSettings = new(true, true, true, true, true, false, false);
            }
		});
	}

    #endregion
}
