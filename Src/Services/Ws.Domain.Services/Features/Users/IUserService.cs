using Ws.Domain.Models.Entities.Users;

namespace Ws.Domain.Services.Features.Users;

public interface IUserService : IGetAll<User>, IGetItemByUid<User>, ICreate<User>, IUpdate<User>, IDelete<Guid>;