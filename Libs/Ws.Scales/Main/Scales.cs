using System.IO.Ports;
using CommunityToolkit.Mvvm.Messaging;
using Ws.Scales.Commands;
using Ws.Scales.Common;
using Ws.Scales.Enums;
using Ws.Scales.Events;

namespace Ws.Scales.Main;

public class Scales : IScales
{
    private SerialPort Port { get; set; }
    
    public Scales(string comPort)
    {
        Port = new()
        {
            PortName = comPort.ToUpper(),
            ReadTimeout = 0_100,
            WriteTimeout = 0_100,
            BaudRate = 19200,
            Parity = Parity.Even,
            StopBits = StopBits.One,
            DataBits = 8,
            Handshake = Handshake.RequestToSend,
        };
        WeakReferenceMessenger.Default.Register<ScalesForceDisconnected>(this, ForceReconnect);
    }
    
    private void ForceReconnect(Object recipient, ScalesForceDisconnected message)
    {
        try
        {
            Disconnect();
            Port.Open();
            WeakReferenceMessenger.Default.Send(new GetScaleStatusEvent(ScalesStatus.IsConnect));
        }
        catch (Exception _)
        {
            WeakReferenceMessenger.Default.Send(new GetScaleStatusEvent(ScalesStatus.IsDisconnected));
        }
    }

    public void Connect()
    {
        Disconnect();
        WeakReferenceMessenger.Default.Send(new ScalesForceDisconnected());
    }
    
    public void Disconnect()
    {
        if (Port.IsOpen) Port.Close();
        Port.Dispose();
    }
    
    public void SendGetWeight()
    { 
        new GetMassaCommand(Port).Activate();
    }
    
    public void Calibrate()
    {
        new CalibrateMassaCommand(Port).Activate();
    }
    
    public void Dispose()
    {
        Disconnect();
        WeakReferenceMessenger.Default.Unregister<GetScaleMassaEvent>(this);
    }
}