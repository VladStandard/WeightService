using Ws.Database.Nhibernate.Entities.Ref.Users;
using Ws.Domain.Models.Entities.Users;

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

    #endregion

    #region CRUD

    [Transactional]
    public User Create(User item) => userRepo.Save(item);

    [Transactional]
    public User Update(User item) => userRepo.Update(item);

    [Transactional]
    public void DeleteById(Guid id) => userRepo.Delete(new() { Uid = id });

    #endregion
}