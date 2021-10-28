// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.Helpers;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using WeightCore.Helpers;
using WeightCore.MassaK;

namespace WeightCore.Managers
{
    public class MassaManagerHelper
    {
        #region Design pattern "Lazy Singleton"

        private static MassaManagerHelper _instance;
        public static MassaManagerHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties - Manager

        public int WaitWhileMiliSeconds { get; private set; }
        public int WaitExceptionMiliSeconds { get; private set; }
        public int WaitCloseMiliSeconds { get; private set; }
        public string ExceptionMsg { get; private set; }
        public delegate void Callback();
        public bool IsExecuteResponse { get; set; }
        public bool IsExecuteRequest { get; set; }

        #endregion

        #region Public fields and properties

        private readonly ExceptionHelper _exception = ExceptionHelper.Instance;
        private readonly DebugHelper _debug = DebugHelper.Instance;
        private readonly LogHelper _log = LogHelper.Instance;
        public static readonly int CommandThreadTimeOut = 100;
        public decimal WeightNet { get; private set; }
        public decimal WeightGross { get; private set; }
        public byte IsStable { get; private set; }
        public int ScaleFactor { get; set; } = 1000;
        public int CurrentError { get; private set; }
        public ResponseParseScaleParEntity DeviceParameters { get; private set; }
        public ResponseParseErrorEntity ResponseError { get; private set; }
        private static readonly object Locker = new();
        private readonly ConcurrentQueue<CmdEntity> _requestQueue = new();
        private DeviceMassaEntity DeviceMassa { get; set; }

        #endregion

        #region Constructor and destructor

        public void Init(int waitWhileMiliSeconds, int waitExceptionMiliSeconds, int waitCloseMiliSeconds,
            string portName, bool isEnableReconnect, int readTimeout, int writeTimeout)
        {
            WaitWhileMiliSeconds = waitWhileMiliSeconds;
            WaitExceptionMiliSeconds = waitExceptionMiliSeconds;
            WaitCloseMiliSeconds = waitCloseMiliSeconds;
            IsExecuteResponse = false;
            IsExecuteRequest = false;

            DeviceMassa = new(portName, isEnableReconnect, readTimeout, writeTimeout);
        }

        #endregion

        #region Public and private methods - Manager

        public void OpenResponse(Callback callback)
        {
            IsExecuteResponse = true;
            while (IsExecuteResponse)
            {
                if (WaitWhileMiliSeconds == 0)
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(100));
                    continue;
                }
                try
                {
                    OpenJobResponse();
                    callback();
                    Thread.Sleep(TimeSpan.FromMilliseconds(WaitWhileMiliSeconds));
                }
                catch (TaskCanceledException)
                {
                    // Console.WriteLine(tcex.Message);
                    // Not the problem.
                }
                catch (Exception ex)
                {
                    _exception.Catch(null, ref ex);
                    Thread.Sleep(TimeSpan.FromMilliseconds(WaitExceptionMiliSeconds));
                    throw;
                }
                System.Windows.Forms.Application.DoEvents();
            }
        }

        public void OpenRequest()
        {
            IsExecuteRequest = true;
            while (IsExecuteRequest)
            {
                if (WaitWhileMiliSeconds == 0)
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(250));
                    continue;
                }
                try
                {
                    GetMassa();
                    Thread.Sleep(TimeSpan.FromMilliseconds(WaitWhileMiliSeconds));
                }
                catch (TaskCanceledException)
                {
                    // Console.WriteLine(tcex.Message);
                    // Not the problem.
                }
                catch (Exception ex)
                {
                    _exception.Catch(null, ref ex);
                    Thread.Sleep(TimeSpan.FromMilliseconds(WaitExceptionMiliSeconds));
                    throw;
                }
                System.Windows.Forms.Application.DoEvents();
            }
        }

        public void Close()
        {
            try
            {
                IsExecuteResponse = false;
                IsExecuteRequest = false;
                CloseJob();
            }
            catch (Exception ex)
            {
                _exception.Catch(null, ref ex);
            }
        }

        #endregion

        #region Public and private methods

        private void OpenJobResponse()
        {
            if (_requestQueue.TryDequeue(out CmdEntity cmd))
            {
                lock (Locker)
                {
                    if (DeviceMassa == null || cmd == null) return;
                    ResponseError = null;
                    switch (cmd.CmdType)
                    {
                        case CmdType.GetMassa:
                            ResponseParseGetMassa(cmd);
                            break;
                        case CmdType.SetZero:
                            ResponseParseSetZero(cmd);
                            break;
                        case CmdType.SetTare:
                            ResponseParseSetTare(cmd);
                            break;
                        case CmdType.GetScalePar:
                            ResponseParseGetScalePar(cmd);
                            break;
                    }
                }
            }
        }

        private void ResponseParseGetMassa(CmdEntity cmd)
        {
            byte[] response = DeviceMassa.GetResponse(cmd.CmdGetMassa());
            ResponseParseEntity responseParse = ResponseParseFactory.ParseResponse(response);
            if (responseParse == null) return;

            switch (responseParse)
            {
                case ResponseParseGetMassaEntity reponseGetMassa:
                    // 1 байт. Цена деления в значении массы нетто и массы тары:
                    // 0 – 100 мг, 1 – 1 г, 2 – 10 г, 3 – 100 г, 4 – 1 кг
                    ScaleFactor = reponseGetMassa.ScaleFactor;
                    // 4 байта. Текущая масса нетто со знаком
                    WeightNet = reponseGetMassa.Weight / (decimal)ScaleFactor;
                    // 4 байта. Текущая масса тары со знаком
                    decimal weightTare = reponseGetMassa.Tare / (decimal)ScaleFactor;
                    // 4 байта. Текущая масса тары со знаком
                    WeightGross = WeightNet + weightTare;
                    // 1 байт. Признак стабилизации массы: 0 – нестабильна, 1 – стабильна
                    IsStable = reponseGetMassa.Stable;
                    // 1 байт. Признак индикации<NET>: 0 – нет индикации, 1 – есть индикация. ... = x.Net;
                    //byte Zero. 1 байт. Признак индикации > 0 < : 0 – нет индикации, 1 – есть индикация. ... = x.Zero;
                    break;
                case ResponseParseErrorEntity reponseError:
                    ResponseError = reponseError;
                    _log.Error(reponseError.GetMessage());
                    break;
            }
        }

        private void ResponseParseSetZero(CmdEntity cmd)
        {
            byte[] response = DeviceMassa.GetResponse(cmd.CmdSetZero());
            ResponseParseEntity responseParse = ResponseParseFactory.ParseResponse(response);
            if (responseParse == null) return;

            switch (responseParse)
            {
                case ResponseParseSetZeroEntity reponseSetZero:
                    if (_debug.IsDebug)
                        _log.Information(reponseSetZero.GetMessage());
                    break;
                case ResponseParseErrorEntity reponseError:
                    ResponseError = reponseError;
                    break;
            }
        }

        private void ResponseParseSetTare(CmdEntity cmd)
        {
            byte[] response = DeviceMassa.GetResponse(cmd.CmdSetTare());
            ResponseParseEntity responseParse = ResponseParseFactory.ParseResponse(response);
            if (responseParse == null) return;

            switch (responseParse)
            {
                case ResponseParseSetTareEntity reponseSetTare:
                    //weightTare = ((CmdSetTare) request).WeightTare;
                    //scaleFactor = ((CmdSetTare) request).ScaleFactor;
                    if (_debug.IsDebug)
                        _log.Information(reponseSetTare.GetMessage());
                    break;
                case ResponseParseNackTareEntity reponseNackTare:
                    if (_debug.IsDebug)
                        _log.Information(reponseNackTare.GetMessage());
                    break;
                case ResponseParseErrorEntity reponseError:
                    ResponseError = reponseError;
                    if (_debug.IsDebug)
                        _log.Information(reponseError.GetMessage());
                    break;
            }
        }

        private void ResponseParseGetScalePar(CmdEntity cmd)
        {
            byte[] response = DeviceMassa.GetResponse(cmd.CmdGetScalePar());
            ResponseParseEntity responseParse = ResponseParseFactory.ParseResponse(response);
            if (responseParse == null) return;

            switch (responseParse)
            {
                case ResponseParseScaleParEntity reponseScalePar:
                    DeviceParameters = reponseScalePar;
                    break;
                case ResponseParseErrorEntity reponseError:
                    ResponseError = reponseError;
                    _log.Error(reponseError.GetMessage());
                    break;
            }
        }

        public void CloseJob()
        {
            DeviceMassa?.Dispose();
        }

        public void GetMassa() => _requestQueue.Enqueue(new CmdEntity(CmdType.GetMassa));

        public void SetZero() => _requestQueue.Enqueue(new CmdEntity(CmdType.SetZero));

        public void SetTareWeight(int weightTare) => _requestQueue.Enqueue(new CmdEntity(CmdType.SetTare, weightTare));

        public void GetScalePar() => _requestQueue.Enqueue(new CmdEntity(CmdType.GetScalePar));

        #endregion
    }
}
