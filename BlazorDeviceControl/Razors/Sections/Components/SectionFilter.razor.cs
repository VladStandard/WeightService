// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Sections.Components;

public partial class SectionFilter<TItem> : RazorPageSectionBase<TItem> where TItem : SqlTableBase, new()
{
    #region Public and private fields, properties, constructor

    //

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        RunActionsParametersSet(new()
        {
            () =>
            {
                ItemsCast = new() { new TItem() { Description = LocaleCore.Table.FieldNull } };
                SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(SqlFieldEnum.Description), 0, false, false);
				TItem[]? itemsFilter = AppSettings.DataAccess.GetItems<TItem>(sqlCrudConfig);
                if (itemsFilter is not null)
                {
                    //ItemsCast.AddRange(itemsFilter.ToList<DataCore.Sql.Tables.TableBase>());
                    ItemsCast.AddRange(itemsFilter);
                    if (ItemFilterCast.EqualsDefault())
                        ItemFilterCast = ItemsCast.First();
                }
            }
        });
	}

    #endregion
}
