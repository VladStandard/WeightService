using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Label;

public interface ILabelService : IGetItemByUid<Models.Entities.Print.Label>, ICreate<Models.Entities.Print.Label>, IGetAll<Models.Entities.Print.Label>;