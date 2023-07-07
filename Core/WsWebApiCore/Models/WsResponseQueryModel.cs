// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Models;

[XmlRoot(WsWebConstants.Query, Namespace = "", IsNullable = false)]
public sealed class WsResponseQueryModel : SerializeBase
{
    #region Public and private fields, properties, constructor

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
}