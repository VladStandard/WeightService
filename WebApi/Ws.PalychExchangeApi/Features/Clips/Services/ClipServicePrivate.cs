using Microsoft.EntityFrameworkCore.Storage;
using Ws.Database.EntityFramework.Entities.Ref1C.Clips;
using Ws.PalychExchangeApi.Features.Clips.Dto;

namespace Ws.PalychExchangeApi.Features.Clips.Services;

internal partial class ClipService
{
    private void SaveClips(IEnumerable<ClipDto> validDtos)
    {
        DateTime updateDt = DateTime.UtcNow.AddHours(3);
        List<ClipEntity> clips = validDtos.Select(dto => dto.ToEntity(updateDt)).ToList();

        using IDbContextTransaction transaction = dbContext.Database.BeginTransaction();
        try
        {
            dbContext.BulkMerge(clips, options =>
            {
                options.InsertIfNotExists = true;
                options.IgnoreOnMergeInsertExpression = c => new { c.CreateDt, c.ChangeDt };
                options.IgnoreOnMergeUpdateExpression = c => new { c.CreateDt };
            });
            transaction.Commit();
            OutputDto.AddSuccess(clips.Select(i => i.Id).ToList());
        }
        catch (Exception)
        {
            transaction.Rollback();
            OutputDto.AddError(clips.Select(i => i.Id).ToList(), "Не предвиденная ошибка");
        }
    }
}