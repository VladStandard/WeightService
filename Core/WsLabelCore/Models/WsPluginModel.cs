namespace WsLabelCore.Models;

/// <summary>
/// Модель плагина.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class WsPluginModel : WsBaseHelper
{
    #region Public and private fields, properties, constructor

    private AsyncLock Mutex { get; }
    private CancellationTokenSource Cts { get; set; }
    private Task Tsk { get; set; }
    public WsPluginConfigModel Config { get; set; }
    
    private ushort _counter;
    /// <summary>
    /// Счётчик.
    /// </summary>
    public ushort SafeCounter
    {
        get => _counter;
        private set => _counter = value > 0_999 ? default : value;
    }
    /// <summary>
    /// Тип плагина.
    /// </summary>
    public WsEnumPluginType PluginType { get; set; }

    public WsPluginModel()
    {
        Mutex = new();
        Cts = new();
        Tsk = Task.Run(() => { });
        Config = new();
    }

    #endregion

    #region Public and private methods

    public void Init(WsPluginConfigModel config)
    {
        Init();
        Config = config;
    }

    /// <summary>
    /// Execute.
    /// </summary>
    /// <param name="action"></param>
    public void Execute(Action action)
    {
        Close();
        Execute();
        Cts = new();

        Tsk = Task.Run(async () =>
        {
            SafeCounter = 0;
            while (IsExecute)
            {
                SafeCounter++;
                try
                {
                    // AsyncLock can be locked asynchronously
                    AwaitableDisposable<IDisposable> lockTask = Mutex.LockAsync(Cts.Token);
                    using (await lockTask.ConfigureAwait(true))
                    {
                        WsPluginConfigModel.WaitSync(Config.StopwatchExecute, Config.WaitExecute);
                        if (Cts.IsCancellationRequested) continue;
                        // It's safe to await while the lock is held
                        action();
                    }
                }
                catch (TaskCanceledException)
                {
                    // Not the problem.
                }
                catch (Exception ex)
                {
                    switch (PluginType)
                    {
                        case WsEnumPluginType.Default:
                            WsSqlContextManagerHelper.Instance.ContextItem.SaveLogErrorWithDescription(ex, WsLocaleCore.LabelPrint.PluginDefault);
                            break;
                        case WsEnumPluginType.Massa:
                            WsSqlContextManagerHelper.Instance.ContextItem.SaveLogErrorWithDescription(ex, WsLocaleCore.LabelPrint.PluginMassa);
                            break;
                        case WsEnumPluginType.Memory:
                            WsSqlContextManagerHelper.Instance.ContextItem.SaveLogErrorWithDescription(ex, WsLocaleCore.LabelPrint.PluginMemory);
                            break;
                        case WsEnumPluginType.Print:
                            WsSqlContextManagerHelper.Instance.ContextItem.SaveLogErrorWithDescription(ex, WsLocaleCore.LabelPrint.PluginPrint);
                            break;
                        case WsEnumPluginType.Label:
                            WsSqlContextManagerHelper.Instance.ContextItem.SaveLogErrorWithDescription(ex, WsLocaleCore.LabelPrint.PluginLabel);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        });
    }
    
    public override void Close()
    {
        // Need to check.
        base.Close();

        Cts.Cancel();
        WsPluginConfigModel.WaitSync(Config.WaitClose);

        Tsk.Wait(WsPluginConfigModel.WaitLowLimit);
    }

    #endregion
}