// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace DataCore.DAL.TableDirectModels
{
    [Serializable]
    public class PluDirect : BaseSerializeEntity<PluDirect>
    {
        public ScaleDirect Scale { get; set; } = new ScaleDirect();
        public int Id { get; set; } = default;
        public int PLU { get; set; } = default;
        public string RRefGoods { get; set; } = string.Empty;
        public string GoodsName { get; set; } = string.Empty;
        public string GoodsDescription { get; set; } = string.Empty;
        public string GoodsFullName { get; set; } = string.Empty;
        public string GTIN { get; set; } = string.Empty;
        public string EAN13 { get; set; } = string.Empty;
        public string ITF14 { get; set; } = string.Empty;

        public int? GoodsShelfLifeDays { get; set; }
        public decimal GoodsTareWeight { get; set; }
        public decimal GoodsFixWeight { get; set; }
        public int? GoodsBoxQuantly { get; set; }
        /// <summary>
        /// Верхнее значение веса короба.
        /// </summary>
        public decimal UpperWeightThreshold { get; set; }
        /// <summary>
        /// Номинальный вес короба.
        /// </summary>
        public decimal NominalWeight { get; set; }
        /// <summary>
        /// Нижнее значение веса короба.
        /// </summary>
        public decimal LowerWeightThreshold { get; set; }
        /// <summary>
        /// Весовая продукция.
        /// </summary>
        public bool? CheckWeight { get; set; }
        /// <summary>
        /// ID шаблона.
        /// </summary>
        public int? TemplateID { get; set; }

        //  [XmlIgnoreAttribute]
        public TemplateDirect Template { get; set; }

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

        public PluDirect()
        {
            Load();
        }

        public PluDirect(ScaleDirect scale, int plu)
        {
            Scale = scale;
            PLU = plu;
            Load();
        }

        public XDocument GetBtXmlNamedSubString()
        {
            IDictionary<string, object> dict = ObjectToDictionary(this);

            XDocument doc = new(
                new XElement("XMLScript", new XAttribute("Version", "2.0"),
                new XElement("Command",
                new XElement("Print",
                    new XElement("Format", new XAttribute("TemplateID", TemplateID)),

                    dict.Select(x => new XElement("NameSubString",
                        new XAttribute("Key", x.Key),
                        new XElement("Value", x.Value)))

                ))));

            return doc;
        }

        public static IDictionary<string, object> ObjectToDictionary<T>(T item)
            where T : class
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

        public static T ObjectFromDictionary<T>(IDictionary<string, object> dict)
            where T : class
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
            StringBuilder sb = new();
            sb.Append($"({PLU})");
            sb.Append($" {GoodsName}");
            //sb.Append($"GoodsDescription : {this.GoodsDescription};\n");
            //sb.Append($"GoodsFullName : {this.GoodsFullName};\n");
            //sb.Append($"GTIN : {this.GTIN};\n");
            //sb.Append($"GLN : {this.GLN};\n");
            //sb.Append($"GoodsShelfLifeDays : {this.GoodsShelfLifeDays};\n");
            //sb.Append($"GoodsTareWeight : {this.GoodsTareWeight};\n");
            //sb.Append($"GoodsBoxQuantly : {this.GoodsBoxQuantly};\n");
            //sb.Append($"TemplateName : {this.TemplateID};\n");
            return sb.ToString();
        }

        public void LoadTemplate()
        {
            if (TemplateID != null)
            {
                Template = new TemplateDirect((int)TemplateID);
            }
            else
            {
                //Scale.Load();
                Template = new TemplateDirect(Scale.TemplateIdDefault);
            }

        }

        public SqlCommand GetLoadCmd(SqlConnection con)
        {
            if (con == null || Scale == null)
                return null;
            string query = @"
select
	 [Id]
	,[GoodsName]
	,[GoodsFullName]
	,[GoodsDescription]
	,[TemplateID]
	,[GTIN]
	,[EAN13]
	,[ITF14]
	,[GoodsShelfLifeDays]
	,[GoodsTareWeight]
	,[GoodsBoxQuantly]
	,[RRefGoods]
	,[PLU]
	,[UpperWeightThreshold]
	,[NominalWeight]			
	,[LowerWeightThreshold]
	,[CheckWeight]

from [db_scales].[GetPLUByID] (@ScaleID, @PLU)
                    ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
            SqlCommand cmd = new(query, con);
            cmd.Parameters.Add(new SqlParameter("@ScaleID", SqlDbType.Int) { Value = Scale.Id });
            cmd.Parameters.Add(new SqlParameter("@PLU", SqlDbType.Int) { Value = PLU });
            cmd.Prepare();
            return cmd;
        }

        public void Load()
        {
            if (Id == default) return;
            using SqlConnection con = SqlConnectFactory.GetConnection();
            con.Open();
            SqlCommand cmd = GetLoadCmd(con);
            if (cmd != null)
            {
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Id = SqlConnectFactory.GetValueAsNotNullable<int>(reader, "Id");
                        GoodsName = SqlConnectFactory.GetValueAsString(reader, "GoodsName");
                        GoodsFullName = SqlConnectFactory.GetValueAsString(reader, "GoodsFullName");
                        GoodsDescription = SqlConnectFactory.GetValueAsString(reader, "GoodsDescription");
                        TemplateID = SqlConnectFactory.GetValueAsNotNullable<int>(reader, "TemplateID");
                        GTIN = SqlConnectFactory.GetValueAsString(reader, "GTIN");
                        EAN13 = SqlConnectFactory.GetValueAsString(reader, "EAN13");
                        ITF14 = SqlConnectFactory.GetValueAsString(reader, "ITF14");
                        GoodsShelfLifeDays = SqlConnectFactory.GetValueAsNotNullable<byte>(reader, "GoodsShelfLifeDays");
                        GoodsTareWeight = SqlConnectFactory.GetValueAsNotNullable<decimal>(reader, "GoodsTareWeight");
                        GoodsBoxQuantly = SqlConnectFactory.GetValueAsNotNullable<int>(reader, "GoodsBoxQuantly");
                        //RRefGoods = SqlConnectFactory.GetValueAsString(reader, "RRefGoods");
                        //PLU = SqlConnectFactory.GetValueAsNotNullable<int>(reader, "PLU");
                        UpperWeightThreshold = SqlConnectFactory.GetValueAsNotNullable<decimal>(reader, "UpperWeightThreshold");
                        NominalWeight = SqlConnectFactory.GetValueAsNotNullable<decimal>(reader, "NominalWeight");
                        LowerWeightThreshold = SqlConnectFactory.GetValueAsNotNullable<decimal>(reader, "LowerWeightThreshold");
                        CheckWeight = SqlConnectFactory.GetValueAsNotNullable<bool>(reader, "CheckWeight");
                    }
                }
                reader.Close();
            }
            con.Close();
        }

        public static SqlCommand GetPluListCmd(SqlConnection con, int scaleId)
        {
            if (con == null)
                return null;
            string query = @"
select
     [Id]
    ,[GoodsName]
    ,[GoodsFullName]
    ,[GoodsDescription]
    ,[TemplateID]
    ,[GTIN]
    ,[EAN13]
    ,[ITF14]
    ,[GoodsShelfLifeDays]
    ,[GoodsTareWeight]
    ,[GoodsBoxQuantly]
    ,[RRefGoods]
	,[PLU]
	,[UpperWeightThreshold]
	,[NominalWeight]			
	,[LowerWeightThreshold]
	,[CheckWeight]
from [db_scales].[GetPLU] (@ScaleID)
order by [PLU]
                    ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
            SqlCommand cmd = new(query, con);
            cmd.Parameters.Add(new SqlParameter("@ScaleID", SqlDbType.Int) { Value = scaleId });
            cmd.Prepare();
            return cmd;
        }

        public static List<PluDirect> GetPluList(ScaleDirect scale)
        {
            List<PluDirect> result = new();
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                if (scale != null)
                {
                    SqlCommand cmd = GetPluListCmd(con, scale.Id);
                    if (cmd != null)
                    {
                        using SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                PluDirect pluEntity = new()
                                {
                                    Scale = scale,
                                    //ScaleId = SqlConnectFactory.GetValueAsString(reader, "GoodsName");
                                    Id = SqlConnectFactory.GetValueAsNotNullable<int>(reader, "Id"),
                                    GoodsName = SqlConnectFactory.GetValueAsString(reader, "GoodsName"),
                                    GoodsFullName = SqlConnectFactory.GetValueAsString(reader, "GoodsFullName"),
                                    GoodsDescription = SqlConnectFactory.GetValueAsString(reader, "GoodsDescription"),
                                    TemplateID = SqlConnectFactory.GetValueAsNotNullable<int>(reader, "TemplateID"),
                                    GTIN = SqlConnectFactory.GetValueAsString(reader, "GTIN"),
                                    EAN13 = SqlConnectFactory.GetValueAsString(reader, "EAN13"),
                                    ITF14 = SqlConnectFactory.GetValueAsString(reader, "ITF14"),
                                    GoodsShelfLifeDays = SqlConnectFactory.GetValueAsNotNullable<byte>(reader, "GoodsShelfLifeDays"),
                                    GoodsTareWeight = SqlConnectFactory.GetValueAsNotNullable<decimal>(reader, "GoodsTareWeight"),
                                    GoodsBoxQuantly = SqlConnectFactory.GetValueAsNotNullable<int>(reader, "GoodsBoxQuantly"),
                                    //RRefGoods = SqlConnectFactory.GetValueAsString(reader, "RRefGoods"),
                                    PLU = SqlConnectFactory.GetValueAsNotNullable<int>(reader, "PLU"),
                                    UpperWeightThreshold = SqlConnectFactory.GetValueAsNotNullable<decimal>(reader, "UpperWeightThreshold"),
                                    NominalWeight = SqlConnectFactory.GetValueAsNotNullable<decimal>(reader, "NominalWeight"),
                                    LowerWeightThreshold = SqlConnectFactory.GetValueAsNotNullable<decimal>(reader, "LowerWeightThreshold"),
                                    CheckWeight = SqlConnectFactory.GetValueAsNotNullable<bool>(reader, "CheckWeight")
                                };
                                result.Add(pluEntity);
                            }
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
            return result;
        }
    }
}