using Ws.WebApiScales.Features.Characteristics.Dto;

namespace Ws.WebApiScales.Features.Characteristics.Services;

internal interface IPluCharacteristicApiService
{
    public void Load(CharacteristicsWrapper characteristics);
}