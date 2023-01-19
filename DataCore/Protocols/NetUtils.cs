// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Printers;
using RestSharp;
using System.Net.Sockets;

namespace DataCore.Protocols;

public static class NetUtils
{
	#region Public and private methods

	public static string GetLocalIpAddress()
	{
		try
		{
			IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
			foreach (IPAddress ip in host.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					return ip.ToString();
				}
			}
		}
		catch (Exception ex)
		{
			throw new($"Exception in {nameof(GetLocalIpAddress)}", ex);
		}
		return string.Empty;
	}

	public static string GetLocalDeviceName(bool isThrow)
	{
		try
		{
			return Dns.GetHostName();
		}
		catch (Exception ex)
		{
			if (isThrow)
				throw new($"Exception in {nameof(GetLocalDeviceName)}", ex);
		}
		return string.Empty;
	}

	public static string GetLocalMacAddress()
	{
		string macAddresses = string.Empty;
		foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
		{
			if (nic.OperationalStatus == OperationalStatus.Up)
			{
				macAddresses += nic.GetPhysicalAddress().ToString();
				break;
			}
		}
		return macAddresses;
	}

	public static void RequestHttpStatus(PrinterModel printer, int timeOut)
	{
		if (printer.HttpStatusCode == HttpStatusCode.OK)
			return;
		printer.HttpStatusCode = HttpStatusCode.BadRequest;
		printer.HttpStatusException = null;
		RestClientOptions options = new(printer.Link)
		{
			UseDefaultCredentials = true,
			ThrowOnAnyError = true,
			MaxTimeout = timeOut,
			RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };
		using RestClient client = new(options);
		RestRequest request = new();
		try
		{
			RestResponse response = client.GetAsync(request).ConfigureAwait(true).GetAwaiter().GetResult();
			printer.HttpStatusCode = response.StatusCode;
		}
		catch (Exception ex)
		{
			printer.HttpStatusException = ex;
		}
	}

	public static bool RequestPing(PrinterModel? printer, int timeOut)
	{
		if (printer is null) return false;
		try
		{
			using Ping ping = new();
			PingReply? pingReply = ping.Send(printer.Ip, timeOut);
			if (pingReply is null) return false;
			return (printer.PingStatus = pingReply.Status) == IPStatus.Success;
		}
		catch (Exception ex)
		{
			printer.HttpStatusException = ex;
			printer.PingStatus = IPStatus.Unknown;
		}
		return false;
	}

    public static string GetPingStatus(IPStatus ipStatus) =>
        ipStatus switch
        {
            IPStatus.Success => LocaleCore.Ping.StatusSuccess,
            IPStatus.DestinationNetworkUnreachable => LocaleCore.Ping.StatusDestinationNetworkUnreachable,
            IPStatus.DestinationHostUnreachable => LocaleCore.Ping.StatusDestinationHostUnreachable,
            IPStatus.DestinationProtocolUnreachable => LocaleCore.Ping.StatusDestinationProtocolUnreachable,
            IPStatus.DestinationPortUnreachable => LocaleCore.Ping.StatusDestinationPortUnreachable,
            IPStatus.NoResources => LocaleCore.Ping.StatusNoResources,
            IPStatus.BadOption => LocaleCore.Ping.StatusBadOption,
            IPStatus.HardwareError => LocaleCore.Ping.StatusHardwareError,
            IPStatus.PacketTooBig => LocaleCore.Ping.StatusPacketTooBig,
            IPStatus.TimedOut => LocaleCore.Ping.StatusTimedOut,
            IPStatus.BadRoute => LocaleCore.Ping.StatusBadRoute,
            IPStatus.TtlExpired => LocaleCore.Ping.StatusTtlExpired,
            IPStatus.TtlReassemblyTimeExceeded => LocaleCore.Ping.StatusTtlReassemblyTimeExceeded,
            IPStatus.ParameterProblem => LocaleCore.Ping.StatusParameterProblem,
            IPStatus.SourceQuench => LocaleCore.Ping.StatusSourceQuench,
            IPStatus.BadDestination => LocaleCore.Ping.StatusBadDestination,
            IPStatus.DestinationUnreachable => LocaleCore.Ping.StatusDestinationUnreachable,
            IPStatus.TimeExceeded => LocaleCore.Ping.StatusTimeExceeded,
            IPStatus.BadHeader => LocaleCore.Ping.StatusBadHeader,
            IPStatus.UnrecognizedNextHeader => LocaleCore.Ping.StatusUnrecognizedNextHeader,
            IPStatus.IcmpError => LocaleCore.Ping.StatusIcmpError,
            IPStatus.DestinationScopeMismatch => LocaleCore.Ping.StatusDestinationScopeMismatch,
            IPStatus.Unknown => LocaleCore.Ping.StatusUnknown,
            _ => LocaleCore.Ping.StatusUnknown
        };

    #endregion
}