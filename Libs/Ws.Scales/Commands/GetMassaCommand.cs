using System.IO.Ports;
using CommunityToolkit.Mvvm.Messaging;
using Ws.Scales.Common;
using Ws.Scales.Events;
using Ws.Scales.Utils;

namespace Ws.Scales.Commands;

public class GetMassaCommand(SerialPort port) : ScaleCommandBase(port, MassaKCommands.CmdGetWeight)
{
    protected override void Response()
    { 
        byte[] buffer = new byte[16];
        Port.Read(buffer, 0, buffer.Length);
        
        ushort lenAsUshort = BitConverter.ToUInt16(buffer.Skip(3).Take(2).ToArray(), 0);
        byte[] crc1 = buffer.Skip(5 + lenAsUshort).Take(2).ToArray();
        byte[] bodyForCrc2 = buffer.Skip(5).Take(lenAsUshort).ToArray();
        
        if (!ParseCrc(crc1, bodyForCrc2)) return;
        
        int weight = BitConverter.ToInt32(buffer.Skip(6).Take(4).ToArray(), 0);
        bool isStable = buffer[11] == 0x01;
        
        WeakReferenceMessenger.Default.Send(new GetScaleMassaEvent(weight, isStable));
    }
}