using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Common;

namespace Ws.Domain.Services.Features.Label;

public interface ILabelService : IUid<LabelEntity>, IAll<ViewLabel>
{
    ViewLabel GetViewByUid(Guid uid);
}