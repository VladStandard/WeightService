namespace Ws.Scales.Utils;

public static class MassaKCommands
{
    public static byte[] CmdGetWeight => CrcUtil.Generate(0xA0);
    public static byte[] CmdSetZero => CrcUtil.Generate(0x72);
}