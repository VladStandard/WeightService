using LinqKit;
using Ws.DeviceControl.Api.App.Shared.Validators.Api.Models;

namespace Ws.DeviceControl.Api.App.Shared.Validators.Api;

public abstract class ApiUpdateValidator<TEntity, TDto, TId>(ErrorHelper errorHelper) : BaseApiValidator<TDto>
    where TEntity : class
    where TDto : class
{
    public abstract Task ValidateAsync(DbSet<TEntity> dbSet, TDto dto, TId id);

    protected async Task ValidatePredicatesAsync(DbSet<TEntity> dbSet, List<PredicateField<TEntity>> predicates,
        PredicateField<TEntity> idPredicate)
    {
        idPredicate.Predicate.Not();

        foreach (PredicateField<TEntity> predicate in predicates)
        {
            Expression<Func<TEntity, bool>> expandedPredicate =
                predicate.Predicate.And(idPredicate.Predicate);

            bool isExist = await dbSet.AsExpandable().AnyAsync(expandedPredicate);

            if (isExist)
                throw new ApiInternalException
                {
                    ErrorDisplayMessage = errorHelper.Localize(ErrorType.Unique, predicate.FieldName),
                    StatusCode = HttpStatusCode.Conflict
                };
        }
    }
}