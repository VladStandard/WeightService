// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Sections;

public partial class SectionAccess : RazorPageSectionBase<AccessModel>
{
    #region Public and private fields, properties, constructor

    public SectionAccess()
    {
	    RazorConfig.IsShowFilterMarked = true;
	    RazorConfig.IsShowFilterOnlyTop = true;
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
                ItemsCast = AppSettings.DataAccess.GetListAcesses(RazorConfig.IsShowMarked, RazorConfig.IsShowOnlyTop);

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
