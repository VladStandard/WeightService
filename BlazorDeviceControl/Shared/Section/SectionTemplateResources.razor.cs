// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Section;

public partial class SectionTemplateResources
{
    #region Public and private fields, properties, constructor

    private List<TemplateResourceEntity> ItemsCast => Items == null ? new() : Items.Select(x => (TemplateResourceEntity)x).ToList();

    /// <summary>
    /// Constructor.
    /// </summary>
    public SectionTemplateResources()
    {
        Table = new TableScaleEntity(ProjectsEnums.TableScale.TemplatesResources);
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
                Items = AppSettings.DataAccess.Crud.GetEntities<TemplateResourceEntity>(
                    new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                    new(DbField.Type, DbOrderDirection.Asc),
                    IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                    ?.ToList<BaseEntity>();
                Items?.OrderBy(x => ((TemplateResourceEntity)x).Name);
                Items?.OrderBy(x => ((TemplateResourceEntity)x).Type);
                ButtonSettings = new(true, true, true, true, true, false, false);
            }
		});
	}

    #endregion
}
