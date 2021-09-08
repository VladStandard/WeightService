// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;
using ScalesCore.Win.Registry.Entities;
using ScalesCore.Win.Registry.Utils;

namespace ScalesCore.Win.Registry.Helpers
{
    /// <summary>
    /// Помощник реестра.
    /// </summary>
    public sealed class RegistryHelper
    {
        #region Design pattern "Singleton"

       // private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly Lazy<RegistryHelper> _instance = new Lazy<RegistryHelper>(() => new RegistryHelper());
        public static RegistryHelper Instance => _instance.Value;
        private RegistryHelper() { }

        #endregion

        #region Public fields and properties

        /// <summary>
        /// Ветка реестра по-умолчанию.
        /// </summary>
        public EnumRegRoot Root { get; } = EnumRegRoot.CurrentUser;

        #endregion

        #region Public methods

        public RegistryKey GetRegistryKey(EnumRegRoot regRoot)
        {
            switch (regRoot)
            {
                case EnumRegRoot.ClassesRoot:
                    return Microsoft.Win32.Registry.ClassesRoot;
                case EnumRegRoot.CurrentUser:
                    return Microsoft.Win32.Registry.CurrentUser;
                case EnumRegRoot.LocalMachine:
                    return Microsoft.Win32.Registry.LocalMachine;
                case EnumRegRoot.Users:
                    return Microsoft.Win32.Registry.Users;
                case EnumRegRoot.CurrentConfig:
                    return Microsoft.Win32.Registry.CurrentConfig;
            }
            throw new ArgumentOutOfRangeException(nameof(regRoot), regRoot, null);
        }

        public RegistryHive GetRegistryHive(EnumRegRoot regRoot)
        {
            switch (regRoot)
            {
                case EnumRegRoot.ClassesRoot:
                    return RegistryHive.ClassesRoot;
                case EnumRegRoot.CurrentUser:
                    return RegistryHive.CurrentUser;
                case EnumRegRoot.LocalMachine:
                    return RegistryHive.LocalMachine;
                case EnumRegRoot.Users:
                    return RegistryHive.Users;
                case EnumRegRoot.CurrentConfig:
                    return RegistryHive.CurrentConfig;
            }
            throw new ArgumentOutOfRangeException(nameof(regRoot), regRoot, null);
        }

        /// <summary>
        /// Проверить наличие раздела.
        /// </summary>
        /// <param name="regRoot"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exists(EnumRegRoot regRoot, string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                RegistryKey rk = GetRegistryKey(regRoot);
                RegistryKey subKey = rk?.OpenSubKey(key, true);
                if (subKey != null)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Проверить наличие параметра.
        /// </summary>
        /// <param name="regRoot"></param>
        /// <param name="key"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool Exists(EnumRegRoot regRoot, string key, string parameter)
        {
            if (!string.IsNullOrEmpty(key))
            {
                RegistryKey rk = GetRegistryKey(regRoot);
                RegistryKey subKey = rk?.OpenSubKey(key, true);
                if (subKey != null)
                {
                    foreach (string item in subKey.GetValueNames())
                    {
                        if (item.Equals(parameter, StringComparison.InvariantCultureIgnoreCase))
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Получить значение параметра.
        /// </summary>
        /// <param name="regRoot"></param>
        /// <param name="key"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public T GetValue<T>(EnumRegRoot regRoot, string key, string parameter)
        {

            //log.Debug(key +":"+ parameter);
            if (!string.IsNullOrEmpty(key))
            {
                RegistryKey rk = GetRegistryKey(regRoot);
                RegistryKey subKey = rk?.OpenSubKey(key, true);
                if (subKey != null)
                {
                    foreach (string item in subKey.GetValueNames())
                    {
                        if (item.Equals(parameter, StringComparison.InvariantCultureIgnoreCase))
                            //result = (T)subKey.GetValue(parameter).ToString();
                            return (T)subKey.GetValue(parameter);
                    }
                }
            }
            return default;
        }

        /// <summary>
        /// Задать значение параметра.
        /// </summary>
        /// <param name="regRoot"></param>
        /// <param name="key"></param>
        /// <param name="parameter"></param>
        /// <param name="value"></param>
        /// <param name="valueKind"></param>
        public bool SetValue<T>(EnumRegRoot regRoot, string key, string parameter, T value, RegistryValueKind valueKind)
        {
            if (!string.IsNullOrEmpty(key))
            {
                RegistryKey rk = GetRegistryKey(regRoot);
                RegistryKey subKey = rk?.OpenSubKey(key, true);
                if (subKey != null)
                {
                    subKey.SetValue(parameter, value, valueKind);
                    subKey.Close();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Создать раздел.
        /// </summary>
        /// <param name="regRoot"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool CreateSubKey(EnumRegRoot regRoot, string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                RegistryKey rk = GetRegistryKey(regRoot);
                RegistryKey subKey = rk?.CreateSubKey(key, RegistryKeyPermissionCheck.ReadWriteSubTree);
                if (subKey == null)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Создать параметр.
        /// </summary>
        /// <param name="regRoot"></param>
        /// <param name="key"></param>
        /// <param name="parameter"></param>
        /// <param name="permissionCheck"></param>
        /// <returns></returns>
        public bool CreateParameter(EnumRegRoot regRoot, string key, string parameter, RegistryKeyPermissionCheck permissionCheck = RegistryKeyPermissionCheck.Default)
        {
            if (!string.IsNullOrEmpty(key))
            {
                RegistryKey rk = GetRegistryKey(regRoot);
                RegistryKey subKey = rk?.CreateSubKey(key, RegistryKeyPermissionCheck.ReadWriteSubTree);
                if (subKey != null)
                {
                    if (!subKey.GetValueNames().Contains(parameter))
                    {
                        rk.CreateSubKey(parameter, permissionCheck);
                        return true;
                    }
                }
            }
            return false;
        }

        public List<string> GetSubKeys(EnumRegRoot regRoot, string key, RegistryView registryView)
        {
            List<string> result = new List<string>();

            if (!string.IsNullOrEmpty(key))
            {
                RegistryKey rk = GetRegistryKey(regRoot);
                if (rk != null)
                {
                    RegistryKey subKey = RegistryKey.OpenBaseKey(GetRegistryHive(regRoot), registryView).OpenSubKey(key, true);
                    if (subKey != null)
                    {
                        result = rk.GetSubKeyNames().ToList();
                    }
                }
            }
            return result;
        }

        public void WindowsSystem(string param, object value, RegistryValueKind valueKind)
        {
            SetValue(EnumRegRoot.CurrentUser, Constants.Software.Policies.Microsoft.Windows.System.Get(), param, value, valueKind);
            SetValue(EnumRegRoot.LocalMachine, Constants.Software.Policies.Microsoft.Windows.System.Get(), param, value, valueKind);
        }

        public void PoliciesExplorer(string param, object value, RegistryValueKind valueKind)
        {
            SetValue(EnumRegRoot.CurrentUser, Constants.Software.Microsoft.Windows.CurrentVersion.Policies.Explorer.Get(), param, value, valueKind);
            SetValue(EnumRegRoot.LocalMachine, Constants.Software.Microsoft.Windows.CurrentVersion.Policies.Explorer.Get(), param, value, valueKind);
        }

        public void PoliciesSystem(string param, object value, RegistryValueKind valueKind)
        {
            SetValue(EnumRegRoot.CurrentUser, Constants.Software.Microsoft.Windows.CurrentVersion.Policies.System.Get(), param, value, valueKind);
            SetValue(EnumRegRoot.LocalMachine, Constants.Software.Microsoft.Windows.CurrentVersion.Policies.System.Get(), param, value, valueKind);
        }

        public void Winlogon(string param, object value, RegistryValueKind valueKind)
        {
            SetValue(EnumRegRoot.CurrentUser, Constants.Software.Microsoft.WindowsNT.CurrentVersion.Winlogon.Get(), param, value, valueKind);
            SetValue(EnumRegRoot.LocalMachine, Constants.Software.Microsoft.WindowsNT.CurrentVersion.Winlogon.Get(), param, value, valueKind);
        }

        #endregion
    }
}
