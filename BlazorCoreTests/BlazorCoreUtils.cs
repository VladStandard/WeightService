// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentValidation;
using System;
using BlazorCore.CssStyles;
using DataCoreTests;

namespace BlazorCoreTests;

public static class BlazorCoreUtils
{
	#region Public and private methods

	private static IValidator<T> GetStyleValidator<T>(T item) where T : TableStyleModel, new()
	{
		switch (item)
		{
			case TableHeadStyleModel:
				return new TableHeadStyleValidator();
			case TableBodyStyleModel:
				return new TableBodyStyleValidator();
		}
		throw new NotImplementedException();
	}

	public static void AssertStyleValidate<T>(T item, bool assertResult) where T : TableStyleModel, new()
	{
		// Arrange.
		IValidator<T> validator = GetStyleValidator<T>(item);
		// Act & Assert.
		DataCoreHelper.Instance.AssertValidate<T>(item, validator, assertResult);
	}

	#endregion
}
