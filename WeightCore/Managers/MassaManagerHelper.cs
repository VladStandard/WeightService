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

        public int WaitResponse { get; private set; }
        public int WaitRequest { get; private set; }
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
        public ResponseParseErrorEntity ResponseGetError { get; private set; } = null;
        public ResponseParseErrorEntity ResponseSetError { get; private set; } = null;
        private static readonly object Locker = new();
        public ConcurrentQueue<CmdEntity> RequestQueue { get; private set; } = new();
        private DeviceMassaEntity DeviceMassa { get; set; }

        #endregion

        #region Constructor and destructor

        public void Init(int waitResponse, int waitRequest, int waitExceptionMiliSeconds, int waitCloseMiliSeconds,
            string portName, bool isEnableReconnect, int readTimeout, int writeTimeout)
        {
            WaitResponse = waitResponse == 0 ? 100 : waitResponse;
            WaitRequest = waitRequest == 0 ? 250 : waitRequest;
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
                try
                {
                    OpenJobResponse();
                    callback?.Invoke();
                    Thread.Sleep(TimeSpan.FromMilliseconds(WaitResponse));
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
                //System.Windows.Forms.Application.DoEvents();
            }
        }

        public void OpenRequest()
        {
            IsExecuteRequest = true;
            while (IsExecuteRequest)
            {
                try
                {
                    GetMassa();
                    Thread.Sleep(TimeSpan.FromMilliseconds(WaitRequest));
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
                //System.Windows.Forms.Application.DoEvents();
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
            if (RequestQueue.TryDequeue(out CmdEntity cmd))
            {
                lock (Locker)
                {
                    if (DeviceMassa == null || cmd == null) return;
                    //ResponseError = null;
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
            //byte[] response = DeviceMassa.GetResponse(cmd.CmdGetMassa());
            //ResponseParseEntity responseParse = MassaUtils.Parser.ParseResponse(response);
            //if (responseParse == null) return;
            cmd.Request = cmd.CmdGetMassa();
            cmd.Response = DeviceMassa.WriteToPort(cmd);
            cmd.ResponseParse = DeviceMassa.Parse(cmd);
            if (cmd.ResponseParse == null) return;

            switch (cmd.ResponseParse)
            {
                case ResponseParseGetMassaEntity reponseGetMassa:
                    ResponseGetError = null;
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
                    ResponseGetError = reponseError;
                    _log.Error(reponseError.GetMessage());
                    break;
            }
        }

        private void ResponseParseSetZero(CmdEntity cmd)
        {
            cmd.Request = cmd.CmdSetZero();
            cmd.Response = DeviceMassa.WriteToPort(cmd);
            cmd.ResponseParse = DeviceMassa.Parse(cmd);
            if (cmd.ResponseParse == null) return;

            switch (cmd.ResponseParse)
            {
                case ResponseParseSetZeroEntity reponseSetZero:
                    ResponseSetError = null;
                    if (_debug.IsDebug)
                        _log.Information(reponseSetZero.GetMessage());
                    break;
                case ResponseParseErrorEntity reponseError:
                    ResponseSetError = reponseError;
                    _log.Error(ResponseSetError.GetMessage());
                    break;
            }
        }

        private void ResponseParseSetTare(CmdEntity cmd)
        {
            //byte[] response = DeviceMassa.GetResponse(cmd.CmdSetTare());
            //ResponseParseEntity responseParse = MassaUtils.Parser.ParseResponse(response);
            //if (responseParse == null) return;
            cmd.Request = cmd.CmdSetTare();
            cmd.Response = DeviceMassa.WriteToPort(cmd);
            cmd.ResponseParse = DeviceMassa.Parse(cmd);
            if (cmd.ResponseParse == null) return;

            switch (cmd.ResponseParse)
            {
                case ResponseParseSetTareEntity reponseSetTare:
                    ResponseSetError = null;
                    //weightTare = ((CmdSetTare) request).WeightTare;
                    //scaleFactor = ((CmdSetTare) request).ScaleFactor;
                    if (_debug.IsDebug)
                        _log.Information(reponseSetTare.GetMessage());
                    break;
                case ResponseParseNackTareEntity reponseNackTare:
                    ResponseSetError = null;
                    if (_debug.IsDebug)
                        _log.Information(reponseNackTare.GetMessage());
                    break;
                case ResponseParseErrorEntity reponseError:
                    ResponseSetError = reponseError;
                    _log.Error(reponseError.GetMessage());
                    break;
            }
        }

        private void ResponseParseGetScalePar(CmdEntity cmd)
        {
            //byte[] response = DeviceMassa.GetResponse(cmd.CmdGetScalePar());
            //ResponseParseEntity responseParse = MassaUtils.Parser.ParseResponse(response);
            //if (responseParse == null) return;
            cmd.Request = cmd.CmdGetScalePar();
            cmd.Response = DeviceMassa.WriteToPort(cmd);
            cmd.ResponseParse = DeviceMassa.Parse(cmd);
            if (cmd.ResponseParse == null) return;

            switch (cmd.ResponseParse)
            {
                case ResponseParseScaleParEntity reponseScalePar:
                    ResponseGetError = null;
                    DeviceParameters = reponseScalePar;
                    break;
                case ResponseParseErrorEntity reponseError:
                    ResponseGetError = reponseError;
                    _log.Error(reponseError.GetMessage());
                    break;
            }
        }

        public void CloseJob()
        {
            DeviceMassa?.Dispose();
        }

        public void GetMassa() => RequestQueue.Enqueue(new CmdEntity(CmdType.GetMassa));

        public void SetZero() => RequestQueue.Enqueue(new CmdEntity(CmdType.SetZero));

        public void SetTareWeight(int weightTare) => RequestQueue.Enqueue(new CmdEntity(CmdType.SetTare, weightTare));

        public void GetScalePar() => RequestQueue.Enqueue(new CmdEntity(CmdType.GetScalePar));

        #endregion
    }
}
