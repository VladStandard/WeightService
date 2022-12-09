// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.CssStyles;

namespace DataCoreTests.CssStyles;

[TestFixture]
internal class CssStyleTableHeadValidatorTests
{
	private static DataCoreHelper DataCore => DataCoreHelper.Instance;

	[Test]
	public void Model_Validate_IsFalse()
	{
		// Arrange.
		CssStyleTableHeadModel item = Substitute.For<CssStyleTableHeadModel>();
		// Act.
		// Assert.
		DataCore.AssertValidate(item, false);
		// Act.
		item.Color = "";
		// Assert.
		DataCore.AssertValidate(item, false);
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
		DataCore.AssertValidate(item, true);
	}
}
