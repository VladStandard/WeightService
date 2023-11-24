namespace Ws.Scales.Common;

public interface IScalesCommands
{
    byte[] CmdGetWeight { get; }
    byte[] CmdSetZero { get; }
}