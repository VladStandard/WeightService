// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataBaseCore.DAL.TableModels;
using DataBaseCore.Utils;
using Microsoft.Data.SqlClient;
using System;

namespace DataBaseCore.DAL.Utils
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
                StringUtils.StringValueTrim(ref scaleName, 150);
                using (SqlCommand cmd = new(SqlQueries.Scales.GetScaleId))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@scale", scaleName);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                result = SqlConnectFactory.GetValue<int>(reader, "ID");
                            }
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
            return result;
        }

        public static ScaleEntity GetScale(int? scaleId)
        {
            ScaleEntity result = new();
            if (scaleId == null)
                return result;
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new(SqlQueries.Scales.GetScaleById))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id", scaleId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                result.Id = SqlConnectFactory.GetValue<int>(reader, "ID");
                                result.Description = SqlConnectFactory.GetValue<string>(reader, "Description");
                                result.DeviceIP = SqlConnectFactory.GetValue<string>(reader, "DeviceIP");
                                result.DevicePort = SqlConnectFactory.GetValue<short>(reader, "DevicePort");
                                result.DeviceMac = SqlConnectFactory.GetValue<string>(reader, "DeviceMAC");
                                result.DeviceSendTimeout = SqlConnectFactory.GetValue<int>(reader, "DeviceSendTimeout");
                                result.DeviceReceiveTimeout = SqlConnectFactory.GetValue<int>(reader, "DeviceReceiveTimeout");
                                result.DeviceComPort = SqlConnectFactory.GetValue<string>(reader, "DeviceComPort");
                                result.ZebraPrinter = new ZebraPrinterEntity(SqlConnectFactory.GetValue<int?>(reader, "ZebraPrinterId"));
                                result.UseOrder = SqlConnectFactory.GetValue<bool>(reader, "UseOrder");
                                result.TemplateIdDefault = SqlConnectFactory.GetValue<int>(reader, "TemplateIdDefault");
                                result.TemplateIdSeries = SqlConnectFactory.GetValue<int?>(reader, "TemplateIdSeries");
                                result.ScaleFactor = SqlConnectFactory.GetValue<int?>(reader, "ScaleFactor");
                            }
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
            return result;
        }

        [Obsolete(@"Deprecated method")]
        public static ScaleEntity Load(int scaleId)
        {
            ScaleEntity result = new();
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new("SELECT * FROM [db_scales].[GetScaleByID] (@ScaleID);"))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ScaleID", scaleId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Id = SqlConnectFactory.GetValue<int>(reader, "ID");
                                result.Description = SqlConnectFactory.GetValue<string>(reader, "Description");
                                result.DeviceIP = SqlConnectFactory.GetValue<string>(reader, "DeviceIP");
                                result.DevicePort = SqlConnectFactory.GetValue<short>(reader, "DevicePort");
                                result.DeviceMac = SqlConnectFactory.GetValue<string>(reader, "DeviceMAC");
                                result.DeviceSendTimeout = SqlConnectFactory.GetValue<int>(reader, "DeviceSendTimeout");
                                result.DeviceReceiveTimeout = SqlConnectFactory.GetValue<int>(reader, "DeviceReceiveTimeout");
                                result.DeviceComPort = SqlConnectFactory.GetValue<string>(reader, "DeviceComPort");
                                result.ZebraPrinter = new ZebraPrinterEntity(SqlConnectFactory.GetValue<int?>(reader, "ZebraPrinterId"));
                                result.UseOrder = SqlConnectFactory.GetValue<bool>(reader, "UseOrder");
                                result.TemplateIdDefault = SqlConnectFactory.GetValue<int>(reader, "TemplateIdDefault");
                                result.TemplateIdSeries = SqlConnectFactory.GetValue<int?>(reader, "TemplateIdSeries");
                                result.ScaleFactor = SqlConnectFactory.GetValue<int?>(reader, "ScaleFactor");
                            }
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
            return result;
        }

        public static void Update(ScaleEntity scale)
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new(SqlQueries.Scales.UpdateScaleFieldDescription))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int);
                    cmd.Parameters["@ID"].Value = scale.Id;
                    cmd.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar, 150);
                    cmd.Parameters["@Description"].Value = scale.Description;
                    //cmd.Parameters.Add("@IP", System.Data.SqlDbType.VarChar, 15);
                    //cmd.Parameters["@IP"].Value = scale.DeviceIP;
                    cmd.Parameters.Add("@Port", System.Data.SqlDbType.SmallInt);
                    cmd.Parameters["@Port"].Value = scale.DevicePort;
                    //cmd.Parameters.Add("@MAC", System.Data.SqlDbType.VarChar, 35);
                    //cmd.Parameters["@MAC"].Value = scale.DeviceMac;
                    cmd.Parameters.Add("@SendTimeout", System.Data.SqlDbType.SmallInt);
                    cmd.Parameters["@SendTimeout"].Value = scale.DeviceSendTimeout;
                    cmd.Parameters.Add("@ReceiveTimeout", System.Data.SqlDbType.SmallInt);
                    cmd.Parameters["@ReceiveTimeout"].Value = scale.DeviceReceiveTimeout;
                    //cmd.Parameters.Add("@ComPort", System.Data.SqlDbType.VarChar, 5);
                    //cmd.Parameters["@ComPort"].Value = scale.DeviceComPort;
                    cmd.Parameters.Add("@UseOrder", System.Data.SqlDbType.SmallInt);
                    cmd.Parameters["@UseOrder"].Value = scale.UseOrder == true ? 1 : 0;
                    //cmd.Parameters.Add("@VerScalesUI", System.Data.SqlDbType.VarChar, 30);
                    //cmd.Parameters["@VerScalesUI"].Value = scale.VerScalesUI;
                    cmd.Parameters.Add("@ScaleFactor", System.Data.SqlDbType.Int);
                    cmd.Parameters["@ScaleFactor"].Value = scale.ScaleFactor;
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        #endregion
    }
}
