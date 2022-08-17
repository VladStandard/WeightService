// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql;
using DataCore.Protocols;
using System.IO;
using DataCore.Files;
using System;
using FluentValidation;
using FluentNHibernate.Data;
using static DataCore.ShareEnums;

namespace DataCoreTests;

public static class TestsUtils
{
    #region Public and private fields, properties, constructor

    public static DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
    public static SqlConnectFactory SqlConnect { get; } = SqlConnectFactory.Instance;

	public static void SetupDebug()
    {
        DataAccess.JsonControl.SetupForTests(Directory.GetCurrentDirectory(),
            NetUtils.GetLocalHostName(true), nameof(DataCoreTests), JsonSettingsController.FileNameDebug);
        TestContext.WriteLine($"{nameof(DataAccess.JsonSettingsIsRemote)}: {DataAccess.JsonSettingsIsRemote}");
        TestContext.WriteLine(DataAccess.JsonSettingsIsRemote ? DataAccess.JsonSettingsRemote : DataAccess.JsonSettingsLocal);
	}

	public static void SetupRelease()
	{
		DataAccess.JsonControl.SetupForTests(Directory.GetCurrentDirectory(),
            NetUtils.GetLocalHostName(true), nameof(DataCoreTests), JsonSettingsController.FileNameRelease);
		TestContext.WriteLine($"{nameof(DataAccess.JsonSettingsIsRemote)}: {DataAccess.JsonSettingsIsRemote}");
		TestContext.WriteLine(DataAccess.JsonSettingsIsRemote ? DataAccess.JsonSettingsRemote : DataAccess.JsonSettingsLocal);
	}

	public static void DbTableAction(Action action)
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

	//private static AbstractValidator<TEntity> GetValidator<TEntity>() where TEntity : BaseEntity, new()
	private static IValidator<TEntity> GetValidator<TEntity>() where TEntity : BaseEntity, new()
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
		throw new NotImplementedException();
    }

	public static void DbTable_UniversalValidate_IsTrue<TEntity>(int maxResults) where TEntity : BaseEntity, new()
	{
		DbTableAction(() =>
		{
			// Arrange.
			IValidator<TEntity> validator = GetValidator<TEntity>();
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
	
	#endregion
}
