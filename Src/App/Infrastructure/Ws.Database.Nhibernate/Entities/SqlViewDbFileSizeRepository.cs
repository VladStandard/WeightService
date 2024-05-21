using NHibernate.Transform;
using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Diag;

namespace Ws.Database.Nhibernate.Entities;

public class SqlViewDbFileSizeRepository : BaseRepository, IGetAll<DbFileSizeInfo>
{
    public IList<DbFileSizeInfo> GetAll()
    {
        const string fileNameAlias = nameof(DbFileSizeInfo.FileName);
        const string sizeMbAlias = nameof(DbFileSizeInfo.SizeMb);
        const string maxSizeMbAlias = nameof(DbFileSizeInfo.MaxSizeMb);

        const string sqlQuery =
            $"SELECT" +
            $"\n [NAME] AS [{fileNameAlias}]," +
            $"\n [SIZE] * 8 / 1024 AS [{sizeMbAlias}]," +
            $"\n [MAX_SIZE] * 8 / 1024 AS [{maxSizeMbAlias}]" +
            $"\n FROM [SYS].[DATABASE_FILES]" +
            $"\n ORDER BY [{sizeMbAlias}] DESC, [NAME]";

        ISQLQuery query = Session.CreateSQLQuery(sqlQuery);
        query.SetResultTransformer(Transformers.AliasToBean<DbFileSizeInfo>());
        return query.List<DbFileSizeInfo>().ToList();
    }
}