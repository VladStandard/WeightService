using Ws.Database.EntityFramework.Entities.Ref1C.Bundles;
using Ws.PalychExchangeApi.Features.Bundles.Dto;

namespace Ws.PalychExchangeApi.Features.Bundles.Services;

internal partial class BundleService
{
    private void ResolveUniqueUidLocal(List<BundleDto> dtos)
    {
        HashSet<Guid> duplicateUidSet = [];
        foreach (IGrouping<Guid, BundleDto> dtoGroup in dtos.GroupBy(dto => dto.Uid))
        {
            if (dtoGroup.Count() <= 1) continue;
            duplicateUidSet.Add(dtoGroup.Key);
            OutputDto.AddError(dtoGroup.Select(i => i.Uid), "Uid не уникален");
        }
        dtos.RemoveAll(brandDto => duplicateUidSet.Contains(brandDto.Uid));
    }
    private void SaveBundles(IEnumerable<BundleDto> validDtos)
    {
        List<BundleEntity> bundles = validDtos.Select(dto => dto.ToEntity()).ToList();

        using var transaction = dbContext.Database.BeginTransaction();
        try
        {
            dbContext.BulkMerge(bundles);
            transaction.Commit();
            dbContext.SaveChanges();
            OutputDto.AddSuccess(bundles.Select(i => i.Id).ToList());
        }
        catch (Exception)
        {
            transaction.Rollback();
            OutputDto.AddError(bundles.Select(i => i.Id).ToList(), "Не предвиденная ошибка");
        }
    }
}