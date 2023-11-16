namespace WsLocalizationCore.Models;

public sealed class LocaleMemory : LocalizationBase
{
    #region Public and private fields, properties, constructor

    public string Memory => Lang == EnumLanguage.English ? "Memory" : "Память";
    public string MemoryActionStart => Lang == EnumLanguage.English ? "Run the memory manager" : "Запустить менеджер памяти";
    public string MemoryActionStop => Lang == EnumLanguage.English ? "Stop the memory manager" : "Остановить менеджер памяти";
    public string MemoryException => Lang == EnumLanguage.English ? "Memory manager error" : "Ошибка менеджера памяти";
    public string MemoryFillSize => Lang == EnumLanguage.English ? "Memory fill percentage" : "Процент заполнения памяти";
    public string MemoryIsExecute => Lang == EnumLanguage.English ? "Application memory manager at work." : "Менеджер памяти приложения в работе.";
    public string MemoryIsNotExecute => Lang == EnumLanguage.English ? "The application memory manager is not running!" : "Менеджер памяти приложения не выполняется!";
    public string MemoryLimit => Lang == EnumLanguage.English ? "Memory limit" : "Лимит памяти";
    public string MemoryLimitNotSet => Lang == EnumLanguage.English ? "Memory limit not set!" : "Лимит памяти не задан!";
    public string MemoryPhysical => Lang == EnumLanguage.English ? "Physical memory" : "Физическая память";
    public string MemoryResult => Lang == EnumLanguage.English ? "Result" : "Результат";
    public string MemoryTitle => Lang == EnumLanguage.English ? "Application memory manager" : "Менеджер памяти приложения";
    public string MemoryUsed => Lang == EnumLanguage.English ? "Memory used" : "Используемая память";
    public string MemoryVirtual => Lang == EnumLanguage.English ? "Virtual memory" : "Виртуальная память";

    #endregion
}