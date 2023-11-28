using System.IO.Ports;
using Ws.Scales.Commands;
using Ws.Scales.Common;

namespace Ws.Scales.Main;

public class Scales : IScales
{
    private SerialPort Port { get; set; }
    
    public Scales(string comPort)
    {
        Port = new(comPort);
        Port.Open();
        
        Port.ReadTimeout = 0_100;
        Port.WriteTimeout = 0_100;
        Port.BaudRate = 4_800;
        Port.Parity = Parity.Even;
        Port.StopBits = StopBits.One;
        Port.DataBits = 8;
        Port.Handshake = Handshake.RequestToSend;
    }
    
    public void SendGetWeight()
    { 
        new GetMassaCommand(Port).Request();
    }
    
    public void Calibrate()
    {
        new CalibrateMassaCommand(Port).Request();
    }
    
    public void Dispose()
    {
        if (Port.IsOpen) Port.Close();
        Port.Dispose();
    }
}