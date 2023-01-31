// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels.PlusGroups;

/// <summary>
/// Table validation "PLUS_GROUPS".
/// </summary>
public class PluGroupValidator : SqlTableValidator<PluGroupModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluGroupValidator() : base(true, true)
    {
        RuleFor(item => item.Name)
            .NotNull();
        RuleFor(item => item.Code)
            .NotEmpty()
            .NotNull();
    }
}