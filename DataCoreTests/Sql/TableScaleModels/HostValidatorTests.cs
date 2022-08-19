// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class HostValidatorTests
{
	[Test]
	public void Entity_Validate_IsFalse()
	{
		// Arrange.
		HostEntity item = Substitute.For<HostEntity>();
		// Act.
		// Assert.
		DataCoreUtils.AssertSqlValidate(item, false);
		// Act.
		item.Name = "";
		item.Ip = "";
		item.MacAddressValue = "";
		item.HostName = "";
		item.AccessDt = DateTime.Now;
		// Assert.
		DataCoreUtils.AssertSqlValidate(item, false);
	}

	[Test]
	public void Entity_Validate_IsTrue()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			HostEntity item = Substitute.For<HostEntity>();
			HostValidator validator = new();
			// Act.
			item.CreateDt = DateTime.Now;
			item.ChangeDt = DateTime.Now;
			item.IdentityId = -1;
			item.Name = "Test";
			item.Ip  = "127.0.0.1";
			item.MacAddressValue = "001122334455";
			item.HostName = "Test";
			item.AccessDt = DateTime.Now;
			ValidationResult result = validator.Validate(item);
			DataCoreUtils.FailureWriteLine(result);
			// Assert.
			Assert.IsTrue(result.IsValid);
		});
	}

	[Test]
	public void DbTable_Validate_IsTrue()
	{
		DataCoreUtils.AssertSqlDataValidate<HostEntity>(0);
	}
}
