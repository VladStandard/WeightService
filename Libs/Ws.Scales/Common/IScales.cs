namespace Ws.Scales.Common;

public interface IScales : IDisposable
{
    void Connect();
    void Disconnect();
    void SendGetWeight();
    void Calibrate();
}