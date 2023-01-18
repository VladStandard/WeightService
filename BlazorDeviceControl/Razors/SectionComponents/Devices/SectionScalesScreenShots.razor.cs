// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Scales;
using DataCore.Sql.TableScaleModels.ScalesScreenshots;

namespace BlazorDeviceControl.Razors.SectionComponents.Devices;

public partial class SectionScalesScreenShots : RazorComponentSectionBase<ScaleScreenShotModel, ScaleModel>
{
    #region Public and private fields, properties, constructor

    public SectionScalesScreenShots() : base()
    {
        ButtonSettings = new(true, true, true, true, true, true, false);
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
	            SqlCrudConfigSection.AddFilters(nameof(ScaleScreenShotModel.Scale), ParentRazor?.SqlItem);
				SqlSectionCast = DataContext.GetListNotNullable<ScaleScreenShotModel>(SqlCrudConfigSection);
            }
        });
    }

    #endregion
}