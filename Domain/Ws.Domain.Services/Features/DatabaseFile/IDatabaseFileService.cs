using Ws.Domain.Models.Entities;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.DatabaseFile;

public interface IDatabaseFileService : IGetAll<DbFileSizeInfoEntity>;