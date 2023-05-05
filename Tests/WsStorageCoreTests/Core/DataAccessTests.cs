// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Core;

[TestFixture]
public sealed class DataAccessTests
{
	#region Public and private methods

	[Test]
	public void GetFreeHosts_Exec_DoesNotThrow()
	{
		WsTestsUtils.DataTests.AssertAction(() =>
		{
			foreach (bool isMarked in DataCoreEnums.GetBool())
			{
				List<WsSqlDeviceTypeFkModel> deviceTypeFks = WsTestsUtils.ContextManager.ContextList.GetListDevicesTypesFkFree(isMarked, false, false);
			}
        }, false, new() { WsConfiguration.DevelopVS, WsConfiguration.ReleaseVS });
    }

	[Test]
	public void GetBusyHosts_Exec_DoesNotThrow()
	{
		WsTestsUtils.DataTests.AssertAction(() =>
		{
			foreach (bool isMarked in DataCoreEnums.GetBool())
			{
				List<WsSqlDeviceTypeFkModel> deviceTypeFks = WsTestsUtils.ContextManager.ContextList.GetListDevicesTypesFkFree(isMarked, false, false);
			}
        }, false, new() { WsConfiguration.DevelopVS, WsConfiguration.ReleaseVS });
    }

    private IEnumerable<string> GetDeviceTypesEnums() => 
		new List<string>() {
            "Monoblock",
            "Print TSC",
            "Print Zebra",
            "Scale Massa-K",
            "SQL-Server",
            "Web-Server",
        };

    [Test]
    public void GetListDevicesTypes_Exec_DoesNotThrow()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
	        List<DeviceTypeModel> deviceTypes = WsTestsUtils.ContextManager.ContextList.GetListDevicesTypes(true, false, false);
	        foreach (DeviceTypeModel deviceType in deviceTypes)
	        {
		        Assert.That(GetDeviceTypesEnums().Contains(deviceType.Name), Is.EqualTo(true));
	        }
        }, false, new() { WsConfiguration.DevelopVS, WsConfiguration.ReleaseVS });
    }

    [Test]
    public void GetItemDeviceType_Exec_DoesNotThrow()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
	        List<DeviceTypeModel> deviceTypes = WsTestsUtils.ContextManager.ContextList.GetListDevicesTypes(true, false, false);
	        foreach (DeviceTypeModel deviceType1 in deviceTypes)
	        {
		        DeviceTypeModel deviceType2 = WsTestsUtils.DataTests.ContextManager.ContextItem.GetItemDeviceTypeNotNullable(deviceType1.Name);
		        Assert.That(deviceType2, Is.EqualTo(deviceType1));
	        }
        }, false, new() { WsConfiguration.DevelopVS, WsConfiguration.ReleaseVS });
    }

    [Test]
    public void GetListDevices_Exec_DoesNotThrow()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
	        List<DeviceModel> devices = WsTestsUtils.ContextManager.ContextList.GetListDevices(true, false, false);
	        foreach (DeviceModel device in devices)
	        {
		        TestContext.WriteLine(device);
		        //Assert.AreEqual(GetDeviceTypesEnums().Contains(deviceType.Name), true);
	        }
        }, false, new() { WsConfiguration.DevelopVS, WsConfiguration.ReleaseVS });
    }

    [Test]
    public void GetListDeviceScalesFks_Exec_DoesNotThrow()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
	        List<DeviceModel> devices = WsTestsUtils.ContextManager.ContextList.GetListDevices(true, false, false);
	        foreach (DeviceModel device in devices)
	        {
		        WsSqlDeviceScaleFkModel deviceScaleFks = WsTestsUtils.ContextManager.ContextItem.GetItemDeviceScaleFkNotNullable(device);
		        TestContext.WriteLine($"{nameof(deviceScaleFks)}: {deviceScaleFks}");
		        TestContext.WriteLine($"{nameof(deviceScaleFks.Scale)}: {deviceScaleFks.Scale}");
	        }
        }, false, new() { WsConfiguration.DevelopVS, WsConfiguration.ReleaseVS });
    }

    [Test]
    public void GetItemDeviceType2_Exec_DoesNotThrow()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
	        List<WsSqlDeviceTypeFkModel> deviceTypesFks = WsTestsUtils.ContextManager.ContextList.GetListDevicesTypesFks(true, false, false);
	        foreach (WsSqlDeviceTypeFkModel deviceTypeFk in deviceTypesFks)
	        {
		        if (deviceTypeFk.Device.IsNotNew)
		        {
			        WsSqlDeviceScaleFkModel deviceScaleFks = WsTestsUtils.ContextManager.ContextItem.GetItemDeviceScaleFkNotNullable(deviceTypeFk.Device);
			        if (deviceTypeFk.Device.Name.Equals("SCALES-MON-101"))
			        {
				        TestContext.WriteLine($"{nameof(deviceTypeFk.Device)}: {deviceTypeFk.Device}");
				        TestContext.WriteLine($"{nameof(deviceScaleFks)}: {deviceScaleFks}");
				        TestContext.WriteLine($"{nameof(deviceScaleFks.Scale)}: {deviceScaleFks.Scale}");
			        }
		        }
	        }
        }, false, new() { WsConfiguration.DevelopVS, WsConfiguration.ReleaseVS });
    }

    #endregion
}