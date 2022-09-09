// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.CssStyles;
using NSubstitute;
using NUnit.Framework;

namespace BlazorCoreTests.Models.CssStyles;

[TestFixture]
internal class CssStyleRadzenColumnValidatorTests
{
	#region Public and private fields, properties, constructor

	private BlazorCoreHelper Helper { get; } = BlazorCoreHelper.Instance;

	#endregion

	#region Public and private methods

	[Test]
	public void Model_Validate_IsFalse()
	{
		// Arrange.
		CssStyleRadzenColumnModel item = Substitute.For<CssStyleRadzenColumnModel>();
		// Act.
		// Assert.
		Helper.AssertStyleValidate(item, false);
		// Act.
		item.Width = "";
		// Assert.
		Helper.AssertStyleValidate(item, false);
	}

	[Test]
	public void Model_Validate_IsTrue()
	{
		// Arrange.
		CssStyleRadzenColumnModel item = Substitute.For<CssStyleRadzenColumnModel>();
		// Act.
		item.Width = "10%";
		// Assert.
		Helper.AssertStyleValidate(item, true);
	}

	#endregion
}
