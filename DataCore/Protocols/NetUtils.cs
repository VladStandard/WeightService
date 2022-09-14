// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using RestSharp;
using System.Net.Sockets;

namespace DataCore.Protocols
{
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

        public static string GetLocalHostName(bool isThrow)
        {
            try
            {
                return Dns.GetHostName();
            }
            catch (Exception ex)
            {
                if (isThrow)
                    throw new($"Exception in {nameof(GetLocalHostName)}", ex);
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
				RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
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

        public static bool RequestPing(PrinterModel printer, int timeOut)
        {
            if (printer == null)
                return false;
            try
            {
                using Ping ping = new();
                PingReply pingReply = ping.Send(printer.Ip, timeOut);
                return (printer.PingStatus = pingReply.Status) == IPStatus.Success;
            }
            catch (Exception ex)
            {
                printer.HttpStatusException = ex;
                printer.PingStatus = IPStatus.Unknown;
            }
            return false;
        }

        #endregion
    }
}