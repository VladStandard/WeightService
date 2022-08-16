// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using DataCore;
using DataCore.Models;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Section;

public partial class SectionTemplates
{
    #region Public and private fields, properties, constructor

    private List<TypeEntity<string>>? TemplateCategories { get; set; }
    private string? TemplateCategory { get; set; } = string.Empty;
    private List<TemplateEntity> ItemsCast => Items == null ? new() : Items.Select(x => (TemplateEntity)x).ToList();

    #endregion

    #region Public and private methods

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Table = new TableScaleEntity(ProjectsEnums.TableScale.Templates);
        TemplateCategories = DataSourceDicsEntity.GetTemplateCategories();
        TemplateCategory = null;
        Items = new();
	}

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        RunActions(new()
        {
            () =>
            {
                if (string.IsNullOrEmpty(TemplateCategory))
                {
                    TemplateCategory = TemplateCategories.FirstOrDefault()?.Value;
                    Items = AppSettings.DataAccess.Crud.GetEntities<TemplateEntity>(
                        IsShowMarkedItems ? null
                            : new FilterListEntity(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                        new(DbField.CategoryId, DbOrderDirection.Asc),
                        IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                        ?.ToList<BaseEntity>();
                }
                else
                {
                    Items = AppSettings.DataAccess.Crud.GetEntities<TemplateEntity>(
                        IsShowMarkedItems
                            ? new(new() { new(DbField.CategoryId, DbComparer.Equal, TemplateCategory) })
                            : new(new() { new(DbField.IsMarked, DbComparer.Equal, false),
                                new(DbField.CategoryId, DbComparer.Equal, TemplateCategory) }),
                        new(DbField.CategoryId, DbOrderDirection.Asc),
                        IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                        ?.ToList<BaseEntity>();
                }
                ButtonSettings = new(true, true, true, true, true, false, false);
            }
        });
    }

    #endregion
}
