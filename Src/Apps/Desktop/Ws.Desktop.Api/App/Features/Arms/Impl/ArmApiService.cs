using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework;
using Ws.Database.EntityFramework.Entities.Ref.Lines;
using Ws.Desktop.Api.App.Features.Arms.Common;
using Ws.Desktop.Api.App.Features.Arms.Expressions;
using Ws.Desktop.Models.Features.Arms.Input;
using Ws.Desktop.Models.Features.Arms.Output;

namespace Ws.Desktop.Api.App.Features.Arms.Impl;

internal sealed class ArmApiService(WsDbContext dbContext, UserHelper userHelper) : IArmService
{
    #region Queries

    public ArmValue? Get() =>
        dbContext.Lines
            .AsNoTracking()
            .Where(i => i.Id == userHelper.UserId)
            .Select(ArmExpressions.ToDto)
            .FirstOrDefault();

    #endregion

    #region Commands

    public bool Update(UpdateArmDto dto)
    {
        LineEntity? arm = dbContext.Lines.Find(userHelper.UserId);
        if (arm == null)
            return false;

        arm.Version = dto.Version;
        dbContext.SaveChanges();
        return true;
    }

    #endregion
}