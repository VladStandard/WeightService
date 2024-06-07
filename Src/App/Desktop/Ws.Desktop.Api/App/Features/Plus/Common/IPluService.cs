using Ws.Desktop.Models.Common;
using Ws.Desktop.Models.Features.Plus.Output;

namespace Ws.Desktop.Api.App.Features.Plus.Common;

public interface IPluService
{
    public OutputDto<List<PluWeight>> GetAllWeightByArm(Guid uid);
}