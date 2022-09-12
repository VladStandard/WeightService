// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MissingXmlDoc

namespace DataCore.Sql.Tables;

/// <summary>
/// DB table model.
/// </summary>
public static class SqlTableBaseExt
{
	#region Public and private methods

	public static string GetFieldDt<T>(this T? item, string fieldName) where T : SqlTableBase, new()
	{
		string result = string.Empty;
		if (item is not null && !string.IsNullOrEmpty(fieldName))
		{
			switch (fieldName)
			{
				case nameof(item.CreateDt):
					result = item.CreateDt.ToString("yyyy-MM-dd HH:mm:ss");
					break;
				case nameof(item.ChangeDt):
					result = item.ChangeDt.ToString("yyyy-MM-dd HH:mm:ss");
					break;
				default:
					switch (item)
					{
						case VersionModel version:
							result = fieldName switch
							{
								nameof(version.ReleaseDt) => version.ReleaseDt.ToString("yyyy-MM-dd"),
								_ => string.Empty
							};
							break;
					}
					break;
			};
		}
		return result;
	}


	#endregion
}
