// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Clips;
using DataCore.Sql.TableScaleModels.Plus;

namespace DataCore.Sql.TableScaleFkModels.PlusClipsFks;

/// <summary>
/// Table validation "PLUS_CLIP_FK".
/// </summary>
public sealed class PluClipFkValidator : SqlTableValidator<PluClipFkModel>
{
    public PluClipFkValidator() : base(true, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new PluValidator());
        RuleFor(item => item.Clip)
            .NotEmpty()
            .NotNull()
            .SetValidator(new ClipValidator());
    }
}