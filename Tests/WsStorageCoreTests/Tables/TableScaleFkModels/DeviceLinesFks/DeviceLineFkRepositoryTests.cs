using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.DeviceLinesFks;

public sealed class DeviceLineFkRepositoryTests : TableRepositoryTests
{
    private WsSqlDeviceLineFkRepository DeviceLineFkRepository { get; set; } = new();
    
    private WsSqlDeviceScaleFkModel GetFirstDeviceScaleFkModel()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        return DeviceLineFkRepository.GetList(SqlCrudConfig).First();
    }
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlDeviceScaleFkModel> items = DeviceLineFkRepository.GetList(SqlCrudConfig);
            WsTestsUtils.DataTests.ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
    
    [Test]
    public void GetItemByDevice()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlDeviceScaleFkModel oldDeviceLines = GetFirstDeviceScaleFkModel();
            WsSqlDeviceModel device = oldDeviceLines.Device;
            WsSqlDeviceScaleFkModel deviceLinesByDevice  = DeviceLineFkRepository.GetItemByDevice(device);

            Assert.That(deviceLinesByDevice.IsNotNew, Is.True);
            Assert.That(deviceLinesByDevice, Is.EqualTo(oldDeviceLines));
            
            TestContext.WriteLine(deviceLinesByDevice);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
    
    [Test]
    public void GetItemByLine()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlDeviceScaleFkModel oldDeviceLines = GetFirstDeviceScaleFkModel();
            WsSqlScaleModel line = oldDeviceLines.Scale;
            WsSqlDeviceScaleFkModel deviceLinesByLine  = DeviceLineFkRepository.GetItemByLine(line);

            Assert.That(deviceLinesByLine.IsNotNew, Is.True);
            Assert.That(deviceLinesByLine, Is.EqualTo(oldDeviceLines));
            
            TestContext.WriteLine(deviceLinesByLine);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}