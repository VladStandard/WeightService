using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.User;

public interface IUserService : IGetAll<Models.Entities.Users.User>, IGetItemByUid<Models.Entities.Users.User>, ICreate<Models.Entities.Users.User>,
    IUpdate<Models.Entities.Users.User>, IDelete<Models.Entities.Users.User>
{
    Models.Entities.Users.User GetItemByNameOrCreate(string username);
}