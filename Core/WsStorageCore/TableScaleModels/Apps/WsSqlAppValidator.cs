// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Apps;

/// <summary>
/// Table validation "APPS".
/// </summary>
public sealed class WsSqlAppValidator : WsSqlTableValidator<WsSqlAppModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlAppValidator() : base(false, false)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}