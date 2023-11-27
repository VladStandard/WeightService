namespace Ws.Scales.Common;

public interface IScales : IDisposable
{
    void SendGetWeight();
    void Calibrate();
}