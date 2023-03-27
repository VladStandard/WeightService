// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Serialization.Models;

namespace WsWebApi.Models;

[XmlRoot(WebConstants.Response, Namespace = "", IsNullable = false)]
public class SqlSimpleV3Model : SerializeBase
{
    #region Public and private fields and properties

    [XmlElement(WebConstants.Simple)]
    public List<SqlSimpleV1Model> Simples { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public SqlSimpleV3Model()
    {
        Simples = new();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => 
        Simples.Aggregate(string.Empty, 
            (current, item) => current + (DataFormatUtils.SerializeAsText<string>(item) + Environment.NewLine));

    #endregion
}
