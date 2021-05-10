// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Xml.Serialization;
using EntitiesLib;
using WeightServices.Common;

namespace WeightServices.Entities
{
    [Serializable]
    public class ScaleEntity
    {
        public int Id { get; set; }
        public String Description { get; set; }
        public String DeviceIP { get; set; }
        public int DeviceId { get; set; }
        public String DeviceMAC { get; set; }
        public Int32 DevicePort { get; set; }
        public Int32 DeviceSendTimeout { get; set; }
        public Int32 DeviceReceiveTimeout { get; set; }
        public String DeviceComPort { get; set; }
        //public String ZebraIP { get; set; }
        //public short ZebraPort { get; set; }
        public Boolean UseOrder { get; set; }
        public int ScaleFactor { get; set; }

        public Int32 TemplateIdDefault { get; set; }
        public Int32 TemplateIdSeries { get; set; }
        
        public ZebraPrinterEntity ZebraPrinter { get; set; }

    public ScaleEntity()
        {
            Init();
        }

        public ScaleEntity(int ScaleID)
        {
            this.Id = ScaleID;
            Init();
        }

 

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
                    cmd.Parameters.AddWithValue("@ScaleID", this.Id);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Description = SqlConnectFactory.GetValue<string>(reader, "Description");
                        DeviceIP = SqlConnectFactory.GetValue<string>(reader, "DeviceIP");
                        DeviceId = SqlConnectFactory.GetValue<int>(reader, "DeviceID");
                        DevicePort = SqlConnectFactory.GetValue<int>(reader, "DevicePort");
                        DeviceMAC = SqlConnectFactory.GetValue<string>(reader, "DeviceMAC");
                        DeviceSendTimeout = SqlConnectFactory.GetValue<int>(reader, "DeviceSendTimeout");
                        DeviceReceiveTimeout = SqlConnectFactory.GetValue<int>(reader, "DeviceReceiveTimeout");
                        DeviceComPort = SqlConnectFactory.GetValue<string>(reader, "DeviceComPort");
                        TemplateIdDefault = SqlConnectFactory.GetValue<Int32>(reader, "TemplateIdDefault");
                        TemplateIdSeries = SqlConnectFactory.GetValue<Int32>(reader, "TemplateIdSeries");
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
                    cmd.Parameters.AddWithValue($"@Uuid", this.Id);  // @1CRRefID
                    cmd.Parameters.AddWithValue($"@Description", this.Description);
                    cmd.Parameters.AddWithValue($"@IP", this.DeviceIP);
                    cmd.Parameters.AddWithValue($"@Port", this.DevicePort);
                    cmd.Parameters.AddWithValue($"@DeviceMAC", this.DeviceMAC);
                    cmd.Parameters.AddWithValue($"@DeviceSendTimeout", this.DeviceSendTimeout);
                    cmd.Parameters.AddWithValue($"@DeviceReceiveTimeout", this.DeviceReceiveTimeout);
                    cmd.Parameters.AddWithValue($"@DeviceComPort", this.DeviceComPort);
                    cmd.Parameters.AddWithValue($"@UseOrder", this.UseOrder);
                    cmd.Parameters.AddWithValue($"@VerScalesUI", "");
                    cmd.Parameters.AddWithValue($"@ScaleFactor", this.ScaleFactor);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    //r = Convert.ToInt32(cmd.ExecuteScalar());
                    //this.DeviceId = r;
                    //con.Close();
                }
            }
        }

    }

}
