// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsBlazorCore.CssStyles;
using WsBlazorCore.Utils;

namespace WsAssertCoreTests;

public class WsDataTestsHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsDataTestsHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsDataTestsHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    public WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    private WsJsonSettingsHelper JsonSettings => WsJsonSettingsHelper.Instance;

    #endregion

    #region Public and private methods

    public void SetupDevelopAleksandrov(bool isShowSql)
    {
        ContextManager.SetupJsonTestsDevelopAleksandrov(Directory.GetCurrentDirectory(),
            MdNetUtils.GetLocalDeviceName(true), nameof(WsAssertCoreTests), isShowSql);
        TestContext.WriteLine($"{nameof(JsonSettings.IsRemote)}: {JsonSettings.IsRemote}");
        TestContext.WriteLine(JsonSettings.IsRemote ? JsonSettings.Remote : JsonSettings.Local);
    }

    public void SetupDevelopMorozov(bool isShowSql)
    {
        ContextManager.SetupJsonTestsDevelopMorozov(Directory.GetCurrentDirectory(),
            MdNetUtils.GetLocalDeviceName(true), nameof(WsAssertCoreTests), isShowSql);
        TestContext.WriteLine($"{nameof(JsonSettings.IsRemote)}: {JsonSettings.IsRemote}");
        TestContext.WriteLine(JsonSettings.IsRemote ? JsonSettings.Remote : JsonSettings.Local);
    }

    public void SetupDevelopVs(bool isShowSql)
    {
        ContextManager.SetupJsonTestsDevelopVs(Directory.GetCurrentDirectory(),
            MdNetUtils.GetLocalDeviceName(true), nameof(WsAssertCoreTests), isShowSql);
        TestContext.WriteLine($"{nameof(JsonSettings.IsRemote)}: {JsonSettings.IsRemote}");
        TestContext.WriteLine(JsonSettings.IsRemote ? JsonSettings.Remote : JsonSettings.Local);
    }

    private void SetupReleaseAleksandrov(bool isShowSql)
    {
        ContextManager.SetupJsonTestsReleaseAleksandrov(Directory.GetCurrentDirectory(),
            MdNetUtils.GetLocalDeviceName(true), nameof(WsAssertCoreTests), isShowSql);
        TestContext.WriteLine($"{nameof(JsonSettings.IsRemote)}: {JsonSettings.IsRemote}");
        TestContext.WriteLine(JsonSettings.IsRemote ? JsonSettings.Remote : JsonSettings.Local);
    }

    private void SetupReleaseMorozov(bool isShowSql)
    {
        ContextManager.SetupJsonTestsReleaseMorozov(Directory.GetCurrentDirectory(),
            MdNetUtils.GetLocalDeviceName(true), nameof(WsAssertCoreTests), isShowSql);
        TestContext.WriteLine($"{nameof(JsonSettings.IsRemote)}: {JsonSettings.IsRemote}");
        TestContext.WriteLine(JsonSettings.IsRemote ? JsonSettings.Remote : JsonSettings.Local);
    }

    private void SetupReleaseVs(bool isShowSql)
    {
        ContextManager.SetupJsonTestsReleaseVs(Directory.GetCurrentDirectory(),
            MdNetUtils.GetLocalDeviceName(true), nameof(WsAssertCoreTests), isShowSql);
        TestContext.WriteLine($"{nameof(JsonSettings.IsRemote)}: {JsonSettings.IsRemote}");
        TestContext.WriteLine(JsonSettings.IsRemote ? JsonSettings.Remote : JsonSettings.Local);
    }

    public void AssertAction(Action action, bool isShowSql, List<WsConfiguration> publishTypes)
    {
        Assert.DoesNotThrow(() =>
        {
            if (publishTypes.Contains(WsConfiguration.DevelopAleksandrov))
            {
                SetupDevelopAleksandrov(isShowSql);
                action();
                TestContext.WriteLine();
            }
            if (publishTypes.Contains(WsConfiguration.DevelopMorozov))
            {
                SetupDevelopMorozov(isShowSql);
                action();
                TestContext.WriteLine();
            }
            if (publishTypes.Contains(WsConfiguration.DevelopVS))
            {
                SetupDevelopVs(isShowSql);
                action();
                TestContext.WriteLine();
            }
            if (publishTypes.Contains(WsConfiguration.ReleaseAleksandrov))
            {
                SetupReleaseAleksandrov(isShowSql);
                action();
                TestContext.WriteLine();
            }
            if (publishTypes.Contains(WsConfiguration.ReleaseMorozov))
            {
                SetupReleaseMorozov(isShowSql);
                action();
                TestContext.WriteLine();
            }
            if (publishTypes.Contains(WsConfiguration.ReleaseVS))
            {
                SetupReleaseVs(isShowSql);
                action();
                TestContext.WriteLine();
            }
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

    public void AssertSqlDbContentValidate<T>(bool isShowMarked = false) where T : WsSqlTableBase, new()
    {
        AssertAction(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfigSection(isShowMarked);
            List<T> items = ContextManager.ContextList.GetListNotNullable<T>(sqlCrudConfig);
            Assert.IsTrue(items.Any());
            PrintTopRecords(items, 5, true);
        }, false, new() { WsConfiguration.DevelopVS, WsConfiguration.ReleaseVS });
    }

    public void AssertSqlValidate<T>(T item, bool assertResult) where T : WsSqlTableBase, new() =>
        AssertSqlTablesValidate(item, assertResult);

    public void AssertSqlDbContentSerialize<T>(bool isShowMarked = false) where T : WsSqlTableBase, new()
    {
        AssertAction(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfigSection(isShowMarked);
            List<T> items = ContextManager.ContextList.GetListNotNullable<T>(sqlCrudConfig);
            Assert.IsTrue(items.Any());
            PrintTopRecords(items, 10, true, true);
        }, false, new() { WsConfiguration.DevelopVS, WsConfiguration.ReleaseVS });
    }

    public void AssertSqlTablesValidate<T>(T item, bool assertResult) where T : class, new()
    {
        Assert.DoesNotThrow(() =>
        {
            ValidationResult validationResult = WsSqlValidationUtils.GetValidationResult(item);
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

    public void AssertBlazorCssStylesValidate<T>(T item, bool assertResult) where T : CssStyleBase, new()
    {
        Assert.DoesNotThrow(() =>
        {
            ValidationResult validationResult = WsBlazorCssValidationUtils.GetValidationResult(item);
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

    public object? GetSqlPropertyValue<T>(bool isNotDefault, string propertyName) where T : WsSqlTableBase, new()
    {
        // Arrange
        T item = CreateNewSubstitute<T>(isNotDefault);
        // Act.
        object? value = item.GetPropertyAsObject(propertyName);
        TestContext.WriteLine($"{typeof(T)}. {propertyName}: {value}");
        return value;
    }

    public void AssertSqlPropertyCheckDt<T>(string propertyName) where T : WsSqlTableBase, new()
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

    public void AssertSqlPropertyCheckBool<T>(string propertyName) where T : WsSqlTableBase, new()
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

    public void AssertSqlPropertyCheckString<T>(string propertyName) where T : WsSqlTableBase, new()
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

    public void AssertGetList<T>(SqlCrudConfigModel sqlCrudConfig, List<WsConfiguration> publishTypes, bool isGreater = true)
        where T : WsSqlTableBase, new()
    {
        AssertAction(() =>
        {
            List<T> items = ContextManager.ContextList.GetListNotNullable<T>(sqlCrudConfig);
            TestContext.WriteLine($"{nameof(items.Count)}: {items.Count}");
            if (isGreater)
                Assert.Greater(items.Count, 0);
            foreach (T item in items)
            {
                Assert.IsNotEmpty(item.ToString());
                ValidationResult validationResult = WsSqlValidationUtils.GetValidationResult(item);
                Assert.IsTrue(validationResult.IsValid);
            }
        }, false, publishTypes);
    }

    public T CreateNewSubstitute<T>(bool isNotDefault) where T : WsSqlTableBase, new()
    {
        SqlFieldIdentityModel fieldIdentity = Substitute.For<SqlFieldIdentityModel>(WsSqlFieldIdentity.Empty);
        fieldIdentity.Name.Returns(WsSqlFieldIdentity.Test);
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
            case WsSqlAccessModel access:
                access.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
                access.Rights.Returns((byte)AccessRightsEnum.None);
                access.LoginDt.Returns(DateTime.Now);
                break;
            case WsSqlAppModel app:
                app.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
                break;
            case BarCodeModel barCode:
                barCode.TypeTop.Returns(BarcodeType.Default.ToString());
                barCode.ValueTop.Returns(LocaleCore.Sql.SqlItemFieldValue);
                barCode.TypeRight.Returns(BarcodeType.Default.ToString());
                barCode.ValueRight.Returns(LocaleCore.Sql.SqlItemFieldValue);
                barCode.TypeBottom.Returns(BarcodeType.Default.ToString());
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
                logWebFk.App = CreateNewSubstitute<WsSqlAppModel>(isNotDefault);
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
                nomenclatureCharacteristicFk.Plu = CreateNewSubstitute<WsSqlPluModel>(isNotDefault);
                nomenclatureCharacteristicFk.Characteristic = CreateNewSubstitute<PluCharacteristicModel>(isNotDefault);
                break;
            case PluFkModel pluFk:
                pluFk.Plu = CreateNewSubstitute<WsSqlPluModel>(isNotDefault);
                pluFk.Parent = CreateNewSubstitute<WsSqlPluModel>(isNotDefault);
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
            case WsSqlPluModel plu:
                plu.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
                plu.Number.Returns((short)100);
                plu.FullName.Returns(LocaleCore.Sql.SqlItemFieldFullName);
                plu.Gtin.Returns(LocaleCore.Sql.SqlItemFieldGtin);
                plu.Ean13.Returns(LocaleCore.Sql.SqlItemFieldEan13);
                plu.Itf14.Returns(LocaleCore.Sql.SqlItemFieldItf14);
                plu.Code.Returns(LocaleCore.Sql.SqlItemFieldCode);
                break;
            case PluBundleFkModel pluBundle:
                pluBundle.Plu = CreateNewSubstitute<WsSqlPluModel>(isNotDefault);
                pluBundle.Bundle = CreateNewSubstitute<BundleModel>(isNotDefault);
                break;
            case PluClipFkModel pluClips:
                pluClips.Plu = CreateNewSubstitute<WsSqlPluModel>(isNotDefault);
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
                pluScale.Plu = CreateNewSubstitute<WsSqlPluModel>(isNotDefault);
                pluScale.Scale = CreateNewSubstitute<ScaleModel>(isNotDefault);
                break;
            case PluStorageMethodModel pluStorageMethod:
                pluStorageMethod.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
                pluStorageMethod.MinTemp.Returns((short)0);
                pluStorageMethod.MaxTemp.Returns((short)0);
                break;
            case PluStorageMethodFkModel pluStorageMethodFk:
                pluStorageMethodFk.Plu = CreateNewSubstitute<WsSqlPluModel>(isNotDefault);
                pluStorageMethodFk.Method = CreateNewSubstitute<PluStorageMethodModel>(isNotDefault);
                pluStorageMethodFk.Resource = CreateNewSubstitute<TemplateResourceModel>(isNotDefault);
                break;
            case PluTemplateFkModel pluTemplateFk:
                pluTemplateFk.Plu = CreateNewSubstitute<WsSqlPluModel>(isNotDefault);
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
            case PrinterResourceFkModel printerResource:
                printerResource.Printer = CreateNewSubstitute<PrinterModel>(isNotDefault);
                printerResource.TemplateResource = CreateNewSubstitute<TemplateResourceModel>(isNotDefault);
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
            case TemplateResourceModel templateResource:
                templateResource.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
                templateResource.Type.Returns(LocaleCore.Sql.SqlItemFieldTemplateResourceType);
                templateResource.DataValue.Returns(new byte[] { 0x00 });
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

    public void TableBaseModelAssertEqualsNew<T>() where T : WsSqlTableBase, new()
    {
        Assert.DoesNotThrow(() =>
        {
            // Arrange.
            T item = new();
            WsSqlTableBase baseItem = new();
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

    public void TableBaseModelAssertSerialize<T>() where T : WsSqlTableBase, new()
    {
        Assert.DoesNotThrow(() =>
        {
            // Arrange.
            T item1 = new();
            WsSqlTableBase base1 = new();
            // Act.
            string xml1 = WsDataFormatUtils.SerializeAsXmlString<T>(item1, true, true);
            string xml2 = WsDataFormatUtils.SerializeAsXmlString<WsSqlTableBase>(base1, true, true);
            // Assert.
            Assert.AreNotEqual(xml1, xml2);
            // Act.
            T item2 = WsDataFormatUtils.DeserializeFromXml<T>(xml1);
            TestContext.WriteLine($"{nameof(item2)}: {item2}");
            WsSqlTableBase base2 = WsDataFormatUtils.DeserializeFromXml<WsSqlTableBase>(xml2);
            TestContext.WriteLine($"{nameof(base2)}: {base2}");
            // Assert.
            Assert.AreNotEqual(item2, base2);
        });
    }

    public void TableBaseModelAssertToString<T>() where T : WsSqlTableBase, new()
    {
        Assert.DoesNotThrow(() =>
        {
            // Arrange.
            T item = new();
            WsSqlTableBase baseItem = new();
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

    public void PrintTopRecords<T>(List<T> items, ushort count = 0, bool isValidate = false, bool isSerialize = false) where T : class, new()
    {
        checked
        {
            if (Equals(count, (ushort)0))
                count = (ushort)items.Count;
            TestContext.WriteLine($"Print top {count} from {items.Count} records.");
            int i = 0;
            foreach (T item in items)
            {
                if (i < count)
                {
                    TestContext.WriteLine(item);
                    if (isValidate)
                    {
                        AssertSqlTablesValidate(item, true);
                        ValidationResult validationResult = WsSqlValidationUtils.GetValidationResult(item);
                        FailureWriteLine(validationResult);
                        Assert.IsTrue(validationResult.IsValid);
                    }
                    if (isSerialize && item is SerializeBase sitem)
                    {
                        string xml = WsDataFormatUtils.SerializeAsXmlString<T>(sitem, true, false);
                        Assert.IsNotEmpty(xml);
                    }
                }
                else break;
                i++;
            }
        }
    }

    #endregion
}
