using Ws.Database.EntityFramework.Entities.Ref1C.Boxes;
using Ws.PalychExchangeApi.Features.Boxes.Dto;

namespace Ws.PalychExchangeApi.Features.Boxes.Services;

internal partial class BoxService
{
    private void ResolveUniqueUidLocal(List<BoxDto> dtos)
    {
        HashSet<Guid> duplicateUidSet = [];
        foreach (IGrouping<Guid, BoxDto> dtoGroup in dtos.GroupBy(dto => dto.Uid))
        {
            if (dtoGroup.Count() <= 1) continue;
            duplicateUidSet.Add(dtoGroup.Key);
            OutputDto.AddError(dtoGroup.Select(i => i.Uid), "Uid не уникален");
        }
        dtos.RemoveAll(brandDto => duplicateUidSet.Contains(brandDto.Uid));
    }
    private void SaveBoxes(IEnumerable<BoxDto> validDtos)
    {
        List<BoxEntity> boxes = validDtos.Select(dto => dto.ToEntity()).ToList();

        using var transaction = dbContext.Database.BeginTransaction();
        try
        {
            dbContext.BulkMerge(boxes);
            transaction.Commit();
            dbContext.SaveChanges();
            OutputDto.AddSuccess(boxes.Select(i => i.Id).ToList());
        }
        catch (Exception)
        {
            transaction.Rollback();
            OutputDto.AddError(boxes.Select(i => i.Id).ToList(), "Не предвиденная ошибка");
        }
    }
}