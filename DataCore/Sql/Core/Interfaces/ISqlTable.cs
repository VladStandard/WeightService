// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using DataCore.Sql.Tables;

namespace DataCore.Sql.Core.Interfaces;

public interface ISqlTable : ISqlDbBase
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

    public bool Equals(ISqlTable item);

    #endregion

    #region Public and private methods - virtual

    public SqlTableBase CloneCast();

    public void CloneSetup(SqlTableBase item);

    public void SetDtNow();

    public void UpdateProperties(ISqlTable item);

    #endregion
}