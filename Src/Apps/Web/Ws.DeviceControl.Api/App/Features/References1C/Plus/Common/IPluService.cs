using Ws.DeviceControl.Models.Features.References1C.Plus.Commands.Update;
using Ws.DeviceControl.Models.Features.References1C.Plus.Queries;

namespace Ws.DeviceControl.Api.App.Features.References1C.Plus.Common;

public interface IPluService : IGetApiService<PluDto>
{
    #region Queries

    Task<List<CharacteristicDto>> GetCharacteristics(Guid id);

    #endregion

    #region Commands

    Task<PluDto> Update(Guid id, PluUpdateDto dto);

    #endregion
}