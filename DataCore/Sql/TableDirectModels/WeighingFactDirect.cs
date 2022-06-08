// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels;
using DataCore.Sql.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Xml.Linq;

namespace DataCore.Sql.TableDirectModels
{
    public class WeighingFactDirect : BaseSerializeEntity
    {
        #region Public and private fields and properties

        public long Id { get; set; } = default;
        public TemplateDirect Template { get; set; } = new TemplateDirect();
        public ScaleEntity Scale { get; set; }
        public string ProductSeries { get; set; } = string.Empty;
        public PluDirect PLU { get; set; }
        public DateTime ProductDate { get; set; }
        public int KneadingNumber { get; set; } = default;
        public decimal NetWeight { get; set; }
        public decimal TareWeight { get; set; }
        public int? ScaleFactor { get; set; }
        public DateTime RegDate { get; set; }
        public SsccDirect Sscc { get; set; }
        
        public DateTime ExpirationDate
        {
            get => ProductDate.AddDays(PLU == null || PLU.GoodsShelfLifeDays == null ? 30 : (int)PLU.GoodsShelfLifeDays);
            // This code need for print labels.
            set => _ = value;
        }
        public string ProductDatePretty
        {
            get => $"{ProductDate:yyMMdd}";
            // This code need for print labels.
            set => _ = value;
        }
        public string LotNumberPretty
        {
            get => $"{ProductDate:yyMM}";
            // This code need for print labels.
            set => _ = value;
        }
        public string ProductTimePretty
        {
            get => $"{ProductDate:HHmm}";
            // This code need for print labels.
            set => _ = value;
        }
        public string NetWeightKgPretty2
        {
            get => $"{NetWeight:00.000}".Replace(',', '.').Split('.')[0];
            // This code need for print labels.
            set => _ = value;
        }
        public string NetWeightKgPretty3
        {
            get => $"{NetWeight:000.000}".Replace(',', '.').Split('.')[0];
            // This code need for print labels.
            set => _ = value;
        }
        public string NetWeightGrPretty2
        {
            get => $"{NetWeight:#.00}".Replace(',', '.').Split('.')[1];
            // This code need for print labels.
            set => _ = value;
        }
        public string NetWeightGrPretty3
        {
            get => $"{NetWeight:#.000}".Replace(',', '.').Split('.')[1];
            // This code need for print labels.
            set => _ = value;
        }
        public decimal GrossWeight => NetWeight + TareWeight;

        #endregion

        #region Constructor and destructor

        public WeighingFactDirect()
        {
            NetWeight = 0;
            TareWeight = 0;
            ScaleFactor = 1000;
            RegDate = DateTime.Now;
            ProductDate = DateTime.Now.Date;
            PLU = new PluDirect();
            Template = new TemplateDirect();
            Scale = new();
            ProductSeries = string.Empty;
            Sscc = new SsccDirect();
        }

        public WeighingFactDirect(ScaleEntity scale, PluDirect plu, DateTime productDate, int kneadingNumber,
            int? scaleFactor, decimal netWeight, decimal tareWeight)
        {
            ScaleFactor = scaleFactor;
            Scale = scale;
            PLU = plu;
            ProductDate = productDate;
            KneadingNumber = kneadingNumber;
            NetWeight = netWeight;
            TareWeight = tareWeight;
            Sscc = new SsccDirect();
        }

        #endregion

        #region Public and private methods

        private void SaveReader(SqlDataReader reader)
        {
            if (reader.Read())
            {
                RegDate = reader.GetDateTime(1);
                Id = reader.GetInt32(3);
                XDocument xDoc = XDocument.Parse(reader.GetString(2));
                SsccDirect sscc = new();
                sscc.SSCC = xDoc.Root.Element("Item").Attribute("SSCC").Value;
                sscc.GLN = xDoc.Root.Element("Item").Attribute("GLN").Value;
                sscc.UnitID = int.Parse(xDoc.Root.Element("Item").Attribute("UnitID").Value);
                sscc.UnitType = byte.Parse(xDoc.Root.Element("Item").Attribute("UnitType").Value);
                sscc.SynonymSSCC = xDoc.Root.Element("Item").Attribute("SynonymSSCC").Value;
                sscc.Check = int.Parse(xDoc.Root.Element("Item").Attribute("Check").Value);
                Sscc = sscc;
            }
        }

        public void Save()
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@OrderID", SqlDbType.VarChar, 38) { Value = DBNull.Value },
                new SqlParameter("@ScaleID", SqlDbType.VarChar, 38) { Value = Scale.IdentityId },
                new SqlParameter("@PLU", SqlDbType.Int) { Value = PLU.PLU },
                new SqlParameter("@NetWeight", SqlDbType.Decimal) { Value = NetWeight },
                new SqlParameter("@TareWeight", SqlDbType.Decimal) { Value = TareWeight },
                new SqlParameter("@ProductDate", SqlDbType.Date) { Value = ProductDate },
                new SqlParameter("@Kneading", SqlDbType.Int) { Value = KneadingNumber },
            };
            SqlConnect.ExecuteReader(SqlQueries.DbScales.Tables.WeithingFacts.Save, parameters, SaveReader);
        }

        //public void SaveDeprecated()
        //{
        //    using (SqlConnection con = SqlConnect.GetConnection())
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

        #endregion
    }
}
