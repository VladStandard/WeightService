namespace Ws.Scales.Events;

public class GetScaleMassaEvent
{
    public int Weight { get; init; }
    public bool IsStable { get; init; }
    
    public GetScaleMassaEvent(int weight, bool isStable)
    {
        Weight = weight;
        IsStable = isStable;
    }
}