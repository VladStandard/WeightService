// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL.TableModels;
using DataProjectsCore.Utils;
using Microsoft.Data.SqlClient;
using System;

namespace DataProjectsCore.DAL.Utils
{
    public static class ScalesUtils
    {
        #region Public and private methods

        public static int GetScaleId(string scaleName)
        {
            int result = 0;
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                DataShareCore.Utils.StringUtils.SetStringValueTrim(ref scaleName, 150);
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
                            result = SqlConnectFactory.GetValue<int>(reader, "ID");
                        }
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }

        public static ScaleDirect GetScale(int? scaleId)
        {
            ScaleDirect result = new();
            if (scaleId == null)
                return result;
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.Scales.GetScaleById))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id", scaleId);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            result.Id = SqlConnectFactory.GetValue<int>(reader, "ID");
                            result.Description = SqlConnectFactory.GetValue<string>(reader, "Description");
                            result.DeviceIP = SqlConnectFactory.GetValue<string>(reader, "DeviceIP");
                            result.DevicePort = SqlConnectFactory.GetValue<short>(reader, "DevicePort");
                            result.DeviceMac = SqlConnectFactory.GetValue<string>(reader, "DeviceMAC");
                            result.DeviceWriteTimeout = SqlConnectFactory.GetValue<int>(reader, "DeviceSendTimeout");
                            result.DeviceReadTimeout = SqlConnectFactory.GetValue<int>(reader, "DeviceReceiveTimeout");
                            result.DeviceComPort = SqlConnectFactory.GetValue<string>(reader, "DeviceComPort");
                            //result.ZebraPrinter = new ZebraPrinterHelper(SqlConnectFactory.GetValue<int?>(reader, "ZebraPrinterId"));
                            result.ZebraPrinter.Load(SqlConnectFactory.GetValue<int?>(reader, "ZebraPrinterId"));
                            result.UseOrder = SqlConnectFactory.GetValue<bool>(reader, "UseOrder");
                            result.TemplateIdDefault = SqlConnectFactory.GetValue<int>(reader, "TemplateIdDefault");
                            result.TemplateIdSeries = SqlConnectFactory.GetValue<int?>(reader, "TemplateIdSeries");
                            result.ScaleFactor = SqlConnectFactory.GetValue<int?>(reader, "ScaleFactor");
                        }
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }

        //[Obsolete(@"Deprecated method")]
        //public static ScaleDirect Load(int scaleId)
        //{
        //    ScaleDirect result = new();
        //    using (SqlConnection con = SqlConnectFactory.GetConnection())
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
        //                    result.Id = SqlConnectFactory.GetValue<int>(reader, "ID");
        //                    result.Description = SqlConnectFactory.GetValue<string>(reader, "Description");
        //                    result.DeviceIP = SqlConnectFactory.GetValue<string>(reader, "DeviceIP");
        //                    result.DevicePort = SqlConnectFactory.GetValue<short>(reader, "DevicePort");
        //                    result.DeviceMac = SqlConnectFactory.GetValue<string>(reader, "DeviceMAC");
        //                    result.DeviceSendTimeout = SqlConnectFactory.GetValue<int>(reader, "DeviceSendTimeout");
        //                    result.DeviceReceiveTimeout = SqlConnectFactory.GetValue<int>(reader, "DeviceReceiveTimeout");
        //                    result.DeviceComPort = SqlConnectFactory.GetValue<string>(reader, "DeviceComPort");
        //                    //result.ZebraPrinter = new ZebraPrinterHelper(SqlConnectFactory.GetValue<int?>(reader, "ZebraPrinterId"));
        //                    result.ZebraPrinter.Load(SqlConnectFactory.GetValue<int?>(reader, "ZebraPrinterId"));
        //                    result.UseOrder = SqlConnectFactory.GetValue<bool>(reader, "UseOrder");
        //                    result.TemplateIdDefault = SqlConnectFactory.GetValue<int>(reader, "TemplateIdDefault");
        //                    result.TemplateIdSeries = SqlConnectFactory.GetValue<int?>(reader, "TemplateIdSeries");
        //                    result.ScaleFactor = SqlConnectFactory.GetValue<int?>(reader, "ScaleFactor");
        //                }
        //            }
        //            reader.Close();
        //        }
        //        con.Close();
        //    }
        //    return result;
        //}

        public static void Update(ScaleDirect scale)
        {
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@ID", System.Data.SqlDbType.Int) { Value = scale.Id },
                new SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 150) { Value = scale.Description },
                //new SqlParameter("@IP", System.Data.SqlDbType.VarChar, 15) { Value = DataShareCore.Utils.StringUtils.GetStringNullValueTrim(scale.DeviceIP, 15) },
                new SqlParameter("@Port", System.Data.SqlDbType.SmallInt) { Value = scale.DevicePort },
                //new SqlParameter("@MAC", System.Data.SqlDbType.VarChar, 35) { Value = DataShareCore.Utils.StringUtils.GetStringNullValueTrim(scale.DeviceMac, 35) },
                new SqlParameter("@SendTimeout", System.Data.SqlDbType.SmallInt) { Value = scale.DeviceWriteTimeout },
                new SqlParameter("@ReceiveTimeout", System.Data.SqlDbType.SmallInt) { Value = scale.DeviceReadTimeout },
                new SqlParameter("@ComPort", System.Data.SqlDbType.VarChar, 5) { Value = DataShareCore.Utils.StringUtils.GetStringNullValueTrim(scale.DeviceComPort, 5) },
                new SqlParameter("@UseOrder", System.Data.SqlDbType.SmallInt) { Value = scale.UseOrder == true ? 1 : 0 },
                new SqlParameter("@VerScalesUI", System.Data.SqlDbType.VarChar, 30) { Value = DataShareCore.Utils.StringUtils.GetStringNullValueTrim(scale.VerScalesUI, 30) },
                new SqlParameter("@ScaleFactor", System.Data.SqlDbType.Int) { Value = scale.ScaleFactor },
            };
            SqlConnectFactory.ExecuteNonQuery(SqlQueries.DbScales.Tables.Scales.UpdateScaleDirect, parameters);
        }

        #endregion
    }
}
