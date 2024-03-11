namespace Ws.Printers.Features.Zebra.Utils;

internal static class ZebraCommandUtil
{
    public const string GetStatus = "! U1 getvar \"device.host_status\"\r\n";
}