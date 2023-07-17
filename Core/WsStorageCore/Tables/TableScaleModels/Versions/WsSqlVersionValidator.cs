// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.Versions;

/// <summary>
/// Table validation "VERSIONS".
/// </summary>
public sealed class WsSqlVersionValidator : WsSqlTableValidator<WsSqlVersionModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="isCheckIdentity"></param>
    public WsSqlVersionValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.ReleaseDt)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
        RuleFor(item => item.Version)
            .NotEmpty()
            .NotNull()
            .GreaterThan(default(short));
        RuleFor(item => item.Name)
            .NotNull();
        RuleFor(item => item.Description)
            .NotEmpty()
            .NotNull();
    }
}
