namespace Ws.StorageCore.Entities.SchemaScale.PlusWeightings;

/// <summary>
/// Table validation "PLUS_WEIGHTINGS".
/// </summary>
public sealed class SqlPluWeighingValidator : SqlTableValidator<SqlPluWeighingEntity>
{

    public SqlPluWeighingValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Kneading)
            .NotEmpty()
            .NotNull()
            .GreaterThan(default(short));
        RuleFor(item => item.PluScale)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlPluScaleValidator(isCheckIdentity));
        RuleFor(item => item.NettoWeight)
            .NotEmpty()
            .NotNull()
            .NotEqual(0);
        RuleFor(item => item.WeightTare)
            .NotNull();
    }
}
