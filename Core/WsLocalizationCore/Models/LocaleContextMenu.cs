// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsLocalizationCore.Common;

namespace WsLocalizationCore.Models;

public sealed class LocaleContextMenu : WsLocalizationBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleContextMenu _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleContextMenu Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

	#region Public and private fields, properties, constructor

	public string Open => Lang == WsEnumLanguage.English ? "Open" : "Открыть";
	public string OpenNewTab => Lang == WsEnumLanguage.English ? "Open in a new tab" : "Открыть в новом окне";
	public string Delete => Lang == WsEnumLanguage.English ? "Delete" : "Удалить навсегда";
	public string Mark => Lang == WsEnumLanguage.English ? "Mark" : "Пометить на удаление";

	#endregion
}