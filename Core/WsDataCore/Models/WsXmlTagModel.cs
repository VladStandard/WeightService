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