// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemsComponents;

public partial class SectionAccess : RazorPageSectionBase<AccessModel, SqlTableBase>
{
    #region Public and private fields, properties, constructor

    public SectionAccess()
    {
	    RazorPageConfig.IsShowFilterMarked = true;
	    RazorPageConfig.IsShowFilterOnlyTop = true;
	}

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                ItemsCast = AppSettings.DataAccess.GetListAcesses(RazorPageConfig.IsShowMarked, RazorPageConfig.IsShowOnlyTop);

				ButtonSettings = new(true, false, true, true, true, false, false);
            }
		});
    }

    private void RowRender(RowRenderEventArgs<AccessModel> args)
    {
        args.Attributes.Add("class", UserSettings.GetColorAccessRights(args.Data.Rights));
        //RowCounter += 1;
    }

    #endregion
}
