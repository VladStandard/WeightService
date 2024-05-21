using Ws.Database.Nhibernate.Entities.Print.Labels;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Labels.Validators;

namespace Ws.Domain.Services.Features.Labels;

internal class LabelService(SqlLabelRepository labelRepo) : ILabelService
{
    #region List

    [Transactional]
    public IList<Label> GetAll() => labelRepo.GetAll();

    #endregion

    #region Items

    [Transactional]
    public Label GetItemByUid(Guid uid) => labelRepo.GetByUid(uid);

    #endregion

    #region CRUD

    [Transactional, Validate<LabelNewValidator>]
    public Label Create(Label item)
    {
        return labelRepo.Save(item);
    }

    #endregion
}