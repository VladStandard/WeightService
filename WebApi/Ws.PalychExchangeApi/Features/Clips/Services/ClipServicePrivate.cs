using Ws.Database.EntityFramework.Entities.Ref1C.Clips;
using Ws.PalychExchangeApi.Features.Clips.Dto;

namespace Ws.PalychExchangeApi.Features.Clips.Services;

internal partial class ClipService
{
    private void ResolveUniqueUidLocal(List<ClipDto> dtos)
    {
        HashSet<Guid> duplicateUidSet = [];
        foreach (IGrouping<Guid, ClipDto> dtoGroup in dtos.GroupBy(dto => dto.Uid))
        {
            if (dtoGroup.Count() <= 1) continue;
            duplicateUidSet.Add(dtoGroup.Key);
            OutputDto.AddError(dtoGroup.Select(i => i.Uid), "Uid не уникален");
        }
        dtos.RemoveAll(brandDto => duplicateUidSet.Contains(brandDto.Uid));
    }
    private void SaveClips(IEnumerable<ClipDto> validDtos)
    {
        List<ClipEntity> clips = validDtos.Select(dto => dto.ToEntity()).ToList();

        using var transaction = dbContext.Database.BeginTransaction();
        try
        {
            dbContext.BulkMerge(clips);
            transaction.Commit();
            dbContext.SaveChanges();
            OutputDto.AddSuccess(clips.Select(i => i.Id).ToList());
        }
        catch (Exception)
        {
            transaction.Rollback();
            OutputDto.AddError(clips.Select(i => i.Id).ToList(), "Не предвиденная ошибка");
        }
    }
}