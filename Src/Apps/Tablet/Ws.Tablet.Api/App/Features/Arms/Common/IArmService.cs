using Ws.Tablet.Models.Features.Arms;

namespace Ws.Tablet.Api.App.Features.Arms.Common;

public interface IArmService
{
    #region Queries

    ArmDto GetCurrent();

    #endregion
}