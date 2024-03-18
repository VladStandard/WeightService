using NHibernate.Transform;
using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities;

namespace Ws.Database.Nhibernate.Entities;

public class SqlViewDbFileSizeRepository : BaseRepository, IGetAll<DbFileSizeInfoEntity>
{
    public IEnumerable<DbFileSizeInfoEntity> GetAll()
    {
        const string fileNameAlias = nameof(DbFileSizeInfoEntity.FileName);
        const string sizeMbAlias = nameof(DbFileSizeInfoEntity.SizeMb);
        const string maxSizeMbAlias = nameof(DbFileSizeInfoEntity.MaxSizeMb);

        const string sqlQuery =
            $"SELECT" +
            $"\n [NAME] AS [{fileNameAlias}]," +
            $"\n [SIZE] * 8 / 1024 AS [{sizeMbAlias}]," +
            $"\n [MAX_SIZE] * 8 / 1024 AS [{maxSizeMbAlias}]" +
            $"\n FROM [SYS].[DATABASE_FILES]" +
            $"\n ORDER BY [{sizeMbAlias}] DESC, [NAME]";

        ISQLQuery query = Session.CreateSQLQuery(sqlQuery);
        query.SetResultTransformer(Transformers.AliasToBean<DbFileSizeInfoEntity>());
        return query.List<DbFileSizeInfoEntity>().ToList();
    }
}