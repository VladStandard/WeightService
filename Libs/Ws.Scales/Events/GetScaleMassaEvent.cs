namespace Ws.Scales.Events;

public class GetScaleMassaEvent(int weight, bool isStable)
{
    public int Weight { get; } = weight;
    public bool IsStable { get; } = isStable;
}