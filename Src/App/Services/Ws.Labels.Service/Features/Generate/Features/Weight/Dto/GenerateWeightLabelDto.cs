using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Labels.Service.Features.Generate.Features.Weight.Dto;

public class GenerateWeightLabelDto
{
    public required PluEntity Plu { get; init; }
    public required LineEntity Line { get; init; }
    public required decimal Weight { get; init; }
    public required short Kneading { get; init; }
    public required DateTime ProductDt { get; init; }
}