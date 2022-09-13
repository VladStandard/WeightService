// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MissingXmlDoc

using NHibernate.Cfg;
using System.Globalization;

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
				nameof(item.Identity) => item.Identity.GetValueAsString(),
				nameof(item.CreateDt) => item.CreateDt.ToString("yyyy-MM-dd HH:mm:ss"),
				nameof(item.ChangeDt) => item.ChangeDt.ToString("yyyy-MM-dd HH:mm:ss"),
				nameof(item.Description) => item.Description.ToString(),
				_ => item switch
				{
					AccessModel access => fieldName switch
					{
						nameof(access.User) => access.User,
						nameof(access.Rights) => DataAccessHelper.Instance.GetAccessRightsDescription(access.Rights),
						_ => string.Empty
					},
					PluModel plu => fieldName switch
					{
						nameof(plu.Name) => plu.Name,
						nameof(plu.ShelfLifeDays) => plu.ShelfLifeDays.ToString(CultureInfo.InvariantCulture),
						nameof(plu.TareWeight) => plu.TareWeight.ToString(CultureInfo.InvariantCulture),
						nameof(plu.BoxQuantly) => plu.BoxQuantly.ToString(CultureInfo.InvariantCulture),
						_ => string.Empty
					},
					PluLabelModel pluLabel => fieldName switch
					{
						_ => string.Empty
					},
					PluScaleModel pluScale => fieldName switch
					{
						_ => string.Empty
					},
					PluWeighingModel pluWeighing => fieldName switch
					{
						_ => string.Empty
					},
					ScaleModel scale => fieldName switch
					{
						nameof(scale.Number) => scale.Number.ToString(CultureInfo.InvariantCulture),
						nameof(scale.Host) => scale.Host is not null ? scale.Host.Name : LocaleCore.Table.FieldNull,
						nameof(scale.DeviceIp) => scale.DeviceIp,
						_ => string.Empty
					},
					VersionModel version => fieldName switch
					{
						nameof(version.ReleaseDt) => version.ReleaseDt.ToString("yyyy-MM-dd"),
						nameof(version.Version) => version.Version.ToString(),
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
