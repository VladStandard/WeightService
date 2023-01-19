// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Forms;
using WsLocalization.Models;
using WsMassa.Controllers;
using WsMassa.Enums;
using WsMassa.Helpers;
using WsMassa.Models;
using WsWeight.Helpers;
using WsWeight.Wpf.Utils;

namespace WsWeight.Managers;

public class ManagerMassa : ManagerBase
{
	#region Public and private fields and properties

	private MassaRequestHelper MassaRequest { get; set; } = MassaRequestHelper.Instance;
    private MassaExchangeHelper MassaExchange => MassaExchangeHelper.Instance;
	public MassaDeviceHelper MassaDevice => MassaDeviceHelper.Instance;
	private Label FieldMassaGet { get; set; }
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
    public ManagerMassa() : base()
    {
        FieldMassaGet = new();
        FieldNettoWeight = new();
        ResponseParseScalePar = new();
        ResponseParseGet = new();
        ResponseParseSet = new();
		Init(Close, ReleaseManaged, ReleaseUnmanaged);
	}

	#endregion

	#region Public and private methods

	public void Init(Label fieldNettoWeight, Label fieldMassaGet)
	{
		try
		{
			Init(TaskType.TaskMassa,
				() =>
				{
					if (UserSessionHelper.Instance.Scale.IsNotNew)
					{
						MassaDevice.Init(UserSessionHelper.Instance.Scale.DeviceComPort,
							UserSessionHelper.Instance.Scale.DeviceReceiveTimeout,
							UserSessionHelper.Instance.Scale.DeviceSendTimeout, GetData);
					}
					FieldMassaGet = fieldMassaGet;
					FieldNettoWeight = fieldNettoWeight;

					SetControlsTextDefault();
				},
                new(waitReopen: 0_250, waitRequest: 0_250, waitResponse: 0_250, waitClose: 0_250, waitException: 0_250));
        }
		catch (Exception ex)
		{
			WpfUtils.CatchException(ex);
		}
	}

	public new void Open()
    {
        base.Open();
		try
		{
			Open(Reopen, Request, Response);
		}
		catch (Exception ex)
		{
			WpfUtils.CatchException(ex);
		}
	}

	private void Reopen()
    {
        if (UserSessionHelper.Instance.PluScale.Plu.IsNew) return;
		if (!UserSessionHelper.Instance.PluScale.Plu.IsCheckWeight) return;
		MassaDevice.Open();
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
        if (UserSessionHelper.Instance.PluScale.Plu.IsNew ||
            !UserSessionHelper.Instance.PluScale.Plu.IsCheckWeight)
        {
            SetControlsTextDefault();
        }
        else
		{
			SetControlsText();
		}
	}

	private void SetControlsTextDefault()
	{
		MDSoft.WinFormsUtils.InvokeControl.SetText(FieldNettoWeight, $"{0:0.000} {LocaleCore.Scales.WeightUnitKg}");
		MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGet, LocaleCore.Scales.ComPort);
	}

	private void SetControlsText()
	{
		switch (MassaDevice.PortController.AdapterStatus)
		{
			case SerialPortController.EnumUsbAdapterStatus.IsNotConnectWithMassa:
				MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGet, LocaleCore.Scales.IsNotConnectWithMassa);
				break;
			case SerialPortController.EnumUsbAdapterStatus.IsDataNotExists:
				MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGet, LocaleCore.Scales.IsDataNotExists);
				break;
			case SerialPortController.EnumUsbAdapterStatus.IsException:
				MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGet, 
					LocaleCore.Scales.IsException(MassaDevice.PortController.CatchException?.Message));
				break;
			case SerialPortController.EnumUsbAdapterStatus.Default:
			case SerialPortController.EnumUsbAdapterStatus.IsConnectWithMassa:
			case SerialPortController.EnumUsbAdapterStatus.IsDataExists:
			default:
                string massaDevice = //MassaDevice is not null ? 
                    MassaDevice.IsOpenPort
                        ? $"{LocaleCore.Scales.ComPort}: {LocaleCore.Scales.StateResponsed} | "
                        : $"{LocaleCore.Scales.ComPort}: {LocaleCore.Scales.StateNotResponsed} | ";
					//: $"{LocaleCore.Scales.ComPort}: {LocaleCore.Scales.StateDisable} | ";
				MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGet, 
                    //ResponseParseGet is null ? $"{massaDevice} {LocaleCore.Scales.Message}: ..." : 
                    $"{massaDevice} {LocaleCore.Scales.Message}: {ResponseParseGet.Message}");
				break;
		}

		decimal weight = UserSessionHelper.Instance.PluScale.IsNew ? 0 : WeightNet - UserSessionHelper.Instance.PluNestingFk.WeightTare;
		MDSoft.WinFormsUtils.InvokeControl.SetText(FieldNettoWeight, MassaStable.IsStable
			? $"{weight:0.000} {LocaleCore.Scales.WeightUnitKg}"
			: $"{LocaleCore.Scales.WeightingIsCalc}");
	}

	public new void Close()
	{
		base.Close();

		MassaStable.StopwatchStable.Stop();
		MassaDevice.Close();
	}

	public new void ReleaseManaged()
	{
		ResponseParseScalePar = new();
		ResponseParseGet = new();
		ResponseParseSet = new();

		MassaDevice.Dispose(true);

		base.ReleaseManaged();
	}

	public new void ReleaseUnmanaged()
	{
		base.ReleaseUnmanaged();
	}

	#endregion

	#region Public and private methods - Control

	//public void ClearRequestsByLimit(ushort countLimit)
	//{
	//    if (Requests.Count > countLimit)
	//    {
	//        Requests = new BlockingCollection<MassaExchangeEntity>();
	//    }
	//}

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