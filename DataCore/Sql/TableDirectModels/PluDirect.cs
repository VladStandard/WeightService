// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.DataModels;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.Data.SqlClient;
using NHibernate.SqlCommand;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace DataCore.Sql.TableDirectModels
{
    public class PluDirect : BaseSerializeEntity
    {
        #region Public and private fields and properties

        public bool IsCheckWeight { get; set; }
        public decimal GoodsFixWeight { get; set; }
        public decimal GoodsTareWeight { get; set; }
        public decimal LowerWeightThreshold { get; set; }
        public decimal NominalWeight { get; set; }
        public decimal UpperWeightThreshold { get; set; }
        public int PLU { get; set; } = default;
        public int? GoodsBoxQuantly { get; set; }
        public int? GoodsShelfLifeDays { get; set; }
        public long Id { get; set; } = default;
        public long? TemplateId { get; set; }
        public string EAN13 { get; set; } = string.Empty;
        public string GoodsDescription { get; set; } = string.Empty;
        public string GoodsFullName { get; set; } = string.Empty;
        public string GoodsName { get; set; } = string.Empty;
        public string GTIN { get; set; } = string.Empty;
        public string PrettyGtin14
        {
            get
            {
                if (GTIN.Length > 12)
                    return BarcodeHelper.Instance.GetGtin(GTIN[..13]);
                return string.Empty;
            }
            // This code need for print labels.
            set => _ = value;
        }
        public string ITF14 { get; set; } = string.Empty;
        public string RRefGoods { get; set; } = string.Empty;
        public ScaleEntity Scale { get; set; }
        //public TemplateDirect Template { get; set; }

        #endregion

        #region Constructor and destructor

        public PluDirect()
        {
            EAN13 = string.Empty;
            GoodsBoxQuantly = null;
            GoodsDescription = string.Empty;
            GoodsFixWeight = 0;
            GoodsFullName = string.Empty;
            GoodsName = string.Empty;
            GoodsShelfLifeDays = null;
            GoodsTareWeight = 0;
            GTIN = string.Empty;
            Id = 0;
            ITF14 = string.Empty;
            LowerWeightThreshold = 0;
            NominalWeight = 0;
            PLU = 0;
            RRefGoods = string.Empty;
            Scale = new();
            TemplateId = null;
            //Template = new();
            UpperWeightThreshold = 0;
            IsCheckWeight = false;

            Load();
        }

        public PluDirect(ScaleEntity scale, int plu) : this()
        {
            Scale = scale;
            PLU = plu;
            Load();
        }

        #endregion

        #region Public and private methods

        public override bool Equals(object obj)
        {
            if (obj is not PluDirect item)
            {
                return false;
            }

            return Id.Equals(item.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public XDocument GetBtXmlNamedSubString()
        {
            IDictionary<string, object> dict = ObjectToDictionary(this);
            XDocument doc = new(
                new XElement("XMLScript", new XAttribute("Version", "2.0"),
                new XElement("Command",
                new XElement("Print",
                    new XElement("Format", new XAttribute("TemplateID", TemplateId)),
                    dict.Select(x => new XElement("NameSubString",
                        new XAttribute("Key", x.Key),
                        new XElement("Value", x.Value)))
                ))));
            return doc;
        }

        public IDictionary<string, object> ObjectToDictionary<T>(T item) where T : class
        {
            Type myObjectType = item.GetType();
            IDictionary<string, object> dict = new Dictionary<string, object>();
            object[] indexer = new object[0];
            PropertyInfo[] properties = myObjectType.GetProperties();
            foreach (PropertyInfo info in properties)
            {
                object value = info.GetValue(item, indexer);
                dict.Add(info.Name, value);
            }
            return dict;
        }

        public T ObjectFromDictionary<T>(IDictionary<string, object> dict) where T : class
        {
            Type type = typeof(T);
            T result = (T)Activator.CreateInstance(type);
            foreach (KeyValuePair<string, object> item in dict)
            {
                type.GetProperty(item.Key)?.SetValue(result, item.Value, null);
            }
            return result;
        }

        public override string ToString()
        {
            return base.ToString() +
                   $"{nameof(PLU)}: {PLU}. " +
                   $"{nameof(GoodsName)}: {GoodsName}. ";
        }

        public TemplateDirect LoadTemplate()
        {
            TemplateDirect template;
            if (TemplateId == null)
            {
                template = new(Scale.TemplateDefault?.IdentityId);
            }
            else
            {
                template = new(TemplateId);
                //SqlConnect.ExecuteReader(SqlQueries.DbScales.Tables.Templates.GetItem,
                //    new SqlParameter("@ID", SqlDbType.Int) { Value = TemplateId },
                //    (reader) =>
                //    {
                //        if (reader.Read())
                //        {
                //            Template.TemplateId = TemplateId;
                //            Template.CategoryId = SqlConnect.GetValueAsString(reader, "CATEGORYID");
                //            Template.Title = SqlConnect.GetValueAsString(reader, "TITLE");
                //            Template.XslContent = SqlConnect.GetValueAsString(reader, "XSLCONTENT");
                //        }
                //    });
            }
            return template;
        }

        public void Load()
        {
            if (Id == default) return;
            SqlConnect.ExecuteReader(SqlQueries.DbScales.Tables.Plu.GetItem,
                new[]
                {
                    new SqlParameter("@ScaleID", SqlDbType.BigInt) { Value = Scale.IdentityId },
                    new SqlParameter("@PLU", SqlDbType.Int) { Value = PLU },
                },
                (reader) =>
                {
                    while (reader.Read())
                    {
                        Id = SqlConnect.GetValueAsNotNullable<long>(reader, "Id");
                        GoodsName = SqlConnect.GetValueAsString(reader, "GoodsName");
                        GoodsFullName = SqlConnect.GetValueAsString(reader, "GoodsFullName");
                        GoodsDescription = SqlConnect.GetValueAsString(reader, "GoodsDescription");
                        TemplateId = SqlConnect.GetValueAsNotNullable<long>(reader, "TemplateID");
                        GTIN = SqlConnect.GetValueAsString(reader, "GTIN");
                        EAN13 = SqlConnect.GetValueAsString(reader, "EAN13");
                        ITF14 = SqlConnect.GetValueAsString(reader, "ITF14");
                        GoodsShelfLifeDays = SqlConnect.GetValueAsNotNullable<byte>(reader, "GoodsShelfLifeDays");
                        GoodsTareWeight = SqlConnect.GetValueAsNotNullable<decimal>(reader, "GoodsTareWeight");
                        GoodsBoxQuantly = SqlConnect.GetValueAsNotNullable<int>(reader, "GoodsBoxQuantly");
                        //RRefGoods = SqlConnect.GetValueAsString(reader, "RRefGoods");
                        //PLU = SqlConnect.GetValueAsNotNullable<int>(reader, "PLU");
                        UpperWeightThreshold = SqlConnect.GetValueAsNotNullable<decimal>(reader, "UpperWeightThreshold");
                        NominalWeight = SqlConnect.GetValueAsNotNullable<decimal>(reader, "NominalWeight");
                        LowerWeightThreshold = SqlConnect.GetValueAsNotNullable<decimal>(reader, "LowerWeightThreshold");
                        IsCheckWeight = SqlConnect.GetValueAsNotNullable<bool>(reader, "CheckWeight");
                    }
                });
            //LoadTemplate();
        }

        public List<PluDirect> GetPluList(ScaleEntity scale)
        {
            List<PluDirect> result = new();
            if (scale == null)
                return result;
            SqlConnect.ExecuteReader(SqlQueries.DbScales.Tables.Plu.GetItems,
                new SqlParameter("@ScaleID", SqlDbType.BigInt) { Value = scale.IdentityId },
                (reader) =>
                {
                    while (reader.Read())
                    {
                        PluDirect plu = new()
                        {
                            Scale = scale,
                            //ScaleId = SqlConnect.GetValueAsString(reader, "GoodsName");
                            Id = SqlConnect.GetValueAsNotNullable<long>(reader, "Id"),
                            GoodsName = SqlConnect.GetValueAsString(reader, "GoodsName"),
                            GoodsFullName = SqlConnect.GetValueAsString(reader, "GoodsFullName"),
                            GoodsDescription = SqlConnect.GetValueAsString(reader, "GoodsDescription"),
                            TemplateId = SqlConnect.GetValueAsNotNullable<long>(reader, "TemplateID"),
                            GTIN = SqlConnect.GetValueAsString(reader, "GTIN"),
                            EAN13 = SqlConnect.GetValueAsString(reader, "EAN13"),
                            ITF14 = SqlConnect.GetValueAsString(reader, "ITF14"),
                            GoodsShelfLifeDays = SqlConnect.GetValueAsNotNullable<byte>(reader, "GoodsShelfLifeDays"),
                            GoodsTareWeight = SqlConnect.GetValueAsNotNullable<decimal>(reader, "GoodsTareWeight"),
                            GoodsBoxQuantly = SqlConnect.GetValueAsNotNullable<int>(reader, "GoodsBoxQuantly"),
                            //RRefGoods = SqlConnect.GetValueAsString(reader, "RRefGoods"),
                            PLU = SqlConnect.GetValueAsNotNullable<int>(reader, "PLU"),
                            UpperWeightThreshold = SqlConnect.GetValueAsNotNullable<decimal>(reader, "UpperWeightThreshold"),
                            NominalWeight = SqlConnect.GetValueAsNotNullable<decimal>(reader, "NominalWeight"),
                            LowerWeightThreshold = SqlConnect.GetValueAsNotNullable<decimal>(reader, "LowerWeightThreshold"),
                            IsCheckWeight = SqlConnect.GetValueAsNotNullable<bool>(reader, "CheckWeight")
                        };
                        result.Add(plu);
                    }
                }); 
            return result;
        }

        #endregion
    }
}
