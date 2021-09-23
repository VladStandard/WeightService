// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using System.Xml.Serialization;
using WebApiTerra1000.Utils;

namespace WebApiTerra1000.Common
{
    [XmlRoot(TerraConsts.Simple, Namespace = "", IsNullable = false)]
    public class SqlSimpleV1Entity : BaseSerializeEntity<SqlSimpleV1Entity>
    {
        #region Public and private fields and properties

        [XmlAttribute("Description")]
        public string Description { get; set; } = string.Empty;

        #endregion

        #region Constructor and destructor

        public SqlSimpleV1Entity(string description)
        {
            Description = description;
        }

        public SqlSimpleV1Entity()
        {
            //
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return @$"{nameof(Description)}: {Description}. ";
        }

        #endregion
    }
}
