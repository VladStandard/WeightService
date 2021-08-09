// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace WeightCore.Db
{
    [Serializable]
    public class PluEntity
    {
        public ScaleEntity Scale { get; set; }
        public int Id { get; set; }
        public int PLU { get; set; }
        public string RRefGoods { get; set; }
        public string GoodsName { get; set; }
        public string GoodsDescription { get; set; }
        public string GoodsFullName { get; set; }
        public string GTIN { get; set; }
        public string EAN13 { get; set; }
        public string ITF14 { get; set; }

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
        public decimal NominalWeight  { get; set; }
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
        public TemplateEntity Template { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is PluEntity item))
            {
                return false;
            }

            return Id.Equals(item.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public PluEntity()
        {
            //
        }

        public PluEntity(ScaleEntity scale, int plu)
        {
            Scale = scale;
            PLU = plu;
            Load();
        }

        public XDocument GetBtXmlNamedSubString()
        {
            IDictionary<string, object> dict = ObjectToDictionary<PluEntity>(this);

            XDocument doc = new XDocument(
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
            var indexer = new object[0];
            PropertyInfo[] properties = myObjectType.GetProperties();
            foreach (var info in properties)
            {
                var value = info.GetValue(item, indexer);
                dict.Add(info.Name, value);
            }
            return dict;
        }

        public static T ObjectFromDictionary<T>(IDictionary<string, object> dict)
            where T : class
        {
            Type type = typeof(T);
            T result = (T)Activator.CreateInstance(type);
            foreach (var item in dict)
            {
                type.GetProperty(item.Key)?.SetValue(result, item.Value, null);
            }
            return result;
        }

        public string SerializeObject()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PluEntity));
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, this);
                return textWriter.ToString();
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
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
                Template = new TemplateEntity((int)TemplateID);
            }
            else
            {
                //Scale.Load();
                Template = new TemplateEntity(Scale.TemplateIdDefault);
            }

        }

        public SqlCommand GetLoadCmd(SqlConnection con)
        {
            if (con == null || Scale == null)
                return null;
            var query = @"
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
                    ".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n');
            var cmd = new SqlCommand(query, con);
            cmd.Parameters.Add(new SqlParameter("@ScaleID", SqlDbType.Int) { Value = Scale.Id });
            cmd.Parameters.Add(new SqlParameter("@PLU", SqlDbType.Int) { Value = PLU });
            cmd.Prepare();
            return cmd;
        }

        public void Load()
        {
            using (var con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                var cmd = GetLoadCmd(con);
                if (cmd != null)
                {
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Id = SqlConnectFactory.GetValue<int>(reader, "Id");
                            GoodsName = SqlConnectFactory.GetValue<string>(reader, "GoodsName");
                            GoodsFullName = SqlConnectFactory.GetValue<string>(reader, "GoodsFullName");
                            GoodsDescription = SqlConnectFactory.GetValue<string>(reader, "GoodsDescription");
                            TemplateID = SqlConnectFactory.GetValue<int>(reader, "TemplateID");
                            GTIN = SqlConnectFactory.GetValue<string>(reader, "GTIN");
                            EAN13 = SqlConnectFactory.GetValue<string>(reader, "EAN13");
                            ITF14 = SqlConnectFactory.GetValue<string>(reader, "ITF14");
                            GoodsShelfLifeDays = SqlConnectFactory.GetValue<byte>(reader, "GoodsShelfLifeDays");
                            GoodsTareWeight = SqlConnectFactory.GetValue<decimal>(reader, "GoodsTareWeight");
                            GoodsBoxQuantly = SqlConnectFactory.GetValue<int>(reader, "GoodsBoxQuantly");
                            //RRefGoods = SqlConnectFactory.GetValue<string>(reader, "RRefGoods");
                            //PLU = SqlConnectFactory.GetValue<int>(reader, "PLU");

                            UpperWeightThreshold = SqlConnectFactory.GetValue<decimal>(reader, "UpperWeightThreshold");
                            NominalWeight = SqlConnectFactory.GetValue<decimal>(reader, "NominalWeight");
                            LowerWeightThreshold = SqlConnectFactory.GetValue<decimal>(reader, "LowerWeightThreshold");
                            CheckWeight = SqlConnectFactory.GetValue<bool>(reader, "CheckWeight");

                        }
                    }
                    reader.Close();
                    con.Close();
                }
            }
        }

        public static SqlCommand GetPluListCmd(SqlConnection con, int scaleId)
        {
            if (con == null)
                return null;
            var query = @"
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
                    ".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n');
            var cmd = new SqlCommand(query, con);
            cmd.Parameters.Add(new SqlParameter("@ScaleID", SqlDbType.Int) { Value = scaleId });
            cmd.Prepare();
            return cmd;
        }

        public static List<PluEntity> GetPluList(ScaleEntity scale)
        {
            var result = new List<PluEntity>();
            using (var con = SqlConnectFactory.GetConnection())
            {
                if (scale != null)
                {
                    con.Open();
                    var cmd = GetPluListCmd(con, scale.Id);
                    if (cmd != null)
                    {
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var pluEntity = new PluEntity()
                                {
                                    Scale = scale,
                                    //ScaleId = SqlConnectFactory.GetValue<string>(reader, "GoodsName");
                                    Id = SqlConnectFactory.GetValue<int>(reader, "Id"),
                                    GoodsName = SqlConnectFactory.GetValue<string>(reader, "GoodsName"),
                                    GoodsFullName = SqlConnectFactory.GetValue<string>(reader, "GoodsFullName"),
                                    GoodsDescription = SqlConnectFactory.GetValue<string>(reader, "GoodsDescription"),
                                    TemplateID = SqlConnectFactory.GetValue<int>(reader, "TemplateID"),
                                    GTIN = SqlConnectFactory.GetValue<string>(reader, "GTIN"),
                                    EAN13 = SqlConnectFactory.GetValue<string>(reader, "EAN13"),
                                    ITF14 = SqlConnectFactory.GetValue<string>(reader, "ITF14"),
                                    GoodsShelfLifeDays = SqlConnectFactory.GetValue<byte>(reader, "GoodsShelfLifeDays"),
                                    GoodsTareWeight = SqlConnectFactory.GetValue<decimal>(reader, "GoodsTareWeight"),
                                    GoodsBoxQuantly = SqlConnectFactory.GetValue<int>(reader, "GoodsBoxQuantly"),
                                    //RRefGoods = SqlConnectFactory.GetValue<string>(reader, "RRefGoods"),
                                    PLU = SqlConnectFactory.GetValue<int>(reader, "PLU"),
                                    UpperWeightThreshold = SqlConnectFactory.GetValue<decimal>(reader, "UpperWeightThreshold"),
                                    NominalWeight = SqlConnectFactory.GetValue<decimal>(reader, "NominalWeight"),
                                    LowerWeightThreshold = SqlConnectFactory.GetValue<decimal>(reader, "LowerWeightThreshold"),
                                    CheckWeight = SqlConnectFactory.GetValue<bool>(reader, "CheckWeight")

                            };
                            result.Add(pluEntity);
                            }
                        }
                        reader.Close();
                    }
                }
            }
            return result;
        }
    }
}