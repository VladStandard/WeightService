// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalization.Models;

public class LocaleContextMenu
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleContextMenu _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleContextMenu Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public Lang Lang { get; set; } = Lang.Russian;

	#region Public and private fields, properties, constructor

	public string Open => Lang == Lang.English ? "Open" : "Открыть";
	public string OpenNewTab => Lang == Lang.English ? "Open in a new tab" : "Открыть в новом окне";
	public string Delete => Lang == Lang.English ? "Delete" : "Удалить навсегда";
	public string Mark => Lang == Lang.English ? "Mark" : "Пометить на удаление";

	#endregion
}