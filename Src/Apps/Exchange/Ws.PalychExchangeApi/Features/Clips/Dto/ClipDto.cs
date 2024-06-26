using System.Xml.Serialization;
using FluentValidation;
using Ws.Database.EntityFramework.Entities.Ref1C.Clips;
using Ws.PalychExchangeApi.Common;
using Ws.PalychExchangeApi.Utils;

namespace Ws.PalychExchangeApi.Features.Clips.Dto;

[Serializable]
public sealed record ClipDto : BaseDto
{
    [XmlAttribute("Name")]
    public string Name = string.Empty;

    [XmlAttribute("Weight")]
    public decimal Weight;

    public ClipEntity ToEntity(DateTime updateDt) => new()
    {
        Id = Uid,
        Name = Name,
        Weight = Weight,
        ChangeDt = updateDt
    };
}

// ReSharper disable once ClassNeverInstantiated.Global
internal sealed class ClipDtoValidator : AbstractValidator<ClipDto>
{
    public ClipDtoValidator()
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