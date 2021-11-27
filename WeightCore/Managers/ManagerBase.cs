// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL;
using DataProjectsCore.Helpers;
using Nito.AsyncEx;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using WeightCore.Helpers;

namespace WeightCore.Managers
{
    public class ManagerBase
    {
        #region Public and private fields and properties - Manager

        public readonly ExceptionHelper Exception = ExceptionHelper.Instance;
        public readonly DebugHelper Debug = DebugHelper.Instance;
        public readonly LogHelper Log = LogHelper.Instance;

        public int WaitReopen { get; set; }
        public int WaitRequest { get; set; }
        public int WaitResponse { get; set; }
        public int WaitException { get; set; }
        public int WaitClose { get; set; }
        public string ExceptionMsg { get; set; }
        public bool IsInit { get; set; }
        public bool IsResponse { get; set; }
        private object Locker { get; set; } = new();

        public AsyncLock MutexReopen { get; private set; } = new();
        public AsyncLock MutexRequest { get; private set; } = new();
        public AsyncLock MutexResponse { get; private set; } = new();

        public bool IsExecuteReopen { get; set; }
        public bool IsExecuteRequest { get; set; }
        public bool IsExecuteResponse { get; set; }

        public string ProgressString { get; set; }

        public ProjectsEnums.TaskType TaskType { get; set; } = ProjectsEnums.TaskType.Default;

        public delegate void InitCallback();
        public delegate void ReopenCallback();
        public delegate void RequestCallback();
        public delegate void ResponseCallback();
        public delegate void CloseCallback();

        #endregion

        #region Public and private methods

        // int waitReopen = 1_000, int waitResponse = 500, int waitRequest = 250, int waitClose = 2_000, int waitException = 1_000)
        // Massa: int waitReopen = 10_000, int waitResponse = 500, int waitRequest = 250, int waitClose = 2_000, int waitException = 1_000)
        public void Init(InitCallback initCallback,
            int waitReopen, int waitResponse, int waitRequest, int waitClose, int waitException)
        {
            lock (Locker)
            {
                if (IsInit)
                    return;
                IsInit = true;

                WaitReopen = waitReopen == 0 ? 5_000 : waitReopen;
                WaitResponse = waitResponse == 0 ? 500 : waitResponse;
                WaitRequest = waitRequest == 0 ? 250 : waitRequest;
                WaitClose = waitClose == 0 ? 5_000 : waitClose;
                WaitException = waitException == 0 ? 5_000 : waitException;

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

        public void DebugLog(string message,
            [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            if (Debug.IsDebug)
                Log.Information(message, filePath, memberName, lineNumber);
        }

        public void Open(ProjectsEnums.TaskType taskType, SqlViewModelEntity sqlViewModel,
            ReopenCallback reopenCallback, RequestCallback requestCallback, ResponseCallback responseCallback)
        {
            WaitSync(2_500);

            TaskType = taskType;
            if (sqlViewModel.IsTaskEnabled(TaskType))
            {
                OpenTaskReopen(reopenCallback);
                OpenTaskRequest(requestCallback);
                OpenTaskResponse(responseCallback);
            }
        }

        private void OpenTaskReopen(ReopenCallback reopenCallback)
        {
            // Task Reopen.
            _ = Task.Run(async () =>
            {
                // AsyncLock can be locked asynchronously
                using (await MutexReopen.LockAsync())
                {
                    // It's safe to await while the lock is held
                    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);

                    IsExecuteReopen = true;
                    while (IsExecuteReopen)
                    {
                        lock (Locker)
                        {
                            try
                            {
                                reopenCallback?.Invoke();
                                DebugLog($"{nameof(TaskType)} is opened");

                                //await Task.Delay(TimeSpan.FromMilliseconds(WaitReopen)).ConfigureAwait(false);
                                Thread.Sleep(WaitReopen);
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
                                Thread.Sleep(WaitException);
                            }
                            finally
                            {
                                System.Windows.Forms.Application.DoEvents();
                            }
                        }
                    }
                }
            });
        }

        private void OpenTaskRequest(RequestCallback requestCallback)
        {
            // Task Reopen.
            _ = Task.Run(async () =>
            {
                // AsyncLock can be locked asynchronously
                using (await MutexRequest.LockAsync())
                {
                    // It's safe to await while the lock is held
                    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);

                    IsExecuteRequest = true;
                    while (IsExecuteRequest)
                    {
                        lock (Locker)
                        {
                            try
                            {
                                requestCallback?.Invoke();
                                DebugLog($"{nameof(TaskType)} is opened");

                                //await Task.Delay(TimeSpan.FromMilliseconds(WaitReopen)).ConfigureAwait(false);
                                Thread.Sleep(WaitRequest);
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
                                Thread.Sleep(WaitException);
                            }
                            finally
                            {
                                System.Windows.Forms.Application.DoEvents();
                            }
                        }
                    }
                }
            });
        }

        private void OpenTaskResponse(ResponseCallback responseCallback)
        {
            // Task Reopen.
            _ = Task.Run(async () =>
            {
                // AsyncLock can be locked asynchronously
                using (await MutexResponse.LockAsync())
                {
                    // It's safe to await while the lock is held
                    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);

                    IsExecuteResponse = true;
                    while (IsExecuteResponse)
                    {
                        lock (Locker)
                        {
                            try
                            {
                                responseCallback?.Invoke();
                                DebugLog($"{nameof(TaskType)} is opened");

                                //await Task.Delay(TimeSpan.FromMilliseconds(WaitReopen)).ConfigureAwait(false);
                                Thread.Sleep(WaitResponse);
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
                                Thread.Sleep(WaitException);
                            }
                            finally
                            {
                                System.Windows.Forms.Application.DoEvents();
                            }
                        }
                    }
                }
            });
        }

        public void Close(CloseCallback closeCallback)
        {
            lock (Locker)
            {
                try
                {
                    IsExecuteReopen = false;
                    IsExecuteRequest = false;
                    IsExecuteResponse = false;

                    closeCallback?.Invoke();

                    System.Windows.Forms.Application.DoEvents();

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
        }

        #endregion
    }
}
