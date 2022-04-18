// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Utils;
using Microsoft.Data.SqlClient;

namespace DataCore.DAL.Utils
{
    public static class ScalesUtils
    {
        #region Public and private fields and properties

        public static SqlConnectFactory SqlConnect { get; private set; } = SqlConnectFactory.Instance;

        #endregion

        #region Public and private methods

        public static long GetScaleId(string scaleName)
        {
            long result = 0;
            using (SqlConnection con = SqlConnect.GetConnection())
            {
                con.Open();
                StringUtils.SetStringValueTrim(ref scaleName, 150);
                using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.Scales.GetScaleId))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@scale", scaleName);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            result = SqlConnect.GetValueAsNotNullable<long>(reader, "ID");
                        }
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }

        //public static ScaleDirect GetScale(long? scaleId)
        //{
        //    ScaleDirect result = new();
        //    if (scaleId == null)
        //        return result;
        //    using (SqlConnection con = SqlConnect.GetConnection())
        //    {
        //        con.Open();
        //        using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.Scales.GetScaleById))
        //        {
        //            cmd.Connection = con;
        //            cmd.Parameters.Clear();
        //            cmd.Parameters.AddWithValue("@id", scaleId);
        //            using SqlDataReader reader = cmd.ExecuteReader();
        //            if (reader.HasRows)
        //            {
        //                if (reader.Read())
        //                {
        //                    result.Id = SqlConnect.GetValueAsNotNullable<long>(reader, "ID");
        //                    result.Description = SqlConnect.GetValueAsString(reader, "Description");
        //                    result.DeviceIP = SqlConnect.GetValueAsString(reader, "DeviceIP");
        //                    result.DevicePort = SqlConnect.GetValueAsNotNullable<short>(reader, "DevicePort");
        //                    result.DeviceMac = SqlConnect.GetValueAsString(reader, "DeviceMAC");
        //                    result.DeviceWriteTimeout = SqlConnect.GetValueAsNotNullable<int>(reader, "DeviceSendTimeout");
        //                    result.DeviceReadTimeout = SqlConnect.GetValueAsNotNullable<int>(reader, "DeviceReceiveTimeout");
        //                    result.DeviceComPort = SqlConnect.GetValueAsString(reader, "DeviceComPort");
        //                    //result.ZebraPrinter = new ZebraPrinterHelper(SqlConnect.GetValueAsNullable<long?>(reader, "ZebraPrinterId"));
        //                    result.PrinterMain.Load(SqlConnect.GetValueAsNotNullable<long>(reader, "ZebraPrinterId"));
        //                    result.UseOrder = SqlConnect.GetValueAsNotNullable<bool>(reader, "UseOrder");
        //                    result.TemplateIdDefault = SqlConnect.GetValueAsNotNullable<long>(reader, "TemplateIdDefault");
        //                    result.TemplateIdSeries = SqlConnect.GetValueAsNullable<long?>(reader, "TemplateIdSeries");
        //                    result.ScaleFactor = SqlConnect.GetValueAsNullable<int?>(reader, "ScaleFactor");
        //                }
        //            }
        //            reader.Close();
        //        }
        //        con.Close();
        //    }
        //    return result;
        //}

        //[Obsolete(@"Deprecated method")]
        //public static ScaleDirect Load(long scaleId)
        //{
        //    ScaleDirect result = new();
        //    using (SqlConnection con = SqlConnect.GetConnection())
        //    {
        //        con.Open();
        //        using (SqlCommand cmd = new("SELECT * FROM [db_scales].[GetScaleByID] (@ScaleID);"))
        //        {
        //            cmd.Connection = con;
        //            cmd.Parameters.AddWithValue("@ScaleID", scaleId);
        //            using SqlDataReader reader = cmd.ExecuteReader();
        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    result.Id = SqlConnect.GetValueAsNotNullable<long>(reader, "ID");
        //                    result.Description = SqlConnect.GetValueAsString(reader, "Description");
        //                    result.DeviceIP = SqlConnect.GetValueAsString(reader, "DeviceIP");
        //                    result.DevicePort = SqlConnect.GetValue<short>(reader, "DevicePort");
        //                    result.DeviceMac = SqlConnect.GetValueAsString(reader, "DeviceMAC");
        //                    result.DeviceSendTimeout = SqlConnect.GetValueAsNotNullable<int>(reader, "DeviceSendTimeout");
        //                    result.DeviceReceiveTimeout = SqlConnect.GetValueAsNotNullable<int>(reader, "DeviceReceiveTimeout");
        //                    result.DeviceComPort = SqlConnect.GetValueAsString(reader, "DeviceComPort");
        //                    //result.ZebraPrinter = new ZebraPrinterHelper(SqlConnect.GetValueAsNullable<long?>(reader, "ZebraPrinterId"));
        //                    result.ZebraPrinter.Load(SqlConnect.GetValueAsNullable<long?>(reader, "ZebraPrinterId"));
        //                    result.UseOrder = SqlConnect.GetValueAsNotNullable<bool>(reader, "UseOrder");
        //                    result.TemplateIdDefault = SqlConnect.GetValueAsNotNullable<int>(reader, "TemplateIdDefault");
        //                    result.TemplateIdSeries = SqlConnect.GetValueAsNullable<int?>(reader, "TemplateIdSeries");
        //                    result.ScaleFactor = SqlConnect.GetValueAsNullable<int?>(reader, "ScaleFactor");
        //                }
        //            }
        //            reader.Close();
        //        }
        //        con.Close();
        //    }
        //    return result;
        //}

        //public static void Update(ScaleDirect scale)
        //{
        //    SqlParameter[] parameters = new SqlParameter[] {
        //        new SqlParameter("@ID", System.Data.SqlDbType.BigInt) { Value = scale.Id },
        //        new SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 150) { Value = scale.Description },
        //        //new SqlParameter("@IP", System.Data.SqlDbType.VarChar, 15) { Value = StringUtils.GetStringNullValueTrim(scale.DeviceIP, 15) },
        //        new SqlParameter("@Port", System.Data.SqlDbType.SmallInt) { Value = scale.DevicePort },
        //        //new SqlParameter("@MAC", System.Data.SqlDbType.VarChar, 35) { Value = StringUtils.GetStringNullValueTrim(scale.DeviceMac, 35) },
        //        new SqlParameter("@SendTimeout", System.Data.SqlDbType.SmallInt) { Value = scale.DeviceWriteTimeout },
        //        new SqlParameter("@ReceiveTimeout", System.Data.SqlDbType.SmallInt) { Value = scale.DeviceReadTimeout },
        //        new SqlParameter("@ComPort", System.Data.SqlDbType.VarChar, 5) { Value = StringUtils.GetStringNullValueTrim(scale.DeviceComPort, 5) },
        //        new SqlParameter("@UseOrder", System.Data.SqlDbType.SmallInt) { Value = scale.UseOrder == true ? 1 : 0 },
        //        new SqlParameter("@VerScalesUI", System.Data.SqlDbType.VarChar, 30) { Value = StringUtils.GetStringNullValueTrim(scale.VerScalesUI, 30) },
        //        new SqlParameter("@ScaleFactor", System.Data.SqlDbType.Int) { Value = scale.ScaleFactor },
        //    };
        //    SqlConnect.ExecuteNonQuery(SqlQueries.DbScales.Tables.Scales.UpdateScaleDirect, parameters);
        //}

        #endregion
    }
}
