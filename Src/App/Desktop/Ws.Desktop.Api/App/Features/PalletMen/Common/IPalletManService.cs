using Ws.Desktop.Models.Common;
using Ws.Desktop.Models.Features.PalletMen;

namespace Ws.Desktop.Api.App.Features.PalletMen.Common;

public interface IPalletManService
{
    OutputDto<List<PalletMan>> GetAll();
}