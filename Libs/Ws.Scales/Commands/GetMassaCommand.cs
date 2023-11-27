using System.IO.Ports;
using CommunityToolkit.Mvvm.Messaging;
using Ws.Scales.Common;
using Ws.Scales.Events;
using Ws.Scales.Main;

namespace Ws.Scales.Commands;

public class GetMassaCommand : ScaleCommandBase, IScaleCommand
{
    public GetMassaCommand(SerialPort port) : base(port)
    {
    }
    
    public void Request()
    { 
        Send(MassaKCommands.CmdGetWeight);
    }
    
    public void Response(byte[] bytes)
    {
        if (bytes.Length != 14) return;
        int weight = BitConverter.ToInt32(bytes.Skip(6).Take(4).ToArray(), 0);
        bool isStable = bytes[11] == 0x01;
        WeakReferenceMessenger.Default.Send(new GetScaleMassaEvent(weight, isStable));
    }
}