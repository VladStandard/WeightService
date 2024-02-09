namespace Ws.Printers.Features.Zebra.Utils;

public static class ZebraCommandUtil
{
    public const string GetStatus = "! U1 getvar \"device.host_status\"\r\n";
}