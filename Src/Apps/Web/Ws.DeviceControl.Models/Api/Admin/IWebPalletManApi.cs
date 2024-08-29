using Ws.DeviceControl.Models.Features.Admins.PalletMen.Commands.Create;
using Ws.DeviceControl.Models.Features.Admins.PalletMen.Commands.Update;
using Ws.DeviceControl.Models.Features.Admins.PalletMen.Queries;

namespace Ws.DeviceControl.Models.Api.Admin;

public interface IWebPalletManApi
{
    #region Queries

    [Get("/pallet-men/{uid}")]
    Task<PalletManDto> GetPalletManByUid(Guid uid);

    [Get("/pallet-men?productionSite={productionSiteUid}")]
    Task<PalletManDto[]> GetPalletMenByProductionSite(Guid productionSiteUid);

    #endregion

    #region Commands

    [Delete("/pallet-men/{uid}")]
    Task<bool> DeletePalletMan(Guid uid);

    [Post("/pallet-men")]
    Task<PalletManDto> CreatePalletMan([Body] PalletManCreateDto createDto);

    [Post("/pallet-men/{uid}")]
    Task<PalletManDto> UpdatePalletMan(Guid uid, [Body] PalletManUpdateDto updateDto);

    #endregion
}