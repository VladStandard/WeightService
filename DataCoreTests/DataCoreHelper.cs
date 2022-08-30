// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Files;
using DataCore.Localizations;
using DataCore.Protocols;
using DataCore.Sql;
using DataCore.Sql.Tables;
using FluentValidation;
using System;
using System.IO;
using System.Threading;

namespace DataCoreTests;

public class DataCoreHelper
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private static DataCoreHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public static DataCoreHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	#region Public and private fields, properties, constructor

	public DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
	public SqlConnectFactory SqlConnect { get; } = SqlConnectFactory.Instance;

	#endregion

	#region Public and private methods

	private void SetupDebug()
	{
		DataAccess.JsonControl.SetupForTests(Directory.GetCurrentDirectory(),
			NetUtils.GetLocalHostName(true), nameof(DataCoreTests), JsonSettingsController.FileNameDebug);
		TestContext.WriteLine($"{nameof(DataAccess.JsonSettingsIsRemote)}: {DataAccess.JsonSettingsIsRemote}");
		TestContext.WriteLine(DataAccess.JsonSettingsIsRemote ? DataAccess.JsonSettingsRemote : DataAccess.JsonSettingsLocal);
	}

	private void SetupRelease()
	{
		DataAccess.JsonControl.SetupForTests(Directory.GetCurrentDirectory(),
			NetUtils.GetLocalHostName(true), nameof(DataCoreTests), JsonSettingsController.FileNameRelease);
		TestContext.WriteLine($"{nameof(DataAccess.JsonSettingsIsRemote)}: {DataAccess.JsonSettingsIsRemote}");
		TestContext.WriteLine(DataAccess.JsonSettingsIsRemote ? DataAccess.JsonSettingsRemote : DataAccess.JsonSettingsLocal);
	}

	public void AssertAction(Action action)
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

	public void FailureWriteLine(ValidationResult result)
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

	private IValidator<T> GetSqlValidator<T>(T item) where T : TableModel, new()
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
			OrganizationEntity => new OrganizationValidator(),
			PluEntity => new PluValidator(),
			PluLabelEntity => new PluLabelValidator(),
			PluObsoleteEntity => new PluObsoleteValidator(),
			PluScaleEntity => new PluScaleValidator(),
			PluWeighingEntity => new PluWeighingValidator(),
			PrinterEntity => new PrinterValidator(),
			PrinterResourceEntity => new PrinterResourceValidator(),
			PrinterTypeEntity => new PrinterTypeValidator(),
			ProductionFacilityEntity => new ProductionFacilityValidator(),
			ProductSeriesEntity => new ProductSeriesValidator(),
			ScaleEntity => new ScaleValidator(),
			VersionEntity => new VersionValidator(),
			TaskEntity => new TaskValidator(),
			TaskTypeEntity => new TaskTypeValidator(),
			TemplateEntity => new TemplateValidator(),
			TemplateResourceEntity => new TemplateResourceValidator(),
			WorkShopEntity => new WorkShopValidator(),
			_ => throw new NotImplementedException()
		};
	}

	public void AssertSqlDataValidate<T>(int maxResults = 0) where T : TableModel, new()
	{
		AssertAction(() =>
		{
			foreach (bool isShowMarked in DataCoreEnums.GetBool())
			{
				// Arrange.
				IValidator<T> validator = GetSqlValidator(Substitute.For<T>());
				SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, null, maxResults, isShowMarked, true);
				T[]? items = DataAccess.Crud.GetItems<T>(sqlCrudConfig);
				// Act.
				if (items == null || !items.Any())
				{
					TestContext.WriteLine($"{nameof(items)} is null or empty!");
				}
				else
				{
					TestContext.WriteLine($"Found {items.Length} items. Print top 10.");
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
			}
		});
	}

	public void AssertSqlExtensionValidate<T>() where T : TableModel, new()
	{
		AssertAction(() =>
		{
			foreach (bool isShowMarked in DataCoreEnums.GetBool())
			{
				// Arrange.
				IValidator<T> validator = GetSqlValidator(Substitute.For<T>());
				SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, null, 0, isShowMarked, true);
				List<T> items = DataAccess.Crud.GetList<T>(sqlCrudConfig);
				// Act.
				if (!items.Any())
				{
					TestContext.WriteLine($"{nameof(items)} is null or empty!");
				}
				else
				{
					TestContext.WriteLine($"Found {items.Count} items. Print top 10.");
					List<T> itemsCast = items.Cast<T>().ToList();
					int i = 0;
					foreach (T item in itemsCast)
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
			}
		});
	}

	public void AssertSqlValidate<T>(T item, bool assertResult) where T : TableModel, new()
	{
		// Arrange.
		IValidator<T> validator = GetSqlValidator(item);
		// Act & Assert.
		AssertValidate(item, validator, assertResult);
	}

	private void AssertValidate<T>(T item, IValidator<T> validator, bool assertResult) where T : class, new()
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

	public T CreateNewSubstitute<T>(bool isNotDefault) where T : TableModel, new()
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
				access.User = "Test";
				break;
			case AppEntity app:
				app.Name = "Test";
				break;
			case BarCodeTypeEntity barCodeTypeV2:
				barCodeTypeV2.Name = "Test";
				break;
			case BarCodeEntity barCodeV2:
				barCodeV2.Value = "Test";
				break;
			case ContragentEntity contragentV2:
				contragentV2.Name = "Test";
				break;
			case HostEntity host:
				host.Name = "Test";
				host.Ip = "127.0.0.1";
				host.MacAddressValue = "001122334455";
				host.HostName = "Test";
				host.AccessDt = DateTime.Now;
				break;
			case LogEntity log:
				log.Version = "0.1.2";
				log.File = "Test.cs";
				log.Line = 1;
				log.Member = "Test";
				log.LogType = CreateNewSubstitute<LogTypeEntity>(isNotDefault);
				log.Message = "Test";
				break;
			case LogTypeEntity logType:
				logType.Icon = "Test";
				break;
			case NomenclatureEntity nomenclature:
				nomenclature.Name = "0.1.2";
				nomenclature.Code = "ЦБД00012345";
				nomenclature.Xml = "<Product Category=\"Сосиски\" > </Product>";
				nomenclature.Weighted = false;
				break;
			case OrderEntity order:
				order.Name = "Test";
				order.BoxCount = 1;
				order.PalletCount = 1;
				break;
			case OrderWeighingEntity orderWeighing:
				orderWeighing.Order = CreateNewSubstitute<OrderEntity>(isNotDefault);
				orderWeighing.PluWeighing = CreateNewSubstitute<PluWeighingEntity>(isNotDefault);
				break;
			case OrganizationEntity organization:
				organization.Name = "Test";
				organization.Gln = 1;
				organization.Xml = "Test";
				break;
			case PluEntity plu:
				plu.Name = "Test";
				plu.Number = 100;
				plu.FullName = "Test";
				plu.Description = "Test";
				plu.Gtin = "Test";
				plu.Ean13 = "Test";
				plu.Itf14 = "Test";
				plu.Template = CreateNewSubstitute<TemplateEntity>(isNotDefault);
				plu.Nomenclature = CreateNewSubstitute<NomenclatureEntity>(isNotDefault);
				break;
			case PluLabelEntity pluLabel:
				pluLabel.Zpl = "Test";
				pluLabel.PluWeighing = CreateNewSubstitute<PluWeighingEntity>(isNotDefault);
				break;
			case PluScaleEntity pluScale:
				pluScale.IsActive = true;
				pluScale.Plu = CreateNewSubstitute<PluEntity>(isNotDefault);
				pluScale.Scale = CreateNewSubstitute<ScaleEntity>(isNotDefault);
				break;
			case PluWeighingEntity pluWeighing:
				pluWeighing.Sscc = "Test";
				pluWeighing.NettoWeight = (decimal)1.1;
				pluWeighing.TareWeight = (decimal)0.25;
				pluWeighing.ProductDt = DateTime.Now;
				pluWeighing.RegNum = 1;
				pluWeighing.Kneading = 1;
				pluWeighing.PluScale = CreateNewSubstitute<PluScaleEntity>(isNotDefault);
				pluWeighing.Series = CreateNewSubstitute<ProductSeriesEntity>(isNotDefault);
				break;
			case PrinterEntity printer:
				printer.DarknessLevel = 1;
				printer.PrinterType = CreateNewSubstitute<PrinterTypeEntity>(isNotDefault);
				break;
			case PrinterResourceEntity printerResource:
				printerResource.Description = "Test";
				printerResource.Printer = CreateNewSubstitute<PrinterEntity>(isNotDefault);
				printerResource.Resource = CreateNewSubstitute<TemplateResourceEntity>(isNotDefault);
				break;
			case PrinterTypeEntity printerType:
				printerType.Name = "Test";
				break;
			case ProductionFacilityEntity productionFacility:
				productionFacility.Name = "Test";
				productionFacility.Address = "Test";
				break;
			case ProductSeriesEntity productSeries:
				productSeries.Sscc = "Test";
				productSeries.IsClose = false;
				productSeries.Scale = CreateNewSubstitute<ScaleEntity>(isNotDefault);
				break;
			case ScaleEntity scale:
				scale.Description = "Test";
				break;
			case VersionEntity version:
				version.Version = 1;
				version.Description = "Test";
				version.ReleaseDt = DateTime.Now;
				break;
			case TaskEntity task:
				task.TaskType = CreateNewSubstitute<TaskTypeEntity>(isNotDefault);
				task.Scale = CreateNewSubstitute<ScaleEntity>(isNotDefault);
				break;
			case TaskTypeEntity taskType:
				taskType.Name = "Test";
				break;
			case TemplateEntity template:
				template.Title = "Test";
				break;
			case TemplateResourceEntity templateResource:
				templateResource.Name = "Test";
				templateResource.Description = "Test";
				break;
			case WorkShopEntity workShop:
				workShop.Name = "Test";
				workShop.ProductionFacility = CreateNewSubstitute<ProductionFacilityEntity>(isNotDefault);
				break;
		}
		return item;
	}

	#endregion
}
