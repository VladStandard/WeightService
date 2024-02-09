using System.IO.Ports;
using Ws.Scales.Utils;

namespace Ws.Scales.Common;

public class ScaleCommandBase(SerialPort port, byte[] command)
{
    protected readonly SerialPort Port = port;

    public virtual void Activate()
    {
        Port.Write(command, 0, command.Length);
        Response();
    }

    protected virtual void Response() {}

    protected static bool ParseCrc(IEnumerable<Byte> crc1, byte[] bodyForCrc2)
    {
        byte[] crc2 = BitConverter.GetBytes(CrcUtil.Crc16Generate(bodyForCrc2));
        return crc1.SequenceEqual(crc2);
    }
}