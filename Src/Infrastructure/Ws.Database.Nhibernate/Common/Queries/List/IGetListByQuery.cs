using Ws.Domain.Models.Entities.Devices.Arms;

namespace Ws.Database.Nhibernate.Common.Queries.List;

internal interface IGetListByQuery<TItem>
{
    IList<ArmPlu> GetListByQuery(QueryOver<TItem> query);
}