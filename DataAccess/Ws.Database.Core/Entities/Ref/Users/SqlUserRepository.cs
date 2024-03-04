using Ws.Database.Core.Common.Commands;
using Ws.Database.Core.Common.Queries.Item;
using Ws.Database.Core.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Users;

public sealed class SqlUserRepository : BaseRepository, IGetItemByUid<UserEntity>, IGetAll<UserEntity>,
    ISave<UserEntity>, IUpdate<UserEntity>, IDelete<UserEntity>
{
    public UserEntity GetByUid(Guid uid) => Session.Get<UserEntity>(uid) ?? new();
    public IEnumerable<UserEntity> GetAll() => Session.Query<UserEntity>().OrderBy(i => i.Name).ToList();

    public UserEntity GetItemByUsername(string userName) => Session.Query<UserEntity>().FirstOrDefault(u =>
        u.Name.ToLower().Equals(userName.ToLower())) ?? new();
    
    public UserEntity Save(UserEntity item) { Session.Save(item); return item; }
    public UserEntity Update(UserEntity item) { Session.Update(item); return item; }
    public void Delete(UserEntity item) => Session.Delete(item);
}