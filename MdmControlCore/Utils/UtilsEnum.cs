// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using DataCore.Models;

namespace MdmControlCore.Utils;

public static class UtilsEnum
{
	public static bool? GetEnumRelevanceStatusBool(short? value)
	{
		return value switch
		{
			1 => true,
			2 => false,
			_ => null
		};
	}

	public static RelevanceStatusEnum GetEnumRelevanceStatusEng(short? value)
	{
		return value switch
		{
			1 => RelevanceStatusEnum.Actual,
			2 => RelevanceStatusEnum.NoActual,
			_ => RelevanceStatusEnum.Unknown
		};
	}
        
	public static string GetEnumRelevanceStatusRus(short? value)
	{
		return value switch
		{
			1 => "Актуальна",
			2 => "Неактуальна",
			_ => "Неизвестна"
		};
	}
        
	public static NormilizationStatusEnum GetEnumNormalizationStatusEng(short? value)
	{
		return value switch
		{
			1 => NormilizationStatusEnum.NormilizedFull,
			2 => NormilizationStatusEnum.NormilizedPart,
			3 => NormilizationStatusEnum.NotSubjectNormalization,
			_ => NormilizationStatusEnum.NotNormilized
		};
	}
        
	public static string GetEnumNormalizationStatusRus(short? value)
	{
		return value switch
		{
			1 => "Нормализована полностью",
			2 => "Нормализована частично",
			3 => "Не подлежит нормализации",
			_ => "Ненормализована"
		};
	}

	public static IEnumerable<RelevanceStatusEnum> GetEnumRelevenaceStatusesEng()
	{
		return (RelevanceStatusEnum[]) Enum.GetValues(typeof(RelevanceStatusEnum));
	}
        
	public static IEnumerable<TypeModel<short>> GetEnumRelevenaceStatusesRus()
	{
		List<TypeModel<short>> result = new List<TypeModel<short>>
		{
			new("Неизвестно", 0),
			new("Актуально", 1),
			new("Неактуально", 2),
		};
		return result;
	}
        
	public static IEnumerable<NormilizationStatusEnum> GetEnumNormilizationStatusesEng()
	{
		return (NormilizationStatusEnum[]) Enum.GetValues(typeof(NormilizationStatusEnum));
	}
        
	public static IEnumerable<TypeModel<short>> GetEnumNormilizationStatusesRus()
	{
		List<TypeModel<short>> result = new List<TypeModel<short>>
		{
			new("Ненормализована", 0),
			new("Нормализована полностью", 1),
			new("Нормализована частично", 2),
			new("Не подлежит нормализации", 3),
		};
		return result;
	}
}