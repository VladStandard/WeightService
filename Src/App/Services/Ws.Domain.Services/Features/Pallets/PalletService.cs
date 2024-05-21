using Ws.Database.Nhibernate.Entities.Print.Labels;
using Ws.Database.Nhibernate.Entities.Print.Pallets;
using Ws.Database.Nhibernate.Sessions;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Pallets;

internal class PalletService(SqlPalletRepository palletRepo, SqlLabelRepository labelRepo) : IPalletService
{
    #region List

    [Transactional]
    public IEnumerable<ViewPallet> GetAllViewByWarehouse(Warehouse warehouse) =>
        palletRepo.GetAllViewByWarehouse(warehouse);

    [Transactional]
    public IEnumerable<Label> GetAllLabels(Guid palletUid) => palletRepo.GetAllLabels(palletUid);

    #endregion

    #region Items

    [Transactional]
    public ViewPallet GetViewByUid(Guid uid) => palletRepo.GetViewByUid(uid);

    #endregion

    #region CRUD

    [Transactional]
    public void Create(Pallet pallet, IList<Label> labels)
    {
        NHibernateHelper.GetSession().SetBatchSize(labels.Count);

        pallet = palletRepo.Save(pallet);
        foreach (Label label in labels)
        {
            label.PalletUid = pallet.Uid;
            labelRepo.Save(label);
        }
    }

    #endregion
}