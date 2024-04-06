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
        HashSet<short> numberList = dtos.Select(dto => dto.Number).ToHashSet();

        List<NumberIdPair> existingNumbersIdPairs = DbContext.Plus
            .Where(plu => numberList.Contains(plu.Number))
            .Select(plu => new NumberIdPair { Number = plu.Number, Id = plu.Id }).OrderBy(i => i.Number)
            .ToList();

        List<PluDto> notUniquePluDto =
            dtos
                .Where(dto => existingNumbersIdPairs.Any(pair => pair.Number == dto.Number))
                .Where(dto => !existingNumbersIdPairs.Any(pair => pair.Number == dto.Number && pair.Id == dto.Uid))
                .ToList();
        OutputDto.AddError(notUniquePluDto.Select(i => i.Uid), "Номер плу не уникален (бд)");

        dtos.RemoveAll(dto =>
        {
            if (existingNumbersIdPairs.All(pair => pair.Number != dto.Number))
                return false;
            return !existingNumbersIdPairs.Any(pair => pair.Number == dto.Number && pair.Id == dto.Uid);
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