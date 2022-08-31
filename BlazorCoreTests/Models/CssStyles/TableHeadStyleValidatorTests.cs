// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using BlazorCore.Models.CssStyles;
using DataCore.Localizations;
using NSubstitute;
using NUnit.Framework;

namespace BlazorCoreTests.Models.CssStyles;

[TestFixture]
internal class TableHeadStyleValidatorTests
{
	[Test]
	public void Model_Validate_IsFalse()
	{
		// Arrange.
		TableHeadStyleModel item = Substitute.For<TableHeadStyleModel>(
			new List<int>());
		// Act.
		// Assert.
		BlazorCoreUtils.AssertStyleValidate(item, false);
		// Act.
		item.Color = "";
		// Assert.
		BlazorCoreUtils.AssertStyleValidate(item, false);
	}

	[Test]
	public void Model_Validate_IsTrue()
	{
		// Arrange.
		TableHeadStyleModel item = Substitute.For<TableHeadStyleModel>(
			new List<int>() { 20, 30, 20, 30});
		// Act.
		item.ColumnsWidths = new() { 20, 30, 20, 30};
		item.ColumnsTitles.Returns(new TableHeadStyleModel().GetColumnsTitles());
		item.Color = "blue";
		item.FontWeight = "bold";
		item.TextAlign = "center";
		// Assert.
		BlazorCoreUtils.AssertStyleValidate(item, true);
	}
}
