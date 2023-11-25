using Ws.Scales.Common;
using Ws.Scales.Utils;

namespace Ws.Scales.Main;

public class MassaKCommands : IScalesCommands
{
    public byte[] CmdGetWeight => ScalesCommandsUtil.Generate(0xA0);
    public byte[] CmdSetZero => ScalesCommandsUtil.Generate(0x72);
}