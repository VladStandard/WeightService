// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;
using Nito.AsyncEx;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using WeightCore.Helpers;
using static DataCore.Models.IDisposableBase;

namespace WeightCore.Managers
{
    public class ManagerBase : DisposableBase, IDisposableBase
    {
        #region Public and private fields and properties - Manager

        public ProjectsEnums.TaskType TaskType { get; set; } = ProjectsEnums.TaskType.Default;
        public ExceptionHelper Exception { get; set; } = ExceptionHelper.Instance;
        public DebugHelper Debug { get; set; } = DebugHelper.Instance;
        public AsyncLock MutexReopen { get; private set; }
        public AsyncLock MutexRequest { get; private set; }
        public AsyncLock MutexResponse { get; private set; }
        public CancellationTokenSource CtsReopen { get; set; }
        public CancellationTokenSource CtsRequest { get; set; }
        public CancellationTokenSource CtsResponse { get; set; }
        public ManagerWaitConfig WaitConfig { get; set; }
        public string ExceptionMsg { get; set; }
        public bool IsResponse { get; set; }
        public Task TaskReopen { get; set; } = null;
        public Task TaskRequest { get; set; } = null;
        public Task TaskResponse { get; set; } = null;
        public bool IsOpenedMethod { get; set; }
        public bool IsClosedMethod { get; set; }
        private readonly object _locker = new();

        #endregion

        #region Public and private methods

        public ManagerBase() : base()
        {
            Init(Close, ReleaseManaged, ReleaseUnmanaged);
            WaitConfig = new();
        }

        public void Init(ProjectsEnums.TaskType taskType, InitCallback initCallback, ManagerWaitConfig waitConfig)
        {
            lock (_locker)
            {
                TaskType = taskType;
                WaitConfig = waitConfig;
                initCallback?.Invoke();
            }
        }

        public static void WaitSync(ushort miliseconds, Task task = null)
        {
            if (miliseconds < 50)
                miliseconds = 50;
            if (miliseconds > 5_000)
                miliseconds = 5_000;
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Restart();
            while (stopwatch.Elapsed.TotalMilliseconds < miliseconds)
            {
                if (task != null && task.Status != TaskStatus.WaitingForActivation)
                    if (task.Status == TaskStatus.Canceled || task.Status == TaskStatus.Faulted)
                        break;
                Thread.Sleep(50);
                System.Windows.Forms.Application.DoEvents();
            }
            stopwatch.Stop();
        }

        public void Open(ReopenCallback reopenCallback, RequestCallback requestCallback, ResponseCallback responseCallback)
        {
            Close();
            if (IsOpenedMethod) return;
            IsOpenedMethod = true;
            IsClosedMethod = false;
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

        private void OpenTaskBase(Task task, CancellationTokenSource cts)
        {
            CheckIsDisposed();
            if (task == null) return;

            cts?.Cancel();
            task.Wait(WaitConfig.WaitClose);
            if (task.IsCompleted)
                task.Dispose();
        }

        //[Obsolete(@"Use OpenTaskReopen")]
        //private void OpenTaskReopenDeprecated(ReopenCallback callback,
        //    [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        //{
        //    OpenTaskBase(TaskReopen, CtsReopen);
        //    CtsReopen = new CancellationTokenSource();

        //    TaskReopen = Task.Run(async () =>
        //    {
        //        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        //        MutexReopen = new AsyncLock();
        //        while (MutexReopen != null && CtsReopen != null)
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
        //                Exception.Catch(null, ref ex, false, filePath, lineNumber, memberName);
        //                WaitSync(WaitException);
        //            }
        //        }
        //    });
        //}

        private void OpenTaskReopen(ReopenCallback callback,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            OpenTaskBase(TaskReopen, CtsReopen);
            CtsReopen = new CancellationTokenSource();

            TaskReopen = Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
                MutexReopen = new AsyncLock();
                while (MutexReopen != null && CtsReopen != null)
                {
                    try
                    {
                        // AsyncLock can be locked asynchronously
                        AwaitableDisposable<IDisposable> lockTask = MutexReopen.LockAsync(CtsReopen.Token);
                        if (CtsReopen.IsCancellationRequested)
                            break;
                        using (await lockTask)
                        {
                            // It's safe to await while the lock is held
                            //await Task.Delay(TimeSpan.FromMilliseconds(WaitReopen)).ConfigureAwait(true);
                            callback?.Invoke();
                        }
                        WaitSync(WaitConfig.WaitReopen);
                    }
                    catch (TaskCanceledException tcex)
                    {
                        // Not the problem.
                        Exception.Catch(null, ref tcex, false, filePath, lineNumber, memberName);
                        WaitSync(WaitConfig.WaitException);
                    }
                    catch (Exception ex)
                    {
                        Exception.Catch(null, ref ex, false, filePath, lineNumber, memberName);
                        WaitSync(WaitConfig.WaitException);
                    }
                }
            });
        }

        //[Obsolete(@"Use OpenTaskRequest")]
        //private void OpenTaskRequestDeprecated(RequestCallback callback,
        //    [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        //{
        //    OpenTaskBase(TaskRequest, CtsRequest);
        //    CtsRequest = new CancellationTokenSource();

        //    TaskRequest = Task.Run(async () =>
        //    {
        //        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        //        MutexRequest = new AsyncLock();
        //        while (MutexRequest != null && CtsRequest != null)
        //        {
        //            try
        //            {
        //                // AsyncLock can be locked asynchronously
        //                using (await MutexRequest.LockAsync(CtsRequest.Token))
        //                {
        //                    if (CtsRequest == null || CtsRequest.IsCancellationRequested)
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
        //                Exception.Catch(null, ref ex, false, filePath, lineNumber, memberName);
        //                WaitSync(WaitException);
        //            }
        //        }
        //    });
        //}

        private void OpenTaskRequest(RequestCallback callback,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            OpenTaskBase(TaskRequest, CtsRequest);
            CtsRequest = new CancellationTokenSource();

            TaskRequest = Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
                MutexRequest = new AsyncLock();
                while (MutexRequest != null && CtsRequest != null)
                {
                    try
                    {
                        // AsyncLock can be locked asynchronously
                        AwaitableDisposable<IDisposable> lockTask = MutexRequest.LockAsync(CtsRequest.Token);
                        if (CtsRequest.IsCancellationRequested)
                            break;
                        using (await lockTask)
                        {
                            // It's safe to await while the lock is held
                            //await Task.Delay(TimeSpan.FromMilliseconds(WaitRequest)).ConfigureAwait(true);
                            callback?.Invoke();
                        }
                        WaitSync(WaitConfig.WaitRequest);
                    }
                    catch (TaskCanceledException tcex)
                    {
                        // Not the problem.
                        Exception.Catch(null, ref tcex, false, filePath, lineNumber, memberName);
                        WaitSync(WaitConfig.WaitException);
                    }
                    catch (Exception ex)
                    {
                        Exception.Catch(null, ref ex, false, filePath, lineNumber, memberName);
                        WaitSync(WaitConfig.WaitException);
                    }
                }
            });
        }

        //[Obsolete(@"Deprecated method")]
        //private void OpenTaskResponseDeprecated(ResponseCallback callback,
        //    [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        //{
        //    OpenTaskBase(TaskResponse, CtsResponse);
        //    CtsResponse = new CancellationTokenSource();

        //    TaskResponse = Task.Run(async () =>
        //    {
        //        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        //        MutexResponse = new AsyncLock();
        //        while (MutexResponse != null && CtsResponse != null)
        //        {
        //            try
        //            {
        //                // AsyncLock can be locked asynchronously
        //                using (await MutexResponse.LockAsync(CtsResponse.Token))
        //                {
        //                    if (CtsResponse == null || CtsResponse.IsCancellationRequested)
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
        //                Exception.Catch(null, ref ex, false, filePath, lineNumber, memberName);
        //                WaitSync(WaitException);
        //            }
        //        }
        //    });
        //}

        private void OpenTaskResponse(ResponseCallback callback,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            OpenTaskBase(TaskResponse, CtsResponse);
            CtsResponse = new CancellationTokenSource();

            TaskResponse = Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
                MutexResponse = new AsyncLock();
                while (MutexResponse != null && CtsResponse != null)
                {
                    try
                    {
                        // AsyncLock can be locked asynchronously
                        AwaitableDisposable<IDisposable> lockTask = MutexResponse.LockAsync(CtsResponse.Token);
                        if (CtsResponse.IsCancellationRequested)
                            break;
                        using (await lockTask)
                        {
                            // It's safe to await while the lock is held
                            //await Task.Delay(TimeSpan.FromMilliseconds(WaitResponse)).ConfigureAwait(true);
                            callback?.Invoke();
                        }
                        WaitSync(WaitConfig.WaitResponse);
                    }
                    catch (TaskCanceledException tcex)
                    {
                        // Not the problem.
                        Exception.Catch(null, ref tcex, false, filePath, lineNumber, memberName);
                        WaitSync(WaitConfig.WaitException);
                    }
                    catch (Exception ex)
                    {
                        Exception.Catch(null, ref ex, false, filePath, lineNumber, memberName);
                        WaitSync(WaitConfig.WaitException);
                    }
                }
            });
        }

        public new void Close()
        {
            if (IsClosedMethod) return;
            IsOpenedMethod = false;
            IsClosedMethod = true;
            CheckIsDisposed();

            CtsReopen?.Cancel();
            CtsRequest?.Cancel();
            CtsResponse?.Cancel();

            WaitSync(WaitConfig.WaitClose);

            MutexReopen = null;
            MutexRequest = null;
            MutexResponse = null;

            base.Close();
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

            if (TaskReopen != null)
            {
                TaskReopen.Wait(100);
                if (TaskReopen.IsCompleted)
                    TaskReopen.Dispose();
                TaskReopen = null;
            }

            if (TaskRequest != null)
            {
                TaskRequest.Wait(100);
                if (TaskRequest.IsCompleted)
                    TaskRequest.Dispose();
                TaskRequest = null;
            }

            if (TaskResponse != null)
            {
                TaskResponse.Wait(100);
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

        #endregion
    }
}
