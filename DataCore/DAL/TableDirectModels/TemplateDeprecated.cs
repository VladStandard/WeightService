//using ScalesLib.Sql.Helpers;
//using System;
//using System.Collections.Generic;
//using Microsoft.Data.SqlClient;
//using System.IO;
//using System.Reflection;
//using System.Text;
//using System.Xml.Serialization;

//namespace DataCore.DAL.TableDirectModels
//{

//    [Serializable]
//    public class TemplateEntity
//    {
//        // Помощник SQL.
//        private SqlHelper _sql { get; set; } = SqlHelper.Instance;

//        public string Title { get; set; }
//        public string XslContent { get; set; }
//        public string TemplateId { get; set; }

//        [XmlIgnoreAttribute]
//        public string CategoryId { get; set; }

//        [XmlIgnoreAttribute]
//        public Dictionary<string, string> Fonts { get; set; }

//        [XmlIgnoreAttribute]
//        public Dictionary<string, string> Logo { get; set; }

//        private void Init()
//        {
//            CategoryId = string.Empty;
//            Title = string.Empty;
//            XslContent = string.Empty;
//            Fonts = new Dictionary<string, string>();
//            Logo = new Dictionary<string, string>();
//        }

//        public override bool Equals(object obj)
//        {
//            if (obj is TemplateEntity item)
//            {
//                return TemplateId.Equals(item.TemplateId);
//            }
//            return false;
//        }

//        public override int GetHashCode()
//        {
//            return TemplateId.GetHashCode();
//        }

//        public override string ToString()
//        {
//            var sb = new StringBuilder();
//            sb.Append($"{CategoryId}/{Title}");
//            return sb.ToString();
//        }

//        public TemplateEntity()
//        {
//            Init();
//        }

//        public TemplateEntity(string templateId) : this()
//        {
//            TemplateId = templateId;
//            GetTemplateObjFromDB();
//        }

//        public static IDictionary<string, object> ObjectToDictionary<T>(T item) where T : class
//        {
//            Type myObjectType = item.GetType();
//            IDictionary<string, object> dict = new Dictionary<string, object>();
//            var indexer = new object[0];
//            PropertyInfo[] properties = myObjectType.GetProperties();
//            foreach (var info in properties)
//            {
//                var value = info.GetValue(item, indexer);
//                dict.Add(info.Name, value);
//            }
//            return dict;
//        }

//        public static T ObjectFromDictionary<T>(IDictionary<string, object> dict)
//            where T : class
//        {
//            Type type = typeof(T);
//            T result = (T)Activator.CreateInstance(type);
//            foreach (var item in dict)
//            {
//                type.GetProperty(item.Key)?.SetValue(result, item.Value, null);
//            }
//            return result;
//        }

//        public string SerializeObject()
//        {
//            var xmlSerializer = new XmlSerializer(typeof(TemplateEntity));
//            using (var textWriter = new StringWriter())
//            {
//                xmlSerializer.Serialize(textWriter, this);
//                return textWriter.ToString();
//            }
//        }

//        public void Load()
//        {
//            if (TemplateId != null)
//            {
//                GetTemplateObjFromDB();
//            }
//        }

//        private void GetTemplateObjFromDB()
//        {
//            using (var con = new SqlConnection(_sql.ConnectionString))
//            {
//                var query = "SELECT TOP(1) CategoryID,Title,XslContent FROM [db_scales].[GetTemplatesObjByID] (@TemplateID);";
//                using (var cmd = new SqlCommand(query))
//                {
//                    cmd.Connection = con;
//                    cmd.Parameters.AddWithValue("@TemplateID", TemplateId);
//                    con.Open();
//                    var reader = cmd.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        CategoryId = reader.GetString(0);
//                        Title = reader.GetString(1);
//                        XslContent = reader.GetString(2);
//                    }
//                    reader.Close();
//                    con.Close();
//                }

//                query = "SELECT [Name],	[ImageData] FROM [db_scales].[GetTemplateResources] (@TemplateID,@Type); ";
//                using (var cmd = new SqlCommand(query))
//                {
//                    Logo.Clear();
//                    cmd.Connection = con;
//                    cmd.Parameters.AddWithValue("@TemplateID", this.TemplateId);
//                    cmd.Parameters.AddWithValue("@Type", "GRF");
//                    con.Open();
//                    var reader = cmd.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        Logo.Add(reader.GetString(0), reader.GetString(1));
//                    }
//                    reader.Close();
//                    con.Close();
//                }

//                query = "SELECT [Name],	[ImageData] FROM [db_scales].[GetTemplateResources] (@TemplateID,@Type); ";
//                using (SqlCommand cmd = new SqlCommand(query))
//                {
//                    Fonts.Clear();
//                    cmd.Connection = con;
//                    cmd.Parameters.AddWithValue("@TemplateID", this.TemplateId);
//                    cmd.Parameters.AddWithValue("@Type", "TTF");
//                    con.Open();
//                    var reader = cmd.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        Fonts.Add(reader.GetString(0), reader.GetString(1));
//                    }
//                    reader.Close();
//                    con.Close();
//                }



//            }
//        }

//        private void GetTemplateFromDB(string TemplateID)
//        {
//            using (var con = new SqlConnection(_sql.ConnectionString))
//            {
//                var query = "SELECT [db_scales].[GetTemplateByID] ( @ID )";
//                using (var cmd = new SqlCommand(query))
//                {
//                    cmd.Connection = con;
//                    cmd.Parameters.AddWithValue("@ID", TemplateID);
//                    con.Open();
//                    //var buf = (byte[])cmd.ExecuteScalar();
//                    //var tmplate = Encoding.UTF8.GetString(buf, 0, buf.Length);
//                    con.Close();
//                }
//            }
//        }
//    }
//}
