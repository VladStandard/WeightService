using Ws.Scales.Utils;

namespace Ws.Scales.Main;

public static class MassaKCommands
{
    public static byte[] CmdGetWeight => ScalesCommandsUtil.Generate(0xA0);
    public static byte[] CmdSetZero => ScalesCommandsUtil.Generate(0x72);
}