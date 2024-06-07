using Ws.Desktop.Models.Common;
using Ws.Desktop.Models.Features.Arms.Output;

namespace Ws.Desktop.Api.App.Features.Arms.Common;

public interface IArmService
{
    public OutputDto<ArmValue>? GetByName(string armName);
}