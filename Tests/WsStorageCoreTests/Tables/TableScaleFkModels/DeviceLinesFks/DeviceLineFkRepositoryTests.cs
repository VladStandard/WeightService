using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.DeviceLinesFks;

public sealed class DeviceLineFkRepositoryTests : TableRepositoryTests
{
    private WsSqlDeviceLineFkRepository DeviceLineFkRepository { get; set; } = new();
    
    private  List<WsSqlDeviceScaleFkModel> Buffer { get; set; }

    public DeviceLineFkRepositoryTests(): base()
    {
        Buffer = new();
    }
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlDeviceScaleFkModel> items = DeviceLineFkRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
    
    [Test]
    public void GetItemByDevice()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlDeviceModel> devices = WsSqlDeviceRepository.Instance.GetList(SqlCrudConfig);
            foreach (WsSqlDeviceModel device in devices)
            {
                WsSqlDeviceScaleFkModel deviceScaleFks = DeviceLineFkRepository.GetItemByDevice(device);
                if (!deviceScaleFks.IsNotNew)
                    continue;
                Assert.That(deviceScaleFks.Device.Name, Is.EqualTo(device.Name));
                Buffer.Add(deviceScaleFks);
                TestContext.WriteLine(deviceScaleFks);
            }
            Assert.That(Buffer.Any(), Is.True);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }

    [Test]
    public void GetItemByLine()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlScaleModel> lines = WsSqlLineRepository.Instance.GetList(SqlCrudConfig);
            foreach (WsSqlScaleModel line in lines)
            {
                WsSqlDeviceScaleFkModel deviceScaleFks = DeviceLineFkRepository.GetItemByLine(line);
                if (!deviceScaleFks.IsNotNew)
                    continue;
                Assert.That(deviceScaleFks.Scale.Description, Is.EqualTo(line.Description));
                Buffer.Add(deviceScaleFks);
                TestContext.WriteLine(deviceScaleFks);
            }
            Assert.That(Buffer.Any(), Is.True);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}