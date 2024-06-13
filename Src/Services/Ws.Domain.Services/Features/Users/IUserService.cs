using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Users;

public interface IUserService : IGetAll<User>, IGetItemByUid<User>, ICreate<User>,
    IUpdate<User>, IDelete<User>
{
    User GetItemByNameOrCreate(string username);
}