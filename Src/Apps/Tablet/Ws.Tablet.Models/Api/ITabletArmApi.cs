using Ws.Tablet.Models.Features.Arms;

namespace Ws.Tablet.Models.Api;

public interface ITabletArmApi
{
    [Get("/arms")]
    Task<ArmDto> GetCurrentArm();
}