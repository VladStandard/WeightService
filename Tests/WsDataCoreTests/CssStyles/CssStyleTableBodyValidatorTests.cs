// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsBlazorCore.CssStyles;
using WsStorageCore.Enums;

namespace WsDataCoreTests.CssStyles;

[TestFixture]
public sealed class CssStyleTableBodyValidatorTests
{
	#region Public and private methods

	[Test]
	public void Model_Validate_IsFalse()
	{
		// Arrange.
		CssStyleTableBodyModel item = Substitute.For<CssStyleTableBodyModel>();
		// Act.
		// Assert.
		WsTestsUtils.DataCore.AssertValidate(item, false);
		// Act.
		item.IdentityName = WsSqlFieldIdentity.Empty;
		// Assert.
		WsTestsUtils.DataCore.AssertValidate(item, false);
	}

	[Test]
	public void Model_Validate_IsTrue()
	{
		// Arrange.
		CssStyleTableBodyModel item = Substitute.For<CssStyleTableBodyModel>();
		// Act.
		item.IdentityName = WsSqlFieldIdentity.Uid;
		item.IsShowMarked = true;
		// Assert.
		WsTestsUtils.DataCore.AssertValidate(item, true);
	}

	#endregion
}
