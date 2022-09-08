// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using BlazorCore.CssStyles;
using DataCore.Sql.Core;
using NSubstitute;
using NUnit.Framework;

namespace BlazorCoreTests.Models.CssStyles;

[TestFixture]
internal class TableBodyStyleValidatorTests
{
	#region Public and private fields, properties, constructor

	private BlazorCoreHelper Helper { get; } = BlazorCoreHelper.Instance;

	#endregion

	#region Public and private methods

	[Test]
	public void Model_Validate_IsFalse()
	{
		// Arrange.
		TableBodyStyleModel item = Substitute.For<TableBodyStyleModel>(
			new List<int>());
		// Act.
		// Assert.
		Helper.AssertStyleValidate(item, false);
		// Act.
		item.IdentityName = SqlFieldIdentityEnum.Empty;
		// Assert.
		Helper.AssertStyleValidate(item, false);
	}

	[Test]
	public void Model_Validate_IsTrue()
	{
		// Arrange.
		TableBodyStyleModel item = Substitute.For<TableBodyStyleModel>(true);
		// Act.
		item.IdentityName = SqlFieldIdentityEnum.Uid;
		// Assert.
		Helper.AssertStyleValidate(item, true);
	}

	#endregion
}
