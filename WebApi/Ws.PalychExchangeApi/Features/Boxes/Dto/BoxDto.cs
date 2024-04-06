using System.Xml.Serialization;
using FluentValidation;
using Ws.Database.EntityFramework.Entities.Ref1C.Boxes;
using Ws.PalychExchangeApi.Common;
using Ws.PalychExchangeApi.Utils;

namespace Ws.PalychExchangeApi.Features.Boxes.Dto;

[Serializable]
public sealed record BoxDto : BaseDto
{
    [XmlAttribute("Name")]
    public string Name = string.Empty;

    [XmlAttribute("Weight")]
    public decimal Weight;

    public BoxEntity ToEntity(DateTime updateDt) => new(Uid, Name, Weight, updateDt);
}

internal sealed class BoxDtoValidator : AbstractValidator<BoxDto>
{
    public BoxDtoValidator()
    {
        RuleFor(dto => dto.Uid)
            .NotEqual(Guid.Empty).WithMessage("UID - обязателен");
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("Наименование - обязательно")
            .MaximumLength(64).WithMessage("Наименование - не должно превышать 64 символа");
        RuleFor(dto => dto.Weight)
            .Must(ValidatorUtils.BeValidWeightDefault)
            .WithMessage("Вес - должен быть в [0, 1)");
    }
}