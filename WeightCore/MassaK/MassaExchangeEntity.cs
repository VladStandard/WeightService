// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Linq;
using static WeightCore.MassaK.MassaEnums;

namespace WeightCore.MassaK
{
    public class MassaExchangeEntity
    {
        #region Public and private fields and properties

        private MassaRequestHelper MassaRequest { get; set; } = MassaRequestHelper.Instance;
        public byte[] Request { get; set; } = null;
        public int ScaleFactor { get; set; } = 1_000;
        public int WeightTare { get; set; } = 0;
        public MassaCmdType CmdType { get; set; } = MassaCmdType.Nack;
        public ResponseParseEntity ResponseParse { get; set; }

        #endregion

        #region Constructor and destructor

        public MassaExchangeEntity(MassaCmdType cmdType)
        {
            CmdType = cmdType;
            ResponseParse = new ResponseParseEntity(CmdType, new byte[0]);
        }

        public MassaExchangeEntity(MassaCmdType cmdType, int weightTare, int scaleFactor = 1_000)
        {
            CmdType = cmdType;
            WeightTare = weightTare;
            ScaleFactor = scaleFactor;
        }

        #endregion

        #region Public and private methods

        public byte[] CmdSetTare()
        {
            byte[] request = MassaRequest.CMD_SET_TARE;
            request[6] = (byte)(WeightTare & 0xFF);
            request[7] = (byte)((byte)(WeightTare >> 0x08) & 0xFF);
            request[8] = (byte)((byte)(WeightTare >> 0x16) & 0xFF);
            request[9] = (byte)((byte)(WeightTare >> 0x32) & 0xFF);
            CmdSetaTareScaleFactor(request);
            MassaRequest.MakeRequestCrcRecalc(request);
            return request;
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
            byte[] request = new byte[MassaRequest.CMD_SET_NAME.Length + name.Length + 2];
            int k = 0;
            for (int i = 0; i < MassaRequest.CMD_SET_NAME.Length; i++)
            {
                request[i] = MassaRequest.CMD_SET_NAME[i];
                k++;
            }
            for (int i = 0; i < name.Length && i < 27; i++, k++)
            {
                request[k] = (byte)name.ToArray()[i];
                k++;
            }
            request[k++] = 0x00;
            request[k++] = 0x00;
            request[4] = (byte)(1 + name.Length);
            request[5] = 0x00;
            MassaRequest.MakeRequestCrcRecalc(request);
            return request;
        }

        //public byte[] CmdTcpSetRegnum(int Regnum)
        //{
        //    byte[] request = _massaRequest.CMD_SET_RGNUM;
        //    request[7] = (byte)(Regnum & 0xFF);
        //    request[8] = (byte)((byte)(Regnum >> 0x08) & 0xFF);
        //    request[9] = (byte)((byte)(Regnum >> 0x16) & 0xFF);
        //    request[10] = (byte)((byte)(Regnum >> 0x32) & 0xFF);
        //    _massaRequest.CrcRecalc(request);
        //    return request;
        //}

        //public byte[] CmdSetDatetime(DateTime dt)
        //{
        //    byte[] data = _massaRequest.CMD_SET_DATETIME;
        //    data[7] = (byte)(dt.Year & 0xFF);
        //    data[8] = (byte)((byte)(dt.Month >> 0xFF) & 0xFF);
        //    data[9] = (byte)((byte)(dt.Day >> 0xFF) & 0xFF);
        //    data[10] = (byte)((byte)(dt.Hour >> 0xFF) & 0xFF);
        //    data[11] = (byte)((byte)(dt.Minute >> 0xFF) & 0xFF);
        //    data[12] = (byte)((byte)(dt.Second >> 0xFF) & 0xFF);
        //    return data;
        //}

        #endregion
    }
}
