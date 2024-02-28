using Ws.Database.Core.Entities.Ref.Users;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.User;

internal class UserService(SqlUserRepository userRepo) : IUserService
{
    [Transactional] public IEnumerable<UserEntity> GetAll() => userRepo.GetAll();
    [Transactional] public UserEntity GetItemByUid(Guid uid) => userRepo.GetByUid(uid);
    [Transactional] public UserEntity Create(UserEntity item) => userRepo.Save(item);
    [Transactional] public UserEntity Update(UserEntity item) => userRepo.Update(item);
    
    [Transactional] public UserEntity GetItemByNameOrCreate(string username)
    {
        UserEntity user = userRepo.GetItemByUsername(username);
        return user.IsExists ? user : userRepo.Save(new() { Name = username, LoginDt = DateTime.Now });
    }
}