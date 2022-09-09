﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.CssStyles;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace BlazorCoreTests.Models.CssStyles;

[TestFixture]
internal class TableHeadStyleValidatorTests
{
	private BlazorCoreHelper Helper { get; } = BlazorCoreHelper.Instance;

	[Test]
	public void Model_Validate_IsFalse()
	{
		// Arrange.
		TableHeadStyleModel item = Substitute.For<TableHeadStyleModel>();
		// Act.
		// Assert.
		Helper.AssertStyleValidate(item, false);
		// Act.
		item.Color = "";
		// Assert.
		Helper.AssertStyleValidate(item, false);
	}

	[Test]
	public void Model_Validate_IsTrue()
	{
		// Arrange.
		TableHeadStyleModel item = Substitute.For<TableHeadStyleModel>();
		// Act.
		item.ColumnsWidths = new() { 20, 30, 20, 30 };
		item.SetColumnsTitles();
		item.Color = "blue";
		item.FontWeight = "bold";
		item.TextAlign = "center";
		// Assert.
		Helper.AssertStyleValidate(item, true);
	}
}
