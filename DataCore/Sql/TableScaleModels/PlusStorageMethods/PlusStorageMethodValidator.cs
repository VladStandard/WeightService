// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.Scales;

namespace DataCore.Sql.TableScaleModels.PlusStorageMethods;

/// <summary>
/// Table validation "PLUS_STORAGE_METHODS".
/// </summary>
public class PlusStorageMethodValidator : SqlTableValidator<PlusStorageMethodModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PlusStorageMethodValidator() : base(true, true)
    {
        RuleFor(item => item.MinTemp)
            .NotNull()
            .LessThanOrEqualTo(item => item.MaxTemp);
        RuleFor(item => item.MaxTemp)
            .NotNull()
            .GreaterThanOrEqualTo(item => item.MinTemp);
    }
}