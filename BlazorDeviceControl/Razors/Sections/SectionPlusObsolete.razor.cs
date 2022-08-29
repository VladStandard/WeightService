// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Fields;
using DataCore.Sql.Tables;

namespace BlazorDeviceControl.Razors.Sections;

public partial class SectionPlusObsolete : BlazorCore.Models.RazorBase
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
                long? scaleId = null;
                if (ItemFilter is ScaleEntity scale)
                    scaleId = scale.IdentityId;
                List<FieldFilterModel> filters = IsShowMarkedFilter ? new() : new List<FieldFilterModel> { new(DbField.IsMarked, DbComparer.Equal, false) };
                if (scaleId is not null)
	                filters.Add(new($"{nameof(PluObsoleteEntity.Scale)}.{DbField.IdentityId}", DbComparer.Equal, scaleId));
                ItemsCast = AppSettings.DataAccess.Crud.GetItemsListNotNull<PluObsoleteEntity>(IsShowOnlyTop, filters, new(DbField.GoodsName));

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
