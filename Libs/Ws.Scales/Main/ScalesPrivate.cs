using System.IO.Ports;
using CommunityToolkit.Mvvm.Messaging;
using Ws.Scales.Common;
using Ws.Scales.Enums;
using Ws.Scales.Events;
using Ws.Shared.Utils;

namespace Ws.Scales.Main;

public partial class Scales
{
    private static SerialPort GenerateSerialPort(string comPort)
    {
        return new()
        {
            PortName = comPort.ToUpper(),
            ReadTimeout = 0_100,
            WriteTimeout = 0_100,
            BaudRate = 19200,
            Parity = Parity.Even,
            StopBits = StopBits.One,
            DataBits = 8,
            Handshake = Handshake.RequestToSend
        };
    }

    private void SetStatus(ScalesStatus status)
    {
        Status = status;
        WeakReferenceMessenger.Default.Send(new GetScaleStatusEvent(Status));
    }

    private void ExecuteCommand(ScaleCommandBase command)
    {
        try
        {
            ErrorUtil.Suppress<TimeoutException>(() => {
                switch (Status)
                {
                    case ScalesStatus.IsConnect:
                        command.Activate();
                        return;
                    case ScalesStatus.IsDisabled:
                        break;
                    case ScalesStatus.IsForceDisconnected:
                        Connect();
                        break;
                    default:
                        Connect();
                        break;
                }
            });
        }
        catch (Exception)
        {
            Connect();
        }
    }
}