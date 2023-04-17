// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Utils;

internal static class WsSqlQueriesBarcodes
{
    public static string FindBottom => @"
SELECT
	 [B].[IS_MARKED]
	,[B].[CREATE_DT]
	,[B].[TYPE_TOP]
	,[B].[VALUE_TOP]
	,[B].[TYPE_RIGHT]
	,[B].[VALUE_RIGHT]
	,[B].[TYPE_BOTTOM]
	,[B].[VALUE_BOTTOM]
	,[PL].[ZPL]
	--,[PL].[XML]
FROM [db_scales].[BARCODES] [B]
INNER JOIN [db_scales].[PLUS_LABELS] [PL] ON [B].[PLU_LABEL_UID] = [PL].[UID]
WHERE [VALUE_BOTTOM] = :VALUE_BOTTOM;
        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

    public static string FindRight => @"
SELECT
	 [B].[IS_MARKED]
	,[B].[CREATE_DT]
	,[B].[TYPE_TOP]
	,[B].[VALUE_TOP]
	,[B].[TYPE_RIGHT]
	,[B].[VALUE_RIGHT]
	,[B].[TYPE_BOTTOM]
	,[B].[VALUE_BOTTOM]
	,[PL].[ZPL]
	--,[PL].[XML]
FROM [db_scales].[BARCODES] [B]
INNER JOIN [db_scales].[PLUS_LABELS] [PL] ON [B].[PLU_LABEL_UID] = [PL].[UID]
WHERE [VALUE_RIGHT] = :VALUE_RIGHT;
        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

    public static string FindTop => @"
SELECT
	 [B].[IS_MARKED]
	,[B].[CREATE_DT]
	,[B].[TYPE_TOP]
	,[B].[VALUE_TOP]
	,[B].[TYPE_RIGHT]
	,[B].[VALUE_RIGHT]
	,[B].[TYPE_BOTTOM]
	,[B].[VALUE_BOTTOM]
	,[PL].[ZPL]
	--,[PL].[XML]
FROM [db_scales].[BARCODES] [B]
INNER JOIN [db_scales].[PLUS_LABELS] [PL] ON [B].[PLU_LABEL_UID] = [PL].[UID]
WHERE [VALUE_TOP] = :VALUE_TOP;
        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

}