// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels.Nomenclatures;

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
        RuleFor(item => item.Number)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo((ushort)0_100)
            .LessThanOrEqualTo((ushort)10_999);
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.FullName)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Description)
            .NotNull();
        RuleFor(item => item.ShelfLifeDays)
            .NotNull()
            .GreaterThanOrEqualTo((ushort)0)
            .LessThanOrEqualTo((ushort)0_365);
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
        RuleFor(item => item.Nomenclature)
            .SetValidator(new NomenclatureValidator());
    }
}