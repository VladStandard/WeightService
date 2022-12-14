﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.NomenclaturesGroupsFks;

namespace DataCoreTests.Sql.TableScaleFkModels;

[TestFixture]
internal class NomenclatureGroupFkValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        // Arrange & Act.
        NomenclatureGroupFkModel item = DataCore.CreateNewSubstitute<NomenclatureGroupFkModel>(false);
        // Assert.
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        // Arrange & Act.
        NomenclatureGroupFkModel item = DataCore.CreateNewSubstitute<NomenclatureGroupFkModel>(true);
        // Assert.
        DataCore.AssertSqlValidate(item, true);
    }
}
