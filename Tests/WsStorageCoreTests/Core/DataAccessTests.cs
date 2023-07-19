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
			List<WsSqlDeviceTypeFkModel> deviceTypeFks = WsTestsUtils.ContextManager.ContextList
                .GetListDevicesTypesFkFree(WsSqlEnumIsMarked.ShowAll, false);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }

	[Test]
	public void GetBusyHosts_Exec_DoesNotThrow()
	{
		WsTestsUtils.DataTests.AssertAction(() =>
		{
			List<WsSqlDeviceTypeFkModel> deviceTypeFks = WsTestsUtils.ContextManager.ContextList
                .GetListDevicesTypesFkFree(WsSqlEnumIsMarked.ShowAll, false);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }

    [Test]
    public void GetListDeviceScalesFks_Exec_DoesNotThrow()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
	        List<WsSqlDeviceModel> devices = WsTestsUtils.ContextManager.ContextList
                .GetListDevices(WsSqlEnumIsMarked.ShowAll, false);
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
                .GetListDevicesTypesFks(WsSqlEnumIsMarked.ShowAll, false);
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