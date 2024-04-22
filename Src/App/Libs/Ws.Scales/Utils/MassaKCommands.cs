namespace Ws.Scales.Utils;

internal static class MassaKCommands
{
    public static readonly Lazy<byte[]> CmdGetWeight = new(() => CrcUtil.Generate(0x23));
    public static readonly Lazy<byte[]> CmdSetZero = new(() => CrcUtil.Generate(0x72));
}