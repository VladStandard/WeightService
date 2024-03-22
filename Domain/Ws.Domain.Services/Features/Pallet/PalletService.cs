using Ws.Database.Nhibernate.Entities.Print.Labels;
using Ws.Database.Nhibernate.Entities.Print.Pallets;
using Ws.Database.Nhibernate.Sessions;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Pallet;

internal class PalletService(SqlPalletRepository palletRepo, SqlLabelRepository labelRepo) : IPalletService
{
    [Transactional]
    public IEnumerable<ViewPallet> GetAllViewByWarehouse(WarehouseEntity warehouse) =>
        palletRepo.GetAllViewByWarehouse(warehouse);

    [Transactional]
    public ViewPallet GetViewByUid(Guid uid) => palletRepo.GetViewByUid(uid);

    [Transactional]
    public IEnumerable<LabelEntity> GetAllLabels(Guid palletUid) => palletRepo.GetAllLabels(palletUid);

    [Transactional]
    public void Create(PalletEntity pallet, IList<LabelEntity> labels)
    {
        NHibernateHelper.GetSession().SetBatchSize(labels.Count);

        pallet = palletRepo.Save(pallet);
        foreach (LabelEntity label in labels)
        {
            label.Pallet = pallet;
            labelRepo.Save(label);
        }
    }
}