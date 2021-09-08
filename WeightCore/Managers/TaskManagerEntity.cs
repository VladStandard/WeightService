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
            // Запустить http-прослушиватель.
            if (SqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.ZabbixManager))
            {
                StartHttpListener();
            }
        }

        ~TaskManagerEntity()
        {
            DeviceManager?.Close();
            MemoryManager?.Close();
            PrintManager?.Close();
            MassaManager?.Close();
            StopHttpListener();
        }

        #endregion

        #region Public and private fields and properties

        //public Task TaskManager { get; set; }
        public CancellationTokenSource Cts { get; set; }
        public SqlViewModelEntity SqlViewModel { get; set; } = SqlViewModelEntity.Instance;
        private LogUtils _logUtils = LogUtils.Instance;

        public DeviceManagerEntity DeviceManager { get; set; }
        public bool DeviceManagerIsExit { get; set; }
        public char DeviceManagerProgressChar { get; set; }

        public MemoryManagerEntity MemoryManager { get; set; }
        public bool MemoryManagerIsExit { get; set; }
        public char MemoryManagerProgressChar { get; set; }

        public PrintManagerEntity PrintManager { get; set; }
        public bool PrintManagerIsExit { get; set; }
        public char PrintManagerProgressChar { get; set; }

        public MassaManagerEntity MassaManager { get; set; }
        public bool MassaManagerIsExit { get; set; }
        public char MassaManagerProgressChar { get; set; }

        private readonly AsyncLock _mutexMemoryManager = new AsyncLock();
        private readonly AsyncLock _mutexPrintManager = new AsyncLock();
        private readonly AsyncLock _mutexDeviceManager = new AsyncLock();
        private readonly AsyncLock _mutexMassaManager = new AsyncLock();

        #endregion

        #region Public and private methods - Http listener

        private void StartHttpListener([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            //_log.Info("Запустить http-listener. начало.");
            //_log.Info("http://localhost:18086/status");
            //try
            //{
            //    CancellationTokenSource cancelTokenSource = new();
            //    _token = cancelTokenSource.Token;
            //    _threadChecker = new ThreadChecker(_token, 2_500);
            //    // Подписка на событие.
            //    //_threadChecker.EventReloadValues += EventHttpListenerReloadValues;
            //    _tokenHttpListener = cancelTokenSource.Token;
            //    HttpListener = new ZabbixHttpListener(_tokenHttpListener, 10);
            //}
            //catch (Exception ex)
            //{
            //    _ws?.Log.SaveError(filePath, lineNumber, memberName, ex.Message);
            //    if (ex.InnerException != null)
            //        _ws?.Log.SaveError(filePath, lineNumber, memberName, ex.InnerException.Message);
            //    string msg = ex.Message;
            //    if (ex.InnerException != null)
            //        msg += Environment.NewLine + ex.InnerException.Message;
            //    CustomMessageBox.Show(this, StartHttpListener + Environment.NewLine + msg, Messages.Exception);
            //}

            //catch (Exception ex)
            //{
            //    Log.SaveError(filePath, lineNumber, memberName, ex.Message);
            //    if (ex.InnerException != null)
            //        Log.SaveError(filePath, lineNumber, memberName, ex.InnerException.Message);
            //    _log.Error(ex.Message);
            //}
            //_log.Info("Запистить http-listener. Финиш.");
        }

        private void StopHttpListener([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "")
        {
            //try
            //{
            //    _log?.Info("Остановить http-listener. Начало.");
            //    HttpListener?.Stop();
            //    _token.ThrowIfCancellationRequested();
            //    _tokenHttpListener.ThrowIfCancellationRequested();
            //    _threadChecker?.Stop();
            //}
            //catch (Exception ex)
            //{
            //    Log.SaveError(filePath, lineNumber, memberName, ex.Message);
            //    if (ex.InnerException != null)
            //        Log.SaveError(filePath, lineNumber, memberName, ex.InnerException.Message);
            //    _log?.Error(ex.Message);
            //}
            //_log?.Info("Остановить http-listener. Финиш.");
        }

        #endregion

        #region Public and private methods

        public delegate void CallbackButtonSetZero(
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "");

        [Obsolete(@"Deprecated method")]
        public async Task TaskManagerAsync(
            MemoryManagerEntity.CallbackAsync callbackMemoryManager,
            PrintManagerEntity.CallbackAsync callbackPrintManager,
            DeviceManagerEntity.CallbackAsync callbackDeviceManager,
            MassaManagerEntity.CallbackAsync callbackMassaManager,
            CallbackButtonSetZero callbackButtonSetZero,
            SqlViewModelEntity sqlViewModel, bool isTscPrinter, ScaleDirect currentScale,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            // MemoryManager.
            if (sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.MemoryManager))
            {
                Task taskMemory = new(() =>
                {
                    try
                    {
                        //while (!_ws.MemoryManagerIsExit)
                        {
                            if (MemoryManager == null)
                            {
                                MemoryManager = new MemoryManagerEntity(1_000, 5_000, 5_000);
                            }
                            MemoryManager.Open(callbackMemoryManager);
                        }
                        _logUtils.Information("MemoryManager is runned", filePath, memberName, lineNumber);
                    }
                    catch (Exception ex)
                    {
                        _logUtils.Error(ex.Message, filePath, memberName, lineNumber);
                        if (ex.InnerException != null)
                            _logUtils.Error(ex.InnerException.Message, filePath, memberName, lineNumber);
                    }
                });
                taskMemory.Start();
            }

            // PrintManager.
            if (sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.PrintManager))
            {
                Task taskPrint = new(() =>
                {
                    try
                    {
                        //while (!_ws.PrintManagerIsExit)
                        {
                            if (PrintManager == null)
                            {
                                PrintManager = new PrintManagerEntity(currentScale.ZebraPrinter.Ip, currentScale.ZebraPrinter.Port, 1_000, 5_000, 5_000);
                            }
                            PrintManager.Open(isTscPrinter, callbackPrintManager);
                            // STOP
                            //if (_ws.PrintManager.PrintControl != null && isTscPrinter && !_ws.PrintManager.PrintControl.IsStatusNormal)
                            //{
                            //    _ws.PrintManager.PrintControl.Close();
                            //    _ws.PrintManager.PrintControl = null;
                            //}
                        }
                        _logUtils.Information("PrintManager is runned", filePath, memberName, lineNumber);
                    }
                    catch (Exception ex)
                    {
                        _logUtils.Error(ex.Message, filePath, memberName, lineNumber);
                        if (ex.InnerException != null)
                            _logUtils.Error(ex.InnerException.Message, filePath, memberName, lineNumber);
                    }
                });
                taskPrint.Start();
            }

            // DeviceManager.
            if (sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.DeviceManager))
            {
                Task taskDevice = new(() =>
                {
                    try
                    {
                        //while (!_ws.DeviceManagerIsExit)
                        {
                            if (DeviceManager == null)
                            {
                                DeviceManager = new DeviceManagerEntity(1_000, 5_000, 5_000);
                            }
                            DeviceManager.Open(callbackDeviceManager);
                        }
                        _logUtils.Information("DeviceManager is runned", filePath, memberName, lineNumber);
                    }
                    catch (Exception ex)
                    {
                        _logUtils.Error(ex.Message, filePath, memberName, lineNumber);
                        if (ex.InnerException != null)
                            _logUtils.Error(ex.InnerException.Message, filePath, memberName, lineNumber);
                    }
                });
                taskDevice.Start();
            }

            // MassaManager.
            if (sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.MassaManager))
            {
                Task taskMassa = new(() =>
                {
                    try
                    {
                        //while (!_ws.MassaManagerIsExit)
                        {
                            if (MassaManager == null)
                            {
                                DeviceSocketRs232 deviceSocketRs232 = new(currentScale.DeviceComPort);
                                MassaManager = new MassaManagerEntity(deviceSocketRs232, 1_000, 5_000, 5_000);
                                callbackButtonSetZero();
                            }
                            MassaManager.Open(callbackMassaManager);
                        }
                        _logUtils.Information("MassaManager is runned", filePath, memberName, lineNumber);
                    }
                    catch (Exception ex)
                    {
                        _logUtils.Error(ex.Message, filePath, memberName, lineNumber);
                        if (ex.InnerException != null)
                            _logUtils.Error(ex.InnerException.Message, filePath, memberName, lineNumber);
                    }
                });
                taskMassa.Start();
            }
        }

        public async Task TaskRunMemoryManagerAsync(MemoryManagerEntity.CallbackAsync callbackMemoryManager, SqlViewModelEntity sqlViewModel, 
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            // AsyncLock can be locked asynchronously
            using (await _mutexMemoryManager.LockAsync())
            {
                // It's safe to await while the lock is held
                await Task.Delay(TimeSpan.FromSeconds(1));
            
                // MemoryManager.
                if (sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.MemoryManager))
                {
                    try
                    {
                        if (MemoryManager == null)
                        {
                            MemoryManager = new MemoryManagerEntity(1_000, 5_000, 5_000);
                        }
                        MemoryManager.Open(callbackMemoryManager);
                        _logUtils.Information("MemoryManager is runned", filePath, memberName, lineNumber);
                    }
                    catch (Exception ex)
                    {
                        _logUtils.Error(ex.Message, filePath, memberName, lineNumber);
                        if (ex.InnerException != null)
                            _logUtils.Error(ex.InnerException.Message, filePath, memberName, lineNumber);
                    }
                }
            }
        }

        public async Task TaskRunPrintManagerAsync(
            PrintManagerEntity.CallbackAsync callbackPrintManager,
            SqlViewModelEntity sqlViewModel, bool isTscPrinter, ScaleDirect currentScale,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            // AsyncLock can be locked asynchronously
            using (await _mutexPrintManager.LockAsync())
            {
                // It's safe to await while the lock is held
                await Task.Delay(TimeSpan.FromSeconds(1));

                // PrintManager.
                if (sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.PrintManager))
                {
                    try
                    {
                        if (PrintManager == null)
                        {
                            PrintManager = new PrintManagerEntity(currentScale.ZebraPrinter.Ip, currentScale.ZebraPrinter.Port, 1_000, 5_000, 5_000);
                        }
                        PrintManager.Open(isTscPrinter, callbackPrintManager);
                        // STOP
                        //if (_ws.PrintManager.PrintControl != null && isTscPrinter && !_ws.PrintManager.PrintControl.IsStatusNormal)
                        //{
                        //    _ws.PrintManager.PrintControl.Close();
                        //    _ws.PrintManager.PrintControl = null;
                        //}
                        _logUtils.Information("PrintManager is runned", filePath, memberName, lineNumber);
                    }
                    catch (Exception ex)
                    {
                        _logUtils.Error(ex.Message, filePath, memberName, lineNumber);
                        if (ex.InnerException != null)
                            _logUtils.Error(ex.InnerException.Message, filePath, memberName, lineNumber);
                    }
                }
            }
        }

        public async Task TaskRunDeviceManagerAsync(
            DeviceManagerEntity.CallbackAsync callbackDeviceManager,
            SqlViewModelEntity sqlViewModel, 
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            // AsyncLock can be locked asynchronously
            using (await _mutexDeviceManager.LockAsync())
            {
                // It's safe to await while the lock is held
                await Task.Delay(TimeSpan.FromSeconds(1));

                // DeviceManager.
                if (sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.DeviceManager))
                {
                    try
                    {
                        if (DeviceManager == null)
                        {
                            DeviceManager = new DeviceManagerEntity(1_000, 5_000, 5_000);
                        }
                        DeviceManager.Open(callbackDeviceManager);
                        _logUtils.Information("DeviceManager is runned", filePath, memberName, lineNumber);
                    }
                    catch (Exception ex)
                    {
                        _logUtils.Error(ex.Message, filePath, memberName, lineNumber);
                        if (ex.InnerException != null)
                            _logUtils.Error(ex.InnerException.Message, filePath, memberName, lineNumber);
                    }
                }
            }
        }

        public async Task TaskRunMassaManagerAsync(
            MassaManagerEntity.CallbackAsync callbackMassaManager,
            CallbackButtonSetZero callbackButtonSetZero,
            SqlViewModelEntity sqlViewModel, bool isTscPrinter, ScaleDirect currentScale,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            // AsyncLock can be locked asynchronously
            using (await _mutexMassaManager.LockAsync())
            {
                // It's safe to await while the lock is held
                await Task.Delay(TimeSpan.FromSeconds(1));

                // MassaManager.
                if (sqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.MassaManager))
                {
                    try
                    {
                        if (MassaManager == null)
                        {
                            DeviceSocketRs232 deviceSocketRs232 = new(currentScale.DeviceComPort);
                            MassaManager = new MassaManagerEntity(deviceSocketRs232, 1_000, 5_000, 5_000);
                            callbackButtonSetZero();
                        }
                        MassaManager.Open(callbackMassaManager);
                        _logUtils.Information("MassaManager is runned", filePath, memberName, lineNumber);
                    }
                    catch (Exception ex)
                    {
                        _logUtils.Error(ex.Message, filePath, memberName, lineNumber);
                        if (ex.InnerException != null)
                            _logUtils.Error(ex.InnerException.Message, filePath, memberName, lineNumber);
                    }
                }
            }
        }

        #endregion
    }
}
