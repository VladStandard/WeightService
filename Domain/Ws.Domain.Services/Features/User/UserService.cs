using Ws.Database.Core.Entities.Ref.Users;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.User;

internal class UserService : IUserService
{
    public IEnumerable<UserEntity> GetAll() => new SqlUserRepository().GetAll();

    public UserEntity GetItemByUid(Guid uid) => new SqlUserRepository().GetByUid(uid);

    public UserEntity GetItemByNameOrCreate(string username)
    {
        UserEntity user = new SqlUserRepository().GetItemByUsername(username);

        user.Name = username;
        user.LoginDt = DateTime.Now;

        SqlCoreHelper.Instance.SaveOrUpdate(user);
        return user;
    }
}