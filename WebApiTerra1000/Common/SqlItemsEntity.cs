// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using System.Xml.Serialization;
using WebApiTerra1000.Utils;

namespace WebApiTerra1000.Common
{
    [XmlRoot(TerraConsts.Items, Namespace = "", IsNullable = false)]
    public class SqlItemsEntity : BaseSerializeEntity<SqlItemsEntity>
    {
        #region Public and private fields and properties

        [XmlElement(TerraConsts.Simple)]
        public SqlSimpleEntity Item { get; set; } = new SqlSimpleEntity();

        #endregion

        #region Constructor and destructor

        public SqlItemsEntity(string description)
        {
            Item = new SqlSimpleEntity(description);
        }

        public SqlItemsEntity()
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
