// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WeightCore.Print.Native;
using WeightCore.Zpl;

namespace WeightCore.Print.Zebra
{
    public class DeviceSocketRaw : IDeviceSocket
    {
        private string PrinterName { get; set; }

        public DeviceSocketRaw(string _PrinterName)
        {
            PrinterName = _PrinterName;
        }

        public override string SendStringToPrinter(string szString)
        {
            string zpl = ZplUtils.ToCodePoints(szString);
            RawPrinterHelper.SendStringToPrinter(PrinterName, zpl);
            return "";
        }
    }
}
