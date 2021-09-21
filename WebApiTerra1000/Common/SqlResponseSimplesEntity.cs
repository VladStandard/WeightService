// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using System;
using System.Xml.Serialization;
using WebApiTerra1000.Utils;

namespace WebApiTerra1000.Common
{
    [XmlRoot(TerraConsts.Response, Namespace = "", IsNullable = false)]
    public class SqlResponseSimplesEntity : BaseSerializeEntity<SqlResponseSimplesEntity>
    {
        #region Public and private fields and properties

        //[XmlArray(TerraConsts.Simple)]
        [XmlArrayItem(TerraConsts.Simple, typeof(SqlSimpleEntity))]
        public SqlSimpleEntity[] Simples { get; set; } = Array.Empty<SqlSimpleEntity>();

        #endregion

        #region Constructor and destructor

        public SqlResponseSimplesEntity()
        {
            //
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string result = string.Empty;
            foreach (SqlSimpleEntity item in Simples)
            {
                result += item.SerializeAsText() + Environment.NewLine;
            }
            return result;
        }

        //public static new SqlResponseItemsEntity DeserializeFromXml(string xml)
        //{
        //    XmlSerializer xmlSerializer = new(typeof(SqlResponseItemsEntity));
        //    return (SqlResponseItemsEntity)xmlSerializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xml)));
        //}

        #endregion
    }
}
