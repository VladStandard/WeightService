// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.PlusStorageMethods;

/// <summary>
/// Table validation "PLUS_STORAGE_METHODS".
/// </summary>
public sealed class PluStorageMethodValidator : WsSqlTableValidator<PluStorageMethodModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluStorageMethodValidator() : base(true, true)
    {
        RuleFor(item => item.MinTemp)
            .NotNull()
            .LessThanOrEqualTo(item => item.MaxTemp);
        RuleFor(item => item.MaxTemp)
            .NotNull()
            .GreaterThanOrEqualTo(item => item.MinTemp);
    }
}