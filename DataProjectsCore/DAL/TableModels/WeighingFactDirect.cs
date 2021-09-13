// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DataProjectsCore.DAL.TableModels
{
    [Serializable]
    public class WeighingFactDirect : BaseSerializeEntity<WeighingFactDirect>
    {
        public int Id { get; set; }
        public TemplateDirect Temp { get; set; }
        public int ScaleId { get; set; }
        public ScaleDirect Scale { get; set; }
        public string ProductSeries { get; set; }

        private PluDirect _plu;
        public PluDirect PLU
        {
            get => _plu;
            set
            {
                _plu = value;
                ExpirationDate = _productDate.AddDays(PLU == null || PLU.GoodsShelfLifeDays == null ? 30 : (int)PLU.GoodsShelfLifeDays);
            }
        }
        private DateTime _productDate;
        public DateTime ProductDate
        {
            get => _productDate;
            set
            {
                _productDate = value;
                ExpirationDate = value.AddDays(PLU == null || PLU.GoodsShelfLifeDays == null ? 30 : (int)PLU.GoodsShelfLifeDays);
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
        //public SsccDirect Sscc { get; set; }
        public WeighingFactDirect()
        {
            ScaleId = 0;
            PLU = null;
            NetWeight = 0;
            TareWeight = 0;
            ScaleFactor = 1000;
            RegDate = DateTime.Now;
            ProductDate = DateTime.Now.Date;
        }

        private void SaveReader(SqlDataReader reader)
        {
            if (reader.Read())
            {
                RegDate = reader.GetDateTime(1);
                Id = reader.GetInt32(3);
                //XDocument xDoc = XDocument.Parse(reader.GetString(2));
                //SsccDirect sscc = new();
                //sscc.SSCC = xDoc.Root.Element("Item").Attribute("SSCC").Value;
                //sscc.GLN = xDoc.Root.Element("Item").Attribute("GLN").Value;
                //sscc.UnitID = int.Parse(xDoc.Root.Element("Item").Attribute("UnitID").Value);
                //sscc.UnitType = byte.Parse(xDoc.Root.Element("Item").Attribute("UnitType").Value);
                //sscc.SynonymSSCC = xDoc.Root.Element("Item").Attribute("SynonymSSCC").Value;
                //sscc.Check = int.Parse(xDoc.Root.Element("Item").Attribute("Check").Value);
                //Sscc = sscc;
            }
        }

        public void Save()
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@OrderID", SqlDbType.VarChar, 38) { Value = DBNull.Value },
                new SqlParameter("@ScaleID", SqlDbType.VarChar, 38) { Value = ScaleId },
                new SqlParameter("@PLU", SqlDbType.Int) { Value = PLU.PLU },
                new SqlParameter("@NetWeight", SqlDbType.Decimal) { Value = NetWeight },
                new SqlParameter("@TareWeight", SqlDbType.Decimal) { Value = TareWeight },
                new SqlParameter("@ProductDate", SqlDbType.Date) { Value = ProductDate },
                new SqlParameter("@Kneading", SqlDbType.Int) { Value = KneadingNumber },
            };
            SqlConnectFactory.ExecuteReader(SqlQueries.DbScales.Tables.WeithingFacts.Save, parameters, SaveReader);
        }

        //public void SaveDeprecated()
        //{
        //    using (SqlConnection con = SqlConnectFactory.GetConnection())
        //    {
        //        con.Open();
        //        using (SqlCommand cmd = new SqlCommand())
        //        {
        //            cmd.Connection = con;
        //            SqlParameter planIndexParameter = new SqlParameter("@OrderID", SqlDbType.VarChar) { Value = DBNull.Value };
        //            cmd.Parameters.Add(planIndexParameter);
        //            cmd.Parameters.AddWithValue("@ScaleID", ScaleId);
        //            cmd.Parameters.AddWithValue("@PLU", PLU.PLU);
        //            cmd.Parameters.AddWithValue("@NetWeight", (NetWeight));
        //            cmd.Parameters.AddWithValue("@TareWeight", (TareWeight));
        //            cmd.Parameters.AddWithValue("@ProductDate", ProductDate);
        //            cmd.Parameters.AddWithValue("@Kneading", KneadingNumber);
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                if (reader.HasRows)
        //                {
        //                    while (reader.Read())
        //                    {
        //                        //string sscc             = reader.GetString(0);
        //                        RegDate = reader.GetDateTime(1);
        //                        Id = reader.GetInt32(3);
        //                        XDocument xDoc = XDocument.Parse(reader.GetString(2));
        //                        SsccDirect sscc = new SsccDirect();
        //                        sscc.SSCC = xDoc.Root.Element("Item").Attribute("SSCC").Value;
        //                        sscc.GLN = xDoc.Root.Element("Item").Attribute("GLN").Value;
        //                        sscc.UnitID = int.Parse(xDoc.Root.Element("Item").Attribute("UnitID").Value);
        //                        sscc.UnitType = byte.Parse(xDoc.Root.Element("Item").Attribute("UnitType").Value);
        //                        sscc.SynonymSSCC = xDoc.Root.Element("Item").Attribute("SynonymSSCC").Value;
        //                        sscc.Check = int.Parse(xDoc.Root.Element("Item").Attribute("Check").Value);
        //                        Sscc = sscc;
        //                    }
        //                }
        //                reader.Close();
        //            }
        //        }
        //        con.Close();
        //    }
        //}

        public static WeighingFactDirect New(ScaleDirect scale, PluDirect plu, DateTime productDate, int kneadingNumber,
            int? scaleFactor, decimal netWeight, decimal tareWeight)
        {
            WeighingFactDirect weighingFact = new()
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
            XmlSerializer xmlSerializer = new(typeof(WeighingFactDirect));
            XmlWriterSettings settings = new()
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

            XmlSerializerNamespaces dummyNSs = new();
            dummyNSs.Add(string.Empty, string.Empty);

            using StringWriter textWriter = new();
            using (XmlWriter xw = XmlWriter.Create(textWriter, settings))
            {
                xmlSerializer.Serialize(xw, this, dummyNSs);
            }
            return textWriter.ToString();
        }
    }
}
