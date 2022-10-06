// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Helpers;
using DataCore.Models;

namespace DataCoreTests.Helpers;

[TestFixture]
public class CollectionsHelperTests
{
    private CollectionsHelper Collections { get; set; } = CollectionsHelper.Instance;

    [Test]
    public void GetDriverFileName_AreEqual()
    {
        string actual = Collections.GetDriverFileName(WinVersionEnum.Win10x64);
        Assert.AreEqual("VCP_V1.5.0_Setup_W8_x64_64bits.exe", actual);
        TestContext.WriteLine();

        actual = Collections.GetDriverFileName(WinVersionEnum.Win10x32);
        Assert.AreEqual("VCP_V1.5.0_Setup_W8_x86_32bits.exe", actual);

        actual = Collections.GetDriverFileName(WinVersionEnum.Win7x64);
        Assert.AreEqual("VCP_V1.5.0_Setup_W7_x64_64bits.exe", actual);

        actual = Collections.GetDriverFileName(WinVersionEnum.Win7x32);
        Assert.AreEqual("VCP_V1.5.0_Setup_W7_x86_32bits.exe", actual);
    }
}
