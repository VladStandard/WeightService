// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Brands;

namespace BlazorDeviceControl.Razors.SectionComponents.References1C;

public partial class SectionBrands : RazorComponentSectionBase<BrandModel, SqlTableBase>
{
    #region Public and private fields, properties, constructor

    public SectionBrands() : base()
    {
        ButtonSettings = new(false, true, true, true, false, false, false);
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                SqlSectionCast = DataContext.GetListNotNullable<BrandModel>(SqlCrudConfigSection);
                AutoShowFilterOnlyTopSetup();
            }
        });
    }

    #endregion
}
