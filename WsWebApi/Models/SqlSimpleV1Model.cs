// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApi.Models;

[XmlRoot(WebConstants.Simple, Namespace = "", IsNullable = false)]
public class SqlSimpleV1Model : SerializeBase
{
    #region Public and private fields and properties

    [XmlAttribute("Description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="description"></param>
    public SqlSimpleV1Model(string description)
    {
        Description = description;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public SqlSimpleV1Model()
    {
        //
    }

    #endregion

    #region Public and private methods

    public override string ToString()
    {
        return @$"{nameof(Description)}: {Description}. ";
    }

    #endregion
}
