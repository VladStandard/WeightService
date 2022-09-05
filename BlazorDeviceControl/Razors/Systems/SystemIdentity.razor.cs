// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Systems;

public partial class SystemIdentity : RazorPageModel
{
	#region Public and private fields, properties, constructor

	//private bool IsAuthorizingLoad { get; set; }
	private List<TypeModel<Lang>>? TemplateLanguages { get; set; }
	private List<Lang> Langs { get; set; }

	public SystemIdentity()
	{
		Langs = new();
		foreach (Lang lang in Enum.GetValues(typeof(Lang)))
			Langs.Add(lang);
		TemplateLanguages = AppSettings.DataSourceDics.GetTemplateLanguages();

		ActionsInitialized = new()
		{
			() =>
			{
				if (HttpContextAccess?.HttpContext is not null)
				{
					UserSettings = new(HttpContextAccess.HttpContext);
				}
			}
		};
	}

	#endregion

	#region Public and private methods

	#endregion
}
