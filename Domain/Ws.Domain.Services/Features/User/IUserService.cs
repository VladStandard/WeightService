using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Common;

namespace Ws.Domain.Services.Features.User;

public interface IUserService : IAll<UserEntity>, IUid<UserEntity>
{
    UserEntity GetItemByNameOrCreate(string username);
}