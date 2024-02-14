using Ws.Database.Core.Entities.Print.Labels;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Domain.Services.Features.Label;

internal class LabelService(SqlLabelRepository labelRepo) : ILabelService
{
    public LabelEntity GetItemByUid(Guid uid) => labelRepo.GetByUid(uid);

    public IEnumerable<ViewLabel> GetAll() => labelRepo.GetAllView();

    public ViewLabel GetViewByUid(Guid uid) => SqlCoreHelper.GetItemById<ViewLabel>(uid);
}