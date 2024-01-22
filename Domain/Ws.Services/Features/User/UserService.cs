using Ws.Domain.Models.Entities.Ref;
using Ws.StorageCore.Entities.Ref.Users;
using Ws.StorageCore.Helpers;

namespace Ws.Services.Features.User;

internal class UserService : IUserService
{
    public UserEntity GetItemByNameOrCreate(string username) => new SqlUserRepository().GetItemByNameOrCreate(username);
    public IEnumerable<UserEntity> GetAll() => new SqlUserRepository().GetEnumerable();
    public UserEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<UserEntity>(uid);
}