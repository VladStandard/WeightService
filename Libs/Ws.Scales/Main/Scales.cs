using System.IO.Ports;
using Ws.Scales.Commands;
using Ws.Scales.Common;

namespace Ws.Scales.Main;

public class Scales : IScales
{ 
    private IScaleCommand? ActiveCommand { get; set; }
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

        Port.DataReceived += DataReceived;

        SubscribeToData();
    }
    
    private void SubscribeToData()
    {
        Port.DataReceived += DataReceived; 
    }
    
    private void UnSubscribeFromData()
    {
        Port.DataReceived -= DataReceived; 
    }

    private void DataReceived(Object sender, SerialDataReceivedEventArgs serialDataReceivedEventArgs)
    {   
        int bytesToRead = Port.BytesToRead;
        byte[] buffer = new byte[bytesToRead];
        Port.Read(buffer, 0, bytesToRead);
        
        ActiveCommand?.Response(buffer);
    }
    
    public void SendGetWeight()
    {
        ActiveCommand = new GetMassaCommand(Port);
        ActiveCommand.Request();
    }
    
    public void Calibrate()
    {
        ActiveCommand = new CalibrateMassaCommand(Port);
        ActiveCommand.Request();
        ActiveCommand = null;
    }
    public void Dispose()
    {
        UnSubscribeFromData();
        if (Port.IsOpen) Port.Close();
        Port.Dispose();
    }
}