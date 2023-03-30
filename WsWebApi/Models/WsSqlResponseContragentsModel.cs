// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Serialization.Models;

namespace WsWebApi.Models;

[XmlRoot(WebConstants.Response, Namespace = "", IsNullable = false)]
public class WsSqlResponseContragentsModel : SerializeBase
{
    #region Public and private fields and properties

    [XmlElement(WebConstants.Simple)]
    public WsSqlSimpleV1Model Item { get; set; } = new();

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="description"></param>
    /// <param name="isDebug"></param>
    public WsSqlResponseContragentsModel(string description, bool isDebug)
    {
        Item = new(description, isDebug);
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlResponseContragentsModel()
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