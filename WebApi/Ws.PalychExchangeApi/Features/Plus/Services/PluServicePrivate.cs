using Microsoft.EntityFrameworkCore.Storage;
using Ws.Database.EntityFramework.Entities.Ref1C.Nestings;
using Ws.Database.EntityFramework.Entities.Ref1C.Plus;
using Ws.PalychExchangeApi.Features.Plus.Dto.PluDto;

namespace Ws.PalychExchangeApi.Features.Plus.Services;

file sealed record NumberIdPair
{
    public required Guid Id { get; set; }
    public required short Number { get; set; }
}

internal sealed partial class PluService
{
    #region Resolve uniques db

    private void ResolveUniqueNumberDb(List<PluDto> dtos)
    {
        HashSet<short> numbers = dtos.Select(dto => dto.Number).ToHashSet();
        HashSet<Guid> plusUid = dtos.Select(dto => dto.Uid).ToHashSet();

        List<NumberIdPair> existingPairs = DbContext.Plus
            .Where(i => !plusUid.Contains(i.Id) && numbers.Contains(i.Number))
            .Select(i => new NumberIdPair { Number = i.Number, Id = i.Id })
            .ToList();

        dtos.RemoveAll(dto =>
        {
            if (!existingPairs.Any(uniq => dto.Uid != uniq.Id && dto.Number == uniq.Number)) return false;
            OutputDto.AddError(dto.Uid, $"Номер плу - ({dto.Number}) не уникален (бд)");
            return true;
        });
    }

    #endregion

    private void SavePlus(IReadOnlyCollection<PluDto> validDtos)
    {
        DateTime updateDt = DateTime.UtcNow.AddHours(3);
        List<PluEntity> plus = validDtos.Select(dto => dto.ToPluEntity(updateDt)).ToList();

        using IDbContextTransaction transaction = DbContext.Database.BeginTransaction();
        try
        {
            DbContext.BulkMerge(plus, options =>
            {
                options.InsertIfNotExists = true;
                options.IgnoreOnMergeInsertExpression = c => new { c.CreateDt, c.ChangeDt };
                options.IgnoreOnMergeUpdateExpression = c => new { c.CreateDt };
            });
            transaction.Commit();
            SaveNestings(validDtos);
            OutputDto.AddSuccess(plus.Select(i => i.Id).ToList());
        }
        catch (Exception)
        {
            transaction.Rollback();
            OutputDto.AddError(plus.Select(i => i.Id).ToList(), "Не предвиденная ошибка");
        }
    }

    private void SaveNestings(IEnumerable<PluDto> validDtos)
    {
        DateTime updateDt = DateTime.UtcNow.AddHours(3);
        List<NestingEntity> nestings = validDtos.Select(dto => dto.ToNestingEntity(updateDt)).ToList();

        using IDbContextTransaction transaction = DbContext.Database.BeginTransaction();
        try
        {
            DbContext.BulkMerge(nestings, options =>
            {
                options.InsertIfNotExists = true;
                options.IgnoreOnMergeInsertExpression = c => new { c.CreateDt, c.ChangeDt };
                options.IgnoreOnMergeUpdateExpression = c => new { c.CreateDt };
                options.MergeKeepIdentity = true;
            });
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
        }
    }
}