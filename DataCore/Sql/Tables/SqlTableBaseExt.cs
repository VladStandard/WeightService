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

	public static string GetFieldAsString<T>(this T? item, string fieldName) where T : SqlTableBase, new()
	{
		string result = string.Empty;
		if (item is not null && !string.IsNullOrEmpty(fieldName))
		{
			result = fieldName switch
			{
				nameof(item.CreateDt) => item.CreateDt.ToString("yyyy-MM-dd HH:mm:ss"),
				nameof(item.ChangeDt) => item.ChangeDt.ToString("yyyy-MM-dd HH:mm:ss"),
				nameof(item.Description) => item.Description.ToString(),
				_ => item switch
				{
					ScaleModel scale => fieldName switch
					{
						nameof(scale.Number) => scale.Number.ToString(),
						_ => string.Empty
					},
					VersionModel version => fieldName switch
					{
						nameof(version.ReleaseDt) => version.ReleaseDt.ToString("yyyy-MM-dd"),
						_ => string.Empty
					},
					_ => string.Empty,
				},
			};
			;
		}
		return result;
	}

	#endregion
}
