// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Xml.Serialization;
using DataCore.Sql.Models;
using WebApiCore.Utils;

namespace WebApiCore.Common
{
    [XmlRoot(TerraConsts.Exception, Namespace = "", IsNullable = false)]
    public class ServiceExceptionEntity : BaseSerializeDeprecatedEntity<ServiceExceptionEntity>
    {
        #region Public and private fields and properties

        public string FilePath { get; set; }
        public int LineNumber { get; set; }
        public string MemberName { get; set; }
        public string Exception { get; set; }
        public string InnerException { get; set; }

        #endregion

        #region Constructor and destructor

        public ServiceExceptionEntity(string filePath, int lineNumber, string memberName, Exception ex)
        {
            FilePath = filePath;
            LineNumber = lineNumber;
            MemberName = memberName;
            Exception = ex.Message;
            InnerException = ex.InnerException != null ? ex.InnerException.Message : string.Empty;
        }

        public ServiceExceptionEntity()
        {
            //
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return
                @$"{nameof(FilePath)}: {FilePath}. " + Environment.NewLine +
                @$"{nameof(LineNumber)}: {LineNumber}. " + Environment.NewLine +
                @$"{nameof(MemberName)}: {MemberName}. " + Environment.NewLine +
                @$"{nameof(Exception)}: {Exception}. " + Environment.NewLine +
                @$"{nameof(InnerException)}: {InnerException}. ";
        }

        #endregion
    }
}
