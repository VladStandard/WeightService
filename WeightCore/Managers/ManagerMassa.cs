// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using System;
using System.Collections.Concurrent;
using System.Windows.Forms;
using WeightCore.Helpers;
using WeightCore.MassaK;
using static WeightCore.MassaK.MassaEnums;
using LocalizationCore = DataCore.Localizations.LocaleCore;

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
        private ProgressBar FieldMassaQueriesProgress { get; set; }
        private readonly object _locker = new();
        public BlockingCollection<MassaExchangeEntity> Requests { get; private set; } = new();
        public byte IsStable { get; private set; }
        public decimal WeightGross { get; private set; }
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
            ProgressBar fieldMassaQueriesProgress, Label fieldThreshold,
            Label fieldMassaGet, Label fieldMassaGetCrc, Label fieldMassaSet, Label fieldMassaSetCrc, Label fieldMassaScalePar)
        {
            Init(ProjectsEnums.TaskType.MassaManager,
            () =>
            {
                if (SessionStateHelper.Instance.CurrentScale != null)
                {
                    MassaDevice = new(SessionStateHelper.Instance.CurrentScale.DeviceComPort,
                        SessionStateHelper.Instance.CurrentScale.DeviceReceiveTimeout,
                        SessionStateHelper.Instance.CurrentScale.DeviceSendTimeout, GetData);
                }
                LabelWeightNetto = labelWeightNetto;
                FieldWeightNetto = fieldWeightNetto;
                LabelWeightTare = labelWeightTare;
                FieldWeightTare = fieldWeightTare;
                FieldMassaQueriesProgress = fieldMassaQueriesProgress;
                FieldThreshold = fieldThreshold;
                FieldMassaGet = fieldMassaGet;
                FieldMassaGetCrc = fieldMassaGetCrc;
                FieldMassaSet = fieldMassaSet;
                FieldMassaSetCrc = fieldMassaSetCrc;
                FieldMassaScalePar = fieldMassaScalePar;

                MDSoft.WinFormsUtils.InvokeControl.SetText(LabelWeightNetto, LocalizationCore.Scales.FieldWeightNetto);
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldWeightNetto, $"{0:0.000} {LocalizationCore.Scales.UnitKg}");
                MDSoft.WinFormsUtils.InvokeControl.SetText(LabelWeightTare, LocalizationCore.Scales.FieldWeightTare);
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldWeightTare, $"{0:0.000} {LocalizationCore.Scales.UnitKg}");
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldThreshold, LocalizationCore.Scales.FieldThresholds);
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGet, LocalizationCore.Scales.ComPort);
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGetCrc, LocalizationCore.Scales.Crc);
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaSet, LocalizationCore.Scales.ScaleQueue);
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaSetCrc, LocalizationCore.Scales.Crc);
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaScalePar, LocalizationCore.Scales.RequestParameters);

                MDSoft.WinFormsUtils.InvokeControl.SetVisible(LabelWeightNetto, true);
                MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldWeightNetto, true);
                MDSoft.WinFormsUtils.InvokeControl.SetVisible(LabelWeightTare, true);
                MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldWeightTare, true);
            },
            1_000, 0_200, 0_300, 1_000, 1_000);
        }

        public new void Open()
        {
            try
            {
                Open(
                () =>
                {
                    if (SessionStateHelper.Instance.CurrentPlu?.IsCheckWeight == true)
                    {
                        MassaDevice?.Open();
                    }
                    if (SessionStateHelper.Instance.CurrentPlu == null)
                    {
                        if (FieldThreshold.Visible)
                            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldThreshold, false);
                        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldThreshold, 
                            $"{LocalizationCore.Scales.FieldThresholds}: {LocalizationCore.Scales.StateDisable}");
                    }
                    else {
                        if (!FieldThreshold.Visible)
                            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldThreshold, true);
                        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldThreshold, $"{LocalizationCore.Scales.FieldThresholds}: " +
                            (SessionStateHelper.Instance.CurrentPlu == null ? $"{LocalizationCore.Scales.StateDisable}" :
                            $"{LocalizationCore.Scales.FieldThresholdLower}: {SessionStateHelper.Instance.CurrentPlu.LowerWeightThreshold:0.000} {LocalizationCore.Scales.UnitKg} | " +
                            $"{LocalizationCore.Scales.FieldThresholdNominal}: {SessionStateHelper.Instance.CurrentPlu.NominalWeight:0.000} {LocalizationCore.Scales.UnitKg} | " +
                            $"{LocalizationCore.Scales.FieldThresholdUpper}: {SessionStateHelper.Instance.CurrentPlu.UpperWeightThreshold:0.000} {LocalizationCore.Scales.UnitKg}"));
                    }
                },
                () =>
                {
                    if (SessionStateHelper.Instance.CurrentPlu?.IsCheckWeight == true)
                    {
                        if (MassaDevice?.IsConnected == true)
                            GetMassa();
                        else
                            ClearRequests(0);
                        RequestMassa();
                        RequestGetMassa();
                        RequestSetMassa();
                        if (!FieldMassaGet.Visible)
                            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaGet, true);
                        if (!FieldMassaSet.Visible)
                            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaSet, true);
                        if (!FieldMassaScalePar.Visible)
                            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaScalePar, true);
                        if (!FieldMassaGetCrc.Visible)
                            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaGetCrc, true);
                        if (!FieldMassaSetCrc.Visible)
                            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaSetCrc, true);
                        if (!FieldMassaQueriesProgress.Visible)
                            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaQueriesProgress, true);
                    }
                    else {
                        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGet, $"{LocalizationCore.Scales.ComPort}: {LocalizationCore.Scales.StateDisable}");
                        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGetCrc, $"{LocalizationCore.Scales.Crc}");
                        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaSet, $"{LocalizationCore.Scales.ScaleQueue} : {LocalizationCore.Scales.StateDisable}");
                        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaSetCrc, $"{LocalizationCore.Scales.Crc}");
                        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaScalePar, $"{LocalizationCore.Scales.RequestParameters}: {LocalizationCore.Scales.StateDisable}");
                        if (FieldMassaGet.Visible)
                            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaGet, false);
                        if (FieldMassaSet.Visible)
                            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaSet, false);
                        if (FieldMassaScalePar.Visible)
                            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaScalePar, false);
                        if (FieldMassaGetCrc.Visible)
                            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaGetCrc, false);
                        if (FieldMassaSetCrc.Visible)
                            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaSetCrc, false);
                        if (FieldMassaQueriesProgress.Visible)
                            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaQueriesProgress, false);
                    }
                },
                () =>
                {
                    if (SessionStateHelper.Instance.CurrentPlu?.IsCheckWeight == true)
                    {
                        if (MassaDevice?.IsConnected == true)
                            OpenResponse();
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

        private void RequestMassa()
        {
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldWeightTare,
                SessionStateHelper.Instance.CurrentPlu != null
                ? $"{SessionStateHelper.Instance.CurrentPlu.GoodsTareWeight:0.000} {LocalizationCore.Scales.UnitKg}"
                : $"0,000 {LocalizationCore.Scales.UnitKg}");

            if (SessionStateHelper.Instance.CurrentPlu == null)
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldWeightNetto, $"{0:0.000} {LocalizationCore.Scales.UnitKg}");
            }
            else
            {
                decimal weight = WeightNet - SessionStateHelper.Instance.CurrentPlu.GoodsTareWeight;
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldWeightNetto, $"{weight:0.000} {LocalizationCore.Scales.UnitKg}");
            }

            if (SessionStateHelper.Instance.CurrentPlu == null)
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaScalePar,
                    $"{0:0.000} {LocalizationCore.Scales.UnitKg} | {LocalizationCore.Scales.RequestParameters}" +
                    (ResponseParseScalePar == null ? string.Empty : $" | {ResponseParseScalePar.Message}"));
            }
            else
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaScalePar,
                    (IsStable == 0
                    ? $"{LocalizationCore.Scales.WeightingProcess}: {WeightNet:0.000} {LocalizationCore.Scales.UnitKg} | " +
                    $"{LocalizationCore.Scales.RequestParameters}"
                    : $"{LocalizationCore.Scales.WeightingStable}: {WeightNet:0.000} {LocalizationCore.Scales.UnitKg}") +
                    (ResponseParseScalePar == null ? string.Empty : $" | {ResponseParseScalePar.Message}"));
            }
        }

        private void RequestSetMassa()
        {
            MDSoft.WinFormsUtils.InvokeProgressBar.SetValue(FieldMassaQueriesProgress, Requests != null ? Requests.Count : 0);
            if (SessionStateHelper.Instance.CurrentPlu?.IsCheckWeight == true)
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaSet,
                    $"{LocalizationCore.Scales.ScaleQueue}: {Requests?.Count} | {LocalizationCore.Scales.WeightingScaleCmd}: " +
                    (ResponseParseSet == null ? $"..." : ResponseParseSet.Message));
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaSetCrc,
                    $"{LocalizationCore.Scales.Crc}: " + (ResponseParseSet == null
                    ? $"..." : (ResponseParseSet.IsValidAll ? $"{LocalizationCore.Scales.StateCorrect}" : $"{LocalizationCore.Scales.StateError}!"
                )));
            }
        }

        private void RequestGetMassa()
        {
            string massaDevice = MassaDevice != null
                ? MassaDevice.IsConnected
                    ? $"{LocalizationCore.Scales.ComPort}: {LocalizationCore.Scales.StateResponsed} | "
                    : $"{LocalizationCore.Scales.ComPort}: {LocalizationCore.Scales.StateNotResponsed} | "
                : $"{LocalizationCore.Scales.ComPort}: {LocalizationCore.Scales.StateDisable} | ";
            if (SessionStateHelper.Instance.CurrentPlu?.IsCheckWeight == true)
            {
                if (ResponseParseGet == null)
                {
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGet, massaDevice +
                        $"{LocalizationCore.Scales.Message}: ...");
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGetCrc, $"{LocalizationCore.Scales.Crc}: ...");
                }
                else
                {
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGet, massaDevice +
                        $"{LocalizationCore.Scales.Message}: {ResponseParseGet.Message}");
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGetCrc, $"{LocalizationCore.Scales.Crc}: " +
                        (ResponseParseGet.IsValidAll ? $"{LocalizationCore.Scales.StateCorrect}" : $"{LocalizationCore.Scales.StateError}!"));
                }
            }
        }

        public new void Close()
        {
            MassaDevice?.Close();
            while (Requests?.Count > 0)
            {
                Requests.Take();
            }

            base.Close();
        }

        public new void ReleaseManaged()
        {
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(LabelWeightNetto, false);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldWeightNetto, false);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(LabelWeightTare, false);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldWeightTare, false);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaQueriesProgress, false);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldThreshold, false);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaGet, false);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaGetCrc, false);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaSet, false);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaSetCrc, false);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaScalePar, false);

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

        public void ClearRequests(ushort limit)
        {
            if (Requests.Count > limit)
            {
                Requests = new BlockingCollection<MassaExchangeEntity>();
            }
        }

        public void OpenResponse()
        {
            ClearRequests(100);

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
                        IsStable = massaExchange.ResponseParse.Massa.Stable;
                        // 1 байт. Признак индикации<NET>: 0 – нет индикации, 1 – есть индикация. ... = x.Net;
                        //byte Zero. 1 байт. Признак индикации > 0 < : 0 – нет индикации, 1 – есть индикация. ... = x.Zero;
                        break;
                }
            }
        }

        public void ResetMassa()
        {
            WeightGross = WeightNet = 0;
            IsStable = 1;
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
