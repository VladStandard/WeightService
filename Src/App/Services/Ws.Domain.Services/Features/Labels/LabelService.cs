using Ws.Database.Nhibernate.Entities.Print.Labels;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Labels.Validators;

namespace Ws.Domain.Services.Features.Labels;

internal class LabelService(SqlLabelRepository labelRepo) : ILabelService
{
    [Transactional]
    public Label GetItemByUid(Guid uid) => labelRepo.GetByUid(uid);

    [Transactional]
    public IEnumerable<Label> GetAll() => labelRepo.GetAll();

    [Transactional, Validate<LabelNewValidator>]
    public Label Create(Label item)
    {
        return labelRepo.Save(item);
    }
}