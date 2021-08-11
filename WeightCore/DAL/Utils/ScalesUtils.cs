// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Data.SqlClient;
using WeightCore.DAL.TableModels;
using WeightCore.Utils;

namespace WeightCore.DAL.Utils
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
                using (SqlCommand cmd = new SqlCommand(SqlQueries.GetScaleId))
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
            ScaleEntity result = new ScaleEntity();
            if (scaleId == null)
                return result;
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(SqlQueries.GetScaleById))
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
                                result.DeviceIp = SqlConnectFactory.GetValue<string>(reader, "DeviceIP");
                                result.DevicePort = SqlConnectFactory.GetValue<int>(reader, "DevicePort");
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
            ScaleEntity result = new ScaleEntity();
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [db_scales].[GetScaleByID] (@ScaleID);"))
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
                                result.DeviceIp = SqlConnectFactory.GetValue<string>(reader, "DeviceIP");
                                result.DevicePort = SqlConnectFactory.GetValue<int>(reader, "DevicePort");
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

        public static void Save(ScaleEntity scale)
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                string query = @"
                    DECLARE @ID int; 
                    EXECUTE [db_scales].[SetNewScale]
                    @Uuid,
                    @Description,
                    @IP,
                    @Port,
                    @DeviceMAC,
                    @DeviceSendTimeout,
                    @DeviceReceiveTimeout,
                    @DeviceComPort,
                    @UseOrder,
                    @VerScalesUI,
                    @ScaleFactor,
                    @ID OUTPUT ;
                    SELECT @ID";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue($"@Uuid", scale.Id);  // @1CRRefID
                    cmd.Parameters.AddWithValue($"@Description", scale.Description);
                    cmd.Parameters.AddWithValue($"@IP", scale.DeviceIp);
                    cmd.Parameters.AddWithValue($"@Port", scale.DevicePort);
                    cmd.Parameters.AddWithValue($"@DeviceMAC", scale.DeviceMac);
                    cmd.Parameters.AddWithValue($"@DeviceSendTimeout", scale.DeviceSendTimeout);
                    cmd.Parameters.AddWithValue($"@DeviceReceiveTimeout", scale.DeviceReceiveTimeout);
                    cmd.Parameters.AddWithValue($"@DeviceComPort", scale.DeviceComPort);
                    cmd.Parameters.AddWithValue($"@UseOrder", scale.UseOrder);
                    cmd.Parameters.AddWithValue($"@VerScalesUI", "");
                    cmd.Parameters.AddWithValue($"@ScaleFactor", scale.ScaleFactor);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        #endregion
    }
}