// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Sections;

public partial class SectionVersions : RazorPageSectionBase<VersionModel>
{
    #region Public and private fields, properties, constructor

    public SectionVersions()
    {
        RazorPageConfig.IsShowFilterMarked = false;
	}

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        RunActionsParametersSet(new()
        {
            () =>
            {
	            ItemsCast = AppSettings.DataAccess.GetListVersions();

                ButtonSettings = new(false, false, false, false, false, false, false);
            }
        });
    }

    #endregion
}
