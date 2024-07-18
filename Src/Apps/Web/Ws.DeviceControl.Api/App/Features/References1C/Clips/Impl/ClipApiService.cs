using Ws.Database.EntityFramework.Entities.Ref1C.Clips;
using Ws.DeviceControl.Api.App.Features.References1C.Clips.Common;
using Ws.DeviceControl.Api.App.Features.References1C.Clips.Impl.Expressions;

namespace Ws.DeviceControl.Api.App.Features.References1C.Clips.Impl;

public class ClipApiService(WsDbContext dbContext) : IClipService
{
    #region Queries

    public async Task<PackageDto> GetByIdAsync(Guid id)
    {
        ClipEntity? clip = await dbContext.Clips.FindAsync(id);
        if (clip == null) throw new KeyNotFoundException();
        return ClipExpressions.ToDto.Compile().Invoke(clip);
    }

    public Task<List<PackageDto>> GetAllAsync()
    {
        return dbContext.Clips
            .AsNoTracking().Select(ClipExpressions.ToDto)
            .OrderBy(i => i.Weight).ThenBy(i => i.Name)
            .ToListAsync();
    }

    #endregion
}