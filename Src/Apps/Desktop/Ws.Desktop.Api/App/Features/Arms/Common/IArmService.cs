using Ws.Desktop.Models.Features.Arms.Output;

namespace Ws.Desktop.Api.App.Features.Arms.Common;

public interface IArmService
{
    public ArmValue? GetByPcName(string armName);
}