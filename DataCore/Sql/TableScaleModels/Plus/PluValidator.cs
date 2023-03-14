// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels.Plus;

/// <summary>
/// Table validation "PLUS".
/// </summary>
public class PluValidator : SqlTableValidator<PluModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluValidator() : base(true, true)
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
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo((short)0_100)
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
}
/*
Вот все атрибуты, которые будут пустыми у групп
ЗаписьXML.ЗаписатьАтрибут("FullName",             "");
ЗаписьXML.ЗаписатьАтрибут("CategoryGuid",         "");
ЗаписьXML.ЗаписатьАтрибут("BrandGuid",             "");
ЗаписьXML.ЗаписатьАтрибут("MeasurementType",     "");
ЗаписьXML.ЗаписатьАтрибут("GroupGuid",             "");
ЗаписьXML.ЗаписатьАтрибут("AttachmentsCount",     "");
ЗаписьXML.ЗаписатьАтрибут("BoxTypeGuid",         "");
ЗаписьXML.ЗаписатьАтрибут("BoxTypeName",         "");
ЗаписьXML.ЗаписатьАтрибут("BoxTypeWeight",         "");
ЗаписьXML.ЗаписатьАтрибут("PackageTypeGuid",     "");
ЗаписьXML.ЗаписатьАтрибут("PackageTypeName",     "");
ЗаписьXML.ЗаписатьАтрибут("PackageTypeWeight",     "");
ЗаписьXML.ЗаписатьАтрибут("ClipTypeGuid",         "");
ЗаписьXML.ЗаписатьАтрибут("ClipTypeName",         "");
ЗаписьXML.ЗаписатьАтрибут("ClipTypeWeight",     "");
ЗаписьXML.ЗаписатьАтрибут("PluNumber",             "0");
ЗаписьXML.ЗаписатьАтрибут("Description",         "");
ЗаписьXML.ЗаписатьАтрибут("ShelfLife",             "");
*/