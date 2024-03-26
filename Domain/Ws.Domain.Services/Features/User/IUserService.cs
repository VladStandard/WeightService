using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.User;

public interface IUserService : IGetAll<UserEntity>, IGetItemByUid<UserEntity>, ICreate<UserEntity>,
    IUpdate<UserEntity>, IDelete<UserEntity>
{
    UserEntity GetItemByNameOrCreate(string username);
}