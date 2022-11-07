// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.SectionComponents;

public partial class RazorSectionFilter<TItem, TItemFilter> : RazorComponentSectionBase<TItem, TItemFilter>
	where TItem : SqlTableBase, new() where TItemFilter : SqlTableBase, new()
{
    #region Public and private fields, properties, constructor

    public RazorSectionFilter()
    {
        SqlItemsFilterCast = new();
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
	            if (ParentRazor?.SqlItem is not null && ParentRazor.SqlItem is TItemFilter)
	            {
		            SqlItemFilter = ParentRazor.SqlItemFilter = ParentRazor.SqlItem;
		        }
                
				SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
                    //new SqlFieldOrderModel(nameof(SqlTableBase.Description), SqlFieldOrderEnum.Asc), 
                    false, false);
                TItemFilter[]? items = DataAccess.GetArrayNullable<TItemFilter>(sqlCrudConfig);
                if (items is not null)
                {
	                // Sort items.
					switch (typeof(TItemFilter))
                    {
                        case var cls when cls == typeof(PluModel):
                            List<PluModel> plus = items.Cast<PluModel>().OrderBy(x => x.Name).ToList();
                            List<PluModel> plusNull = new() { DataAccess.GetItemNew<PluModel>() };
                            plusNull.AddRange(plus);
                            items = plusNull.Cast<TItemFilter>().ToArray();
							// Add null item first.
							break;
                        case var cls when cls == typeof(ScaleModel):
                            List<ScaleModel> scales = items.Cast<ScaleModel>().OrderBy(x => x.Description).ToList();
                            List<ScaleModel> scalesNull = new() { DataAccess.GetItemNew<ScaleModel>() };
                            scalesNull.AddRange(scales);
                            items = scalesNull.Cast<TItemFilter>().ToArray();
							// Add null item first.
							SqlItemsFilterCast = new() { DataAccess.GetItemNew<TItemFilter>() };
                            break;
                    }
					// Add sorted items second.
					SqlItemsFilterCast = new();
                    SqlItemsFilterCast.AddRange(items);
					// Select filter item.
					if (SqlItemFilterCast.EqualsDefault())
						SqlItemFilterCast = SqlItemsFilterCast.First();
                }
            }
        });
    }

    #endregion
}
