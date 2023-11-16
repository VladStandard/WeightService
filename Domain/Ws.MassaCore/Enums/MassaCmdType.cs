namespace Ws.MassaCore.Enums;

public enum MassaCmdType
{
	Nack = 0,
	UdpPoll,
	GetInit2,
	GetInit3,
	GetEthernet,
	GetWiFiIp,
	GetWiFiSsid,
	GetMassa,
	GetName,
	GetScalePar,
	GetScaleParAfter,
	GetSys,
	GetTare,
	GetWeight,
	SetEthernet,
	SetWiFiIp,
	SetWiFiSsid,
	SetDatetime,
	SetName,
	SetRegnum,
	SetTare,
	SetZero,
	ResponseParse,
}