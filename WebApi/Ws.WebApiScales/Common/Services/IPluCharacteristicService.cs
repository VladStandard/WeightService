using Ws.WebApiScales.Dto.PluCharacteristic;

namespace Ws.WebApiScales.Common.Services;

public interface IPluCharacteristicService
{
    public void Load(PluCharacteristicsWrapper pluCharacteristics);
}