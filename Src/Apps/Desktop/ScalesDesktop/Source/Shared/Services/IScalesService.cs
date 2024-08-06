namespace ScalesDesktop.Source.Shared.Services;

public interface IScalesService
{
    public void Setup(string comPort);

    public void Connect();

    public void Disconnect();

    public void Calibrate();

    public void StopPolling();

    public void StartPolling();

}