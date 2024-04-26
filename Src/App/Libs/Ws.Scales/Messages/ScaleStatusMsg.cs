using Ws.Scales.Enums;

namespace Ws.Scales.Messages;

public class ScaleStatusMsg(ScalesStatus status)
{
    public ScalesStatus Status { get; } = status;
}