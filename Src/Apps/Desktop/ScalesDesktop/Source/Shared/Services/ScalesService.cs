using MassaK.Plugin;
using MassaK.Plugin.Abstractions.Enums;
using MassaK.Plugin.Abstractions.Events;
using MassaK.Plugin.Impl;
using ScalesDesktop.Source.Shared.Services.Stores;
using IDispatcher = Fluxor.IDispatcher;

namespace ScalesDesktop.Source.Shared.Services;

public class ScalesService : IDisposable
{
    private readonly IDispatcher _dispatcher;
    private const string DefaultComPort = "COM6";
    private IMassaK Scales { get; set; } = new MassaUsb(DefaultComPort);


    public ScalesService(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
        Setup();
    }

    private void Setup(string comPort = DefaultComPort)
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
        _dispatcher.Dispatch(new ChangeScalesStatusAction(MassaKStatus.Ready));
    }

    public void Disconnect()
    {
        Scales.Disconnect();
        _dispatcher.Dispatch(new ChangeScalesStatusAction(MassaKStatus.Disabled));
    }

    public void Calibrate() => Scales.Calibrate();

    public void StopPolling() => Scales.StopWeightPolling();

    public void StartPolling() => Scales.StartWeightPolling();

    private void ScalesOnStatusChanged(object? sender, MassaKStatus e) =>
        _dispatcher.Dispatch(new ChangeScalesStatusAction(e));

    private void ScalesOnWeightChanged(object? sender, WeightEventArg e) =>
        _dispatcher.Dispatch(new ChangeWeightAction(e.Weight, e.IsStable));

    public void Dispose()
    {
        Scales.Dispose();
        Scales.OnStatusChanged -= ScalesOnStatusChanged;
        Scales.OnWeightChanged -= ScalesOnWeightChanged;
        GC.SuppressFinalize(this);
    }
}