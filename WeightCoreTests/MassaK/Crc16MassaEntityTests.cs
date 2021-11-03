// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// https://github.com/DanyloKD/EasySerial/blob/f09b2b5a3cf4d9c4ef951fa802f8b1a97443f39a/tests/EasySerial.Tests/Crc16MassaEntityTests.cs
// https://www.tahapaksu.com/crc/
// https://github.com/nullfx/NullFX.CRC

using NUnit.Framework;
using System;
using System.Linq;
using WeightCore.MassaK;

namespace HardwareTests.MassaK
{
    [TestFixture]
    internal class Crc16MassaEntityTests
    {
        // WRITE    F8 55 CE 01 00 23 23 00
        private byte[] getMassaRequest = MassaRequestHelper.Instance.CMD_GET_MASSA;
        // READ     F8 55 CE 0D 00 24 00 00 00 00 01 01 00 01 00 00 00 00 FC 23
        private byte[] getMassaResponse = new byte[] { 0x24, 0x00, 0x00, 0x00, 0x00, 0x01, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00 };

        [Test]
        public void ComputeChecksum_AreEqual()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                byte[] data = getMassaRequest;
                ushort crc = new Crc16MassaEntity().ComputeChecksum(data);
                Assert.AreEqual(crc, 0x2300);
                
                crc = new Crc16MassaEntity().ComputeChecksum(data, false);
                Assert.AreEqual(crc, 0x0023);

                data = getMassaResponse;
                crc = new Crc16MassaEntity().ComputeChecksum(data);
                Assert.AreEqual(crc, 0xFC23);
            });

            Utils.MethodComplete();
        }
    }
}
