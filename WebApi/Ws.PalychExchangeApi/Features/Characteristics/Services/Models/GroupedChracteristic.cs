using FluentValidation;
using Ws.Database.EntityFramework.Entities.Ref1C.Characteristics;
using Ws.PalychExchangeApi.Common;

namespace Ws.PalychExchangeApi.Features.Characteristics.Services.Models;

internal record GroupedCharacteristic : BaseDto
{
    public required Guid PluUid { get; set; }
    public required Guid BoxUid { get; set; }
    public required short BundleCount { get; set; }
    public required bool IsDelete { get; set; }
    public required string Name { get; set; } = string.Empty;

    public CharacteristicEntity ToEntity(DateTime updateDt) =>
        new(Uid, updateDt)
        {
            Name = Name,
            PluId = PluUid,
            BoxId = BoxUid,
            BundleCount = BundleCount
        };
}

// ReSharper disable once ClassNeverInstantiated.Global
internal sealed class GroupedCharacteristicValidator : AbstractValidator<GroupedCharacteristic>
{
    public GroupedCharacteristicValidator()
    {
        RuleFor(dto => dto.Uid)
            .NotEqual(Guid.Empty).WithMessage("UID - обязателен");

        RuleFor(dto => dto.BundleCount)
            .Must(count => count is > 0 and <= 100)
            .WithMessage("Кол-во пакетов - должно быть от 1 до 100");

        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("Наименование - обязательно")
            .MaximumLength(64).WithMessage("Наименование - не должно превышать 64 символа");

        RuleFor(dto => dto.BoxUid)
            .NotEqual(Guid.Empty).WithMessage("Короб - обязателен");
    }
}