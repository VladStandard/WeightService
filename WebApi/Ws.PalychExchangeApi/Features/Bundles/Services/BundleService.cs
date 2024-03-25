using FluentValidation.Results;
using Ws.Database.EntityFramework;
using Ws.Database.EntityFramework.Entities.Ref1C.Boxes;
using Ws.Database.EntityFramework.Entities.Ref1C.Bundles;
using Ws.PalychExchangeApi.Features.Bundles.Common;
using Ws.PalychExchangeApi.Features.Bundles.Dto;
using Ws.PalychExchangeApi.Features.Bundles.Services.Validators;

namespace Ws.PalychExchangeApi.Features.Bundles.Services;

internal class BundleService : IBundleService
{
    private void SaveBundle(BundleDto bundleDto)
    {
        using var dbContext = new WsDbContext();
        using var transaction = dbContext.Database.BeginTransaction();

        try
        {
            BundleEntity existingBundle = dbContext.Bundles.FirstOrDefault(b => b.Uid1C == bundleDto.Uid) ?? new();

            existingBundle.Name = bundleDto.Name;
            existingBundle.Weight = bundleDto.Weight;

            if (existingBundle.IsNew)
            {
                existingBundle.Uid1C = bundleDto.Uid;
                dbContext.Add(existingBundle);
            }
            dbContext.SaveChanges();
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }


    public BundleWrapper Load(BundleWrapper dto)
    {
        foreach (BundleDto bundleDto in dto.Bundles)
        {
            ValidationResult validationResult = new BundleDtoValidator().Validate(bundleDto);

            if (!validationResult.IsValid)
                continue;
            SaveBundle(bundleDto);
        }
        return new();
    }
}