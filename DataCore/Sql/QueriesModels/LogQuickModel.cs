// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using static DataCore.ShareEnums;

namespace DataCore.Sql.QueriesModels;

[Serializable]
public class LogQuickModel : TableModel, ISerializable, ITableModel
{
    #region Public and private fields, properties, constructor

    public virtual string Scale { get; set; }
    public virtual string Host { get; set; }
    public virtual string App { get; set; }
    public virtual string Version { get; set; }
    public virtual string File { get; set; }
    public virtual int Line { get; set; }
    public virtual string Member { get; set; }
    public virtual string Icon { get; set; }
    public virtual string Message { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public LogQuickModel() : base(ColumnName.Uid, Guid.Empty, false)
    {
	    Init();
    }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityUid"></param>
	/// <param name="isSetupDates"></param>
	public LogQuickModel(Guid identityUid, bool isSetupDates) : base(ColumnName.Uid, identityUid, isSetupDates)
    {
	    Init();
    }

    #endregion

    #region Public and private methods

    public new virtual void Init()
    {
	    base.Init();
        Scale = string.Empty;
        Host = string.Empty;
        App = string.Empty;
        Version = string.Empty;
        File = string.Empty;
        Line = 0;
        Member = string.Empty;
        Icon = string.Empty;
        Message = string.Empty;
    }

    public override string ToString() =>
        base.ToString() +
        $"{nameof(Scale)}: {Scale}. " +
        $"{nameof(Host)}: {Host}. " +
        $"{nameof(App)}: {App}. " +
        $"{nameof(Version)}: {Version}. " +
        $"{nameof(File)}: {File}. " +
        $"{nameof(Line)}: {Line}. " +
        $"{nameof(Member)}: {Member}. " +
        $"{nameof(Icon)}: {Icon}. " +
        $"{nameof(Message)}: {Message}. ";

    public virtual bool Equals(LogQuickModel item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item) &&
               Equals(CreateDt, item.CreateDt) &&
               Equals(Scale, item.Scale) &&
               Equals(Host, item.Host) &&
               Equals(App, item.App) &&
               Equals(Version, item.Version) &&
               Equals(File, item.File) &&
               Equals(Line, item.Line) &&
               Equals(Member, item.Member) &&
               Equals(Icon, item.Icon) &&
               Equals(Message, item.Message);
    }

    public override bool Equals(object obj)
    {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((LogQuickModel)obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return base.EqualsDefault() &&
               Equals(Scale, string.Empty) &&
               Equals(Host, string.Empty) &&
               Equals(App, string.Empty) &&
               Equals(Version, string.Empty) &&
               Equals(File, string.Empty) &&
               Equals(Line, 0) &&
               Equals(Member, string.Empty) &&
               Equals(Icon, string.Empty) &&
               Equals(Message, string.Empty);
    }

    public new virtual object Clone()
    {
        LogQuickModel item = new();
        item.Scale = Scale;
        item.Host = Host;
        item.App = App;
        item.Version = Version;
        item.File = File;
        item.Line = Line;
        item.Member = Member;
        item.Icon = Icon;
        item.Message = Message;
        item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual LogQuickModel CloneCast() => (LogQuickModel)Clone();

    public virtual long GetScaleIdentityId(DataAccessHelper dataAccess)
    {
	    switch (string.IsNullOrEmpty(Scale))
	    {
		    case false:
				SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
					new() { new(DbField.Description, DbComparer.Equal, Scale) }, null, 0, false, false);
				ScaleEntity? scale = dataAccess.Crud.GetItem<ScaleEntity>(sqlCrudConfig);
			    if (scale is not null)
				    return scale.IdentityId;
			    break;
	    }
	    return 0;
    }

    public virtual long GetHostIdentityId(DataAccessHelper dataAccess)
    {
	    switch (string.IsNullOrEmpty(Host))
	    {
		    case false:
			    SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
				    new() { new(DbField.HostName, DbComparer.Equal, Host) }, null, 0, false, false);
			    HostEntity? host = dataAccess.Crud.GetItem<HostEntity>(sqlCrudConfig);
                if (host is not null)
					return host.IdentityId;
                break;
	    }
		return 0;
    }

    #endregion
}
