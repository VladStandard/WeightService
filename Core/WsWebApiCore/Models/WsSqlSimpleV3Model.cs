namespace WsWebApiCore.Models;

[XmlRoot(WsWebConstants.Response, Namespace = "", IsNullable = false)]
public sealed class WsSqlSimpleV3Model : SerializeBase
{
    #region Public and private fields, properties, constructor

    [XmlElement(WsWebConstants.Simple)]
    public List<WsSqlSimpleV1Model> Simples { get; set; }

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