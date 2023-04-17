// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Tables;

namespace WsStorageCore.TableScaleModels.PlusCharacteristics;

public sealed class PluCharacteristicValidator : WsSqlTableValidator<PluCharacteristicModel>
{
    public PluCharacteristicValidator() : base(true, true)
    {
        RuleFor(item => item.AttachmentsCount)
            .NotNull()
            .GreaterThanOrEqualTo(0);
    }
}