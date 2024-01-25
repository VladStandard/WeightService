using Ws.WebApiScales.Features.Nesting.Dto;

namespace Ws.WebApiScales.Features.Nesting.Services;

internal interface IPluCharacteristicApiService
{
    public void Load(PluCharacteristicsWrapper pluCharacteristics);
}