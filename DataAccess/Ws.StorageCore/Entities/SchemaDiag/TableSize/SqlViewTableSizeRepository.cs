namespace Ws.StorageCore.Entities.SchemaDiag.TableSize;

public sealed class SqlViewTableSizeRepository
{
    private SqlCoreHelper SqlCore => SqlCoreHelper.Instance;
    
    public IEnumerable<SqlViewTableSizeModel> GetEnumerable()
    {
       return SqlCore.GetEnumerable<SqlViewTableSizeModel>(new());
    }
}