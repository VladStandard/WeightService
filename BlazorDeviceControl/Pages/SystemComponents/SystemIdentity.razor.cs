// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Pages.SystemComponents;

public partial class SystemIdentity : RazorComponentBase
{
	#region Public and private fields, properties, constructor

	private List<TypeModel<Lang>>? TemplateLanguages { get; set; }
	private List<Lang> Langs { get; set; }
    private string HttpId => HttpContext is null ? string.Empty : HttpContext.Connection.Id;
    private string IpAddress => HttpContext?.Connection.RemoteIpAddress is null ? string.Empty : HttpContext.Connection.RemoteIpAddress.ToString();
    protected string HttpIdDescription => $"{LocaleCore.Strings.AuthorizingId}: {HttpId}";
    protected string IpAddressDescription => $"{LocaleCore.Strings.AuthorizingApAddress}: {IpAddress}";

    public SystemIdentity()
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