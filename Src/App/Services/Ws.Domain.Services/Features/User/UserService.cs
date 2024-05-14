using Ws.Database.Nhibernate.Entities.Ref.Users;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.User.Validators;

namespace Ws.Domain.Services.Features.User;

internal class UserService(SqlUserRepository userRepo) : IUserService
{
    [Transactional]
    public IEnumerable<Models.Entities.Users.User> GetAll() => userRepo.GetAll();

    [Transactional]
    public Models.Entities.Users.User GetItemByUid(Guid uid) => userRepo.GetByUid(uid);

    [Transactional, Validate<UserNewValidator>]
    public Models.Entities.Users.User Create(Models.Entities.Users.User item) => userRepo.Save(item);

    [Transactional, Validate<UserUpdateValidator>]
    public Models.Entities.Users.User Update(Models.Entities.Users.User item) => userRepo.Update(item);

    [Transactional]
    public void Delete(Models.Entities.Users.User item) => userRepo.Delete(item);

    [Transactional]
    public Models.Entities.Users.User GetItemByNameOrCreate(string username)
    {
        Models.Entities.Users.User user = userRepo.GetItemByUsername(username);
        return user.IsExists ? user : userRepo.Save(new() { Name = username, LoginDt = DateTime.Now });
    }
}