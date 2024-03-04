using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Label;

public interface ILabelService : IGetItemByUid<LabelEntity>, IGetAll<ViewLabel>, ICreate<LabelEntity>
{
    ViewLabel GetViewByUid(Guid uid);
}