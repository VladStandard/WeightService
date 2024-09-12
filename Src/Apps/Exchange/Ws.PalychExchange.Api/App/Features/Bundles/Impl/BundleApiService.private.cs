using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore.Storage;
using Ws.Database.EntityFramework.Entities.Ref1C.Bundles;
using Ws.PalychExchange.Api.App.Features.Bundles.Dto;

namespace Ws.PalychExchange.Api.App.Features.Bundles.Impl;

internal partial class BundleApiService
{
    private void SaveBundles(HashSet<BundleDto> validDtos)
    {
        DateTime updateDt = DateTime.UtcNow.AddHours(3);
        List<BundleEntity> bundles = validDtos.Select(dto => dto.ToEntity(updateDt)).ToList();

        using IDbContextTransaction transaction = DbContext.Database.BeginTransaction();
        try
        {
            DbContext.BulkInsertOrUpdate(bundles, options =>
            {
                options.UpdateByProperties = [nameof(BundleEntity.Id)];
                options.PropertiesToExcludeOnUpdate = [nameof(BundleEntity.CreateDt)];
            });
            transaction.Commit();
            OutputDto.AddSuccess(bundles.Select(i => i.Id).ToList());
        }
        catch (Exception)
        {
            transaction.Rollback();
            OutputDto.AddError(bundles.Select(i => i.Id).ToList(), "Не предвиденная ошибка");
        }
    }
}