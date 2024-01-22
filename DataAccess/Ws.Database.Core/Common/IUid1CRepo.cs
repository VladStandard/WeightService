using Ws.Domain.Models.Common;

namespace Ws.Database.Core.Common;

internal interface IUid1CRepo<out TItem> where TItem : Table1CBase
{
    TItem GetByUid1C(Guid uid1C);
}