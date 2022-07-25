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
    /// <summary>
    /// Table WeighingFact.
    /// </summary>
    public class WeighingFactDirect : BaseSerializeEntity
    {
        #region Public and private fields and properties

        public long Id { get; set; }
        public TemplateDirect Template { get; set; } = new();
        public ScaleEntity Scale { get; set; }
        public string ProductSeries { get; set; } = string.Empty;
        public PluDirect PLU { get; set; }
        public DateTime ProductDate { get; set; }
        public int KneadingNumber { get; set; }
        public decimal NetWeight { get; set; }
        public decimal TareWeight { get; set; }
        public int? ScaleFactor { get; set; }
        public DateTime RegDate { get; set; }
        public SsccDirect Sscc { get; set; }
        
        public DateTime ExpirationDate
        {
            get => ProductDate.AddDays(PLU.GoodsShelfLifeDays ?? 30);
            // This code need for print labels.
            set => _ = value;
        }
        public string ProductDateBarCode
        {
            get => $"{ProductDate:yyMMdd}";
            // This code need for print labels.
            set => _ = value;
        }
        public string ProductTimeBarCode
        {
            get => $"{ProductDate:HHmmss}";
            // This code need for print labels.
            set => _ = value;
        }
        public string ProductDateRus
        {
            get => $"{ProductDate:dd.MM.yyyy}";
            // This code need for print labels.
            set => _ = value;
        }
        public string LotNumberPretty
        {
            get => $"{ProductDate:yyMM}";
            // This code need for print labels.
            set => _ = value;
        }
        public string ProductTimePrettyBarCode
        {
            get => $"{ProductDate:HHmm}";
            // This code need for print labels.
            set => _ = value;
        }
        public string ProductTimePrettyRus
        {
            get => $"{ProductDate:HH:mm}";
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
        public string NetWeightKgPretty2Dot3Eng
        {
            get => $"{NetWeight:00.000}".Replace(',', '.');
            // This code need for print labels.
            set => _ = value;
        }
        public string NetWeightKgPretty2Dot3Rus
        {
            get => $"{NetWeight:#0.000}".Replace('.', ',');
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
        public string ProductSscc
        {
            get => Sscc.SSCC;
            // This code need for print labels.
            set => _ = value;
        }

        #endregion

        #region Constructor and destructor

        public WeighingFactDirect()
        {
            NetWeight = 0;
            TareWeight = 0;
            ScaleFactor = 1000;
            RegDate = DateTime.Now;
            ProductDate = DateTime.Now.Date;
            PLU = new();
            Template = new();
            Scale = new();
            ProductSeries = string.Empty;
            Sscc = new();
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
            Sscc = new();
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
                XElement? element = xDoc.Root?.Element("Item");
                if (element != null)
                {
                    if (element.Attribute("SSCC") is { } attributeSscc)
                        sscc.SSCC = attributeSscc.Value;
                    if (element.Attribute("GLN") is { } attributeGln)
                        sscc.GLN = attributeGln.Value;
                    if (element.Attribute("UnitID") is { } attributeUnitId)
                        sscc.UnitID = int.Parse(attributeUnitId.Value);
                    if (element.Attribute("UnitType") is { } attributeUnitType)
                        sscc.UnitType = byte.Parse(attributeUnitType.Value);
                    //if (element.Attribute("SynonymSSCC") is { } attributeSynonymSscc)
                    //    sscc.SynonymSSCC = attributeSynonymSscc.Value;
                    if (element.Attribute("Check") is { } attributeCheck)
                        sscc.Check = int.Parse(attributeCheck.Value);
                }

                Sscc = sscc;
            }
        }

        public void Save()
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new("@OrderID", SqlDbType.VarChar, 38) { Value = DBNull.Value },
                new("@ScaleID", SqlDbType.VarChar, 38) { Value = Scale.IdentityId },
                new("@PLU", SqlDbType.Int) { Value = PLU.PLU },
                new("@NetWeight", SqlDbType.Decimal) { Value = NetWeight },
                new("@TareWeight", SqlDbType.Decimal) { Value = TareWeight },
                new("@ProductDate", SqlDbType.Date) { Value = ProductDate },
                new("@Kneading", SqlDbType.Int) { Value = KneadingNumber },
            };
            SqlConnect.ExecuteReader(SqlQueries.DbScales.Tables.WeithingFacts.Save, parameters, SaveReader);
        }

        #endregion
    }
}
