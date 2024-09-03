using Microsoft.EntityFrameworkCore.Storage;
using Ws.Database.EntityFramework.Entities.Ref1C.Clips;
using Ws.PalychExchange.Api.Features.Clips.Dto;

namespace Ws.PalychExchange.Api.Features.Clips.Services;

internal partial class ClipApiService
{
    private void SaveClips(IEnumerable<ClipDto> validDtos)
    {
        DateTime updateDt = DateTime.UtcNow.AddHours(3);
        List<ClipEntity> clips = validDtos.Select(dto => dto.ToEntity(updateDt)).ToList();

        using IDbContextTransaction transaction = DbContext.Database.BeginTransaction();
        try
        {
            DbContext.BulkMerge(clips, options =>
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