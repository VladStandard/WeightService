using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref1c.Plu;

namespace Ws.Labels.Service.Generate.Features.Weight.Dto;

public class GenerateWeightLabelDto
{
    public required Plu Plu { get; init; }
    public required Arm Line { get; init; }
    public required decimal Weight { get; init; }
    public required short Kneading { get; init; }
    public required DateTime ProductDt { get; init; }
}