using Ws.DeviceControl.Models.Features.References1C.Plus.Commands.Update;
using Ws.DeviceControl.Models.Features.References1C.Plus.Queries;

namespace Ws.DeviceControl.Models.Api.References1c;

public interface IPluApi
{
    #region Queries

    [Get("/plu")]
    Task<PluDto[]> GetPlus();

    [Get("/plu/{uid}")]
    Task<PluDto> GetPluByUid(Guid uid);

    [Get("/plu/{uid}/characteristics")]
    Task<CharacteristicDto> GetPluCharacteristics(Guid uid);

    [Put("/plu/{uid}")]
    Task<PluDto> UpdatePlu(Guid uid, [Body] PluUpdateDto updateDto);

    #endregion
}