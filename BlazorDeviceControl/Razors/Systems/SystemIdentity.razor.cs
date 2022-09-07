// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Components;
namespace BlazorDeviceControl.Razors.Systems;

public partial class SystemIdentity : RazorPageBase
{
	#region Public and private fields, properties, constructor

	[Parameter] public string AuthorizingText { get; set; }
	private List<TypeModel<Lang>>? TemplateLanguages { get; set; }
	private List<Lang> Langs { get; set; }

	public SystemIdentity()
	{
		AuthorizingText = string.Empty;
		Langs = new();
		foreach (Lang lang in Enum.GetValues(typeof(Lang)))
			Langs.Add(lang);
		TemplateLanguages = AppSettings.DataSourceDics.GetTemplateLanguages();
	}

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();

		RunActionsInitialized(new()
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

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		RunActionsParametersSet(new()
		{
			() =>
			{
				//
			}
		});
	}

	private string GetAuthorizingText()
	{
		UserSettings.SetupUserName(string.Empty, ParentRazor);
		return AuthorizingText = LocaleCore.Strings.AuthorizingProcess;
	}

	private string GetAuthorizedText(string? name)
	{
		UserSettings.SetupUserName(name, ParentRazor);
		return AuthorizingText = LocaleCore.Strings.AuthorizingSuccess;
	}

	private string GetNotAuthorizedText()
	{
		UserSettings.SetupUserName(LocaleCore.System.SystemIdentityNotAuthorized, ParentRazor);
		return AuthorizingText = LocaleCore.Strings.AuthorizingNot;
	}

	#endregion
}
