// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Xml.Serialization;
using DataCore.Sql.Models;
using WebApiCore.Utils;

namespace WebApiCore.Common
{
    [XmlRoot(TerraConsts.Info, Namespace = "", IsNullable = false)]
    public class ServiceReplyEntity : BaseSerializeDeprecatedEntity<ServiceReplyEntity>
    {
        #region Public and private fields and properties

        public string Message { get; set; } = string.Empty;

        #endregion

        #region Constructor and destructor

        public ServiceReplyEntity(string message)
        {
            Message = message;
        }

        public ServiceReplyEntity()
        {
            //
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return
                @$"{nameof(Message)}: {Message}. ";
        }

        #endregion
    }
}
