using Ws.Database.Core.Entities.Print.Labels;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Label;

internal class LabelService(SqlLabelRepository labelRepo) : ILabelService
{
    [Session] public LabelEntity GetItemByUid(Guid uid) => labelRepo.GetByUid(uid);

    [Session] public IEnumerable<ViewLabel> GetAll() => labelRepo.GetAllView();

    [Session] public ViewLabel GetViewByUid(Guid uid) => labelRepo.GetViewByUid(uid);
}