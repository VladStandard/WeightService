using Ws.Scales.Enums;

namespace Ws.Scales.Events;

public class GetScaleStatusEvent(ScalesStatus status)
{
    public ScalesStatus Status { get; } = status;
}