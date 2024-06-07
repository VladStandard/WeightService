using MassaK.Plugin;
using MassaK.Plugin.Abstractions.Enums;
using MassaK.Plugin.Abstractions.Events;
using MassaK.Plugin.Impl;

namespace ScalesDesktop.Source.Shared.Services;

public class ScalesService(IDispatcher dispatcher) : IDisposable
{
    private IMassaK Scales { get; set; } = new MassaUsb(DefaultComPort);
    public MassaKStatus Status { get; private set; } = MassaKStatus.Disabled;
    public bool IsStable { get; private set; }
    public int CurrentWeight { get; private set; }

    public event Action? StatusChanged;
    public event Action? WeightChanged;

    private const string DefaultComPort = "COM6";

    public void Setup(string comPort = DefaultComPort)
    {
        Scales.OnStatusChanged -= ScalesOnStatusChanged;
        Scales.OnWeightChanged -= ScalesOnWeightChanged;
        Scales = new MassaUsb(comPort);
        Scales.OnStatusChanged += ScalesOnStatusChanged;
        Scales.OnWeightChanged += ScalesOnWeightChanged;
    }

    public void Connect()
    {
        Scales.Connect();
        Status = MassaKStatus.Ready;
        StatusChanged?.Invoke();
    }

    public void Disconnect()
    {
        Scales.Disconnect();
        Status = MassaKStatus.Disabled;
        StatusChanged?.Invoke();
    }

    public void Calibrate() => Scales.Calibrate();

    public void StopPolling() => Scales.StopWeightPolling();

    public void StartPolling() => Scales.StartWeightPolling();

    private async void ScalesOnStatusChanged(object? sender, MassaKStatus e) =>
        await dispatcher.DispatchAsync(() =>
        {
            if (Status.Equals(e)) return;
            Status = e;
            StatusChanged?.Invoke();
        });

    private async void ScalesOnWeightChanged(object? sender, WeightEventArg e) =>
        await dispatcher.DispatchAsync(() =>
        {
            IsStable = e.IsStable;
            CurrentWeight = e.Weight;
            WeightChanged?.Invoke();
        });

    public void Dispose()
    {
        Scales.Dispose();
        Scales.OnStatusChanged -= ScalesOnStatusChanged;
        Scales.OnWeightChanged -= ScalesOnWeightChanged;
        GC.SuppressFinalize(this);
    }
}