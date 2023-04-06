// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace DataCore.Sql.Core.Models;

public interface IWsSqlTable : IWsSqlDbBase
{
    #region Public and private fields, properties, constructor

    public SqlFieldIdentityModel Identity { get; set; }
    public long IdentityValueId { get; set; }
    public Guid IdentityValueUid { get; set; }
    public bool IsNew { get; }
    public bool IsNotNew { get; }
    public bool IsExists { get; }
    public bool IsNotExists { get; }
    public bool IsIdentityId { get; }
    public bool IsIdentityUid { get; }
    public DateTime CreateDt { get; set; }
    public DateTime ChangeDt { get; set; }
    public bool IsMarked { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ParseResultModel ParseResult { get; set; }

    #endregion

    #region Public and private methods - override

    public bool Equals(IWsSqlTable item);

    #endregion

    #region Public and private methods - virtual

    public SqlTableBase CloneCast();

    public void CloneSetup(SqlTableBase item);

    public void SetDtNow();

    public void UpdateProperties(IWsSqlTable item);

    #endregion
}