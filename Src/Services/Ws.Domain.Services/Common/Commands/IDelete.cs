namespace Ws.Domain.Services.Common.Commands;

public interface IDelete<in T>
{
    void DeleteById(T id);
}