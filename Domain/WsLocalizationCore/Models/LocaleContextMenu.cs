using WsLocalizationCore.Common;
namespace WsLocalizationCore.Models;

public sealed class LocaleContextMenu : LocalizationBase
{
	#region Public and private fields, properties, constructor

	public string Open => Lang == EnumLanguage.English ? "Open" : "Открыть";
	public string OpenNewTab => Lang == EnumLanguage.English ? "Open in a new tab" : "Открыть в новом окне";
	public string Delete => Lang == EnumLanguage.English ? "Delete" : "Удалить навсегда";
	public string Mark => Lang == EnumLanguage.English ? "Mark" : "Пометить на удаление";

	#endregion
}