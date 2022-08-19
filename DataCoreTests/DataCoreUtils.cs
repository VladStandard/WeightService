// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Files;
using DataCore.Protocols;
using DataCore.Sql;
using FluentValidation;
using System;
using System.IO;

namespace DataCoreTests;

public static class DataCoreUtils
{
	#region Public and private fields, properties, constructor

	public static DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
	public static SqlConnectFactory SqlConnect { get; } = SqlConnectFactory.Instance;

	#endregion

	#region Public and private methods

	private static void SetupDebug()
	{
		DataAccess.JsonControl.SetupForTests(Directory.GetCurrentDirectory(),
			NetUtils.GetLocalHostName(true), nameof(DataCoreTests), JsonSettingsController.FileNameDebug);
		TestContext.WriteLine($"{nameof(DataAccess.JsonSettingsIsRemote)}: {DataAccess.JsonSettingsIsRemote}");
		TestContext.WriteLine(DataAccess.JsonSettingsIsRemote ? DataAccess.JsonSettingsRemote : DataAccess.JsonSettingsLocal);
	}

	private static void SetupRelease()
	{
		DataAccess.JsonControl.SetupForTests(Directory.GetCurrentDirectory(),
			NetUtils.GetLocalHostName(true), nameof(DataCoreTests), JsonSettingsController.FileNameRelease);
		TestContext.WriteLine($"{nameof(DataAccess.JsonSettingsIsRemote)}: {DataAccess.JsonSettingsIsRemote}");
		TestContext.WriteLine(DataAccess.JsonSettingsIsRemote ? DataAccess.JsonSettingsRemote : DataAccess.JsonSettingsLocal);
	}

	public static void AssertAction(Action action)
	{
		Assert.DoesNotThrow(() =>
		{
			SetupRelease();
			action.Invoke();
			TestContext.WriteLine();

			SetupDebug();
			action.Invoke();
		});
	}

	public static void FailureWriteLine(ValidationResult result)
	{
		if (!result.IsValid)
			foreach (ValidationFailure? failure in result.Errors)
			{
				TestContext.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
			}
	}

	private static IValidator<TEntity> GetSqlValidator<TEntity>()
		where TEntity : BaseEntity, new()
	{
		if (typeof(TEntity) == typeof(AccessEntity))
		{
			return new AccessValidator();
		}
		else if (typeof(TEntity) == typeof(AppEntity))
		{
			return new AppValidator();
		}
		else if (typeof(TEntity) == typeof(BarCodeTypeV2Entity))
		{
			return new BarCodeTypeV2Validator();
		}
		else if (typeof(TEntity) == typeof(BarCodeV2Entity))
		{
			return new BarCodeV2Validator();
		}
		else if (typeof(TEntity) == typeof(ContragentV2Entity))
		{
			return new ContragentV2Validator();
		}
		else if (typeof(TEntity) == typeof(HostEntity))
		{
			return new HostValidator();
		}
		else if (typeof(TEntity) == typeof(LabelEntity))
		{
			return new LabelValidator();
		}
		else if (typeof(TEntity) == typeof(LogTypeEntity))
		{
			return new LogTypeValidator();
		}
		else if (typeof(TEntity) == typeof(LogEntity))
		{
			return new LogValidator();
		}
		else if (typeof(TEntity) == typeof(NomenclatureEntity))
		{
			return new NomenclatureValidator();
		}
		else if (typeof(TEntity) == typeof(OrderEntity))
		{
			return new OrderValidator();
		}
		else if (typeof(TEntity) == typeof(OrderWeighingEntity))
		{
			return new OrderWeighingValidator();
		}
		else if (typeof(TEntity) == typeof(PluScaleEntity))
		{
			return new PluScaleValidator();
		}
		else if (typeof(TEntity) == typeof(PluEntity))
		{
			return new PluValidator();
		}
		throw new NotImplementedException();
	}

	public static void AssertSqlDataValidate<TEntity>(int maxResults) where TEntity : BaseEntity, new()
	{
		AssertAction(() =>
		{
			// Arrange.
			IValidator<TEntity> validator = GetSqlValidator<TEntity>();
			TEntity[]? items = DataAccess.Crud.GetEntities<TEntity>(null, null, maxResults);
			// Act.
			if (items == null || !items.Any())
			{
				TestContext.WriteLine($"{nameof(items)} is null or empty!");
			}
			else
			{
				TestContext.WriteLine($"Found {nameof(items)}.Count: {items.Count()}");
				int i = 0;
				foreach (TEntity item in items)
				{
					if (i < 10)
						TestContext.WriteLine(item);
					i++;
					ValidationResult result = validator.Validate(item);
					FailureWriteLine(result);
					// Assert.
					Assert.IsTrue(result.IsValid);
				}
			}
		});
	}

	public static void AssertSqlValidate<TEntity>(TEntity item, bool assertResult)
		where TEntity : BaseEntity, new()
	{
		// Arrange.
		IValidator<TEntity> validator = GetSqlValidator<TEntity>();
		// Act & Assert.
		DataCoreUtils.AssertValidate(item, validator, assertResult);
	}

	public static void AssertValidate<TEntity>(TEntity item, IValidator<TEntity> validator, bool assertResult)
		where TEntity : new()
	{
		Assert.DoesNotThrow(() =>
		{
			// Act.
			ValidationResult result = validator.Validate(item);
			FailureWriteLine(result);
			// Assert.
			switch (assertResult)
			{
				case true:
					Assert.IsTrue(result.IsValid);
					break;
				default:
					Assert.IsFalse(result.IsValid);
					break;
			}
		});
	}

	#endregion
}
