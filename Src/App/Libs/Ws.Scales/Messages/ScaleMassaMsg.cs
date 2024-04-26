namespace Ws.Scales.Messages;

public class ScaleMassaMsg(int weight, bool isStable)
{
    public int Weight { get; } = weight;
    public bool IsStable { get; } = isStable;
}