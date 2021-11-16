// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL;
using DataProjectsCore.DAL.TableModels;
using DataProjectsCore.Helpers;
using Nito.AsyncEx;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using WeightCore.Helpers;
using WeightCore.Print.Tsc;

namespace WeightCore.Managers
{
    public class TaskManagerHelper
    {
        #region Design pattern "Lazy Singleton"

        private static TaskManagerHelper _instance;
        public static TaskManagerHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Constructor and destructor

        ~TaskManagerHelper()
        {
            Close();
            ClosePrintManager();
        }

        #endregion

        #region Public and private fields and properties

        private readonly ExceptionHelper _exception = ExceptionHelper.Instance;
        private readonly DebugHelper _debug = DebugHelper.Instance;
        public SqlViewModelEntity SqlViewModel { get; set; } = SqlViewModelEntity.Instance;
        private readonly LogHelper _log = LogHelper.Instance;

        // MassaManager.
        public MassaManagerHelper MassaManager = MassaManagerHelper.Instance;
        private readonly AsyncLock _mutexMassaReOpen = new();
        private readonly AsyncLock _mutexMassaRequest = new();
        private readonly AsyncLock _mutexMassaResponse = new();
        public bool IsExecuteMassaReopen { get; private set; }
        public bool IsExecuteMassaRequest { get; private set; }
        public bool IsExecuteMassaResponse { get; private set; }
        public char MassaManagerProgressChar { get; private set; }
        public string MassaManagerProgressString { get; set; }
        public string MassaQueriesProgressString { get; set; }
        public string MassaRequestProgressString { get; set; }
        public string MassaResponseProgressString { get; set; }

        // PrintManager.
        public PrintManagerHelper PrintManager = PrintManagerHelper.Instance;
        private readonly AsyncLock _mutexPrintReOpen = new();
        private readonly AsyncLock _mutexPrintRequest = new();
        private readonly AsyncLock _mutexPrintResponse = new();
        public bool IsExecutePrintReopen { get; private set; }
        public bool IsExecutePrintRequest { get; private set; }
        public bool IsExecutePrintResponse { get; private set; }
        public string PrintManagerProgressString { get; set; }
        private bool IsTscPrinter { get; set; }

        // MemoryManager.
        public MemoryManagerHelper MemoryManager = MemoryManagerHelper.Instance;
        private readonly AsyncLock _mutexMemoryReopen = new();
        public bool IsExecuteMemoryReopen { get; private set; }
        public string MemoryManagerProgressString { get; set; }

        #endregion

        #region Public and private methods

        public void Open(SqlViewModelEntity sqlViewModel, bool isTscPrinter, ScaleDirect currentScale)
        {
            try
            {
                IsTscPrinter = isTscPrinter;

                bool taskEnabled = false;

                taskEnabled = sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.MassaManager);
                if (taskEnabled)
                {
                    MassaManager.Init(currentScale.DeviceComPort, currentScale.DeviceReceiveTimeout, currentScale.DeviceSendTimeout);
                    TaskRunMassaManagerReopen();
                    TaskRunMassaManagerRequest();
                    TaskRunMassaManagerResponse();
                }

                taskEnabled = sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.MemoryManager);
                if (taskEnabled)
                {
                    MemoryManager.Init();
                    TaskRunMemoryManagerReopen();
                }
            }
            catch (Exception ex)
            {
                _exception.Catch(null, ref ex);
            }
        }

        public void OpenPrintManager(TscPrintControlHelper.Callback callbackPrintManagerClose,
            SqlViewModelEntity sqlViewModel, bool isTscPrinter, ScaleDirect currentScale)
        {
            try
            {
                IsTscPrinter = isTscPrinter;

                bool taskEnabled = sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.PrintManager);
                if (taskEnabled)
                {
                    PrintManager.Init(currentScale.ZebraPrinter.Name, currentScale.ZebraPrinter.Ip, currentScale.ZebraPrinter.Port);
                    TaskRunPrintManagerReopen(callbackPrintManagerClose);
                }
            }
            catch (Exception ex)
            {
                _exception.Catch(null, ref ex);
            }
        }

        public void Close()
        {
            try
            {
                IsExecuteMassaReopen = false;
                IsExecuteMassaRequest = false;
                IsExecuteMassaResponse = false;
                IsExecuteMemoryReopen = false;
                System.Windows.Forms.Application.DoEvents();

                MemoryManager.Close();
                MassaManager.Close();

                DebugLog($"{nameof(MemoryManager)} is closed");
                DebugLog($"{nameof(MassaManager)} is closed");
            }
            catch (Exception ex)
            {
                _exception.Catch(null, ref ex);
            }
            finally
            {
                System.Windows.Forms.Application.DoEvents();
            }
        }

        public void ClosePrintManager()
        {
            try
            {
                IsExecutePrintReopen = false;
                System.Windows.Forms.Application.DoEvents();

                PrintManager.Close();

                DebugLog($"{nameof(PrintManager)} is closed");
            }
            catch (Exception ex)
            {
                _exception.Catch(null, ref ex);
            }
            finally
            {
                System.Windows.Forms.Application.DoEvents();
            }
        }

        public void TaskRunMassaManagerReopen()
        {
            _ = Task.Run(async () =>
            {
                // AsyncLock can be locked asynchronously
                using (await _mutexMassaReOpen.LockAsync())
                {
                    // It's safe to await while the lock is held
                    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);

                    IsExecuteMassaReopen = true;
                    while (IsExecuteMassaReopen)
                    {
                        try
                        {
                            MassaManager.MassaDevice.Open();
                            await Task.Delay(TimeSpan.FromMilliseconds(MassaManager.WaitReopen)).ConfigureAwait(false);
                        }
                        catch (TaskCanceledException)
                        {
                            // Console.WriteLine(tcex.Message);
                            // Not the problem.
                        }
                        catch (Exception ex)
                        {
                            _exception.Catch(null, ref ex);
                            await Task.Delay(TimeSpan.FromMilliseconds(MassaManager.WaitException)).ConfigureAwait(false);
                        }
                    }
                }
            });
        }

        public void TaskRunMassaManagerRequest()
        {
            _ = Task.Run(async () =>
            {
                // AsyncLock can be locked asynchronously
                using (await _mutexMassaRequest.LockAsync())
                {
                    // It's safe to await while the lock is held
                    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);

                    IsExecuteMassaRequest = true;
                    while (IsExecuteMassaRequest)
                    {
                        try
                        {
                            if (MassaManager.MassaDevice.IsConnected)
                                MassaManager.GetMassa();
                            else
                                MassaManager.ClearRequests(0);
                            await Task.Delay(TimeSpan.FromMilliseconds(MassaManager.WaitRequest)).ConfigureAwait(false);
                        }
                        catch (TaskCanceledException)
                        {
                            // Console.WriteLine(tcex.Message);
                            // Not the problem.
                        }
                        catch (Exception ex)
                        {
                            _exception.Catch(null, ref ex);
                            await Task.Delay(TimeSpan.FromMilliseconds(MassaManager.WaitException)).ConfigureAwait(false);
                        }
                    }
                }
            });
        }

        public void TaskRunMassaManagerResponse()
        {
            _ = Task.Run(async () =>
            {
                // AsyncLock can be locked asynchronously
                using (await _mutexMassaResponse.LockAsync())
                {
                    // It's safe to await while the lock is held
                    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);

                    IsExecuteMassaResponse = true;
                    while (IsExecuteMassaResponse)
                    {
                        try
                        {
                            if (MassaManager.MassaDevice.IsConnected)
                                MassaManager.OpenResponse();
                            else
                                MassaManager.ResetMassa();
                            await Task.Delay(TimeSpan.FromMilliseconds(MassaManager.WaitResponse)).ConfigureAwait(false);
                        }
                        catch (TaskCanceledException)
                        {
                            // Console.WriteLine(tcex.Message);
                            // Not the problem.
                        }
                        catch (Exception ex)
                        {
                            _exception.Catch(null, ref ex);
                            await Task.Delay(TimeSpan.FromMilliseconds(MassaManager.WaitException)).ConfigureAwait(false);
                        }
                    }
                }
            });
        }

        public void TaskRunPrintManagerReopen(TscPrintControlHelper.Callback callbackPrintManagerClose)
        {
            _ = Task.Run(async () =>
            {
                // AsyncLock can be locked asynchronously
                using (await _mutexPrintReOpen.LockAsync())
                {
                    // It's safe to await while the lock is held
                    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);

                    IsExecutePrintReopen = true;
                    while (IsExecutePrintReopen)
                    {
                        try
                        {
                            PrintManager.Open(IsTscPrinter, callbackPrintManagerClose);
                            await Task.Delay(TimeSpan.FromMilliseconds(PrintManager.WaitReopen)).ConfigureAwait(false);
                        }
                        catch (TaskCanceledException)
                        {
                            // Console.WriteLine(tcex.Message);
                            // Not the problem.
                        }
                        catch (Exception ex)
                        {
                            _exception.Catch(null, ref ex);
                            await Task.Delay(TimeSpan.FromMilliseconds(PrintManager.WaitException)).ConfigureAwait(false);
                        }
                    }
                }
            });
        }

        public void TaskRunMemoryManagerReopen()
        {
            _ = Task.Run(async () =>
            {
                // AsyncLock can be locked asynchronously
                using (await _mutexPrintReOpen.LockAsync())
                {
                    // It's safe to await while the lock is held
                    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);

                    IsExecutePrintReopen = true;
                    while (IsExecutePrintReopen)
                    {
                        try
                        {
                            MemoryManager.Open();
                            await Task.Delay(TimeSpan.FromMilliseconds(PrintManager.WaitReopen)).ConfigureAwait(false);
                        }
                        catch (TaskCanceledException)
                        {
                            // Console.WriteLine(tcex.Message);
                            // Not the problem.
                        }
                        catch (Exception ex)
                        {
                            _exception.Catch(null, ref ex);
                            await Task.Delay(TimeSpan.FromMilliseconds(PrintManager.WaitException)).ConfigureAwait(false);
                        }
                    }
                }
            });
        }

        private void DebugLog(string message, 
            [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            if (_debug.IsDebug)
                _log.Information(message, filePath, memberName, lineNumber);
        }

        #endregion
    }
}
