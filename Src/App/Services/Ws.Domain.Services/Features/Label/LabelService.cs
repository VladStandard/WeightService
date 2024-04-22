using Ws.Database.Nhibernate.Entities.Print.Labels;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Label.Validators;

namespace Ws.Domain.Services.Features.Label;

internal class LabelService(SqlLabelRepository labelRepo) : ILabelService
{
    [Transactional]
    public LabelEntity GetItemByUid(Guid uid) => labelRepo.GetByUid(uid);

    [Transactional]
    public IEnumerable<LabelEntity> GetAll() => labelRepo.GetAll();

    [Transactional, Validate<LabelNewValidator>]
    public LabelEntity Create(LabelEntity item)
    {
        return labelRepo.Save(item);
    }
}