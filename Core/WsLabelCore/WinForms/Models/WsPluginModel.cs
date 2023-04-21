// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.WinForms.Models;

public sealed class WsPluginModel : HelperBase
{
    #region Public and private fields, properties, constructor

    private AsyncLock Mutex { get; }
    private CancellationTokenSource Cts { get; set; }
    private Task Tsk { get; set; }
    public WsConfigModel Config { get; set; }
    public TaskType TskType { get; set; }
    private ushort _counter;
    public ushort Counter
    {
        get => _counter;
        private set => _counter = value > 0_999 ? default : value;
    }

    public WsPluginModel()
    {
        TskType = TaskType.Default;
        Mutex = new();
        Cts = new();
        Tsk = Task.Run(() => { });
        Config = new();
    }

    #endregion

    #region Public and private methods

    public void Init(WsConfigModel config)
    {
        base.Init();
        Config = config;
    }

    /// <summary>
    /// Execute.
    /// </summary>
    /// <param name="action"></param>
    public void Execute(Action action)
    {
        Close();
        base.Execute();
        Cts = new();

        Tsk = Task.Run(async () =>
        {
            Counter = 0;
            while (IsExecute)
            {
                Counter++;
                try
                {
                    // AsyncLock can be locked asynchronously
                    AwaitableDisposable<IDisposable> lockTask = Mutex.LockAsync(Cts.Token);
                    using (await lockTask.ConfigureAwait(true))
                    {
                        Config.WaitSync(Config.StopwatchExecute, Config.WaitExecute);
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
                    WsSqlContextManagerHelper.Instance.ContextItem.SaveLogError(ex);
                }
            }
        });
    }
    
    public override void Close()
    {
        // Need to check.
        base.Close();

        Cts.Cancel();
        Config.WaitSync(Config.WaitClose);

        Tsk.Wait(WsConfigModel.WaitLowLimit);
    }

    #endregion
}
