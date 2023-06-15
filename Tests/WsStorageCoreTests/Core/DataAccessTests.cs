// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Common;

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
			List<WsSqlDeviceTypeFkModel> deviceTypeFks = WsTestsUtils.ContextManager.ContextList
                .GetListDevicesTypesFkFree(WsSqlEnumIsMarked.ShowAll, false, false);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }

	[Test]
	public void GetBusyHosts_Exec_DoesNotThrow()
	{
		WsTestsUtils.DataTests.AssertAction(() =>
		{
			List<WsSqlDeviceTypeFkModel> deviceTypeFks = WsTestsUtils.ContextManager.ContextList
                .GetListDevicesTypesFkFree(WsSqlEnumIsMarked.ShowAll, false, false);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
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
	        List<WsSqlDeviceTypeModel> deviceTypes = WsTestsUtils.ContextManager.ContextList
                .GetListDevicesTypes(WsSqlEnumIsMarked.ShowAll, false, false);
	        foreach (WsSqlDeviceTypeModel deviceType in deviceTypes)
            {
                if (!GetDeviceTypesEnums().Contains(deviceType.Name))
                    TestContext.WriteLine($"{deviceType.Name} is not registered at {nameof(GetDeviceTypesEnums)}!");
		        Assert.That(GetDeviceTypesEnums().Contains(deviceType.Name), Is.EqualTo(true));
	        }
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }

    [Test]
    public void GetItemDeviceType_Exec_DoesNotThrow()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
	        List<WsSqlDeviceTypeModel> deviceTypes = WsTestsUtils.ContextManager.ContextList
                .GetListDevicesTypes(WsSqlEnumIsMarked.ShowAll, false, false);
	        foreach (WsSqlDeviceTypeModel deviceType1 in deviceTypes)
	        {
		        WsSqlDeviceTypeModel deviceType2 = WsTestsUtils.DataTests.ContextManager.ContextItem.GetItemDeviceTypeNotNullable(deviceType1.Name);
		        Assert.That(deviceType2, Is.EqualTo(deviceType1));
	        }
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }

    [Test]
    public void GetListDevices_Exec_DoesNotThrow()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
	        List<WsSqlDeviceModel> devices = WsTestsUtils.ContextManager.ContextList
                .GetListDevices(WsSqlEnumIsMarked.ShowAll, false, false);
	        foreach (WsSqlDeviceModel device in devices)
	        {
		        TestContext.WriteLine(device);
		        //Assert.AreEqual(GetDeviceTypesEnums().Contains(deviceType.Name), true);
	        }
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }

    [Test]
    public void GetListDeviceScalesFks_Exec_DoesNotThrow()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
	        List<WsSqlDeviceModel> devices = WsTestsUtils.ContextManager.ContextList
                .GetListDevices(WsSqlEnumIsMarked.ShowAll, false, false);
	        foreach (WsSqlDeviceModel device in devices)
	        {
		        WsSqlDeviceScaleFkModel deviceScaleFks = WsTestsUtils.ContextManager.ContextItem.GetItemDeviceScaleFkNotNullable(device);
		        TestContext.WriteLine($"{nameof(deviceScaleFks)}: {deviceScaleFks}");
		        TestContext.WriteLine($"{nameof(deviceScaleFks.Scale)}: {deviceScaleFks.Scale}");
	        }
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }

    [Test]
    public void GetItemDeviceType2_Exec_DoesNotThrow()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
	        List<WsSqlDeviceTypeFkModel> deviceTypesFks = WsTestsUtils.ContextManager.ContextList
                .GetListDevicesTypesFks(WsSqlEnumIsMarked.ShowAll, false, false);
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
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }

    #endregion
}