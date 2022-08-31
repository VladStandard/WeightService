// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Sections.Plus;

public partial class SectionPlusObsolete : RazorBase
{
    #region Public and private fields, properties, constructor

    private List<PluObsoleteEntity> ItemsCast
    {
	    get => Items == null ? new() : Items.Select(x => (PluObsoleteEntity)x).ToList();
	    set => Items = !value.Any() ? null : new(value);
    }

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
    {
        base.OnInitialized();

        Table = new TableScaleEntity(ProjectsEnums.TableScale.PlusObsolete);
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
				ItemsCast = AppSettings.DataAccess.Crud.GetListPluObsoletes(IsShowMarked, IsShowOnlyTop, ItemFilter);

				ButtonSettings = new(true, true, true, true, true, false, false);
            }
        });
    }

    private void SetFilterItems(List<TableModel>? items, long? scaleId)
    {
        if (items != null)
        {
            Items = new();
            foreach (TableModel item in items)
            {
                if (item is PluObsoleteEntity plu && plu.Scale.IdentityId == scaleId)
                    Items.Add(item);
            }
        }
    }

    #endregion
}
