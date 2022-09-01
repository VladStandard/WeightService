// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Localizations;

public class LocaleMemory
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleMemory _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleMemory Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public ShareEnums.Lang Lang { get; set; } = ShareEnums.Lang.Russian;

    #region Public and private fields, properties, constructor

    public string Memory => Lang == ShareEnums.Lang.English ? "Memory" : "Память";
    public string MemoryActionStart => Lang == ShareEnums.Lang.English ? "Run the memory manager" : "Запустить менеджер памяти";
    public string MemoryActionStop => Lang == ShareEnums.Lang.English ? "Stop the memory manager" : "Остановить менеджер памяти";
    public string MemoryException => Lang == ShareEnums.Lang.English ? "Memory manager error" : "Ошибка менеджера памяти";
    public string MemoryFillSize => Lang == ShareEnums.Lang.English ? "Memory fill percentage" : "Процент заполнения памяти";
    public string MemoryIsExecute => Lang == ShareEnums.Lang.English ? "Application memory manager at work." : "Менеджер памяти приложения в работе.";
    public string MemoryIsNotExecute => Lang == ShareEnums.Lang.English ? "The application memory manager is not running!" : "Менеджер памяти приложения не выполняется!";
    public string MemoryLimit => Lang == ShareEnums.Lang.English ? "Memory limit" : "Лимит памяти";
    public string MemoryLimitNotSet => Lang == ShareEnums.Lang.English ? "Memory limit not set!" : "Лимит памяти не задан!";
    public string MemoryPhysical => Lang == ShareEnums.Lang.English ? "Physical memory" : "Физическая память";
    public string MemoryResult => Lang == ShareEnums.Lang.English ? "Result" : "Результат";
    public string MemoryTitle => Lang == ShareEnums.Lang.English ? "Application memory manager" : "Менеджер памяти приложения";
    public string MemoryUsed => Lang == ShareEnums.Lang.English ? "Memory used" : "Используемая память";
    public string MemoryVirtual => Lang == ShareEnums.Lang.English ? "Virtual memory" : "Виртуальная память";

    #endregion
}
