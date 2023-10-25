namespace WsLocalizationCore.Models;

public sealed class WsLocaleMemory : WsLocalizationBase
{
    #region Public and private fields, properties, constructor

    public string Memory => Lang == WsEnumLanguage.English ? "Memory" : "Память";
    public string MemoryActionStart => Lang == WsEnumLanguage.English ? "Run the memory manager" : "Запустить менеджер памяти";
    public string MemoryActionStop => Lang == WsEnumLanguage.English ? "Stop the memory manager" : "Остановить менеджер памяти";
    public string MemoryException => Lang == WsEnumLanguage.English ? "Memory manager error" : "Ошибка менеджера памяти";
    public string MemoryFillSize => Lang == WsEnumLanguage.English ? "Memory fill percentage" : "Процент заполнения памяти";
    public string MemoryIsExecute => Lang == WsEnumLanguage.English ? "Application memory manager at work." : "Менеджер памяти приложения в работе.";
    public string MemoryIsNotExecute => Lang == WsEnumLanguage.English ? "The application memory manager is not running!" : "Менеджер памяти приложения не выполняется!";
    public string MemoryLimit => Lang == WsEnumLanguage.English ? "Memory limit" : "Лимит памяти";
    public string MemoryLimitNotSet => Lang == WsEnumLanguage.English ? "Memory limit not set!" : "Лимит памяти не задан!";
    public string MemoryPhysical => Lang == WsEnumLanguage.English ? "Physical memory" : "Физическая память";
    public string MemoryResult => Lang == WsEnumLanguage.English ? "Result" : "Результат";
    public string MemoryTitle => Lang == WsEnumLanguage.English ? "Application memory manager" : "Менеджер памяти приложения";
    public string MemoryUsed => Lang == WsEnumLanguage.English ? "Memory used" : "Используемая память";
    public string MemoryVirtual => Lang == WsEnumLanguage.English ? "Virtual memory" : "Виртуальная память";

    #endregion
}