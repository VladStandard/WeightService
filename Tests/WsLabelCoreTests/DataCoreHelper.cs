using WsDataCore.Enums;
using WsLocalizationCore.Utils;
using WsStorageCore.Entities.SchemaDiag.Logs;
using WsStorageCore.Entities.SchemaDiag.LogsTypes;
using WsStorageCore.Entities.SchemaDiag.LogsWebs;
using WsStorageCore.Entities.SchemaRef.Hosts;
using WsStorageCore.Entities.SchemaRef.ProductionSites;
using WsStorageCore.Entities.SchemaRef.WorkShops;
using WsStorageCore.Entities.SchemaRef1c.Boxes;
using WsStorageCore.Entities.SchemaRef1c.Bundles;
using WsStorageCore.Entities.SchemaRef1c.Clips;
using WsStorageCore.Entities.SchemaRef1c.Plus;
using WsStorageCore.Entities.SchemaScale.Access;
using WsStorageCore.Entities.SchemaScale.BarCodes;
using WsStorageCore.Entities.SchemaScale.Organizations;
using WsStorageCore.Entities.SchemaScale.PlusClipsFks;
using WsStorageCore.Entities.SchemaScale.PlusFks;
using WsStorageCore.Entities.SchemaScale.PlusLabels;
using WsStorageCore.Entities.SchemaScale.PlusNestingFks;
using WsStorageCore.Entities.SchemaScale.PlusScales;
using WsStorageCore.Entities.SchemaScale.PlusStorageMethods;
using WsStorageCore.Entities.SchemaScale.PlusStorageMethodsFks;
using WsStorageCore.Entities.SchemaScale.PlusTemplatesFks;
using WsStorageCore.Entities.SchemaScale.PlusWeightings;
using WsStorageCore.Entities.SchemaScale.Scales;
using WsStorageCore.Entities.SchemaScale.Tasks;
using WsStorageCore.Entities.SchemaScale.TasksTypes;
using WsStorageCore.Entities.SchemaScale.Templates;
using WsStorageCore.Entities.SchemaScale.TemplatesResources;
using WsStorageCore.Entities.SchemaScale.Versions;
namespace WsLabelCoreTests;

public sealed class DataCoreHelper
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private static DataCoreHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public static DataCoreHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	#region Public and private fields, properties, constructor

    private WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    public WsJsonSettingsHelper JsonSettings => WsJsonSettingsHelper.Instance;

    #endregion

    #region Public and private methods

    public void SetupDevelopAleksandrov(bool isShowSql)
    {
        ContextManager.SetupJsonTestsDevelopAleksandrov(Directory.GetCurrentDirectory(),
            MdNetUtils.GetLocalDeviceName(true), nameof(WsLabelCoreTests), isShowSql);
        TestContext.WriteLine($"{nameof(JsonSettings.IsRemote)}: {JsonSettings.IsRemote}");
        TestContext.WriteLine(JsonSettings.IsRemote ? JsonSettings.Remote : JsonSettings.Local);
    }

    public void SetupDevelopMorozov(bool isShowSql)
    {
        ContextManager.SetupJsonTestsDevelopMorozov(Directory.GetCurrentDirectory(),
            MdNetUtils.GetLocalDeviceName(true), nameof(WsLabelCoreTests), isShowSql);
        TestContext.WriteLine($"{nameof(JsonSettings.IsRemote)}: {JsonSettings.IsRemote}");
        TestContext.WriteLine(JsonSettings.IsRemote ? JsonSettings.Remote : JsonSettings.Local);
    }

    public void SetupDevelopVs(bool isShowSql)
    {
        ContextManager.SetupJsonTestsDevelopVs(Directory.GetCurrentDirectory(),
            MdNetUtils.GetLocalDeviceName(true), nameof(WsLabelCoreTests), isShowSql);
        TestContext.WriteLine($"{nameof(JsonSettings.IsRemote)}: {JsonSettings.IsRemote}");
        TestContext.WriteLine(JsonSettings.IsRemote ? JsonSettings.Remote : JsonSettings.Local);
    }

    private void SetupReleaseVs(bool isShowSql)
    {
        ContextManager.SetupJsonTestsReleaseVs(Directory.GetCurrentDirectory(),
            MdNetUtils.GetLocalDeviceName(true), nameof(WsLabelCoreTests), isShowSql);
        TestContext.WriteLine($"{nameof(JsonSettings.IsRemote)}: {JsonSettings.IsRemote}");
        TestContext.WriteLine(JsonSettings.IsRemote ? JsonSettings.Remote : JsonSettings.Local);
    }

	public void AssertAction(Action action, bool isShowSql, bool isSkipDbRelease = false)
	{
		Assert.DoesNotThrow(() =>
		{
			if (!isSkipDbRelease)
			{
				SetupReleaseVs(isShowSql);
				action.Invoke();
				TestContext.WriteLine();
			}

			SetupDevelopVs(isShowSql);
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
						TestContext.WriteLine($"{WsLocaleCore.Validator.Property} {failure.PropertyName} {WsLocaleCore.Validator.FailedValidation}. {WsLocaleCore.Validator.Error}: {failure.ErrorMessage}");
					}
					break;
				}
		}
	}
    
    public void AssertSqlValidate<T>(T item, bool assertResult) where T : WsSqlEntityBase, new() =>
		AssertValidate(item, assertResult);
    
    private void AssertValidate<T>(T item, bool assertResult) where T : class, new()
	{
		Assert.DoesNotThrow(() =>
		{
			ValidationResult validationResult = WsSqlValidationUtils.GetValidationResult(item, true);
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

	public object? GetSqlPropertyValue<T>(bool isNotDefault, string propertyName) where T : WsSqlEntityBase, new()
	{
		// Arrange
		T item = CreateNewSubstitute<T>(isNotDefault);
		// Act.
		object? value = item.GetPropertyAsObject(propertyName);
		TestContext.WriteLine($"{typeof(T)}. {propertyName}: {value}");
		return value;
	}

	public void AssertSqlPropertyCheckDt<T>(string propertyName) where T : WsSqlEntityBase, new()
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

	public void AssertSqlPropertyCheckBool<T>(string propertyName) where T : WsSqlEntityBase, new()
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

	public void AssertSqlPropertyCheckString<T>(string propertyName) where T : WsSqlEntityBase, new()
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

	public T CreateNewSubstitute<T>(bool isNotDefault) where T : WsSqlEntityBase, new()
	{
		WsSqlFieldIdentityModel fieldIdentity = Substitute.For<WsSqlFieldIdentityModel>(WsSqlEnumFieldIdentity.Empty);
		fieldIdentity.Uid.Returns(Guid.NewGuid());
		fieldIdentity.Id.Returns(-1);

		T item = Substitute.For<T>();
		if (!isNotDefault) return item;

		item.Identity.Returns(fieldIdentity);
		item.CreateDt.Returns(DateTime.Now);
		item.ChangeDt.Returns(DateTime.Now);
		item.IsMarked.Returns(false);

		switch (item)
		{
			case WsSqlAccessEntity access:
				access.Rights.Returns((byte)WsEnumAccessRights.None);
				access.LoginDt.Returns(DateTime.Now);
				break;
			case WsSqlBarCodeEntity barCode:
				barCode.TypeTop.Returns(WsSqlBarcodeType.Default.ToString());
				barCode.TypeRight.Returns(WsSqlBarcodeType.Default.ToString());
				barCode.TypeBottom.Returns(WsSqlBarcodeType.Default.ToString());
				break;
            case WsSqlBoxEntity box:
                box.Weight.Returns(3);
                break;
			case WsSqlBundleEntity bundle:
                bundle.Weight.Returns(3);
                break;
            case WsSqlClipEntity clip:
                clip.Weight.Returns(2);
                break;
			case WsSqlHostEntity device:
                device.LoginDt.Returns(DateTime.Now);
				break;
			case WsSqlLogEntity log:
				log.Version.Returns(WsLocaleCore.Sql.SqlItemFieldVersion);
				log.File.Returns(WsLocaleCore.Sql.SqlItemFieldFile);
				log.Line.Returns(1);
				log.Member.Returns(WsLocaleCore.Sql.SqlItemFieldMember);
                log.LogType = CreateNewSubstitute<WsSqlLogTypeEntity>(isNotDefault);
                log.Message.Returns(WsLocaleCore.Sql.SqlItemFieldMessage);
				break;
			case WsSqlLogTypeEntity logType:
				logType.Icon.Returns(WsLocaleCore.Sql.SqlItemFieldIcon);
				break;
            case WsSqlLogWebEntity logWeb:
                logWeb.StampDt.Returns(DateTime.Now);
                logWeb.Version.Returns(WsLocaleCore.Sql.SqlItemFieldVersion);
                logWeb.Url.Returns(WsLocaleCore.Sql.SqlItemFieldUrl);
                logWeb.DataRequest.Returns(string.Empty);
                logWeb.CountAll.Returns(2);
                logWeb.CountSuccess.Returns(1);
                logWeb.CountErrors.Returns(1);
                break;
            case WsSqlPluFkEntity pluFk:
                pluFk.Plu = CreateNewSubstitute<WsSqlPluEntity>(isNotDefault);
                pluFk.Parent = CreateNewSubstitute<WsSqlPluEntity>(isNotDefault);
                break;
			case WsSqlOrganizationEntity organization:
				organization.Gln.Returns(1);
				break;
            case WsSqlPluEntity plu:
				plu.Number.Returns((short)100);
                break;
			case WsSqlPluClipFkEntity pluClips:
                pluClips.Plu = CreateNewSubstitute<WsSqlPluEntity>(isNotDefault);
                pluClips.Clip = CreateNewSubstitute<WsSqlClipEntity>(isNotDefault);
                break;
            case WsSqlPluLabelEntity pluLabel:
				pluLabel.Zpl.Returns(WsLocaleCore.Sql.SqlItemFieldZpl);
				pluLabel.PluWeighing = CreateNewSubstitute<WsSqlPluWeighingEntity>(isNotDefault);
				pluLabel.PluScale = CreateNewSubstitute<WsSqlPluScaleEntity>(isNotDefault);
				pluLabel.ProductDt.Returns(DateTime.Now);
				pluLabel.ExpirationDt.Returns(DateTime.Now);
				break;
			case WsSqlPluScaleEntity pluScale:
				pluScale.IsActive.Returns(true);
				pluScale.Plu = CreateNewSubstitute<WsSqlPluEntity>(isNotDefault);
				pluScale.Line = CreateNewSubstitute<WsSqlScaleEntity>(isNotDefault);
				break;
            case WsSqlPluStorageMethodEntity plusStorageMethod:
                plusStorageMethod.MinTemp.Returns((short)0);
                plusStorageMethod.MaxTemp.Returns((short)0);
                break;
            case WsSqlPluStorageMethodFkEntity pluStorageMethodFk:
                pluStorageMethodFk.Plu = CreateNewSubstitute<WsSqlPluEntity>(isNotDefault);
                pluStorageMethodFk.Method = CreateNewSubstitute<WsSqlPluStorageMethodEntity>(isNotDefault);
                break;
			case WsSqlPluTemplateFkEntity pluTemplateFk:
                pluTemplateFk.Plu = CreateNewSubstitute<WsSqlPluEntity>(isNotDefault);
                pluTemplateFk.Template = CreateNewSubstitute<WsSqlTemplateEntity>(isNotDefault);
				break;
			case WsSqlPluWeighingEntity pluWeighing:
				pluWeighing.NettoWeight.Returns(1.1M);
				pluWeighing.WeightTare.Returns(0.25M);
				pluWeighing.Kneading.Returns((short)1);
				pluWeighing.PluScale = CreateNewSubstitute<WsSqlPluScaleEntity>(isNotDefault);
				break;
            case WsSqlPluNestingFkEntity pluNestingFk:
                pluNestingFk.IsDefault.Returns(false);
				pluNestingFk.Plu = CreateNewSubstitute<WsSqlPluEntity>(isNotDefault);
                pluNestingFk.Box = CreateNewSubstitute<WsSqlBoxEntity>(isNotDefault);
                pluNestingFk.BundleCount.Returns((short)0);
                break;
			case WsSqlScaleEntity scale:
                scale.WorkShop = CreateNewSubstitute<WsSqlWorkShopEntity>(isNotDefault);
                scale.Number.Returns(10000);
                break;
			case WsSqlTaskEntity task:
				task.TaskType = CreateNewSubstitute<WsSqlTaskTypeEntity>(isNotDefault);
				task.Scale = CreateNewSubstitute<WsSqlScaleEntity>(isNotDefault);
				break;
			case WsSqlTemplateResourceEntity templateResource:
                templateResource.Type.Returns(WsLocaleCore.Sql.SqlItemFieldTemplateResourceType);
                templateResource.DataValue.Returns(new byte[] { 0x00 } );
                break;
			case WsSqlVersionEntity version:
				version.Version.Returns((short)1);
				version.ReleaseDt.Returns(DateTime.Now);
				break;
			case WsSqlWorkShopEntity workShop:
				workShop.ProductionSite = CreateNewSubstitute<WsSqlProductionSiteEntity>(isNotDefault);
				break;
		}
		return item;
	}

	public void TableBaseModelAssertEqualsNew<T>() where T : WsSqlEntityBase, new()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			T item = new();
			WsSqlEntityBase baseItem = new();
			// Act.
			bool itemEqualsNew = item.EqualsNew();
			bool baseEqualsNew = baseItem.EqualsNew();
			// Assert.
			Assert.AreEqual(baseEqualsNew, itemEqualsNew);
		});
	}

	public void FieldBaseModelAssertEqualsNew<T>() where T : WsSqlFieldBase, new()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			T item = new();
			WsSqlFieldBase baseItem = new();
			// Act.
			bool itemEqualsNew = item.EqualsNew();
			bool baseEqualsNew = baseItem.EqualsNew();
			// Assert.
			Assert.AreEqual(baseEqualsNew, itemEqualsNew);
		});
	}

	public void TableBaseModelAssertToString<T>() where T : WsSqlEntityBase, new()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			T item = new();
			WsSqlEntityBase baseItem = new();
			// Act.
			string itemString = item.ToString();
			string baseString = baseItem.ToString();
			TestContext.WriteLine($"{nameof(itemString)}: {itemString}");
			TestContext.WriteLine($"{nameof(baseString)}: {baseString}");
			// Assert.
			Assert.AreNotEqual(baseString, itemString);
		});
	}

	public void FieldBaseModelAssertToString<T>() where T : WsSqlFieldBase, new()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			T item = new();
			WsSqlFieldBase baseItem = new();
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
