// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Sql;
using Microsoft.Data.SqlClient;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace WeightCore.Helpers
{
    /// <summary>
    /// Application helper.
    /// </summary>
    public sealed class AppHelper : BaseViewModel
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static AppHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static AppHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public fields and properties

        public SqlHelper SqlHelp { get; } = SqlHelper.Instance;

        private string _status;
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }
        public Mutex InstanceCheckMutex { get; set; }

        #endregion

        #region Constructor and destructor

        public AppHelper()
        {
            _status = string.Empty;
        }

        #endregion

        #region Public and private methods

        public bool IsAdministrator()
        {
            using WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public string GetDescription(Assembly assembly)
        {
            string result = string.Empty;
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                AssemblyDescriptionAttribute descriptionAttribute = (AssemblyDescriptionAttribute)attributes[0];
                result = descriptionAttribute.Description;
            }
            return result;
        }

        public string GetCurrentVersion(ShareEnums.AppVerCountDigits countDigits, List<ShareEnums.AppVerStringFormat> stringFormats = null, Version version = null)
        {
            if (version == null)
                version = Assembly.GetExecutingAssembly().GetName().Version;
            string version1;
            string version2;
            string version3;
            string version4;
            if (stringFormats == null || stringFormats.Count == 0)
                stringFormats = new List<ShareEnums.AppVerStringFormat>() { ShareEnums.AppVerStringFormat.Use1, ShareEnums.AppVerStringFormat.Use2, ShareEnums.AppVerStringFormat.Use2 };

            ShareEnums.AppVerStringFormat formatMajor = stringFormats[0];
            ShareEnums.AppVerStringFormat formatMinor = ShareEnums.AppVerStringFormat.AsString;
            ShareEnums.AppVerStringFormat formatBuild = ShareEnums.AppVerStringFormat.AsString;
            ShareEnums.AppVerStringFormat formatRevision = ShareEnums.AppVerStringFormat.AsString;
            if (stringFormats.Count > 1)
                formatMinor = stringFormats[1];
            if (stringFormats.Count > 2)
                formatBuild = stringFormats[2];
            if (stringFormats.Count > 3)
                formatRevision = stringFormats[3];

            string major = GetCurrentVersionFormat(version.Major, formatMajor);
            string minor = GetCurrentVersionFormat(version.Minor, formatMinor);
            string build = GetCurrentVersionFormat(version.Build, formatBuild);
            string revision = GetCurrentVersionFormat(version.Revision, formatRevision);
            version4 = $"{major}.{minor}.{build}.{revision}";
            version3 = $"{major}.{minor}.{build}";
            version2 = $"{major}.{minor}";
            version1 = $"{major}";

            return countDigits == ShareEnums.AppVerCountDigits.Use1
                ? version1 : countDigits == ShareEnums.AppVerCountDigits.Use2
                ? version2 : countDigits == ShareEnums.AppVerCountDigits.Use3
                ? version3 : version4;
        }

        public string GetCurrentVersionSubString(string input)
        {
            string result = string.Empty;
            int idx = input.LastIndexOf('.');
            if (idx >= 0)
                result = input.Substring(0, idx);
            return result;
        }

        /// <summary>
        /// Get version string.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public string GetCurrentVersionFormat(int input, ShareEnums.AppVerStringFormat format)
        {
            return format switch
            {
                ShareEnums.AppVerStringFormat.Use1 => $"{input:D1}",
                ShareEnums.AppVerStringFormat.Use2 => $"{input:D2}",
                ShareEnums.AppVerStringFormat.Use3 => $"{input:D3}",
                ShareEnums.AppVerStringFormat.Use4 => $"{input:D4}",
                _ => $"{input:D}",
            };
        }

        /// <summary>
        /// Checl instance run.
        /// </summary>
        /// <returns></returns>
        public bool CheckInstance()
        {
            InstanceCheckMutex = new Mutex(true, "ScalesUI", out bool result);
            return result;
        }

        /// <summary>
        /// Set new form size.
        /// </summary>
        /// <param name="form"></param>
        /// <param name="startPosition"></param>
        public void SetNewSize(Form form, FormStartPosition startPosition = FormStartPosition.CenterScreen)
        {
            if (form == null)
                return;

            if (Application.OpenForms.Count > 0)
            {
                form.Width = Application.OpenForms[0].Width;
                form.Height = Application.OpenForms[0].Height;
                form.Left = Application.OpenForms[0].Left;
                form.Top = Application.OpenForms[0].Top;
            }
            form.StartPosition = startPosition;
        }
        

        /// <summary>
        /// Check SQL-connection.
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        //public bool SqlConCheck(ShareEnums.Lang language)
        //{
        //    if (string.IsNullOrEmpty(SqlHelp.Authentication.Server) || string.IsNullOrEmpty(SqlHelp.Authentication.Database))
        //    {
        //        Status = LocaleCore.Sql.StatusExceptionConnect();
        //        return false;
        //    }

        //    if (!SqlHelp.Authentication.IntegratedSecurity && 
        //        (string.IsNullOrEmpty(SqlHelp.Authentication.UserId) || string.IsNullOrEmpty(SqlHelp.Authentication.Password)))
        //    {
        //        Status = LocaleCore.Sql.StatusExceptionConnect();
        //        return false;
        //    }

        //    SqlHelp.Open();
        //    SqlHelp.OpenConnection();
        //    if (SqlHelp.Connection.State == System.Data.ConnectionState.Open)
        //    {
        //        Status = LocaleCore.Sql.StatusConnected;
        //        return true;
        //    }
        //    return false;
        //}

        /// <summary>
        /// Check guid exists.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public bool SqlExistsGuid(string guid)
        {
            if (SqlHelp.Connection.State == System.Data.ConnectionState.Open)
            {
                Collection<Collection<object>> records = SqlHelp.SelectData(SqlQueries.DbScales.Tables.Scales.QueryFindGuid,
                    new Collection<string>() { "RESULT" },
                    new Collection<SqlParameter>() { new SqlParameter("GUID", guid) });
                foreach (Collection<object> rec in records)
                {
                    foreach (object field in rec)
                    {
                        if (field.ToString().Equals("TRUE", StringComparison.InvariantCultureIgnoreCase))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        #endregion
    }
}
