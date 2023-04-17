// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.CssStyles;

namespace DataCoreTests.CssStyles;

[TestFixture]
internal class CssStyleTableHeadValidatorTests
{
	[Test]
	public void Model_Validate_IsFalse()
	{
		// Arrange.
		CssStyleTableHeadModel item = Substitute.For<CssStyleTableHeadModel>();
		// Act.
		// Assert.
		DataCoreTestsUtils.DataCore.AssertValidate(item, false);
		// Act.
		item.Color = "";
		// Assert.
		DataCoreTestsUtils.DataCore.AssertValidate(item, false);
	}

	[Test]
	public void Model_Validate_IsTrue()
	{
		// Arrange.
		CssStyleTableHeadModel item = Substitute.For<CssStyleTableHeadModel>();
		// Act.
		item.ColumnsWidths = new() { 20, 30, 20, 30 };
		item.ColumnsTitles = item.GetColumnsTitles();
		item.Color = "blue";
		item.FontWeight = "bold";
		item.TextAlign = "center";
		// Assert.
		DataCoreTestsUtils.DataCore.AssertValidate(item, true);
	}
}
