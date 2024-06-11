using Ws.Desktop.Models.Features.Pallets;

namespace Ws.Desktop.Api.App.Features.Pallets.Common;

public interface IPalletApiService
{
    public List<PalletList> GetAllByArm(Guid arm);
}