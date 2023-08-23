// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsDataCore.Models;

[DebuggerDisplay("{ToString()}")]
public sealed class WsXmlTagModel
{
    #region Public and private fields, properties, constructor

    public string ElementName { get; }
    public string? AttributeName { get; }
    public string? AttributeValue { get; }

    public WsXmlTagModel(string elementName, string? attributeName = null, string? attributeValue = null)
    {
        ElementName = elementName;
        AttributeName = attributeName;
        AttributeValue = attributeValue;
    }

    #endregion

    #region Public and private methods

    public override string ToString() => $"{ElementName} | {AttributeName} | {AttributeValue}";

    #endregion
}