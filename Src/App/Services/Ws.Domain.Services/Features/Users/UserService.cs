using Ws.Database.Nhibernate.Entities.Ref.Users;
using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Users.Validators;

namespace Ws.Domain.Services.Features.Users;

internal class UserService(SqlUserRepository userRepo) : IUserService
{
    [Transactional]
    public IEnumerable<User> GetAll() => userRepo.GetAll();

    [Transactional]
    public User GetItemByUid(Guid uid) => userRepo.GetByUid(uid);

    [Transactional, Validate<UserNewValidator>]
    public User Create(User item) => userRepo.Save(item);

    [Transactional, Validate<UserUpdateValidator>]
    public User Update(User item) => userRepo.Update(item);

    [Transactional]
    public void Delete(User item) => userRepo.Delete(item);

    [Transactional]
    public User GetItemByNameOrCreate(string username)
    {
        User user = userRepo.GetItemByUsername(username);
        return user.IsExists ? user : userRepo.Save(new() { Name = username, LoginDt = DateTime.Now });
    }
}