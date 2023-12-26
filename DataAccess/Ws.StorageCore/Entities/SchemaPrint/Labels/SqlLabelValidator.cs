namespace Ws.StorageCore.Entities.SchemaPrint.Labels;

public sealed class SqlLabelValidator : SqlTableValidator<SqlLabelEntity>
{

    public SqlLabelValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Zpl)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.WeightNet)
            .NotNull();
         RuleFor(item => item.WeightTare)
            .NotNull();
         RuleFor(item => item.BarcodeTop)
            .NotEmpty();
        RuleFor(item => item.BarcodeRight)
            .NotEmpty();
        RuleFor(item => item.BarcodeBottom)
            .NotNull();
    }
}

// namespace Ws.StorageCore.Entities.SchemaScale.PlusWeightings;
//
// /// <summary>
// /// Table validation "PLUS_WEIGHTINGS".
// /// </summary>
// public sealed class SqlPluWeighingValidator : SqlTableValidator<SqlPluWeighingEntity>
// {
//
//     public SqlPluWeighingValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
//     {
//         RuleFor(item => item.Kneading)
//             .NotEmpty()
//             .NotNull()
//             .GreaterThan(default(short));
//         RuleFor(item => item.PluScale)
//             .NotEmpty()
//             .NotNull()
//             .SetValidator(new SqlPluScaleValidator(isCheckIdentity));
//         RuleFor(item => item.NettoWeight)
//             .NotEmpty()
//             .NotNull()
//             .NotEqual(0);
//         RuleFor(item => item.WeightTare)
//             .NotNull();
//     }
// }


// RuleFor(item => item.ValueTop)
//     //.NotEmpty()
//     .NotNull();
// RuleFor(item => item.ValueRight)
//     //.NotEmpty()
//     .NotNull();
// RuleFor(item => item.ValueBottom)
//     //.NotEmpty()
//     .NotNull();