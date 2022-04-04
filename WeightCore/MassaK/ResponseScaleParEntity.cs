// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.IO;

namespace WeightCore.MassaK
{
    public class ResponseScaleParEntity
    {
        //Максимальная нагрузка, Max
        public string P_Max;
        //Минимальная нагрузка, Min
        public string P_Min;
        //Поверочный интервал весов
        public string P_e;
        //Максимальная масса тары, T
        public string P_T;
        //Параметр фиксации веса
        public string Fix;
        //Код юстировки
        public string Calcode;
        //Версия ПО датчика взвешивания,
        public string PO_Ver;
        //Контрольная сумма ПО датчика взвешивания
        public string PO_Summ;

        public ResponseScaleParEntity(byte[] response)
        {
            System.Text.ASCIIEncoding encoding = new();
            // сюда надо вставить логику
            int i = 6;
            using MemoryStream memStream = new();

            //while (response.Length > i || response[i] != 0x0D)
            //{
            //    memStream.WriteByte(response[i++]);
            //}
            while (response.Length > i)
            {
                i++;
                if (response.Length <= i || response[i] == 0x0D)
                    break;
                memStream.WriteByte(response[i]);
            }

            P_Max = encoding.GetString(memStream.ToArray(), 0, memStream.ToArray().Length);

            // skip 0x0D
            if (response[i] == 0x0D)
                i++;
            // skip 0x0A
            if (response[i] == 0x0A)
                i++;
            memStream.SetLength(0);

            //while (response[i] != 0x0D)
            //{
            //    memStream.WriteByte(response[i++]);
            //}
            if (response.Length > i)
            {
                while (response[i] != 0x0D)
                {
                    i++;
                    if (response.Length <= i || response[i] == 0x0D)
                        break;
                    memStream.WriteByte(response[i]);
                }
            }
            P_Min = encoding.GetString(memStream.ToArray(), 0, memStream.ToArray().Length);
            i++; // пропустим 0x0D
            i++; // пропустим 0x0A
            memStream.SetLength(0);

            while (response[i] != 0x0D)
            {
                memStream.WriteByte(response[i++]);
            }
            P_e = encoding.GetString(memStream.ToArray(), 0, memStream.ToArray().Length);
            i++; // пропустим 0x0D
            i++; // пропустим 0x0A
            memStream.SetLength(0);

            while (response[i] != 0x0D)
            {
                memStream.WriteByte(response[i++]);
            }
            P_T = encoding.GetString(memStream.ToArray(), 0, memStream.ToArray().Length);
            i++; // пропустим 0x0D
            i++; // пропустим 0x0A
            memStream.SetLength(0);

            while (response[i] != 0x0D)
            {
                memStream.WriteByte(response[i++]);
            }
            Fix = encoding.GetString(memStream.ToArray(), 0, memStream.ToArray().Length);
            i++; // пропустим 0x0D
            i++; // пропустим 0x0A
            memStream.SetLength(0);

            while (response[i] != 0x0D)
            {
                memStream.WriteByte(response[i++]);
            }
            Calcode = encoding.GetString(memStream.ToArray(), 0, memStream.ToArray().Length);
            i++; // пропустим 0x0D
            i++; // пропустим 0x0A
            memStream.SetLength(0);

            while (response[i] != 0x0D)
            {
                memStream.WriteByte(response[i++]);
            }
            PO_Ver = encoding.GetString(memStream.ToArray(), 0, memStream.ToArray().Length);
            i++; // пропустим 0x0D
            i++; // пропустим 0x0A
            memStream.SetLength(0);

            while (response[i] != 0x0D)
            {
                memStream.WriteByte(response[i++]);
            }
            PO_Summ = encoding.GetString(memStream.ToArray(), 0, memStream.ToArray().Length);
            i++; // пропустим 0x0D
            i++; // пропустим 0x0A
            memStream.SetLength(0);

            //ErrorMessage = $"Код ответа CMD_ACK_SCALE_PAR: {Command};\n{P_Max};\n{P_Min};\n{P_e};\n{P_T};\n{Fix};\n{Calcode};\n{PO_Ver};\n{PO_Summ}";
        }
    }
}
