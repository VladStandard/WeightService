// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Xml.Serialization;

namespace DataProjectsCore.DAL.TableModels
{
    [Serializable]
    public class ProductSeriesDirect : BaseSerializeEntity<ProductSeriesDirect>
    {
        #region Public and private fields and properties

        public int Id { get; set; }
        public Guid UUID { get; set; }
        public ScaleDirect Scale { get; set; }
        public DateTime CreateDate { get; set; }
        public SsccDirect Sscc { get; set; }
        public PluDirect Plu { get; set; }
        [XmlIgnore]
        public TemplateDirect Template { get; set; }
        public int CountUnit { get; set; }
        public decimal TotalNetWeight { get; set; }
        public decimal TotalTareWeight { get; set; }

        #endregion

        #region Constructor and destructor

        public ProductSeriesDirect()
        {
            Load();
        }

        public ProductSeriesDirect(ScaleDirect scale)
        {
            Scale = scale;
            Load();
        }

        #endregion

        #region Public and private methods

        public void LoadTemplate(int id)
        {
            Template = new TemplateDirect(id);
        }

        public void New()
        {
            if (Scale == null)
            {
                throw new Exception("Equipment instance not identified. Set [Scale].");
            }

            using SqlConnection con = SqlConnectFactory.GetConnection();
            con.Open();
            string query =
                "DECLARE @SSCC varchar(50);\n" +
                "DECLARE @WeithingDate datetime;\n" +
                "DECLARE @xmldata xml;\n" +
                "EXECUTE [db_scales].[NewProductSeries] @ScaleID, @SSCC OUTPUT, @xmldata OUTPUT;\n " +
                "SELECT Id, CreateDate, UUID, SSCC,CountUnit,TotalNetWeight, TotalTareWeight " +
                " FROM [db_scales].[GetCurrentProductSeries](@ScaleId);";
            using (SqlCommand cmd = new(query))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@ScaleID", Scale.Id);
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Id = reader.GetInt32(0);
                        CreateDate = reader.GetDateTime(1);
                        UUID = reader.GetGuid(2);
                        CountUnit = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                        TotalNetWeight = reader.IsDBNull(5) ? 0 : reader.GetDecimal(5);
                        TotalTareWeight = reader.IsDBNull(6) ? 0 : reader.GetDecimal(6);
                        Sscc = new SsccDirect(reader.GetString(3));
                    }
                }
                reader.Close();
            }
            con.Close();
        }

        public void Load()
        {
            if (Scale == null || Scale.Id == default) return;
            using SqlConnection con = SqlConnectFactory.GetConnection();
            con.Open();
            string query =
                "SELECT Id, CreateDate, UUID, SSCC, CountUnit,TotalNetWeight, TotalTareWeight " +
                " FROM [db_scales].[GetCurrentProductSeries](@ScaleId);";
            using (SqlCommand cmd = new(query))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@ScaleID", Scale.Id);
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Id = reader.GetInt32(0);
                        CreateDate = reader.GetDateTime(1);
                        UUID = reader.GetGuid(2);
                        CountUnit = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                        TotalNetWeight = reader.IsDBNull(5) ? 0 : reader.GetDecimal(5);
                        TotalTareWeight = reader.IsDBNull(6) ? 0 : reader.GetDecimal(6);
                        Sscc = new SsccDirect(reader.GetString(3));
                    }
                }
                reader.Close();
            }
            con.Close();
        }

        #endregion
    }
}
