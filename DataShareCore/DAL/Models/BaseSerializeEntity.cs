// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml.Serialization;

namespace DataShareCore.DAL.Models
{
    [Serializable]
    public class BaseSerializeEntity<T>
    {
        #region Public and private methods

        public string SerializeObjectAsXml()
        {
            XmlSerializer xmlSerializer = new(typeof(T));
            using StringWriter textWriter = new();
            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();
        }

        public string SerializeObjectAsJson() => JsonConvert.SerializeObject(this);

        #endregion
    }
}
