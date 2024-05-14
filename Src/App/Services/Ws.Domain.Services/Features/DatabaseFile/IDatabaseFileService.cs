using Ws.Domain.Models.Entities.Diag;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.DatabaseFile;

public interface IDatabaseFileService : IGetAll<DbFileSizeInfo>;