// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Files;

[Serializable]
public class JsonSettingsModel : ISerializable
{
	#region Public and private fields, properties, constructor

	public JsonSettingsSqlModel Sql { get; set; }
	public int SectionRowsCount { get; set; }
	public int ItemRowsCount { get; set; }
	public int SelectTopRowsCount { get; set; }
	public ushort Version { get; set; }
	public string AllowedHosts { get; set; }
	[NonSerialized] private string _connectionString;
	public string ConnectionString { get => _connectionString; set => _connectionString = value; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jsonSettingsSql"></param>
	/// <param name="isCheckProperties"></param>
	public JsonSettingsModel(JsonSettingsSqlModel jsonSettingsSql, bool isCheckProperties)
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

	/// <summary>
	/// Constructor.
	/// </summary>
	public JsonSettingsModel() : this(new(), false) { }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	protected JsonSettingsModel(SerializationInfo info, StreamingContext context)
	{
		Sql = (JsonSettingsSqlModel)info.GetValue(nameof(Sql), typeof(JsonSettingsSqlModel));
		SectionRowsCount = info.GetInt32(nameof(SectionRowsCount));
		ItemRowsCount = info.GetInt32(nameof(ItemRowsCount));
		SelectTopRowsCount = info.GetInt32(nameof(SelectTopRowsCount));
		Version = info.GetUInt16(nameof(Version));
		AllowedHosts = info.GetString(nameof(AllowedHosts));
		ConnectionString = _connectionString = string.Empty;
	}

	#endregion

	#region Public and private methods

	public override string ToString()
	{
		return 
			Sql +
			$"{nameof(SectionRowsCount)}: {SectionRowsCount}. " +
			$"{nameof(ItemRowsCount)}: {ItemRowsCount}. " +
			$"{nameof(SelectTopRowsCount)}: {SelectTopRowsCount}. " +
			$"{nameof(Version)}: {Version}. " +
			$"{nameof(AllowedHosts)}: {AllowedHosts}. ";
	}

	/// <summary>
	/// Check properties.
	/// </summary>
	/// <param name="isGenerateException"></param>
	/// <returns></returns>
	/// <exception cref="ArgumentNullException"></exception>
	public bool CheckProperties(bool isGenerateException)
	{
		if (string.IsNullOrEmpty(Sql.DataSource))
		{
			if (isGenerateException)
				throw new ArgumentNullException(Sql.DataSource, $"{nameof(JsonSettingsModel)}.{nameof(Sql.DataSource)} IsNullOrEmpty!");
			return false;
		}

		if (string.IsNullOrEmpty(Sql.InitialCatalog))
		{
			if (isGenerateException)
				throw new ArgumentNullException(Sql.InitialCatalog, $"{nameof(JsonSettingsModel)}.{nameof(Sql.InitialCatalog)} IsNullOrEmpty!");
			return false;
		}

		if (!Sql.PersistSecurityInfo)
		{
			if (string.IsNullOrEmpty(Sql.UserId))
			{
				if (isGenerateException)
					throw new ArgumentNullException(Sql.UserId, $"{nameof(JsonSettingsModel)}.{nameof(Sql.UserId)} IsNullOrEmpty!");
				return false;
			}
			if (string.IsNullOrEmpty(Sql.Password))
			{
				if (isGenerateException)
					throw new ArgumentNullException(Sql.Password, $"{nameof(JsonSettingsModel)}.{nameof(Sql.Password)} IsNullOrEmpty!");
				return false;
			}
		}

		if (Version == 0)
		{
			if (isGenerateException)
				throw new ArgumentNullException(Sql.InitialCatalog, $"{nameof(JsonSettingsModel)}.{nameof(Version)} == 0!");
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