// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels.PlusCharacteristics;

public class PluCharacteristicValidator : SqlTableValidator<PluCharacteristicModel>
{
    public PluCharacteristicValidator() : base(true, true)
    {
        RuleFor(item => item.AttachmentsCount)
            .NotNull()
            .GreaterThanOrEqualTo(0);
    }
}