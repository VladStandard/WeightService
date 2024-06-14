using Ws.Desktop.Models.Features.Arms.Output;

namespace Ws.Desktop.Api.App.Features.Arms.Common;

public interface IArmService
{
    #region Queries

    public ArmValue? GetByPcName(string armName);

    #endregion
}