namespace WsLabelCore.Models;

/// <summary>
/// Модель плагина.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class PluginModel : IDisposable
{
    #region Public and private fields, properties, constructor

    private AsyncLock Mutex { get; }
    private Task Tsk { get; set; }
    public PluginConfigModel Config { get; set; }
    public EnumPluginType PluginType { get; set; }
    private CancellationTokenSource Cts { get; set; }
    private SqlContextItemHelper ContextItem => SqlContextItemHelper.Instance;
    
    public PluginModel()
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
                        PluginConfigModel.WaitSync(Config.WaitExecute);
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
                        case EnumPluginType.Default:
                            ContextItem.SaveLogErrorWithDescription(ex, LocaleCore.LabelPrint.PluginDefault);
                            break;
                        case EnumPluginType.Massa:
                            ContextItem.SaveLogErrorWithDescription(ex, LocaleCore.LabelPrint.PluginMassa);
                            break;
                        case EnumPluginType.Memory:
                            ContextItem.SaveLogErrorWithDescription(ex, LocaleCore.LabelPrint.PluginMemory);
                            break;
                        case EnumPluginType.Label:
                            ContextItem.SaveLogErrorWithDescription(ex, LocaleCore.LabelPrint.PluginLabel);
                            break;
                    }
                }
            }
        });
    }
    
    public void Dispose()
    {
        Cts.Cancel();
        Tsk.Wait(PluginConfigModel.WaitLowLimit);
    }

    #endregion
}