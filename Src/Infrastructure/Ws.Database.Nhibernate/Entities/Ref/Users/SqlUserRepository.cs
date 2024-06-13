using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Users;

namespace Ws.Database.Nhibernate.Entities.Ref.Users;

public sealed class SqlUserRepository : BaseRepository, IGetItemByUid<User>, IGetAll<User>,
    ISave<User>, IUpdate<User>, IDelete<User>
{
    public User GetByUid(Guid uid) => Session.Get<User>(uid) ?? new();
    public IList<User> GetAll() => Session.Query<User>().OrderBy(i => i.Name).ToList();

    public User GetItemByUsername(string userName) => Session.Query<User>().FirstOrDefault(u =>
        u.Name.ToLower().Equals(userName.ToLower())) ?? new();

    public User Save(User item) { Session.Save(item); return item; }
    public User Update(User item) { Session.Update(item); return item; }
    public void Delete(User item) => Session.Delete(item);
}