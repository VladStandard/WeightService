namespace WsDataCore.Helpers;

[Serializable]
public class WsJsonSettingsModel : ISerializable
{
	#region Public and private fields, properties, constructor

	public WsJsonSettingsSqlModel Sql { get; set; }
	public int SectionRowsCount { get; set; }
	public int ItemRowsCount { get; set; }
	public int SelectTopRowsCount { get; set; }
    public ushort Version { get; set; }
	public string AllowedHosts { get; set; }
	[NonSerialized] private string _connectionString;
	public string ConnectionString { get => _connectionString; set => _connectionString = value; }


	/// <param name="jsonSettingsSql"></param>
	/// <param name="isCheckProperties"></param>
	public WsJsonSettingsModel(WsJsonSettingsSqlModel jsonSettingsSql, bool isCheckProperties)
	{
		Sql = jsonSettingsSql;
		SectionRowsCount = 0;
		ItemRowsCount = 0;
		SelectTopRowsCount = 0;
		AllowedHosts = string.Empty;
		Version = 0;
		ConnectionString = _connectionString = string.Empty;
		CheckProperties(isCheckProperties);
	}


	public WsJsonSettingsModel() : this(new(), false) { }


	/// <param name="info"></param>
	/// <param name="context"></param>
	protected WsJsonSettingsModel(SerializationInfo info, StreamingContext context)
	{
		Sql = (WsJsonSettingsSqlModel)info.GetValue(nameof(Sql), typeof(WsJsonSettingsSqlModel));
		SectionRowsCount = info.GetInt32(nameof(SectionRowsCount));
		ItemRowsCount = info.GetInt32(nameof(ItemRowsCount));
		SelectTopRowsCount = info.GetInt32(nameof(SelectTopRowsCount));
		Version = info.GetUInt16(nameof(Version));
		AllowedHosts = info.GetString(nameof(AllowedHosts));
		ConnectionString = _connectionString = string.Empty;
	}

	#endregion

	#region Public and private methods

	public override string ToString() => Sql +
        $"{nameof(SectionRowsCount)}: {SectionRowsCount}. " +
        $"{nameof(ItemRowsCount)}: {ItemRowsCount}. " +
        $"{nameof(SelectTopRowsCount)}: {SelectTopRowsCount}. " +
        $"{nameof(Version)}: {Version}. " +
        $"{nameof(AllowedHosts)}: {AllowedHosts}. ";
    
	public bool CheckProperties(bool isGenerateException)
	{
		if (string.IsNullOrEmpty(Sql.DataSource))
		{
			if (isGenerateException)
				throw new ArgumentNullException(Sql.DataSource, $"{nameof(WsJsonSettingsModel)}.{nameof(Sql.DataSource)} IsNullOrEmpty!");
			return false;
		}

		if (string.IsNullOrEmpty(Sql.InitialCatalog))
		{
			if (isGenerateException)
				throw new ArgumentNullException(Sql.InitialCatalog, $"{nameof(WsJsonSettingsModel)}.{nameof(Sql.InitialCatalog)} IsNullOrEmpty!");
			return false;
		}

		if (!Sql.PersistSecurityInfo)
		{
			if (string.IsNullOrEmpty(Sql.UserId))
			{
				if (isGenerateException)
					throw new ArgumentNullException(Sql.UserId, $"{nameof(WsJsonSettingsModel)}.{nameof(Sql.UserId)} IsNullOrEmpty!");
				return false;
			}
			if (string.IsNullOrEmpty(Sql.Password))
			{
				if (isGenerateException)
					throw new ArgumentNullException(Sql.Password, $"{nameof(WsJsonSettingsModel)}.{nameof(Sql.Password)} IsNullOrEmpty!");
				return false;
			}
		}

		if (Version == 0)
		{
			if (isGenerateException)
				throw new ArgumentNullException(Sql.InitialCatalog, $"{nameof(WsJsonSettingsModel)}.{nameof(Version)} == 0!");
			return false;
		}

		return true;
	}

	public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue(nameof(Sql), Sql);
		info.AddValue(nameof(SectionRowsCount), SectionRowsCount);
		info.AddValue(nameof(ItemRowsCount), ItemRowsCount);
		info.AddValue(nameof(SelectTopRowsCount), SelectTopRowsCount);
		info.AddValue(nameof(Version), Version);
		info.AddValue(nameof(AllowedHosts), AllowedHosts);
		info.AddValue(nameof(ConnectionString), ConnectionString);
	}

	#endregion
}