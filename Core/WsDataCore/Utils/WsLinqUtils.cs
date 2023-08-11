// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsDataCore.Utils;

public static class WsLinqUtils
{
    public static bool DoIfAny<T>(this IEnumerable<T> collection, Action<IEnumerable<T>> action)
    {
        IEnumerator<T> enumerator = collection.GetEnumerator();
        if (!enumerator.MoveNext()) return false;
        action(CreateEnumerableFromStartedEnumerable(enumerator));
        return true;
    }

    private static IEnumerable<T> CreateEnumerableFromStartedEnumerable<T>(IEnumerator<T> enumerator)
    {
        do
            yield return enumerator.Current;
        while (enumerator.MoveNext());
    }
}
