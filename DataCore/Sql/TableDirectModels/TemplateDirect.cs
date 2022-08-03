// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace DataCore.Sql.TableDirectModels
{
    public class TemplateDirect : BaseSerializeEntity
    {
        #region Public and private fields, properties, constructor

        public string Title { get; set; } = string.Empty;
        public string XslContent { get; set; } = string.Empty;
        public long? TemplateId { get; set; }
        [XmlIgnore]
        public string CategoryId { get; set; } = string.Empty;
        [XmlIgnore]
        public Dictionary<string, string> Fonts { get; set; } = new();
        [XmlIgnore]
        public Dictionary<string, string> Logo { get; set; } = new();

        #endregion

        #region Constructor and destructor

        public TemplateDirect()
        {
            CategoryId = string.Empty;
            Title = string.Empty;
            XslContent = string.Empty;
            Fonts = new();
            Logo = new();
        }

        public TemplateDirect(long? templateId) : this()
        {
            SqlUtils.GetTemplateFromDb(this, templateId);
        }

        public TemplateDirect(string title) : this()
        {
            SqlUtils.GetTemplateFromDb(this, title);
        }

        #endregion

        #region Public and private methods

        public override bool Equals(object obj)
        {
            if (obj is TemplateDirect item)
            {
                return TemplateId.Equals(item.TemplateId);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return TemplateId.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append($"{CategoryId}/{Title}");
            return sb.ToString();
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

        public T ObjectFromDictionary<T>(IDictionary<string, object> dict)
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

        public void Load()
        {
            SqlUtils.GetTemplateFromDb(this, TemplateId);
        }

        #endregion
    }
}
