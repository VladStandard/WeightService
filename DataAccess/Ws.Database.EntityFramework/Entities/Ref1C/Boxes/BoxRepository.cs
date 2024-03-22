namespace Ws.Database.EntityFramework.Entities.Ref1C.Boxes;

public class BoxRepository(DbContext context)
{
    public async Task<BoxEntity> GetByIdAsync(Guid uid, CancellationToken cancelToken = default) =>
        await context.Set<BoxEntity>().FindAsync([uid], cancelToken) ?? new();
}