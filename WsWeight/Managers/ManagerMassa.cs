// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;
using DataCore.Managers;
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

	private Label FieldMassaGet { get; set; }
	private Label FieldNettoWeight { get; set; }
	private MassaRequestHelper MassaRequest { get; set; } = MassaRequestHelper.Instance;
	private readonly object _locker = new();
	private MassaExchangeHelper MassaExchange { get; set; }
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
	public MassaDeviceHelper MassaDevice => MassaDeviceHelper.Instance;
	private ResponseParseModel ResponseParseGet { get; set; }
    private ResponseParseModel ResponseParseScalePar { get; set; }
    private ResponseParseModel ResponseParseSet { get; set; }
	public bool IsWeightNetFake { get; set; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public ManagerMassa() : base()
    {
        //ResponseParseScalePar = new();
        //ResponseParseGet = new();
        //ResponseParseSet = new();
		Init(Close, ReleaseManaged, ReleaseUnmanaged);
	}

	#endregion

	#region Public and private methods

	public void Init(Label fieldNettoWeight, Label fieldMassaGet)
	{
		try
		{
			Init(TaskTypeEnum.MassaManager,
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
				new(waitReopen: 1_000, waitRequest: 0_250, waitResponse: 0_250, waitClose: 0_500, waitException: 0_500,
					true, Application.DoEvents));
		}
		catch (Exception ex)
		{
			WpfUtils.CatchException(ex);
		}
	}

	public new void Open()
	{
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
		if (UserSessionHelper.Instance.PluScale.Plu.IsCheckWeight)
			MassaDevice.Open();
	}

	private void Request()
	{
		if (UserSessionHelper.Instance.PluScale.Plu.IsCheckWeight)
		{
			if (MassaDevice.IsOpenPort)
				GetMassa();

			if (MassaDevice.IsOpenPort)
			{
				//ClearRequestsByLimit(100);
				//if (Requests.Count > 0)
				//{
				//    foreach (MassaExchangeEntity massaExchange in Requests.GetConsumingEnumerable())
				//    {
				//        if (MassaDevice is null || massaExchange is null) return;
				//        SendData(massaExchange);
				//    }
				//}
				//Requests = new BlockingCollection<MassaExchangeEntity>();
				SendData(MassaExchange);
			}
			else
			{
				ResetMassa();
			}
		}
	}

	private void Response()
	{
		if (UserSessionHelper.Instance.PluScale.Plu.IsCheckWeight)
		{
			SetControlsText();
		}
		else
		{
			if (UserSessionHelper.Instance.PluScale.IsNew)
				SetControlsTextDefault();
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
		MassaExchange = new();
	}

	public new void ReleaseManaged()
	{
		ResponseParseScalePar = new();
		ResponseParseGet = new();
		ResponseParseSet = new();

		MassaExchange = new();
		MassaRequest = new();
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

	private void SendData(MassaExchangeHelper massaExchange)
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

        MassaDevice.SendData(massaExchange);
	}

	private void GetData(MassaExchangeHelper massaExchange, byte[] response)
	{
		lock (_locker)
		{
			if (response.Length == 0)
				return;

			massaExchange.ResponseParse = new(massaExchange.CmdType, response);
			ParseSetResponse(massaExchange);
			ParseSetMassa(massaExchange);
		}
	}

	private void ParseSetResponse(MassaExchangeHelper? massaExchange)
	{
        if (massaExchange is null)
        {
            ResponseParseGet = new();
            ResponseParseScalePar = new();
            ResponseParseSet = new();
            return;
        }

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

	private void ParseSetMassa(MassaExchangeHelper massaExchange)
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
				MassaStable.IsStable = massaExchange.ResponseParse.Massa.IsStable == 0x01;
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

    private void GetInit1() => MassaExchange = new(MassaCmdType.UdpPoll);
    private void GetInit2() => MassaExchange = new(MassaCmdType.GetInit2);
    private void GetInit3() => MassaExchange = new(MassaCmdType.GetInit3);
    private void GetMassa() => MassaExchange = new(MassaCmdType.GetMassa);
    private void GetScalePar() => MassaExchange = new(MassaCmdType.GetScalePar);
    private void GetScaleParAfter() => MassaExchange = new(MassaCmdType.GetScaleParAfter);
    private void SetWeightTare(int weightTare) => MassaExchange = new(MassaCmdType.SetTare, weightTare);
    private void SetZero() => MassaExchange = new(MassaCmdType.SetZero);

	#endregion
}