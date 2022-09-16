// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemsComponents.Components;

public partial class SectionFilter<TItems, TItemFilter> : RazorPageSectionBase<TItems, TItemFilter>
	where TItems : SqlTableBase, new() where TItemFilter : SqlTableBase, new()
{
    #region Public and private fields, properties, constructor

    //

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                ItemsFilterCast = new() { new() { Description = LocaleCore.Table.FieldNull } };
                SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(SqlFieldEnum.Description), 0, false, false);
                TItems[]? itemsFilter = AppSettings.DataAccess.GetItems<TItems>(sqlCrudConfig);
                if (itemsFilter is not null)
                {
                    //ItemsCast.AddRange(itemsFilter.ToList<DataCore.Sql.Tables.TableBase>());
                    ItemsCast.AddRange(itemsFilter);
                    if (ItemFilterCast.EqualsDefault())
                        ItemFilterCast = ItemsFilterCast.First();
                }
            }
        });
	}

    #endregion
}
