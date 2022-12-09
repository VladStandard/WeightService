// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql.Core;

[TestFixture]
internal class DataAcessTests
{
	#region Public and private fields, properties, constructor

	private static DataCoreHelper DataCore => DataCoreHelper.Instance;

	#endregion

	#region Public and private methods

	[Test]
	public void GetFreeHosts_Exec_DoesNotThrow()
	{
		DataCore.AssertAction(() =>
		{
			foreach (bool isMarked in DataCoreEnums.GetBool())
			{
				List<DeviceTypeFkModel> deviceTypeFks = DataCore.DataAccess.GetListDevicesTypesFkFree(isMarked, false, false);
			}
		});
	}

	[Test]
	public void GetBusyHosts_Exec_DoesNotThrow()
	{
		DataCore.AssertAction(() =>
		{
			foreach (bool isMarked in DataCoreEnums.GetBool())
			{
				List<DeviceTypeFkModel> deviceTypeFks = DataCore.DataAccess.GetListDevicesTypesFkFree(isMarked, false, false);
			}
		});
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
        DataCore.AssertAction(() =>
        {
            List<DeviceTypeModel> deviceTypes = DataCore.DataAccess.GetListDevicesTypes(true, false, false);
			foreach (DeviceTypeModel deviceType in deviceTypes)
			{
				Assert.AreEqual(GetDeviceTypesEnums().Contains(deviceType.Name), true);
			}
        });
    }

    [Test]
    public void GetItemDeviceType_Exec_DoesNotThrow()
    {
        DataCore.AssertAction(() =>
        {
            List<DeviceTypeModel> deviceTypes = DataCore.DataAccess.GetListDevicesTypes(true, false, false);
			foreach (DeviceTypeModel deviceType1 in deviceTypes)
			{
				DeviceTypeModel deviceType2 = DataCore.DataAccess.GetItemDeviceTypeNotNullable(deviceType1.Name);
                Assert.AreEqual(deviceType1, deviceType2);
			}
        });
    }

    [Test]
    public void GetListDevices_Exec_DoesNotThrow()
    {
        DataCore.AssertAction(() =>
        {
            List<DeviceModel> devices = DataCore.DataAccess.GetListDevices(true, false, false);
            foreach (DeviceModel device in devices)
            {
                TestContext.WriteLine(device);
                //Assert.AreEqual(GetDeviceTypesEnums().Contains(deviceType.Name), true);
            }
        });
    }

    [Test]
    public void GetListDeviceScalesFks_Exec_DoesNotThrow()
    {
        DataCore.AssertAction(() =>
        {
            List<DeviceModel> devices = DataCore.DataAccess.GetListDevices(true, false, false);
            foreach (DeviceModel device in devices)
            {
                DeviceScaleFkModel deviceScaleFks = DataCore.DataAccess.GetItemDeviceScaleFkNotNullable(device);
                TestContext.WriteLine($"{nameof(deviceScaleFks)}: {deviceScaleFks}");
                TestContext.WriteLine($"{nameof(deviceScaleFks.Scale)}: {deviceScaleFks.Scale}");
            }
        });
    }

    [Test]
    public void GetItemDeviceType2_Exec_DoesNotThrow()
    {
        DataCore.AssertAction(() =>
        {
            List<DeviceTypeFkModel> deviceTypesFks = DataCore.DataAccess.GetListDevicesTypesFks(true, false, false);
            foreach (DeviceTypeFkModel deviceTypeFk in deviceTypesFks)
            {
                if (deviceTypeFk.Device.IdentityIsNotNew)
                {
                    DeviceScaleFkModel deviceScaleFks = DataCore.DataAccess.GetItemDeviceScaleFkNotNullable(deviceTypeFk.Device);
                    if (deviceTypeFk.Device.Name.Equals("SCALES-MON-101"))
                    {
                        TestContext.WriteLine($"{nameof(deviceTypeFk.Device)}: {deviceTypeFk.Device}");
                        TestContext.WriteLine($"{nameof(deviceScaleFks)}: {deviceScaleFks}");
                        TestContext.WriteLine($"{nameof(deviceScaleFks.Scale)}: {deviceScaleFks.Scale}");
                    }
                }
            }
        });
    }

    #endregion
}
