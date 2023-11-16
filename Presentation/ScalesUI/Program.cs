namespace ScalesUI;

internal static class Program
{
    private static AppVersionHelper AppVersion => AppVersionHelper.Instance;
    private static SqlContextManagerHelper ContextManager => SqlContextManagerHelper.Instance;
    private static LabelSessionHelper LabelSession => LabelSessionHelper.Instance;
    private static SqlCoreHelper SqlCore => SqlCoreHelper.Instance;
    
    [STAThread]
    internal static void Main()
    {
        // В первую очередь.
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        // Проверить каталог и файлы локализации.
        LocalizationUtils.CheckDirectoryWithFiles();
        // Настройка.
        SqlCore.SetSessionFactory(DebugHelper.Instance.IsDevelop);
        ContextManager.SetupJsonScales(Directory.GetCurrentDirectory(), typeof(Program).Assembly.GetName().Name);
        AppVersion.Setup(Assembly.GetExecutingAssembly(), LabelSession.Localization.LabelPrint.App);

        // Запуск.
        Application.Run(new MainForm());
    }
}
