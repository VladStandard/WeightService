using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Core.Entities.Scales.TemplatesResources;

public class SqlTemplateResourceRepository :  BaseRepository, IGetItemByUid<TemplateResourceEntity>, IGetAll<TemplateResourceEntity>
{
    public TemplateResourceEntity GetByUid(Guid uid) =>
        SqlCoreHelper.GetItemById<TemplateResourceEntity>(uid);
    
    public IEnumerable<TemplateResourceEntity> GetAll()
    {
        return SqlCoreHelper.GetEnumerable(
            QueryOver.Of<TemplateResourceEntity>().OrderBy(i => i.Name).Asc
        );
    }
    
    public TemplateResourceEntity GetByName(string name)
    {
        return SqlCoreHelper.GetItem(
            QueryOver.Of<TemplateResourceEntity>().Where(i => i.Name == name)
        );
    }
}