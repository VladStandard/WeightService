using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Label;

public interface ILabelService : IGetItemByUid<LabelEntity>, ICreate<LabelEntity>, IGetAll<LabelEntity>;