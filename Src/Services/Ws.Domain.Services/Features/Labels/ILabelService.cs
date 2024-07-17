using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Labels;

public interface ILabelService : IGetItemByUid<Label>, IGetAll<Label>
{
    (Label, LabelZpl) Create(Label label, LabelZpl zpl);
    LabelZpl GetItemZplByUid(Guid uid);
}