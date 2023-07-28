namespace WsDataCoreTests.Helpers;

[TestFixture]
public sealed class CollectionsHelperTests
{
    private WsCollectionsHelper Collections => WsCollectionsHelper.Instance;

    [Test]
    public void GetDriverFileName_AreEqual()
    {
        string actual = Collections.GetDriverFileName(WsEnumWinVersion.Win10x64);
        Assert.AreEqual("VCP_V1.5.0_Setup_W8_x64_64bits.exe", actual);
        TestContext.WriteLine();

        actual = Collections.GetDriverFileName(WsEnumWinVersion.Win10x32);
        Assert.AreEqual("VCP_V1.5.0_Setup_W8_x86_32bits.exe", actual);

        actual = Collections.GetDriverFileName(WsEnumWinVersion.Win7x64);
        Assert.AreEqual("VCP_V1.5.0_Setup_W7_x64_64bits.exe", actual);

        actual = Collections.GetDriverFileName(WsEnumWinVersion.Win7x32);
        Assert.AreEqual("VCP_V1.5.0_Setup_W7_x86_32bits.exe", actual);
    }
}
