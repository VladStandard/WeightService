namespace Ws.LabelCore.Helpers;

/// <summary>
/// Плагин весов Масса-К.
/// </summary>
public sealed class PluginMassaHelper : PluginBaseHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static PluginMassaHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static PluginMassaHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields and properties

    private readonly object _locker = new();

    private MassaRequestHelper MassaRequest => MassaRequestHelper.Instance;
    private MassaExchangeHelper MassaExchange => MassaExchangeHelper.Instance;
    private MassaDeviceHelper MassaDevice => MassaDeviceHelper.Instance;
    private LabelSessionHelper LabelSession => LabelSessionHelper.Instance;
    private Label FieldMassa { get; set; }
    private Label FieldMassaExt { get; set; }
    private Label FieldNettoWeight { get; set; }
    public bool IsStable { get; private set; }
    public decimal WeightNet { get; private set; }
    private int ScaleFactor { get ; set; } = 1_000;
    private ResponseParseModel ResponseParseGet { get; set; }
    private ResponseParseModel ResponseParseScalePar { get; set; }
    private ResponseParseModel ResponseParseSet { get; set; }
    private Action ResetWarning { get; set; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public PluginMassaHelper()
    {
        PluginType = EnumPluginType.Massa;
        ResponseItem.PluginType = RequestItem.PluginType = ReopenItem.PluginType = PluginType;
        
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

    public void Init(PluginConfigModel configReopen, PluginConfigModel configRequest, PluginConfigModel configResponse,
        Label fieldNettoWeight, Label fieldMassa, Action resetWarning)
    {
        ReopenItem.Config = configReopen;
        RequestItem.Config = configRequest;
        ResponseItem.Config = configResponse;
        FieldNettoWeight = fieldNettoWeight;
        FieldMassa = fieldMassa;
        ResetWarning = resetWarning;
        MassaDevice.Init(LabelSession.Line.DeviceComPort, GetData);
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
        if (LabelSession.PluLine.Plu.IsNew) return;
        if (!LabelSession.PluLine.Plu.IsCheckWeight) return;
        MassaDevice.Execute();
    }

    private void Request()
    {
        if (LabelSession.PluLine.Plu.IsNew) return;
        if (!LabelSession.PluLine.Plu.IsCheckWeight) return;
        if (!MassaDevice.IsOpenPort) return;
        GetMassa();
        SendData();
    }

    private void Response()
    {
        if (!LabelSession.PluLine.Plu.IsCheckWeight)
            MdInvokeControl.SetVisible(FieldMassa, false);
        else
            SetLabelsText();
    }

    /// <summary>
    /// Задать текст весовой ПЛУ.
    /// </summary>
    private void SetLabelsText()
    {
        MdInvokeControl.SetVisible(FieldMassa, true);
        MdInvokeControl.SetForeColor(FieldMassa, Color.Red);
        string name = $"{LocaleCore.LabelPrint.MassaK} | {MassaDevice.ComPort}";
        switch (MassaDevice.SerialPort.AdapterStatus)
        {
            case UsbAdapterStatus.IsDataNotExists:
                MdInvokeControl.SetText(FieldMassa, $"{name} | {LocaleCore.LabelPrint.IsDataNotExists}");
                break;
            case UsbAdapterStatus.IsException:
                MdInvokeControl.SetText(FieldMassa, $"{name} | {LocaleCore.LabelPrint.IsException(MassaDevice.SerialPort.Exception.Message)}");
                break;
            case UsbAdapterStatus.IsNotConnectWithMassa:
                MdInvokeControl.SetText(FieldMassa, $"{name} | Отключены");
                break;
            default:
                MdInvokeControl.SetForeColor(FieldMassa, Color.Green);
                MdInvokeControl.SetText(FieldMassa, $"{name} | Подключены | {ResponseParseGet.Message}");
                break;
        }

        decimal weight = LabelSession.PluLine.IsNew 
            ? 0 : WeightNet - LabelSession.ViewPluNesting.TareWeight;

        MdInvokeControl.SetText(FieldNettoWeight, IsStable
            ? $"{weight:0.000} {LocaleCore.LabelPrint.WeightUnitKg}"
            : $"{LocaleCore.LabelPrint.WeightingIsCalc}");
        MdInvokeControl.SetForeColor(FieldNettoWeight, Equals(IsStable, true) && weight > 0 ? Color.Green : Color.Red);
    }

    public override void Dispose()
    {
        base.Dispose();
        MassaDevice.Dispose();
        ReopenItem.Dispose();
        RequestItem.Dispose();
        ResponseItem.Dispose();

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
            case MassaCmdType.GetEthernet:
                MassaExchange.Request = MassaRequest.CMD_GET_ETHERNET;
                break;
            case MassaCmdType.GetInit2:
                MassaExchange.Request = MassaRequest.CMD_GET_INIT_2;
                break;
            case MassaCmdType.GetInit3:
                MassaExchange.Request = MassaRequest.CMD_GET_INIT_3;
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
            case MassaCmdType.GetWiFiIp:
                MassaExchange.Request = MassaRequest.CMD_GET_WIFI_IP;
                break;
            case MassaCmdType.SetTare:
                MassaExchange.Request = MassaExchange.CmdSetTare();
                break;
            case MassaCmdType.SetZero:
                MassaExchange.Request = MassaRequest.CMD_SET_ZERO;
                break;
            case MassaCmdType.UdpPoll:
                MassaExchange.Request = MassaRequest.CMD_UDP_POLL;
                break;
            
        }

        MassaDevice.SendData();
    }

    private void GetData(byte[] response)
    {
        lock (_locker)
        {
            if (response.Length == 0) return;
            MassaExchange.ResponseParse = new(MassaExchange.CmdType, response);
            switch (MassaExchange.CmdType)
            {
                case MassaCmdType.GetMassa:
                    ResponseParseGet = MassaExchange.ResponseParse;
                    // 1 байт. Цена деления в значении массы нетто и массы тары:
                    // 0 – 100 мг, 1 – 1 г, 2 – 10 г, 3 – 100 г, 4 – 1 кг
                    ScaleFactor = MassaExchange.ResponseParse.Massa.ScaleFactor;
                    // 4 байта. Текущая масса нетто со знаком
                    // #HotFix для ошибки "Попытка деления на нуль"
                    WeightNet = ScaleFactor.Equals(0) ? 0 : MassaExchange.ResponseParse.Massa.Weight / (decimal)ScaleFactor;
                    // 4 байта. Текущая масса тары со знаком
                    //decimal weightTare = MassaExchange.ResponseParse.Massa.Tare / (decimal)ScaleFactor;
                    // 4 байта. Текущая масса тары со знаком
                    //WeightGross = WeightNet + weightTare;
                    // 1 байт. Признак стабилизации массы: 0 – нестабильна, 1 – стабильна
                    //MassaStable = new(0_100, MassaExchange.ResponseParse.Massa.IsStable == 0x01);
                    IsStable = MassaExchange.ResponseParse.Massa.IsStable;
                    //if (IsStable) ResetWarning();
                    // 1 байт. Признак индикации<NET>: 0 – нет индикации, 1 – есть индикация. ... = x.Net;
                    //byte Zero. 1 байт. Признак индикации > 0 < : 0 – нет индикации, 1 – есть индикация. ... = x.Zero;
                    break;

                case MassaCmdType.GetScalePar:
                    ResponseParseScalePar = MassaExchange.ResponseParse;
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
                case MassaCmdType.SetWiFiSsid:
                    ResponseParseSet = MassaExchange.ResponseParse;
                    break;
                case MassaCmdType.SetZero:
                    ResponseParseSet = MassaExchange.ResponseParse;
                    break;
                
            }
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