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
        private readonly AsyncLock _mutexMassaManagerResponse = new();
        private readonly AsyncLock _mutexMassaManagerRequest = new();

        public delegate void CallbackButtonSetZero(object sender, EventArgs e);

        #endregion

        #region Public and private methods

        public void Open(TscPrintControlHelper.Callback callbackPrintManagerClose, SqlViewModelEntity sqlViewModel, bool isTscPrinter, ScaleDirect currentScale)
        {
            try
            {
                IsTscPrinter = isTscPrinter;

                TaskRunDeviceManager(sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.DeviceManager));
                TaskRunMemoryManager(sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.MemoryManager));
                TaskRunMassaManagerResponse(sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.MassaManager), currentScale);
                TaskRunMassaManagerRequest(sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.MassaManager));
                TaskRunPrintManager(callbackPrintManagerClose, ref sqlViewModel, currentScale);
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

                TaskRunPrintManager(callbackPrintManagerClose, ref sqlViewModel, currentScale);
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

        private void TaskRunDeviceManager(bool taskEnabled)
        {
            _ = Task.Run(async () =>
            {
                // AsyncLock can be locked asynchronously
                //using (await _mutexDeviceManager.LockAsync(_ctsDeviceManager.Token))
                using (await _mutexDeviceManager.LockAsync())
                {
                    // It's safe to await while the lock is held
                    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);

                    try
                    {
                        if (taskEnabled)
                        {
                            DeviceManager.Init(1_000, 5_000, 5_000);
                            DeviceManager.Open();
                        }
                    }
                    catch (Exception ex)
                    {
                        _exception.Catch(null, ref ex);
                    }
                }
            });
        }

        public void TaskRunMemoryManager(bool taskEnabled)
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
                        if (taskEnabled)
                        {
                            MemoryManager.Init(1_000, 5_000, 5_000);
                            MemoryManager.Open();
                        }
                    }
                    catch (Exception ex)
                    {
                        _exception.Catch(null, ref ex);
                    }
                }
            });
        }

        public void TaskRunPrintManager(TscPrintControlHelper.Callback callbackPrintManagerClose,
            ref SqlViewModelEntity sqlViewModel, ScaleDirect currentScale)
        {
            bool taskEnabled = sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.PrintManager);
            _ = Task.Run(async () =>
            {
                // AsyncLock can be locked asynchronously
                //using (await _mutexPrintManager.LockAsync(_ctsPrintManager.Token))
                using (await _mutexPrintManager.LockAsync())
                {
                    // It's safe to await while the lock is held
                    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);

                    try
                    {
                        if (taskEnabled)
                        { 
                            PrintManager.Init(currentScale.ZebraPrinter.Name, currentScale.ZebraPrinter.Ip, currentScale.ZebraPrinter.Port, 1_000, 5_000, 5_000);
                            PrintManager.Open(IsTscPrinter, callbackPrintManagerClose);
                        }
                    }
                    catch (Exception ex)
                    {
                        _exception.Catch(null, ref ex);
                    }
                }
            });
        }

        public void TaskRunMassaManagerResponse(bool taskEnabled, ScaleDirect currentScale)
        {
            _ = Task.Run(async () =>
            {
                // AsyncLock can be locked asynchronously
                //using (await _mutexMassaManager.LockAsync(_ctsMassaManager.Token))
                using (await _mutexMassaManagerResponse.LockAsync())
                {
                    // It's safe to await while the lock is held
                    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);

                    try
                    {
                        if (taskEnabled)
                        {
                            MassaManager.Init(100, 250, 1_000, 5_000,
                                currentScale.DeviceComPort, true, currentScale.DeviceReceiveTimeout, currentScale.DeviceSendTimeout);
                            MassaManager.OpenResponse();
                        }
                    }
                    //catch (ConnectionException cex)
                    //{
                    //    Exception ex = new(cex.Message);
                    //    _exception.Catch(null, ref ex);
                    //}
                    catch (Exception ex)
                    {
                        _exception.Catch(null, ref ex);
                    }
                }
            });
        }

        public void TaskRunMassaManagerRequest(bool taskEnabled)
        {
            _ = Task.Run(async () =>
            {
                // AsyncLock can be locked asynchronously
                //using (await _mutexMassaManager.LockAsync(_ctsMassaManager.Token))
                using (await _mutexMassaManagerRequest.LockAsync())
                {
                    // It's safe to await while the lock is held
                    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);

                    try
                    {
                        if (taskEnabled)
                        {
                            MassaManager.OpenRequest();
                        }
                    }
                    catch (Exception ex)
                    {
                        _exception.Catch(null, ref ex);
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
