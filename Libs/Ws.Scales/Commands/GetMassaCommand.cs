using System.IO.Ports;
using CommunityToolkit.Mvvm.Messaging;
using Ws.Scales.Common;
using Ws.Scales.Events;
using Ws.Scales.Main;

namespace Ws.Scales.Commands;

public class GetMassaCommand : ScaleCommandBase, IScaleCommand
{
    private static readonly byte[] Command = MassaKCommands.CmdGetWeight;
    
    public GetMassaCommand(SerialPort port) : base(port)
    {
    }
    
    public void Request()
    { 
        Send(Command);
                
        byte[] buffer = new byte[16];
        Port.Read(buffer, 0, buffer.Length);
        
        int weight = BitConverter.ToInt32(buffer.Skip(6).Take(4).ToArray(), 0);
        bool isStable = buffer[11] == 0x01;
        WeakReferenceMessenger.Default.Send(new GetScaleMassaEvent(weight, isStable));
    }
}