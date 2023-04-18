// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsBlazorCore.CssStyles;

namespace WsDataCoreTests.CssStyles;

[TestFixture]
public sealed class CssStyleTableHeadValidatorTests
{
	[Test]
	public void Model_Validate_IsFalse()
	{
		CssStyleTableHeadModel item = Substitute.For<CssStyleTableHeadModel>();
		
        WsTestsUtils.DataTests.AssertValidate(item, false);
		
        item.Color = "";
		
        WsTestsUtils.DataTests.AssertValidate(item, false);
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
		WsTestsUtils.DataTests.AssertValidate(item, true);
	}
}
