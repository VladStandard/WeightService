using Ws.Database.Nhibernate.Entities.Print.Labels;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Domain.Services.Features;

public class LabelService(SqlLabelRepository labelRepo)
{
    [Transactional]
    public (Label, LabelZpl) Create(Label label, LabelZpl zpl)
    {
        return labelRepo.Save(label, zpl);
    }
}