// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WeightCore.Zpl;

namespace WeightCore.Print.Zebra
{
    public class DeviceSocketTcp : IDeviceSocket
    {
        public string DeviceIP { get; private set; }
        public int DevicePort { get; private set; }

        public DeviceSocketTcp(string _DeviceIP, int _DevicePort)
        {
            DeviceIP = _DeviceIP;
            DevicePort = _DevicePort;
        }

        public override string SendStringToPrinter(string szString)
        {
            string info = ZplPipeUtils.InterplayToPrinter(DeviceIP, DevicePort, szString.Split('\n'), out string _errorMessage);
            if (_errorMessage.Length > 0)
            {
                throw new System.Exception(_errorMessage);
            }
            return info;
        }
    }
}
