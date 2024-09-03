using System.Xml.Serialization;
using FluentValidation;
using Ws.Database.EntityFramework.Entities.Ref1C.Bundles;
using Ws.PalychExchange.Api.Common;
using Ws.PalychExchange.Api.Utils;

namespace Ws.PalychExchange.Api.Features.Bundles.Dto;

[Serializable]
public sealed record BundleDto : BaseDto
{
    [XmlAttribute("Name")]
    public string Name = string.Empty;

    [XmlAttribute("Weight")]
    public decimal Weight;

    public BundleEntity ToEntity(DateTime updateDt) => new()
    {
        Id = Uid,
        Name = Name,
        Weight = Weight,
        ChangeDt = updateDt
    };
}

// ReSharper disable once ClassNeverInstantiated.Global
internal sealed class BundleDtoValidator : AbstractValidator<BundleDto>
{
    public BundleDtoValidator()
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