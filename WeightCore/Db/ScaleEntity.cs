// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Serialization;

namespace WeightCore.Db
{
    [Serializable]
    public class ScaleEntity
    {
        #region Public and private fields and properties

        public int Id { get; set; }
        public string Description { get; set; }
        public string DeviceIp { get; set; }
        public int DeviceId { get; set; }
        public string DeviceMac { get; set; }
        public int DevicePort { get; set; }
        public int DeviceSendTimeout { get; set; }
        public int DeviceReceiveTimeout { get; set; }
        public string DeviceComPort { get; set; }
        public bool UseOrder { get; set; }
        public int ScaleFactor { get; set; }

        public int TemplateIdDefault { get; set; }
        public int TemplateIdSeries { get; set; }

        public ZebraPrinterEntity ZebraPrinter { get; set; }

        #endregion

        #region Constructor and destructor

        public ScaleEntity()
        {
            Init();
        }

        public ScaleEntity(int scaleId)
        {
            Id = scaleId;
            Init();
        }

        #endregion

        #region Public and private methods

        private void Init()
        {
            DevicePort = 5001;
            Description = "";
            DeviceSendTimeout = 500;
            DeviceReceiveTimeout = 1000;
            DeviceComPort = "COM4";
            UseOrder = false;
        }

        public string SerializeObject()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ScaleEntity));
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, this);
                return textWriter.ToString();
            }
        }

        public void Load()
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                string query = "SELECT * FROM [db_scales].[GetScaleByID] (@ScaleID);";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    int? zebraPrinterId = null;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ScaleID", Id);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Description = SqlConnectFactory.GetValue<string>(reader, "Description");
                        DeviceIp = SqlConnectFactory.GetValue<string>(reader, "DeviceIP");
                        DeviceId = SqlConnectFactory.GetValue<int>(reader, "DeviceID");
                        DevicePort = SqlConnectFactory.GetValue<int>(reader, "DevicePort");
                        DeviceMac = SqlConnectFactory.GetValue<string>(reader, "DeviceMAC");
                        DeviceSendTimeout = SqlConnectFactory.GetValue<int>(reader, "DeviceSendTimeout");
                        DeviceReceiveTimeout = SqlConnectFactory.GetValue<int>(reader, "DeviceReceiveTimeout");
                        DeviceComPort = SqlConnectFactory.GetValue<string>(reader, "DeviceComPort");
                        TemplateIdDefault = SqlConnectFactory.GetValue<int>(reader, "TemplateIdDefault");
                        TemplateIdSeries = SqlConnectFactory.GetValue<int>(reader, "TemplateIdSeries");
                        ScaleFactor = SqlConnectFactory.GetValue<int>(reader, "ScaleFactor");
                        UseOrder = SqlConnectFactory.GetValue<bool>(reader, "UseOrder");
                        zebraPrinterId = SqlConnectFactory.GetValue<int>(reader, "ZebraPrinterId");
                    }

                    reader.Close();
                    con.Close();

                    ZebraPrinter = new ZebraPrinterEntity(zebraPrinterId);
                }
            }
        }

        public void Save()
        {

            //Int32 r = Int32.MinValue;
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
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
                    cmd.Parameters.AddWithValue($"@Uuid", Id);  // @1CRRefID
                    cmd.Parameters.AddWithValue($"@Description", Description);
                    cmd.Parameters.AddWithValue($"@IP", DeviceIp);
                    cmd.Parameters.AddWithValue($"@Port", DevicePort);
                    cmd.Parameters.AddWithValue($"@DeviceMAC", DeviceMac);
                    cmd.Parameters.AddWithValue($"@DeviceSendTimeout", DeviceSendTimeout);
                    cmd.Parameters.AddWithValue($"@DeviceReceiveTimeout", DeviceReceiveTimeout);
                    cmd.Parameters.AddWithValue($"@DeviceComPort", DeviceComPort);
                    cmd.Parameters.AddWithValue($"@UseOrder", UseOrder);
                    cmd.Parameters.AddWithValue($"@VerScalesUI", "");
                    cmd.Parameters.AddWithValue($"@ScaleFactor", ScaleFactor);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    //r = Convert.ToInt32(cmd.ExecuteScalar());
                    //this.DeviceId = r;
                    //con.Close();
                }
            }
        }

        #endregion
    }
}