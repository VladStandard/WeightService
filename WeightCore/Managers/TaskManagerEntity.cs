// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL;
using DataProjectsCore.DAL.TableModels;
using DataProjectsCore.Utils;
using Nito.AsyncEx;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using WeightCore.Helpers;
using WeightCore.Print;

namespace WeightCore.Managers
{
    public class TaskManagerEntity
    {
        #region Design pattern "Lazy Singleton"

        private static TaskManagerEntity _instance;
        public static TaskManagerEntity Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Constructor and destructor

        public TaskManagerEntity()
        {
            //
        }

        ~TaskManagerEntity()
        {
            Close();
        }

        #endregion

        #region Public and private fields and properties

        private readonly ExceptionHelper _exception = ExceptionHelper.Instance;
        public SqlViewModelEntity SqlViewModel { get; set; } = SqlViewModelEntity.Instance;
        private readonly LogUtils _logUtils = LogUtils.Instance;
        private bool IsTscPrinter { get; set; }

        public DeviceManagerEntity DeviceManager { get; set; }
        public bool DeviceManagerIsExit { get; set; }
        public char DeviceManagerProgressChar { get; set; }
        private readonly AsyncLock _mutexDeviceManager = new();
        private readonly CancellationTokenSource _ctsDeviceManager = new(TimeSpan.FromMilliseconds(1_000));

        public MemoryManagerEntity MemoryManager { get; set; }
        public bool MemoryManagerIsExit { get; set; }
        public char MemoryManagerProgressChar { get; set; }
        private readonly AsyncLock _mutexMemoryManager = new();
        private readonly CancellationTokenSource _ctsMemoryManager = new(TimeSpan.FromMilliseconds(1_000));

        public PrintManagerEntity PrintManager { get; set; }
        public bool PrintManagerIsExit { get; set; }
        public char PrintManagerProgressChar { get; set; }
        private readonly AsyncLock _mutexPrintManager = new();
        private readonly CancellationTokenSource _ctsPrintManager = new(TimeSpan.FromMilliseconds(1_000));

        public MassaManagerEntity MassaManager { get; set; }
        public bool MassaManagerIsExit { get; set; }
        public char MassaManagerProgressChar { get; set; }
        private readonly AsyncLock _mutexMassaManager = new();
        private readonly CancellationTokenSource _ctsMassaManager = new(TimeSpan.FromMilliseconds(1_000));

        public delegate void CallbackButtonSetZero(object sender, EventArgs e);

        #endregion

        #region Public and private methods - Http listener

        //private void StartHttpListener([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
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
        //    //    _ws?.Log.SaveError(filePath, lineNumber, memberName, ex.Message);
        //    //    if (ex.InnerException != null)
        //    //        _ws?.Log.SaveError(filePath, lineNumber, memberName, ex.InnerException.Message);
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

        public void Open(DeviceManagerEntity.Callback callbackDeviceManager, MemoryManagerEntity.Callback callbackMemoryManager, 
            PrintManagerEntity.Callback callbackPrintManager, MassaManagerEntity.Callback callbackMassaManager,
            CallbackButtonSetZero callbackButtonSetZero, SqlViewModelEntity sqlViewModel, bool isTscPrinter, ScaleDirect currentScale)
        {
            try
            {
                DeviceManagerIsExit = false;
                MemoryManagerIsExit = false;
                PrintManagerIsExit = false;
                MassaManagerIsExit = false;
                IsTscPrinter = isTscPrinter;

                TaskRunDeviceManager(callbackDeviceManager, sqlViewModel);
                TaskRunMemoryManager(callbackMemoryManager, sqlViewModel);
                TaskRunPrintManager(callbackPrintManager, sqlViewModel, currentScale);
                TaskRunMassaManager(callbackMassaManager, callbackButtonSetZero, sqlViewModel, currentScale);
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
                PrintManagerIsExit = true;
                MassaManagerIsExit = true;

                _ctsDeviceManager.Cancel();
                _ctsMemoryManager.Cancel();
                _ctsPrintManager.Cancel();
                _ctsMassaManager.Cancel();

                DeviceManager?.Close();
                MemoryManager?.Close();
                PrintManager?.Close();
                MassaManager?.Close();

                _logUtils.Information($"{nameof(DeviceManager)} is closed");
                _logUtils.Information($"{nameof(MemoryManager)} is closed");
                _logUtils.Information($"{nameof(PrintManager)} is closed");
                _logUtils.Information($"{nameof(MassaManager)} is closed");

                if (PrintManager?.PrintControl != null && IsTscPrinter && !PrintManager.PrintControl.IsStatusNormal)
                {
                    PrintManager.PrintControl.Close();
                }
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

        private void TaskRunDeviceManager(DeviceManagerEntity.Callback callbackDeviceManager, SqlViewModelEntity sqlViewModel)
        {
            _ = Task.Run(async () =>
            {
                // AsyncLock can be locked asynchronously
                //using (await _mutexDeviceManager.LockAsync(_ctsDeviceManager.Token))
                using (await _mutexDeviceManager.LockAsync())
                {
                    // It's safe to await while the lock is held
                    await Task.Delay(TimeSpan.FromSeconds(1));

                    try
                    {
                        // DeviceManager.
                        if (sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.DeviceManager))
                        {
                            if (DeviceManager == null)
                            {
                                DeviceManager = new DeviceManagerEntity(1_000, 5_000, 5_000);
                            }
                            _logUtils.Information($"{nameof(DeviceManager)} is runned");
                            DeviceManager.Open(callbackDeviceManager);
                            _logUtils.Information($"{nameof(DeviceManager)} is finished");
                        }
                    }
                    catch (Exception ex)
                    {
                        _exception.Catch(null, ref ex);
                    }
                }
            });
        }

        public void TaskRunMemoryManager(MemoryManagerEntity.Callback callbackMemoryManager, SqlViewModelEntity sqlViewModel)
        {
            _ = Task.Run(async () =>
            {
                // AsyncLock can be locked asynchronously
                //using (await _mutexMemoryManager.LockAsync(_ctsMemoryManager.Token))
                using (await _mutexMemoryManager.LockAsync())
                {
                    // It's safe to await while the lock is held
                    await Task.Delay(TimeSpan.FromSeconds(1));

                    try
                    {
                        // MemoryManager.
                        if (sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.MemoryManager))
                        {
                            if (MemoryManager == null)
                            {
                                MemoryManager = new MemoryManagerEntity(1_000, 5_000, 5_000);
                            }
                            _logUtils.Information($"{nameof(MemoryManager)} is runned");
                            MemoryManager.Open(callbackMemoryManager);
                            _logUtils.Information($"{nameof(MemoryManager)} is finished");
                        }
                    }
                    catch (Exception ex)
                    {
                        _exception.Catch(null, ref ex);
                    }
                }
            });
        }

        public void TaskRunPrintManager(PrintManagerEntity.Callback callbackPrintManager,
            SqlViewModelEntity sqlViewModel, ScaleDirect currentScale)
        {
            _ = Task.Run(async () =>
            {
                // AsyncLock can be locked asynchronously
                //using (await _mutexPrintManager.LockAsync(_ctsPrintManager.Token))
                using (await _mutexPrintManager.LockAsync())
                {
                    // It's safe to await while the lock is held
                    await Task.Delay(TimeSpan.FromSeconds(1));

                    try
                    {
                        // PrintManager.
                        if (sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.PrintManager))
                        {
                            if (PrintManager == null)
                            {
                                PrintManager = new PrintManagerEntity(currentScale.ZebraPrinter.Ip, currentScale.ZebraPrinter.Port, 1_000, 5_000, 5_000);
                            }
                            _logUtils.Information($"{nameof(PrintManager)} is runned");
                            PrintManager.Open(IsTscPrinter, callbackPrintManager);
                            _logUtils.Information($"{nameof(PrintManager)} is finished");
                        }
                    }
                    catch (Exception ex)
                    {
                        _exception.Catch(null, ref ex);
                    }
                }
            });
        }

        public void TaskRunMassaManager(MassaManagerEntity.Callback callbackMassaManager,
            CallbackButtonSetZero callbackButtonSetZero, SqlViewModelEntity sqlViewModel, ScaleDirect currentScale,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            _ = Task.Run(async () =>
            {
                // AsyncLock can be locked asynchronously
                //using (await _mutexMassaManager.LockAsync(_ctsMassaManager.Token))
                using (await _mutexMassaManager.LockAsync())
                {
                    // It's safe to await while the lock is held
                    await Task.Delay(TimeSpan.FromSeconds(1));

                    try
                    {
                        // MassaManager.
                        if (sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.MassaManager))
                        {
                            if (MassaManager == null)
                            {
                                DeviceSocketRs232 deviceSocketRs232 = new(currentScale.DeviceComPort);
                                MassaManager = new MassaManagerEntity(deviceSocketRs232, 1_000, 5_000, 5_000);
                                callbackButtonSetZero(null, null);
                            }
                            _logUtils.Information($"{nameof(MassaManager)} is runned");
                            MassaManager.Open(callbackMassaManager);
                            _logUtils.Information($"{nameof(MassaManager)} is finished");
                        }
                    }
                    catch (Exception ex)
                    {
                        _exception.Catch(null, ref ex);
                    }
                }
            });
        }

        #endregion
    }
}
