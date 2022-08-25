// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models.CssStyles;
using FluentValidation;
using System;
using DataCoreTests;

namespace BlazorCoreTests;

public static class BlazorCoreUtils
{
	#region Public and private methods

	private static IValidator<T> GetStyleValidator<T>(T item) where T : IBaseStyleModel, new()
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

	public static void AssertStyleValidate<T>(T item, bool assertResult) where T : IBaseStyleModel, new()
	{
		// Arrange.
		IValidator<T> validator = GetStyleValidator<T>(item);
		// Act & Assert.
		DataCoreUtils.AssertValidate<T>(item, validator, assertResult);
	}

	#endregion
}
