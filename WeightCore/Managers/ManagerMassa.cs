// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Windows.Forms;
using WeightCore.Helpers;
using WeightCore.MassaK;
using static WeightCore.MassaK.MassaEnums;

namespace WeightCore.Managers
{
    public class ManagerMassa : ManagerBase
    {
        #region Public and private fields and properties

        private Label FieldMassaGet { get; set; }
        private Label FieldMassaGetCrc { get; set; }
        private Label FieldMassaScalePar { get; set; }
        private Label FieldMassaSet { get; set; }
        private Label FieldMassaSetCrc { get; set; }
        private Label FieldThreshold { get; set; }
        private Label FieldWeightNetto { get; set; }
        private Label FieldWeightTare { get; set; }
        private Label LabelWeightNetto { get; set; }
        private Label LabelWeightTare { get; set; }
        private MassaRequestHelper MassaRequest { get; set; } = MassaRequestHelper.Instance;
        private ProgressBar FieldMassaProgress { get; set; }
        private readonly object _locker = new();
        public BlockingCollection<MassaExchangeEntity> Requests { get; private set; } = new();
        private readonly int _stableMilliseconds = 1_000;
        public Stopwatch StopwatchStable { get; private set; } = new();
        private bool _isStable;
        public bool IsStable
        {
            get
            {
                if (!_isStable || StopwatchStable.Elapsed.TotalMilliseconds < _stableMilliseconds)
                    return false;
                return _isStable;
            }
            private set
            {
                if (_isStable != value)
                    StopwatchStable.Restart();
                _isStable = value;
            }
        }
        /// <summary>
        /// Вес брутто.
        /// </summary>
        public decimal WeightGross { get; private set; }
        /// <summary>
        /// Вес нетто.
        /// </summary>
        public decimal WeightNet { get; private set; }
        public int CurrentError { get; private set; }
        public int ScaleFactor { get; set; } = 1_000;
        public MassaDeviceModel MassaDevice { get; private set; }
        public ResponseParseEntity ResponseParseGet { get; private set; } = null;
        public ResponseParseEntity ResponseParseScalePar { get; private set; } = null;
        public ResponseParseEntity ResponseParseSet { get; private set; } = null;

        #endregion

        #region Constructor and destructor

        public ManagerMassa() : base()
        {
            Init(Close, ReleaseManaged, ReleaseUnmanaged);
        }

        #endregion

        #region Public and private methods

        public void Init(Label labelWeightNetto, Label fieldWeightNetto, Label labelWeightTare, Label fieldWeightTare,
            ProgressBar fieldMassaProgress, Label fieldThreshold,
            Label fieldMassaGet, Label fieldMassaGetCrc, Label fieldMassaSet, Label fieldMassaSetCrc, Label fieldMassaScalePar)
        {
            try
            {
                Init(ProjectsEnums.TaskType.MassaManager,
                    () =>
                    {
                        if (UserSessionHelper.Instance.Scale != null)
                        {
                            MassaDevice = new(UserSessionHelper.Instance.Scale.DeviceComPort,
                                UserSessionHelper.Instance.Scale.DeviceReceiveTimeout,
                                UserSessionHelper.Instance.Scale.DeviceSendTimeout, GetData);
                        }
                        LabelWeightNetto = labelWeightNetto;
                        FieldWeightNetto = fieldWeightNetto;
                        LabelWeightTare = labelWeightTare;
                        FieldWeightTare = fieldWeightTare;
                        FieldMassaProgress = fieldMassaProgress;
                        FieldThreshold = fieldThreshold;
                        FieldMassaGet = fieldMassaGet;
                        FieldMassaGetCrc = fieldMassaGetCrc;
                        FieldMassaSet = fieldMassaSet;
                        FieldMassaSetCrc = fieldMassaSetCrc;
                        FieldMassaScalePar = fieldMassaScalePar;

                        SetControlsTextDefault();
                    },
                    new(waitReopen: 5_000, waitRequest: 0_100, waitResponse: 0_100, waitClose: 1_000, waitException: 5_000));
            }
            catch (Exception ex)
            {
                Exception.Catch(null, ref ex, false);
            }
        }

        public new void Open()
        {
            try
            {
                Open(
                // Reopen.
                () =>
                {
                    if (UserSessionHelper.Instance.Plu?.IsCheckWeight == true)
                    {
                        MassaDevice?.Open();
                    }
                    
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldThreshold, 
                        $"{LocaleCore.Scales.FieldThresholds}: " +
                        (UserSessionHelper.Instance.Plu == null 
                        ? $"{LocaleCore.Scales.StateDisable}"
                        :
                        $"{LocaleCore.Scales.FieldThresholdLower}: {UserSessionHelper.Instance.Plu.LowerWeightThreshold:0.000} {LocaleCore.Scales.UnitKg} | " +
                        $"{LocaleCore.Scales.FieldThresholdNominal}: {UserSessionHelper.Instance.Plu.NominalWeight:0.000} {LocaleCore.Scales.UnitKg} | " +
                        $"{LocaleCore.Scales.FieldThresholdUpper}: {UserSessionHelper.Instance.Plu.UpperWeightThreshold:0.000} {LocaleCore.Scales.UnitKg}"));
                },
                // Request.
                () =>
                {
                    SetControlsVisible(true, true);
                    if (UserSessionHelper.Instance.Plu?.IsCheckWeight == true)
                    {
                        if (MassaDevice?.IsConnected == true)
                            GetMassa();
                        else
                            ClearRequestsByLimit(0);
                        RequestMassa();
                        RequestGetMassa();
                        RequestSetMassa();
                        SetControlsVisible(false, true);
                    }
                    else
                    {
                        SetControlsTextDefault();
                        SetControlsVisible(false, false);
                    }
                },
                // Response.
                () =>
                {
                    if (UserSessionHelper.Instance.Plu?.IsCheckWeight == true)
                    {
                        if (MassaDevice?.IsConnected == true)
                            Response();
                        else
                            ResetMassa();
                    }
                });
            }
            catch (Exception ex)
            {
                Exception.Catch(null, ref ex, false);
            }
        }

        private void SetControlsTextDefault()
        {
            MDSoft.WinFormsUtils.InvokeControl.SetText(LabelWeightNetto, LocaleCore.Scales.FieldWeightNetto);
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldWeightNetto, $"{0:0.000} {LocaleCore.Scales.UnitKg}");
            MDSoft.WinFormsUtils.InvokeControl.SetText(LabelWeightTare, LocaleCore.Scales.FieldWeightTare);
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldWeightTare, $"{0:0.000} {LocaleCore.Scales.UnitKg}");
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldThreshold, LocaleCore.Scales.FieldThresholds);
            
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGet, LocaleCore.Scales.ComPort);
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGetCrc, LocaleCore.Scales.Crc);
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaSet, LocaleCore.Scales.ScaleQueue);
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaSetCrc, LocaleCore.Scales.Crc);
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaScalePar, LocaleCore.Scales.RequestParameters);
        }

        private void SetControlsVisible(bool isTop, bool isVisible)
        {
            if (isTop)
            {
                if (LabelWeightNetto.Visible != isVisible)
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(LabelWeightNetto, isVisible);
                if (FieldWeightNetto.Visible != isVisible)
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldWeightNetto, isVisible);
                if (LabelWeightTare.Visible != isVisible)
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(LabelWeightTare, isVisible);
                if (FieldWeightTare.Visible != isVisible)
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldWeightTare, isVisible);
            }
            else {
                if (UserSessionHelper.Instance.Plu == null)
                {
                    if (FieldThreshold.Visible != isVisible)
                        MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldThreshold, isVisible);
                }
                else
                {
                    if (!FieldThreshold.Visible)
                        MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldThreshold, true);
                }

                if (FieldMassaGet.Visible != isVisible)
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaGet, isVisible);
                if (!FieldMassaSet.Visible)
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaSet, isVisible);
                if (!FieldMassaScalePar.Visible)
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaScalePar, isVisible);
                if (!FieldMassaGetCrc.Visible)
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaGetCrc, isVisible);
                if (!FieldMassaSetCrc.Visible)
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaSetCrc, isVisible);
                if (!FieldMassaProgress.Visible)
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaProgress, isVisible);
            }
        }

        private void RequestMassa()
        {
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldWeightTare,
                UserSessionHelper.Instance.Plu != null
                ? $"{UserSessionHelper.Instance.Plu.GoodsTareWeight:0.000} {LocaleCore.Scales.UnitKg}"
                : $"0,000 {LocaleCore.Scales.UnitKg}");

            decimal weight = UserSessionHelper.Instance.Plu == null ? 0 : WeightNet - UserSessionHelper.Instance.Plu.GoodsTareWeight;
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldWeightNetto, IsStable
                ? $"{weight:0.000} {LocaleCore.Scales.UnitKg}"
                : $"{LocaleCore.Scales.WeightingIsCalc}");
                  //$" ({(int)StopwatchStable.Elapsed.Add(TimeSpan.FromMilliseconds(-_stableMilliseconds)).TotalMilliseconds})");

            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaScalePar,
                $"{LocaleCore.Scales.WeightingProcess}: " +
                (UserSessionHelper.Instance.Plu == null ? $"{0:0.000} " : $"{WeightNet:0.000} ") +
                $"{LocaleCore.Scales.UnitKg} | {LocaleCore.Scales.RequestParameters}" +
                (ResponseParseScalePar == null ? string.Empty : $" | {ResponseParseScalePar.Message}"));
        }

        private void RequestSetMassa()
        {
            MDSoft.WinFormsUtils.InvokeProgressBar.SetValue(FieldMassaProgress, Requests != null ? Requests.Count : 0);
            if (UserSessionHelper.Instance.Plu?.IsCheckWeight == true)
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaSet,
                    $"{LocaleCore.Scales.ScaleQueue}: {Requests?.Count} | {LocaleCore.Scales.WeightingScaleCmd}: " +
                    (ResponseParseSet == null ? $"..." : ResponseParseSet.Message));
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaSetCrc,
                    $"{LocaleCore.Scales.Crc}: " + (ResponseParseSet == null
                    ? $"..." : (ResponseParseSet.IsValidAll ? $"{LocaleCore.Scales.StateCorrect}" : $"{LocaleCore.Scales.StateError}!"
                )));
            }
        }

        private void RequestGetMassa()
        {
            string massaDevice = MassaDevice != null
                ? MassaDevice.IsConnected
                    ? $"{LocaleCore.Scales.ComPort}: {LocaleCore.Scales.StateResponsed} | "
                    : $"{LocaleCore.Scales.ComPort}: {LocaleCore.Scales.StateNotResponsed} | "
                : $"{LocaleCore.Scales.ComPort}: {LocaleCore.Scales.StateDisable} | ";
            if (UserSessionHelper.Instance.Plu?.IsCheckWeight == true)
            {
                if (ResponseParseGet == null)
                {
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGet, massaDevice +
                        $"{LocaleCore.Scales.Message}: ...");
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGetCrc, $"{LocaleCore.Scales.Crc}: ...");
                }
                else
                {
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGet, massaDevice +
                        $"{LocaleCore.Scales.Message}: {ResponseParseGet.Message}");
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGetCrc, $"{LocaleCore.Scales.Crc}: " +
                        (ResponseParseGet.IsValidAll ? $"{LocaleCore.Scales.StateCorrect}" : $"{LocaleCore.Scales.StateError}!"));
                }
            }
        }

        public new void Close()
        {
            StopwatchStable.Stop();
            MassaDevice?.Close();
            while (Requests?.Count > 0)
            {
                Requests.Take();
            }

            base.Close();
        }

        public new void ReleaseManaged()
        {
            SetControlsVisible(true, false);
            SetControlsVisible(false, false);

            ResponseParseScalePar = null;
            ResponseParseGet = null;
            ResponseParseSet = null;

            Requests?.Dispose();
            Requests = null;
            MassaRequest = null;
            MassaDevice?.Dispose(true);
            MassaDevice = null;
            
            base.ReleaseManaged();
        }

        public new void ReleaseUnmanaged()
        {
            base.ReleaseUnmanaged();
        }

        #endregion

        #region Public and private methods - Control

        public void ClearRequestsByLimit(ushort limit)
        {
            if (Requests.Count > limit)
            {
                Requests = new BlockingCollection<MassaExchangeEntity>();
            }
        }

        public void Response()
        {
            ClearRequestsByLimit(100);

            foreach (MassaExchangeEntity massaExchange in Requests.GetConsumingEnumerable())
            {
                if (MassaDevice == null || massaExchange == null) return;
                SendData(massaExchange);
            }
            Requests = new BlockingCollection<MassaExchangeEntity>();
        }

        private void SendData(MassaExchangeEntity massaExchange)
        {
            switch (massaExchange.CmdType)
            {
                case MassaCmdType.UdpPoll:
                    massaExchange.Request = MassaRequest.CMD_UDP_POLL;
                    break;
                case MassaCmdType.GetInit2:
                    massaExchange.Request = MassaRequest.CMD_GET_INIT_2;
                    break;
                case MassaCmdType.GetInit3:
                    massaExchange.Request = MassaRequest.CMD_GET_INIT_3;
                    break;
                case MassaCmdType.GetEthernet:
                    massaExchange.Request = MassaRequest.CMD_GET_ETHERNET;
                    break;
                case MassaCmdType.GetWiFiIp:
                    massaExchange.Request = MassaRequest.CMD_GET_WIFI_IP;
                    break;
                case MassaCmdType.GetMassa:
                    massaExchange.Request = MassaRequest.CMD_GET_MASSA;
                    break;
                case MassaCmdType.GetName:
                    break;
                case MassaCmdType.GetScalePar:
                    massaExchange.Request = MassaRequest.CMD_GET_SCALE_PAR;
                    break;
                case MassaCmdType.GetScaleParAfter:
                    massaExchange.Request = MassaRequest.CMD_GET_SCALE_PAR_AFTER;
                    break;
                case MassaCmdType.SetTare:
                    massaExchange.Request = massaExchange.CmdSetTare();
                    break;
                case MassaCmdType.SetZero:
                    massaExchange.Request = MassaRequest.CMD_SET_ZERO;
                    break;
            }
            if (massaExchange.Request == null)
                return;

            MassaDevice?.SendData(massaExchange);
        }

        private void GetData(MassaExchangeEntity massaExchange, byte[] response)
        {
            lock (_locker)
            {
                IsResponse = response != null;
                if (!IsResponse)
                    return;

                if (massaExchange != null)
                {
                    massaExchange.ResponseParse = new ResponseParseEntity(massaExchange.CmdType, response);
                    ParseSetResponse(massaExchange);
                    ParseSetMassa(massaExchange);
                }
            }
        }

        private void ParseSetResponse(MassaExchangeEntity massaExchange)
        {
            switch (massaExchange.CmdType)
            {
                case MassaCmdType.GetMassa:
                    ResponseParseGet = massaExchange.ResponseParse;
                    break;
                case MassaCmdType.GetScalePar:
                    ResponseParseScalePar = massaExchange.ResponseParse;
                    break;
                case MassaCmdType.SetWiFiSsid:
                    ResponseParseSet = massaExchange.ResponseParse;
                    break;
                case MassaCmdType.SetDatetime:
                    ResponseParseSet = massaExchange.ResponseParse;
                    break;
                case MassaCmdType.SetName:
                    ResponseParseSet = massaExchange.ResponseParse;
                    break;
                case MassaCmdType.SetRegnum:
                    ResponseParseSet = massaExchange.ResponseParse;
                    break;
                case MassaCmdType.SetTare:
                    ResponseParseSet = massaExchange.ResponseParse;
                    break;
                case MassaCmdType.SetZero:
                    ResponseParseSet = massaExchange.ResponseParse;
                    break;
            }
        }

        private void ParseSetMassa(MassaExchangeEntity massaExchange)
        {
            if (massaExchange?.ResponseParse?.Massa != null)
            {
                switch (massaExchange.CmdType)
                {
                    case MassaCmdType.GetMassa:
                        // 1 байт. Цена деления в значении массы нетто и массы тары:
                        // 0 – 100 мг, 1 – 1 г, 2 – 10 г, 3 – 100 г, 4 – 1 кг
                        ScaleFactor = massaExchange.ResponseParse.Massa.ScaleFactor;
                        // 4 байта. Текущая масса нетто со знаком
                        WeightNet = massaExchange.ResponseParse.Massa.Weight / (decimal)ScaleFactor;
                        // 4 байта. Текущая масса тары со знаком
                        decimal weightTare = massaExchange.ResponseParse.Massa.Tare / (decimal)ScaleFactor;
                        // 4 байта. Текущая масса тары со знаком
                        WeightGross = WeightNet + weightTare;
                        // 1 байт. Признак стабилизации массы: 0 – нестабильна, 1 – стабильна
                        IsStable = massaExchange.ResponseParse.Massa.IsStable == 0x01;
                        // 1 байт. Признак индикации<NET>: 0 – нет индикации, 1 – есть индикация. ... = x.Net;
                        //byte Zero. 1 байт. Признак индикации > 0 < : 0 – нет индикации, 1 – есть индикация. ... = x.Zero;
                        break;
                }
            }
        }

        public void ResetMassa()
        {
            WeightGross = WeightNet = 0;
        }

        public void GetInit()
        {
            GetInit1();
            GetInit2();
            GetInit3();

            GetScalePar();
            GetScaleParAfter();
            GetScalePar();
            GetMassa();

            SetZero();
            SetTareWeight(0);
            SetZero();
        }

        public void GetInit1() => Requests.Add(new MassaExchangeEntity(MassaCmdType.UdpPoll));
        public void GetInit2() => Requests.Add(new MassaExchangeEntity(MassaCmdType.GetInit2));
        public void GetInit3() => Requests.Add(new MassaExchangeEntity(MassaCmdType.GetInit3));
        public void GetMassa() => Requests.Add(new MassaExchangeEntity(MassaCmdType.GetMassa));
        public void GetScalePar() => Requests.Add(new MassaExchangeEntity(MassaCmdType.GetScalePar));
        public void GetScaleParAfter() => Requests.Add(new MassaExchangeEntity(MassaCmdType.GetScaleParAfter));
        public void SetTareWeight(int weightTare) => Requests.Add(new MassaExchangeEntity(MassaCmdType.SetTare, weightTare));
        public void SetZero() => Requests.Add(new MassaExchangeEntity(MassaCmdType.SetZero));

        #endregion
    }
}
