// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Xml.Serialization;
using DataCore.Sql.Models;
using WebApiCore.Utils;

namespace WebApiCore.Models.WebResponses;

[XmlRoot(WebConstants.Query, Namespace = "", IsNullable = false)]
public class ResponseQueryModel : SerializeBase
{
    #region Public and private fields and properties

    private string _query;
    [XmlElement(nameof(Query), IsNullable = false)]
    public string Query
    {
        get => _query; set => _query = value
        .Replace("\r", " ")
        .Replace("\n", " ")
        .Replace("\t", " ");
    }
    [XmlArray(nameof(Parameters)), XmlArrayItem("Parameter")]
    public List<ResponseQueryParameterModel> Parameters { get; set; }

    public ResponseQueryModel(string query, List<ResponseQueryParameterModel> parameters)
    {
        _query = query;
        Parameters = parameters;
    }

    public ResponseQueryModel()
    {
        _query = string.Empty;
        Parameters = new();
    }

    #endregion

    #region Public and private methods

    //

    #endregion
}
