// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using System.Xml.Serialization;
using WebApiTerra1000.Utils;

namespace WebApiTerra1000.Common
{
    [XmlRoot(TerraConsts.Simple, Namespace = "", IsNullable = false)]
    public class SqlSimpleEntity : BaseSerializeEntity<SqlSimpleEntity>
    {
        #region Public and private fields and properties

        [XmlAttribute("Description")]
        public string Description { get; set; } = string.Empty;

        #endregion

        #region Constructor and destructor

        public SqlSimpleEntity(string description)
        {
            Description = description;
        }

        public SqlSimpleEntity()
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
