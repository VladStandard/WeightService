using Ws.Labels.Service.Generate.Shared.Dto;

namespace Ws.Labels.Service.Generate.Features.Weight.Dto;

public class GenerateWeightLabelDto
{
    public required PluFolLabel Plu { get; init; }
    public required ArmForLabel Line { get; init; }
    public required NestingForLabel Nesting { get; init; }
    public required short Kneading { get; init; }
    public required DateTime ProductDt { get; init; }
    public DateTime ExpirationDt => ProductDt.AddDays(Plu.ShelfLifeDays);
}