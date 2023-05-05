// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.PlusCharacteristics;

public sealed class WsSqlPluCharacteristicValidator : WsSqlTableValidator<WsSqlPluCharacteristicModel>
{
    public WsSqlPluCharacteristicValidator() : base(true, true)
    {
        RuleFor(item => item.AttachmentsCount)
            .NotNull()
            .GreaterThanOrEqualTo(0);
    }
}