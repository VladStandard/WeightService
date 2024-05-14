using Ws.Database.Nhibernate.Entities.Print.Labels;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Label.Validators;

namespace Ws.Domain.Services.Features.Label;

internal class LabelService(SqlLabelRepository labelRepo) : ILabelService
{
    [Transactional]
    public Models.Entities.Print.Label GetItemByUid(Guid uid) => labelRepo.GetByUid(uid);

    [Transactional]
    public IEnumerable<Models.Entities.Print.Label> GetAll() => labelRepo.GetAll();

    [Transactional, Validate<LabelNewValidator>]
    public Models.Entities.Print.Label Create(Models.Entities.Print.Label item)
    {
        return labelRepo.Save(item);
    }
}