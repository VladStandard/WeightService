// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WeightCore.Print.Zebra
{
    public abstract class IDeviceSocket
    {
        public abstract string SendStringToPrinter(string szString);
    }
}
