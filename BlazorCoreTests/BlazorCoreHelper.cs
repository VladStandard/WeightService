// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.CssStyles;
using BlazorCore.Models;
using DataCoreTests;
using FluentValidation;
using NUnit.Framework;
using System;
using System.Threading;
using TableBase = DataCore.Sql.Tables.TableBase;

namespace BlazorCoreTests;

public class BlazorCoreHelper
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private static BlazorCoreHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public static BlazorCoreHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	#region Public and private fields, properties, constructor

	private DataCoreHelper Helper { get; } = DataCoreHelper.Instance;

	#endregion


	#region Public and private methods

	private IValidator<T> GetStyleValidator<T>(T item) where T : CssStyleBase, new()
	{
		return item switch
		{
			CssStyleRadzenColumnModel => new CssStyleRadzenColumnValidator(),
			CssStyleTableHeadModel => new CssStyleTableHeadValidator(),
			CssStyleTableBodyModel => new CssStyleTableBodyValidator(),
			_ => throw new NotImplementedException()
		};
	}

	public void AssertStyleValidate<T>(T item, bool assertResult) where T : CssStyleBase, new()
	{
		// Arrange.
		IValidator<T> validator = GetStyleValidator<T>(item);
		// Act & Assert.
		DataCoreHelper.Instance.AssertValidate<T>(item, validator, assertResult);
	}

	public void Model_GetRoutePath_IsNotEmpty<T>() where T : TableBase, new()
	{
		// Arrange.
		RazorPageBase razorPage = new();
		//AccessModel item = Substitute.For<AccessModel>();
		T item = Helper.CreateNewSubstitute<T>(true);
		// Act.
		string url = razorPage.GetRoutePath(item);
		TestContext.WriteLine(url);
		// Assert.
		Assert.IsNotEmpty(url);
	}

	#endregion
}
