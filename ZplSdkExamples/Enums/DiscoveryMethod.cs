// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.ComponentModel;

namespace ZplSdkExamples.Enums
{
    public enum DiscoveryMethod
    {

        [Description("Local Broadcast")]
        LocalBroadcast,

        [Description("Directed Broadcast")]
        DirectedBroadcast,

        [Description("Multicast Broadcast")]
        MulticastBroadcast,

        [Description("Subnet Search")]
        SubnetSearch,

        [Description("Zebra USB Drivers")]
        ZebraUsbDrivers,

        [Description("USB Direct")]
        UsbDirect,

        [Description("Find Printers Near Me")]
        FindPrintersNearMe,

        [Description("Find all Bluetooth Devices")]
        Bluetooth
    }
}
