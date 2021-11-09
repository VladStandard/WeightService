// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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

        private MassaRequestHelper _massaRequest = MassaRequestHelper.Instance;
        private readonly ExceptionHelper _exception = ExceptionHelper.Instance;
        public decimal WeightNet { get; private set; }
        public decimal WeightGross { get; private set; }
        public byte IsStable { get; private set; }
        public int ScaleFactor { get; set; } = 1_000;
        public int CurrentError { get; private set; }
        public ResponseParseEntity ResponseParseScalePar { get; private set; } = null;
        public ResponseParseEntity ResponseParseGet { get; private set; } = null;
        public ResponseParseEntity ResponseParseSet { get; private set; } = null;
        public bool IsResponse {  get; private set; }
        private static readonly object Locker = new();
        public ConcurrentQueue<MassaExchangeEntity> RequestQueue { get; private set; } = new();
        private MassaDeviceEntity DeviceMassa { get; set; }

        #endregion

        #region Constructor and destructor

        public MassaManagerHelper()
        {
            
        }

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
                    DeviceMassa.Open();
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
                    //throw;
                }
                finally
                {
                    OpenJobResponse();
                    callback?.Invoke();
                    Thread.Sleep(TimeSpan.FromMilliseconds(WaitResponse));
                }
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
                    //throw;
                }
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
            if (RequestQueue.Count > 100)
            {
                // Clear queue.
                while (RequestQueue.Count > 0)
                {
                    RequestQueue.TryDequeue(out _);
                }
            }

            if (DeviceMassa.IsConnected)
            {
                if (RequestQueue.TryDequeue(out MassaExchangeEntity cmd))
                {
                    lock (Locker)
                    {
                        if (DeviceMassa == null || cmd == null) return;
                        Parse(cmd);
                    }
                }
            }
        }

        private void Parse(MassaExchangeEntity cmd)
        {
            switch (cmd.CmdType)
            {
                case MassaCmdType.UdpPoll:
                    cmd.Request = _massaRequest.CMD_UDP_POLL;
                    break;
                case MassaCmdType.GetInit2:
                    cmd.Request = _massaRequest.CMD_GET_INIT_2;
                    break;
                case MassaCmdType.GetInit3:
                    cmd.Request = _massaRequest.CMD_GET_INIT_3;
                    break;
                case MassaCmdType.GetEthernet:
                    cmd.Request = _massaRequest.CMD_GET_ETHERNET;
                    break;
                case MassaCmdType.GetWiFiIp:
                    cmd.Request = _massaRequest.CMD_GET_WIFI_IP;
                    break;
                case MassaCmdType.GetMassa:
                    cmd.Request = _massaRequest.CMD_GET_MASSA;
                    break;
                case MassaCmdType.GetName:
                    break;
                case MassaCmdType.GetScalePar:
                    cmd.Request = _massaRequest.CMD_GET_SCALE_PAR;
                    break;
                case MassaCmdType.GetScaleParAfter:
                    cmd.Request = _massaRequest.CMD_GET_SCALE_PAR_AFTER;
                    break;
                case MassaCmdType.SetTare:
                    cmd.Request = cmd.CmdSetTare();
                    break;
                case MassaCmdType.SetZero:
                    cmd.Request = _massaRequest.CMD_SET_ZERO;
                    break;
            }
            if (cmd.Request == null)
                return;

            byte[] response = DeviceMassa.WriteToPort(cmd);
            IsResponse = response != null;
            if (!IsResponse)
                return;

            cmd.ResponseParse = new ResponseParseEntity(cmd.CmdType, response);
            ParseSetResponse(cmd);
            ParseSetMassa(cmd);
        }

        private void ParseSetResponse(MassaExchangeEntity cmd)
        {
            switch (cmd.CmdType)
            {
                case MassaCmdType.GetMassa:
                    ResponseParseGet = cmd.ResponseParse;
                    break;
                case MassaCmdType.GetScalePar:
                    ResponseParseScalePar = cmd.ResponseParse;
                    break;
                case MassaCmdType.SetWiFiSsid:
                    ResponseParseSet = cmd.ResponseParse;
                    break;
                case MassaCmdType.SetDatetime:
                    ResponseParseSet = cmd.ResponseParse;
                    break;
                case MassaCmdType.SetName:
                    ResponseParseSet = cmd.ResponseParse;
                    break;
                case MassaCmdType.SetRegnum:
                    ResponseParseSet = cmd.ResponseParse;
                    break;
                case MassaCmdType.SetTare:
                    ResponseParseSet = cmd.ResponseParse;
                    break;
                case MassaCmdType.SetZero:
                    ResponseParseSet = cmd.ResponseParse;
                    break;
            }
        }

        private void ParseSetMassa(MassaExchangeEntity cmd)
        {
            switch (cmd.CmdType)
            {
                case MassaCmdType.GetMassa:
                    // 1 байт. Цена деления в значении массы нетто и массы тары:
                    // 0 – 100 мг, 1 – 1 г, 2 – 10 г, 3 – 100 г, 4 – 1 кг
                    ScaleFactor = cmd.ResponseParse.Massa.ScaleFactor;
                    // 4 байта. Текущая масса нетто со знаком
                    WeightNet = cmd.ResponseParse.Massa.Weight / (decimal)ScaleFactor;
                    // 4 байта. Текущая масса тары со знаком
                    decimal weightTare = cmd.ResponseParse.Massa.Tare / (decimal)ScaleFactor;
                    // 4 байта. Текущая масса тары со знаком
                    WeightGross = WeightNet + weightTare;
                    // 1 байт. Признак стабилизации массы: 0 – нестабильна, 1 – стабильна
                    IsStable = cmd.ResponseParse.Massa.Stable;
                    // 1 байт. Признак индикации<NET>: 0 – нет индикации, 1 – есть индикация. ... = x.Net;
                    //byte Zero. 1 байт. Признак индикации > 0 < : 0 – нет индикации, 1 – есть индикация. ... = x.Zero;
                    break;
            }
        }

        public void CloseJob()
        {
            DeviceMassa?.Dispose();
        }

        public void GetInit()
        {
            int timeOut = 500;

            GetInit1();
            Thread.Sleep(timeOut);
            GetInit2();
            Thread.Sleep(timeOut);
            GetInit3();
            Thread.Sleep(timeOut);

            GetScalePar();
            Thread.Sleep(timeOut);
            GetScaleParAfter();
            Thread.Sleep(timeOut);
            GetScalePar();
            Thread.Sleep(timeOut);
            GetMassa();
            Thread.Sleep(timeOut);
            
            SetZero();
            Thread.Sleep(timeOut);
            SetTareWeight(0);
            Thread.Sleep(timeOut);
            SetZero();
            Thread.Sleep(timeOut);
        }

        public void GetInit1() => RequestQueue.Enqueue(new MassaExchangeEntity(MassaCmdType.UdpPoll));
        public void GetInit2() => RequestQueue.Enqueue(new MassaExchangeEntity(MassaCmdType.GetInit2));
        public void GetInit3() => RequestQueue.Enqueue(new MassaExchangeEntity(MassaCmdType.GetInit3));
        public void GetMassa() => RequestQueue.Enqueue(new MassaExchangeEntity(MassaCmdType.GetMassa));
        public void GetScalePar() => RequestQueue.Enqueue(new MassaExchangeEntity(MassaCmdType.GetScalePar));
        public void GetScaleParAfter() => RequestQueue.Enqueue(new MassaExchangeEntity(MassaCmdType.GetScaleParAfter));
        public void SetTareWeight(int weightTare) => RequestQueue.Enqueue(new MassaExchangeEntity(MassaCmdType.SetTare, weightTare));
        public void SetZero() => RequestQueue.Enqueue(new MassaExchangeEntity(MassaCmdType.SetZero));

        #endregion
    }
}
