using Ws.Database.Nhibernate.Entities.Print.Labels;
using Ws.Database.Nhibernate.Entities.Print.Pallets;
using Ws.Database.Nhibernate.Entities.Ref.Arms;
using Ws.Database.Nhibernate.Sessions;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Pallets;

internal class PalletService(SqlPalletRepository palletRepo, SqlLabelRepository labelRepo, SqlArmRepository armRepo)
    : IPalletService
{
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

        armRepo.Update(pallet.Arm);
    }

    #endregion
}