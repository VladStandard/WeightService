using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework.Entities.Ref1C.Bundles;
using Ws.PalychExchangeApi.Features.Bundles.Common;
using Ws.PalychExchangeApi.Features.Bundles.Dto;
using Ws.PalychExchangeApi.Features.Bundles.Services.Validators;

namespace Ws.PalychExchangeApi.Features.Bundles.Services;

internal class BundleService(DbContext dbContext) : IBundleService
{
    private void SaveBundles(IEnumerable<BundleDto> validDtos)
    {
        List<BundleEntity> bundles = validDtos.Select(dto => dto.ToEntity()).ToList();

        using var transaction = dbContext.Database.BeginTransaction();
        try
        {
            dbContext.BulkMerge(bundles);
            transaction.Commit();
            dbContext.SaveChanges();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }


    public BundleWrapper Load(BundleWrapper dtoWrapper)
    {
        BundleDtoValidator validator = new();
        HashSet<BundleDto> validDtos = [];

        foreach (BundleDto dto in dtoWrapper.Bundles)
        {
            ValidationResult validationResult = validator.Validate(dto);
            if (validDtos.Any(box => box.Uid == dto.Uid))
                continue;
            if (!validationResult.IsValid)
                continue;
            validDtos.Add(dto);
        }
        SaveBundles(validDtos);
        return new();
    }
}