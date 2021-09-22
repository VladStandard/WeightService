// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using System.Xml.Serialization;
using WebApiTerra1000.Utils;

namespace WebApiTerra1000.Common
{
    [XmlRoot(TerraConsts.Response, Namespace = "", IsNullable = false)]
    public class SqlSimpleV2Entity : BaseSerializeEntity<SqlSimpleV2Entity>
    {
        #region Public and private fields and properties

        [XmlElement(TerraConsts.Simple)]
        public SqlSimpleV1Entity Item { get; set; } = new SqlSimpleV1Entity();

        #endregion

        #region Constructor and destructor

        public SqlSimpleV2Entity(string description)
        {
            Item = new SqlSimpleV1Entity(description);
        }

        public SqlSimpleV2Entity()
        {
            //
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return @$"{nameof(Item)}: {Item}";
        }

        #endregion
    }
}
