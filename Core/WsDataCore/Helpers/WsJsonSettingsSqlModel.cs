// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsDataCore.Helpers;

[Serializable]
public class WsJsonSettingsSqlModel : ISerializable
{
	#region Public and private fields, properties, constructor

	public string DataSource { get; set; }
	public string InitialCatalog { get; set; }
	public bool PersistSecurityInfo { get; set; }
	public bool IntegratedSecurity { get; set; }
	public string UserId { get; set; }
	public string Password { get; set; }
	public string Schema { get; set; }
	public ushort ConnectTimeout { get; set; }
	public bool Encrypt { get; set; }
	public bool TrustServerCertificate { get; set; }

	#endregion

	#region Constructor and destructor

	public WsJsonSettingsSqlModel()
	{
		DataSource = string.Empty;
		InitialCatalog = string.Empty;
		PersistSecurityInfo = false;
		IntegratedSecurity = false;
		UserId = string.Empty;
		Password = string.Empty;
		Schema = string.Empty;
		ConnectTimeout = 15;
		Encrypt = false;
		TrustServerCertificate = false;
	}

	protected WsJsonSettingsSqlModel(SerializationInfo info, StreamingContext context)
	{
		DataSource = info.GetString("Data Source");
		InitialCatalog = info.GetString("Initial Catalog");
		PersistSecurityInfo = info.GetBoolean("Persist Security Info");
		IntegratedSecurity = info.GetBoolean("Integrated Security");
		UserId = info.GetString("User ID");
		Password = info.GetString("Password");
		Schema = info.GetString("Schema");
		Encrypt = info.GetBoolean("Encrypt");
		ConnectTimeout = info.GetUInt16("Connect Timeout");
		TrustServerCertificate = info.GetBoolean("TrustServerCertificate");
	}

	#endregion

	#region Public and private methods

	public override string ToString()
	{
		string strTrusted = IntegratedSecurity 
			? $"{nameof(IntegratedSecurity)}: {IntegratedSecurity}. " 
			: $"{nameof(UserId)}: {UserId}. {nameof(Password)}: {Password}. ";
		return $"{nameof(DataSource)}: {DataSource}. " +
		       $"{nameof(InitialCatalog)}: {InitialCatalog}. " +
		       $"{strTrusted} " +
		       $"{nameof(ConnectTimeout)}: {ConnectTimeout}. " + 
		       $"{nameof(Encrypt)}: {Encrypt}. " + 
		       $"{nameof(TrustServerCertificate)}: {TrustServerCertificate}. ";
	}

	public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("Data Source", DataSource);
		info.AddValue("Initial Catalog", InitialCatalog);
		info.AddValue("Persist Security Info", PersistSecurityInfo);
		info.AddValue("Integrated Security", IntegratedSecurity);
		info.AddValue("User ID", UserId);
		info.AddValue("Password", Password);
		info.AddValue("Schema", Schema);
		info.AddValue("Encrypt", Encrypt);
		info.AddValue("Connect Timeout", ConnectTimeout);
		info.AddValue("TrustServerCertificate", TrustServerCertificate);
	}

	#endregion
}