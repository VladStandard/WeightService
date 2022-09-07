// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;

namespace BlazorDeviceControl.Razors.Sections.Plus;

public partial class SectionPlusObsolete : RazorPageModel
{
    #region Public and private fields, properties, constructor

    private List<PluObsoleteModel> ItemsCast
    {
	    get => Items == null ? new() : Items.Select(x => (PluObsoleteModel)x).ToList();
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
		        Table = new TableScaleModel(ProjectsEnums.TableScale.PlusObsolete);
		        IsShowMarkedFilter = true;
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
				ItemsCast = AppSettings.DataAccess.GetListPluObsoletes(IsShowMarked, IsShowOnlyTop, ItemFilter);

				ButtonSettings = new(true, true, true, true, true, false, false);
            }
        });
    }

    private void SetFilterItems(List<TableBaseModel>? items, long? scaleId)
    {
        if (items != null)
        {
            Items = new();
            foreach (TableBaseModel item in items)
            {
                if (item is PluObsoleteModel plu && plu.Scale.Identity.Id == scaleId)
                    Items.Add(item);
            }
        }
    }

    #endregion
}
