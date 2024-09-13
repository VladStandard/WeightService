using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore.Storage;
using Ws.Database.EntityFramework.Entities.Ref1C.Clips;
using Ws.PalychExchange.Api.App.Features.Clips.Dto;

namespace Ws.PalychExchange.Api.App.Features.Clips.Impl;

internal partial class ClipApiService
{
    private void SaveClips(HashSet<ClipDto> validDtos)
    {
        List<ClipEntity> clips = validDtos.Select(dto => dto.ToEntity(DateTime.Now)).ToList();

        using IDbContextTransaction transaction = DbContext.Database.BeginTransaction();
        try
        {
            DbContext.BulkInsertOrUpdate(clips, options =>
            {
                options.UpdateByProperties = [nameof(ClipEntity.Id)];
                options.PropertiesToExcludeOnUpdate = [nameof(ClipEntity.CreateDt)];
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