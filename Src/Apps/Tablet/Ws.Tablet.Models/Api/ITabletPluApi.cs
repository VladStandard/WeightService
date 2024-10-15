using Ws.Tablet.Models.Features.Plus;

namespace Ws.Tablet.Models.Api;

public interface ITabletPluApi
{
    [Get("/plus")]
    Task<PluDto> GetPluByNumber(ushort number);
}