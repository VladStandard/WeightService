using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Ws.Database.EntityFramework.Entities.Ref1C.Nestings;
using Ws.Database.EntityFramework.Entities.Ref1C.Plus;
using Ws.PalychExchangeApi.Features.Plus.Dto;
using Ws.Shared.Constants;

namespace Ws.PalychExchangeApi.Features.Plus.Services;


internal sealed partial class PluApiService
{
    #region Resolve uniques db

    private void ResolveUniqueNumberDb(List<PluDto> dtos)
    {
        HashSet<short> numbers = dtos.Select(dto => dto.Number).ToHashSet();
        HashSet<Guid> plusUid = dtos.Select(dto => dto.Uid).ToHashSet();

        List<short> existingNumbers = DbContext.Plus
            .Where(i => !plusUid.Contains(i.Id) && numbers.Contains(i.Number))
            .Select(i => i.Number)
            .ToList();

        dtos.RemoveAll(dto =>
        {
            if (!existingNumbers.Contains(dto.Number)) return false;
            OutputDto.AddError(dto.Uid, $"Номер плу - ({dto.Number}) не уникален (бд)");
            return true;
        });
    }

    #endregion

    private void SetDefaultFk(List<PluDto> validDtos)
    {
        validDtos.ForEach(i =>
        {
            i.ClipUid = i.ClipUid == Guid.Empty ? BaseConsts.GuidMax : i.ClipUid;
            i.BundleUid = i.BundleUid == Guid.Empty ? BaseConsts.GuidMax : i.BundleUid;
        });
    }

    private void SavePlus(IReadOnlyCollection<PluDto> validDtos)
    {
        DateTime updateDt = DateTime.UtcNow.AddHours(3);
        List<PluEntity> plus = validDtos.Select(dto => dto.ToPluEntity(updateDt)).ToList();

        using IDbContextTransaction transaction = DbContext.Database.BeginTransaction();
        try
        {
            DbContext.BulkMerge(plus, options =>
            {
                options.InsertIfNotExists = true;
                options.IgnoreOnMergeInsertExpression = c => new { c.CreateDt, c.ChangeDt, TemplateEntityId = c.TemplateId };
                options.IgnoreOnMergeUpdateExpression = c => new { c.CreateDt, TemplateEntityId = c.TemplateId };
            });
            transaction.Commit();
            SaveNestings(validDtos);
            OutputDto.AddSuccess(plus.Select(i => i.Id).ToList());
        }
        catch (Exception)
        {
            transaction.Rollback();
            OutputDto.AddError(plus.Select(i => i.Id).ToList(), "Не предвиденная ошибка");
        }
    }

    private void DeleteNestings(List<PluDto> dtos)
    {
        List<Guid> uidToDelete = dtos.Where(dto => dto.IsDelete).Select(dto => dto.Uid).ToList();

        if (!uidToDelete.Any()) return;

        IDbContextTransaction transaction = DbContext.Database.BeginTransaction();

        try
        {
            List<object> parameters = uidToDelete.Select((uid, index) => new SqlParameter($"@p{index}", uid)).ToList<object>();

            string inClause = string.Join(", ", parameters.Select((p, index) => $"@p{index}"));
            string sql = $@"
                DELETE FROM [dbo].[ARMS_PLUS_FK] WHERE PLU_UID IN ({inClause});
                UPDATE [PRINT].[LABELS] SET PLU_UID = NULL WHERE PLU_UID IN ({inClause});
                DELETE FROM [REF_1C].[PLUS] WHERE UID IN ({inClause});
            ";
            DbContext.Database.ExecuteSqlRaw(sql, parameters.ToArray());

            transaction.Commit();
            OutputDto.AddSuccess(uidToDelete);
        }
        catch (Exception ex)
        {
            OutputDto.AddError(uidToDelete, "Не предвиденная ошибка");
            transaction.Rollback();
        }
        finally
        {
            dtos.RemoveAll(dto => uidToDelete.Contains(dto.Uid));
        }
    }


    private void SaveNestings(IEnumerable<PluDto> validDtos)
    {
        DateTime updateDt = DateTime.UtcNow.AddHours(3);
        List<NestingEntity> nestings = validDtos.Select(dto => dto.ToNestingEntity(updateDt)).ToList();

        using IDbContextTransaction transaction = DbContext.Database.BeginTransaction();
        try
        {
            DbContext.BulkMerge(nestings, options =>
            {
                options.InsertIfNotExists = true;
                options.IgnoreOnMergeInsertExpression = c => new { c.CreateDt, c.ChangeDt };
                options.IgnoreOnMergeUpdateExpression = c => new { c.CreateDt };
                options.MergeKeepIdentity = true;
            });
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
        }
    }
}