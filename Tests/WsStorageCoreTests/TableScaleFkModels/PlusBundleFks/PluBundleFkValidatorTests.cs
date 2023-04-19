﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleFkModels.PlusBundleFks;

[TestFixture]
public sealed class PluBundleFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluBundleFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PluBundleFkModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluBundleFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PluBundleFkModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}