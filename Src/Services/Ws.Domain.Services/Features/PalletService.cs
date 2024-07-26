using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Entities.Print.Labels;
using Ws.Database.Nhibernate.Entities.Print.Pallets;
using Ws.Database.Nhibernate.Sessions;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Domain.Services.Features;

public class PalletService(SqlLabelRepository labelRepo)
    : BaseRepository
{

    [Transactional]
    public void Create(Pallet pallet, IList<(Label, LabelZpl)> labels)
    {
        NHibernateHelper.GetSession().SetBatchSize(labels.Count);
        Session.Save(pallet);

        foreach ((Label label, LabelZpl labelZpl) in labels)
        {
            label.PalletUid = pallet.Uid;
            labelRepo.Save(label, labelZpl);
        }

        Session.Update(pallet.Arm);
    }
}