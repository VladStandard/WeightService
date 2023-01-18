// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading.Tasks;
using DataCore.Models;
using DataCore.Protocols;
using DataCore.Sql.Core.Helpers;
using Nito.AsyncEx;
using static DataCore.Models.IDisposableBase;

namespace DataCore.Managers;

public class ManagerBase : DisposableBase, IDisposableBase
{
	#region Public and private fields and properties - Manager

	private TaskTypeEnum TaskType { get; set; } = TaskTypeEnum.Default;
	private AsyncLock? MutexReopen { get; set; }
	private AsyncLock? MutexRequest { get; set; }
	private AsyncLock? MutexResponse { get; set; }
	private CancellationTokenSource? CtsReopen { get; set; }
	private CancellationTokenSource? CtsRequest { get; set; }
	private CancellationTokenSource? CtsResponse { get; set; }
	private ManagerConfigModel Config { get; set; }
	private Task? TaskReopen { get; set; }
	private Task? TaskRequest { get; set; }
	private Task? TaskResponse { get; set; }
    public bool IsReopenSuspend { get; set; }
    public bool IsRequestSuspend { get; set; }
    public bool IsResponseSuspend { get; set; }
    private readonly object _locker = new();

	#endregion

	#region Public and private methods

    protected ManagerBase() : base()
	{
		Init(Close, ReleaseManaged, ReleaseUnmanaged);
		Config = new();
	}

    protected void Init(TaskTypeEnum taskType, InitCallback? initCallback, ManagerConfigModel waitConfig)
	{
		lock (_locker)
		{
			TaskType = taskType;
			Config = waitConfig;
			initCallback?.Invoke();
		}
	}

    protected void Open(ReopenCallback? reopenCallback, RequestCallback? requestCallback, ResponseCallback? responseCallback)
	{
		Close();
		Open();

		MutexReopen = null;
		MutexRequest = null;
		MutexResponse = null;

		CtsReopen = null;
		CtsRequest = null;
		CtsResponse = null;

		OpenTaskReopen(reopenCallback);
		OpenTaskRequest(requestCallback);
		OpenTaskResponse(responseCallback);
	}

	private void OpenTaskBase(Task? task, CancellationTokenSource? cts)
	{
		CheckIsDisposed();
		if (task is null) return;

		cts?.Cancel();
		task.Wait(Config.WaitClose);
		if (task.IsCompleted)
			task.Dispose();
	}

	// OpenTaskReopen v.1
	//private void OpenTaskReopen(ReopenCallback callback,
	//    [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
	//{
	//    OpenTaskBase(TaskReopen, CtsReopen);
	//    CtsReopen = new CancellationTokenSource();

	//    TaskReopen = Task.Run(async () =>
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
	//    OpenTaskBase(TaskReopen, CtsReopen);
	//    CtsReopen = new CancellationTokenSource();

	//    TaskReopen = Task.Run(async () =>
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

	private void OpenTaskReopen(ReopenCallback? callback)
	{
		OpenTaskBase(TaskReopen, CtsReopen);

		TaskReopen = Task.Run(async () =>
		{
			ReopenCount = 0;
			while (IsOpen && !IsReopenSuspend)
			{
				ReopenCount++;
				MutexReopen ??= new();
				CtsReopen ??= new();
				try
				{
					// AsyncLock can be locked asynchronously
					AwaitableDisposable<IDisposable> lockTask = MutexReopen.LockAsync(CtsReopen.Token);
					using (await lockTask.ConfigureAwait(true))
					{
						Config.WaitSync(Config.StopwatchReopen, Config.WaitReopen);

						if (CtsReopen is null || CtsReopen.IsCancellationRequested)
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

	private void OpenTaskRequest(RequestCallback? callback)
	{
		OpenTaskBase(TaskRequest, CtsRequest);

		TaskRequest = Task.Run(async () =>
		{
			RequestCount = 0;
			while (IsOpen && !IsRequestSuspend)
			{
				RequestCount++;
				MutexRequest ??= new();
				CtsRequest ??= new();
				try
				{
					// AsyncLock can be locked asynchronously
					AwaitableDisposable<IDisposable> lockTask = MutexRequest.LockAsync(CtsRequest.Token);
					using (await lockTask.ConfigureAwait(true))
					{
						Config.WaitSync(Config.StopwatchRequest, Config.WaitRequest);

						if (CtsRequest is null || CtsRequest.IsCancellationRequested)
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
					DataAccessHelper.Instance.LogError(ex, NetUtils.GetLocalDeviceName(false), nameof(DataCore));
					Config.WaitSync(Config.WaitException);
				}
			}
		});
	}

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

	private void OpenTaskResponse(ResponseCallback? callback)
	{
		OpenTaskBase(TaskResponse, CtsResponse);

		TaskResponse = Task.Run(async () =>
		{
			ResponseCount = 0;
			while (IsOpen && !IsResponseSuspend)
			{
				ResponseCount++;
				MutexResponse ??= new();
				CtsResponse ??= new();
				try
				{
					// AsyncLock can be locked asynchronously
					AwaitableDisposable<IDisposable> lockTask = MutexResponse.LockAsync(CtsResponse.Token);
					using (await lockTask.ConfigureAwait(true))
					{
						Config.WaitSync(Config.StopwatchResponse, Config.WaitResponse);

						if (CtsResponse is null || CtsResponse.IsCancellationRequested)
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
					DataAccessHelper.Instance.LogError(ex, NetUtils.GetLocalDeviceName(false), nameof(DataCore));
					Config.WaitSync(Config.WaitException);
				}
			}
		});
	}

	public new void Close()
	{
		base.Close();

		CheckIsDisposed();

		CtsReopen?.Cancel();
		CtsRequest?.Cancel();
		CtsResponse?.Cancel();

		Config.WaitSync(Config.WaitClose);

		MutexReopen = null;
		MutexRequest = null;
		MutexResponse = null;

	}

	public void ReleaseManaged()
	{
		Close();

		CtsReopen?.Cancel();
		CtsReopen?.Dispose();
		CtsRequest?.Cancel();
		CtsRequest?.Dispose();
		CtsResponse?.Cancel();
		CtsResponse?.Dispose();

		if (TaskReopen is not null)
		{
			TaskReopen.Wait(ManagerConfigModel.WaitLowLimit);
			if (TaskReopen.IsCompleted)
				TaskReopen.Dispose();
			TaskReopen = null;
		}

		if (TaskRequest is not null)
		{
			TaskRequest.Wait(ManagerConfigModel.WaitLowLimit);
			if (TaskRequest.IsCompleted)
				TaskRequest.Dispose();
			TaskRequest = null;
		}

		if (TaskResponse is not null)
		{
			TaskResponse.Wait(ManagerConfigModel.WaitLowLimit);
			if (TaskResponse.IsCompleted)
				TaskResponse.Dispose();
			TaskResponse = null;
		}

		CtsReopen = null;
		CtsRequest = null;
		CtsResponse = null;
	}

	public void ReleaseUnmanaged()
	{
		//
	}

    public void Suspend()
    {
        IsReopenSuspend = true;
        IsRequestSuspend = true;
        IsResponseSuspend = true;
    }

    public void UnSuspend()
    {
        IsReopenSuspend = false;
        IsRequestSuspend = false;
        IsResponseSuspend = false;
    }

    #endregion
}