using System.IO.Ports;
using CommunityToolkit.Mvvm.Messaging;
using Ws.Scales.Commands;
using Ws.Scales.Common;
using Ws.Scales.Enums;
using Ws.Scales.Events;
using Ws.Scales.Utils;

namespace Ws.Scales.Main;

public partial class Scales : IScales
{
    private SerialPort Port { get; set; }
    private ScalesStatus Status { get; set; }
    
    public Scales(string comPort)
    {
        Port = GenerateSerialPort(comPort);
        SetStatus(ScalesStatus.IsDisabled);
    }
    
    public void Calibrate() => ExecuteCommand(new(Port, MassaKCommands.CmdSetZero));
    public void SendGetWeight() => ExecuteCommand(new GetMassaCommand(Port));

    public void Connect()
    {
        try
        {
            Disconnect();
            Port.Open();
            SetStatus(ScalesStatus.IsConnect);
        }
        catch (Exception)
        {
            SetStatus(ScalesStatus.IsForceDisconnected);
        }
    }
    
    public void Disconnect()
    {
        SetStatus(ScalesStatus.IsDisabled);
        if (Port.IsOpen) Port.Close();
        Port.Dispose();
        WeakReferenceMessenger.Default.Unregister<GetScaleMassaEvent>(this);
    }
    
    public void Dispose() => Disconnect();
}