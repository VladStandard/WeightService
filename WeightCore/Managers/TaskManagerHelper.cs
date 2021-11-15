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
using WeightCore.Print;
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
        private bool IsTscPrinter { get; set; }

        public DeviceManagerHelper DeviceManager = DeviceManagerHelper.Instance;
        public char DeviceManagerProgressChar { get; set; }
        private readonly AsyncLock _mutexDeviceManager = new();

        public MemoryManagerHelper MemoryManager = MemoryManagerHelper.Instance;
        public char MemoryManagerProgressChar { get; set; }
        public string MemoryManagerProgressString { get; set; }
        private readonly AsyncLock _mutexMemoryManager = new();

        public PrintManagerHelper PrintManager = PrintManagerHelper.Instance;
        public char PrintManagerProgressChar { get; set; }
        public string PrintManagerProgressString { get; set; }
        private readonly AsyncLock _mutexPrintManager = new();

        public MassaManagerHelper MassaManager = MassaManagerHelper.Instance;
        public char MassaManagerProgressChar { get; set; }
        public string MassaManagerProgressString { get; set; }
        public string MassaQueriesProgressString { get; set; }
        public string MassaRequestProgressString { get; set; }
        public string MassaResponseProgressString { get; set; }
        
        private readonly AsyncLock _mutexMassaResponse = new();
        private readonly AsyncLock _mutexMassaRequest = new();
        private readonly AsyncLock _mutexMassaReOpen = new();

        //public delegate void CallbackButtonSetZero(object sender, EventArgs e);
        public bool IsExecuteMassaReopen { get; set; }
        public bool IsExecuteMassaRequest { get; set; }
        public bool IsExecuteMassaResponse { get; set; }

        #endregion

        #region Public and private methods

        public void Open(SqlViewModelEntity sqlViewModel, bool isTscPrinter, ScaleDirect currentScale)
        {
            try
            {
                IsTscPrinter = isTscPrinter;
                
                bool taskEnabled = sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.DeviceManager);
                if (taskEnabled)
                    TaskRunDeviceManager();

                taskEnabled = sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.MemoryManager);
                if (taskEnabled)
                    TaskRunMemoryManager();
                
                taskEnabled = sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.MassaManager);
                if (taskEnabled)
                {
                    MassaManager.Init(currentScale.DeviceComPort, currentScale.DeviceReceiveTimeout, currentScale.DeviceSendTimeout);
                    TaskRunMassaManagerReOpen();
                    TaskRunMassaManagerRequest();
                    TaskRunMassaManagerResponse();
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
                    PrintManager.Init(currentScale.ZebraPrinter.Name, currentScale.ZebraPrinter.Ip, currentScale.ZebraPrinter.Port, 1_000, 5_000, 5_000);
                    TaskRunPrintManager(callbackPrintManagerClose);
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

                DeviceManager.Close();
                MemoryManager.Close();
                MassaManager.Close();

                DebugLog($"{nameof(DeviceManager)} is closed");
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
                PrintManager.Close();
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

        private void TaskRunDeviceManager()
        {
            _ = Task.Run(async () =>
            {
                // AsyncLock can be locked asynchronously
                using (await _mutexDeviceManager.LockAsync())
                {
                    // It's safe to await while the lock is held
                    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);

                    try
                    {
                        DeviceManager.Init(1_000, 5_000, 5_000);
                        DeviceManager.Open();
                    }
                    catch (Exception ex)
                    {
                        _exception.Catch(null, ref ex);
                    }
                }
            });
        }

        public void TaskRunMemoryManager()
        {
            _ = Task.Run(async () =>
            {
                // AsyncLock can be locked asynchronously
                using (await _mutexMemoryManager.LockAsync())
                {
                    // It's safe to await while the lock is held
                    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);

                    try
                    {
                        MemoryManager.Init(1_000, 5_000, 5_000);
                        MemoryManager.Open();
                    }
                    catch (Exception ex)
                    {
                        _exception.Catch(null, ref ex);
                    }
                }
            });
        }

        public void TaskRunPrintManager(TscPrintControlHelper.Callback callbackPrintManagerClose)
        {
            _ = Task.Run(async () =>
            {
                // AsyncLock can be locked asynchronously
                using (await _mutexPrintManager.LockAsync())
                {
                    // It's safe to await while the lock is held
                    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);

                    try
                    {
                        PrintManager.Open(IsTscPrinter, callbackPrintManagerClose);
                    }
                    catch (Exception ex)
                    {
                        _exception.Catch(null, ref ex);
                    }
                }
            });
        }

        public void TaskRunMassaManagerReOpen()
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

        private void DebugLog(string message, 
            [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            if (_debug.IsDebug)
                _log.Information(message, filePath, memberName, lineNumber);
        }

        #endregion
    }
}
