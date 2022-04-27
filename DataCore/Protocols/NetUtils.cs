// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Net.NetworkInformation;
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
                throw new Exception($"Exception in {nameof(GetLocalIpAddress)}", ex);
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
                    throw new Exception($"Exception in {nameof(GetLocalHostName)}", ex);
            }
            return string.Empty;
        }

        public static string GetMacAddress()
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

        #endregion
    }
}