// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WeightCore.Zpl;

namespace WeightCore.Print.Zebra
{
    public class DeviceSocketTcp : IDeviceSocket
    {
        private string DeviceIP { get; }
        private int DevicePort { get; }

        public DeviceSocketTcp(string deviceIp, int devicePort)
        {
            DeviceIP = deviceIp;
            DevicePort = devicePort;
        }

        public override string SendStringToPrinter(string szString)
        {
            string info = ZplUtils.InterplayToPrinter(DeviceIP, DevicePort, szString.Split('\n'), out string _errorMessage);
            if (_errorMessage.Length > 0)
            {
                throw new System.Exception(_errorMessage);
            }
            return info;
        }
    }
}
