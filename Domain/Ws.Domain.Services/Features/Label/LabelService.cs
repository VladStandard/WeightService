using Ws.Database.Core.Entities.Print.Labels;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Label;

internal class LabelService(SqlLabelRepository labelRepo) : ILabelService
{
    [Transactional] public LabelEntity GetItemByUid(Guid uid) => labelRepo.GetByUid(uid);

    [Transactional] public IEnumerable<ViewLabel> GetAll() => labelRepo.GetAllView();

    [Transactional] public ViewLabel GetViewByUid(Guid uid) => labelRepo.GetViewByUid(uid);
}