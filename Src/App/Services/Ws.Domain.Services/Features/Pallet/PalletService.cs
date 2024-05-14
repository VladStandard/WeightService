using Ws.Database.Nhibernate.Entities.Print.Labels;
using Ws.Database.Nhibernate.Entities.Print.Pallets;
using Ws.Database.Nhibernate.Sessions;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Pallet;

internal class PalletService(SqlPalletRepository palletRepo, SqlLabelRepository labelRepo) : IPalletService
{
    [Transactional]
    public IEnumerable<ViewPallet> GetAllViewByWarehouse(Models.Entities.Ref.Warehouse warehouse) =>
        palletRepo.GetAllViewByWarehouse(warehouse);

    [Transactional]
    public ViewPallet GetViewByUid(Guid uid) => palletRepo.GetViewByUid(uid);

    [Transactional]
    public IEnumerable<Models.Entities.Print.Label> GetAllLabels(Guid palletUid) => palletRepo.GetAllLabels(palletUid);

    [Transactional]
    public void Create(Models.Entities.Print.Pallet pallet, IList<Models.Entities.Print.Label> labels)
    {
        NHibernateHelper.GetSession().SetBatchSize(labels.Count);

        pallet = palletRepo.Save(pallet);
        foreach (Models.Entities.Print.Label label in labels)
        {
            label.PalletUid = pallet.Uid;
            labelRepo.Save(label);
        }
    }
}