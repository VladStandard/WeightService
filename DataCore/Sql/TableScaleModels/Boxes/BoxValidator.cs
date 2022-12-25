// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels.Boxes;

/// <summary>
/// Table validation "BOXES".
/// </summary>
public class BoxValidator : SqlTableValidator<BoxModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public BoxValidator()
    {
        RuleFor(item => item.Name)
            .NotNull();
        RuleFor(item => item.Weight)
            .NotEmpty()
            .NotNull();
    }
}