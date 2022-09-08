// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using DataCore.Models;

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class AccessValidatorTests
{
	#region Public and private fields, properties, constructor

	private DataCoreHelper Helper { get; } = DataCoreHelper.Instance;

	#endregion

	#region Public and private methods

	[Test]
	public void Model_Validate_IsFalse()
	{
		// Arrange & Act.
		AccessModel item = Helper.CreateNewSubstitute<AccessModel>(false);
		// Assert.
		Helper.AssertSqlValidate(item, false);
	}

	[Test]
	public void Model_Validate_IsTrue()
	{
		// Arrange.
		AccessModel item = Helper.CreateNewSubstitute<AccessModel>(true);
		// Act.
		foreach (AccessRightsEnum rights in Enum.GetValues(typeof(AccessRightsEnum)))
		{
			item.Rights = (byte)rights;
			// Assert.
			Helper.AssertSqlValidate(item, true);
		}
	}

	#endregion
}
