// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Xml.Serialization;
using DataCore.Sql.Models;
using WebApiCore.Utils;

namespace WebApiCore.Common;

[XmlRoot(TerraConsts.Response, Namespace = "", IsNullable = false)]
public class SqlResponseContragentsEntity : SerializeDeprecatedModel<SqlResponseContragentsEntity>
{
    #region Public and private fields and properties

    [XmlElement(TerraConsts.Simple)]
    public SqlSimpleV1Entity Item { get; set; } = new();

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="description"></param>
    public SqlResponseContragentsEntity(string description)
    {
        Item = new(description);
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public SqlResponseContragentsEntity()
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
