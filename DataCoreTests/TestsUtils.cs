// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql;
using DataCore.Protocols;
using System.IO;
using DataCore.Files;
using FluentValidation.Results;
using NUnit.Framework;
using DataCore.Sql.TableScaleModels;
using System.Linq;
using System;

namespace DataCoreTests;

public static class TestsUtils
{
    #region Public and private fields, properties, constructor

    public static DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
    public static SqlConnectFactory SqlConnect { get; } = SqlConnectFactory.Instance;

	/// <summary>
	/// Constructor.
	/// </summary>
	static TestsUtils()
    {
	    SetupDebug();
    }

	public static void SetupDebug()
    {
        DataAccess.JsonControl.SetupForTests(Directory.GetCurrentDirectory(),
            NetUtils.GetLocalHostName(true), nameof(DataCoreTests), JsonSettingsController.FileNameDebug);
        TestContext.WriteLine(DataAccess.JsonSettingsLocal);
    }

	public static void SetupRelease()
	{
		DataAccess.JsonControl.SetupForTests(Directory.GetCurrentDirectory(),
            NetUtils.GetLocalHostName(true), nameof(DataCoreTests), JsonSettingsController.FileNameRelease);
		TestContext.WriteLine(DataAccess.JsonSettingsLocal);
	}

	public static void DbTableAction(Action action)
	{
		Assert.DoesNotThrow(() =>
		{
			for (int i = 0; i < 2; i++)
			{
				if (i == 0)
					TestsUtils.SetupRelease();
				else if (i == 1)
				{
					TestContext.WriteLine();
					TestsUtils.SetupDebug();
				}

				action.Invoke();
			}
		});
	}

	public static void FailureWriteLine(ValidationResult result)
    {
        if (!result.IsValid)
		    foreach (ValidationFailure? failure in result.Errors)
		    {
			    TestContext.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
		    }
	}

    #endregion
}
