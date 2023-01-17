// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.TableScaleModels.WorkShops;

namespace BlazorDeviceControl.Razors.SectionComponents.Devices;

public partial class SectionWorkshops : RazorComponentSectionBase<WorkShopModel, SqlTableBase>
{
    #region Public and private fields, properties, constructor

    public SectionWorkshops() : base()
    {
        SqlCrudConfigSection.IsGuiShowFilterOnlyTop = false;
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
	            SqlSectionCast = DataContext.GetListNotNullable<WorkShopModel>(SqlCrudConfigSection);
            }
        });
    }

    #endregion
}