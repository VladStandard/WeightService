using Microsoft.Data.SqlClient;
using Ws.Shared.Exceptions;

namespace Ws.DeviceControl.Api.App.Shared.Extensions;

internal static class DbContextExtensions
{
    public static async Task ThrowIfExistAsync<T>(this DbSet<T> dbSet, Expression<Func<T, bool>> predicate, string message) where T : class
    {
        bool isExist = await dbSet.AnyAsync(predicate);
        if (isExist)
            throw new ApiInternalException
            {
                ErrorDisplayMessage = message,
                StatusCode = HttpStatusCode.Conflict
            };
    }

    public static async Task SafeExistAsync<T>(this DbSet<T> dbSet, Expression<Func<T, bool>> predicate, string message) where T : class
    {
        bool isExist = await dbSet.AnyAsync(predicate);
        if (!isExist)
            throw new ApiInternalException
            {
                ErrorDisplayMessage = message,
                StatusCode = HttpStatusCode.Conflict
            };
    }

    public static async Task SafeDeleteAsync<T>(this DbSet<T> dbSet, Expression<Func<T, bool>> predicate) where T : class
    {
        try
        {
            await dbSet.Where(predicate).ExecuteDeleteAsync();
        }
        catch (SqlException)
        {
            throw new ApiInternalException
            {
                ErrorDisplayMessage = "Запись используются",
                StatusCode = HttpStatusCode.Conflict
            };
        }
    }

    public static async Task<T> SafeGetById<T>(this DbSet<T> dbSet, Guid id, string message) where T : class
    {
        T? entity = await dbSet.FindAsync(id);
        if (entity == null)
            throw new ApiInternalException
            {
                ErrorDisplayMessage = message,
                StatusCode = HttpStatusCode.NotFound
            };
        return entity;
    }
}