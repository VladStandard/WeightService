// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.Organizations;

/// <summary>
/// Table validation "ORGANIZATIONS".
/// </summary>
public sealed class WsSqlOrganizationValidator : WsSqlTableValidator<WsSqlOrganizationModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="isCheckIdentity"></param>
    public WsSqlOrganizationValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Gln)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Description)
            .NotNull();
    }
}
