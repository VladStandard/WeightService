using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Labels;

public interface ILabelService : IGetItemByUid<Label>, ICreate<Label>, IGetAll<Label>;