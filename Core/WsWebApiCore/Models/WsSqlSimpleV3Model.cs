// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Models;

[XmlRoot(WsWebConstants.Response, Namespace = "", IsNullable = false)]
public class WsSqlSimpleV3Model : SerializeBase
{
    #region Public and private fields and properties

    [XmlElement(WsWebConstants.Simple)]
    public List<WsSqlSimpleV1Model> Simples { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlSimpleV3Model()
    {
        Simples = new();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => 
        Simples.Aggregate(string.Empty, 
            (current, item) => current + (WsDataFormatUtils.SerializeAsText<string>(item) + Environment.NewLine));

    #endregion
}