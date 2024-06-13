using Ws.Database.Nhibernate.Entities.Ref.Users;
using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Users.Validators;

namespace Ws.Domain.Services.Features.Users;

internal class UserService(SqlUserRepository userRepo) : IUserService
{
    #region List

    [Transactional]
    public IList<User> GetAll() => userRepo.GetAll();

    #endregion

    #region Items

    [Transactional]
    public User GetItemByUid(Guid uid) => userRepo.GetByUid(uid);

    [Transactional]
    public User GetItemByNameOrCreate(string username)
    {
        User user = userRepo.GetItemByUsername(username);
        return user.IsExists ? user : userRepo.Save(new() { Name = username, LoginDt = DateTime.Now });
    }


    #endregion

    #region CRUD

    [Transactional, Validate<UserNewValidator>]
    public User Create(User item) => userRepo.Save(item);

    [Transactional, Validate<UserUpdateValidator>]
    public User Update(User item) => userRepo.Update(item);

    [Transactional]
    public void Delete(User item) => userRepo.Delete(item);

    #endregion
}