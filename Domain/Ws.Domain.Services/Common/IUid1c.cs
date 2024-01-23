using Ws.Domain.Models.Common;

namespace Ws.Domain.Services.Common;

public interface IUid1C<out TItem> where TItem : Table1CBase
{
    TItem GetByUid1С(Guid uid);
}