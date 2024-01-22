using Ws.Domain.Models.Entities.Print;
using Ws.Services.Common;

namespace Ws.Services.Features.Label;

public interface ILabelService : IUid<LabelEntity>, IAll<ViewLabel>
{
    ViewLabel GetViewByUid(Guid uid);
}