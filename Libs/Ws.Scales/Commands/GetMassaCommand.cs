using System.IO.Ports;
using CommunityToolkit.Mvvm.Messaging;
using Ws.Scales.Common;
using Ws.Scales.Events;
using Ws.Scales.Utils;

namespace Ws.Scales.Commands;

internal class GetMassaCommand(SerialPort port) : ScaleCommandBase(port, MassaKCommands.CmdGetWeight.Value)
{
    protected override void Response()
    {
        byte[] buffer = new byte[20];
        Port.Read(buffer, 0, buffer.Length);

        if (buffer[5] != 0x24) return;

        int packetLen = BitConverter.ToUInt16(buffer.Skip(3).Take(2).ToArray(), 0);
        byte[] packet = buffer.Skip(5).Take(packetLen).ToArray();

        ushort getCrc = BitConverter.ToUInt16(buffer.Skip(18).Take(2).ToArray(), 0);
        ushort generatedCrc = CrcUtil.CalculateCrc16(packet);

        if (getCrc != generatedCrc) return;

        int weight = BitConverter.ToInt32(buffer.Skip(6).Take(4).ToArray(), 0);
        bool isStable = buffer[11] == 0x01;
        WeakReferenceMessenger.Default.Send(new GetScaleMassaEvent(weight, isStable));
    }
}