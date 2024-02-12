using Ws.Domain.Models.Entities.Print;

namespace Ws.Database.Core.Entities.Print.ViewLabels;

public sealed class ViewLabelRepository : BaseRepository, IGetAll<ViewLabel>
{
    public IEnumerable<ViewLabel> GetAll()
    {
        return SqlCoreHelper.Instance.GetEnumerable(
            QueryOver.Of<ViewLabel>().OrderBy(log => log.CreateDt).Desc
        ).ToList();
    }
}