namespace WsStorageCore.Entities.SchemaScale.BarCodes;

/// <summary>
/// Table validation "BARCODES".
/// </summary>
[DebuggerDisplay("{ToString()}")]
public sealed class SqlBarCodeValidator : SqlTableValidator<SqlBarCodeEntity>
{

    public SqlBarCodeValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.TypeTop)
            //.NotEmpty()
            .NotNull();
        RuleFor(item => item.ValueTop)
            //.NotEmpty()
            .NotNull();
        RuleFor(item => item.TypeRight)
            //.NotEmpty()
            .NotNull();
        RuleFor(item => item.ValueRight)
            //.NotEmpty()
            .NotNull();
        RuleFor(item => item.TypeBottom)
            //.NotEmpty()
            .NotNull();
        RuleFor(item => item.ValueBottom)
            //.NotEmpty()
            .NotNull();
    }
}
