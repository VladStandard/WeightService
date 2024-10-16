using Ws.Tablet.Api.App.Features.Arms.Common;
using Ws.Tablet.Api.App.Shared.Helpers;
using Ws.Tablet.Models.Features.Arms;

namespace Ws.Tablet.Api.App.Features.Arms.Impl;

internal sealed class ArmApiService(UserHelper userHelper) : IArmService
{
    #region Queries

    public ArmDto GetCurrent()
    {
        return new()
        {
            Id = userHelper.UserId,
            Name = "Тестовая линия",
            WarehouseName = "Тестовый склад"
        };
    }

    #endregion

}