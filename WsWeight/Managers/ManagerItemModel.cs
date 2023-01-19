// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Tls;

namespace WsWeight.Managers;

public class ManagerItemModel : DisposableBase
{
    #region Public and private fields, properties, constructor

    public AsyncLock? Mutex { get; set; }
    public CancellationTokenSource? Cts { get; set; }
    public Task? Tsk { get; set; }
    public bool IsSuspend { get; set; }
    public ManagerConfigModel Config { get; set; }
    public TaskType TaskType { get; set; }

    public ManagerItemModel()
    {
        Mutex = null;
        Cts = null;
        Tsk = null;
        IsSuspend = true;
        TaskType = TaskType.Default;
        Config = new();
    }

    public ManagerItemModel(TaskType taskType, ManagerConfigModel managerConfig) : this()
    {
        TaskType = taskType;
        Config = managerConfig;
    }

    #endregion

    #region Public and private methods

    private void OpenTaskBase()
    {
        CheckIsDisposed();
        if (Tsk is null) return;

        Cts?.Cancel();
        Tsk.Wait(Config.WaitClose);
        if (Tsk.IsCompleted)
            Tsk.Dispose();
    }

    /// <summary>
    /// OpenTask v.4
    /// </summary>
    /// <param name="callback"></param>
    public void Open(Action? callback)
    {
        Mutex = null;
        Cts = null;
        
        OpenTaskBase();

        Tsk = System.Threading.Tasks.Task.Run(async () =>
        {
            ReopenCount = 0;
            while (IsOpen && !IsSuspend)
            {
                ReopenCount++;
                Mutex ??= new();
                Cts ??= new(); try
                {
                    // AsyncLock can be locked asynchronously
                    AwaitableDisposable<IDisposable> lockTask = Mutex.LockAsync(Cts.Token);
                    using (await lockTask.ConfigureAwait(true))
                    {
                        Config.WaitSync(Config.StopwatchReopen, Config.WaitReopen);

                        if (Cts is null || Cts.IsCancellationRequested)
                            continue;
                        // It's safe to await while the lock is held
                        callback?.Invoke();
                    }
                }
                catch (TaskCanceledException)
                {
                    // Not the problem.
                }
                catch (Exception ex)
                {
                    DataAccessHelper.Instance.LogErrorFast(ex);
                    Config.WaitSync(Config.WaitException);
                }
            }
        });
    }

    // OpenTaskReopen v.1
    //private void OpenTaskReopen(ReopenCallback callback,
    //    [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    //{
    //    OpenTaskBase(Tsk, CtsReopen);
    //    CtsReopen = new CancellationTokenSource();

    //    Tsk = Task.Run(async () =>
    //    {
    //        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
    //        MutexReopen = new AsyncLock();
    //        while (MutexReopen is not null && CtsReopen is not null)
    //        {
    //            try
    //            {
    //                // AsyncLock can be locked asynchronously
    //                using (await MutexReopen.LockAsync(CtsReopen.Token))
    //                {
    //                    if (CtsReopen.IsCancellationRequested)
    //                        break;
    //                    // It's safe to await while the lock is held
    //                    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);
    //                    callback?.Invoke();
    //                    //WaitSync(WaitReopen);
    //                }
    //            }
    //            catch (TaskCanceledException tcex)
    //            {
    //                // Not the problem.
    //                Exception.Catch(null, ref tcex, false, filePath, lineNumber, memberName);
    //            }
    //            catch (Exception ex)
    //            {
    //                Exception.Catch(null, ex, false, filePath, lineNumber, memberName);
    //                WaitSync(WaitException);
    //            }
    //        }
    //    });
    //}

    // OpenTaskReopen v.2
    //private void OpenTaskReopen(ReopenCallback callback,
    //    [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    //{
    //    OpenTaskBase(Tsk, CtsReopen);
    //    CtsReopen = new CancellationTokenSource();

    //    Tsk = Task.Run(async () =>
    //    {
    //        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
    //        MutexReopen = new AsyncLock();
    //        while (MutexReopen is not null && CtsReopen is not null)
    //        {
    //            try
    //            {
    //                // AsyncLock can be locked asynchronously
    //                AwaitableDisposable<IDisposable> lockTask = MutexReopen.LockAsync(CtsReopen.Token);
    //                if (CtsReopen.IsCancellationRequested)
    //                    break;
    //                using (await lockTask)
    //                {
    //                    // It's safe to await while the lock is held
    //                    //await Task.Delay(TimeSpan.FromMilliseconds(WaitReopen)).ConfigureAwait(true);
    //                    callback?.Invoke();
    //                }
    //                WaitSync(WaitConfig.WaitReopen);
    //            }
    //            catch (TaskCanceledException tcex)
    //            {
    //                // Not the problem.
    //                Exception.Catch(null, ref tcex, false, filePath, lineNumber, memberName);
    //                WaitSync(WaitConfig.WaitException);
    //            }
    //            catch (Exception ex)
    //            {
    //                Exception.Catch(null, ex, false, filePath, lineNumber, memberName);
    //                WaitSync(WaitConfig.WaitException);
    //            }
    //        }
    //    });
    //}

    // OpenTaskRequest v.1
    //private void OpenTaskRequest(RequestCallback callback,
    //    [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    //{
    //    OpenTaskBase(TaskRequest, CtsRequest);
    //    CtsRequest = new CancellationTokenSource();

    //    TaskRequest = Task.Run(async () =>
    //    {
    //        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
    //        MutexRequest = new AsyncLock();
    //        while (MutexRequest is not null && CtsRequest is not null)
    //        {
    //            try
    //            {
    //                // AsyncLock can be locked asynchronously
    //                using (await MutexRequest.LockAsync(CtsRequest.Token))
    //                {
    //                    if (CtsRequest is null || CtsRequest.IsCancellationRequested)
    //                        break;
    //                    // It's safe to await while the lock is held
    //                    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);
    //                    callback?.Invoke();
    //                    //WaitSync(WaitRequest);
    //                }
    //            }
    //            catch (TaskCanceledException tcex)
    //            {
    //                // Not the problem.
    //                Exception.Catch(null, ref tcex, false, filePath, lineNumber, memberName);
    //            }
    //            catch (Exception ex)
    //            {
    //                Exception.Catch(null, ex, false, filePath, lineNumber, memberName);
    //                WaitSync(WaitException);
    //            }
    //        }
    //    });
    //}

    // OpenTaskRequest v.2
    //private void OpenTaskRequest(RequestCallback callback,
    //    [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    //{
    //    OpenTaskBase(TaskRequest, CtsRequest);
    //    CtsRequest = new CancellationTokenSource();

    //    TaskRequest = Task.Run(async () =>
    //    {
    //        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
    //        MutexRequest = new AsyncLock();
    //        while (MutexRequest is not null && CtsRequest is not null)
    //        {
    //            try
    //            {
    //                // AsyncLock can be locked asynchronously
    //                AwaitableDisposable<IDisposable> lockTask = MutexRequest.LockAsync(CtsRequest.Token);
    //                if (CtsRequest.IsCancellationRequested)
    //                    break;
    //                using (await lockTask)
    //                {
    //                    // It's safe to await while the lock is held
    //                    //await Task.Delay(TimeSpan.FromMilliseconds(WaitRequest)).ConfigureAwait(true);
    //                    callback?.Invoke();
    //                }
    //                WaitSync(WaitConfig.WaitRequest);
    //            }
    //            catch (TaskCanceledException tcex)
    //            {
    //                // Not the problem.
    //                Exception.Catch(null, ref tcex, false, filePath, lineNumber, memberName);
    //                WaitSync(WaitConfig.WaitException);
    //            }
    //            catch (Exception ex)
    //            {
    //                Exception.Catch(null, ex, false, filePath, lineNumber, memberName);
    //                WaitSync(WaitConfig.WaitException);
    //            }
    //        }
    //    });
    //}

    // OpenTaskRequest v.3
    //private void OpenTaskRequest(RequestCallback? callback)
    //{
    //    OpenTaskBase(TaskRequest, CtsRequest);

    //    TaskRequest = Task.Run(async () =>
    //    {
    //        RequestCount = 0;
    //        while (IsOpen && !IsRequestSuspend)
    //        {
    //            RequestCount++;
    //            MutexRequest ??= new();
    //            CtsRequest ??= new();
    //            try
    //            {
    //                // AsyncLock can be locked asynchronously
    //                AwaitableDisposable<IDisposable> lockTask = MutexRequest.LockAsync(CtsRequest.Token);
    //                using (await lockTask.ConfigureAwait(true))
    //                {
    //                    Config.WaitSync(Config.StopwatchRequest, Config.WaitRequest);

    //                    if (CtsRequest is null || CtsRequest.IsCancellationRequested)
    //                        continue;
    //                    // It's safe to await while the lock is held
    //                    callback?.Invoke();
    //                }
    //            }
    //            catch (TaskCanceledException)
    //            {
    //                // Not the problem.
    //            }
    //            catch (Exception ex)
    //            {
    //                DataAccessHelper.Instance.LogError(ex, NetUtils.GetLocalDeviceName(false), nameof(DataCore));
    //                Config.WaitSync(Config.WaitException);
    //            }
    //        }
    //    });
    //}

    // OpenTaskResponse v.1
    //private void OpenTaskResponse(ResponseCallback callback,
    //    [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    //{
    //    OpenTaskBase(TaskResponse, CtsResponse);
    //    CtsResponse = new CancellationTokenSource();

    //    TaskResponse = Task.Run(async () =>
    //    {
    //        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
    //        MutexResponse = new AsyncLock();
    //        while (MutexResponse is not null && CtsResponse is not null)
    //        {
    //            try
    //            {
    //                // AsyncLock can be locked asynchronously
    //                using (await MutexResponse.LockAsync(CtsResponse.Token))
    //                {
    //                    if (CtsResponse is null || CtsResponse.IsCancellationRequested)
    //                        break;
    //                    // It's safe to await while the lock is held
    //                    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);
    //                    callback?.Invoke();
    //                    //WaitSync(WaitResponse);
    //                }
    //            }
    //            catch (TaskCanceledException tcex)
    //            {
    //                // Not the problem.
    //                Exception.Catch(null, ref tcex, false, filePath, lineNumber, memberName);
    //            }
    //            catch (Exception ex)
    //            {
    //                Exception.Catch(null, ex, false, filePath, lineNumber, memberName);
    //                WaitSync(WaitException);
    //            }
    //        }
    //    });
    //}

    // OpenTaskResponse v.2
    //private void OpenTaskResponse(ResponseCallback callback,
    //    [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    //{
    //    OpenTaskBase(TaskResponse, CtsResponse);
    //    CtsResponse = new CancellationTokenSource();

    //    TaskResponse = Task.Run(async () =>
    //    {
    //        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
    //        MutexResponse = new AsyncLock();
    //        while (MutexResponse is not null && CtsResponse is not null)
    //        {
    //            try
    //            {
    //                // AsyncLock can be locked asynchronously
    //                AwaitableDisposable<IDisposable> lockTask = MutexResponse.LockAsync(CtsResponse.Token);
    //                if (CtsResponse.IsCancellationRequested)
    //                    break;
    //                using (await lockTask)
    //                {
    //                    // It's safe to await while the lock is held
    //                    //await Task.Delay(TimeSpan.FromMilliseconds(WaitResponse)).ConfigureAwait(true);
    //                    callback?.Invoke();
    //                }
    //                WaitSync(WaitConfig.WaitResponse);
    //            }
    //            catch (TaskCanceledException tcex)
    //            {
    //                // Not the problem.
    //                Exception.Catch(null, ref tcex, false, filePath, lineNumber, memberName);
    //                WaitSync(WaitConfig.WaitException);
    //            }
    //            catch (Exception ex)
    //            {
    //                Exception.Catch(null, ex, false, filePath, lineNumber, memberName);
    //                WaitSync(WaitConfig.WaitException);
    //            }
    //        }
    //    });
    //}

    // OpenTaskResponse v.3
    //private void OpenTaskResponse(ResponseCallback? callback)
    //{
    //    OpenTaskBase(TaskResponse, CtsResponse);

    //    TaskResponse = Task.Run(async () =>
    //    {
    //        ResponseCount = 0;
    //        while (IsOpen && !IsResponseSuspend)
    //        {
    //            ResponseCount++;
    //            MutexResponse ??= new();
    //            CtsResponse ??= new();
    //            try
    //            {
    //                // AsyncLock can be locked asynchronously
    //                AwaitableDisposable<IDisposable> lockTask = MutexResponse.LockAsync(CtsResponse.Token);
    //                using (await lockTask.ConfigureAwait(true))
    //                {
    //                    Config.WaitSync(Config.StopwatchResponse, Config.WaitResponse);

    //                    if (CtsResponse is null || CtsResponse.IsCancellationRequested)
    //                        continue;
    //                    // It's safe to await while the lock is held
    //                    callback?.Invoke();
    //                }
    //            }
    //            catch (TaskCanceledException)
    //            {
    //                // Not the problem.
    //            }
    //            catch (Exception ex)
    //            {
    //                DataAccessHelper.Instance.LogError(ex, NetUtils.GetLocalDeviceName(false), nameof(DataCore));
    //                Config.WaitSync(Config.WaitException);
    //            }
    //        }
    //    });
    //}

    public new void Close()
    {
        Cts?.Cancel();
        Config.WaitSync(Config.WaitClose);
        Mutex = null;
    }

    public void ReleaseManaged()
    {
        Cts?.Cancel();
        Cts?.Dispose();

        if (Tsk is not null)
        {
            Tsk.Wait(ManagerConfigModel.WaitLowLimit);
            if (Tsk.IsCompleted)
                Tsk.Dispose();
            Tsk = null;
        }
    }

    #endregion
}