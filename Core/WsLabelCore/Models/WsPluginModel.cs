namespace WsLabelCore.Models;

/// <summary>
/// Модель плагина.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class WsPluginModel : IDisposable
{
    #region Public and private fields, properties, constructor

    private AsyncLock Mutex { get; }
    private Task Tsk { get; set; }
    public WsPluginConfigModel Config { get; set; }
    public WsEnumPluginType PluginType { get; set; }
    private CancellationTokenSource Cts { get; set; }
    
    public WsPluginModel()
    {
        Mutex = new();
        Cts = new();
        Tsk = Task.CompletedTask;
        Config = new();
    }

    #endregion

    #region Public and private methods
    
    public void Execute(Action action)
    {
        Dispose();
        Cts = new();
        Tsk = Task.Run(async () =>
        {
            while (!Cts.IsCancellationRequested)
            {
                try
                {
                    // AsyncLock can be locked asynchronously
                    AwaitableDisposable<IDisposable> lockTask = Mutex.LockAsync(Cts.Token);
                    using (await lockTask.ConfigureAwait(true))
                    {
                        action();
                        if (Cts.IsCancellationRequested) continue;
                        WsPluginConfigModel.WaitSync(Config.WaitExecute);
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
                        case WsEnumPluginType.Label:
                            WsSqlContextManagerHelper.Instance.ContextItem.SaveLogErrorWithDescription(ex, WsLocaleCore.LabelPrint.PluginLabel);
                            break;
                    }
                }
            }
        });
    }
    
    public void Dispose()
    {
        Cts.Cancel();
        Tsk.Wait(WsPluginConfigModel.WaitLowLimit);
    }

    #endregion
}