using Ws.Database.Nhibernate.Entities.Ref.Users;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.User.Validators;

namespace Ws.Domain.Services.Features.User;

internal class UserService(SqlUserRepository userRepo) : IUserService
{
    [Transactional]
    public IEnumerable<UserEntity> GetAll() => userRepo.GetAll();

    [Transactional]
    public UserEntity GetItemByUid(Guid uid) => userRepo.GetByUid(uid);

    [Transactional, Validate<UserNewValidator>]
    public UserEntity Create(UserEntity item) => userRepo.Save(item);

    [Transactional, Validate<UserUpdateValidator>]
    public UserEntity Update(UserEntity item) => userRepo.Update(item);

    [Transactional]
    public void Delete(UserEntity item) => userRepo.Delete(item);

    [Transactional]
    public UserEntity GetItemByNameOrCreate(string username)
    {
        UserEntity user = userRepo.GetItemByUsername(username);
        return user.IsExists ? user : userRepo.Save(new() { Name = username, LoginDt = DateTime.Now });
    }
}