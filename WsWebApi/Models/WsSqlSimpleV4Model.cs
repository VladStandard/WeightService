// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Serialization.Models;

namespace WsWebApi.Models;

[XmlRoot(WebConstants.Response, Namespace = "", IsNullable = false)]
public class WsSqlSimpleV4Model : SerializeBase
{
    #region Public and private fields and properties

    [XmlArray(WebConstants.Items), XmlArrayItem(WebConstants.Simple, typeof(WsSqlSimpleV1Model))]
    public List<WsSqlSimpleV1Model> Simples { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlSimpleV4Model()
    {
        Simples = new();
    }

    #endregion

    #region Public and private methods

    public override string ToString()
    {
        string result = string.Empty;
        if (Simples?.Count > 0)
        {
            foreach (WsSqlSimpleV1Model item in Simples)
            {
                result += DataFormatUtils.SerializeAsText<string>(item) + Environment.NewLine;
            }
        }
        return result;
    }

    #endregion
}