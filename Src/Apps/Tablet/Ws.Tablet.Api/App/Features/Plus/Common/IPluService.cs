using Ws.Tablet.Models.Features.Plus;

namespace Ws.Tablet.Api.App.Features.Plus.Common;

public interface IPluService
{
    #region Queries

    PluDto GetByNumber(uint number);

    #endregion
}