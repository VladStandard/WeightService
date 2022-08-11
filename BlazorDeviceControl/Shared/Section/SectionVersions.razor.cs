// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Section;

public partial class SectionVersions
{
    #region Public and private fields, properties, constructor

    private List<VersionEntity> ItemsCast => Items == null ? new() : Items.Select(x => (VersionEntity)x).ToList();

    #endregion

    #region Public and private methods

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Table = new TableSystemEntity(ProjectsEnums.TableSystem.Versions);
        Items = new();
	}

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        RunActions(new()
        {
            () =>
            {
                Items = AppSettings.DataAccess.Crud.GetEntities<VersionEntity>(
                    null,
                    new FieldOrderEntity(DbField.Version, DbOrderDirection.Desc),
                    IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                ?.ToList<BaseEntity>();
                ButtonSettings = new(false, false, false, false, false, false, false);
            }
        });
    }

    #endregion
}
