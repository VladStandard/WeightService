// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Models;

namespace BlazorDeviceControl.Pages.Menu.Profiles;

public partial class Profile : RazorComponentBase
{
	#region Public and private fields, properties, constructor

	private List<TypeModel<Lang>>? TemplateLanguages { get; set; }
	private List<Lang> Langs { get; set; }
    private string IpAddress => HttpContext?.Connection.RemoteIpAddress is null ? string.Empty : HttpContext.Connection.RemoteIpAddress.ToString();

    public Profile()
	{
		Langs = new();
		foreach (Lang lang in Enum.GetValues(typeof(Lang)))
			Langs.Add(lang);
		TemplateLanguages = BlazorAppSettings.DataSourceDics.GetTemplateLanguages();
	}
    
    #endregion

	#region Public and private methods
    
    #endregion
}