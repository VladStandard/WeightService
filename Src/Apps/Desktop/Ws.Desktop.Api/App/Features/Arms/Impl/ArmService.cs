using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework;
using Ws.Desktop.Api.App.Features.Arms.Common;
using Ws.Desktop.Api.App.Features.Arms.Extensions;
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
}