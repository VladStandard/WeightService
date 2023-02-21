// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels.Organizations;

/// <summary>
/// Table validation "ORGANIZATIONS".
/// </summary>
public class OrganizationValidator : SqlTableValidator<OrganizationModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public OrganizationValidator() : base(true, true)
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
