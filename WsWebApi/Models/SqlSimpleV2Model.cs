// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Serialization.Models;

namespace WsWebApi.Models;

[XmlRoot(WebConstants.Response, Namespace = "", IsNullable = false)]
public class SqlSimpleV2Model : SerializeBase
{
    #region Public and private fields and properties

    [XmlElement(WebConstants.Simple)]
    public SqlSimpleV1Model Item { get; set; } = new();

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="description"></param>
    /// <param name="isDebug"></param>
    public SqlSimpleV2Model(string description, bool isDebug)
    {
        Item = new(description, isDebug);
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public SqlSimpleV2Model()
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
