// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels.Contragents;

/// <summary>
/// Table validation "CONTRAGENTS_V2".
/// </summary>
public sealed class ContragentValidator : SqlTableValidator<ContragentModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public ContragentValidator() : base(true, true)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}
