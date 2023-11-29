using System.IO.Ports;
using CommunityToolkit.Mvvm.Messaging;
using Ws.Scales.Common;
using Ws.Scales.Events;
using Ws.Scales.Utils;

namespace Ws.Scales.Commands;

public class GetMassaCommand : ScaleCommandBase
{
    private static readonly byte[] Command = MassaKCommands.CmdGetWeight;
    
    public GetMassaCommand(SerialPort port) : base(port)
    {
    }

    public override void Activate()
    {
        Request(Command);
    }
    
    protected override void Response()
    { 
        byte[] buffer = new byte[16];
        Port.Read(buffer, 0, buffer.Length);
        
        ushort lenAsUshort = BitConverter.ToUInt16(buffer.Skip(3).Take(2).ToArray(), 0);
        byte[] body = buffer.Skip(5).Take(lenAsUshort).ToArray();
        byte[] crc = buffer.Skip(5 + lenAsUshort).Take(2).ToArray();
        byte[] crcNew = BitConverter.GetBytes(ScalesCommandsUtil.Crc16Generate(body));
        
        bool isValidCrc = crc.Length > 0 && crc[0] == crcNew[0] && crc[1] == crcNew[1];
        if (isValidCrc == false) return;
        
        int weight = BitConverter.ToInt32(buffer.Skip(6).Take(4).ToArray(), 0);
        bool isStable = buffer[11] == 0x01;
        WeakReferenceMessenger.Default.Send(new GetScaleMassaEvent(weight, isStable));
    }
}