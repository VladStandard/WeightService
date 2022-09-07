// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Sections;

public partial class SectionHosts : RazorPageSectionBase<HostModel>
{
    #region Public and private fields, properties, constructor

    public SectionHosts()
    {
		IsShowMarkedFilter = true;
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
	            ItemsCast = AppSettings.DataAccess.GetListHosts(IsShowMarked, IsShowOnlyTop, false);
                ButtonSettings = new(true, true, true, true, true, false, false);
            }
        });
    }

    #endregion
}
