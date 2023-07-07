// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Serialization;

namespace WsWebApiCore.Models;

[XmlRoot(WsWebConstants.Response, Namespace = "", IsNullable = false)]
public sealed class WsSqlSimpleV4Model : SerializeBase
{
    #region Public and private fields, properties, constructor

    [XmlArray(WsWebConstants.Items), XmlArrayItem(WsWebConstants.Simple, typeof(WsSqlSimpleV1Model))]
    public List<WsSqlSimpleV1Model> Simples { get; set; }

    public WsSqlSimpleV4Model()
    {
        Simples = new();
    }

    #endregion

    #region Public and private methods

    public override string ToString()
    {
        string result = string.Empty;
        if (Simples.Any())
            foreach (WsSqlSimpleV1Model item in Simples)
            {
                result += WsDataFormatUtils.SerializeAsText<string>(item) + Environment.NewLine;
            }
        return result;
    }

    #endregion
}