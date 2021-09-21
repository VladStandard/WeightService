// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace DataShareCore.DAL.Models
{
    [Serializable()]
    public class BaseSerializeEntity<T> where T : new ()
    {
        #region Public and private methods

        public string SerializeAsJson() => JsonConvert.SerializeObject(this);

        public string SerializeAsXml()
        {
            XmlSerializer xmlSerializer = new(typeof(T));
            using StringWriter textWriter = new();
            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();
        }

        public static T DeserializeFromXml(string xml)
        {
            //T result = new();
            //XmlSerializer xmlSerializer = new(typeof(T));
            //using (TextReader reader = new StringReader(xml))
            //{
            //    result = (T)xmlSerializer.Deserialize(reader);
            //}
            //return result;
            
            //xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + xml;
            XmlSerializer xmlSerializer = new(typeof(T));
            return (T)xmlSerializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xml)));
        }

        public string SerializeAsHtml() => @$"
<html>
    <body>
        {this}
    </body>
</html>"
                .TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public string SerializeAsText() => ToString();

        #endregion
    }
}
