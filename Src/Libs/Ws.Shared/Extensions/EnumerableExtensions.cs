namespace Ws.Shared.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<TItem> ReplaceItemByKey<TItem>(this IEnumerable<TItem> items, TItem newItem, Func<TItem, bool> predicate)
    {
        List<TItem> itemList = items.ToList();
        int index = itemList.FindIndex(item => predicate(item));
        if (index != -1) itemList[index] = newItem;
        return itemList;
    }
}