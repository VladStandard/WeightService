// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Files;
using DataCore.Protocols;
using DataCore.Sql;
using FluentValidation;
using System;
using System.IO;
using DataCore.Localizations;
using static DataCore.Sql.SqlQueries.DbScales.Tables;

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
		switch (result.IsValid)
		{
			case false:
			{
				foreach (ValidationFailure failure in result.Errors)
				{
					TestContext.WriteLine($"{LocaleCore.Validator.Property} {failure.PropertyName} {LocaleCore.Validator.FailedValidation}. {LocaleCore.Validator.Error}: {failure.ErrorMessage}");
				}
				break;
			}
		}
	}

	private static IValidator<T> GetSqlValidator<T>(T item) where T : BaseEntity, new()
	{
		return item switch
		{
			AccessEntity => new AccessValidator(),
			AppEntity => new AppValidator(),
			BarCodeEntity => new BarCodeValidator(),
			BarCodeTypeEntity => new BarCodeTypeValidator(),
			ContragentEntity => new ContragentValidator(),
			HostEntity => new HostValidator(),
			LogEntity => new LogValidator(),
			LogTypeEntity => new LogTypeValidator(),
			NomenclatureEntity => new NomenclatureValidator(),
			OrderEntity => new OrderValidator(),
			OrderWeighingEntity => new OrderWeighingValidator(),
			PluEntity => new PluValidator(),
			PluLabelEntity => new PluLabelValidator(),
			PluScaleEntity => new PluScaleValidator(),
			PluWeighingEntity => new PluWeighingValidator(),
			TemplateEntity => new TemplateValidator(),
			_ => throw new NotImplementedException()
		};
	}

	public static void AssertSqlDataValidate<T>(int maxResults = 0) where T : BaseEntity, new()
	{
		AssertAction(() =>
		{
			// Arrange.
			IValidator<T> validator = GetSqlValidator<T>(Substitute.For<T>());
			T[]? items = DataAccess.Crud.GetEntities<T>(null, null, maxResults);
			// Act.
			if (items == null || !items.Any())
			{
				TestContext.WriteLine($"{nameof(items)} is null or empty!");
			}
			else
			{
				TestContext.WriteLine($"Found {nameof(items)}.Count: {items.Length}");
				int i = 0;
				foreach (T item in items)
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

	public static void AssertSqlValidate<T>(T item, bool assertResult) where T : BaseEntity, new()
	{
		// Arrange.
		IValidator<T> validator = GetSqlValidator<T>(item);
		// Act & Assert.
		DataCoreUtils.AssertValidate(item, validator, assertResult);
	}

	public static void AssertValidate<T>(T item, IValidator<T> validator, bool assertResult) where T : class, new()
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

	public static T CreateNewSubstitute<T>(bool isNotDefault) where T : BaseEntity, new()
	{
		T item = Substitute.For<T>();
		if (!isNotDefault)
		{
			return item;
		}

		item.IdentityUid = Guid.NewGuid();
		item.IdentityId = -1;
		item.CreateDt = DateTime.Now;
		item.ChangeDt = DateTime.Now;
		item.IsMarked = false;

		switch (item)
		{
			case AccessEntity access:
				{
					access.User = "Test";
					break;
				}
			case AppEntity app:
				{
					app.Name = "Test";
					break;
				}
			case BarCodeTypeEntity barCodeTypeV2:
				{
					barCodeTypeV2.Name = "Test";
					break;
				}
			case BarCodeEntity barCodeV2:
				{
					barCodeV2.Value = "Test";
					break;
				}
			case ContragentEntity contragentV2:
				{
					contragentV2.Name = "Test";
					break;
				}
			case HostEntity host:
				{
					host.Name = "Test";
					host.Ip = "127.0.0.1";
					host.MacAddressValue = "001122334455";
					host.HostName = "Test";
					host.AccessDt = DateTime.Now;
					break;
				}
			case LogTypeEntity logType:
				{
					logType.Icon = "Test";
					break;
				}
			case NomenclatureEntity nomenclature:
				{
					nomenclature.Name = "0.1.2";
					nomenclature.Code = "ЦБД00012345";
					nomenclature.Xml = "<Product Category=\"Сосиски\" > </Product>";
					nomenclature.Weighted = false;
					break;
				}
			case OrderEntity order:
				{
					order.Name = "Test";
					order.BoxCount = 1;
					order.PalletCount = 1;
					break;
				}
			case OrderWeighingEntity orderWeighing:
				{
					orderWeighing.Order = CreateNewSubstitute<OrderEntity>(isNotDefault);
					orderWeighing.PluWeighing = CreateNewSubstitute<PluWeighingEntity>(isNotDefault);
					break;
				}
			case PluEntity plu:
				{
					plu.Name = "Test";
					plu.Number = 100;
					plu.FullName = "Test";
					plu.Description = "Test";
					plu.Gtin = "Test";
					plu.Ean13 = "Test";
					plu.Itf14 = "Test";
					break;
				}
			case PluLabelEntity pluLabel:
				{
					pluLabel.Zpl = "Test";
					pluLabel.PluWeighing = CreateNewSubstitute<PluWeighingEntity>(isNotDefault);
					break;
				}
			case PluScaleEntity pluScale:
				{
					pluScale.IsActive = true;
					break;
				}
			case PluWeighingEntity pluWeighing:
				{
					pluWeighing.Sscc = "Test";
					pluWeighing.NettoWeight = (decimal)1.1;
					pluWeighing.TareWeight = (decimal)0.25;
					pluWeighing.ProdDt = DateTime.Now;
					pluWeighing.RegNum = 1;
					pluWeighing.PluScale = CreateNewSubstitute<PluScaleEntity>(isNotDefault);
					pluWeighing.Series = CreateNewSubstitute<ProductSeriesEntity>(isNotDefault);
					break;
				}
		}
		return item;
	}

	#endregion
}
