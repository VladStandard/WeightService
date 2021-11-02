// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;

namespace WeightCore.MassaK
{
    public class CmdEntity
    {
        #region Public and private fields and properties

        public int WeightTare { get; set; } = 0;
        public int ScaleFactor { get; set; } = 1_000;
        public CmdType CmdType { get; set; } = CmdType.Nack;
        public byte[] Request { get; set; } = null;
        public ResponseParseEntity ResponseParse { get; set; } = null;

        #endregion

        #region Constructor and destructor

        public CmdEntity(CmdType cmdType)
        {
            CmdType = cmdType;
        }

        public CmdEntity(CmdType cmdType, int weightTare, int scaleFactor = 1_000)
        {
            CmdType = cmdType;
            WeightTare = weightTare;
            ScaleFactor = scaleFactor;
        }

        #endregion

        #region Public and private methods

        private byte[] CrcRecalc(byte[] response, short skip = 5, short len = 1)
        {
            byte[] data = response.Skip(skip).Take(len).ToArray();
            if (len > 1)
                _ = data.Reverse();
            //ushort crc = MassaUtils.Crc16.GetChecksum(selected);
            ushort crc = NullFX.CRC.Crc16.ComputeChecksum(NullFX.CRC.Crc16Algorithm.Ccitt, data);
            response[response.Length - 2] = (byte)(crc >> 0x08 & 0xFF);
            response[response.Length - 1] = (byte)(crc & 0xFF);
            return response;
        }

        public byte[] CmdGetName() => CrcRecalc(MassaUtils.Cmd.Get.CMD_GET_NAME);

        public byte[] CmdSetTare()
        {
            byte[] data = MassaUtils.Cmd.Set.CMD_SET_TARE;

            data[6] = (byte)(WeightTare & 0xFF);
            data[7] = (byte)((byte)(WeightTare >> 0x08) & 0xFF);
            data[8] = (byte)((byte)(WeightTare >> 0x16) & 0xFF);
            data[9] = (byte)((byte)(WeightTare >> 0x32) & 0xFF);
            CmdSetaTareScaleFactor(data);
            CrcRecalc(data, 5, 9);
            //byte[] selected = data.Skip(5).Take(9).ToArray();
            //_ = selected.Reverse();
            //ushort crc = Crc16.ComputeChecksum(selected);
            //data[data.Length - 2] = (byte)(crc >> 0x08 & 0xFF);
            //data[data.Length - 1] = (byte)(crc & 0xFF);

            return data;
        }

        private void CmdSetaTareScaleFactor(byte[] data)
        {
            data[10] = ScaleFactor switch
            {
                10000 => 0x00,
                1000 => 0x01,
                100 => 0x02,
                10 => 0x03,
                1 => 0x04,
                _ => 0x01,
            };
        }

        public byte[] CmdSetName(string name = "xx")
        {
            byte[] data = new byte[MassaUtils.Cmd.Set.CMD_SET_NAME.Length + name.Length + 2];
            int k = 0;
            for (int i = 0; i < MassaUtils.Cmd.Set.CMD_SET_NAME.Length; i++)
            {
                data[i] = MassaUtils.Cmd.Set.CMD_SET_NAME[i];
                k++;
            }
            for (int i = 0; i < name.Length && i < 27; i++, k++)
            {
                data[k] = (byte)name.ToArray()[i];
                k++;
            }

            data[k++] = 0x00;
            data[k++] = 0x00;
            data[4] = (byte)(1 + name.Length);
            data[5] = 0x00;
            CrcRecalc(data, 6, (short)(1 + name.Length));
            //byte[] selected = data.Skip(6).Take(1 + name.Length).ToArray();
            //selected.Reverse();
            //ushort crc = Crc16.ComputeChecksum(selected);
            //data[data.Length - 2] = (byte)((crc >> 0x08) & 0xFF);
            //data[data.Length - 1] = (byte)(crc & 0xFF);
            return data;
        }

        public byte[] CmdTcpGetTare()
        {
            byte[] data = MassaUtils.Cmd.Get.CMD_GET_TARE;
            CrcRecalc(data);
            return data;
        }

        public byte[] CmdTcpSetRegnum(int Regnum)
        {
            byte[] data = MassaUtils.Cmd.Set.CMD_SET_RGNUM;
            data[7] = (byte)(Regnum & 0xFF);
            data[8] = (byte)((byte)(Regnum >> 0x08) & 0xFF);
            data[9] = (byte)((byte)(Regnum >> 0x16) & 0xFF);
            data[10] = (byte)((byte)(Regnum >> 0x32) & 0xFF);

            CrcRecalc(data, 5, 6);
            //byte[] selected = data.Skip(5).Take(6).ToArray();
            //selected.Reverse();
            //ushort crc = Crc16.ComputeChecksum(selected);
            //data[data.Length - 2] = (byte)((crc >> 0x08) & 0xFF);
            //data[data.Length - 1] = (byte)(crc & 0xFF);
            return data;
        }

        public byte[] CmdSetDatetime(DateTime dt)
        {
            byte[] data = MassaUtils.Cmd.Set.CMD_SET_DATETIME;
            data[7] = (byte)(dt.Year & 0xFF);
            data[8] = (byte)((byte)(dt.Month >> 0xFF) & 0xFF);
            data[9] = (byte)((byte)(dt.Day >> 0xFF) & 0xFF);
            data[10] = (byte)((byte)(dt.Hour >> 0xFF) & 0xFF);
            data[11] = (byte)((byte)(dt.Minute >> 0xFF) & 0xFF);
            data[12] = (byte)((byte)(dt.Second >> 0xFF) & 0xFF);

            return data;
        }

        public byte[] CmdGetSys()
        {
            byte[] data = MassaUtils.Cmd.Get.CMD_GET_SYS;
            CrcRecalc(data);
            return data;
        }

        public byte[] CmdGetWeight()
        {
            byte[] data = MassaUtils.Cmd.Get.CMD_GET_WEIGHT;
            CrcRecalc(data);
            return data;
        }

        //public void CmdResponseParse(byte[] data)
        //{
        //    byte header0 = data[0];
        //    byte header1 = data[1];
        //    byte header2 = data[2];
        //    short len = BitConverter.ToInt16(data.Skip(3).Take(2).ToArray(), 0);
        //    byte command = data[5];
        //    //short ScalesID = BitConverter.ToInt16(data.Skip(6).Take(2).ToArray(), 0);
        //    //byte[] selected = data.Skip(5).Take(len).ToArray();
        //    //selected.Reverse();
        //    //short crc = Crc16.ComputeChecksum(selected);
        //    short crc = BitConverter.ToInt16(data.Skip(6).Take(2).ToArray(), 0);
        //    //return (Int16)CRC == (Int16)crc;
        //}

        #endregion
    }
}
