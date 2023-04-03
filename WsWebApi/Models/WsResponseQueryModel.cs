// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Serialization.Models;

namespace WsWebApi.Models;

[XmlRoot(WebConstants.Query, Namespace = "", IsNullable = false)]
public class WsResponseQueryModel : SerializeBase
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
    public List<WsResponseQueryParameterModel> Parameters { get; set; }

    public WsResponseQueryModel(string query, List<WsResponseQueryParameterModel> parameters)
    {
        _query = query;
        Parameters = parameters;
    }

    public WsResponseQueryModel()
    {
        _query = string.Empty;
        Parameters = new();
    }

    #endregion

    #region Public and private methods

    //

    #endregion
}