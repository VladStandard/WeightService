// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Fields;

namespace BlazorDeviceControl.Razors.Sections;

public partial class SectionTemplates : BlazorCore.Models.RazorBase
{
    #region Public and private fields, properties, constructor

    private List<TypeEntity<string>>? TemplateCategories { get; set; }
    private string? TemplateCategory { get; set; } = string.Empty;
    private List<TemplateEntity> ItemsCast
    {
        get => Items == null ? new() : Items.Select(x => (TemplateEntity)x).ToList();
        set => Items = !value.Any() ? null : new(value);
    }

    #endregion

    #region Public and private methods

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Table = new TableScaleEntity(ProjectsEnums.TableScale.Templates);
        TemplateCategories = DataSourceDicsEntity.GetTemplateCategories();
        TemplateCategory = null;
        ItemsCast = new();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        RunActions(new()
        {
            () =>
            {
                List<FieldFilterModel> filters = IsShowMarkedFilter ? new() : new List<FieldFilterModel> { new(DbField.IsMarked, DbComparer.Equal, false) };
                TemplateCategory = TemplateCategories?.FirstOrDefault()?.Value;
                if (TemplateCategory is not null)
                    filters.Add(new(DbField.CategoryId, DbComparer.Equal, TemplateCategory));
                ItemsCast = AppSettings.DataAccess.Crud.GetItemsListNotNull<TemplateEntity>(IsShowOnlyTop,filters);

                ButtonSettings = new(true, true, true, true, true, false, false);
            }
        });
    }

    #endregion
}
