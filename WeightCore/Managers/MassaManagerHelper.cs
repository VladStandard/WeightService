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
        public bool IsRequestInit { get; set; } = true;

        #endregion

        #region Public fields and properties

        private readonly ExceptionHelper _exception = ExceptionHelper.Instance;
        public decimal WeightNet { get; private set; }
        public decimal WeightGross { get; private set; }
        public byte IsStable { get; private set; }
        public int ScaleFactor { get; set; } = 1000;
        public int CurrentError { get; private set; }
        public ResponseParseEntity ResponseParseGetScalePar { get; private set; } = null;
        public ResponseParseEntity ResponseParseGetMassa { get; private set; } = null;
        public ResponseParseEntity ResponseParseSetAll { get; private set; } = null;
        public bool IsResponse {  get; private set; }
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
            IsRequestInit = true;
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
                    if (IsRequestInit)
                    {
                        GetInit();
                        IsRequestInit = false;
                    }
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
                    Parse(cmd);
                }
            }
        }

        private void Parse(CmdEntity cmd)
        {
            switch (cmd.CmdType)
            {
                case CmdType.Init1:
                    cmd.Request = MassaUtils.Cmd.Get.CMD_INIT_1;
                    break;
                case CmdType.Init2:
                    cmd.Request = MassaUtils.Cmd.Get.CMD_INIT_2;
                    break;
                case CmdType.Init3:
                    cmd.Request = MassaUtils.Cmd.Get.CMD_INIT_3;
                    break;
                case CmdType.GetEthernet:
                    cmd.Request = MassaUtils.Cmd.Get.CMD_GET_ETHERNET;
                    break;
                case CmdType.GetWiFiIp:
                    cmd.Request = MassaUtils.Cmd.Get.CMD_GET_WIFI_IP;
                    break;
                case CmdType.GetMassa:
                    cmd.Request = MassaUtils.Cmd.Get.CMD_GET_MASSA;
                    break;
                case CmdType.GetName:
                    break;
                case CmdType.GetScalePar:
                    cmd.Request = MassaUtils.Cmd.Get.CMD_GET_SCALE_PAR;
                    break;
                case CmdType.SetTare:
                    cmd.Request = cmd.CmdSetTare();
                    break;
                case CmdType.SetZero:
                    cmd.Request = MassaUtils.Cmd.Set.CMD_SET_ZERO;
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

        private void ParseSetResponse(CmdEntity cmd)
        {
            switch (cmd.CmdType)
            {
                case CmdType.GetMassa:
                    ResponseParseGetMassa = cmd.ResponseParse;
                    break;
                case CmdType.GetScalePar:
                    ResponseParseGetScalePar = cmd.ResponseParse;
                    break;
                case CmdType.SetWiFiSsid:
                    ResponseParseSetAll = cmd.ResponseParse;
                    break;
                case CmdType.SetDatetime:
                    ResponseParseSetAll = cmd.ResponseParse;
                    break;
                case CmdType.SetName:
                    ResponseParseSetAll = cmd.ResponseParse;
                    break;
                case CmdType.SetRegnum:
                    ResponseParseSetAll = cmd.ResponseParse;
                    break;
                case CmdType.SetTare:
                    ResponseParseSetAll = cmd.ResponseParse;
                    break;
                case CmdType.SetZero:
                    ResponseParseSetAll = cmd.ResponseParse;
                    break;
            }
        }

        private void ParseSetMassa(CmdEntity cmd)
        {
            switch (cmd.CmdType)
            {
                //case CmdType.GetScalePar:
                //    cmd.ResponseParse.ScalePar = new ResponseScaleParEntity(cmd.ResponseParse.Response);
                //    break;
                case CmdType.GetMassa:
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
            RequestQueue.Enqueue(new CmdEntity(CmdType.Init1));
            Thread.Sleep(1_000);
            RequestQueue.Enqueue(new CmdEntity(CmdType.Init2));
            Thread.Sleep(1_000);
            RequestQueue.Enqueue(new CmdEntity(CmdType.Init3));
            Thread.Sleep(1_000);

            GetScalePar();
            Thread.Sleep(1_000);
            GetMassa();
            Thread.Sleep(1_000);
            SetZero();
            Thread.Sleep(1_000);
            SetTareWeight(0);
            Thread.Sleep(1_000);
            SetZero();
            Thread.Sleep(1_000);
        }

        public void GetMassa() => RequestQueue.Enqueue(new CmdEntity(CmdType.GetMassa));

        public void SetZero() => RequestQueue.Enqueue(new CmdEntity(CmdType.SetZero));

        public void SetTareWeight(int weightTare) => RequestQueue.Enqueue(new CmdEntity(CmdType.SetTare, weightTare));

        public void GetScalePar() => RequestQueue.Enqueue(new CmdEntity(CmdType.GetScalePar));

        #endregion
    }
}
