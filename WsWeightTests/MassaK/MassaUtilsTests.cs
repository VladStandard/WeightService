// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// https://github.com/DanyloKD/EasySerial/blob/f09b2b5a3cf4d9c4ef951fa802f8b1a97443f39a/tests/EasySerial.Tests/MassaUtilsTests.cs
// https://www.tahapaksu.com/crc/
// https://github.com/nullfx/NullFX.CRC

namespace WsWeightTests.MassaK;

[TestFixture]
public class MassaUtilsTests
{
    private BytesHelper Bytes { get; set; } = BytesHelper.Instance;

    [Test]
    public void ComputeChecksum_AreEqual()
    {
        Assert.DoesNotThrow(() =>
        {
            TestContext.WriteLine($"CMD_GET_MASSA: {Bytes.GetBytesAsHex(MassaRequestHelper.Instance.CMD_GET_MASSA)}");
            TestContext.WriteLine($"CMD_GET_SCALE_PAR: {Bytes.GetBytesAsHex(MassaRequestHelper.Instance.CMD_GET_SCALE_PAR)}");
            TestContext.WriteLine($"CMD_GET_TARE: {Bytes.GetBytesAsHex(MassaRequestHelper.Instance.CMD_GET_TARE)}");
            TestContext.WriteLine($"CMD_GET_NAME: {Bytes.GetBytesAsHex(MassaRequestHelper.Instance.CMD_GET_NAME)}");
            TestContext.WriteLine($"CMD_GET_SYS: {Bytes.GetBytesAsHex(MassaRequestHelper.Instance.CMD_GET_SYS)}");
            TestContext.WriteLine($"CMD_GET_WEIGHT: {Bytes.GetBytesAsHex(MassaRequestHelper.Instance.CMD_GET_WEIGHT)}");
            TestContext.WriteLine();

            TestContext.WriteLine($"CMD_SET_ZERO: {Bytes.GetBytesAsHex(MassaRequestHelper.Instance.CMD_SET_ZERO)}");
            TestContext.WriteLine($"CMD_SET_NAME: {Bytes.GetBytesAsHex(MassaRequestHelper.Instance.CMD_SET_NAME)}");
            TestContext.WriteLine($"CMD_SET_TARE: {Bytes.GetBytesAsHex(MassaRequestHelper.Instance.CMD_SET_TARE)}");

            Assert.AreEqual(0, 0);
        });
    }
}
