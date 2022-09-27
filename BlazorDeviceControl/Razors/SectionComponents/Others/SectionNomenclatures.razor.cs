// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.SectionComponents.Others;

public partial class SectionNomenclatures : RazorComponentSectionBase<NomenclatureModel, SqlTableBase>
{
    #region Public and private fields, properties, constructor

    public SectionNomenclatures()
    {
		RazorComponentConfig.IsShowItemsCount = true;
        RazorComponentConfig.IsShowFilterMarked = true;
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
	            SqlItemsCast = AppSettings.DataAccess.GetListNomenclatures(RazorComponentConfig.IsShowMarked, RazorComponentConfig.IsShowOnlyTop, false);

                ButtonSettings = new(true, true, true, true, true, false, false);
            }
        });
    }

    #endregion
}
