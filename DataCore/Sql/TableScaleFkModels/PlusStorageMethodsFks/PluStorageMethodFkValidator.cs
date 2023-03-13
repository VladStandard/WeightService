// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.PlusStorageMethods;

namespace DataCore.Sql.TableScaleFkModels.PlusStorageMethodsFks;

/// <summary>
/// Table validation "PLUS_STORAGE_METHODS_FK".
/// </summary>
public class PluStorageMethodFkValidator : SqlTableValidator<PluStorageMethodFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluStorageMethodFkValidator() : base(true, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new PluValidator());
        RuleFor(item => item.Method)
            .NotEmpty()
            .NotNull()
            .SetValidator(new PluStorageMethodValidator());
    }
}