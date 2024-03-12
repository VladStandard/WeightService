using Ws.Database.Core.Entities.Print.Labels;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Label.Validators;

namespace Ws.Domain.Services.Features.Label;

internal class LabelService(SqlLabelRepository labelRepo) : ILabelService
{
    [Transactional]
    public LabelEntity GetItemByUid(Guid uid) => labelRepo.GetByUid(uid);

    [Transactional]
    public IEnumerable<ViewLabel> GetAll() => labelRepo.GetAllView();

    [Transactional]
    public ViewLabel GetViewByUid(Guid uid) => labelRepo.GetViewByUid(uid);

    [Transactional, Validate<LabelNewValidator>]
    public LabelEntity Create(LabelEntity item)
    {
        item.Pallet = null;
        return labelRepo.Save(item);
    }
}