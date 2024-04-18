namespace Ws.Shared.Utils;

public static class ErrorUtil
{
    public static bool Suppress(Action operation, params Type[] ignoredExceptions)
    {
        try
        {
            operation.Invoke();
            return false;
        }
        catch (Exception ex) when (Array.Exists(ignoredExceptions, t => t.IsInstanceOfType(ex)))
        {
            return true;
        }
    }

    public static bool Suppress<T>(Action operation) where T : Exception
    {
        return Suppress(operation, typeof(T));
    }
}