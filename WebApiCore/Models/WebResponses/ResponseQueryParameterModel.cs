// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Xml.Serialization;
using DataCore.Sql.Models;
using Microsoft.Data.SqlClient;
using WebApiCore.Utils;

namespace WebApiCore.Models.WebResponses;

[XmlRoot(WebConstants.QueryParameter, Namespace = "", IsNullable = false)]
public class ResponseQueryParameterModel : SerializeBase
{
    #region Public and private fields and properties

    [XmlAttribute(nameof(Name))]
    public string Name { get; set; }
    [XmlAttribute(nameof(Value))]
    public string Value { get; set; }

    public ResponseQueryParameterModel(string query, string value)
    {
        Name = query;
        Value = value;
    }

    public ResponseQueryParameterModel(SqlParameter sqlParameter)
    {
        Name = sqlParameter.ParameterName;
        Value = sqlParameter.Value.ToString() ?? $"<{nameof(string.Empty)}>";
    }

    public ResponseQueryParameterModel()
    {
        Name = string.Empty;
        Value = string.Empty;
    }

    #endregion

    #region Public and private methods

    public override string ToString()
    {
        return
            $"{nameof(Name)}: {Name}. " +
            $"{nameof(Value)}: {Value}. ";
    }

    #endregion
}
