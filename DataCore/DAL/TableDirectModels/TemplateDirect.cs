// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace DataCore.DAL.TableDirectModels
{
    [Serializable]
    public class TemplateDirect : BaseSerializeDeprecatedEntity<TemplateDirect>
    {
        #region Public and private fields and properties

        public string Title { get; set; } = string.Empty;
        public string XslContent { get; set; } = string.Empty;
        public long? TemplateId { get; set; }
        [XmlIgnore]
        public string CategoryId { get; set; } = string.Empty;
        [XmlIgnore]
        public Dictionary<string, string> Fonts { get; set; } = new Dictionary<string, string>();
        [XmlIgnore]
        public Dictionary<string, string> Logo { get; set; } = new Dictionary<string, string>();

        #endregion

        #region Constructor and destructor

        public TemplateDirect()
        {
            CategoryId = string.Empty;
            Title = string.Empty;
            XslContent = string.Empty;
            Fonts = new Dictionary<string, string>();
            Logo = new Dictionary<string, string>();
        }

        public TemplateDirect(long? templateId) : this()
        {
            TemplateId = templateId;
            GetTemplateObjFromDb();
        }

        public TemplateDirect(string title) : this()
        {
            GetTemplateObjFromDb(title);
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
            GetTemplateObjFromDb();
        }

        private void GetTemplateObjFromDb()
        {
            using SqlConnection con = SqlConnect.GetConnection();
            con.Open();
            string query = "SELECT TOP(1) CategoryID,Title,XslContent FROM [db_scales].[GetTemplatesObjByID] (@TemplateID);";
            using (SqlCommand cmd = new(query))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@TemplateID", TemplateId);
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CategoryId = reader.GetString(0);
                        Title = reader.GetString(1);
                        XslContent = reader.GetString(2);
                    }
                }
                reader.Close();
            }
            con.Close();

            // Ресурсы принадлежат весам и должны загружатся 
            // оттуда. Из программы управления устройствами 
            // Тут они не нужны.
            // потому и закомментировано
            // 
            //using (SqlConnection con = SqlConnect.GetConnection())
            //{
            //    string query = "SELECT [Name],MAX([ImageData]) [ImageData] FROM [db_scales].[GetTemplateResources] (@TemplateID,@Type) GROUP BY [Name]; ";
            //    using (var cmd = new SqlCommand(query))
            //    {
            //        Logo.Clear();
            //        cmd.Connection = con;
            //        cmd.Parameters.AddWithValue("@TemplateID", this.TemplateId);
            //        cmd.Parameters.AddWithValue("@Type", "GRF");
            //        con.Open();
            //        var reader = cmd.ExecuteReader();
            //        while (reader.Read())
            //        {
            //            Logo.Add(reader.GetString(0), reader.GetString(1));
            //        }
            //        reader.Close();
            //    }
            //}

            //using (SqlConnection con = SqlConnect.GetConnection())
            //{
            //    string query = "SELECT [Name], [ImageData] FROM [db_scales].[GetTemplateResources] (@TemplateID,@Type); ";
            //    using (SqlCommand cmd = new SqlCommand(query))
            //    {
            //        Fonts.Clear();
            //        cmd.Connection = con;
            //        cmd.Parameters.AddWithValue("@TemplateID", this.TemplateId);
            //        cmd.Parameters.AddWithValue("@Type", "TTF");
            //        con.Open();
            //        var reader = cmd.ExecuteReader();
            //        while (reader.Read())
            //        {
            //            Fonts.Add(reader.GetString(0), reader.GetString(1));
            //        }
            //        reader.Close();
            //    }
            //}
        }

        private void GetTemplateObjFromDb(string title)
        {
            using SqlConnection con = SqlConnect.GetConnection();
            con.Open();
            string query = @"
select top (1) [Id], [CategoryId], convert(nvarchar(max),[ImageData],0) [XslContent]
from [db_scales].[Templates]
where [Title] = @Title
                    ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
            using (SqlCommand cmd = new(query))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Title", title);
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        TemplateId = reader.GetInt32(0);
                        CategoryId = reader.GetString(1);
                        XslContent = reader.GetString(2);
                        Title = title;
                    }
                }
                reader.Close();
            }
            con.Close();
        }

        #endregion
    }
}
