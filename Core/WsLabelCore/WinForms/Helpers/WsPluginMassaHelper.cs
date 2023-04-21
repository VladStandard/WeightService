// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.WinForms.Helpers;

public sealed class WsPluginMassaHelper : WsPluginHelperBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsPluginMassaHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsPluginMassaHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields and properties

    private MassaRequestHelper MassaRequest => MassaRequestHelper.Instance;
    private MassaExchangeHelper MassaExchange => MassaExchangeHelper.Instance;
    public MassaDeviceHelper MassaDevice => MassaDeviceHelper.Instance;
    private Label FieldMassa { get; set; }
    private Label FieldMassaExt { get; set; }
    private Label FieldNettoWeight { get; set; }
    private readonly object _locker = new();
    //public MassaStableModel MassaStable { get; set; } = new(0_100, false);
    public bool IsStable { get; private set; }
    private decimal _weightNet;
    public decimal WeightNet
    {
        get => _weightNet;
        set { if (!IsWeightNetFake) _weightNet = value; }
    }

    private int ScaleFactor { get ; set; } = 1_000;
    private ResponseParseModel ResponseParseGet { get; set; }
    private ResponseParseModel ResponseParseScalePar { get; set; }
    private ResponseParseModel ResponseParseSet { get; set; }
    public bool IsWeightNetFake { get; set; }
    private Action ResetWarning { get; set; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public WsPluginMassaHelper()
    {
        TskType = TaskType.TaskMassa;
        FieldMassa = new();
        FieldMassaExt = new();
        FieldNettoWeight = new();
        ResponseParseScalePar = new();
        ResponseParseGet = new();
        ResponseParseSet = new();
        ResetWarning = () => { };
    }

    #endregion

    #region Public and private methods

    public void Init(WsConfigModel configReopen, WsConfigModel configRequest, WsConfigModel configResponse,
        Label fieldNettoWeight, Label fieldMassa, Label fieldMassaExt, Action resetWarning)
    {
        base.Init();
        
        ReopenItem.Config = configReopen;
        RequestItem.Config = configRequest;
        ResponseItem.Config = configResponse;
        FieldNettoWeight = fieldNettoWeight;
        FieldMassa = fieldMassa;
        FieldMassaExt = fieldMassaExt;
        ResetWarning = resetWarning;

        WsActionUtils.ActionTryCatch(() =>
        {
            if (WsUserSessionHelper.Instance.Scale.IsNotNew)
            {
                MassaDevice.Init(WsUserSessionHelper.Instance.Scale.DeviceComPort,
                    WsUserSessionHelper.Instance.Scale.DeviceReceiveTimeout,
                    WsUserSessionHelper.Instance.Scale.DeviceSendTimeout, GetData);
            }
            SetControlsTextDefault();
        });
    }

    public override void Execute()
    {
        base.Execute();
        
        ReopenItem.Execute(Reopen);
        RequestItem.Execute(Request);
        ResponseItem.Execute(Response);
    }

    private void Reopen()
    {
        MdInvokeControl.SetText(FieldMassaExt, $"{ReopenCounter} | {RequestCounter} | {ReopenCounter}");

        if (WsUserSessionHelper.Instance.PluScale.Plu.IsNew) return;
        if (!WsUserSessionHelper.Instance.PluScale.Plu.IsCheckWeight) return;
        
        MassaDevice.Execute();
    }

    private void Request()
    {
        if (WsUserSessionHelper.Instance.PluScale.Plu.IsNew) return;
        if (WsUserSessionHelper.Instance.PluScale.Plu.IsCheckWeight)
        {
            if (MassaDevice.IsOpenPort)
            {
                GetMassa();
                SendData();
            }
            //else ResetMassa();
        }
    }

    private void Response()
    {
        if (!WsUserSessionHelper.Instance.PluScale.Plu.IsCheckWeight)
            SetControlsTextDefault();
        else
            SetLabelsText();
    }

    private void SetControlsTextDefault()
    {
        MdInvokeControl.SetText(FieldNettoWeight, $"{0:0.000} {LocaleCore.Scales.WeightUnitKg}");
        MdInvokeControl.SetText(FieldMassa, LocaleCore.Scales.ComPort);
        MdInvokeControl.SetText(FieldMassaExt, $"{ReopenCounter} | {RequestCounter} | {ResponseCounter}");
    }

    /// <summary>
    /// Задать текст весовой ПЛУ.
    /// </summary>
    private void SetLabelsText()
    {
        switch (MassaDevice.PortController.AdapterStatus)
        {
            case UsbAdapterStatus.IsNotConnectWithMassa:
                MdInvokeControl.SetText(FieldMassa, 
                    $"{LocaleCore.Scales.MassaK} | {LocaleCore.Scales.IsNotConnectWithMassa}");
                break;
            case UsbAdapterStatus.IsDataNotExists:
                MdInvokeControl.SetText(FieldMassa,
                    $"{LocaleCore.Scales.MassaK} | {LocaleCore.Scales.IsDataNotExists}");
                break;
            case UsbAdapterStatus.IsException:
                MdInvokeControl.SetText(FieldMassa,
                    $"{LocaleCore.Scales.MassaK} | {LocaleCore.Scales.IsException(MassaDevice.PortController.Exception.Message)}");
                break;
            default:
                MdInvokeControl.SetText(FieldMassa, $"{(MassaDevice.IsOpenPort
                    ? $"{LocaleCore.Scales.MassaK} | {LocaleCore.Scales.StateIsResponsed} | "
                    : $"{LocaleCore.Scales.MassaK} | {LocaleCore.Scales.StateIsNotResponsed} | ")} | {ResponseParseGet.Message}");
                break;
        }

        decimal weight = WsUserSessionHelper.Instance.PluScale.IsNew ? 0 : WeightNet - WsUserSessionHelper.Instance.PluNestingFk.WeightTare;

        MdInvokeControl.SetText(FieldNettoWeight, IsStable
            ? $"{weight:0.000} {LocaleCore.Scales.WeightUnitKg}"
            : $"{LocaleCore.Scales.WeightingIsCalc}");
        MdInvokeControl.SetForeColor(FieldNettoWeight, Equals(IsStable, true) && weight > 0 ? Color.Green : Color.Red);
    }

    public override void Close()
    {
        base.Close();

        //MassaStable.StopwatchStable.Stop();
        MassaDevice.Close();

        ResponseParseScalePar = new();
        ResponseParseGet = new();
        ResponseParseSet = new();
    }

    #endregion

    #region Public and private methods - Control

    private void SendData()
    {
        switch (MassaExchange.CmdType)
        {
            case MassaCmdType.UdpPoll:
                MassaExchange.Request = MassaRequest.CMD_UDP_POLL;
                break;
            case MassaCmdType.GetInit2:
                MassaExchange.Request = MassaRequest.CMD_GET_INIT_2;
                break;
            case MassaCmdType.GetInit3:
                MassaExchange.Request = MassaRequest.CMD_GET_INIT_3;
                break;
            case MassaCmdType.GetEthernet:
                MassaExchange.Request = MassaRequest.CMD_GET_ETHERNET;
                break;
            case MassaCmdType.GetWiFiIp:
                MassaExchange.Request = MassaRequest.CMD_GET_WIFI_IP;
                break;
            case MassaCmdType.GetMassa:
                MassaExchange.Request = MassaRequest.CMD_GET_MASSA;
                break;
            case MassaCmdType.GetName:
                break;
            case MassaCmdType.GetScalePar:
                MassaExchange.Request = MassaRequest.CMD_GET_SCALE_PAR;
                break;
            case MassaCmdType.GetScaleParAfter:
                MassaExchange.Request = MassaRequest.CMD_GET_SCALE_PAR_AFTER;
                break;
            case MassaCmdType.SetTare:
                MassaExchange.Request = MassaExchange.CmdSetTare();
                break;
            case MassaCmdType.SetZero:
                MassaExchange.Request = MassaRequest.CMD_SET_ZERO;
                break;
        }

        MassaDevice.SendData();
    }

    private void GetData(byte[] response)
    {
        lock (_locker)
        {
            if (response.Length == 0)
                return;

            MassaExchange.ResponseParse = new(MassaExchange.CmdType, response);
            ParseSetResponse();
            ParseMassa();
        }
    }

    private void ParseSetResponse()
    {
        switch (MassaExchange.CmdType)
        {
            case MassaCmdType.GetMassa:
                ResponseParseGet = MassaExchange.ResponseParse;
                break;
            case MassaCmdType.GetScalePar:
                ResponseParseScalePar = MassaExchange.ResponseParse;
                break;
            case MassaCmdType.SetWiFiSsid:
                ResponseParseSet = MassaExchange.ResponseParse;
                break;
            case MassaCmdType.SetDatetime:
                ResponseParseSet = MassaExchange.ResponseParse;
                break;
            case MassaCmdType.SetName:
                ResponseParseSet = MassaExchange.ResponseParse;
                break;
            case MassaCmdType.SetRegnum:
                ResponseParseSet = MassaExchange.ResponseParse;
                break;
            case MassaCmdType.SetTare:
                ResponseParseSet = MassaExchange.ResponseParse;
                break;
            case MassaCmdType.SetZero:
                ResponseParseSet = MassaExchange.ResponseParse;
                break;
        }
    }

    private void ParseMassa()
    {
        switch (MassaExchange.CmdType)
        {
            case MassaCmdType.GetMassa:
                // 1 байт. Цена деления в значении массы нетто и массы тары:
                // 0 – 100 мг, 1 – 1 г, 2 – 10 г, 3 – 100 г, 4 – 1 кг
                ScaleFactor = MassaExchange.ResponseParse.Massa.ScaleFactor;
                // 4 байта. Текущая масса нетто со знаком
                WeightNet = MassaExchange.ResponseParse.Massa.Weight / (decimal)ScaleFactor;
                // 4 байта. Текущая масса тары со знаком
                //decimal weightTare = MassaExchange.ResponseParse.Massa.Tare / (decimal)ScaleFactor;
                // 4 байта. Текущая масса тары со знаком
                //WeightGross = WeightNet + weightTare;
                // 1 байт. Признак стабилизации массы: 0 – нестабильна, 1 – стабильна
                //MassaStable = new(0_100, MassaExchange.ResponseParse.Massa.IsStable == 0x01);
                IsStable = MassaExchange.ResponseParse.Massa.IsStable;
                if (IsStable)
                    ResetWarning();
                // 1 байт. Признак индикации<NET>: 0 – нет индикации, 1 – есть индикация. ... = x.Net;
                //byte Zero. 1 байт. Признак индикации > 0 < : 0 – нет индикации, 1 – есть индикация. ... = x.Zero;
                break;
        }
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
        SetWeightTare(0);
        SetZero();
    }

    private void GetInit1() => MassaExchange.Init(MassaCmdType.UdpPoll);
    private void GetInit2() => MassaExchange.Init(MassaCmdType.GetInit2);
    private void GetInit3() => MassaExchange.Init(MassaCmdType.GetInit3);
    private void GetMassa() => MassaExchange.Init(MassaCmdType.GetMassa);
    private void GetScalePar() => MassaExchange.Init(MassaCmdType.GetScalePar);
    private void GetScaleParAfter() => MassaExchange.Init(MassaCmdType.GetScaleParAfter);
    private void SetWeightTare(int weightTare) => MassaExchange.Init(MassaCmdType.SetTare, weightTare);
    private void SetZero() => MassaExchange.Init(MassaCmdType.SetZero);

    #endregion
}