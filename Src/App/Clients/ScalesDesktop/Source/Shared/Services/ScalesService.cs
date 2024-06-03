using CommunityToolkit.Mvvm.Messaging;
using Ws.Scales.Common;
using Ws.Scales.Enums;
using Ws.Scales.Main;
using Ws.Scales.Messages;

namespace ScalesDesktop.Source.Shared.Services;

public class ScalesService: IRecipient<ScaleStatusMsg>, IRecipient<ScaleMassaMsg>, IDisposable
{
    private IScales Scales { get; set; } = new Scales(DefaultComPort);
    public ScalesStatus Status { get; private set; } = ScalesStatus.IsDisabled;
    public bool IsStable { get; private set; }
    public int CurrentWeight { get; private set; }

    public event Action? OnStatusChanged;
    public event Action? OnStableChanged;

    private const string DefaultComPort = "COM6";
    private readonly IDispatcher _dispatcher;

    public ScalesService(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
        WeakReferenceMessenger.Default.Register<ScaleMassaMsg>(this);
        WeakReferenceMessenger.Default.Register<ScaleStatusMsg>(this);
    }

    public void Setup(string comPort = DefaultComPort)
    {
        Scales.Dispose();
        Scales = new Scales(comPort);
    }

    public void Connect()
    {
        Scales.Connect();
        Status = ScalesStatus.IsConnect;
        OnStatusChanged?.Invoke();
    }

    public void Disconnect()
    {
        Scales.Disconnect();
        Status = ScalesStatus.IsForceDisconnected;
        OnStatusChanged?.Invoke();
    }

    public void Calibrate() => Scales.Calibrate();

    public void StopPolling() => Scales.StopWeightPolling();

    public void StartPolling() => Scales.StartWeightPolling();

    public async void Receive(ScaleStatusMsg message)
    {
        await _dispatcher.DispatchAsync(() =>
        {
            if (Status.Equals(message.Status)) return;
            Status = message.Status;
            OnStatusChanged?.Invoke();
        });
    }

    public async void Receive(ScaleMassaMsg message)
    {
        await _dispatcher.DispatchAsync(() =>
        {
            IsStable = message.IsStable;
            CurrentWeight = message.Weight;
            OnStableChanged?.Invoke();
        });
    }

    public void Dispose()
    {
        Scales.Dispose();
        WeakReferenceMessenger.Default.Unregister<ScaleMassaMsg>(this);
        WeakReferenceMessenger.Default.Unregister<ScaleStatusMsg>(this);
        GC.SuppressFinalize(this);
    }
}