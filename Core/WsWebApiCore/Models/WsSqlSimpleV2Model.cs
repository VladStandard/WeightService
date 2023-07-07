// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Serialization;

namespace WsWebApiCore.Models;

[XmlRoot(WsWebConstants.Response, Namespace = "", IsNullable = false)]
public sealed class WsSqlSimpleV2Model : SerializeBase
{
    #region Public and private fields, properties, constructor

    [XmlElement(WsWebConstants.Simple)]
    public WsSqlSimpleV1Model Item { get; set; } = new();

    public WsSqlSimpleV2Model(string description, bool isDebug)
    {
        Item = new(description, isDebug);
    }

    public WsSqlSimpleV2Model()
    {
        //
    }

    #endregion

    #region Public and private methods

    public override string ToString()
    {
        return @$"{nameof(Item)}: {Item}";
    }

    #endregion
}