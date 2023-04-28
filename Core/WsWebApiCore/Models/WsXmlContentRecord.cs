// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Models;

public sealed record WsXmlContentRecord<T> where T : WsSqlTableBase1c, new()
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