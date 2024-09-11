using Ws.Database.EntityFramework.Entities.Ref1C.Brands;

namespace Ws.PalychExchange.Api.App.Features.Brands.Dto;

[Serializable]
public sealed record BrandDto : BaseDto
{
    [XmlAttribute("Name")]
    public string Name = string.Empty;

    public BrandEntity ToEntity(DateTime updateDate) => new()
    {
        Id = Uid,
        Name = Name,
        ChangeDt = updateDate
    };
}


// ReSharper disable once ClassNeverInstantiated.Global
internal sealed class BrandDtoValidator : AbstractValidator<BrandDto>
{
    public BrandDtoValidator()
    {
        RuleFor(dto => dto.Uid)
            .NotEqual(Guid.Empty).WithMessage("UID - обязателен");
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("Наименование - обязательно")
            .MaximumLength(64).WithMessage("Наименование - не должно превышать 64 символа");
    }
}