// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Sections.Componets;

public partial class SectionFilter<T> : RazorPageSectionBase<T> where T : DataCore.Sql.Tables.TableBase, new()
{
    #region Public and private fields, properties, constructor

    //[Parameter] public SqlTableScaleEnum FilterTable { get; set; }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        RunActionsParametersSet(new()
        {
            () =>
            {
                ItemsCast = new() { new T() { Description = LocaleCore.Table.FieldNull } };
                SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(SqlFieldEnum.Description), 0, false, false);
                T[]? itemsFilter = AppSettings.DataAccess.GetItems<T>(sqlCrudConfig);
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
