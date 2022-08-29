// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using BlazorCore.Models.CssStyles;
using DataCore.Sql.Tables;
using NSubstitute;
using NUnit.Framework;

namespace BlazorCoreTests.Models.CssStyles;

[TestFixture]
internal class TableBodyStyleValidatorTests
{
	[Test]
	public void Entity_Validate_IsFalse()
	{
		// Arrange.
		TableBodyStyleModel item = Substitute.For<TableBodyStyleModel>(
			new List<int>());
		// Act.
		// Assert.
		BlazorCoreUtils.AssertStyleValidate(item, false);
		// Act.
		item.IdentityName = ColumnName.Default;
		// Assert.
		BlazorCoreUtils.AssertStyleValidate(item, false);
	}

	[Test]
	public void Entity_Validate_IsTrue()
	{
		// Arrange.
		TableBodyStyleModel item = Substitute.For<TableBodyStyleModel>(true);
		// Act.
		item.IdentityName = ColumnName.Uid;
		// Assert.
		BlazorCoreUtils.AssertStyleValidate(item, true);
	}
}
