﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels.NomenclaturesCharacteristics;

public class NomenclaturesCharacteristicsValidator : SqlTableValidator<NomenclaturesCharacteristicsModel>
{
    public NomenclaturesCharacteristicsValidator()
    {
        RuleFor(item => item.AttachmentsCount)
            .NotNull()
            .GreaterThan(0);
    }
}
