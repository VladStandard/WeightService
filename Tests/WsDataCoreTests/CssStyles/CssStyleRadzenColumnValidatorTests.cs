// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsBlazorCore.CssStyles;

namespace WsDataCoreTests.CssStyles;

[TestFixture]
public sealed class CssStyleRadzenColumnValidatorTests
{
    #region Public and private methods

    [Test]
    public void Model_Validate_IsFalse()
    {
        // Arrange.
        CssStyleRadzenColumnModel item = Substitute.For<CssStyleRadzenColumnModel>();
        // Act.
        // Assert.
        WsTestsUtils.DataCore.AssertValidate(item, false);
        // Act.
        item.Width = "";
        // Assert.
        WsTestsUtils.DataCore.AssertValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        // Arrange.
        CssStyleRadzenColumnModel item = Substitute.For<CssStyleRadzenColumnModel>();
        // Act.
        item.Width = "10%";
        // Assert.
        WsTestsUtils.DataCore.AssertValidate(item, true);
    }

    #endregion
}