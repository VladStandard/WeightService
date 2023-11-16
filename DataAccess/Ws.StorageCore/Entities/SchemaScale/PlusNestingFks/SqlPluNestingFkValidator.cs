namespace Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;

/// <summary>
/// Валидатор таблицы "PLUS_NESTING_FK".
/// </summary>
public sealed class SqlPluNestingFkValidator : SqlTableValidator<SqlPluNestingFkEntity>
{
    public SqlPluNestingFkValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlPluValidator(isCheckIdentity));
        RuleFor(item => item.BundleCount)
            .NotNull();
        RuleFor(item => item.Box)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlBoxValidator(isCheckIdentity));
        RuleFor(item => item.WeightMax)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100);
        RuleFor(item => item.WeightNom)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100);
        RuleFor(item => item.WeightMin)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100);
        RuleFor(item => item.WeightMax)
            .GreaterThanOrEqualTo(item => item.WeightMin)
            .GreaterThanOrEqualTo(item => item.WeightNom)
            .When(item => item.WeightMax > 0 && item is { WeightNom: > 0, WeightMin: > 0 });
        RuleFor(item => item.WeightNom)
            .GreaterThanOrEqualTo(item => item.WeightMin)
            .LessThanOrEqualTo(item => item.WeightMax)
            .When(item => item.WeightMax > 0 && item is { WeightNom: > 0, WeightMin: > 0 });
        RuleFor(item => item.WeightMin)
            .LessThanOrEqualTo(item => item.WeightMax)
            .LessThanOrEqualTo(item => item.WeightNom)
            .When(item => item.WeightMax > 0 && item is { WeightNom: > 0, WeightMin: > 0 });
    }
}