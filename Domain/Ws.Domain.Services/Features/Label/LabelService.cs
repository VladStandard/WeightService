using Ws.Database.Core.Entities.Print.Labels;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Domain.Services.Features.Label;

internal class LabelService : ILabelService
{
    public LabelEntity GetItemByUid(Guid uid) => SqlCoreHelper.GetItemById<LabelEntity>(uid);

    public IEnumerable<ViewLabel> GetAll() => new SqlLabelRepository().GetAllView();

    public ViewLabel GetViewByUid(Guid uid) => SqlCoreHelper.GetItemById<ViewLabel>(uid);
}