using Ws.Database.Nhibernate.Entities.Print.Labels;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Aspects;

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

    [Transactional]
    public LabelZpl GetItemZplByUid(Guid uid) => labelRepo.GetZplByUid(uid);

    #endregion

    #region CRUD

    [Transactional]
    public (Label, LabelZpl) Create(Label label, LabelZpl zpl)
    {
        return labelRepo.Save(label, zpl);
    }

    #endregion
}