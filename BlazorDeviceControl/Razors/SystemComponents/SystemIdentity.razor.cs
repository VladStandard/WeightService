// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.SystemComponents;

public partial class SystemIdentity : RazorComponentBase
{
	#region Public and private fields, properties, constructor

	private List<TypeModel<LangEnum>>? TemplateLanguages { get; set; }
	private List<LangEnum> Langs { get; set; }

	public SystemIdentity()
	{
		Langs = new();
		foreach (LangEnum lang in Enum.GetValues(typeof(LangEnum)))
			Langs.Add(lang);
		TemplateLanguages = AppSettings.DataSourceDics.GetTemplateLanguages();
	}

	#endregion

	#region Public and private methods

	//protected override void OnParametersSet()
 //   {
 //       RunActionsParametersSet(new()
 //       {
 //           () =>
 //           {
                
 //           }
 //       }
 //       );
 //   }

	#endregion
}
