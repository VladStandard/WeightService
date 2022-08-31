// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Sections;

public partial class SectionTemplates : RazorBase
{
    #region Public and private fields, properties, constructor

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
        IsShowMarkedFilter = true;
		ItemsCast = new();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        RunActions(new()
        {
            () =>
            {
                ItemsCast = AppSettings.DataAccess.Crud.GetListTemplates(IsShowMarked, IsShowOnlyTop, false);

                ButtonSettings = new(true, true, true, true, true, false, false);
            }
        });
    }

    #endregion
}
