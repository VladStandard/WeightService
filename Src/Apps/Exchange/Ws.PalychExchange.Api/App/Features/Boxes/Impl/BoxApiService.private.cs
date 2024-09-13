using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore.Storage;
using Ws.Database.EntityFramework.Entities.Ref1C.Boxes;
using Ws.PalychExchange.Api.App.Features.Boxes.Dto;

namespace Ws.PalychExchange.Api.App.Features.Boxes.Impl;

internal partial class BoxApiService
{
    private void SaveBoxes(HashSet<BoxDto> validDtos)
    {
        List<BoxEntity> boxes = validDtos.Select(dto => dto.ToEntity(DateTime.Now)).ToList();

        using IDbContextTransaction transaction = DbContext.Database.BeginTransaction();
        try
        {
            DbContext.BulkInsertOrUpdate(boxes, options =>
            {
                options.UpdateByProperties = [nameof(BoxEntity.Id)];
                options.PropertiesToExcludeOnUpdate = [nameof(BoxEntity.CreateDt)];
            });
            transaction.Commit();
            OutputDto.AddSuccess(boxes.Select(i => i.Id).ToList());
        }
        catch (Exception)
        {
            transaction.Rollback();
            OutputDto.AddError(boxes.Select(i => i.Id).ToList(), "Не предвиденная ошибка");
        }
    }
}