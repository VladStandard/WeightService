// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Systems;

public partial class SystemIdentity : RazorPageModel
{
	private bool IsAuthorizingLoad { get; set; }

	#region Public and private methods

	protected override void OnInitialized()
    {
        base.OnInitialized();
	}

	protected override void OnParametersSet()
    {
	    base.OnParametersSet();

	    RunActions(new()
	    {
		    () =>
		    {
			    if (HttpContextAccess?.HttpContext is not null)
			    {
				    UserSettings = new(HttpContextAccess.HttpContext);
			    }
		    }
	    });
    }

	#endregion
}
