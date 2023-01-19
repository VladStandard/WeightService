﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsMassa.Enums;

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