// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL;
using DataProjectsCore.Helpers;
using DataShareCore;
using Nito.AsyncEx;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using WeightCore.Helpers;

namespace WeightCore.Managers
{
    public class ManagerBase : AbstractDisposable
    {
        #region Public and private fields and properties - Manager

        public ProjectsEnums.TaskType TaskType { get; set; } = ProjectsEnums.TaskType.Default;
        public ExceptionHelper Exception { get; set; } = ExceptionHelper.Instance;
        public DebugHelper Debug { get; set; } = DebugHelper.Instance;
        public LogHelper Log { get; set; } = LogHelper.Instance;
        public AsyncLock MutexReopen { get; private set; } = new();
        public AsyncLock MutexRequest { get; private set; } = new();
        public AsyncLock MutexResponse { get; private set; } = new();
        public bool IsExecuteReopen { get; set; }
        public bool IsExecuteRequest { get; set; }
        public bool IsExecuteResponse { get; set; }
        public string ProgressString { get; set; }
        public ushort WaitReopen { get; set; }
        public ushort WaitRequest { get; set; }
        public ushort WaitResponse { get; set; }
        public ushort WaitException { get; set; }
        public ushort WaitClose { get; set; }
        public string ExceptionMsg { get; set; }
        public bool IsInit { get; set; }
        public bool IsResponse { get; set; }
        public delegate void InitCallback();
        public delegate void ReopenCallback();
        public delegate void RequestCallback();
        public delegate void ResponseCallback();
        public delegate void CloseCallback();
        public CloseCallback CloseMethod { get; set; }
        public Task TaskReopen { get; set; } = null;
        public Task TaskRequest { get; set; } = null;
        public Task TaskResponse { get; set; } = null;

        #endregion

        #region Public and private methods

        public ManagerBase()
        {
        }

        public void Init(ProjectsEnums.TaskType taskType, InitCallback initCallback,
            ushort waitReopen = 0, ushort waitRequest = 0, ushort waitResponse = 0, ushort waitClose = 0, ushort waitException = 0)
        {
            Init(
                () => { ReleaseManaged(); },
                () => { }
            );
            lock (this)
            {
                if (IsInit)
                    return;
                IsInit = true;
                TaskType = taskType;

                WaitReopen = waitReopen == 0 ? (ushort)2_000 : waitReopen;
                WaitRequest = waitRequest == 0 ? (ushort)250 : waitRequest;
                WaitResponse = waitResponse == 0 ? (ushort)500 : waitResponse;
                WaitClose = waitClose == 0 ? (ushort)2_000 : waitClose;
                WaitException = waitException == 0 ? (ushort)2_000 : waitException;

                initCallback?.Invoke();
            }
        }

        public static void WaitSync(ushort miliseconds)
        {
            if (miliseconds < 50)
                miliseconds = 50;
            if (miliseconds > 10_000)
                miliseconds = 10_000;
            Stopwatch sw = Stopwatch.StartNew();
            while (sw.Elapsed.TotalMilliseconds < miliseconds)
            {
                Thread.Sleep(50);
                System.Windows.Forms.Application.DoEvents();
            }
        }

        public void DebugLog(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            CheckIfDisposed();
            if (Debug.IsDebug)
                Log.Information(message, filePath, memberName, lineNumber);
        }

        public void Open(SqlViewModelEntity sqlViewModel,
            ReopenCallback reopenCallback, RequestCallback requestCallback, ResponseCallback responseCallback)
        {
            if (sqlViewModel.IsTaskEnabled(TaskType))
            {
                OpenTaskReopen(reopenCallback);
                OpenTaskRequest(requestCallback);
                OpenTaskResponse(responseCallback);
            }
        }

        private void OpenTaskReopen(ReopenCallback callback)
        {
            CheckIfDisposed();
            TaskReopen?.Dispose();
            TaskReopen = Task.Run(async () =>
            {
                IsExecuteReopen = true;
                while (IsExecuteReopen)
                {
                    try
                    {
                        // AsyncLock can be locked asynchronously
                        using (await MutexReopen.LockAsync())
                        {
                            // It's safe to await while the lock is held
                            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);
                            callback?.Invoke();
                            WaitSync(WaitReopen);
                        }
                    }
                    catch (TaskCanceledException)
                    {
                        // Console.WriteLine(tcex.Message);
                        // Not the problem.
                    }
                    catch (Exception ex)
                    {
                        Exception.Catch(null, ref ex, false);
                        //await Task.Delay(TimeSpan.FromMilliseconds(WaitException)).ConfigureAwait(false);
                        //Thread.Sleep(WaitException);
                        WaitSync(WaitException);
                    }
                    finally
                    {
                        System.Windows.Forms.Application.DoEvents();
                    }
                }
            });
        }

        private void OpenTaskRequest(RequestCallback callback)
        {
            CheckIfDisposed();
            TaskRequest?.Dispose();
            TaskRequest = Task.Run(async () =>
            {
                IsExecuteRequest = true;
                while (IsExecuteRequest)
                {
                    try
                    {
                        // AsyncLock can be locked asynchronously
                        using (await MutexRequest.LockAsync())
                        {
                            // It's safe to await while the lock is held
                            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);
                            callback?.Invoke();
                            WaitSync(WaitRequest);
                        }
                    }
                    catch (TaskCanceledException)
                    {
                        // Console.WriteLine(tcex.Message);
                        // Not the problem.
                    }
                    catch (Exception ex)
                    {
                        Exception.Catch(null, ref ex, false);
                        //await Task.Delay(TimeSpan.FromMilliseconds(WaitException)).ConfigureAwait(false);
                        //Thread.Sleep(WaitException);
                        WaitSync(WaitException);
                    }
                    finally
                    {
                        System.Windows.Forms.Application.DoEvents();
                    }
                }
            });
        }

        private void OpenTaskResponse(ResponseCallback callback)
        {
            CheckIfDisposed();
            TaskResponse?.Dispose();
            TaskResponse = Task.Run(async () =>
            {
                IsExecuteResponse = true;
                while (IsExecuteResponse)
                {
                    try
                    {
                        // AsyncLock can be locked asynchronously
                        using (await MutexResponse.LockAsync())
                        {
                            // It's safe to await while the lock is held
                            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);
                            callback?.Invoke();
                            WaitSync(WaitResponse);
                        }
                    }
                    catch (TaskCanceledException)
                    {
                        // Console.WriteLine(tcex.Message);
                        // Not the problem.
                    }
                    catch (Exception ex)
                    {
                        Exception.Catch(null, ref ex, false);
                        //await Task.Delay(TimeSpan.FromMilliseconds(WaitException)).ConfigureAwait(false);
                        //Thread.Sleep(WaitException);
                        WaitSync(WaitException);
                    }
                    finally
                    {
                        System.Windows.Forms.Application.DoEvents();
                    }
                }
            });
        }

        private void ReleaseManaged()
        {
            try
            {
                IsExecuteReopen = false;
                IsExecuteRequest = false;
                IsExecuteResponse = false;

                CloseMethod?.Invoke();
                TaskReopen?.Dispose();
                TaskReopen = null;
                TaskRequest?.Dispose();
                TaskRequest = null;
                TaskResponse?.Dispose();
                TaskResponse = null;
                
                WaitSync(WaitClose);
                DebugLog($"{nameof(TaskType)} is closed");
            }
            catch (Exception ex)
            {
                Exception.Catch(null, ref ex, false);
            }
            finally
            {
                System.Windows.Forms.Application.DoEvents();
            }
        }

        #endregion
    }
}
