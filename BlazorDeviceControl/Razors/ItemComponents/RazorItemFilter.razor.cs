// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemComponents;

public partial class RazorItemFilter<TItem> : RazorComponentItemBase<TItem> where TItem : SqlTableBase, new() 
{
    #region Public and private fields, properties, constructor

    private List<TItem> SqlItemsCast { get; set; }

    public RazorItemFilter()
    {
	    SqlItemsCast = new();
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
	            SqlItemsCast = new() { new() { Description = LocaleCore.Table.FieldNull } };
                SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(SqlFieldEnum.Description), 0, false, false);
                TItem[]? items = AppSettings.DataAccess.GetItems<TItem>(sqlCrudConfig);
                if (items is not null)
                {
                    //ItemsCast.AddRange(itemsFilter.ToList<DataCore.Sql.Tables.TableBase>());
                    SqlItemsCast.AddRange(items);
                    if (SqlItemCast.EqualsDefault())
                        SqlItemCast = SqlItemsCast.First();
                }
            }
        });
    }

    #endregion
}
