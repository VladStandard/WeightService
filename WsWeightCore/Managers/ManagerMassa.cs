// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;
using DataCore.Managers;
using WeightCore.Helpers;
using WeightCore.MassaK;
using WeightCore.MassaK.Enums;
using WeightCore.Wpf.Utils;
using WsLocalization.Models;

namespace WeightCore.Managers;

public class ManagerMassa : ManagerBase
{
	#region Public and private fields and properties

	private Label FieldMassaGet { get; set; }
	private Label FieldMassaPluDescription { get; set; }
	private Label FieldMassaThreshold { get; set; }
	private Label FieldNettoWeight { get; set; }
	private Label FieldPackageWeight { get; set; }
	private Label LabelNettoWeight { get; set; }
	private Label LabelPackageWeight { get; set; }
	private MassaRequestHelper MassaRequest { get; set; } = MassaRequestHelper.Instance;
	private readonly object _locker = new();
	//private BlockingCollection<MassaExchangeEntity> Requests { get; set; } = new();
	private MassaExchangeModel MassaExchange { get; set; }
	public MassaStableModel MassaStable { get; } = new();
	public MassaStableModel MassaStableEmpty { get; } = new();
	public decimal WeightGross { get; private set; }
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
	public int ScaleFactor { get; set; } = 1_000;
	public MassaDeviceModel MassaDevice { get; private set; }
	public ResponseParseModel ResponseParseGet { get; private set; }
	public ResponseParseModel ResponseParseScalePar { get; private set; }
	public ResponseParseModel ResponseParseSet { get; private set; }
	public bool IsWeightNetFake { get; set; }

	#endregion

	#region Constructor and destructor

	public ManagerMassa() : base()
	{
		Init(Close, ReleaseManaged, ReleaseUnmanaged);
	}

	#endregion

	#region Public and private methods

	public void Init(Label labelNettoWeight, Label fieldNettoWeight, 
		Label labelPackageWeight, Label fieldPackageWeight,
		Label fieldMassaThreshold, Label fieldMassaGet, Label fieldMassaPluDescription)
	{
		try
		{
			Init(TaskTypeEnum.MassaManager,
				() =>
				{
					if (UserSessionHelper.Instance.Scale.IsNotNew)
					{
						MassaDevice = new(UserSessionHelper.Instance.Scale.DeviceComPort,
							UserSessionHelper.Instance.Scale.DeviceReceiveTimeout,
							UserSessionHelper.Instance.Scale.DeviceSendTimeout, GetData);
					}
					FieldMassaGet = fieldMassaGet;
					FieldMassaPluDescription = fieldMassaPluDescription;
					FieldMassaThreshold = fieldMassaThreshold;
					FieldNettoWeight = fieldNettoWeight;
					FieldPackageWeight = fieldPackageWeight;
					LabelNettoWeight = labelNettoWeight;
					LabelPackageWeight = labelPackageWeight;

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
			Open(
				// Reopen.
				() =>
				{
					Reopen();
				},
				// Request.
				() =>
				{
					Request();
				},
				// Response.
				() =>
				{
					Response();
				});
		}
		catch (Exception ex)
		{
			WpfUtils.CatchException(ex);
		}
	}

	private void Reopen()
	{
		if (UserSessionHelper.Instance.PluScale.Plu.IsCheckWeight)
			MassaDevice?.Open();
		if (UserSessionHelper.Instance.PluScale.IsNew)
			MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaThreshold, $"{LocaleCore.Scales.FieldThresholds}: {LocaleCore.Scales.StateDisable}");
		else
			MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaThreshold,
				$"{LocaleCore.Scales.FieldThresholds}: " +
				$"{LocaleCore.Scales.FieldThresholdLower}: {UserSessionHelper.Instance.PluNestingFk.WeightMin:0.000} {LocaleCore.Scales.WeightUnitKg} | " +
				$"{LocaleCore.Scales.FieldThresholdNominal}: {UserSessionHelper.Instance.PluNestingFk.WeightNom:0.000} {LocaleCore.Scales.WeightUnitKg} | " +
				$"{LocaleCore.Scales.FieldThresholdUpper}: {UserSessionHelper.Instance.PluNestingFk.WeightMax:0.000} {LocaleCore.Scales.WeightUnitKg}");
		SetControlsVisible(true, true);
	}

	private void Request()
	{
		if (UserSessionHelper.Instance.PluScale.Plu.IsCheckWeight)
		{
			if (MassaDevice?.IsOpenPort == true)
				GetMassa();
			//else
			//    ClearRequestsByLimit(0);

			if (MassaDevice?.IsOpenPort == true)
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
			SetControlsVisible(false, true);
		}
		else
		{
			if (UserSessionHelper.Instance.PluScale.IsNew)
				SetControlsTextDefault();
			SetControlsVisible(false, false);
		}
	}

	private void SetControlsTextDefault()
	{
		MDSoft.WinFormsUtils.InvokeControl.SetText(LabelNettoWeight, LocaleCore.Scales.FieldWeightNetto);
		MDSoft.WinFormsUtils.InvokeControl.SetText(FieldNettoWeight, $"{0:0.000} {LocaleCore.Scales.WeightUnitKg}");
		MDSoft.WinFormsUtils.InvokeControl.SetText(LabelPackageWeight, LocaleCore.Scales.FieldWeightTare);
		MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPackageWeight, $"{0:0.000} {LocaleCore.Scales.WeightUnitKg}");
		MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaThreshold, LocaleCore.Scales.FieldThresholds);

		MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGet, LocaleCore.Scales.ComPort);
		MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaPluDescription, LocaleCore.Scales.RequestParameters);
	}

	private void SetControlsText()
	{
            
		switch (MassaDevice.PortController.AdapterStatus)
		{
			case MDSoft.SerialPorts.SerialPortController.EnumUsbAdapterStatus.IsNotConnectWithMassa:
				MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGet, LocaleCore.Scales.IsNotConnectWithMassa);
				break;
			case MDSoft.SerialPorts.SerialPortController.EnumUsbAdapterStatus.IsDataNotExists:
				MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGet, LocaleCore.Scales.IsDataNotExists);
				break;
			case MDSoft.SerialPorts.SerialPortController.EnumUsbAdapterStatus.IsException:
				MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGet, 
					LocaleCore.Scales.IsException(MassaDevice.PortController.CatchException.Message));
				break;
			case MDSoft.SerialPorts.SerialPortController.EnumUsbAdapterStatus.Default:
			case MDSoft.SerialPorts.SerialPortController.EnumUsbAdapterStatus.IsConnectWithMassa:
			case MDSoft.SerialPorts.SerialPortController.EnumUsbAdapterStatus.IsDataExists:
			default:
				string massaDevice = MassaDevice is not null
					? MassaDevice.IsOpenPort
						? $"{LocaleCore.Scales.ComPort}: {LocaleCore.Scales.StateResponsed} | "
						: $"{LocaleCore.Scales.ComPort}: {LocaleCore.Scales.StateNotResponsed} | "
					: $"{LocaleCore.Scales.ComPort}: {LocaleCore.Scales.StateDisable} | ";
				MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGet, ResponseParseGet is null
					? $"{massaDevice} {LocaleCore.Scales.Message}: ..."
					: $"{massaDevice} {LocaleCore.Scales.Message}: {ResponseParseGet.Message}");
				break;
		}

		MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPackageWeight,
			UserSessionHelper.Instance.PluScale.IsNotNew
				? $"{UserSessionHelper.Instance.PluNestingFk.WeightTare:0.000} {LocaleCore.Scales.WeightUnitKg}"
				: $"0,000 {LocaleCore.Scales.WeightUnitKg}");

		decimal weight = UserSessionHelper.Instance.PluScale.IsNew ? 0 : WeightNet - UserSessionHelper.Instance.PluNestingFk.WeightTare;
		MDSoft.WinFormsUtils.InvokeControl.SetText(FieldNettoWeight, MassaStable.IsStable
			? $"{weight:0.000} {LocaleCore.Scales.WeightUnitKg}"
			:
#if DEBUG
			$"{LocaleCore.Scales.WeightingIsCalc}" +
			$" ({(ushort)MassaStable.StopwatchStable.Elapsed.TotalMilliseconds})");
#else
                $"{LocaleCore.Scales.WeightingIsCalc}");
#endif

		MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaPluDescription,
			$"{LocaleCore.Scales.WeightingProcess}: " +
			(UserSessionHelper.Instance.PluScale.IsNew ? $"{0:0.000} " : $"{WeightNet:0.000} ") +
			$"{LocaleCore.Scales.WeightUnitKg} | {LocaleCore.Scales.RequestParameters}" +
			(ResponseParseScalePar is null ? string.Empty : $" | {ResponseParseScalePar.Message}"));
	}

	private void SetControlsVisible(bool isTopControls, bool isVisible)
	{
		if (isTopControls)
		{
			if (LabelNettoWeight.Visible != isVisible)
				MDSoft.WinFormsUtils.InvokeControl.SetVisible(LabelNettoWeight, isVisible);
			if (FieldNettoWeight.Visible != isVisible)
				MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldNettoWeight, isVisible);
			if (LabelPackageWeight.Visible != isVisible)
				MDSoft.WinFormsUtils.InvokeControl.SetVisible(LabelPackageWeight, isVisible);
			if (FieldPackageWeight.Visible != isVisible)
				MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldPackageWeight, isVisible);
		}
		else
		{
			if (UserSessionHelper.Instance.PluScale.IsNew)
			{
				if (FieldMassaThreshold.Visible != isVisible)
					MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaThreshold, isVisible);
			}
			else
			{
				if (!FieldMassaThreshold.Visible)
					MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaThreshold, true);
			}

			if (FieldMassaGet.Visible != isVisible)
				MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaGet, isVisible);
			if (!FieldMassaPluDescription.Visible)
				MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaPluDescription, isVisible);
		}
	}

	public new void Close()
	{
		base.Close();

		MassaStable.StopwatchStable.Stop();
		MassaDevice?.Close();
		MassaExchange = null;
		//while (Requests?.Count > 0)
		//{
		//    Requests.Take();
		//}
	}

	public new void ReleaseManaged()
	{
		SetControlsVisible(true, false);
		SetControlsVisible(false, false);

		ResponseParseScalePar = null;
		ResponseParseGet = null;
		ResponseParseSet = null;

		//Requests?.Dispose();
		MassaExchange = null;
		MassaRequest = null;
		MassaDevice?.Dispose(true);

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

	private void SendData(MassaExchangeModel massaExchange)
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
		if (massaExchange.Request is null)
			return;

		MassaDevice?.SendData(massaExchange);
	}

	private void GetData(MassaExchangeModel massaExchange, byte[] response)
	{
		lock (_locker)
		{
			if (response is null || response.Length == 0)
				return;

			if (massaExchange is not null)
			{
				massaExchange.ResponseParse = new ResponseParseModel(massaExchange.CmdType, response);
				ParseSetResponse(massaExchange);
				ParseSetMassa(massaExchange);
			}
		}
	}

	private void ParseSetResponse(MassaExchangeModel massaExchange)
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

	private void ParseSetMassa(MassaExchangeModel massaExchange)
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

	public void GetInit1() => MassaExchange = new(MassaCmdType.UdpPoll);
	public void GetInit2() => MassaExchange = new(MassaCmdType.GetInit2);
	public void GetInit3() => MassaExchange = new(MassaCmdType.GetInit3);
	public void GetMassa() => MassaExchange = new(MassaCmdType.GetMassa);
	public void GetScalePar() => MassaExchange = new(MassaCmdType.GetScalePar);
	public void GetScaleParAfter() => MassaExchange = new(MassaCmdType.GetScaleParAfter);
	public void SetWeightTare(int weightTare) => MassaExchange = new(MassaCmdType.SetTare, weightTare);
	public void SetZero() => MassaExchange = new(MassaCmdType.SetZero);

	#endregion
}