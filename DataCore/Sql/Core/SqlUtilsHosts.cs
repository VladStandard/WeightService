// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableDirectModels;
using DataCore.Sql.Tables;

namespace DataCore.Sql.Core;

public static partial class SqlUtils
{
	#region Public and private methods

	public static HostDirect LoadReader(SqlDataReader reader)
	{
		HostDirect result = new();
		if (reader.Read())
		{
			result.Id = SqlConnect.GetValueAsNotNullable<int>(reader, "ID");
			result.Name = SqlConnect.GetValueAsNullable<string>(reader, "NAME");
			result.HostName = SqlConnect.GetValueAsNullable<string>(reader, "HOSTNAME");
			result.Ip = SqlConnect.GetValueAsNullable<string>(reader, "IP");
			result.Mac = SqlConnect.GetValueAsNullable<string>(reader, "MAC");
			result.IdRRef = SqlConnect.GetValueAsNotNullable<Guid>(reader, "IDRREF");
			result.IsMarked = SqlConnect.GetValueAsNotNullable<bool>(reader, "MARKED");
			result.ScaleId = SqlConnect.GetValueAsNotNullable<int>(reader, "SCALE_ID");
		}
		return result;
	}

	public static HostModel? GetHost(string hostName)
	{
		SqlCrudConfigModel sqlCrudConfig = new(new()
            {
                new(nameof(HostModel.HostName), SqlFieldComparerEnum.Equal, hostName), 
                new(nameof(SqlTableBase.IsMarked), SqlFieldComparerEnum.Equal, false)
            },
			new(nameof(SqlTableBase.CreateDt), SqlFieldOrderEnum.Desc), 0);
		return DataAccess.GetItem<HostModel>(sqlCrudConfig);
	}

	public static HostDirect Load(Guid uid) =>
		SqlConnect.ExecuteReaderForItem(SqlQueries.DbScales.Tables.Hosts.GetHostByUid,
			new SqlParameter("@idrref", SqlDbType.UniqueIdentifier) { Value = uid }, LoadReader);

	public static HostDirect Load(string hostName) =>
		SqlConnect.ExecuteReaderForItem(SqlQueries.DbScales.Tables.Hosts.GetHostByHostName,
			new SqlParameter("@HOST_NAME", SqlDbType.NVarChar, 255) { Value = hostName }, LoadReader);

	public static HostDirect GetHostDirect()
	{
		if (!File.Exists(FilePathToken))
		{
			return new();
		}
		XDocument doc = XDocument.Load(FilePathToken);
		Guid idrref = Guid.Parse(doc.Root.Elements("ID").First().Value);
		//string EncryptConnectionString = doc.Root.Elements("EncryptConnectionString").First().Value;
		//string connectionString = EncryptDecryptUtil.Decrypt(EncryptConnectionString);
		return Load(idrref);
	}

	public static HostDirect GetHostDirect(string hostName) => Load(hostName);

	public static bool CheckHostUidInFile()
	{
		if (!File.Exists(FilePathToken))
			return false;

		XDocument doc = XDocument.Load(FilePathToken);
		Guid idrref = Guid.Parse(doc.Root.Elements("ID").First().Value);
		bool result = default;
		SqlConnect.ExecuteReader(SqlQueries.DbScales.Tables.Hosts.GetHostIdByIdRRef,
			new SqlParameter("@idrref", SqlDbType.UniqueIdentifier) { Value = idrref }, (reader) =>
			{
				result = reader.Read();
			});
		return result;
	}

	#endregion
}
