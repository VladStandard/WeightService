using System.IO.Ports;
using CommunityToolkit.Mvvm.Messaging;
using Ws.Scales.Commands;
using Ws.Scales.Common;
using Ws.Scales.Enums;
using Ws.Scales.Events;

namespace Ws.Scales.Main;

public partial class Scales : IScales
{
    private SerialPort Port { get; }
    private ScalesStatus Status { get; set; }

    public Scales(string comPort)
    {
        Port = GenerateSerialPort(comPort);
        SetStatus(ScalesStatus.IsDisabled);
    }

    public void Calibrate() => ExecuteCommand(new CalibrateCommand(Port));
    
    public void StartWeightPolling()
    {
        lock (_pollingLock)
        {
            if (!_cancelPollingToken.IsCancellationRequested)
                return;
            
            _cancelPollingToken = new();
            CancellationToken cancellationToken = _cancelPollingToken.Token;

            
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    ExecuteCommand(new GetMassaCommand(Port));
                    await Task.Delay(200, cancellationToken);
                }
            }, cancellationToken);
        }
    }
    
    public void StopWeightPolling()
    {
        lock (_pollingLock)
        {
            if (_cancelPollingToken.IsCancellationRequested)
                return;
            _cancelPollingToken.Cancel();
        }
    }
    
    public void Connect()
    {
        try
        {
            StopWeightPolling();
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
        StopWeightPolling();
        
        if (Port.IsOpen) Port.Close();
        Port.Dispose();

        WeakReferenceMessenger.Default.Unregister<GetScaleMassaEvent>(this);
    }

    public void Dispose() => Disconnect();
}