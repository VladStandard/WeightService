// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Xml.Serialization;
using DataCore.Sql.Models;
using WebApiCore.Utils;

namespace WebApiCore.Common
{
    [XmlRoot(TerraConsts.Response, Namespace = "", IsNullable = false)]
    public class SqlSimpleV4Entity : BaseSerializeDeprecatedEntity<SqlSimpleV4Entity>
    {
        #region Public and private fields and properties

        [XmlArray(TerraConsts.Items)]
        [XmlArrayItem(TerraConsts.Simple, typeof(SqlSimpleV1Entity))]
        public List<SqlSimpleV1Entity> Simples { get; set; }

        #endregion

        #region Constructor and destructor

        public SqlSimpleV4Entity()
        {
            //
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string result = string.Empty;
            if (Simples?.Count > 0)
            {
                foreach (SqlSimpleV1Entity item in Simples)
                {
                    result += item.SerializeAsText() + Environment.NewLine;
                }
            }
            return result;
        }

        #endregion
    }
}
