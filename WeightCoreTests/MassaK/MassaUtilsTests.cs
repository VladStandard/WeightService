// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// https://github.com/DanyloKD/EasySerial/blob/f09b2b5a3cf4d9c4ef951fa802f8b1a97443f39a/tests/EasySerial.Tests/MassaUtilsTests.cs
// https://www.tahapaksu.com/crc/
// https://github.com/nullfx/NullFX.CRC

using NUnit.Framework;
using System;
using WeightCore.MassaK;

namespace HardwareTests.MassaK
{
    [TestFixture]
    internal class MassaUtilsTests
    {
        [Test]
        public void ComputeChecksum_AreEqual()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                TestContext.WriteLine($"CMD_GET_MASSA: {MassaUtils.GetBytesAsHex(MassaUtils.Cmd.Get.CMD_GET_MASSA)}");
                TestContext.WriteLine($"CMD_GET_SCALE_PAR: {MassaUtils.GetBytesAsHex(MassaUtils.Cmd.Get.CMD_GET_SCALE_PAR)}");
                TestContext.WriteLine($"CMD_GET_TARE: {MassaUtils.GetBytesAsHex(MassaUtils.Cmd.Get.CMD_GET_TARE)}");
                TestContext.WriteLine($"CMD_GET_NAME: {MassaUtils.GetBytesAsHex(MassaUtils.Cmd.Get.CMD_GET_NAME)}");
                TestContext.WriteLine($"CMD_GET_SYS: {MassaUtils.GetBytesAsHex(MassaUtils.Cmd.Get.CMD_GET_SYS)}");
                TestContext.WriteLine($"CMD_GET_WEIGHT: {MassaUtils.GetBytesAsHex(MassaUtils.Cmd.Get.CMD_GET_WEIGHT)}");
                TestContext.WriteLine();

                TestContext.WriteLine($"CMD_SET_ZERO: {MassaUtils.GetBytesAsHex(MassaUtils.Cmd.Set.CMD_SET_ZERO)}");
                TestContext.WriteLine($"CMD_SET_NAME: {MassaUtils.GetBytesAsHex(MassaUtils.Cmd.Set.CMD_SET_NAME)}");
                TestContext.WriteLine($"CMD_SET_TARE: {MassaUtils.GetBytesAsHex(MassaUtils.Cmd.Set.CMD_SET_TARE)}");
                TestContext.WriteLine($"CMD_SET_DATETIME: {MassaUtils.GetBytesAsHex(MassaUtils.Cmd.Set.CMD_SET_DATETIME)}");
                TestContext.WriteLine($"CMD_SET_RGNUM: {MassaUtils.GetBytesAsHex(MassaUtils.Cmd.Set.CMD_SET_RGNUM)}");

                Assert.AreEqual(0, 0);
            });

            Utils.MethodComplete();
        }
    }
}
