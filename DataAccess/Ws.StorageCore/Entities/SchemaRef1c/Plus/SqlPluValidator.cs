namespace Ws.StorageCore.Entities.SchemaRef1c.Plus;

/// <summary>
/// Table validation "PLUS".
/// </summary>
public sealed class SqlPluValidator : SqlTableValidator<SqlPluEntity>
{
    public SqlPluValidator(bool isCheckIdentity, bool isCheckCreateDt, bool isCheckChangeDt) : base(isCheckIdentity, isCheckCreateDt, isCheckChangeDt)
    {
        RuleFor(item => item.Description)
            .NotNull();
        RuleFor(item => item.ShelfLifeDays)
            .NotNull()
            .GreaterThanOrEqualTo(byte.MinValue)
            .LessThanOrEqualTo(byte.MaxValue);
        // RuleFor(item => item.Gtin)
        //     .NotNull()
        //     .Empty().When(item => item.IsGroup)
        //     .Length(14).When(item => item.IsGroup == false);
        // RuleFor(item => item.Ean13)
        //     .NotNull()
        //     .Empty().When(item => item.IsGroup)
        //     .Length(13).When(item => item.IsGroup == false);
        // RuleFor(item => item.Itf14)
        //     .NotNull()
        //     .Empty().When(item => item.IsGroup || item.IsCheckWeight)
        //     .Length(14).When(item => item.IsGroup == false);
        RuleFor(item => item.IsCheckWeight)
            .NotNull();
        RuleFor(item => item.Code)
            .NotNull()
           // .NotEmpty()
            .Length(11);
        RuleFor(item => item.Name)
            .NotEmpty()
            .MaximumLength(150)
            .NotNull();
        RuleFor(item => item.Number)
            .NotNull()
            .GreaterThanOrEqualTo((short)0)
            .LessThanOrEqualTo((short)10_999)
            .When(item => !item.IsGroup);
        RuleFor(item => item.FullName)
            .NotNull()
            .When(item => !item.IsGroup);
        RuleFor(item => item.Number)
            .NotNull()
            //.NotEmpty()
            .When(item => item.IsGroup);
        RuleFor(item => item.FullName)
            .NotNull()
            .When(item => item.IsGroup);
        RuleFor(item => item.Bundle)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlBundleValidator(isCheckIdentity));
        RuleFor(item => item.Brand)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlBrandValidator(isCheckIdentity));
    }

    public SqlPluValidator(bool isCheckIdentity) : this(isCheckIdentity, true, true) { }
}