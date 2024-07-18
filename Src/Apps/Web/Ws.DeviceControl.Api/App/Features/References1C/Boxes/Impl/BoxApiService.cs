using Ws.Database.EntityFramework.Entities.Ref1C.Boxes;
using Ws.DeviceControl.Api.App.Features.References1C.Boxes.Common;
using Ws.DeviceControl.Api.App.Features.References1C.Boxes.Impl.Expressions;

namespace Ws.DeviceControl.Api.App.Features.References1C.Boxes.Impl;

public class BoxApiService(WsDbContext dbContext) : IBoxService
{
    #region Queries

    public async Task<PackageDto> GetByIdAsync(Guid id)
    {
        BoxEntity? box = await dbContext.Boxes.FindAsync(id);
        if (box == null) throw new KeyNotFoundException();
        return BoxExpressions.ToDto.Compile().Invoke(box);
    }

    public Task<List<PackageDto>> GetAllAsync()
    {
        return dbContext.Boxes
            .AsNoTracking().Select(BoxExpressions.ToDto)
            .OrderBy(i => i.Weight).ThenBy(i => i.Name)
            .ToListAsync();
    }

    #endregion
}