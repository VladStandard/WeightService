using Ws.Domain.Models.Entities.Print;
using Ws.StorageCore.Entities.Print.ViewLabels;
using Ws.StorageCore.Helpers;

namespace Ws.Services.Features.Label;

internal class LabelService : ILabelService
{
    public LabelEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<LabelEntity>(uid);

    public IEnumerable<ViewLabel> GetAll() => new ViewLabelRepository().GetList(new());

    public ViewLabel GetViewByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<ViewLabel>(uid);
}