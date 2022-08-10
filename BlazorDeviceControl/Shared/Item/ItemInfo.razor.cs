// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using BlazorCore.Utils;
using DataCore;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Sql;
using DataCore.Utils;
using Microsoft.AspNetCore.Components;

namespace BlazorDeviceControl.Shared.Item;

public partial class ItemInfo
{
    #region Public and private fields, properties, constructor

    private string VerApp => AssemblyUtuls.GetAppVersion(System.Reflection.Assembly.GetExecutingAssembly());
    private string VerLibBlazorCore => BlazorCoreUtuls.GetLibVersion();
    private string VerLibDataCore => AssemblyUtuls.GetLibVersion();
    private List<TypeEntity<ShareEnums.Lang>>? TemplateLanguages { get; set; }
    private List<TypeEntity<bool>> TemplateIsDebug { get; set; } = new();
    private uint DbCurSize { get; set; }
    private string DbCurSizeAsString => $"{DbCurSize:### ###} {LocaleCore.Strings.From} {DbMaxSize:### ###} MB";
    private uint DbMaxSize { get; set; } = 10_240;
    private uint DbFillSize => DbCurSize == 0 ? 0 : DbCurSize * 100 / DbMaxSize;
    private string DbFillSizeAsString => $"{DbFillSize:### ###} %";
    private List<ShareEnums.Lang> Langs { get; set; } = new();

    public ItemInfo()
    {
        Langs = new();
        foreach (ShareEnums.Lang lang in Enum.GetValues(typeof(ShareEnums.Lang)))
            Langs.Add(lang);
    }

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		base.OnParametersSet();
		SetParametersWithAction(new()
		{
			() =>
			{
				TemplateLanguages = AppSettings.DataSourceDics.GetTemplateLanguages();
				TemplateIsDebug = DataSourceDicsEntity.GetTemplateIsDebug();
				object[] objects =
					AppSettings.DataAccess.Crud.GetEntitiesNativeObject(SqlQueries.DbSystem.Properties.GetDbSpace);
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
