// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsWeight.WinForms.Utils;
using Label = System.Windows.Forms.Label;

namespace WsWeight.Plugins.Helpers;

public class PluginMassaHelper : PluginHelperBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static PluginMassaHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static PluginMassaHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields and properties

    private MassaRequestHelper MassaRequest => MassaRequestHelper.Instance;
    private MassaExchangeHelper MassaExchange => MassaExchangeHelper.Instance;
    public MassaDeviceHelper MassaDevice => MassaDeviceHelper.Instance;
    private Label FieldMassa { get; set; }
    private Label FieldMassaExt { get; set; }
    private Label FieldNettoWeight { get; set; }
    private readonly object _locker = new();
    public MassaStableModel MassaStable { get; } = new();
    private decimal WeightGross { get; set; }
    private decimal _weightNet;
    public decimal WeightNet
    {
        get => _weightNet;
        set
        {
            if (!IsWeightNetFake)
                _weightNet = value;
        }
    }
    private int ScaleFactor { get; set; } = 1_000;
    private ResponseParseModel ResponseParseGet { get; set; }
    private ResponseParseModel ResponseParseScalePar { get; set; }
    private ResponseParseModel ResponseParseSet { get; set; }
    public bool IsWeightNetFake { get; set; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public PluginMassaHelper()
    {
        TskType = TaskType.TaskMassa;
        FieldMassa = new();
        FieldMassaExt = new();
        FieldNettoWeight = new();
        ResponseParseScalePar = new();
        ResponseParseGet = new();
        ResponseParseSet = new();
    }

    #endregion

    #region Public and private methods

    public void Init(ConfigModel configReopen, ConfigModel configRequest, ConfigModel configResponse,
        Label fieldNettoWeight, Label fieldMassa, Label fieldMassaExt)
    {
        base.Init();
        ReopenItem.Config = configReopen;
        RequestItem.Config = configRequest;
        ResponseItem.Config = configResponse;
        ActionUtils.ActionTryCatch(() =>
        {
            if (UserSessionHelper.Instance.Scale.IsNotNew)
            {
                MassaDevice.Init(UserSessionHelper.Instance.Scale.DeviceComPort,
                    UserSessionHelper.Instance.Scale.DeviceReceiveTimeout,
                    UserSessionHelper.Instance.Scale.DeviceSendTimeout, GetData);
            }
            FieldNettoWeight = fieldNettoWeight;
            FieldMassa = fieldMassa;
            FieldMassaExt = fieldMassaExt;
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
        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaExt, $"{ReopenCounter} | {RequestCounter} | {ReopenCounter}");

        if (UserSessionHelper.Instance.PluScale.Plu.IsNew) return;
        if (!UserSessionHelper.Instance.PluScale.Plu.IsCheckWeight) return;
        MassaDevice.Execute();
    }

    private void Request()
    {
        if (UserSessionHelper.Instance.PluScale.Plu.IsNew) return;
        if (UserSessionHelper.Instance.PluScale.Plu.IsCheckWeight)
        {
            if (MassaDevice.IsOpenPort)
                GetMassa();

            if (MassaDevice.IsOpenPort)
            {
                //ClearRequestsByLimit(100);
                //if (Requests.Count > 0)
                //{
                //    foreach (MassaExchangeEntity MassaExchange in Requests.GetConsumingEnumerable())
                //    {
                //        if (MassaDevice is null || MassaExchange is null) return;
                //        SendData(MassaExchange);
                //    }
                //}
                //Requests = new BlockingCollection<MassaExchangeEntity>();
                SendData();
            }
            else
            {
                ResetMassa();
            }
        }
    }

    private void Response()
    {
        if (!UserSessionHelper.Instance.PluScale.Plu.IsCheckWeight)
            SetControlsTextDefault();
        else
            SetControlsText();
    }

    private void SetControlsTextDefault()
    {
        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldNettoWeight, $"{0:0.000} {LocaleCore.Scales.WeightUnitKg}");
        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassa, LocaleCore.Scales.ComPort);
        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaExt, $"{ReopenCounter} | {RequestCounter} | {ResponseCounter}");
    }

    private void SetControlsText()
    {
        switch (MassaDevice.PortController.AdapterStatus)
        {
            case UsbAdapterStatus.IsNotConnectWithMassa:
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassa, LocaleCore.Scales.IsNotConnectWithMassa);
                break;
            case UsbAdapterStatus.IsDataNotExists:
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassa, LocaleCore.Scales.IsDataNotExists);
                break;
            case UsbAdapterStatus.IsException:
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassa,
                    LocaleCore.Scales.IsException(MassaDevice.PortController.CatchException?.Message));
                break;
            default:
                string massaDevice = MassaDevice.IsOpenPort
                    ? $"{LocaleCore.Scales.MassaK} | {LocaleCore.Scales.StateIsResponsed} | "
                    : $"{LocaleCore.Scales.MassaK} | {LocaleCore.Scales.StateIsNotResponsed} | ";
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassa, $"{massaDevice} | {ResponseParseGet.Message}");
                break;
        }

        decimal weight = UserSessionHelper.Instance.PluScale.IsNew ? 0 : WeightNet - UserSessionHelper.Instance.PluNestingFk.WeightTare;
        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldNettoWeight, MassaStable.IsStable
            ? $"{weight:0.000} {LocaleCore.Scales.WeightUnitKg}"
            : $"{LocaleCore.Scales.WeightingIsCalc}");
    }

    public override void Close()
    {
        base.Close();

        MassaStable.StopwatchStable.Stop();
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
            ParseSetMassa();
        }
    }

    private void ParseSetResponse()
    {
        //if (MassaExchange is null)
        //{
        //    ResponseParseGet = new();
        //    ResponseParseScalePar = new();
        //    ResponseParseSet = new();
        //    return;
        //}
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

    private void ParseSetMassa()
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
                decimal weightTare = MassaExchange.ResponseParse.Massa.Tare / (decimal)ScaleFactor;
                // 4 байта. Текущая масса тары со знаком
                WeightGross = WeightNet + weightTare;
                // 1 байт. Признак стабилизации массы: 0 – нестабильна, 1 – стабильна
                MassaStable.IsStable = MassaExchange.ResponseParse.Massa.IsStable == 0x01;
                // 1 байт. Признак индикации<NET>: 0 – нет индикации, 1 – есть индикация. ... = x.Net;
                //byte Zero. 1 байт. Признак индикации > 0 < : 0 – нет индикации, 1 – есть индикация. ... = x.Zero;
                break;
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