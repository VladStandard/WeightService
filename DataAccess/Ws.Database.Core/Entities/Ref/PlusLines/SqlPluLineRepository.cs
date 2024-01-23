using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref.PlusLines;

public sealed class SqlPluLineRepository
{
    public PluLineEntity GetItemByLinePlu(LineEntity line, PluEntity plu)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilters([
            SqlRestrictions.EqualFk(nameof(PluLineEntity.Line), line),
            SqlRestrictions.EqualFk(nameof(PluLineEntity.Plu), plu)
        ]);
        return SqlCoreHelper.Instance.GetItemByCrud<PluLineEntity>(sqlCrudConfig);
    }
    
    public IEnumerable<PluLineEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<PluLineEntity> items = SqlCoreHelper.Instance.GetEnumerable<PluLineEntity>(sqlCrudConfig);
        items = items.OrderBy(item => item.Plu.Number);
        return items.ToList();
    }

    public IEnumerable<PluLineEntity> GetListByLine(LineEntity line)
    {
        SqlCrudConfigModel crud = new();
        crud.AddFilter(SqlRestrictions.EqualFk(nameof(PluLineEntity.Line), line));
        return GetList(crud).OrderBy(i => i.Plu.Number);
    }
    
    public IEnumerable<PluLineEntity> GetWeightListByLine(LineEntity line)
    {
        IEnumerable<PluLineEntity> items = GetListByLine(line);
        return items.Where(x => x.Plu.IsCheckWeight);
    }
    
    public IEnumerable<PluLineEntity> GetPieceListByLine(LineEntity line)
    {
        IEnumerable<PluLineEntity> items = GetListByLine(line);
        return items.Where(x => !x.Plu.IsCheckWeight);
    }
}