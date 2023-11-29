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
    private ScalesStatus Status { get; set; }
    
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
        SetStatus(ScalesStatus.IsDisabled);
        WeakReferenceMessenger.Default.Register<ScalesForceDisconnected>(this, ForceReconnect);
    }

    #region Public

    public void Calibrate()
    {
        ExecuteCommand(new CalibrateMassaCommand(Port));
    }
    
    public void SendGetWeight()
    {
        ExecuteCommand(new GetMassaCommand(Port));
    }
    
    public void Connect()
    {
        Disconnect();
        SetStatus(ScalesStatus.IsForceDisconnected);
        WeakReferenceMessenger.Default.Send(new ScalesForceDisconnected());
    }
    
    public void Disconnect()
    {
        SetStatus(ScalesStatus.IsDisabled);
        if (Port.IsOpen) Port.Close();
        Port.Dispose();
    }
    
    #endregion

    #region Private

    private void SetStatus(ScalesStatus status)
    {
        Status = status;
        WeakReferenceMessenger.Default.Send(new GetScaleStatusEvent(Status));
    }
    
    private void ExecuteCommand(ScaleCommandBase command)
    {
        if (Status == ScalesStatus.IsDisabled) return;
        if (Status != ScalesStatus.IsConnect)
        {
            WeakReferenceMessenger.Default.Send(new ScalesForceDisconnected());
            return;
        }
        command.Activate();
    }
    
    private void ForceReconnect(Object recipient, ScalesForceDisconnected message)
    {
        if (Status == ScalesStatus.IsDisabled)
            return;
        try
        {
            if (Port.IsOpen) Port.Close();
            Port.Open();
            SetStatus(ScalesStatus.IsConnect);
        }
        catch (Exception _)
        {
            SetStatus(ScalesStatus.IsForceDisconnected);
        }
    }

    #endregion
    
    public void Dispose()
    {
        Disconnect();
        WeakReferenceMessenger.Default.Unregister<GetScaleMassaEvent>(this);
        WeakReferenceMessenger.Default.Unregister<ScalesForceDisconnected>(this);
    }
}