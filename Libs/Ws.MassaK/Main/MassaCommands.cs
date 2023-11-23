using Ws.MassaK.Utils;

namespace Ws.MassaK.Main;

public static class MassaCommands
{
    /// <summary>
    /// F8 55 CE 01 00 A1 00 00
    /// </summary>
    public static byte[] CmdGetTare => MassaCommandsUtil.Generate(0xA1);

    /// <summary>
    /// F8 55 CE 01 00 A0 A0 00
    /// </summary>
    public static byte[] CmdGetWeight => MassaCommandsUtil.Generate(0xA0);

    /// <summary>
    /// F8 55 CE 01 00 72 72 00
    /// </summary>
    public static byte[] CmdSetZero => MassaCommandsUtil.Generate(0x72);
}