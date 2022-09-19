// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.SystemComponents;

public partial class SystemAppInfo : RazorComponentBase
{
	#region Public and private fields, properties, constructor

	private string VerApp => AssemblyUtuls.GetAppVersion(global::System.Reflection.Assembly.GetExecutingAssembly());
	private string VerLibBlazorCore => BlazorCoreUtuls.GetLibVersion();
	private string VerLibDataCore => AssemblyUtuls.GetLibVersion();
	private List<TypeModel<bool>> TemplateIsDebug { get; set; } = new();
	private uint DbCurSize { get; set; }
	private string DbCurSizeAsString => $"{LocaleCore.Sql.SqlDbCurSize}: {DbCurSize:### ###} MB {LocaleCore.Strings.From} {DbMaxSize:### ###} MB";
	private uint DbMaxSize => 10_240;
	private uint DbFillSize => DbCurSize == 0 ? 0 : DbCurSize * 100 / DbMaxSize;

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
				TemplateIsDebug = AppSettings.DataSourceDics.GetTemplateIsDebug();
				object[] objects = AppSettings.DataAccess.GetObjects(SqlQueries.DbSystem.Properties.GetDbSpace);
				DbCurSize = 0;
				foreach (object obj in objects)
				{
					if (obj is object[] { Length: 5 } item)
					{
						if (uint.TryParse(Convert.ToString(item[2]), out uint dbSizeMb))
						{
							DbCurSize += dbSizeMb;
						}
					}
				}
			}
		});
	}

	#endregion
}
