using FluentValidation;
using WsWebApiScales.Dto.Plu;

namespace WsWebApiScales.Validators;

public class PluDtoValidator : AbstractValidator<PluDto>
{
    public PluDtoValidator()
    {
        RuleFor(dto => dto.Uid)
            .NotEmpty().WithMessage("Поле 'Guid' обязательно для заполнения.");
        RuleFor(dto => dto.PluNumber)
            .Equal(0).When(item => item.IsGroup).WithMessage("Поле 'PluNumber' должно больше 0, при IsGroup = 1.")
            .GreaterThan(0).WithMessage("Поле 'PluNumber' должно больше 0.")
            .LessThanOrEqualTo(999).WithMessage("Поле 'PluNumber' должно быть меньше 1000.");
        RuleFor(dto => dto.Description)
            .Empty().When(item => item.IsGroup).WithMessage("Поле Description у PluGroup должно быть пустым.")
            .NotEmpty().When(item => !item.IsGroup).WithMessage("Поле 'Description' обязательно.");
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("Поле 'Name' обязательно для заполнения.")
            .MaximumLength(150).WithMessage("Поле 'Name' не должно превышать 150 символов.");
        RuleFor(dto => dto.FullName)
            .NotEmpty().When(item => !item.IsGroup).WithMessage("Поле 'FullName' обязательно для заполнения.");
        RuleFor(dto => dto.MeasurementType)
            .Must(value => value is "шт" or "кг").When(item=>!item.IsGroup).WithMessage("Значение MeasurementType должно быть 'шт' или 'кг'");
        RuleFor(dto => dto.Code)
            .NotEmpty()
            .Length(11).WithMessage("Поле 'Code' должно состоять из 11 символов.");
        RuleFor(dto => dto.Itf14)
            .Empty().When(item => item.IsGroup).WithMessage("Поле Itf14 у PluGroup должно быть пустым.")
            .Empty().When(item => item.IsCheckWeight).WithMessage("Поле Itf14 у IsCheckWeight должно быть пустым.")
            .Length(14).When(item => !item.IsGroup && !item.IsCheckWeight).WithMessage("Поле 'Itf14' должно состоять из 14 символов.");
        RuleFor(dto => dto.Ean13)
            .Empty().When(item => item.IsGroup).WithMessage("Поле Ean13 у PluGroup должно быть пустым.")
            .Length(13).When(item => !item.IsGroup).WithMessage("Поле 'Ean13' должно состоять из 13 символов.");
        RuleFor(dto => dto.ShelfLife)
            .Empty().When(item => item.IsGroup).WithMessage("Поле ShelfLife у PluGroup должно быть пустым.")
            .NotEmpty().When(item => !item.IsGroup).WithMessage("Поле 'ShelfLife' не должно быть == 0.");
        RuleFor(dto => dto.BrandGuid)
            .Empty().When(item => item.IsGroup).WithMessage("Поле BrandGuid у PluGroup должно быть пустым.")
            .NotEmpty().When(item => !item.IsGroup).WithMessage("Поле 'BrandGuid' обязательно.");
        RuleFor(dto => dto.BoxTypeGuid)
            .Empty().When(item => item.IsGroup).WithMessage("Поле BoxTypeGuid у PluGroup должно быть пустым.")
            .NotEmpty().When(item => !item.IsGroup).WithMessage("Поле 'BoxTypeGuid' обязательно.");
        RuleFor(dto => dto.BoxTypeName)
            .Empty().When(item => item.IsGroup).WithMessage("Поле BoxTypeName у PluGroup должно быть пустым.")
            .NotEmpty().When(item => !item.IsGroup).WithMessage("Поле 'BoxTypeName' обязательно.");
        RuleFor(dto => dto.BoxTypeWeight)
            .GreaterThan(0.001m).When(item => !item.IsGroup).WithMessage("Поле 'BoxTypeWeight' обязательно.");
        RuleFor(dto => dto.PackageTypeGuid)
            .Empty().When(item => item.IsGroup).WithMessage("Поле PackageTypeGuid у PluGroup должно быть пустым.");
        RuleFor(dto => dto.PackageTypeName)
            .NotEmpty().When(item => item.PackageTypeGuid != Guid.Empty).WithMessage("Поле 'PackageTypeName' обязательно.");
        RuleFor(dto => dto.PackageTypeWeight)
            .GreaterThan(0.001m).When(item => item.PackageTypeGuid != Guid.Empty).WithMessage("Поле 'PackageTypeWeight' обязательно.");
    }
}