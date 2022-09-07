// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;

namespace BlazorDeviceControl.Razors.Sections;

public partial class SectionContragents : RazorPageModel
{
    #region Public and private fields, properties, constructor

    private List<ContragentModel> ItemsCast
    {
        get => Items == null ? new() : Items.Select(x => (ContragentModel)x).ToList();
        set => Items = !value.Any() ? null : new(value);
    }

    #endregion

    #region Public and private methods

    protected override void OnInitialized()
    {
        base.OnInitialized();

        RunActionsInitialized(new()
        {
	        () =>
	        {
		        Table = new TableScaleModel(ProjectsEnums.TableScale.Contragents);
		        ItemsCast = new();
	        }
        });
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        RunActionsParametersSet(new()
        {
            () =>
            {
	            ItemsCast = AppSettings.DataAccess.GetListContragents(IsShowMarked, IsShowOnlyTop);

                ButtonSettings = new(true, true, true, true, true, false, false);
            }
        });
    }

    #endregion
}
