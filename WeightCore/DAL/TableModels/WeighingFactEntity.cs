// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace WeightCore.DAL.TableModels
{
    [Serializable]
    public class WeighingFactEntity : BaseEntity<WeighingFactEntity>
    {
        public int Id { get; set; }
        public TemplateEntity Temp { get; set; }
        public int ScaleId { get; set; }
        public ScaleEntity Scale { get; set; }
        public string ProductSeries { get; set; }
     
        private PluEntity _plu;
        public PluEntity PLU
        {
            get => _plu;
            set
            {
                _plu = value;
                ExpirationDate = _productDate.AddDays(PLU == null ? 30 : (int)PLU.GoodsShelfLifeDays);
            }
        }
        private DateTime _productDate;
        public DateTime ProductDate
        {
            get => _productDate;
            set
            {
                _productDate = value;
                ExpirationDate = value.AddDays(PLU == null ? 30 : (int)PLU.GoodsShelfLifeDays);
            }
        }

        public DateTime ExpirationDate { get; set; }

        public int KneadingNumber { get; set; }

        private decimal _netWeight = 0;
        public decimal NetWeight
        {
            get => _netWeight;
            set
            {
                _netWeight = value;
                GrossWeight = _netWeight + _tareWeight;
            }
        }
        public string NetWeightKg
        {
            get
            {
                string[] chars = $"{_netWeight:00.000}".Replace(',', '.').Split('.');
                return chars.Length > 0 ? chars[0] : "00";
            }
            set => _ = value;
        }
        public string NetWeightGr
        {
            get
            {
                string[] chars = $"{_netWeight:00.000}".Replace(',', '.').Split('.');
                return chars.Length > 0 ? chars[1] : "000";
            }
            set => _ = value;
        }

        private decimal _tareWeight = 0;
        public decimal TareWeight
        {
            get => _tareWeight;
            set
            {
                _tareWeight = value;
                GrossWeight = _netWeight + _tareWeight;
            }
        }
        public decimal GrossWeight { get; set; }
        public int? ScaleFactor { get; set; }
        public DateTime RegDate { get; set; }
        public SsccEntity Sscc { get; set; }
        public WeighingFactEntity()
        {
            ScaleId = 0;
            PLU = null;
            NetWeight = 0;
            TareWeight = 0;
            ScaleFactor = 1000;
            RegDate = DateTime.Now;
            ProductDate = DateTime.Now.Date;
        }

        /// <summary>
        /// Сохранить запись в таблице [WeithingFact].
        /// </summary>
        public void Save()
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                string query =
                    "DECLARE @SSCC varchar(50);\n" +
                    "DECLARE @WeithingDate datetime;\n" +
                    "DECLARE @xmldata xml;\n" +
                    "DECLARE @ID int;\n" +
                    "EXECUTE [db_scales].[SetWeithingFact] @OrderID,@ScaleID,@PLU,@NetWeight,@TareWeight,@ProductDate,@Kneading,@SSCC OUTPUT,@WeithingDate OUTPUT,@xmldata OUTPUT,@ID OUTPUT;\n" +
                    "SELECT  @SSCC, @WeithingDate, convert(varchar(max), @xmldata) xmldata, @ID; ";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    SqlParameter planIndexParameter = new SqlParameter("@OrderID", SqlDbType.VarChar) {Value = DBNull.Value};
                    cmd.Parameters.Add(planIndexParameter);
                    cmd.Parameters.AddWithValue("@ScaleID", ScaleId);
                    cmd.Parameters.AddWithValue("@PLU", PLU.PLU);
                    cmd.Parameters.AddWithValue("@NetWeight", (NetWeight));
                    cmd.Parameters.AddWithValue("@TareWeight", (TareWeight));
                    cmd.Parameters.AddWithValue("@ProductDate", ProductDate);
                    cmd.Parameters.AddWithValue("@Kneading", KneadingNumber);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //string sscc             = reader.GetString(0);
                                RegDate    = reader.GetDateTime(1);
                                Id         = reader.GetInt32(3);
                                XDocument xDoc = XDocument.Parse(reader.GetString(2));
                                SsccEntity sscc = new SsccEntity();
                                sscc.SSCC = xDoc.Root.Element("Item").Attribute("SSCC").Value;
                                sscc.GLN = xDoc.Root.Element("Item").Attribute("GLN").Value;
                                sscc.UnitID = int.Parse(xDoc.Root.Element("Item").Attribute("UnitID").Value);
                                sscc.UnitType = byte.Parse(xDoc.Root.Element("Item").Attribute("UnitType").Value);
                                sscc.SynonymSSCC = xDoc.Root.Element("Item").Attribute("SynonymSSCC").Value;
                                sscc.Check = int.Parse(xDoc.Root.Element("Item").Attribute("Check").Value);
                                Sscc = sscc;
                            }
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
        }

        public static WeighingFactEntity New(ScaleEntity scale, PluEntity plu, DateTime productDate, int kneadingNumber,
            int? scaleFactor, decimal netWeight, decimal tareWeight)
        {
            WeighingFactEntity weighingFact = new WeighingFactEntity
            {
                ScaleId = scale.Id,
                ScaleFactor = scaleFactor,
                Scale = scale,
                PLU = plu,
                ProductDate = productDate,
                KneadingNumber = kneadingNumber,
                NetWeight = netWeight,
                TareWeight = tareWeight
            };
            return weighingFact;
        }

        public new string SerializeObject()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(WeighingFactEntity));
            XmlWriterSettings settings = new XmlWriterSettings
            {
                ConformanceLevel = ConformanceLevel.Document,
                OmitXmlDeclaration = false,
                Encoding = Encoding.UTF8,
                Indent = true,
                IndentChars = "\t"
            };
            // не подавлять xml заголовок
            // кодировка
            // Какого то кипариса! эта настройка не работает
            // и UTF16 записывается в шапку XML
            // типа Visual Studio работает только с UTF16

            // добавлять отступы
            // сиволы отступа

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
