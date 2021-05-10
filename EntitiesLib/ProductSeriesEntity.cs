// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using WeightServices.Common;

namespace WeightServices.Entities
{
    [Serializable]
    public class ProductSeriesEntity
    {
        public ProductSeriesEntity()
        {

        }


        public ProductSeriesEntity(ScaleEntity _Scale)
        {
            this.Scale = _Scale;
        }


        public int Id { get; set; }
        public Guid UUID { get; set; }
        public ScaleEntity Scale { get; set; }
        public DateTime CreateDate { get; set; }
        public SsccEntity Sscc { get; set; }
        public PluEntity Plu { get; set; }

        //public string TemplateID { get; set; }

        [XmlIgnoreAttribute]
        public TemplateEntity Template { get; set; }
        public Int32 CountUnit { get; set; }
        public Decimal TotalNetWeight { get; set; }
        public Decimal TotalTareWeight { get; set; }

        public void LoadTemplate(int _TemplateID)
        {
            if (_TemplateID != null)
                this.Template = new TemplateEntity(_TemplateID);
        }

        public void New()
        {

            if (this.Scale == null)
            {
                throw new Exception("Equipment instance not identified. Set [Scale].");
            }

            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                string query =

                    "DECLARE @SSCC varchar(50);\n" +
                    "DECLARE @WeithingDate datetime;\n" +
                    "DECLARE @xmldata xml;\n" +
                    "EXECUTE [db_scales].[NewProductSeries] @ScaleID, @SSCC OUTPUT, @xmldata OUTPUT;\n " +
                    "SELECT Id, CreateDate, UUID, SSCC,CountUnit,TotalNetWeight, TotalTareWeight " +
                    " FROM [db_scales].[GetCurrentProductSeries](@ScaleId);";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ScaleID", Scale.Id);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        this.Id = reader.GetInt32(0);
                        this.CreateDate = reader.GetDateTime(1);
                        this.UUID = reader.GetGuid(2);
                        this.CountUnit = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                        this.TotalNetWeight = reader.IsDBNull(5) ? 0 : reader.GetDecimal(5);
                        this.TotalTareWeight = reader.IsDBNull(6) ? 0 : reader.GetDecimal(6);

                        this.Sscc = new SsccEntity(reader.GetString(3));

                    }
                    reader.Close();
                    con.Close();
                }
            }

        }

        public void Load()
        {

            if (this.Scale.Id == null)
            {
                throw new Exception("Equipment instance not identified. Set [Scale].");
            }

            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                string query =
                    "SELECT Id, CreateDate, UUID, SSCC, CountUnit,TotalNetWeight, TotalTareWeight " +
                    " FROM [db_scales].[GetCurrentProductSeries](@ScaleId);";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ScaleID", Scale.Id);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        this.Id = reader.GetInt32(0);
                        this.CreateDate = reader.GetDateTime(1);
                        this.UUID = reader.GetGuid(2);
                        this.CountUnit = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                        this.TotalNetWeight = reader.IsDBNull(5) ? 0 : reader.GetDecimal(5);
                        this.TotalTareWeight = reader.IsDBNull(6) ? 0 : reader.GetDecimal(6);

                        this.Sscc = new SsccEntity(reader.GetString(3));

                    }
                    reader.Close();
                    con.Close();
                }
            }

        }

        public string SerializeObject()
        {

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProductSeriesEntity));

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.OmitXmlDeclaration = false;    // не подавлять xml заголовок

            settings.Encoding = Encoding.UTF8;      // кодировка
            // Какого то кипариса! эта настройка не работает
            // и UTF16 записывается в шапку XML
            // типа Visual Studio работает только с UTF16

            settings.Indent = true;                // добавлять отступы
            settings.IndentChars = "\t";           // сиволы отступа

            XmlSerializerNamespaces dummyNSs = new XmlSerializerNamespaces();
            dummyNSs.Add(string.Empty, string.Empty);

            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter xw = XmlWriter.Create(textWriter, settings))
                {
                    xmlSerializer.Serialize(xw, this, dummyNSs);
                }
                return textWriter.ToString();
            }

        }

    }

}
