// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Reflection;

namespace BlazorDeviceControl.Pages.SectionComponents.Admins;

public sealed partial class SystemAppInfo : RazorComponentBase
{
	#region Public and private fields, properties, constructor

	private string VerApp => AssemblyUtuls.GetAppVersion(Assembly.GetExecutingAssembly());
	private string VerLibBlazorCore => BlazorCoreUtils.GetLibVersion();
	private string VerLibDataCore => AssemblyUtuls.GetLibVersion();
    private uint DbFileSizeAll { get; set; }
    private List<SqlDbFileSizeInfoModel> DbSizeInfos { get; set; } = new();
	private string DbCurSizeAsString => $"{LocaleCore.Sql.SqlDbCurSize}: {DbFileSizeAll:### ###} MB {LocaleCore.Strings.From} {DbMaxSize:### ###} MB";
	private uint DbMaxSize => 10_240;
	private uint DbFillSize => DbFileSizeAll == 0 ? 0 : DbFileSizeAll * 100 / DbMaxSize;

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
            {
                DbSizeInfos = DataContext.GetDbFileSizeInfos();
                DbFileSizeAll = DataContext.GetDbFileSizeAll();
			}
		});
	}

	#endregion
}
