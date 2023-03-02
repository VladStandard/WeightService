// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleFkModels.PlusTemplatesFks;
using DataCore.Sql.TableScaleModels.Access;
using DataCore.Sql.TableScaleModels.Apps;
using DataCore.Sql.TableScaleModels.BarCodes;
using DataCore.Sql.TableScaleModels.Boxes;
using DataCore.Sql.TableScaleModels.Brands;
using DataCore.Sql.TableScaleModels.Bundles;
using DataCore.Sql.TableScaleModels.Contragents;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.DeviceTypes;
using DataCore.Sql.TableScaleModels.Logs;
using DataCore.Sql.TableScaleModels.LogsTypes;
using DataCore.Sql.TableScaleModels.Nomenclatures;
using DataCore.Sql.TableScaleModels.Orders;
using DataCore.Sql.TableScaleModels.OrdersWeighings;
using DataCore.Sql.TableScaleModels.Organizations;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.PlusLabels;
using DataCore.Sql.TableScaleModels.PlusScales;
using DataCore.Sql.TableScaleModels.PlusWeighings;
using DataCore.Sql.TableScaleModels.Printers;
using DataCore.Sql.TableScaleModels.PrintersResources;
using DataCore.Sql.TableScaleModels.PrintersTypes;
using DataCore.Sql.TableScaleModels.ProductionFacilities;
using DataCore.Sql.TableScaleModels.ProductSeries;
using DataCore.Sql.TableScaleModels.Scales;
using DataCore.Sql.TableScaleModels.ScalesScreenshots;
using DataCore.Sql.TableScaleModels.Tasks;
using DataCore.Sql.TableScaleModels.TasksTypes;
using DataCore.Sql.TableScaleModels.Templates;
using DataCore.Sql.TableScaleModels.TemplatesResources;
using DataCore.Sql.TableScaleModels.Versions;
using DataCore.Sql.TableScaleModels.WorkShops;
using DataCore.Sql.TableScaleFkModels.PlusNestingFks;
using DataCore.Sql.Core.Enums;
using DataCore.Sql.Core.Helpers;
using DataCore.Sql.Core.Utils;
using DataCore.Sql.Core.Models;
using DataCore.Sql.TableScaleFkModels.PlusCharacteristicsFks;
using DataCore.Sql.TableScaleFkModels.PlusClipsFks;
using DataCore.Sql.TableScaleFkModels.PlusFks;
using DataCore.Sql.TableScaleFkModels.PlusGroupsFks;
using DataCore.Sql.TableScaleModels.Clips;
using DataCore.Sql.TableScaleModels.PlusCharacteristics;
using DataCore.Sql.TableScaleModels.PlusGroups;
using DataCore.Sql.TableScaleModels.LogsWebs;
using DataCore.Sql.TableScaleFkModels.LogsWebsFks;

namespace AssertCoreTests;

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
	public DataContextModel DataContext { get; } = new();
    public JsonSettingsHelper JsonSettings { get; } = JsonSettingsHelper.Instance;

	#endregion

	#region Public and private methods

	public void SetupDebug(bool isShowSql)
	{
		JsonSettings.SetupTestsDebug(Directory.GetCurrentDirectory(),
			NetUtils.GetLocalDeviceName(true), nameof(AssertCoreTests), isShowSql);
		TestContext.WriteLine($"{nameof(DataAccess.JsonSettings.IsRemote)}: {DataAccess.JsonSettings.IsRemote}");
		TestContext.WriteLine(DataAccess.JsonSettings.IsRemote ? DataAccess.JsonSettings.Remote : DataAccess.JsonSettings.Local);
	}

	private void SetupRelease(bool isShowSql)
	{
		DataAccess.JsonSettings.SetupTestsRelease(Directory.GetCurrentDirectory(),
			NetUtils.GetLocalDeviceName(true), nameof(AssertCoreTests), isShowSql);
		TestContext.WriteLine($"{nameof(DataAccess.JsonSettings.IsRemote)}: {DataAccess.JsonSettings.IsRemote}");
		TestContext.WriteLine(DataAccess.JsonSettings.IsRemote ? DataAccess.JsonSettings.Remote : DataAccess.JsonSettings.Local);
	}

	public void AssertAction(Action action, bool isShowSql, bool isSkipDbRelease = false)
	{
		Assert.DoesNotThrow(() =>
		{
			if (!isSkipDbRelease)
			{
				SetupRelease(isShowSql);
				action.Invoke();
				TestContext.WriteLine();
			}

			SetupDebug(isShowSql);
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

	public void AssertSqlDbContentValidate<T>(bool isShowMarked = false) where T : SqlTableBase, new()
    {
        AssertAction(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfigSection(isShowMarked);
            List<T> items = DataContext.GetListNotNullable<T>(sqlCrudConfig);

            if (!items.Any())
                TestContext.WriteLine($"{nameof(items)} is null or empty!");
            else
            {
                TestContext.WriteLine($"Found {items.Count} items. Print top 5.");
                int i = 0;
                foreach (T item in items)
                {
                    if (i < 5)
                        TestContext.WriteLine(item);
                    i++;
                    AssertSqlValidate(item, true);
                    ValidationResult validationResult = ValidationUtils.GetValidationResult(item);
                    FailureWriteLine(validationResult);
                    // Assert.
                    Assert.IsTrue(validationResult.IsValid);
                }
            }
        }, false, false);
    }

	public void AssertSqlValidate<T>(T item, bool assertResult) where T : SqlTableBase, new() =>
		AssertValidate(item, assertResult);

    public void AssertSqlDbContentSerialize<T>() where T : SqlTableBase, new()
    {
        AssertAction(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfigSection(false);
            List<T> items = DataContext.GetListNotNullable<T>(sqlCrudConfig);

            if (!items.Any())
                TestContext.WriteLine($"{nameof(items)} is null or empty!");
            else
            {
                TestContext.WriteLine($"Found {items.Count} items. Print top 5.");
                int i = 0;
                foreach (T item in items)
                {
                    string xml = DataFormatUtils.SerializeAsXmlString<T>(item, true, false);
                    if (i < 5)
                    {
                        TestContext.WriteLine(xml);
                        TestContext.WriteLine();
                    }
                    i++;
                    // Assert.
                    Assert.IsNotEmpty(xml);
                }
            }
        }, false, false);
    }

	public string GetSqlPropertyAsString<T>(bool isNotDefault, string propertyName) where T : SqlTableBase, new()
	{
		// Arrange
		T item = CreateNewSubstitute<T>(isNotDefault);
		// Act.
		string result = item.GetPropertyAsString(propertyName);
		TestContext.WriteLine($"{typeof(T)}. {propertyName}: {result}");
		return result;
	}

	public void AssertValidate<T>(T item, bool assertResult) where T : class, new()
	{
		Assert.DoesNotThrow(() =>
		{
			ValidationResult validationResult = ValidationUtils.GetValidationResult(item);
			FailureWriteLine(validationResult);
			// Assert.
			switch (assertResult)
			{
				case true:
					Assert.IsTrue(validationResult.IsValid);
					break;
				default:
					Assert.IsFalse(validationResult.IsValid);
					break;
			}
		});
	}

	public object? GetSqlPropertyValue<T>(bool isNotDefault, string propertyName) where T : SqlTableBase, new()
	{
		// Arrange
		T item = CreateNewSubstitute<T>(isNotDefault);
		// Act.
		object? value = item.GetPropertyAsObject(propertyName);
		TestContext.WriteLine($"{typeof(T)}. {propertyName}: {value}");
		return value;
	}

	public void AssertSqlPropertyCheckDt<T>(string propertyName) where T : SqlTableBase, new()
	{
		// Arrange & Act.
		object? value = GetSqlPropertyValue<T>(true, propertyName);
		if (value is DateTime dtValue)
		{
			// Assert.
			Assert.IsNotNull(dtValue);
			Assert.AreNotEqual(DateTime.MinValue, dtValue);
		}
	}

	public void AssertSqlPropertyCheckBool<T>(string propertyName) where T : SqlTableBase, new()
	{
		// Arrange & Act.
		object? value = GetSqlPropertyValue<T>(true, propertyName);
		if (value is bool isValue)
		{
			// Assert.
			Assert.IsNotNull(isValue);
			Assert.IsFalse(isValue);
		}
	}

	public void AssertSqlPropertyCheckString<T>(string propertyName) where T : SqlTableBase, new()
	{
		// Arrange & Act.
		object? value = GetSqlPropertyValue<T>(true, propertyName);
		if (value is string strValue)
		{
			// Assert.
			Assert.IsNotEmpty(strValue);
			Assert.IsNotNull(strValue);
		}
	}

	public T CreateNewSubstitute<T>(bool isNotDefault) where T : SqlTableBase, new()
	{
		SqlFieldIdentityModel fieldIdentity = Substitute.For<SqlFieldIdentityModel>(SqlFieldIdentity.Empty);
		fieldIdentity.Name.Returns(SqlFieldIdentity.Test);
		fieldIdentity.Uid.Returns(Guid.NewGuid());
		fieldIdentity.Id.Returns(-1);

		T item = Substitute.For<T>();
		if (!isNotDefault) return item;

		item.Identity.Returns(fieldIdentity);
		item.CreateDt.Returns(DateTime.Now);
		item.ChangeDt.Returns(DateTime.Now);
		item.IsMarked.Returns(false);
		item.Description.Returns(LocaleCore.Sql.SqlItemFieldDescription);

		switch (item)
		{
			case AccessModel access:
				access.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
				access.Rights.Returns((byte)AccessRightsEnum.None);
				access.LoginDt.Returns(DateTime.Now);
				break;
			case AppModel app:
				app.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
				break;
			case BarCodeModel barCode:
				barCode.TypeTop.Returns(BarcodeTypeEnum.Default.ToString());
				barCode.ValueTop.Returns(LocaleCore.Sql.SqlItemFieldValue);
				barCode.TypeRight.Returns(BarcodeTypeEnum.Default.ToString());
				barCode.ValueRight.Returns(LocaleCore.Sql.SqlItemFieldValue);
				barCode.TypeBottom.Returns(BarcodeTypeEnum.Default.ToString());
				barCode.ValueBottom.Returns(LocaleCore.Sql.SqlItemFieldValue);
				break;
            case BoxModel box:
                box.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
                box.Weight.Returns(3);
                break;
            case BrandModel brand:
                brand.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
                brand.Code.Returns(LocaleCore.Sql.SqlItemFieldCode);
				break;
			case BundleModel bundle:
                bundle.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
                bundle.Weight.Returns(3);
                break;
            case ClipModel clip:
                clip.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
                clip.Weight.Returns(2);
                break;
            case ContragentModel contragent:
				contragent.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
				break;
			case DeviceModel device:
                device.LoginDt.Returns(DateTime.Now);
				device.LogoutDt.Returns(DateTime.Now);
				device.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
				device.PrettyName.Returns(LocaleCore.Sql.SqlItemFieldPrettyName);
				device.Ipv4.Returns(LocaleCore.Sql.SqlItemFieldIp);
				device.MacAddressValue.Returns(LocaleCore.Sql.SqlItemFieldMac);
				break;
			case DeviceTypeModel deviceType:
                deviceType.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
				deviceType.PrettyName.Returns(LocaleCore.Sql.SqlItemFieldPrettyName);
				break;
			case DeviceTypeFkModel deviceTypeFk:
                deviceTypeFk.Device = CreateNewSubstitute<DeviceModel>(isNotDefault);
				deviceTypeFk.Type = CreateNewSubstitute<DeviceTypeModel>(isNotDefault);
				break;
			case DeviceScaleFkModel deviceScaleFk:
                deviceScaleFk.Device = CreateNewSubstitute<DeviceModel>(isNotDefault);
				deviceScaleFk.Scale = CreateNewSubstitute<ScaleModel>(isNotDefault);
				break;
			case LogModel log:
				log.Version.Returns(LocaleCore.Sql.SqlItemFieldVersion);
				log.File.Returns(LocaleCore.Sql.SqlItemFieldFile);
				log.Line.Returns(1);
				log.Member.Returns(LocaleCore.Sql.SqlItemFieldMember);
                log.LogType = CreateNewSubstitute<LogTypeModel>(isNotDefault);
                log.Message.Returns(LocaleCore.Sql.SqlItemFieldMessage);
				break;
			case LogTypeModel logType:
				logType.Icon.Returns(LocaleCore.Sql.SqlItemFieldIcon);
				break;
            case LogWebModel logWeb:
                logWeb.StampDt.Returns(DateTime.Now);
                logWeb.Version.Returns(LocaleCore.Sql.SqlItemFieldVersion);
                logWeb.Direction.Returns((byte)0);
                logWeb.Url.Returns(LocaleCore.Sql.SqlItemFieldUrl);
                logWeb.Params.Returns(string.Empty);
                logWeb.Headers.Returns(string.Empty);
                logWeb.DataString.Returns(string.Empty);
                logWeb.DataType.Returns((byte)0);
                logWeb.CountAll.Returns(2);
                logWeb.CountSuccess.Returns(1);
                logWeb.CountErrors.Returns(1);
                break;
			case LogWebFkModel logWebFk:
                logWebFk.LogWebRequest = CreateNewSubstitute<LogWebModel>(isNotDefault);
                logWebFk.LogWebResponse = CreateNewSubstitute<LogWebModel>(isNotDefault);
                logWebFk.LogWebResponse.Direction.Returns((byte)1);
                logWebFk.App = CreateNewSubstitute<AppModel>(isNotDefault);
                logWebFk.LogType = CreateNewSubstitute<LogTypeModel>(isNotDefault);
                logWebFk.Device = CreateNewSubstitute<DeviceModel>(isNotDefault);
				break;
			case PluGroupModel pluGroup:
                pluGroup.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
                pluGroup.Code.Returns(LocaleCore.Sql.SqlItemFieldCode);
                break;
            case PluCharacteristicModel nomenclatureCharacteristic:
                nomenclatureCharacteristic.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
                nomenclatureCharacteristic.AttachmentsCount.Returns(3);
                break;
            case PluCharacteristicsFkModel nomenclatureCharacteristicFk:
                nomenclatureCharacteristicFk.Plu = CreateNewSubstitute<PluModel>(isNotDefault);
                nomenclatureCharacteristicFk.Characteristic = CreateNewSubstitute<PluCharacteristicModel>(isNotDefault);
                break;
            case PluFkModel pluFk:
                pluFk.Plu = CreateNewSubstitute<PluModel>(isNotDefault);
                pluFk.Parent = CreateNewSubstitute<PluModel>(isNotDefault);
                break;
            case PluGroupFkModel pluGroupFk:
                pluGroupFk.PluGroup = CreateNewSubstitute<PluGroupModel>(isNotDefault);
                pluGroupFk.Parent = CreateNewSubstitute<PluGroupModel>(isNotDefault);
                break;
			case OrderModel order:
				order.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
				order.BoxCount.Returns(1);
				order.PalletCount.Returns(1);
				break;
			case OrderWeighingModel orderWeighing:
				orderWeighing.Order = CreateNewSubstitute<OrderModel>(isNotDefault);
				orderWeighing.PluWeighing = CreateNewSubstitute<PluWeighingModel>(isNotDefault);
				break;
			case OrganizationModel organization:
				organization.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
				organization.Gln.Returns(1);
				break;
            case PluModel plu:
				plu.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
				plu.Number.Returns((short)100);
				plu.FullName.Returns(LocaleCore.Sql.SqlItemFieldFullName);
				plu.Gtin.Returns(LocaleCore.Sql.SqlItemFieldGtin);
				plu.Ean13.Returns(LocaleCore.Sql.SqlItemFieldEan13);
				plu.Itf14.Returns(LocaleCore.Sql.SqlItemFieldItf14);
                plu.Code.Returns(LocaleCore.Sql.SqlItemFieldCode);
                plu.Nomenclature = CreateNewSubstitute<NomenclatureModel>(isNotDefault);
                break;
			case PluBundleFkModel pluBundle:
                pluBundle.Plu = CreateNewSubstitute<PluModel>(isNotDefault);
                pluBundle.Bundle = CreateNewSubstitute<BundleModel>(isNotDefault);
				break;
			case PluClipFkModel pluClips:
                pluClips.Plu = CreateNewSubstitute<PluModel>(isNotDefault);
                pluClips.Clip = CreateNewSubstitute<ClipModel>(isNotDefault);
                break;
            case PluLabelModel pluLabel:
				pluLabel.Zpl.Returns(LocaleCore.Sql.SqlItemFieldZpl);
				pluLabel.PluWeighing = CreateNewSubstitute<PluWeighingModel>(isNotDefault);
				pluLabel.PluScale = CreateNewSubstitute<PluScaleModel>(isNotDefault);
				pluLabel.ProductDt.Returns(DateTime.Now);
				pluLabel.ExpirationDt.Returns(DateTime.Now);
				break;
			case PluScaleModel pluScale:
				pluScale.IsActive.Returns(true);
				pluScale.Plu = CreateNewSubstitute<PluModel>(isNotDefault);
				pluScale.Scale = CreateNewSubstitute<ScaleModel>(isNotDefault);
				break;
			case PluTemplateFkModel pluTemplateFk:
                pluTemplateFk.Plu = CreateNewSubstitute<PluModel>(isNotDefault);
                pluTemplateFk.Template = CreateNewSubstitute<TemplateModel>(isNotDefault);
				break;
			case PluWeighingModel pluWeighing:
				pluWeighing.Sscc.Returns(LocaleCore.Sql.SqlItemFieldSscc);
				pluWeighing.NettoWeight.Returns(1.1M);
				pluWeighing.WeightTare.Returns(0.25M);
				pluWeighing.RegNum.Returns(1);
				pluWeighing.Kneading.Returns((short)1);
				pluWeighing.PluScale = CreateNewSubstitute<PluScaleModel>(isNotDefault);
				pluWeighing.Series = CreateNewSubstitute<ProductSeriesModel>(isNotDefault);
				break;
            case PluNestingFkModel pluNestingFk:
                pluNestingFk.IsDefault.Returns(false);
				pluNestingFk.PluBundle = CreateNewSubstitute<PluBundleFkModel>(isNotDefault);
                pluNestingFk.Box = CreateNewSubstitute<BoxModel>(isNotDefault);
                pluNestingFk.BundleCount.Returns((short)0);
                break;
            case PrinterModel printer:
				printer.DarknessLevel.Returns((short)1);
				printer.PrinterType = CreateNewSubstitute<PrinterTypeModel>(isNotDefault);
				break;
			case PrinterResourceModel printerResource:
				printerResource.Printer = CreateNewSubstitute<PrinterModel>(isNotDefault);
				printerResource.TemplateResource = CreateNewSubstitute<TemplateResourceDeprecatedModel>(isNotDefault);
				break;
			case PrinterTypeModel printerType:
				printerType.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
				break;
			case ProductionFacilityModel productionFacility:
				productionFacility.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
				productionFacility.Address.Returns(LocaleCore.Sql.SqlItemFieldAddress);
				break;
			case ProductSeriesModel productSeries:
				productSeries.Sscc.Returns(LocaleCore.Sql.SqlItemFieldSscc);
				productSeries.IsClose.Returns(false);
				productSeries.Scale = CreateNewSubstitute<ScaleModel>(isNotDefault);
				break;
			case ScaleModel scale:
                scale.WorkShop = CreateNewSubstitute<WorkShopModel>(isNotDefault);
				scale.PrinterMain = CreateNewSubstitute<PrinterModel>(isNotDefault);
				scale.PrinterShipping = CreateNewSubstitute<PrinterModel>(isNotDefault);
                scale.Number.Returns(10000);
                break;
			case ScaleScreenShotModel scaleScreenShot:
				scaleScreenShot.Scale = CreateNewSubstitute<ScaleModel>(isNotDefault);
				scaleScreenShot.ScreenShot.Returns(new byte[] { 0x00 });
				break;
			case TaskModel task:
				task.TaskType = CreateNewSubstitute<TaskTypeModel>(isNotDefault);
				task.Scale = CreateNewSubstitute<ScaleModel>(isNotDefault);
				break;
			case TaskTypeModel taskType:
				taskType.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
				break;
			case TemplateModel template:
				template.Title.Returns(LocaleCore.Sql.SqlItemFieldTitle);
				break;
			case TemplateResourceDeprecatedModel templateResource:
				templateResource.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
				break;
			case VersionModel version:
				version.Version.Returns((short)1);
				version.ReleaseDt.Returns(DateTime.Now);
				break;
			case WorkShopModel workShop:
				workShop.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
				workShop.ProductionFacility = CreateNewSubstitute<ProductionFacilityModel>(isNotDefault);
				break;
		}
		return item;
	}

	public void TableBaseModelAssertEqualsNew<T>() where T : SqlTableBase, new()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			T item = new();
			SqlTableBase baseItem = new();
			// Act.
			bool itemEqualsNew = item.EqualsNew();
			bool baseEqualsNew = baseItem.EqualsNew();
			// Assert.
			Assert.AreEqual(baseEqualsNew, itemEqualsNew);
		});
	}

	public void FieldBaseModelAssertEqualsNew<T>() where T : SqlFieldBase, new()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			T item = new();
			SqlFieldBase baseItem = new();
			// Act.
			bool itemEqualsNew = item.EqualsNew();
			bool baseEqualsNew = baseItem.EqualsNew();
			// Assert.
			Assert.AreEqual(baseEqualsNew, itemEqualsNew);
		});
	}

	public void TableBaseModelAssertSerialize<T>() where T : SqlTableBase, new()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			T item1 = new();
			SqlTableBase base1 = new();
			// Act.
			string xml1 = DataFormatUtils.SerializeAsXmlString<T>(item1, true, true);
			string xml2 = DataFormatUtils.SerializeAsXmlString<SqlTableBase>(base1, true, true);
			// Assert.
			Assert.AreNotEqual(xml1, xml2);
			// Act.
			T item2 = DataFormatUtils.DeserializeFromXml<T>(xml1);
			TestContext.WriteLine($"{nameof(item2)}: {item2}");
			SqlTableBase base2 = DataFormatUtils.DeserializeFromXml<SqlTableBase>(xml2);
			TestContext.WriteLine($"{nameof(base2)}: {base2}");
			// Assert.
			Assert.AreNotEqual(item2, base2);
		});
	}

	public void TableBaseModelAssertToString<T>() where T : SqlTableBase, new()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			T item = new();
			SqlTableBase baseItem = new();
			// Act.
			string itemString = item.ToString();
			string baseString = baseItem.ToString();
			TestContext.WriteLine($"{nameof(itemString)}: {itemString}");
			TestContext.WriteLine($"{nameof(baseString)}: {baseString}");
			// Assert.
			Assert.AreNotEqual(baseString, itemString);
		});
	}

	public void FieldBaseModelAssertToString<T>() where T : SqlFieldBase, new()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			T item = new();
			SqlFieldBase baseItem = new();
			// Act.
			string itemString = item.ToString();
			string baseString = baseItem.ToString();
			TestContext.WriteLine($"{nameof(itemString)}: {itemString}");
			TestContext.WriteLine($"{nameof(baseString)}: {baseString}");
			// Assert.
			Assert.AreNotEqual(baseString, itemString);
		});
	}

	#endregion
}
