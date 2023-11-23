using Ws.Scales.Enums;

namespace Ws.Scales.Events;

public class GetScaleStatusEvent
{
    public ScalesStatus Status { get; init; }

    public GetScaleStatusEvent(ScalesStatus status)
    {
        Status = status;
    }
}