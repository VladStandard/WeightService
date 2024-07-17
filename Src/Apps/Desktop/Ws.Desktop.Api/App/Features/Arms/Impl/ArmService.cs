using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework;
using Ws.Database.EntityFramework.Entities.Ref.Lines;
using Ws.Desktop.Api.App.Features.Arms.Common;
using Ws.Desktop.Api.App.Features.Arms.Extensions;
using Ws.Desktop.Models.Features.Arms.Input;
using Ws.Desktop.Models.Features.Arms.Output;

namespace Ws.Desktop.Api.App.Features.Arms.Impl;

public class ArmService(WsDbContext dbContext) : IArmService
{
    #region Queries

    public ArmValue? GetByPcName(string armName) =>
        dbContext.Lines
            .AsNoTracking()
            .Where(i => i.PcName == armName)
            .ToArmValue()
            .FirstOrDefault();

    #endregion

    #region Commands

    public bool Update(Guid armId, UpdateArmDto dto)
    {
        LineEntity? arm = dbContext.Lines.Find(armId);
        if (arm == null)
            return false;

        arm.Version = dto.Version;
        dbContext.SaveChanges();
        return true;
    }

    #endregion
}