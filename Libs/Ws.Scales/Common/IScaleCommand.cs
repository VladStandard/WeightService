namespace Ws.Scales.Common;

public interface IScaleCommand
{
    void Request();
    void Response(byte[] bytes);
}