using Ws.Desktop.Api.App.Features.Pallets.Common;
using Ws.Desktop.Models.Features.Pallets;

namespace Ws.Desktop.Api.App.Features.Pallets.Impl;

public class PalletApiService : IPalletApiService
{
    public List<PalletList> GetAllByArm(Guid arm) => [];
}