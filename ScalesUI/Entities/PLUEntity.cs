using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ScalesUI.Entities
{
    [Serializable]
    public class PluEntity
    {
        public int Id { get; set; }
        public int PLU { get; set; }
        public string RRefGoods { get; set; }
        public string ScaleId { get; set; }
        public string GoodsName { get; set; }
        public string GoodsDescription { get; set; }
        public string GoodsFullName { get; set; }
        public string GTIN { get; set; }
        public string EAN13 { get; set; }
        public string ITF14 { get; set; }
        
        public int? GoodsShelfLifeDays   { get; set; }
        public decimal GoodsTareWeight  { get; set; }
        public int? GoodsBoxQuantly      { get; set; }

        public string TemplateID { get; set; }
        
        [XmlIgnoreAttribute]
        public TemplateEntity Template { get; set; }

        public int GLN { get; set; }
        public string ConsumerName { get; set; }

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

        public PluEntity ()
        {
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
                Template = new TemplateEntity(TemplateID);
        }
    }
}
