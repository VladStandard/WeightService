// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Plus;

/// <summary>
/// Table validation "PLUS".
/// </summary>
public sealed class WsSqlPluValidator : WsSqlTableValidator<WsSqlPluModel>
{
    public WsSqlPluValidator(bool isCheckCreateDt, bool isCheckChangeDt) : base(isCheckCreateDt, isCheckChangeDt)
    {
        RuleFor(item => item.Description)
            .NotNull();
        RuleFor(item => item.ShelfLifeDays)
            .NotNull()
            .GreaterThanOrEqualTo(byte.MinValue)
            .LessThanOrEqualTo(byte.MaxValue);
        RuleFor(item => item.Gtin)
            .NotNull();
        RuleFor(item => item.Ean13)
            .NotNull();
        RuleFor(item => item.Itf14)
            .NotNull();
        RuleFor(item => item.IsCheckWeight)
            .NotNull();
        RuleFor(item => item.Code)
            .NotNull();
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        // !IsGroup.
        RuleFor(item => item.Number)
            .NotNull()
            .GreaterThanOrEqualTo((short)0)
            .LessThanOrEqualTo((short)10_999)
            .When(item => !item.IsGroup);
        RuleFor(item => item.FullName)
            .NotNull()
            .When(item => !item.IsGroup);
        // IsGroup.
        RuleFor(item => item.Number)
            .NotNull()
            .When(item => item.IsGroup);
        RuleFor(item => item.FullName)
            .NotNull()
            .When(item => item.IsGroup);
    }

    public WsSqlPluValidator() : this(true, true) { }
}
/*
Пустые атрибуты у групп
FullName
CategoryGuid
BrandGuid
MeasurementType
GroupGuid
AttachmentsCount
BoxTypeGuid
BoxTypeName
BoxTypeWeight
PackageTypeGuid
PackageTypeName
PackageTypeWeight
ClipTypeGuid
ClipTypeName
ClipTypeWeight
PluNumber
Description
ShelfLife
*/