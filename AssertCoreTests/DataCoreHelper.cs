// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

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
	public SqlConnectFactory SqlConnect { get; } = SqlConnectFactory.Instance;
	public JsonSettingsHelper JsonSettings { get; } = JsonSettingsHelper.Instance;

	#endregion

	#region Public and private methods

	public void SetupDebug()
	{
		JsonSettings.SetupTestsDebug(Directory.GetCurrentDirectory(),
			NetUtils.GetLocalDeviceName(true), nameof(AssertCoreTests));
		TestContext.WriteLine($"{nameof(DataAccess.JsonSettings.IsRemote)}: {DataAccess.JsonSettings.IsRemote}");
		TestContext.WriteLine(DataAccess.JsonSettings.IsRemote ? DataAccess.JsonSettings.Remote : DataAccess.JsonSettings.Local);
	}

	private void SetupRelease()
	{
		DataAccess.JsonSettings.SetupTestsRelease(Directory.GetCurrentDirectory(),
			NetUtils.GetLocalDeviceName(true), nameof(AssertCoreTests));
		TestContext.WriteLine($"{nameof(DataAccess.JsonSettings.IsRemote)}: {DataAccess.JsonSettings.IsRemote}");
		TestContext.WriteLine(DataAccess.JsonSettings.IsRemote ? DataAccess.JsonSettings.Remote : DataAccess.JsonSettings.Local);
	}

	public void AssertAction(Action action, bool isSkipDbRelease = false)
	{
		Assert.DoesNotThrow(() =>
		{
			if (!isSkipDbRelease)
			{
				SetupRelease();
				action.Invoke();
				TestContext.WriteLine();
			}

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

	public void AssertSqlDbContentValidate<T>() where T : SqlTableBase, new()
	{
		AssertAction(() =>
		{
			foreach (bool isShowMarked in DataCoreEnums.GetBool())
			{
				SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfigSection(isShowMarked);
				// Arrange.
				List<T> items = DataContext.GetListNotNullable<T>(sqlCrudConfig);
				// Act.
				if (!items.Any())
				{
					TestContext.WriteLine($"{nameof(items)} is null or empty!");
				}
				else
				{
					TestContext.WriteLine($"Found {items.Count} items. Print top 10.");
					int i = 0;
					foreach (T item in items)
					{
						if (i < 10)
							TestContext.WriteLine(item);
						i++;
						ValidationResult validationResult = ValidationUtils.GetValidationResult(item);
						FailureWriteLine(validationResult);
						// Assert.
						Assert.IsTrue(validationResult.IsValid);
					}
				}
			}
		});
	}

	public void AssertSqlValidate<T>(T item, bool assertResult) where T : SqlTableBase, new() =>
		AssertValidate(item, assertResult);

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
		SqlFieldIdentityModel fieldIdentity = Substitute.For<SqlFieldIdentityModel>(SqlFieldIdentityEnum.Empty);
		fieldIdentity.Name.Returns(SqlFieldIdentityEnum.Test);
		fieldIdentity.Uid.Returns(Guid.NewGuid());
		fieldIdentity.Id.Returns(-1);

		T item = Substitute.For<T>();
		if (!isNotDefault)
		{
			return item;
		}

		item.Identity.Returns(fieldIdentity);
		item.CreateDt.Returns(DateTime.Now);
		item.ChangeDt.Returns(DateTime.Now);
		item.IsMarked.Returns(false);
		item.Description.Returns(LocaleCore.Sql.SqlItemFieldDescription);

		switch (item)
		{
			case AccessModel access:
				access.Name = LocaleCore.Sql.SqlItemFieldName;
				access.Rights = (byte)AccessRightsEnum.None;
				access.LoginDt = DateTime.Now;
				break;
			case AppModel app:
				app.Name = LocaleCore.Sql.SqlItemFieldName;
				break;
			case BarCodeModel barCode:
				barCode.TypeTop = BarcodeTypeEnum.Default.ToString();
				barCode.ValueTop = LocaleCore.Sql.SqlItemFieldValue;
				barCode.TypeRight = BarcodeTypeEnum.Default.ToString();
				barCode.ValueRight = LocaleCore.Sql.SqlItemFieldValue;
				barCode.TypeBottom = BarcodeTypeEnum.Default.ToString();
				barCode.ValueBottom = LocaleCore.Sql.SqlItemFieldValue;
				break;
			case BrandModel brand:
                brand.Name = LocaleCore.Sql.SqlItemFieldName;
                brand.Code = LocaleCore.Sql.SqlItemFieldCode;
				break;
			case ContragentModel contragent:
				contragent.Name = LocaleCore.Sql.SqlItemFieldName;
				break;
			case DeviceModel device:
				//device.FillProperties();
				device.LoginDt = DateTime.Now;
				device.LogoutDt = DateTime.Now;
				device.Name = LocaleCore.Sql.SqlItemFieldName;
				device.PrettyName = LocaleCore.Sql.SqlItemFieldPrettyName;
				device.Ipv4 = LocaleCore.Sql.SqlItemFieldIp;
				device.MacAddressValue = LocaleCore.Sql.SqlItemFieldMac;
				break;
			case DeviceTypeModel deviceType:
				//deviceType.FillProperties();
				deviceType.Name = LocaleCore.Sql.SqlItemFieldName;
				deviceType.PrettyName = LocaleCore.Sql.SqlItemFieldPrettyName;
				break;
			case DeviceTypeFkModel deviceTypeFk:
				//deviceTypeFk.FillProperties();
				deviceTypeFk.Device = CreateNewSubstitute<DeviceModel>(isNotDefault);
				deviceTypeFk.Type = CreateNewSubstitute<DeviceTypeModel>(isNotDefault);
				break;
			case DeviceScaleFkModel deviceScaleFk:
				//deviceScaleFk.FillProperties();
				deviceScaleFk.Device = CreateNewSubstitute<DeviceModel>(isNotDefault);
				deviceScaleFk.Scale = CreateNewSubstitute<ScaleModel>(isNotDefault);
				break;
			case LogModel log:
				log.Version = LocaleCore.Sql.SqlItemFieldVersion;
				log.File = LocaleCore.Sql.SqlItemFieldFile;
				log.Line = 1;
				log.Member = LocaleCore.Sql.SqlItemFieldMember;
				log.LogType = CreateNewSubstitute<LogTypeModel>(isNotDefault);
				log.Message = LocaleCore.Sql.SqlItemFieldMessage;
				break;
			case LogTypeModel logType:
				logType.Icon = LocaleCore.Sql.SqlItemFieldIcon;
				break;
			case NomenclatureModel nomenclature:
				nomenclature.Name = LocaleCore.Sql.SqlItemFieldName;
				nomenclature.Code = LocaleCore.Sql.SqlItemFieldCode;
				nomenclature.Xml = LocaleCore.Sql.SqlItemFieldProductXml;
				nomenclature.Weighted = false;
				break;
			case NomenclatureV2Model nomenclatureV2:
                nomenclatureV2.Name = LocaleCore.Sql.SqlItemFieldName;
                nomenclatureV2.FullName = LocaleCore.Sql.SqlItemFieldFullName;
                nomenclatureV2.Code = LocaleCore.Sql.SqlItemFieldCode;
                nomenclatureV2.MeasurementType = LocaleCore.Sql.SqlItemFieldMeasurementTypeKg;
                break;
			case NomenclatureGroupModel nomenclatureGroup:
                nomenclatureGroup.Name = LocaleCore.Sql.SqlItemFieldName;
                nomenclatureGroup.Code = LocaleCore.Sql.SqlItemFieldCode;
                break;
            case NomenclatureGroupFkModel nomenclatureGroupFk:
                //nomenclatureGroupFk.FillProperties();
                nomenclatureGroupFk.NomenclatureGroup = CreateNewSubstitute<NomenclatureGroupModel>(isNotDefault);
                nomenclatureGroupFk.NomenclatureGroupParent = CreateNewSubstitute<NomenclatureGroupModel>(isNotDefault);
                break;
			case OrderModel order:
				order.Name = LocaleCore.Sql.SqlItemFieldName;
				order.BoxCount = 1;
				order.PalletCount = 1;
				break;
			case OrderWeighingModel orderWeighing:
				orderWeighing.Order = CreateNewSubstitute<OrderModel>(isNotDefault);
				orderWeighing.PluWeighing = CreateNewSubstitute<PluWeighingModel>(isNotDefault);
				break;
			case OrganizationModel organization:
				organization.Name = LocaleCore.Sql.SqlItemFieldName;
				organization.Gln = 1;
				break;
			case PluPackageModel packagePlu:
				packagePlu.Name = LocaleCore.Sql.SqlItemFieldName;
				packagePlu.Package = CreateNewSubstitute<PackageModel>(isNotDefault);
				packagePlu.Plu = CreateNewSubstitute<PluModel>(isNotDefault);
				break;
			case PackageModel package:
				package.Name = LocaleCore.Sql.SqlItemFieldName;
				package.Weight = 0.560M;
				break;
			case PluModel plu:
				plu.Name = LocaleCore.Sql.SqlItemFieldName;
				plu.Number = 100;
				plu.FullName = LocaleCore.Sql.SqlItemFieldFullName;
				plu.Gtin = LocaleCore.Sql.SqlItemFieldGtin;
				plu.Ean13 = LocaleCore.Sql.SqlItemFieldEan13;
				plu.Itf14 = LocaleCore.Sql.SqlItemFieldItf14;
				plu.Template = CreateNewSubstitute<TemplateModel>(isNotDefault);
				plu.Nomenclature = CreateNewSubstitute<NomenclatureModel>(isNotDefault);
				break;
			case PluLabelModel pluLabel:
				pluLabel.Zpl = LocaleCore.Sql.SqlItemFieldZpl;
				pluLabel.PluWeighing = CreateNewSubstitute<PluWeighingModel>(isNotDefault);
				pluLabel.PluScale = CreateNewSubstitute<PluScaleModel>(isNotDefault);
				pluLabel.ProductDt = DateTime.Now;
				pluLabel.ExpirationDt = DateTime.Now;
				break;
			case PluScaleModel pluScale:
				pluScale.IsActive = true;
				pluScale.Plu = CreateNewSubstitute<PluModel>(isNotDefault);
				pluScale.Scale = CreateNewSubstitute<ScaleModel>(isNotDefault);
				break;
			case PluWeighingModel pluWeighing:
				pluWeighing.Sscc = LocaleCore.Sql.SqlItemFieldSscc;
				pluWeighing.NettoWeight = 1.1M;
				pluWeighing.TareWeight = 0.25M;
				pluWeighing.RegNum = 1;
				pluWeighing.Kneading = 1;
				pluWeighing.PluScale = CreateNewSubstitute<PluScaleModel>(isNotDefault);
				pluWeighing.Series = CreateNewSubstitute<ProductSeriesModel>(isNotDefault);
				break;
			case PrinterModel printer:
				printer.DarknessLevel = 1;
				printer.PrinterType = CreateNewSubstitute<PrinterTypeModel>(isNotDefault);
				break;
			case PrinterResourceModel printerResource:
				printerResource.Printer = CreateNewSubstitute<PrinterModel>(isNotDefault);
				printerResource.TemplateResource = CreateNewSubstitute<TemplateResourceModel>(isNotDefault);
				break;
			case PrinterTypeModel printerType:
				printerType.Name = LocaleCore.Sql.SqlItemFieldName;
				break;
			case ProductionFacilityModel productionFacility:
				productionFacility.Name = LocaleCore.Sql.SqlItemFieldName;
				productionFacility.Address = LocaleCore.Sql.SqlItemFieldAddress;
				break;
			case ProductSeriesModel productSeries:
				productSeries.Sscc = LocaleCore.Sql.SqlItemFieldSscc;
				productSeries.IsClose = false;
				productSeries.Scale = CreateNewSubstitute<ScaleModel>(isNotDefault);
				break;
			case ScaleModel scale:
				scale.TemplateDefault = CreateNewSubstitute<TemplateModel>(isNotDefault);
				scale.TemplateSeries = CreateNewSubstitute<TemplateModel>(isNotDefault);
				scale.WorkShop = CreateNewSubstitute<WorkShopModel>(isNotDefault);
				scale.PrinterMain = CreateNewSubstitute<PrinterModel>(isNotDefault);
				scale.PrinterShipping = CreateNewSubstitute<PrinterModel>(isNotDefault);
				break;
			case ScaleScreenShotModel scaleScreenShot:
				scaleScreenShot.Scale = CreateNewSubstitute<ScaleModel>(isNotDefault);
				scaleScreenShot.ScreenShot = new byte[] { 0x00 };
				break;
			case TaskModel task:
				task.TaskType = CreateNewSubstitute<TaskTypeModel>(isNotDefault);
				task.Scale = CreateNewSubstitute<ScaleModel>(isNotDefault);
				break;
			case TaskTypeModel taskType:
				taskType.Name = LocaleCore.Sql.SqlItemFieldName;
				break;
			case TemplateModel template:
				template.Title = LocaleCore.Sql.SqlItemFieldTitle;
				break;
			case TemplateResourceModel templateResource:
				templateResource.Name = LocaleCore.Sql.SqlItemFieldName;
				break;
			case VersionModel version:
				version.Version = 1;
				version.ReleaseDt = DateTime.Now;
				break;
			case WorkShopModel workShop:
				workShop.Name = LocaleCore.Sql.SqlItemFieldName;
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
#pragma warning disable SYSLIB0011
			// Arrange.
			T item1 = new();
			SqlTableBase base1 = new();
			BinaryFormatter binaryFormatterItem = new();
			BinaryFormatter binaryFormatterBase = new();
			MemoryStream memoryStreamItem = new();
			MemoryStream memoryStreamBase = new();
			// Act.
			binaryFormatterItem.Serialize(memoryStreamItem, item1);
			binaryFormatterBase.Serialize(memoryStreamBase, base1);
			TestContext.WriteLine($"{nameof(item1)}: {item1}");
			TestContext.WriteLine($"{nameof(base1)}: {base1}");
			// Assert.
			Assert.AreNotEqual(memoryStreamItem, memoryStreamBase);
			// Act.
			memoryStreamItem.Position = 0;
			T item2 = (T)binaryFormatterItem.Deserialize(memoryStreamItem);
			TestContext.WriteLine($"{nameof(item2)}: {item2}");
			memoryStreamBase.Position = 0;
			SqlTableBase base2 = (SqlTableBase)binaryFormatterBase.Deserialize(memoryStreamBase);
			TestContext.WriteLine($"{nameof(base2)}: {base2}");
			// Assert.
			Assert.AreNotEqual(item2, base2);
#pragma warning restore SYSLIB0011
			// Finally.
			memoryStreamItem.Close();
			memoryStreamItem.Dispose();
			memoryStreamBase.Close();
			memoryStreamBase.Dispose();
		});
	}

	public void FieldBaseModelAssertSerialize<T>() where T : SqlFieldBase, new()
	{
		Assert.DoesNotThrow(() =>
		{
#pragma warning disable SYSLIB0011
			// Arrange.
			T item1 = new();
			SqlFieldBase base1 = new();
			BinaryFormatter binaryFormatterItem = new();
			BinaryFormatter binaryFormatterBase = new();
			MemoryStream memoryStreamItem = new();
			MemoryStream memoryStreamBase = new();
			// Act.
			binaryFormatterItem.Serialize(memoryStreamItem, item1);
			binaryFormatterBase.Serialize(memoryStreamBase, base1);
			TestContext.WriteLine($"{nameof(item1)}: {item1}");
			TestContext.WriteLine($"{nameof(base1)}: {base1}");
			// Assert.
			Assert.AreNotEqual(memoryStreamItem, memoryStreamBase);
			// Act.
			memoryStreamItem.Position = 0;
			T item2 = (T)binaryFormatterItem.Deserialize(memoryStreamItem);
			TestContext.WriteLine($"{nameof(item2)}: {item2}");
			memoryStreamBase.Position = 0;
			SqlTableBase base2 = (SqlTableBase)binaryFormatterBase.Deserialize(memoryStreamBase);
			TestContext.WriteLine($"{nameof(base2)}: {base2}");
			// Assert.
			Assert.AreNotEqual(item2, base2);
#pragma warning restore SYSLIB0011
			// Finally.
			memoryStreamItem.Close();
			memoryStreamItem.Dispose();
			memoryStreamBase.Close();
			memoryStreamBase.Dispose();
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
