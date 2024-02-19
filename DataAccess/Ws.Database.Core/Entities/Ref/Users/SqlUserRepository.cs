using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Users;

public sealed class SqlUserRepository :  BaseRepository, IGetItemByUid<UserEntity>, IGetAll<UserEntity>
{
    public UserEntity GetByUid(Guid uid) => Session.Get<UserEntity>(uid) ?? new();

    public IEnumerable<UserEntity> GetAll() => Session.Query<UserEntity>().OrderBy(i => i.Name).ToList();

    public UserEntity GetItemByUsername(string userName) => Session.Query<UserEntity>().FirstOrDefault(u => 
        u.Name.ToLower().Equals(userName.ToLower())) ?? new();
}