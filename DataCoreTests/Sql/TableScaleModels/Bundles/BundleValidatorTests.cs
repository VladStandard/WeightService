﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Bundles;

namespace DataCoreTests.Sql.TableScaleModels.Bundles;

internal class BundleValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        BundleModel item = DataCore.CreateNewSubstitute<BundleModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        BundleModel item = DataCore.CreateNewSubstitute<BundleModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}