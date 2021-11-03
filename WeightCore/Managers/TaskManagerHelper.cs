// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL;
using DataProjectsCore.DAL.TableModels;
using DataProjectsCore.Helpers;
using Nito.AsyncEx;
using System;
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
        public bool DeviceManagerIsExit { get; set; }
        public char DeviceManagerProgressChar { get; set; }
        private readonly AsyncLock _mutexDeviceManager = new();
        private readonly CancellationTokenSource _ctsDeviceManager = new(TimeSpan.FromMilliseconds(1_000));

        public MemoryManagerHelper MemoryManager = MemoryManagerHelper.Instance;
        public bool MemoryManagerIsExit { get; set; }
        public char MemoryManagerProgressChar { get; set; }
        private readonly AsyncLock _mutexMemoryManager = new();
        private readonly CancellationTokenSource _ctsMemoryManager = new(TimeSpan.FromMilliseconds(1_000));

        public PrintManagerHelper PrintManager = PrintManagerHelper.Instance;
        public bool PrintManagerIsExit { get; set; }
        public char PrintManagerProgressChar { get; set; }
        private readonly AsyncLock _mutexPrintManager = new();
        private readonly CancellationTokenSource _ctsPrintManager = new(TimeSpan.FromMilliseconds(1_000));

        public MassaManagerHelper MassaManager = MassaManagerHelper.Instance;
        public bool MassaManagerIsExit { get; set; }
        public char MassaManagerProgressChar { get; set; }
        private readonly AsyncLock _mutexMassaManagerResponse = new();
        private readonly AsyncLock _mutexMassaManagerRequest = new();
        private readonly CancellationTokenSource _ctsMassaManager = new(TimeSpan.FromMilliseconds(1_000));

        public delegate void CallbackButtonSetZero(object sender, EventArgs e);

        #endregion

        #region Public and private methods - Http listener

        //private void StartHttpListener()
        //{
        //    _logUtils.Information("Запустить http-listener. начало.");
        //    _logUtils.Information("http://localhost:18086/status");
        //    try
        //    {
        //    //    CancellationTokenSource cancelTokenSource = new();
        //    //    _token = cancelTokenSource.Token;
        //    //    _threadChecker = new ThreadChecker(_token, 2_500);
        //    //    // Подписка на событие.
        //    //    //_threadChecker.EventReloadValues += EventHttpListenerReloadValues;
        //    //    _tokenHttpListener = cancelTokenSource.Token;
        //    //    HttpListener = new ZabbixHttpListener(_tokenHttpListener, 10);
        //    }
        //    catch (Exception ex)
        //    {
        //    //    _sessionState?.Log.SaveError(filePath, lineNumber, memberName, ex.Message);
        //    //    if (ex.InnerException != null)
        //    //        _sessionState?.Log.SaveError(filePath, lineNumber, memberName, ex.InnerException.Message);
        //    //    string msg = ex.Message;
        //    //    if (ex.InnerException != null)
        //    //        msg += Environment.NewLine + ex.InnerException.Message;
        //    //    CustomMessageBox.Show(this, StartHttpListener + Environment.NewLine + msg, Messages.Exception);
        //    }
        //    //catch (Exception ex)
        //    //{
        //    //    Log.SaveError(filePath, lineNumber, memberName, ex.Message);
        //    //    if (ex.InnerException != null)
        //    //        Log.SaveError(filePath, lineNumber, memberName, ex.InnerException.Message);
        //    //    _log.Error(ex.Message);
        //    //}
        //    //_logUtils.Information("Запистить http-listener. Финиш.");
        //}

        //private void StopHttpListener([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0,
        //    [CallerMemberName] string memberName = "")
        //{
        //    //try
        //    //{
        //    //    _log?.Info("Остановить http-listener. Начало.");
        //    //    HttpListener?.Stop();
        //    //    _token.ThrowIfCancellationRequested();
        //    //    _tokenHttpListener.ThrowIfCancellationRequested();
        //    //    _threadChecker?.Stop();
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Log.SaveError(filePath, lineNumber, memberName, ex.Message);
        //    //    if (ex.InnerException != null)
        //    //        Log.SaveError(filePath, lineNumber, memberName, ex.InnerException.Message);
        //    //    _log?.Error(ex.Message);
        //    //}
        //    //_log?.Info("Остановить http-listener. Финиш.");
        //}

        #endregion

        #region Public and private methods

        public void Open(DeviceManagerHelper.Callback callbackDeviceManager, MemoryManagerHelper.Callback callbackMemoryManager,
            MassaManagerHelper.Callback callbackMassaManager, 
            PrintManagerHelper.Callback callbackPrintManager, TscPrintControlHelper.Callback callbackPrintManagerClose,
            SqlViewModelEntity sqlViewModel, bool isTscPrinter, ScaleDirect currentScale)
        {
            try
            {
                DeviceManagerIsExit = false;
                MemoryManagerIsExit = false;
                MassaManagerIsExit = false;
                PrintManagerIsExit = false;
                IsTscPrinter = isTscPrinter;

                TaskRunDeviceManager(callbackDeviceManager, sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.DeviceManager));
                TaskRunMemoryManager(callbackMemoryManager, sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.MemoryManager));
                TaskRunMassaManagerResponse(callbackMassaManager, sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.MassaManager), currentScale);
                TaskRunMassaManagerRequest(sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.MassaManager));
                TaskRunPrintManager(callbackPrintManager, callbackPrintManagerClose, ref sqlViewModel, currentScale);
            }
            catch (Exception ex)
            {
                _exception.Catch(null, ref ex);
            }
        }

        public void OpenPrintManager(PrintManagerHelper.Callback callbackPrintManager, TscPrintControlHelper.Callback callbackPrintManagerClose,
            SqlViewModelEntity sqlViewModel, bool isTscPrinter, ScaleDirect currentScale)
        {
            try
            {
                PrintManagerIsExit = false;
                IsTscPrinter = isTscPrinter;

                TaskRunPrintManager(callbackPrintManager, callbackPrintManagerClose, ref sqlViewModel, currentScale);
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
                DeviceManagerIsExit = true;
                MemoryManagerIsExit = true;
                MassaManagerIsExit = true;

                _ctsDeviceManager.Cancel();
                _ctsMemoryManager.Cancel();
                _ctsPrintManager.Cancel();
                _ctsMassaManager.Cancel();

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
                PrintManagerIsExit = true;
                _ctsPrintManager.Cancel();
                PrintManager.Close();
                //if (IsTscPrinter && !PrintManager.PrintControl.IsStatusNormal)
                //    if (PrintManager.PrintControl != null && IsTscPrinter)
                //    {
                //        PrintManager.PrintControl.Close();
                //    }
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

        private void TaskRunDeviceManager(DeviceManagerHelper.Callback callbackDeviceManager, bool taskEnabled)
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
                            DeviceManager.Open(callbackDeviceManager);
                        }
                    }
                    catch (Exception ex)
                    {
                        _exception.Catch(null, ref ex);
                    }
                }
            });
        }

        public void TaskRunMemoryManager(MemoryManagerHelper.Callback callbackMemoryManager, bool taskEnabled)
        {
            _ = Task.Run(async () =>
            {
                // AsyncLock can be locked asynchronously
                //using (await _mutexMemoryManager.LockAsync(_ctsMemoryManager.Token))
                using (await _mutexMemoryManager.LockAsync())
                {
                    // It's safe to await while the lock is held
                    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);

                    try
                    {
                        if (taskEnabled)
                        {
                            MemoryManager.Init(1_000, 5_000, 5_000);
                            MemoryManager.Open(callbackMemoryManager);
                        }
                    }
                    catch (Exception ex)
                    {
                        _exception.Catch(null, ref ex);
                    }
                }
            });
        }

        public void TaskRunPrintManager(PrintManagerHelper.Callback callbackPrintManager, TscPrintControlHelper.Callback callbackPrintManagerClose,
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
                            PrintManager.Open(IsTscPrinter, callbackPrintManager, callbackPrintManagerClose);
                        }
                    }
                    catch (Exception ex)
                    {
                        _exception.Catch(null, ref ex);
                    }
                }
            });
        }

        public void TaskRunMassaManagerResponse(MassaManagerHelper.Callback callbackMassaManager, bool taskEnabled, ScaleDirect currentScale)
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
                            MassaManager.OpenResponse(callbackMassaManager);
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

        private void DebugLog(string message)
        {
            if (_debug.IsDebug)
                _log.Information(message);
        }

        #endregion
    }
}
