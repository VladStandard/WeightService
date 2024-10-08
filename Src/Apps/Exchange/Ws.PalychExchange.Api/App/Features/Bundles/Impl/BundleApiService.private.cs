using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore.Storage;
using Ws.Database.EntityFramework.Entities.Ref1C.Bundles;
using Ws.PalychExchange.Api.App.Features.Bundles.Dto;

namespace Ws.PalychExchange.Api.App.Features.Bundles.Impl;

internal partial class BundleApiService
{
    private void SaveBundles(HashSet<BundleDto> validDtos)
    {
        List<BundleEntity> bundles = validDtos.Select(dto => dto.ToEntity(DateTime.Now)).ToList();

        using IDbContextTransaction transaction = DbContext.Database.BeginTransaction();
        try
        {
            DbContext.BulkInsertOrUpdate(bundles, options =>
            {
                options.UseTempDB = true;
                options.UpdateByProperties = [nameof(BundleEntity.Id)];
                options.PropertiesToExcludeOnUpdate = [nameof(BundleEntity.CreateDt)];
            });
            transaction.Commit();
            OutputDto.AddSuccess(bundles.ConvertAll(i => i.Id));
        }
        catch (Exception)
        {
            transaction.Rollback();
            OutputDto.AddError(bundles.ConvertAll(i => i.Id), "Не предвиденная ошибка");
        }
    }
}