using Microsoft.EntityFrameworkCore.Storage;
using Ws.Database.EntityFramework.Entities.Ref1C.Boxes;
using Ws.PalychExchangeApi.Features.Boxes.Dto;

namespace Ws.PalychExchangeApi.Features.Boxes.Services;

internal partial class BoxService
{
    private void SaveBoxes(IEnumerable<BoxDto> validDtos)
    {
        DateTime updateDt = DateTime.UtcNow.AddHours(3);
        List<BoxEntity> boxes = validDtos.Select(dto => dto.ToEntity(updateDt)).ToList();

        using IDbContextTransaction transaction = DbContext.Database.BeginTransaction();
        try
        {
            DbContext.BulkMerge(boxes, options =>
            {
                options.InsertIfNotExists = true;
                options.IgnoreOnMergeInsertExpression = c => new { c.CreateDt, c.ChangeDt };
                options.IgnoreOnMergeUpdateExpression = c => new { c.CreateDt };
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