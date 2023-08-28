namespace WsLocalizationCore.Models;

public sealed class WsLocaleContextMenu : WsLocalizationBase
{
	#region Public and private fields, properties, constructor

	public string Open => Lang == WsEnumLanguage.English ? "Open" : "Открыть";
	public string OpenNewTab => Lang == WsEnumLanguage.English ? "Open in a new tab" : "Открыть в новом окне";
	public string Delete => Lang == WsEnumLanguage.English ? "Delete" : "Удалить навсегда";
	public string Mark => Lang == WsEnumLanguage.English ? "Mark" : "Пометить на удаление";

	#endregion
}