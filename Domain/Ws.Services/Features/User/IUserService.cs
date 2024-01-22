using Ws.Domain.Models.Entities.Ref;
using Ws.Services.Common;

namespace Ws.Services.Features.User;

public interface IUserService : IAll<UserEntity>, IUid<UserEntity>
{
    UserEntity GetItemByNameOrCreate(string username);
}