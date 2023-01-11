// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Xml.Serialization;

namespace WsWebApiCore.Models;

[XmlRoot(WebConstants.Response, Namespace = "", IsNullable = false)]
public class SqlResponseContragentsModel : SerializeBase
{
    #region Public and private fields and properties

    [XmlElement(WebConstants.Simple)]
    public SqlSimpleV1Model Item { get; set; } = new();

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="description"></param>
    public SqlResponseContragentsModel(string description)
    {
        Item = new(description);
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public SqlResponseContragentsModel()
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
