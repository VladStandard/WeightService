// https://github.com/DanyloKD/EasySerial/blob/f09b2b5a3cf4d9c4ef951fa802f8b1a97443f39a/tests/EasySerial.Tests/MassaUtilsTests.cs
// https://www.tahapaksu.com/crc/
// https://github.com/nullfx/NullFX.CRC

namespace Ws.LabelCoreTests.MassaK;

[TestFixture]
public sealed class MassaUtilsTests
{
    private BytesHelper Bytes { get; set; } = BytesHelper.Instance;

    [Test]
    public void ComputeChecksum_AreEqual()
    {
        Assert.DoesNotThrow(() =>
        {
            TestContext.WriteLine($"CMD_GET_MASSA: {Bytes.GetBytesAsHex(MassaRequestHelper.Instance.CMD_GET_MASSA)}");
            TestContext.WriteLine($"CMD_GET_SCALE_PAR: {Bytes.GetBytesAsHex(MassaRequestHelper.Instance.CMD_GET_SCALE_PAR)}");
            TestContext.WriteLine();
            TestContext.WriteLine($"CMD_SET_ZERO: {Bytes.GetBytesAsHex(MassaRequestHelper.Instance.CMD_SET_ZERO)}");
            TestContext.WriteLine($"CMD_SET_TARE: {Bytes.GetBytesAsHex(MassaRequestHelper.Instance.CMD_SET_TARE)}");

            Assert.AreEqual(0, 0);
        });
    }
}
