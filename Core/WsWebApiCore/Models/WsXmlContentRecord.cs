namespace WsWebApiCore.Models;

public sealed record WsXmlContentRecord<T> where T : WsSqlTable1CBase, new()
{
    #region Public and private fields, properties, constructor

    public T Item { get; init; }
    public string Content { get; init; }

    public override int GetHashCode() => (Item, Content.ToUpper()).GetHashCode();

    public WsXmlContentRecord(T item, string content)
    {
        Item = item;
        Content = content;
    }

    #endregion
}