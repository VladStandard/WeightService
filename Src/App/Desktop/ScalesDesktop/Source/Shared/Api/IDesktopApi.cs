using Ws.Desktop.Models.Features.Arms.Output;
using Ws.Desktop.Models.Features.Plus.Output;

namespace ScalesDesktop.Source.Shared.Api;

public interface IDesktopApi
{
    Task<ArmValue> GetArmByName(string armName);
    Task<PluWeight[]> GetPlusByArm(Guid armUid);
}