using Ws.Scales.Utils;

namespace Ws.Scales.Main;

public static class ScalesCommands
{
    /// <summary>
    /// F8 55 CE 01 00 A1 00 00
    /// </summary>
    public static byte[] CmdGetTare => ScalesCommandsUtil.Generate(0xA1);

    /// <summary>
    /// F8 55 CE 01 00 A0 A0 00
    /// </summary>
    public static byte[] CmdGetWeight => ScalesCommandsUtil.Generate(0xA0);

    /// <summary>
    /// F8 55 CE 01 00 72 72 00
    /// </summary>
    public static byte[] CmdSetZero => ScalesCommandsUtil.Generate(0x72);
}