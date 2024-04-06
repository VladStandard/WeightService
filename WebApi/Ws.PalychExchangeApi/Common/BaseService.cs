using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework;
using Ws.Database.EntityFramework.Common;
using Ws.PalychExchangeApi.Dto;

namespace Ws.PalychExchangeApi.Common;

// ReSharper disable once SuggestBaseTypeForParameterInConstructor
internal abstract class BaseService<TDto>(WsDbContext dbContext, IValidator<TDto> validator) where TDto : BaseDto
{
    private readonly IValidator _validator = validator;
    protected readonly WsDbContext DbContext = dbContext;
    protected readonly ResponseDto OutputDto = new();

    # region ResolveUniqueLocal
    protected void ResolveUniqueUidLocal<T>(List<T> dtos) where T : BaseDto =>
        ResolveUniqueLocal(dtos, arg => arg.Uid, "Uid - не уникален");

    protected void ResolveUniqueLocal<T, TKey>(List<T> dtos, Func<T, TKey> groupBy, string msg) where T : BaseDto
    {
        List<Guid> duplicates = dtos
            .GroupBy(groupBy)
            .Where(group => group.Count() > 1)
            .SelectMany(group => group)
            .Select(dto => dto.Uid)
            .ToList();

        dtos.RemoveAll(dto =>
        {
            if (!duplicates.Contains(dto.Uid))
                return false;
            OutputDto.AddError(dto.Uid, msg);
            return true;
        });
    }

    # endregion

    protected void ResolveNotExistsFkDb<T, TEntity>(List<T> dtos, DbSet<TEntity> dbSet, Func<T, Guid> select, string msg)
        where T : BaseDto where TEntity : EfEntityBase
    {
        HashSet<Guid> uidsFkList = dtos.Select(select).ToHashSet();

        HashSet<Guid> existing = dbSet
            .Where(entity => uidsFkList.Contains(entity.Id))
            .Select(entity => entity.Id)
            .ToHashSet();

        dtos.RemoveAll(dto =>
        {
            if (existing.Contains(select(dto)))
                return false;
            OutputDto.AddError(select(dto), msg);
            return true;
        });
    }

    protected List<TDto> FilterValidDtos(IEnumerable<TDto> dtos)
    {
        return dtos.Where(i =>
        {
            ValidationContext<TDto> context = new(i);
            ValidationResult validationResult = _validator.Validate(context);

            if (validationResult.IsValid) return true;

            OutputDto.AddError(i.Uid, validationResult.Errors.First().ErrorMessage);
            return false;
        }).ToList();
    }
}