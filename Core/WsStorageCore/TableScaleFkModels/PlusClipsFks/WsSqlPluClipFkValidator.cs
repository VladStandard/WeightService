// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusClipsFks;

/// <summary>
/// Table validation "PLUS_CLIP_FK".
/// </summary>
public sealed class WsSqlPluClipFkValidator : WsSqlTableValidator<WsSqlPluClipFkModel>
{
    public WsSqlPluClipFkValidator() : base(true, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluValidator());
        RuleFor(item => item.Clip)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlClipValidator());
    }
}